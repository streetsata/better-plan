using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IPostRepository _post;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IPostRepository postRepository
        {
            get
            {
                if (_post == null)
                {
                    _post = new PostRepository(_repositoryContext);
                }

                return _post;
            }
        }

        public Int32 Save()
        {
           return _repositoryContext.SaveChanges();
        }
    }
}
