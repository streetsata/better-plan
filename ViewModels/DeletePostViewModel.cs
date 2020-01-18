using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterPlan.ViewModels
{
    public class DeletePostViewModel
    {
        [Required]
        public string post_id { get; set; }
    }
}
