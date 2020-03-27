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
        private IRepositoryWrapper _db;
        private ILoggerManager _logger;
        private Post _post;

        public delegate Task<Tuple<int, string>> MyDel(PostViewModel model);
        public delegate Task<Tuple<int, string>> MyDelFiles(PostViewModelFiles model);

        private event MyDel MyEvent;
        private event MyDelFiles MyEventFiles;
        public BetterPlanAPI(HttpResponse response, IRepositoryWrapper context, ILoggerManager logger)
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

            using (var context = new RepositoryContext())
            {

                DateTime now = DateTime.UtcNow;
                PostViewModel postViewModel = model;
                await Task.Delay(model.WhenCreateDateTime.GetValueOrDefault() - now);

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

        public async void PublishPostDelay(PostViewModelFiles model)
        {

            using (var context = new RepositoryContext())
            {

                DateTime now = DateTime.UtcNow;
                PostViewModelFiles postViewModel = model;
                await Task.Delay(model.WhenCreateDateTime.GetValueOrDefault() - now);

                Post resDB = await context.Posts.FirstOrDefaultAsync(id => id.PostId == postViewModel.PostId);
                if (resDB.status == Status.Waiting && resDB.WhenCreateDateTime == postViewModel.WhenCreateDateTime && resDB.isPosting == true)
                {
                    var result = await MyEventFiles.Invoke(model);

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
        /// <remarks>
        ///
        /// 
        /// </remarks>
        public async Task<Tuple<Int32, Int32>> SavePost(string userId, SaveViewModel model)
        {
            if (model.PostId == null)
            {
                Post postDB = new Post(userId, model);
                postDB.SaveCreateDateTime = DateTime.UtcNow;

                try
                {
                    _db.postRepository.Create(postDB);

                    if (await _db.Save() > 0)
                    {
                        //переделать не нравится
                        var post = _db.postRepository.FindByCondition(
                            e => (
                                e.PostId == postDB.PostId &&
                                e.Text == postDB.Text &&
                                e.IsDelete == postDB.IsDelete &&
                                e.isPosting == postDB.isPosting &&
                                e.isWaiting == postDB.isWaiting &&
                                e.SaveUpdateDateTime == postDB.SaveUpdateDateTime &&
                                e.SaveCreateDateTime == postDB.SaveCreateDateTime &&
                                e.SaveDeleteDateTime == postDB.SaveDeleteDateTime &&
                                e.CreateDateTime == postDB.CreateDateTime &&
                                e.UpdateDateTime == postDB.UpdateDateTime &&
                                e.DeleteDateTime == postDB.DeleteDateTime &&
                                e.WhenCreateDateTime == postDB.WhenCreateDateTime &&
                                e.UserId == postDB.UserId &&
                                e.ImagesListJSON == postDB.ImagesListJSON &&
                                e.FacebookPostId == postDB.FacebookPostId &&
                                e.status == postDB.status
                            )
                                 );

                        return new Tuple<Int32, Int32>(200, ((Post)post.First()).PostId);
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
                var arrayresDB = _db.postRepository.FindByCondition(id => id.PostId == model.PostId);
                var resDB = arrayresDB.First();
                resDB.Copy(model);
                resDB.SaveUpdateDateTime = DateTime.UtcNow;
                if (await _db.Save() > 0)
                {
                    return new Tuple<Int32, Int32>(200, model.PostId.Value);
                }
                else
                {
                    return new Tuple<Int32, Int32>(400, -1);
                }
            }
        }

        public async Task<Tuple<Int32, Int32>> SavePost(string userId, SaveViewModelFiles model)
        {
            if (model.PostId == null)
            {
                Post postDB = new Post(userId, model);
                postDB.SaveCreateDateTime = DateTime.UtcNow;
                try
                {
                    _db.postRepository.Create(postDB);

                    if (await _db.Save() > 0)
                    {
                        //переделать не нравится
                        var postArray = _db.postRepository.FindByCondition(
                            e => (
                                e.PostId == postDB.PostId &&
                                e.Text == postDB.Text &&
                                e.IsDelete == postDB.IsDelete &&
                                e.isPosting == postDB.isPosting &&
                                e.isWaiting == postDB.isWaiting &&
                                e.SaveUpdateDateTime == postDB.SaveUpdateDateTime &&
                                e.SaveCreateDateTime == postDB.SaveCreateDateTime &&
                                e.SaveDeleteDateTime == postDB.SaveDeleteDateTime &&
                                e.CreateDateTime == postDB.CreateDateTime &&
                                e.UpdateDateTime == postDB.UpdateDateTime &&
                                e.DeleteDateTime == postDB.DeleteDateTime &&
                                e.WhenCreateDateTime == postDB.WhenCreateDateTime &&
                                e.UserId == postDB.UserId &&
                                e.ImagesListJSON == postDB.ImagesListJSON &&
                                e.FacebookPostId == postDB.FacebookPostId &&
                                e.status == postDB.status
                            )
                        );
                        var post = postArray.First();



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
                var resDBArray = _db.postRepository.FindByCondition(id => id.PostId == model.PostId);
                var resDB = resDBArray.First();
                resDB.Copy(model);
                resDB.SaveUpdateDateTime = DateTime.UtcNow;
                if (await _db.Save() > 0)
                {
                    return new Tuple<Int32, Int32>(200, model.PostId.Value);
                }
                else
                {
                    return new Tuple<Int32, Int32>(400, -1);
                }
            }
        }

        public async Task<Tuple<Int32, Int32>> SavePost(string userId, PostViewModel model)
        {
            if (model.PostId == null)
            {
                Post postDB = new Post(userId, model);
                postDB.SaveCreateDateTime = DateTime.UtcNow;
                try
                {
                    _db.postRepository.Create(postDB);

                    if (await _db.Save() > 0)
                    {
                        //переделать не нравится
                        var postArray = _db.postRepository.FindByCondition(
                            e => (
                                e.PostId == postDB.PostId &&
                                e.Text == postDB.Text &&
                                e.IsDelete == postDB.IsDelete &&
                                e.isPosting == postDB.isPosting &&
                                e.isWaiting == postDB.isWaiting &&
                                e.SaveUpdateDateTime == postDB.SaveUpdateDateTime &&
                                e.SaveCreateDateTime == postDB.SaveCreateDateTime &&
                                e.SaveDeleteDateTime == postDB.SaveDeleteDateTime &&
                                e.CreateDateTime == postDB.CreateDateTime &&
                                e.UpdateDateTime == postDB.UpdateDateTime &&
                                e.DeleteDateTime == postDB.DeleteDateTime &&
                                e.WhenCreateDateTime == postDB.WhenCreateDateTime &&
                                e.UserId == postDB.UserId &&
                                e.ImagesListJSON == postDB.ImagesListJSON &&
                                e.FacebookPostId == postDB.FacebookPostId &&
                                e.status == postDB.status
                            )
                        );
                        var post = postArray.First();
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
                var resDBArray = _db.postRepository.FindByCondition(id => id.PostId == model.PostId);
                var resDB = resDBArray.First();
                resDB.Copy(model);
                resDB.SaveUpdateDateTime = DateTime.UtcNow;
                if (await _db.Save() > 0)
                {
                    return new Tuple<Int32, Int32>(200, model.PostId.Value);
                }
                else
                {
                    return new Tuple<Int32, Int32>(400, -1);
                }
            }
        }

        public async Task<Tuple<Int32, Int32>> SavePost(string userId, PostViewModelFiles model)
        {
            if (model.PostId == null)
            {
                Post postDB = new Post(userId, model);
                postDB.SaveCreateDateTime = DateTime.UtcNow;
                try
                {
                    _db.postRepository.Create(postDB);

                    if (await _db.Save() > 0)
                    {
                        //переделать не нравится
                        var postArray = _db.postRepository.FindByCondition(
                            e => (
                                e.PostId == postDB.PostId &&
                                e.Text == postDB.Text &&
                                e.IsDelete == postDB.IsDelete &&
                                e.isPosting == postDB.isPosting &&
                                e.isWaiting == postDB.isWaiting &&
                                e.SaveUpdateDateTime == postDB.SaveUpdateDateTime &&
                                e.SaveCreateDateTime == postDB.SaveCreateDateTime &&
                                e.SaveDeleteDateTime == postDB.SaveDeleteDateTime &&
                                e.CreateDateTime == postDB.CreateDateTime &&
                                e.UpdateDateTime == postDB.UpdateDateTime &&
                                e.DeleteDateTime == postDB.DeleteDateTime &&
                                e.WhenCreateDateTime == postDB.WhenCreateDateTime &&
                                e.UserId == postDB.UserId &&
                                e.ImagesListJSON == postDB.ImagesListJSON &&
                                e.FacebookPostId == postDB.FacebookPostId &&
                                e.status == postDB.status
                            )
                        );
                        var post = postArray.First();
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
                var resDBArray = _db.postRepository.FindByCondition(id => id.PostId == model.PostId);
                var resDB = resDBArray.First();
                resDB.Copy(model);
                resDB.SaveUpdateDateTime = DateTime.UtcNow;
                if (await _db.Save() > 0)
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
        ///<remarks>
        /// </remarks>
        public JsonResult GetUserPosts(string userId)
        {
            if (!TempUsersDb.ContainsKey(userId))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }

            List<Post> res = _db.postRepository.FindByCondition(e => e.UserId == userId).ToListAsync().Result.OrderByDescending(e => e.PostId).ToList();

            List<PostViewModelGetPost> list = new List<PostViewModelGetPost>();
            for (int i = 0; i < res.Count(); i++)
            {
                list.Add(new PostViewModelGetPost(res[i], false));
            }

            return new JsonResult(list);

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

            if (post.PostId == null)
            {
                var res = SavePost(userId, post).Result;
                post.PostId = res.Item2;
            }
            else
            {
                var array = _db.postRepository.FindByCondition(id => id.PostId == post.PostId);
                _post = array.First();
                _post.Copy(post);
                await _db.Save();
            }

            Tuple<Int32, String> result;
            FacebookAPI facebookApi = new FacebookAPI(page.Item2, page.Item1);

            if (post.isWaiting == false)
            {
                result = new FacebookAPI(page.Item2, page.Item1).PostToFacebookAsync(post).Result;//??? 

                if (result.Item1 == 200)
                {
                    var array = _db.postRepository.FindByCondition(id => id.PostId == post.PostId);
                    _post = array.First();
                    _post.CreateDateTime = DateTime.UtcNow;
                    _post.FacebookPostId = result.Item2;
                    _post.status = Status.Published;
                    await _db.Save();
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

        /// <summary>
        /// Публикует пост на бизнес странице пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<JsonResult> UserPost(string userId, PostViewModelFiles post)
        {
            if (!TempUsersDb.ContainsKey(userId))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }

            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[userId]).Result;

            if (post.PostId == null)
            {
                var res = SavePost(userId, post).Result;
                post.PostId = res.Item2;
            }
            else
            {
                var array = _db.postRepository.FindByCondition(id => id.PostId == post.PostId);
                _post = array.First();
                _post.Copy(post);
                await _db.Save();
            }

            Tuple<Int32, String> result;
            FacebookAPI facebookApi = new FacebookAPI(page.Item2, page.Item1);

            if (post.isWaiting == false)
            {
                result = new FacebookAPI(page.Item2, page.Item1).PostToFacebookAsync(post).Result;//??? 

                if (result.Item1 == 200)
                {
                    var array = _db.postRepository.FindByCondition(id => id.PostId == post.PostId);
                    _post = array.First();
                    _post.CreateDateTime = DateTime.UtcNow;
                    _post.FacebookPostId = result.Item2;
                    _post.status = Status.Published;
                    await _db.Save();
                    return new JsonResult(new { status = "OK", post_id = result.Item2 });
                }
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
            else
            {
                this.MyEventFiles += facebookApi.PostToFacebookAsync;
                PublishPostDelay(post);
                return new JsonResult(new { status = "Wait", post_id = "" });
            }

        }

        /// <summary>
        /// Изменяет публикацию пользователя по FacebookPostId
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

            if (editPost.FacebookPostId == null)
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
                var resArray =
                    _db.postRepository.FindByCondition(post => post.FacebookPostId == editPost.FacebookPostId);
                var model = resArray.First();

                if (editPost.FacebookPostId != null)
                {
                    model.Text = editPost.edit_text;
                }


                model.UpdateDateTime = DateTime.UtcNow;

                if (await _db.Save() > 0)
                {
                    return new JsonResult(new { status = "OK", PostId = model.PostId });
                }
                else
                {
                    return new JsonResult(new { status = "error", error_message = result.Item2 });
                }

            }
            else
            {
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
        }

        /// <summary>
        /// Удаляет публикацию по FacebookPostId
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

            if (deletePost.FacebookPostId == null)
            {
                return new JsonResult(new { status = "error", error_message = "Поле post_id обязательно!" });
            }

            var page = FacebookAPI.GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result;
            var result = new FacebookAPI(page.Item2, page.Item1).DeletePostFacebookAsync(deletePost).Result;

            if (result.Item1 == 200)
            {
                var resArray = _db.postRepository.FindByCondition(post => post.FacebookPostId == deletePost.FacebookPostId);
                var model = resArray.First();

                model.IsDelete = true;
                model.DeleteDateTime = DateTime.UtcNow;

                if (await _db.Save() > 0)
                {
                    return new JsonResult(new { status = "OK" });
                }
                else
                {
                    return new JsonResult(new { status = "error", error_message = result.Item2 });
                }
            }
            else
            {
                return new JsonResult(new { status = "error", error_message = result.Item2 });
            }
        }

        /// <summary>
        /// Удаляет публикацию по FacebookPostId
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="deletePost"></param>
        /// <returns></returns>
        public async Task<JsonResult> UserDeleteSavePost(string user_id, DeleteSaveViewModel deletePost)
        {
            if (!TempUsersDb.ContainsKey(user_id))
            {
                _response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "Полученный id не существует!" });
            }

            var resArray = _db.postRepository.FindByCondition(post => post.PostId == deletePost.PostId);
            var model = resArray.First();
            if (model == null)
            {
                return new JsonResult(new { status = "error", error_message = "Данный пост не найден в DB" });
            }
            model.IsDelete = true;
            model.SaveDeleteDateTime = DateTime.UtcNow;

            if (await _db.Save() > 0)
            {
                return new JsonResult(new { status = "OK" });
            }
            else
            {
                return new JsonResult(new { status = "error", error_message = "Данный пост не был удален с DB" });
            }
        }


        //ну бле, знаю что фигово, надо будет перенести в базу данных, сделаем

        private static Dictionary<string, string> TempUsersDb = new Dictionary<string, string>() {
            { "100559284835939","EAAHD5fytWZAABADfTdcE8ZCp2d323x0YYgcaNMAfVGbNjtnCtKN9Ay9yBDBfnM2MkhzT5UQZBC0eDZBizgJEZCBgXZAxNXDFgAK1TN2ZCwPD6iLMpP6X8gSkQoN6YcFG39oZBgHz6U6OeOcOB41oLGNXQYVXJVeh4nfjhRCnuEde8CwQF83UYFee" },
            { "895127244222164","EAAjnVI1sCkwBADWHQOeCunZCMMezGDc2Xit0rb4ZCM86gnOe78qMUtjwqkDel73hJPwilAZANenNKPufhXRFEbydLEplhSwIuRORFe4HICwflMQqVEyFR49c9VsgZCVsYFivZCmNYEOdCuXJ7auyVFZCN4eVBwqP2hFi8EvkfI7QZDZD" } ,
            {"2404663569642984","EAAiLB13fdegBAJyu4NcrtJTqFRMxnZCLzMsOK7Ri530kkTecVlM0ZCFXOWJgHTM7z7g9tooMOnq3FdYXflwCkYBrUB9IanT7lpxApZBFGhiZCTxPj0ejWmZB55ti01H7kyQ6A8OrQtwgav2kZA0FZAvJQ3zEDnGwzPcJBacQ8y7ZCvXAWHYK5n5Nr9lNg3MURPSqAzk6xUpvfAZDZD" }
        };

    }

}
