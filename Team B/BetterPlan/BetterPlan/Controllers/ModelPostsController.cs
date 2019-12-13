using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetterPlan.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json.Linq;

namespace BetterPlan.Controllers
{
    [Produces("application/json")]
    [Route("api/ModelPostsController")]
    public class ModelPostsController : ControllerBase
    {
        private readonly ModelPostContext _context;
        Facebook.Facebook facebook = new Facebook.Facebook("EAAiLB13fdegBAO5ssNSrU0DaJchbw2ARTFIfk8ugDx3HteGRx6zHpnAtNEJJr8hyvxM8gkbkb3XYYnngwLe638MNVx16xgOAZCsSWsO3zbcGUMrOeDAp0AocVMMtQJzc89m0WOr7RjPBWDSZBXVFHUcUBYuHz6sWVxIV6FZBpJE7uDbSrIu", "109632140505509");
        public ModelPostsController(ModelPostContext context)
        {
            _context = context;
        }

        // GET: api/ModelPostsController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelPost>>> GetModelPosts()
        {
            return await _context.ModelPosts.Where(e => e.IsDelete == false).ToListAsync();
        }

        // PUT: api/ModelPostsController
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        [Route("putPost")]
        public async Task<JsonResult> PutModelPost([FromBody]ModelPost modelPost)
        {
            var model = await _context.ModelPosts.FirstOrDefaultAsync(e => e.IdPost == modelPost.IdPost);
            if (model == null)
            {
                return new JsonResult(400);
            }

            var res = facebook.PutPost(modelPost.IdPost, modelPost.Text);

            if (res.Result.Item1 == 200)
            {
                model.Text = modelPost.Text;
                modelPost.UpdateDateTime = DateTime.Now;
                _context.SaveChangesAsync();
                return new JsonResult(res.Result.Item1);
            }
            return new JsonResult(res.Result.Item1);
        }

        // POST: api/ModelPostsController
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("post")]
        public async Task<JsonResult> PostModelPost([FromBody]ModelPost modelPost)
        {
            if (modelPost.ReferenceImage == null)
            {
                var res = facebook.PublishSimplePost(modelPost.Text);
                if (res.Result.Item1 == 200)
                {
                    var model = modelPost;
                    model.CreateDateTime = DateTime.Now;
                    var json = JObject.Parse(res.Result.Item2);

                    model.IdPost = json["id"].ToString();
                    _context.ModelPosts.Add(modelPost);
                    await _context.SaveChangesAsync();
                    return new JsonResult(200);
                }
            }
            else
            {
                var res = facebook.PublishToFacebook(modelPost.Text, modelPost.ReferenceImage);
                if (res.Result.Item1 == 200)
                {
                    var model = modelPost;
                    model.CreateDateTime = DateTime.Now;

                    model.IdPost = res.Result.Item2;
                    _context.ModelPosts.Add(modelPost);
                    await _context.SaveChangesAsync();
                    return new JsonResult(200);
                }
            }
            return new JsonResult(400);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelPost"></param>
        /// <returns></returns>
        // DELETE: api/ModelPostsController
        [HttpDelete]
        [Route("deletePost")]
        public async Task<JsonResult> DeleteModelPost([FromBody]ModelPost modelPost)
        {
            var model = await _context.ModelPosts.FirstOrDefaultAsync(e => e.IdPost == modelPost.IdPost);
            if (model == null)
            {
                return new JsonResult(400);
            }

            var res = facebook.DeletePost(model.IdPost);
            if (res.Result.Item1 == 200)
            {
                model.IsDelete = true;
                model.DeleteDateTime = DateTime.Now;
                await _context.SaveChangesAsync();
                return new JsonResult(res.Result.Item1);
            }

            return new JsonResult(res.Result.Item1);
        }

        private bool ModelPostExists(long id)
        {
            return _context.ModelPosts.Any(e => e.Id == id);
        }
    }
}
