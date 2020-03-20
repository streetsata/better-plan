using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {}

        public DbSet<Post> Posts { get; set; }
        #region DbSet<Entity>
        #endregion
    }
}
