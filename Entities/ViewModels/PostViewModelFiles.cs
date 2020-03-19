using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;

namespace Entities.ViewModels
{
    public class PostViewModelFiles
    {
        /// <summary>
        /// ID поста в БД
        /// </summary>
        public Int32? PostId { get; set; }

        /// <summary>
        /// Текст публикуемого поста
        /// </summary>
        public String post_text { get; set; }

        /// <summary>
        /// List картинок
        /// </summary>
        public List<IFormFile> ImagesListIFormFile { get; set; }

        /// <summary>
        /// List картинок(array bte)
        /// </summary>
        [DefaultValue(null)]
        public List<Object> ImagesURLList { get; set; }


        /// <summary>
        /// Публиковать пост
        /// </summary>
        [DefaultValue(true)]
        public Boolean isPosting { get; set; }

        /// <summary>
        /// Публикуется отложенный пост
        /// </summary>
        //[DefaultValue(false)]
        public Boolean isWaiting { get; set; } = false;

        /// <summary>
        /// Время отложенного постинга
        /// </summary>
        public DateTime? WhenCreateDateTime { get; set; }


        public PostViewModelFiles()
        {

        }

        public PostViewModelFiles(Post post, Boolean isPosing)
        {
            this.post_text = post.Text;
            if (post.ImagesListJSON != null && isPosing)
            {
                this.ImagesListIFormFile = GetIFormFileImages(post.ImagesListJSON);
            }
            else if (post.ImagesListJSON != null && !isPosing)
            {
                this.ImagesURLList = JsonConvert.DeserializeObject<List<Object>>(post.ImagesListJSON);
            }

            this.isPosting = post.isPosting;
            this.isWaiting = post.isWaiting;
            this.WhenCreateDateTime = post.WhenCreateDateTime;
        }

        private List<IFormFile> GetIFormFileImages(string imagesListJSON)
        {
            var list = JsonConvert.DeserializeObject<List<String>>(imagesListJSON);
            List<IFormFile> formFiles = new List<IFormFile>();
            foreach (var url in list)
            {
                using (var stream = File.OpenRead(url))
                {
                    formFiles.Add(new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name)));
                }
            }

            return formFiles;
        }
    }
}
