using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacebookPostBack.BL;
using FacebookPostBack.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FacebookPostBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FacebookPostController : ControllerBase
    {
        [HttpGet]
        public JsonResult Index() {
            return new JsonResult(new { Test = "Awesome" });
        }
        [HttpPost]
        public JsonResult FacebookPost([FromBody] PostViewModel PostModel) {
            Facebook facebook = new Facebook("EAAjnVI1sCkwBABcMIB2AYmVZCPYeZAKiUeZA2bjEjDFc0bYVOJy8lGvyL4RKTlv9Ux8Ol7VQ8pGHsJYBh2u0pBpoMn0LI2jUiIzRuQwVYXJNaZCGOarcUMEk1cBhNB5jEzyJsn9ZAPBQq3FOpaZByCUZCa9bBiUF4AqyCGyvQhpZAgZDZD", "100396138106408");
            string result = string.Empty;
            if(PostModel.post_img_url == null) {
                result = facebook.PublishToFacebook(PostModel.post_text);
                var json = JObject.Parse(result);
                result = json["id"].ToString();
                return new JsonResult(new { result = "OK", post_id = result });
            } else {
                result = facebook.PublishToFacebook(PostModel.post_text, PostModel.post_img_url);
                return new JsonResult(new { result = "OK" });
            }
        }
    }
}