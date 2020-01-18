using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterPlan.ViewModels
{
    public class PostViewModel
    {
        [Required]
        public string post_text { get; set; }
        public string link { get; set; }
        public string place { get; set; }
        public string action_id { get; set; }
        public string icon_id { get; set; }
        public string object_id { get; set; }
    }
}
