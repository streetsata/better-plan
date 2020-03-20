using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class PostRepository:RepositoryBase<Post>,IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
