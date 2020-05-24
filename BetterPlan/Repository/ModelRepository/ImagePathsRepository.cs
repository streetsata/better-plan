using Contracts.ModelInterfaces;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.ModelRepository
{
    public class ImagePathsRepository: RepositoryBase<ImagePath>, IImagePathsRepository
    {
        public ImagePathsRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
