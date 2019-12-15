using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using First_API_App.Models;


/// <summary>
/// https://parkdrive.ua/storage/cars/44484/Z3mKu5p4LB_preview.jpg
/// </summary>

namespace First_API_App.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly Context _context;
        private string accessToken;
        private string pageID;
        Facebook facebook;

        public PostsController(Context context)
        {
            _context = context;
        }
        
       // GET: api/Posts
        [HttpGet]
        public async Task<JsonResult> GetPosts() => new JsonResult(await _context.Posts.Where(q => q.DateDelete == null).ToListAsync());

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null) return new JsonResult(new { answer = false });

            return new JsonResult(new { post });
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<JsonResult> PutPost(int id, Post post)
        {
            if (!PostExists(id)) return new JsonResult(new { answer = false });

            Post changePost = _context.Posts.Find(id);
            changePost.PostText = post.PostText;
            changePost.UrlPicture = post.UrlPicture;
            changePost.DateModification = DateTime.Now;

            await _context.SaveChangesAsync();
            
            return new JsonResult(new { answer = true });
        }

        // POST: api/Posts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<JsonResult> PostPost( Post post )
        {
            if (post.PostText == "") return new JsonResult(new { answer = false });

            accessToken = "EAAHmpBL4WgoBAL369pXDj2lzyC9Io7LjtJZBfNL7dPSpLBFrOZBRnYx6SqMCR4ZCOiHW1WfaGCZBO4VJwwbxaJX7HtSCwDewwxiJkRngv9jNeJ3ZBRKZCpFSyWYBagUSCb7kBZBjbS2IzBrZA8tfnVVNAqZAQrRQpkqjAVJhV2xMS3AZDZD";
            pageID = "106759404131789";
            facebook = new Facebook(accessToken, pageID);
            string result = "";
            //result = facebook.PublishToFacebook(post.PostText, post.UrlPicture);

            //if (result != "OK")
            //{
            //    return new JsonResult(new { answer = false });
            //}

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return new JsonResult(new { answer = true });
            //return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return "Post not found";
            }

            post.DateDelete = DateTime.Now;

            await _context.SaveChangesAsync();

            return "Post was deleted!";
        }

        private bool PostExists(int id) => _context.Posts.Any(e => e.Id == id);
    }
}
