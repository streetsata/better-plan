using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using System.Configuration;
using System.Collections.Specialized;

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

        public BetterPlanContext()
        {
            Database.EnsureCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = ConfigurationManager.AppSettings.Get("DefaultConnection");
            //var connectionString = configure["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseSqlServer(
                "Data Source=SQL5050.site4now.net;Initial Catalog=DB_A54339_betterplanDBTest;User Id=DB_A54339_betterplanDBTest_admin;Password=betterplan2020;");
        }
    }
}