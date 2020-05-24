using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("image_path")]
    public class ImagePath
    {
        public Guid ImagePathID { get; set; }

        [Required(ErrorMessage = "Property is required")]
        public string Path { get; set; }

        // Nav
        [ForeignKey(nameof(FacebookPost))]
        public Guid FacebookPostId { get; set; }
        public FacebookPost FacebookOwnerPost { get; set; }
    }
}
