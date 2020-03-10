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
using System.Linq;

namespace Entities.Models
{
    public class BetterPlanAPI
    {
        private HttpResponse _response;
        private BetterPlanContext _db;
        private ILoggerManager _logger;
        private Post _post;

        public delegate Task<Tuple<int, string>> MyDel(PostViewModel model);

        private event MyDel MyEvent;
        public BetterPlanAPI(HttpResponse response, BetterPlanContext context, ILoggerManager logger)
        {
            _response = response;
            _db = context;
            _logger = logger;

        }

        /// <summary>
        /// Публикует отложенный пост 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async void PublishPostDelay(PostViewModel model)
        {

            using (var context = new BetterPlanContext())
            {

                DateTime now = DateTime.Now;
                PostViewModel postViewModel = model;
                await Task.Delay( model.WhenCreateDateTime.GetValueOrDefault() - now);

                Post resDB = await context.Posts.FirstOrDefaultAsync(id => id.PostId == postViewModel.PostId);
                if (resDB.status == Status.Waiting && resDB.WhenCreateDateTime == postViewModel.WhenCreateDateTime && resDB.isPosting == true)
                {
                    var result = await MyEvent.Invoke(model);

                    if (result.Item1 == 200)
                    {
                        resDB.CreateDateTime = DateTime.UtcNow;
                        resDB.FacebookPostId = result.Item2;
                        resDB.status = Status.Published;
                    }
                    else
                    {
                        resDB.status = Status.Error;
                    }

                    await context.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Сохраняет пост на DВ
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Tuple<Int32, Int32>> SavePost(string userId, PostViewModel model)
        {
            if (model.PostId == null)
            {
                Post postDB = new Post(userId, model);

                try
                {
                    var res = _db.Posts.Add(postDB);

                    if (await _db.SaveChangesAsync() > 0)
                    {
                        //переделать не нравится
                        Post post = await _db.Posts.FirstOrDefaultAsync(e => e.Text == postDB.Text);

                        return new Tuple<Int32, Int32>(200, post.PostId);
                    }
                    else
                    {
                        return new Tuple<Int32, Int32>(400, -1);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            else
            {
                Post resDB = await _db.Posts.FirstOrDefaultAsync(id => id.PostId == model.PostId);
                resDB.Copy(model);
                if (await _db.SaveChangesAsync() > 0)
                {
                    return new Tuple<Int32, Int32>(200, model.PostId.Value);
                }
                else
                {
                    return new Tuple<Int32, Int32>(400, -1);
                }
            }
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
                usersObj.Add(result.Value);
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

            var res = _db.Posts.Where(e => e.UserId == userId).ToListAsync().Result.OrderByDescending(e => e.PostId).ToList();

            List<PostViewModelGetPost> list = new List<PostViewModelGetPost>();
            for (int i = 0; i < res.Count(); i++)
            {
                list.Add(new PostViewModelGetPost(res[i], false));
            }


            return new JsonResult(/*list.OrderByDescending(e => e.PostId.Value)*/list);

            /*

            JToken posts = FacebookAPI.GetPagePostsAsync(TempUsersDb[userId]).Result;
            string str = posts.ToString();


            List<object> postsObj = new List<object>();
            foreach (var post in posts)
            {



                postsObj.Add(Info.GetJSONImage(post));
            }

            */
            //return new JsonResult(postsObj);
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
            //PostViewModel postView = post;

            if (post.PostId == null)
            {
                var res = SavePost(userId, post).Result;
                post.PostId = res.Item2;
            }
            else
            {
                _post = await _db.Posts.FirstOrDefaultAsync(id => id.PostId == post.PostId);
                _post.Copy(post);
                await _db.SaveChangesAsync();
            }

            Tuple<Int32, String> result;
            FacebookAPI facebookApi = new FacebookAPI(page.Item2, page.Item1);

            if (post.isWaiting == false)
            {
                result = new FacebookAPI(page.Item2, page.Item1).PostToFacebookAsync(post).Result;//??? 

                if (result.Item1 == 200)
                {
                    _post = _db.Posts.FirstOrDefaultAsync(id => id.PostId == post.PostId).Result;
                    _post.CreateDateTime = DateTime.Now;
                    _post.FacebookPostId = result.Item2;
                    _post.status = Status.Published;
                    await _db.SaveChangesAsync();
                    return new JsonResult(new { status = "OK", post_id = result.Item2 });
                }
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
            else
            {
                this.MyEvent += facebookApi.PostToFacebookAsync;
                PublishPostDelay(post);
                return new JsonResult(new { status = "Wait", post_id = "" });
            }

        }

        ///// <summary>
        ///// Публикует пост на бизнес странице пользователя
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="post"></param>
        ///// <returns></returns>
        //public async Task<JsonResult> UserPostFromImages(string userId, PostViewModel post)
        //{
        //    if (!TempUsersDb.ContainsKey(userId))
        //    {
        //        _response.StatusCode = 400;
        //        return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
        //    }

        //    var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[userId]).Result;
        //    string JSON = String.Empty;
        //    //string JSON = await ImgJSON(post);
        //    var result = new FacebookAPI(page.Item2, page.Item1).PostFromImagesToFacebookAsync(post, JSON).Result;

        //    if (result.Item1 == 200)
        //    {
        //        _post = new Post(userId, post);
        //        _post.CreateDateTime = DateTime.UtcNow;
        //        _post.FacebookPostId = result.Item2;
        //        //_post.ImagesListJSON = JSON;
        //        _db.Posts.Add(_post);
        //        await _db.SaveChangesAsync();


        //        _response.StatusCode = result.Item1;
        //        return new JsonResult(new { status = "OK", post_id = result.Item2 });
        //    }
        //    else
        //    {
        //        //сделать метод для удаления файлов с диска
        //        _response.StatusCode = result.Item1;
        //        return new JsonResult(new { status = "error", error_message = result.Item2 });
        //    }
        //}

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
            {"2404663569642984","EAAiLB13fdegBADC55a6uXEweSZC35BVGlZAEC10tyZBAZAbv623Wq08crwDU3VQvhjgxov7ZBEKXTiPcMBJ6eGeVibzTAOYcrmLroGk1SxyYqvUHyuVvjo1CILn3769YlTPmZAmpS1KF9KWA0v30zq5IjpWZAo5V3nXmlgaZAMpCoZC7PrFXM5el8QWeuZC1df85CNuLhxxVBb4gZDZD"*/ }
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
