using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Models.Requests
{
    public class ExternalLoginModel
    {
        public string Email { get; set; }

        public ClaimsPrincipal Principal { get; set; }
    }
}
