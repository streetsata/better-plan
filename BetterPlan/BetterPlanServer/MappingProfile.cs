using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterPlanServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<FacebookUser, UserDto>();
            CreateMap<FacebookPost, PostsGetAllDto>();
            CreateMap<FacebookPost, PostByIDDto>();
        }
    }
}
