using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterPlan.ViewModels
{
    public class EditPostViewModel
    {
        [Required]
        public string post_id { get; set; }
        [Required]
        public string edit_text { get; set; }
        public string place { get; set; }
    }
}
