using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<FacebookUser> FacebookUsers { get; set; }
        public DbSet<FacebookPost> FacebookPosts { get; set; }
        public DbSet<ImagePath> ImagePaths { get; set; }
    }
}
