using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetterPlanServer.Controllers
{
    [Route("api/FacebookPost")]
    [ApiController]
    public class FacebookPostController : ControllerBase
    {
        private ILoggerManager logger;
        private IRepositoryWrapper repository;
        private IMapper mapper;

        public FacebookPostController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{facebookUserId}")]
        public IActionResult GetAllPosts(Guid facebookUserId)
        {
            try
            {

                var user = repository.FacebookUser.GetFacebookUserById(facebookUserId);

                if (user == null)
                {
                    logger.LogError($"User with facebookUserId: {facebookUserId}, hasn't been found in DB");
                    return NotFound();
                }


                var posts = repository.FacebookPost.GetAllPosts(facebookUserId);

                if (posts == null)
                {
                    logger.LogError($"Posts with facebookUserId: {facebookUserId}, hasn't been found in DB");
                    return NotFound();
                }
                else
                {
                    logger.LogInfo($"Returned posts with facebookUserId: {facebookUserId}");

                    var allPostsResult = mapper.Map<IEnumerable<PostsGetAllDto>>(posts);
                    return Ok(allPostsResult);
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside facebookUserId action: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("{facebookUserId}/{PostId}")]
        [HttpGet("{facebookUserId}/{PostId}")]
        public IActionResult GetPostByID(Guid facebookUserId, Guid postId)
        {
            try
            {
                FacebookPost post = repository.FacebookPost.GetFacebookPostById(postId);

                if (post == null)
                {
                    logger.LogError($"Post with postId: {postId}, hasn't been found in DB");
                    return NotFound();
                }
                else
                {
                    logger.LogInfo($"Returned post with postId: {postId}");

                    var postResult = mapper.Map<PostByIDDto>(post);
                    return Ok(postResult);
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside postId action: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

}