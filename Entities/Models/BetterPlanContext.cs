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
            //optionsBuilder.UseSqlServer("Data Source=SQL5052.site4now.net;Initial Catalog=DB_A56840_betterplanDBTest;User Id=DB_A56840_betterplanDBTest_admin;Password=betterplan2020;");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=remotemysql.com;UserId=A33OXseZmP;Password=yeJmMLTiDX;database=A33OXseZmP;port=3306");
            }
        }
        public DbSet<Post> Posts { get; set; }


    }
}