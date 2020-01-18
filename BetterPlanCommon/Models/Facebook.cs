using BetterPlan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BetterPlan.Models
{
    class Facebook
    {
        readonly string _accessToken;
        readonly string _facebookAPI = "https://graph.facebook.com/";
        readonly string _pageEdgeFeed = "feed";
        readonly string _postToPageURL;

        public Facebook(string accessToken, string pageID)
        {
            _accessToken = accessToken;
            _postToPageURL = $"{_facebookAPI}{pageID}/{_pageEdgeFeed}";
        }

        public async Task<JsonResult> PostToFacebookAsync(PostViewModel post, BetterPlanContext _db)
        {
            if (post.post_text == null) return new JsonResult(new { status = "error", error_message = "post_text doesn't exist" });
            var data = new Dictionary<string, string> {
                { "access_token", _accessToken },
                { "message", post.post_text },
            };
            if (post.link != null) data.Add("link", post.link);
            if (post.place != null) data.Add("place", post.place);
            if (post.action_id != null) data.Add("og_action_type_id", post.action_id);
            if (post.icon_id != null) data.Add("og_icon_id", post.icon_id);
            if (post.object_id != null) data.Add("og_object_id", post.object_id);

            string res = string.Empty;
            using (var http = new HttpClient())
            {
                var httpResponse = await http.PostAsync(_postToPageURL, new FormUrlEncodedContent(data));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                res = httpContent;
            }
            var rezTextJson = JObject.Parse(res);
            if (rezTextJson["error"] != null)
            {
                return new JsonResult(new { status = "error", error_message = rezTextJson["error"]["message"].ToString() });
            }
            _db.Posts.Add(new Post() { Post_id = rezTextJson["id"].ToString(), Text = post.post_text, Link = post.link, Place = post.place, Action_id = post.action_id, Icon_id = post.icon_id, Object_id = post.icon_id });
            await _db.SaveChangesAsync();
            return new JsonResult(new { status = "OK", post_id = rezTextJson["id"].ToString() });
        }
        public async Task<JsonResult> DeletePostFacebookAsync(DeletePostViewModel deletePost, BetterPlanContext _db)
        {
            if (deletePost.post_id == null) return new JsonResult(new { status = "error", error_message = "post_id doesn't exist" });

            string res = string.Empty;
            using (var http = new HttpClient())
            {
                var httpResponse = await http.DeleteAsync($"{_facebookAPI}{deletePost.post_id}?access_token={_accessToken}");
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                res = httpContent;
            }
            var rezTextJson = JObject.Parse(res);
            if (rezTextJson["error"] != null)
            {
                return new JsonResult(new { status = "error", error_message = rezTextJson["error"]["message"].ToString() });
            }
            _db.Remove(_db.Posts.Single(post => post.Post_id == deletePost.post_id));
            await _db.SaveChangesAsync();
            return new JsonResult(new { status = "OK" });
        }

        public async Task<JsonResult> EditPostFacebookAsync(EditPostViewModel editPost, BetterPlanContext _db)
        {
            if (editPost.post_id == null) return new JsonResult(new { status = "error", error_message = "post_id doesn't exist" });
            if (editPost.edit_text == null) return new JsonResult(new { status = "error", error_message = "edit_text doesn't exist" });

            var data = new Dictionary<string, string> {
                { "access_token", _accessToken },
                { "message", editPost.edit_text }
            };


            if (editPost.place != null) data.Add("place", editPost.place);
            string res = string.Empty;
            using (var http = new HttpClient())
            {


                var httpResponse = await http.PostAsync($"https://graph.facebook.com/{editPost.post_id}", new FormUrlEncodedContent(data));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                res = httpContent;
            }
            var rezTextJson = JObject.Parse(res);
            if (rezTextJson["error"] != null)
            {
                return new JsonResult(new { status = "error", error_message = rezTextJson["error"]["message"].ToString() });
            }
            //_db.Find
            Post dbpost = _db.Posts.SingleOrDefault(post => post.Post_id == editPost.post_id);
            dbpost.Text = editPost.edit_text;
            if (editPost.place != null) dbpost.Place = editPost.place;
            await _db.SaveChangesAsync();
            return new JsonResult(new { status = "OK" });
        }
    }
}
