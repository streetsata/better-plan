using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BetterPlanServer.Controllers
{
    [Route("api/Project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private ILoggerManager logger;
        private IRepositoryWrapper repository;
        private IMapper mapper;

        public ProjectController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            try
            {
                var projects = repository.Project.GetAllProjects();
                logger.LogInfo("Returned all projects from DB");

                var projectsResult = mapper.Map<IEnumerable<ProjectDto>>(projects);

                return Ok(projectsResult);

            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside GetAllProjects action: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProjectByID(Guid id)
        {
            try
            {
                var project = repository.Project.GetProjectById(id);

                if (project == null)
                {
                    logger.LogError($"Project with id: {id}, hasn't been found in DB");
                    return NotFound();
                }
                else
                {
                    logger.LogInfo($"Returned projectwith id: {id}");

                    var projectResult = mapper.Map<ProjectDto>(project);
                    return Ok(projectResult);
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong inside GetProjectByID action: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
