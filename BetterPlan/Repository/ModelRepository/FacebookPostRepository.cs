using Contracts.ModelInterfaces;
using Entities;
using Entities.Models;

namespace Repository
{
    public class FacebookPostRepository : RepositoryBase<FacebookPost>, IFacebookPostRepository
    {
        public FacebookPostRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            
        }
    }
}
