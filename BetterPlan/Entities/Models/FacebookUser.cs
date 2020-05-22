using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("facebook_user")]
    public class FacebookUser
    {
        public Guid FacebookUserId { get; set; }

        // Nav
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<FacebookPost> FacebookPosts { get; set; }
    }
}
