using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Entities.ViewModels
{
    /// <summary>
    /// Status поста
    ///     0 - Save,
    ///     1 - Published,
    ///     2 - Waiting,
    ///     3 - Error
    /// </summary>
    public enum Status
    {
        Save,
        Published,
        Waiting,
        Error
    }

    /// <summary>
    /// Модель для публикации поста
    /// </summary>
    public class PostViewModel
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
        /// ID места приклепленному к посту
        /// </summary>
        public String place { get; set; }

        ///// <summary>
        ///// действие к посту
        ///// </summary>
        //public String action { get; set; }

        ///// <summary>
        ///// действие к посту2
        ///// </summary>
        //public String objectAction { get; set; }

        ///// <summary>
        ///// иконка к посту 
        ///// </summary>
        //public String icon { get; set; }

        /// <summary>
        /// List картинок
        /// </summary>
        public List<IFormFile> ImagesListIFormFile { get; set; }

        /// <summary>
        /// List картинок(array bte)
        /// </summary>
        public List<Object> ImagesURLList { get; set; }


        /// <summary>
        /// Публиковать пост
        /// </summary>
        public Boolean isPosting { get; set; }

        /// <summary>
        /// Публикуется отложенный пост
        /// </summary>
        public Boolean isWaiting { get; set; }

        /// <summary>
        /// Время создания поста
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// Время обновления(изменения) поста
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// Время Удаления поста
        /// </summary>
        public DateTime? DeleteDateTime { get; set; }

        /// <summary>
        /// Время отложенного постинга
        /// </summary>
        public DateTime? WhenCreateDateTime { get; set; }


        /// <summary>
        /// Статус публикации
        /// </summary>
        public Status status { get; set; }

        public PostViewModel()
        {

        }

        public PostViewModel(Post post, Boolean isPosing)
        {
            this.post_text = post.Text;
            this.place = post.Place;
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
            this.CreateDateTime = post.CreateDateTime;
            this.UpdateDateTime = post.UpdateDateTime;
            this.DeleteDateTime = post.DeleteDateTime;
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
