using System;
using System.Collections.Generic;
using System.Text;
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

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
