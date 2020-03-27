using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IPostRepository postRepository { get; }
        Task<Int32> Save();
    }
}
