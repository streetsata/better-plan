using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Facebook
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

        public async Task<JsonResult> PostToFacebookAsync(HttpResponse response, PostViewModel post, BetterPlanContext _db)
        {
            if (post.post_text == null) return new JsonResult(new { status = "error", error_message = "post_text doesn't exist" });
            var data = new Dictionary<string, string> {
                { "access_token", _accessToken },
                { "message", post.post_text },
            };
            if (post.place != null) data.Add("place", post.place);

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
                response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = rezTextJson["error"]["message"].ToString() });
            }
            _db.Posts.Add(new Post() { Post_id = rezTextJson["id"].ToString(), Text = post.post_text, Place = post.place });
            await _db.SaveChangesAsync();
            return new JsonResult(new { status = "OK", post_id = rezTextJson["id"].ToString() });
        }
        public async Task<JsonResult> DeletePostFacebookAsync(HttpResponse response, DeletePostViewModel deletePost, BetterPlanContext _db)
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
                response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = rezTextJson["error"]["message"].ToString() });
            }
            _db.Remove(_db.Posts.Single(post => post.Post_id == deletePost.post_id));
            await _db.SaveChangesAsync();
            return new JsonResult(new { status = "OK" });
        }

        public async Task<JsonResult> EditPostFacebookAsync(HttpResponse response, EditPostViewModel editPost, BetterPlanContext _db)
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
                response.StatusCode = 400;
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
    public class BetterPlanLogic
    {

        static Dictionary<string, string> TempUsersDb = new Dictionary<string, string>() {
            { "100559284835939","EAAHD5fytWZAABAGe8mGPsXkL4mZB8fi5BZB0G2ZCCvRvTcZBvA3eTA5CvjpXkOA1ZAZCZC0ZCB7sfk6SV9KZAMN2jLXwpR67oqZAXtL07RZC0HgoZBlz5eBQbD5im1P9dGGbWE2Ds6pTZCVEyqCSgSZCvTZA3IWuSV4HvzAquYLejh93YEhE50CtIB4OcQeI" },
            { "895127244222164","EAAjnVI1sCkwBADWHQOeCunZCMMezGDc2Xit0rb4ZCM86gnOe78qMUtjwqkDel73hJPwilAZANenNKPufhXRFEbydLEplhSwIuRORFe4HICwflMQqVEyFR49c9VsgZCVsYFivZCmNYEOdCuXJ7auyVFZCN4eVBwqP2hFi8EvkfI7QZDZD" }
        };
        public static async Task<JsonResult> GetUsersAsync()
        {
            using (var http = new HttpClient())
            {
                List<object> usersObj = new List<object>();

                foreach (var user in TempUsersDb)
                {
                    var httpResponse = await http.GetAsync($"https://graph.facebook.com/me?access_token={user.Value}");
                    var httpContent = await httpResponse.Content.ReadAsStringAsync();
                    var json = JObject.Parse(httpContent);
                    usersObj.Add(new { id = json["id"].ToString(), name = json["name"].ToString() });
                }
                return new JsonResult(usersObj);

            }

        }

        public static JsonResult GetUserPosts(HttpResponse response, string user_id)
        {
            if (!TempUsersDb.ContainsKey(user_id))
            {
                response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "id is doesn't exists!" });
            }


            var posts = GetPagePostsAsync(GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result.Item1, GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result.Item2).Result;
            List<object> postsObj = new List<object>();
            foreach (var post in posts)
            {
                Dictionary<string, string> pairs = new Dictionary<string, string>();

                pairs.Add("post_id", post["id"].ToString());
                if (post["message"] != null) pairs.Add("text", post["message"].ToString());
                if (post["full_picture"] != null) pairs.Add("img", post["full_picture"].ToString());
                if (post["place"] != null) pairs.Add("place", post["place"]["id"].ToString());

                postsObj.Add(pairs);
            }
            return new JsonResult(postsObj);
        }
        public static async Task<JToken> GetPagePostsAsync(string page_id, string token)
        {
            using (var http = new HttpClient())
            {
                //string id = "100396138106408_134654548013900";
                //string _accessToken = "EAAjnVI1sCkwBAAKxVhMZAdXKCq0fK9tLZCJkOuoXgvY8JFPkAyZCGTduwiqtIDZAJLEddXCp4rZC0gHx5QChk9nuhyy7xNT0ZCMMcmRxROWNjIGl72pVBeQVnbgjEUZCqEfjMl5qyjmcSSIZBPycNZCTPq5oJDPC0lNroFvz96SSsa8dcOR5KarrvveGxF5DkstQZD";

                var httpResponse = await http.GetAsync($"https://graph.facebook.com/{page_id}/feed?access_token={token}&fields=message,place,full_picture");
                var httpContent = await httpResponse.Content.ReadAsStringAsync();

                var json = JObject.Parse(httpContent)["data"];

                return json;
            }
        }
        public static async Task<Tuple<string, string>> GetPageIdAndTokenAsync(string UserToken)
        {
            using (var http = new HttpClient())
            {
                string url = $"https://graph.facebook.com/me/accounts?access_token={UserToken}";

                var httpResponse = await http.GetAsync(url);
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
                var rezJson = JObject.Parse(httpContent);


                return new Tuple<string, string>(
                    rezJson["data"][0]["id"].ToString(),
                    rezJson["data"][0]["access_token"].ToString()
                );
            }
        }
        public static JsonResult UserPost(string user_id, HttpResponse Response, PostViewModel post, BetterPlanContext _db)
        {
            if (!TempUsersDb.ContainsKey(user_id))
            {
                Response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "id is doesn't exists!" });
            }
            var page = GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result;
            return new Facebook(page.Item2, page.Item1).PostToFacebookAsync(Response, post, _db).Result;
        }
        public static JsonResult UserEdit(string user_id, HttpResponse Response, EditPostViewModel editPost, BetterPlanContext _db)
        {
            if (!TempUsersDb.ContainsKey(user_id))
            {
                Response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "id is doesn't exists!" });
            }
            var page = GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result;
            return new Facebook(page.Item2, page.Item1).EditPostFacebookAsync(Response, editPost, _db).Result;
        }
        public static JsonResult UserDelete(string user_id, HttpResponse Response, DeletePostViewModel deletePost, BetterPlanContext _db)
        {
            if (!TempUsersDb.ContainsKey(user_id))
            {
                Response.StatusCode = 400;
                return new JsonResult(new { status = "error", error_message = "id is doesn't exists!" });
            }
            var page = GetPageIdAndTokenAsync(TempUsersDb[user_id]).Result;
            return new Facebook(page.Item2, page.Item1).DeletePostFacebookAsync(Response, deletePost, _db).Result;
        }
    }
}
