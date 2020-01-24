using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BetterPlanAPI
    {
        private HttpResponse _response;
        private BetterPlanContext _db;  

        public BetterPlanAPI(HttpResponse response, BetterPlanContext context)
        {
            _response = response;
            _db = context;
        }

        /// <summary>
        /// Получает всех пользуватель из "импровизированной" (временно) базы данных 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUsers()
        {
            List<object> usersObj = new List<object>();
            foreach (var user in TempUsersDb)
            {
                var result = FacebookAPI.GetUserAsync(user.Value).Result; 
                usersObj.Add(new { id = result.Item1, name = result.Item2 });
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
            List<object> postsObj = new List<object>();
            foreach (var post in posts)
            {
                Dictionary<string, string> pairs = new Dictionary<string, string>();

                pairs.Add("post_id", post["id"].ToString());
                if (post["message"] != null) pairs.Add("text", post["message"].ToString());
                if (post["full_picture"] != null) pairs.Add("img", post["full_picture"].ToString());
                if (post["place"] != null) pairs.Add("place", post["place"]["id"].ToString());

                postsObj.Add(pairs);
            }
            return new JsonResult(postsObj);
        }

        /// <summary>
        /// Публикует пост на бизнес странице пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public JsonResult UserPost(string userId, PostViewModel post)
        {
            if (!TempUsersDb.ContainsKey(userId))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }
            if (post.post_text == null) {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Поле post_text обязательно!" }); 
            }

            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[userId]).Result;
            var result = new FacebookAPI(page.Item2, page.Item1).PostToFacebookAsync(post).Result;

            if(result.Item1 == 200)
            {
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
        /// Изменяет публикацию пользователя по id
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="editPost"></param>
        /// <returns></returns>
        public JsonResult UserEdit(string user_id, EditPostViewModel editPost)
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
            if (editPost.edit_text == null)
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Поле edit_text обязательно!" });
            }

            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result;
            var result = new FacebookAPI(page.Item2, page.Item1).EditPostFacebookAsync(editPost).Result;

            if (result.Item1 == 200) {
                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "OK", post_id = result.Item2 });
            } else {
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
        public JsonResult UserDelete(string user_id, DeletePostViewModel deletePost)
        {
            if (!TempUsersDb.ContainsKey(user_id))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }
            if (deletePost.post_id == null) {
                return new JsonResult(new { status = "error", error_message = "Поле post_id обязательно!" });
            }
            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result;
            var result = new FacebookAPI(page.Item2, page.Item1).DeletePostFacebookAsync(deletePost).Result;

            if (result.Item1 == 200) {
                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "OK" });
            } else {
                _response.StatusCode = result.Item1;
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
        }


        //ну бле, знаю что фигово, надо будет перенести в базу данных, сделаем
        private static Dictionary<string, string> TempUsersDb = new Dictionary<string, string>() {
            { "100559284835939","EAAHD5fytWZAABADfTdcE8ZCp2d323x0YYgcaNMAfVGbNjtnCtKN9Ay9yBDBfnM2MkhzT5UQZBC0eDZBizgJEZCBgXZAxNXDFgAK1TN2ZCwPD6iLMpP6X8gSkQoN6YcFG39oZBgHz6U6OeOcOB41oLGNXQYVXJVeh4nfjhRCnuEde8CwQF83UYFee" },
            { "895127244222164","EAAjnVI1sCkwBADWHQOeCunZCMMezGDc2Xit0rb4ZCM86gnOe78qMUtjwqkDel73hJPwilAZANenNKPufhXRFEbydLEplhSwIuRORFe4HICwflMQqVEyFR49c9VsgZCVsYFivZCmNYEOdCuXJ7auyVFZCN4eVBwqP2hFi8EvkfI7QZDZD" }
        };
    }
}
