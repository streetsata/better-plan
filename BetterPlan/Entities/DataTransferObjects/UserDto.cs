using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public Guid FacebookUserId { get; set; }

        [Required(ErrorMessage = "Property is required")]
        public string Name { get; set; }
    }
}
