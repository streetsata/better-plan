using Contracts;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Entities.Models
{
    public class BetterPlanAPI
    {
        private HttpResponse _response;
        private BetterPlanContext _db;
        private ILoggerManager _logger;
        private Post _post;

        public BetterPlanAPI(HttpResponse response, BetterPlanContext context, ILoggerManager logger)
        {
            _response = response;
            _db = context;
            _logger = logger;

        }

        /// <summary>
        /// Получает всех пользуватель из "импровизированной" (временно) базы данных 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUsers()
        {
            List<String> usersObj = new List<String>();
            foreach (var user in TempUsersDb)
            {
                var result = FacebookAPI.GetUserAsync(user.Value).Result;
                usersObj.Add(result);
            }

            return new JsonResult(usersObj);
        }

        /// <summary>
        /// Получает посты из бизнес аккаунта пользователя по id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult GetUserPosts(string userId)
        {
            if (!TempUsersDb.ContainsKey(userId))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }


            JToken posts = FacebookAPI.GetPagePostsAsync(TempUsersDb[userId]).Result;
            string str = posts.ToString();


            List<object> postsObj = new List<object>();
            foreach (var post in posts)
            {



                postsObj.Add(Info.GetJSONImage(post));
            }

            return new JsonResult(postsObj);
        }

        /// <summary>
        /// Публикует пост на бизнес странице пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<JsonResult> UserPost(string userId, PostViewModel post)
        {
            if (!TempUsersDb.ContainsKey(userId))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }


            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[userId]).Result;
            var result = new FacebookAPI(page.Item2, page.Item1).PostToFacebookAsync(post).Result;

            if (result.Item1 == 200)
            {
                _post = new Post(userId, post);
                _post.CreateDateTime = DateTime.UtcNow;
                _post.FacebookPostId = result.Item2;
                _db.Posts.Add(_post);
                await _db.SaveChangesAsync();


                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "OK", post_id = result.Item2 });
            }
            else
            {
                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
        }

        /// <summary>
        /// Публикует пост на бизнес странице пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<JsonResult> UserPostFromImages(string userId, PostViewModel post)
        {
            if (!TempUsersDb.ContainsKey(userId))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }

            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[userId]).Result;
            string JSON = String.Empty;
            //string JSON = await ImgJSON(post);
            var result = new FacebookAPI(page.Item2, page.Item1).PostFromImagesToFacebookAsync(post, JSON).Result;

            if (result.Item1 == 200)
            {
                _post = new Post(userId, post);
                _post.CreateDateTime = DateTime.UtcNow;
                _post.FacebookPostId = result.Item2;
                //_post.ImagesListJSON = JSON;
                _db.Posts.Add(_post);
                await _db.SaveChangesAsync();


                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "OK", post_id = result.Item2 });
            }
            else
            {
                //сделать метод для удаления файлов с диска
                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
        }

        /// <summary>
        /// Изменяет публикацию пользователя по id
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="editPost"></param>
        /// <returns></returns>
        public async Task<JsonResult> UserEdit(string user_id, EditPostViewModel editPost)
        {
            if (!TempUsersDb.ContainsKey(user_id))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }

            if (editPost.post_id == null)
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Поле post_id обязательно!" });
            }

            if (editPost.edit_text == null) //проверка в самом EditPostFacebookAsync
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Поле edit_text обязательно!" });
            }

            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result;
            var result = new FacebookAPI(page.Item2, page.Item1).EditPostFacebookAsync(editPost).Result;

            if (result.Item1 == 200)
            {
                var model = await _db.Posts.FirstOrDefaultAsync(post => post.FacebookPostId == editPost.post_id);

                if (editPost.post_id != null)
                {
                    model.Text = editPost.edit_text;
                }

                if (editPost.place != null)
                {
                    model.Place = editPost.place;
                }

                model.UpdateDateTime = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "OK", post_id = result.Item2 });
            }
            else
            {
                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
        }

        /// <summary>
        /// Удаляет публикацию по id
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="deletePost"></param>
        /// <returns></returns>
        public async Task<JsonResult> UserDelete(string user_id, DeletePostViewModel deletePost)
        {
            if (!TempUsersDb.ContainsKey(user_id))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }

            if (deletePost.post_id == null)
            {
                return new JsonResult(new { status = "error", error_message = "Поле post_id обязательно!" });
            }

            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result;
            var result = new FacebookAPI(page.Item2, page.Item1).DeletePostFacebookAsync(deletePost).Result;

            if (result.Item1 == 200)
            {

                var model = await _db.Posts.FirstOrDefaultAsync(post => post.FacebookPostId == deletePost.post_id);

                model.IsDelete = true;
                model.DeleteDateTime = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "OK" });
            }
            else
            {
                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
        }


        //ну бле, знаю что фигово, надо будет перенести в базу данных, сделаем

        private static Dictionary<string, string> TempUsersDb = new Dictionary<string, string>() {
            { "100559284835939","EAAHD5fytWZAABADfTdcE8ZCp2d323x0YYgcaNMAfVGbNjtnCtKN9Ay9yBDBfnM2MkhzT5UQZBC0eDZBizgJEZCBgXZAxNXDFgAK1TN2ZCwPD6iLMpP6X8gSkQoN6YcFG39oZBgHz6U6OeOcOB41oLGNXQYVXJVeh4nfjhRCnuEde8CwQF83UYFee" },
            { "895127244222164","EAAjnVI1sCkwBADWHQOeCunZCMMezGDc2Xit0rb4ZCM86gnOe78qMUtjwqkDel73hJPwilAZANenNKPufhXRFEbydLEplhSwIuRORFe4HICwflMQqVEyFR49c9VsgZCVsYFivZCmNYEOdCuXJ7auyVFZCN4eVBwqP2hFi8EvkfI7QZDZD" }/*,
            {"2404663569642984","EAAiLB13fdegBAF75p4EwZBP4Nx0CKRjWdqVxvzYC03ful7XayQrEymHGO6s9ZCnZCqDyQRiFyK1rJbZBggd0VB0Ha7VZCopSFebTfCAHuZCHjgFKZCnsHVeBQwjKMYjZC0vmu6r3YU5dHCPzcT9MnP07jtZB0oTqkFrXZBCpYyukdGCRqG4Uu0CRayK6lcrlzd780ZD" }*/
        };


        // моя страничка
        //private static Dictionary<string, string> TempUsersDb = new Dictionary<string, string>()
        //{
        //    {
        //        "2404663569642984","EAAiLB13fdegBACqSIWtWZAsWCC9HDps2YuymmwLq6Yibnw3FahBDD0p4ziprGJgdDNdvNfeQgwixGki4P2rOtjwhOYq2v33dZC7ldiW7tZBH5ZAxVTt7GmhloD8XEFqc66yZCxHR2aEoQ9laNnrpmG8CfRSjWVnFrxxP2zlzu1cgMX06VXZCC5CrbmTR5I9IueUx941PFZA7QZDZD"
        //    }
        //};

        /* //
        private async Task<String> ImgJSON(PostViewModel postViewModel)
        {
            List<String> linksList = new List<string>();
            var uploads = Path.Combine(Directory.GetCurrentDirectory() + "\\App_Data\\internal_logs", "Uploads");

            foreach (var imageFile in postViewModel.ImagesListJSON)
            {
                var filePath = Path.Combine(uploads, Guid.NewGuid() + imageFile.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    linksList.Add(filePath);
                    await imageFile.CopyToAsync(fileStream);
                }
            }

            return JsonConvert.SerializeObject(linksList, Formatting.Indented);
        }
        */
    }

}
