using System.Collections.Generic;

namespace Identity.Models.Requests
{
    public class EditUserRolesRequest
    {
        public string id { get; set; }
        public List<string> roles { get; set; }
    }
}
