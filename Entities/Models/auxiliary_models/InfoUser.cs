using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Entities.Models.auxiliary_models
{
    public  static class InfoUser
    {
        public class Data
        {
            public string url { get; set; }
        }

        public class Picture
        {
            public Data data { get; set; }
        }

        public class Cover
        {
            public string cover_id { get; set; }
            public int offset_x { get; set; }
            public int offset_y { get; set; }
            public string source { get; set; }
            public string id { get; set; }
        }

        public class Datum
        {
            public Picture picture { get; set; }
            public Cover cover { get; set; }
            public string name { get; set; }
            public string id { get; set; }
        }

        public class Cursors
        {
            public string before { get; set; }
            public string after { get; set; }
        }

        public class Paging
        {
            public Cursors cursors { get; set; }
        }

        public class RootObject
        {
            public List<Datum> data { get; set; }
            public Paging paging { get; set; }
        }

        public static Dictionary<string, string> GetJSON(JObject post, string id)
        {
            var UserRootObject = JsonConvert.DeserializeObject<RootObject>(post.ToString());

            Dictionary<string, string> pairs = new Dictionary<string, string>();
            var user = UserRootObject.data[0];
            pairs.Add("id", id);

            if (user.name != null)
            {
                pairs.Add("name", user.name);
            }

            if (user.picture != null)
            {
                pairs.Add("picture", user.picture.data.url);
            }
            else
            {
                pairs.Add("picture", String.Empty);

            }

            if (user.cover != null)
            {
                pairs.Add("cover", user.cover.source);
            }
            else
            {
                pairs.Add("cover", String.Empty);

            }

            return pairs;
            //return JsonConvert.SerializeObject(pairs, Formatting.None);

        }
    }
}
