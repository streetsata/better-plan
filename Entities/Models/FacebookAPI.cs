﻿using Entities.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Entities.Models.auxiliary_models;
using RestSharp;

namespace Entities.Models
{
    public class FacebookAPI
    {
        private static readonly string _facebookAPI = "https://graph.facebook.com/";
        private static readonly string _pageEdgeFeed = "feed";
        private static readonly string _pageEdgePhotos = "photos";
        private static readonly string _getUserURL = $"{_facebookAPI}/me";

        private readonly string _pageAccessToken;
        private readonly string _feedPageURL;
        private readonly string _photosPageURL;

        public FacebookAPI(string accessToken, string pageID)
        {
            _pageAccessToken = accessToken;
            _feedPageURL = $"{_facebookAPI}{pageID}/{_pageEdgeFeed}";
            _photosPageURL = $"{_facebookAPI}{pageID}/{_pageEdgePhotos}";
        }

        /// <summary>
        /// Получает ID о пользователе по его токену
        /// </summary>
        /// <param name="UserToken"></param>
        /// <returns>
        /// Возвращает кортеж который состоит из 1. id 2. имени пользователя
        /// </returns>
        private static async Task<string> GetUserAsyncID(string UserToken)
        {
            using (var http = new HttpClient())
            {

                var httpResponse = await http.GetAsync($"{_getUserURL}?access_token={UserToken}");
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                var Json = JObject.Parse(httpContent);

                var result = Json["id"].ToString();
                return result;
            }
        }

        /// <summary>
        /// Получает инфу о пользователе по его токену
        /// </summary>
        /// <param name="UserToken"></param>
        /// <returns>
        /// Возвращает кортеж который состоит из 1. id 2. имени пользователя
        /// </returns>
        public static async Task<JsonResult> GetUserAsync(string UserToken)
        {
            using (var http = new HttpClient())
            {
                string id = await GetUserAsyncID(UserToken);

                var httpResponse = await http.GetAsync($"{_getUserURL}/accounts?access_token={UserToken}&fields=picture{{url}},cover,name");
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                var Json = JObject.Parse(httpContent);

                var res = InfoUser.GetJSON(Json, id);

                return new JsonResult(new { Id = res["id"], name = res["name"], picture = res["picture"], cover = res["cover"] });
            }
        }


        /// <summary>
        /// Возвращает посты бизнес аккаунта пользователя по его токену
        /// </summary>
        /// <param name="UserToken"></param>
        /// <returns>
        /// Возвращает JObject Json в котором находится массив постов
        /// </returns>
        public static async Task<JToken> GetPagePostsAsync(string UserToken)
        {
            using (var http = new HttpClient())
            {
                var pageInfo = GetPageIdAndTokenAsync(UserToken).Result;
                var httpResponse = await http.GetAsync($"{_facebookAPI}{pageInfo.Item1}/{_pageEdgeFeed}?access_token={pageInfo.Item2}&fields=attachments{{media,subattachments}},place,id,full_picture,message");
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var Json = JObject.Parse(httpContent)["data"];

                return Json;
            }
        }

        /// <summary>
        /// Получает id и токен бизнес страницы пользователя, по токену пользователя
        /// </summary>
        /// <param name="UserToken"></param>
        /// <returns>
        /// Возвращает кортеж из 1. id 2. токен страницы
        /// </returns>
        public static async Task<Tuple<string, string>> GetPageIdAndTokenAsync(string UserToken)
        {
            using (var http = new HttpClient())
            {
                string url = $"{_getUserURL}/accounts?access_token={UserToken}";

                var httpResponse = await http.GetAsync(url);
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                var Json = JObject.Parse(httpContent);



                return new Tuple<string, string>(
                    Json["data"][0]["id"].ToString(),
                    Json["data"][0]["access_token"].ToString()
                );
            }
        }

        /// <summary>
        /// Постит на Лицокнигу(с) 
        /// </summary>
        /// <param name="post"></param>
        /// <returns>
        /// Возвращает кортеж 1. Результат выполнения 2. Сообщение ошибки или id
        /// </returns>
        public async Task<Tuple<int, string>> PostToFacebookAsync(PostViewModel post)
        {

            using (var http = new HttpClient())
            {
                var data = new Dictionary<string, string> {
                    { "access_token", _pageAccessToken }
                };

                if (post.post_text != null)
                {
                    data.Add("message", post.post_text);
                }

                //if (post.action != null && post.objectAction != null)
                //{
                //    data.Add("og_action_type_id", post.action);
                //    data.Add("og_object_id", post.objectAction);
                //    if (post.icon != null)
                //    {
                //        data.Add("og_icon_id", post.icon);
                //    }
                //}

                if (post.ImagesListIFormFile != null)
                {
                    RestClient client = new RestClient(_photosPageURL);
                    RestRequest request;
                    List<string> idList = new List<string>();

                    for (int i = 0; i < post.ImagesListIFormFile.Count; i++)
                    {
                        if (post.ImagesListIFormFile[i].Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                post.ImagesListIFormFile[i].CopyTo(ms);
                                byte[] bytes = ms.ToArray();
                                // act on the Base64 data
                                request = new RestRequest(Method.POST);
                                request.AddFileBytes($"{post.ImagesListIFormFile[i].Name}+{i}", bytes, post.ImagesListIFormFile[i].FileName, "multipart/form-data");
                                request.AddParameter("published", false);
                                request.AddParameter("access_token", _pageAccessToken);
                                var resp = client.Post(request);
                                var rezImageJson = JObject.Parse(resp.Content);
                                string postID = rezImageJson["id"].Value<string>();

                                idList.Add(postID);
                            }
                        }
                    }

                    var postImagesData = new List<Object>();

                    request = new RestRequest(Method.POST);

                    for (int i = 0; i < idList.Count; i++)

                    {
                        postImagesData.Add(new { media_fbid = idList[i].ToString() });
                    }
                    string json = JsonConvert.SerializeObject(postImagesData);

                    data.Add("attached_media", json);

                    // В идеале вот это одна строчка
                    //data.Add("attached_media", ListJSON);
                }

                var httpResponse = await http.PostAsync(_feedPageURL, new FormUrlEncodedContent(data));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var Json = JObject.Parse(httpContent);
                if (Json["error"] != null)
                {
                    return new Tuple<int, string>(400, Json["error"]["message"].ToString());
                }

                return new Tuple<int, string>(200, Json["id"].ToString());
            }
        }

