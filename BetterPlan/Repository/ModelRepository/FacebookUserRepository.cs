using System;
using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class FacebookUserRepository : RepositoryBase<FacebookUser>, IFacebookUserRepository
    {
        public FacebookUserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<FacebookUser> GetAllUsers()
        {
            return FindAll()
                .OrderBy(user => user.Name)
                .ToList();
        }

        public FacebookUser GetFacebookUserById(Guid id)
        {
            return FindByCondition(user => user.FacebookUserId == id)
                .FirstOrDefault();
        }
    }
}
