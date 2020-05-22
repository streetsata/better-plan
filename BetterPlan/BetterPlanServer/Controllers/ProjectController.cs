using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BetterPlanServer.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ILoggerManager logger;
        private IRepositoryWrapper repository;

        public ProjectController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            try
            {
                var projects = repository.Project.GetAllProjects();

                logger.LogInfo($"Returned all projects from database.");

                return Ok(projects);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside GetAllProjects action: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
