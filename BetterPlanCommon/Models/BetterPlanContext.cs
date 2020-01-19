using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterPlan.Models
{
    public class BetterPlanContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public BetterPlanContext(DbContextOptions<BetterPlanContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public async Task<JsonResult> GetJsonDbPostsAsync(HttpResponse response)
        {
            var PostsList = await Posts.ToListAsync();
            List<object> posts = new List<object>();
            foreach (var item in PostsList)
            {
                posts.Add(new { id = item.Id, post_id = item.Post_id, text = item.Text, place = item.Place });
            }

            return new JsonResult(posts);
        }
    }
}
