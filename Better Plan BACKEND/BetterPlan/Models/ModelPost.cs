using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace BetterPlan.Models
{
    public class ModelPost
    {
        [Key]
        public long Id { get; set; }


        public string IdPost { get; set; }
        public string Text { get; set; }
        public string ReferenceImage { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime DeleteDateTime { get; set; }
        public bool IsDelete { get; set; }

    }
}
