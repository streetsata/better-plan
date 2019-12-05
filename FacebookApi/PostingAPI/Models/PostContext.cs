using Microsoft.EntityFrameworkCore;

namespace PostingAPI.Models
{
	public class PostContext : DbContext
	{
		public PostContext(DbContextOptions<PostContext> options) : base(options)
		{
		}

		public DbSet<PostModel> PostModels { get; set; }
	}
}
