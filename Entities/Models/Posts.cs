using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string FacebookPostId { get; set; }
        public string Text { get; set; }
    }
}
