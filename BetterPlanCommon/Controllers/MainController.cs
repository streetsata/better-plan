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
        /// Возвращает посты из базы данных
        /// </summary>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///       [
        ///         {
        ///           "post_id": "id",
        ///           "text": "post_text",
        ///           "link": "post_link",
        ///           "place": "id",
        ///           "action_id": "id",
        ///           "icon_id": "id",
        ///           "object_id": "id"
        ///         }
        ///       ]
        ///     }
        /// 
        /// </response>
        [HttpGet("GET")]
        [Produces("application/json")]
        public JsonResult GetPosts()
        {
            //_logger.LogInformation("This is my log"); // logging
            return _db.GetJsonDbPostsAsync(Response).Result;
            
        }

        /// <summary>
        /// Создает пост на Facebook
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /POST
        ///     {
        ///        "post_text":"text"
        ///     }
        ///
        /// </remarks>
        /// <param name="post"></param>
        /// <returns></returns>
        /// <response code="200">
        /// Sample response:
        /// 
        ///     {
        ///         "status": "OK"
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
        [HttpPost("POST")]
        [Produces("application/json")]
        public JsonResult Post([FromBody] PostViewModel post)
        {
            return _facebook.PostToFacebookAsync(Response,post, _db).Result;
        }

        /// <summary>
        /// Изменяет существующий пост на Facebook
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /EDIT
        ///     {
        ///        "post_id":"id",
        ///        "edit_text":"text"
        ///     }
        ///
        /// </remarks>
        /// <param name="post"></param>
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
        [HttpPut("EDIT")]
        [Produces("application/json")]
        public JsonResult EditPost([FromBody] EditPostViewModel editPost)
        {
            return _facebook.EditPostFacebookAsync(Response,editPost, _db).Result;
        }

        /// <summary>
        /// Удаляет пост на Facebook
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /DELETE
        ///     {
        ///        "post_id":"id"
        ///     }
        ///
        /// </remarks>
        /// <param name="post"></param>
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
        [HttpDelete("DELETE")]
        [Produces("application/json")]
        public JsonResult DeletePost([FromBody] DeletePostViewModel deletePost)
        {
            return _facebook.DeletePostFacebookAsync(Response,deletePost, _db).Result;
        }
    }
}