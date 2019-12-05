using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PostingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostingAPI.Controllers
{
	[Produces("application/json")]
	[Route("api/PostModelsController")]
	[ApiController]
	public class PostModelsController : ControllerBase
	{
		private readonly PostContext _context;
		readonly Facebook facebook = new Facebook("EAAnbt2t9bm0BAFpRIWQITrv2u0rAuTw5fC5CEz9G5PsAv2HYw7MylfLgaOAgAZCT9mAZCOhA7oQlXBHOxZCZAYVSedAn7BmeRvBPuGDlj1qUOXtfetzYdlj2AA6hRSSqkIMuWqPqe6PstE9xWGxlzxNT84GZAUXh2sDQbs1ZCjkNlDakRCSayE", "358463971694050");

		public PostModelsController(PostContext context)
		{
			_context = context;
		}

		// GET: api/PostModelsController
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PostModel>>> GetPostModels()
		{
			return await _context.PostModels.Where(d => d.IsDelete == false).ToListAsync();
		}

		// PUT: api/PostModelsController
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPut]
		[Route("PutPost")]
		public async Task<JsonResult> PutPostModel([FromBody]PostModel postModel)
		{
			var model = await _context.PostModels.FirstOrDefaultAsync(i => i.PostId == postModel.PostId);
			if (model == null)
			{
				return new JsonResult(400);
			}

			var res = facebook.PutPost(postModel.PostId, postModel.TextOfPost);

			if (res.Result.Item1 == 200)
			{
				model.TextOfPost = postModel.TextOfPost;
				postModel.ModificationDate = DateTime.Now;
				await _context.SaveChangesAsync();
				return new JsonResult(res.Result.Item1);
			}

			return new JsonResult(res.Result.Item1);
		}

		// POST: api/PostModelsController
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see https://aka.ms/RazorPagesCRUD.
		[HttpPost]
		[Route("Posting")]
		public async Task<JsonResult> PostPostModel([FromBody]PostModel postModel)
		{
			if (postModel.TextOfLink == null)
			{
				var res = facebook.PublishSimplePost(postModel.TextOfPost);

				if (res.Result.Item1 == 200)
				{
					var model = postModel;
					model.DateOfPosting = DateTime.Now;
					var json = JObject.Parse(res.Result.Item2);

					model.PostId = json["id"].ToString();
					_context.PostModels.Add(postModel);
					await _context.SaveChangesAsync();

					return new JsonResult(200);
				}
			}
			else
			{
				var res = facebook.PublishToFacebook(postModel.TextOfPost, postModel.TextOfLink);

				if (res.Result.Item1 == 200)
				{
					var model = postModel;
					model.DateOfPosting = DateTime.Now;

					model.PostId = res.Result.Item2;
					_context.PostModels.Add(postModel);
					await _context.SaveChangesAsync();

					return new JsonResult(200);
				}
			}

			return new JsonResult(400);
		}

		// DELETE: api/PostModelsController
		[HttpDelete]
		[Route("DeletePost")]
		public async Task<JsonResult> DeletePostModel([FromBody]PostModel postModel)
		{
			var model = await _context.PostModels.FirstOrDefaultAsync(i => i.PostId == postModel.PostId);
			if (model == null)
			{
				return new JsonResult(400);
			}

			var res = facebook.DeletePost(model.PostId);

			if (res.Result.Item1 == 200)
			{
				model.IsDelete = true;
				model.DateOfDeletion = DateTime.Now;
				await _context.SaveChangesAsync();
				return new JsonResult(res.Result.Item1);
			}

			return new JsonResult(res.Result.Item1);
		}

		private bool PostModelExists(int id)
		{
			return _context.PostModels.Any(e => e.Id == id);
		}
	}
}
