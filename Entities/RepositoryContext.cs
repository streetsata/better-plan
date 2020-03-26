using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public RepositoryContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=remotemysql.com;UserId=A33OXseZmP;Password=yeJmMLTiDX;database=A33OXseZmP;port=3306");
            }
        }


        public DbSet<Post> Posts { get; set; }
        #region DbSet<Entity>
        #endregion
    }
}
