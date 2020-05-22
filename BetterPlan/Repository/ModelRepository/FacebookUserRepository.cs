using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class FacebookUserRepository : RepositoryBase<FacebookUser>, IFacebookUserRepository 
    {
        public FacebookUserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
