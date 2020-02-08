using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Models.Requests
{
    public class RefreshUserTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
