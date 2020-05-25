using Contracts.ModelInterfaces;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class FacebookPostRepository : RepositoryBase<FacebookPost>, IFacebookPostRepository
    {
        public FacebookPostRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public IEnumerable<FacebookPost> GetAllPosts(Guid facebookUserId)
        {
            return FindByCondition(facebookPost => facebookPost.FacebookUserId.Equals(facebookUserId)).ToList();
        }

        public FacebookPost GetFacebookPostById(Guid postId)
        {
            return FindByCondition(facebookPost => facebookPost.PostId.Equals(postId))
                .FirstOrDefault();
        }
    }
}
