using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("project")]
    public class Project
    {
        public Guid projectId { get; set; }

        // Nav
        public ICollection<FacebookUser> facebookUsers { get; set; }
    }
}
