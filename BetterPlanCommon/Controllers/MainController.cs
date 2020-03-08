
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
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private BetterPlanAPI _bpApi;
        private BetterPlanContext _db;
        
        public MainController(IConfiguration Configuration, BetterPlanContext context, ILoggerManager logger)
        {
            _db = context;
            _logger = logger;
            _bpApi = new BetterPlanAPI(Response, context,logger);
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
                //_logger.LogWarn("TEST WARM");
                //throw new Exception();
                var result = _bpApi.GetUsers();
                var res = result.Value;
                _logger.LogInfo($"GET JsonResult: {res}");
                return result;

            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }
        /// <summary>
        ///     Возвращает посты пользователя
        /// </summary>
        /// <param name="userId">ID Пользователя</param>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///       [
        ///         {
        ///           "post_id":"post_id",
        ///           "text": "post_text",
        ///           "img": "img_link",
        ///           "place": "place_id"
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
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }

        /// <summary>
        /// Публикует пост на Facebook
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /{id}/POST
        ///     {
        ///        "post_text":"text"
        ///     }
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
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = e.Message });
            }
        }

        /// <summary>
        /// Публикует пост на Facebook c images
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /{id}/POST
        ///     {
        ///        "post_text":"text"
        ///     }
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
        [HttpPost("USER/{userId}/POSTIMAGE")]
        public async Task<JsonResult> PostFromImages(string userId, PostViewModel post)
        {
            _logger.LogInfo($"POST /api/v1/USER/{userId}/POSTIMAGE {post}");
            try
            {

                var result = await _bpApi.UserPostFromImages(userId, post);
                var res = result.Value;
                _logger.LogInfo($"Post /api/v1/USER/{userId}/POSTIMAGE result {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
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
            _logger.LogInfo($"PUT /api/v1/USER/{userId}/EDIT [Body]: post_id: '{editPost.post_id}'");
            try
            {
                var result = await _bpApi.UserEdit(userId, editPost);
                var res = result.Value;
                _logger.LogInfo($"PUT /api/v1/USER/{userId}/EDIT [Body]: post_id: '{editPost.post_id}', result {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = e.Message });
            }

        }

        /// <summary>
        /// Удаляет пост на Facebook
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /{id}/DELETE
        ///     {
        ///        "post_id":"id"
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
            _logger.LogInfo($"PUT /api/v1/USER/{userId}/EDIT [Body]: post_id: '{deletePost.post_id}'");
            try
            {
                var result = await _bpApi.UserDelete(userId, deletePost);
                var res = result.Value;
                _logger.LogInfo($"PUT /api/v1/USER/{userId}/EDIT [Body]: post_id: '{deletePost.post_id}' result {res}");
                return result;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = e.Message});
            }
        }
    }
}