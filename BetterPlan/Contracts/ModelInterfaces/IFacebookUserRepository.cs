using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IFacebookUserRepository : IRepositoryBase<FacebookUser>
    {
        IEnumerable<FacebookUser> GetAllUsers();
        FacebookUser GetFacebookUserById(Guid id);

    }
}
