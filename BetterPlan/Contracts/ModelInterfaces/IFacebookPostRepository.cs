using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts.ModelInterfaces
{
    public interface IFacebookPostRepository : IRepositoryBase<FacebookPost>
    {
        IEnumerable<FacebookPost> GetAllPosts(Guid facebookUserId);
        FacebookPost GetFacebookPostById(Guid postId);
    }
}
