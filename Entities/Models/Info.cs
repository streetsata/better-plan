using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Entities.Models
{
    // class для расборки json 
    static class Info
    {
        public class Image
        {
            public int height { get; set; }
            public string src { get; set; }
            public int width { get; set; }
        }

        public class Media
        {
            public Image image { get; set; }
        }

        public class Target
        {
            public string id { get; set; }
            public string url { get; set; }
        }

        public class Datum2
        {
            public Media media { get; set; }
            public Target target { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }

        public class Subattachments
        {
            public List<Datum2> data { get; set; }
        }

        public class Datum
        {
            public Subattachments subattachments { get; set; }
        }

        public class Attachments
        {
            public List<Datum> data { get; set; }
        }

        public class RootObject
        {
            public Attachments attachments { get; set; }
            public string id { get; set; }
            public string full_picture { get; set; }
            public string message { get; set; }
        }


        public static Dictionary<string, string> GetJSONImage(JToken post)
        {
            List<String> list = new List<string>();


            var tmp = JsonConvert.DeserializeObject<RootObject>(post.ToString());
            if (tmp.attachments != null)
            {
                for (int i = 0; i < tmp.attachments.data.Count; i++)
                {
                    if (tmp.attachments.data[i].subattachments != null)
                    {
                        for (int j = 0; j < tmp.attachments.data[i].subattachments.data.Count; j++)
                        {
                            list.Add(tmp.attachments.data[i].subattachments.data[j].media.image.src);
                        }
                    }

                }
            }

            string ImagesListJSON = JsonConvert.SerializeObject(list);


            Dictionary<string, string> pairs = new Dictionary<string, string>();

            pairs.Add("post_id", tmp.id);
            if (tmp.message != null)
            {
                pairs.Add("text", tmp.message);
            }

            if (tmp.full_picture != null)
            {
                pairs.Add("img", tmp.full_picture);
            }

            if (post["place"] != null)
            {
                pairs.Add("place", post["place"]["id"].ToString());
            }

            pairs.Add("srcJSON", ImagesListJSON);


            return pairs;

        }
    }
}