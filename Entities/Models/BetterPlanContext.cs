using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class BetterPlanContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public BetterPlanContext(DbContextOptions<BetterPlanContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
