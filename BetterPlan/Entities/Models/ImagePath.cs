using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class ImagePath
    {
        public Guid ImagePathID { get; set; }
        public string Path { get; set; }

        // Nav
        [ForeignKey(nameof(FacebookPost))]
        public Guid FacebookPostId { get; set; }
        public FacebookPost FacebookPost { get; set; }

    }
}
