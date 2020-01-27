using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Entities.Models {
    public class FacebookAPI {
        private static readonly string _facebookAPI = "https://graph.facebook.com/";
        private static readonly string _pageEdgeFeed = "feed";
        private static readonly string _getUserURL = $"{_facebookAPI}/me";

        private readonly string _pageAccessToken;
        private readonly string _feedPageURL;

        public FacebookAPI(string accessToken, string pageID) {
            _pageAccessToken = accessToken;
            _feedPageURL = $"{_facebookAPI}{pageID}/{_pageEdgeFeed}";
        }

        /// <summary>
        /// Получает инфу о пользователе по его токену
        /// </summary>
        /// <param name="UserToken"></param>
        /// <returns>
        /// Возвращает кортеж который состоит из 1. id 2. имени пользователя
        /// </returns>
        public static async Task<Tuple<string, string>> GetUserAsync(string UserToken) {
            using (var http = new HttpClient()) {

                var httpResponse = await http.GetAsync($"{_getUserURL}?access_token={UserToken}");
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                var Json = JObject.Parse(httpContent);

                var result = new Tuple<string, string>(Json["id"].ToString(), Json["name"].ToString());
                return result;
            }
        }

        /// <summary>
        /// Возвращает посты бизнес аккаунта пользователя по его токену
        /// </summary>
        /// <param name="UserToken"></param>
        /// <returns>
        /// Возвращает JObject Json в котором находится массив постов
        /// </returns>
        public static async Task<JToken> GetPagePostsAsync(string UserToken) {
            using (var http = new HttpClient()) {
                var pageInfo = GetPageIdAndTokenAsync(UserToken).Result;
                var httpResponse = await http.GetAsync($"{_facebookAPI}{pageInfo.Item1}/{_pageEdgeFeed}?access_token={pageInfo.Item2}&fields=message,place,full_picture");
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
        public static async Task<Tuple<string, string>> GetPageIdAndTokenAsync(string UserToken) {
            using (var http = new HttpClient()) {
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
        public async Task<Tuple<int, string>> PostToFacebookAsync(PostViewModel post) {
            using (var http = new HttpClient()) {
                var data = new Dictionary<string, string> {
                    { "access_token", _pageAccessToken },
                    { "message", post.post_text },
                };

                var httpResponse = await http.PostAsync(_feedPageURL, new FormUrlEncodedContent(data));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var Json = JObject.Parse(httpContent);
                if (Json["error"] != null) {
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
        public async Task<Tuple<int, string>> EditPostFacebookAsync(EditPostViewModel editPost) {
            using (var http = new HttpClient()) {
                var data = new Dictionary<string, string> {
                    { "access_token", _pageAccessToken },
                    { "message", editPost.edit_text }
                };


                var httpResponse = await http.PostAsync($"{_facebookAPI}{editPost.post_id}", new FormUrlEncodedContent(data));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var Json = JObject.Parse(httpContent);

                if (Json["error"] != null) {
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
        public async Task<Tuple<int, string>> DeletePostFacebookAsync(DeletePostViewModel deletePost) {
            using (var http = new HttpClient()) {
                var httpResponse = await http.DeleteAsync($"{_facebookAPI}{deletePost.post_id}?access_token={_pageAccessToken}");
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var Json = JObject.Parse(httpContent);
                if (Json["error"] != null) {
                    return new Tuple<int, string>(400, Json["error"]["message"].ToString());
                }
                
                return new Tuple<int, string>(200, string.Empty);
            }
        }
    }
}
