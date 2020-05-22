using Contracts.ModelInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IProjectRepository Project { get; }
        IFacebookUserRepository FacebookUser { get; }
        IFacebookPostRepository FacebookPost { get; }
    }
}
