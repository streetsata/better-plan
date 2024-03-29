﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("facebook_users")]
    public class FacebookUser
    {
        public Guid FacebookUserId { get; set; }

        [Required(ErrorMessage = "Property is required")]
        public string Name { get; set; }
        // Nav
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<FacebookPost> FacebookPosts { get; set; }
    }
}
