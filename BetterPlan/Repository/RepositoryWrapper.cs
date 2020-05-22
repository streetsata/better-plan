using Contracts;
using Contracts.ModelInterfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext repositoryContext;
        private IProjectRepository project;
        private IFacebookUserRepository facebookUser;
        private IFacebookPostRepository facebookPost;

        public IProjectRepository Project
        {
            get
            {
                if (project == null)
                {
                    project = new ProjectRepository(repositoryContext);
                }

                return project;
            }
        }

        public IFacebookUserRepository FacebookUser
        {
            get
            {
                if (facebookUser == null)
                {
                    facebookUser = new FacebookUserRepository(repositoryContext);
                }

                return facebookUser;
            }
        }

        public IFacebookPostRepository FacebookPost
        {
            get
            {
                if (facebookPost == null)
                {
                    facebookPost = new FacebookPostRepository(repositoryContext);
                }

                return facebookPost;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public void Save()
        {
            repositoryContext.SaveChanges();
        }
    }
}
