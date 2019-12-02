using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterPlan.Models
{
    public class ModelPostContext:DbContext
    {

        public ModelPostContext(DbContextOptions<ModelPostContext> options)
            : base(options)
        {
        }

        public DbSet<ModelPost> ModelPosts { get; set; }
    }

}

