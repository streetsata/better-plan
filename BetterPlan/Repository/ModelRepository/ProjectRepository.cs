﻿using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return FindAll()
                .OrderBy(p => p.Name)
                .ToList();
        }

        public Project GetProjectById(Guid projectId)
        {
            return FindByCondition(project => project.ProjectId.Equals(projectId))
                .FirstOrDefault();
        }
    }
}
