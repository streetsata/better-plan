using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetterPlanServer.Controllers
{
    [Route("api/FacebookUser")]
    [ApiController]
    public class FacebookUserController : ControllerBase
    {
        private ILoggerManager logger;
        private IRepositoryWrapper repository;
        private IMapper mapper;

        public FacebookUserController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = repository.FacebookUser.GetAllUsers();
                logger.LogInfo("Returned all users from DB");

                var usersResult = mapper.Map<IEnumerable<UserDto>>(users);

                return Ok(usersResult);

            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                var user = repository.FacebookUser.GetFacebookUserById(id);

                if (user == null)
                {
                    logger.LogError($"User with id: {id}, hasn't been found in DB");
                    return NotFound();
                }
                else
                {
                    logger.LogInfo($"Returned user with id: {id}");

                    var userResult = mapper.Map<UserDto>(user);
                    return Ok(userResult);
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside GetUserByID action: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}