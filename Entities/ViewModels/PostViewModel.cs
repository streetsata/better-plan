using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        /// <summary>
        /// List картинок
        /// </summary>
        public List<IFormFile> ImagesListIFormFile { get; set; }

        /// <summary>
        /// List картинок(array bte)
        /// </summary>
        public List<Object> ImagesURLList { get; set; }

        public PostViewModel()
        {

        }

        public PostViewModel(Post post, Boolean isPosing)
        {
            this.PostId = post.PostId;
            this.post_text = post.Text;
            this.isPosting = post.isPosting;
            this.isWaiting = post.isWaiting;
            this.WhenCreateDateTime = post.WhenCreateDateTime;
        }
    }
}
