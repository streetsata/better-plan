using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ValuesController : ControllerBase
    {
        private readonly UserContext userContext;

        public ValuesController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        // GET: api/Values
        [HttpGet]
        public JsonResult Get() => new JsonResult(userContext.Users.ToList());   
    }
}
