using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterPlan.Models;
using BetterPlan.ViewModels;
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
        private IConfiguration _fb_config;
        private readonly ILogger<MainController> _logger;
        private Facebook _facebook;
        private BetterPlanContext _db;
        public MainController(IConfiguration Configuration, BetterPlanContext context, ILogger<MainController> logger)
        {
            _db = context;
            _logger = logger;
            _fb_config = Configuration.GetSection("FacebookConfig");
            _facebook = new Facebook(_fb_config.GetSection("AccessToken").Value, _fb_config.GetSection("PageId").Value);
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
            //_logger.LogInformation("This is my log"); // logging
            //return _db.GetJsonDbPostsAsync(Response).Result;
            return BetterPlanLogic.GetUsersAsync().Result;
        }
        /// <summary>
        ///     Возвращает посты пользователя
        /// </summary>
        /// <param name="id">ID Пользователя</param>
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
        [HttpGet("USER/{id}/POSTS")]
        public JsonResult GetUserPosts(string id)
        {
            //_logger.LogInformation("This is my log"); // logging
            //return _db.GetJsonDbPostsAsync(Response).Result;
            return BetterPlanLogic.GetUserPosts(Response, id);

            //return new JsonResult(new { status = id });
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
        /// <param name="id">ID Пользователя</param>
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
        [HttpPost("USER/{id}/POST")]
        [Produces("application/json")]
        public JsonResult Post(string id,[FromBody] PostViewModel post) // ,
        {
            //return _facebook.PostToFacebookAsync(Response,post, _db).Result;
            return BetterPlanLogic.UserPost(id,Response, post, _db);
            //return new JsonResult(new { status = id });

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
        /// <param name="post"></param>
        /// <param name="id">ID Пользователя</param>
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
        [HttpPut("USER/{id}/EDIT")]
        [Produces("application/json")]
        public JsonResult EditPost(string id,[FromBody] EditPostViewModel editPost)
        {
            return BetterPlanLogic.UserEdit(id,Response, editPost, _db);

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
        /// <param name="post"></param>
        /// <param name="id">ID Пользователя</param>
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
        [HttpDelete("USER/{id}/DELETE")]
        [Produces("application/json")]
        public JsonResult DeletePost(string id,[FromBody] DeletePostViewModel deletePost)
        {
            return BetterPlanLogic.UserDelete(id,Response, deletePost, _db);
        }
    }
}