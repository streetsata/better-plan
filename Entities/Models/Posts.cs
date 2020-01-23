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
        public int Id { get; set; }
        public string Post_id { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }
    }
}