        public async Task<Tuple<int, string>> PostToFacebookAsync(PostViewModelFiles post)
        {

            using (var http = new HttpClient())
            {
                var data = new Dictionary<string, string> {
                    { "access_token", _pageAccessToken }
                };

                if (post.post_text != null)
                {
                    data.Add("message", post.post_text);
                }

                //if (post.action != null && post.objectAction != null)
                //{
                //    data.Add("og_action_type_id", post.action);
                //    data.Add("og_object_id", post.objectAction);
                //    if (post.icon != null)
                //    {
                //        data.Add("og_icon_id", post.icon);
                //    }
                //}

                if (post.ImagesListIFormFile != null)
                {
                    RestClient client = new RestClient(_photosPageURL);
                    RestRequest request;
                    List<string> idList = new List<string>();

                    for (int i = 0; i < post.ImagesListIFormFile.Count; i++)
                    {
                        if (post.ImagesListIFormFile[i].Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                post.ImagesListIFormFile[i].CopyTo(ms);
                                byte[] bytes = ms.ToArray();
                                // act on the Base64 data
                                request = new RestRequest(Method.POST);
                                request.AddFileBytes($"{post.ImagesListIFormFile[i].Name}+{i}", bytes, post.ImagesListIFormFile[i].FileName, "multipart/form-data");
                                request.AddParameter("published", false);
                                request.AddParameter("access_token", _pageAccessToken);
                                var resp = client.Post(request);
                                var rezImageJson = JObject.Parse(resp.Content);
                                string postID = rezImageJson["id"].Value<string>();

                                idList.Add(postID);
                            }
                        }
                    }

                    var postImagesData = new List<Object>();

                    request = new RestRequest(Method.POST);

                    for (int i = 0; i < idList.Count; i++)

                    {
                        postImagesData.Add(new { media_fbid = idList[i].ToString() });
                    }
                    string json = JsonConvert.SerializeObject(postImagesData);

                    data.Add("attached_media", json);

                    // В идеале вот это одна строчка
                    //data.Add("attached_media", ListJSON);
                }

                var httpResponse = await http.PostAsync(_feedPageURL, new FormUrlEncodedContent(data));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var Json = JObject.Parse(httpContent);
                if (Json["error"] != null)
                {
                    return new Tuple<int, string>(400, Json["error"]["message"].ToString());
                }

                return new Tuple<int, string>(200, Json["id"].ToString());
            }
        }



        /// <summary>
        /// Изменяет пост на Лицокниге(с)
        /// </summary>
        /// <param name="editPost"></param>
        /// <returns>
        /// Возвращает кортеж 1. Результат выполнения 2. Сообщение ошибки (если есть)
        /// </returns>
        public async Task<Tuple<int, string>> EditPostFacebookAsync(EditPostViewModel editPost)
        {
            using (var http = new HttpClient())
            {
                var data = new Dictionary<string, string> {
                    { "access_token", _pageAccessToken }
                };
                if (editPost.edit_text != null)
                {
                    data.Add("message", editPost.edit_text);
                }
                
                var httpResponse = await http.PostAsync($"{_facebookAPI}{editPost.FacebookPostId}", new FormUrlEncodedContent(data));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var Json = JObject.Parse(httpContent);

                if (Json["error"] != null)
                {
                    return new Tuple<int, string>(400, Json["error"]["message"].ToString());
                }

                return new Tuple<int, string>(200, string.Empty);
            }
        }

        /// <summary>
        /// Удаляет пост, где? Вы как всегда правы, мои дорогие друзья! На Лице которая как книга...
        /// </summary>
        /// <param name="deletePost"></param>
        /// <returns>
        /// Возвращает кортеж 1. Результат выполнения 2. Сообщение ошибки (если есть)
        /// </returns>
        public async Task<Tuple<int, string>> DeletePostFacebookAsync(DeletePostViewModel deletePost)
        {
            using (var http = new HttpClient())
            {
                var httpResponse = await http.DeleteAsync($"{_facebookAPI}{deletePost.FacebookPostId}?access_token={_pageAccessToken}");
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var Json = JObject.Parse(httpContent);
                if (Json["error"] != null)
                {
                    return new Tuple<int, string>(400, Json["error"]["message"].ToString());
                }

                return new Tuple<int, string>(200, string.Empty);
            }
        }
    }
}
