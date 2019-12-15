using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace First_API_App.Models
{
    public class Post
    {

        [Key]
        public int Id { get; set; }
        public string PostText { get; set; }
        public string UrlPicture { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateModification { get; set; }
        public DateTime? DateDelete { get; set; }

        public Post()
        {
            DateCreate = DateTime.Now;
            DateModification = null;
            DateDelete = null;
        }
    }
}
