
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.ViewModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace BetterPlan.Controllers
{
    [Route("api/v1")]
    public class FacebookController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private BetterPlanAPI _bpApi;
        private BetterPlanContext _db;
        private IRepositoryWrapper _repositoryWrapper;

        public FacebookController(IConfiguration Configuration, BetterPlanContext context, ILoggerManager logger)
        {
            _db = context;
            _logger = logger;
            _bpApi = new BetterPlanAPI(Response, context, logger);
        }
        /// <summary>
        /// Возвращает доступных пользователей
        /// </summary>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///       [
        ///         {
        ///           "id":"user_id",
        ///           "name": "user_name"
        ///           "picture": "url"(string)
        ///           "cover": "url"(string)
        ///         }
        ///       ]
        ///     }
        /// 
        /// </response>

        [HttpGet("USERS")]
        [Produces("application/json")]
        public JsonResult GetUsers()
        {
            _logger.LogInfo("GET /api/v1/USERS");
            try
            {
                var result = _bpApi.GetUsers();
                var res = result.Value;
                _logger.LogInfo($"GET JsonResult: {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }
        /// <summary>
        /// Возвращает посты пользователя
        /// </summary>
        /// <param name="userId">ID Пользователя</param>
        /// <returns></returns>
        /// <response code="200">
        /// 
        /// Sample response:
        /// 
        ///     {
        ///       [
        ///         {
        ///             "postId": Int32,
        ///             "post_text": "string",
        ///             "facebookPostId": "string",
        ///             "imagesURLList":
        ///             [
        ///                 "string",
        ///                 "string"
        ///             ],
        ///             "isPosting": false,
        ///             "isWaiting": false,
        ///             "IsDelete": false,
        ///             "CreateDateTime": "DATETIME2" or null,
        ///             "UpdateDateTime": "DATETIME2" or null,
        ///             "DeleteDateTime": "DATETIME2" or null,
        ///             "SaveCreateDateTime": "DATETIME2" or null,
        ///             "SaveUpdateDateTime": "DATETIME2" or null,
        ///             "SaveDeleteDateTime": "DATETIME2" or null,
        ///             "whenCreateDateTime": "DATETIME2" or null,
        ///             "status": Int32
        ///         }
        ///       ]
        ///     }
        /// 
        /// </response>
        [HttpGet("USER/{userId}/POSTS")]
        public JsonResult GetUserPosts(string userId)
        {
            _logger.LogInfo($"GET /api/v1/USER/{userId}/POSTS");
            try
            {
                
                // will be checked
                var result = _bpApi.GetUserPosts(userId);
                var res = result.Value;
                _logger.LogInfo($"GET JsonResult: {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }

        /// <summary>
        /// Публикует пост на Facebook
        /// </summary>
        /// <remarks>
        /// Публикует новый пост (текст) в Facebook, пост сохраняется в DB 
        /// 
        /// Sample request:
        /// 
        ///     POST /{id}/POST
        ///     {
        ///       [
        ///         {
        ///             "post_text": "string"
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "facebookPostId": "id"
        ///     }
        /// 
        /// </response>
        ///
        /// -----------------------------------------------------------------------
        /// Публикует отложенный новый пост (текст) в Facebook, пост сохраняется в DB 
        /// 
        /// Sample request:
        /// 
        ///     POST /{id}/POST
        ///     {
        ///       [
        ///         {
        ///             "post_text": "string",
        ///             "WhenCreateDateTime": "DATETIME2" ("2020-03-07T00:37:00"),
        ///             "isWaiting": true
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "Wait",
        ///         "post_id": ""
        ///     }
        /// 
        /// </response>
        /// -----------------------------------------------------------------------
        /// Перезапишет существующий пост в DB и потом опубликует его в Facebook
        ///
        /// Sample request:
        /// 
        ///     POST /{id}/POST
        ///     {
        ///       [
        ///         {
        ///             "postId": Int32,
        ///             "post_text": "string"
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "facebookPostId": "id"
        ///     }
        /// 
        /// </response>
        ///
        /// </remarks>
        /// <param name="post"></param>
        /// <param name="userId">ID Пользователя</param>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "facebookPostId": "id"
        ///     }
        /// 
        /// </response>
        /// <response code="400">
        /// Error response:
        /// 
        ///     {
        ///         "status": "error",
        ///         "error_message": "msg"
        ///     }
        /// </response>
        [HttpPost("USER/{userId}/POST")]
        [Produces("application/json")]
        public async Task<JsonResult> Post(string userId, [FromBody] PostViewModel post)
        {
            _logger.LogInfo($"POST /api/v1/USER/{userId}/POST [Body] {post}");
            try
            {
                var result = await _bpApi.UserPost(userId, post);
                var res = result.Value;
                _logger.LogInfo($"Post /api/v1/USER/{userId}/POST result {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }

        /// <summary>
        /// Публикует пост на Facebook c picture
        /// </summary>
        /// <remarks>
        /// Публикует новый пост в Facebook, пост сохраняется в DB 
        /// 
        /// Sample request:
        /// 
        ///     POST /{id}/POST
        ///     {
        ///       [
        ///         {
        ///             "post_text": "string",
        ///             "ImagesListIFormFile": []
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "facebookPostId": "id"
        ///     }
        /// 
        /// </response>
        ///
        /// -----------------------------------------------------------------------
        /// Публикует отложенный новый пост (текст) в Facebook, пост сохраняется в DB 
        /// 
        /// Sample request:
        /// 
        ///     POST /{id}/POST
        ///     {
        ///       [
        ///         {
        ///             "post_text": "string",
        ///             "ImagesListIFormFile": [],
        ///             "WhenCreateDateTime": "DATETIME2" ("2020-03-07T00:37:00"),
        ///             "isWaiting": true
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "Wait",
        ///         "post_id": ""
        ///     }
        /// 
        /// </response>
        /// -----------------------------------------------------------------------
        /// Перезапишет существующий пост в DB и потом опубликует его в Facebook
        ///
        /// Sample request:
        /// 
        ///     POST /{id}/POST
        ///     {
        ///       [
        ///         {
        ///             "postId": Int32,
        ///             "post_text": "string",
        ///             "ImagesListIFormFile": []
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "facebookPostId": "id"
        ///     }
        /// 
        /// </response>
        ///
        /// </remarks>
        /// <param name="post"></param>
        /// <param name="userId">ID Пользователя</param>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "facebookPostId": "id"
        ///     }
        /// 
        /// </response>
        /// <response code="400">
        /// Error response:
        /// 
        ///     {
        ///         "status": "error",
        ///         "error_message": "msg"
        ///     }
        /// </response>
        [HttpPost("USER/{userId}/POSTIMAGE")]
        public async Task<JsonResult> PostFromImages(string userId, PostViewModelFiles post)
        {
            _logger.LogInfo($"POST /api/v1/USER/{userId}/POSTIMAGE {post}");
            try
            {

                //var result = await _bpApi.UserPostFromImages(userId, post);
                var result = await _bpApi.UserPost(userId, post);
                var res = result.Value;
                _logger.LogInfo($"Post /api/v1/USER/{userId}/POSTIMAGE result {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }

        /// <summary>
        /// Сохранение поста в DB
        /// </summary>
        /// <remarks>
        /// Сохраняет новый пост в DB
        /// 
        /// Sample request:
        /// 
        ///     POST /{id}/SavePost
        ///     {
        ///       [
        ///         {
        ///             "post_text": "string"
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "PostId": int32
        ///     }
        /// 
        /// </response>
        /// -----------------------------------------------------------------------
        /// Изменит существующий пост в DB
        ///
        /// Sample request:
        /// 
        ///     POST /{id}/SavePost
        ///     {
        ///       [
        ///         {
        ///             "postId": Int32,
        ///             "post_text": "string"
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "PostId": int32
        ///     }
        /// 
        /// </response>
        ///
        /// </remarks>
        /// <param name="post"></param>
        /// <param name="userId">ID Пользователя</param>
        /// <returns></returns>
        /// <response code="400">
        /// Error response:
        /// 
        ///     {
        ///         "status": "error",
        ///         "error_message": "msg"
        ///     }
        /// </response>
        [HttpPost("USER/{userId}/SavePost")]
        [Produces("application/json")]
        public async Task<JsonResult> SavePostDB(string userId, [FromBody] SaveViewModel post)
        {
            _logger.LogInfo($"SavePostDB /api/v1/USER/{userId}/SavePost [Body] {post}");
            try
            {
                var result = await _bpApi.SavePost(userId, post);
                _logger.LogInfo($"Save /api/v1/USER/{userId}/SavePostDB result {result.Item1}");

                return new JsonResult(new { status = result.Item1, postID = result.Item2 });
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }

        /// <summary>
        /// Сохранение поста в DB c picture
        /// </summary>
        /// <remarks>
        /// Создаст новый пост в DB
        /// 
        /// Sample request:
        /// 
        ///     POST /{id}/SavePost
        ///     {
        ///       [
        ///         {
        ///             "post_text": "string",
        ///             "ImagesListIFormFile": []
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "PostId": int32
        ///     }
        /// 
        /// </response>
        /// -----------------------------------------------------------------------
        /// Изменит существующий пост в DB
        ///
        /// Sample request:
        /// 
        ///     POST /{id}/SavePost
        ///     {
        ///       [
        ///         {
        ///             "postId": Int32,
        ///             "post_text": "string",
        ///             "ImagesListIFormFile": []
        ///         }
        ///       ]
        ///     }
        ///<response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "PostId": int32
        ///     }
        /// 
        /// </response>
        ///
        /// </remarks>
        /// 
        /// <param name="post"></param>
        /// <param name="userId">ID Пользователя</param>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK",
        ///         "post_id": "id"
        ///     }
        /// 
        /// </response>
        /// <response code="400">
        /// Error response:
        /// 
        ///     {
        ///         "status": "error",
        ///         "error_message": "msg"
        ///     }
        /// </response>
        [HttpPost("USER/{userId}/SavePostImages")]
        public async Task<JsonResult> SavePostDBImages(string userId, SaveViewModelFiles post)
        {
            _logger.LogInfo($"SavePostDBImages /api/v1/USER/{userId}/SavePost [Body] {post}");
            try
            {
                var result = await _bpApi.SavePost(userId, post);
                _logger.LogInfo($"Save /api/v1/USER/{userId}/SavePostDB result {result.Item1}");

                return new JsonResult(new { status = result.Item1, postID = result.Item2 });
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }

        /// <summary>
        /// Изменяет существующий пост на Facebook
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /{id}/EDIT
        ///     {
        ///        "post_id":"id",
        ///        "edit_text":"text"
        ///     }
        ///
        /// </remarks>
        /// <param name="editPost"></param>
        /// <param name="userId">ID Пользователя</param>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK"
        ///     }
        ///     
        /// </response>
        /// <response code="400">
        /// Error response:
        /// 
        ///     {
        ///         "status": "error",
        ///         "error_message": "msg"
        ///     }
        /// </response>
        [HttpPut("USER/{userId}/EDIT")]
        [Produces("application/json")]
        public async Task<JsonResult> EditPost(string userId, [FromBody] EditPostViewModel editPost)
        {
            _logger.LogInfo($"PUT /api/v1/USER/{userId}/EDIT [Body]: post_id: '{editPost.FacebookPostId}'");
            try
            {
                var result = await _bpApi.UserEdit(userId, editPost);
                var res = result.Value;
                _logger.LogInfo($"PUT /api/v1/USER/{userId}/EDIT [Body]: post_id: '{editPost.FacebookPostId}', result {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }

        }

        /// <summary>
        /// Удаляет пост на Facebook по FacebookPostId (помечает IsDelete = true)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /{id}/DELETE
        ///     {
        ///        "FacebookPostId":"id"
        ///     }
        ///
        /// </remarks>
        /// <param name="deletePost"></param>
        /// <param name="userId">ID Пользователя</param>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK"
        ///     }
        ///  
        /// </response>
        /// <response code="400">
        /// Error response:
        /// 
        ///     {
        ///         "status": "error",
        ///         "error_message": "msg"
        ///     }
        /// </response>
        [HttpDelete("USER/{userId}/DELETE")]
        [Produces("application/json")]
        public async Task<JsonResult> DeletePost(string userId, [FromBody] DeletePostViewModel deletePost)
        {
            _logger.LogInfo($"PUT /api/v1/USER/{userId}/DELETE [Body]: post_id: '{deletePost.FacebookPostId}'");
            try
            {
                var result = await _bpApi.UserDelete(userId, deletePost);
                var res = result.Value;
                _logger.LogInfo($"PUT /api/v1/USER/{userId}/DELETE [Body]: post_id: '{deletePost.FacebookPostId}' result {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }

        /// <summary>
        /// Удаляет сохраненный пост в базе данных (помечает IsDelete = true)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /{id}/DELETESAVEPOST
        ///     {
        ///        "PostId":Int32
        ///     }
        ///
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="deleteSavePost"></param>
        /// <returns></returns>

        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK"
        ///     }
        ///  
        /// </response>
        /// <response code="400">
        /// Error response:
        /// 
        ///     {
        ///         "status": "error",
        ///         "error_message": "msg"
        ///     }
        /// </response>

        [HttpDelete("USER/{userId}/DELETESAVEPOST")]
        [Produces("application/json")]
        public async Task<JsonResult> DeleteSavePost(string userId, [FromBody] DeleteSaveViewModel deleteSavePost)
        {
            _logger.LogInfo($"PUT /api/v1/USER/{userId}/DELETESAVEPOST [Body]: PostId: '{deleteSavePost.PostId}'");
            try
            {
                var result = await _bpApi.UserDeleteSavePost(userId, deleteSavePost);
                var res = result.Value;
                _logger.LogInfo($"PUT /api/v1/USER/{userId}/DELETESAVEPOST [Body]: PostId: '{deleteSavePost.PostId}' result {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError($"{e.Message} : {e.StackTrace}");
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }
    }
}