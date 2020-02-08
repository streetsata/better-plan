using System.Collections.Generic;

namespace Identity.Models.Requests
{
    public class EditUserPermissionsRequest
    {
        public string id { get; set; }
        public List<string> claims { get; set; }
    }
}
