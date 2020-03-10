using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.Requests
{
    public class ForgotPasswordRequest
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
