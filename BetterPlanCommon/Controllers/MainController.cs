
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
                return _bpApi.GetUsers();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = "Server error!" });
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
                return _bpApi.GetUserPosts(userId);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = "Server error!" });
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
        public JsonResult Post(string userId, [FromBody] PostViewModel post) 
        {
            _logger.LogInfo($"POST /api/v1/USER/{userId}/POST [Body]: post_text: '{post.post_text}'");
            try
            {
                return _bpApi.UserPost(userId, post);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = "Server error!" });
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
        public JsonResult EditPost(string userId, [FromBody] EditPostViewModel editPost)
        {
            _logger.LogInfo($"PUT /api/v1/USER/{userId}/EDIT [Body]: post_id: '{editPost.post_id}', edit_text: '{editPost.edit_text}'");
            try
            {
                return _bpApi.UserEdit(userId, editPost);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = "Server error!" });
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
        public JsonResult DeletePost(string userId, [FromBody] DeletePostViewModel deletePost)
        {
            _logger.LogInfo($"PUT /api/v1/USER/{userId}/EDIT [Body]: post_id: '{deletePost.post_id}'");
            try
            {
                return _bpApi.UserDelete(userId, deletePost);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                _logger.LogError(e.Message);
                return new JsonResult(new { status = "error", error_message = "Server error!" });
            }
        }
    }
}