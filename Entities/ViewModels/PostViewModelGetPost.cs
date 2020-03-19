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
    /// Модель для публикации поста
    /// </summary>
    public class PostViewModelGetPost
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
        /// Id поста в facebook
        /// </summary>
        public string FacebookPostId { get; set; }

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
        /// Время сохранения поста
        /// </summary>
        public DateTime? SaveCreateDateTime { get; set; }

        /// <summary>
        /// Время изменения поста
        /// </summary>
        public DateTime? SaveUpdateDateTime { get; set; }

        /// <summary>
        /// Время Удаления поста
        /// </summary>
        public DateTime? SaveDeleteDateTime { get; set; }

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

        public PostViewModelGetPost()
        {

        }

        public PostViewModelGetPost(Post post, Boolean isPosing)
        {
            this.PostId = post.PostId;
            this.FacebookPostId = post.FacebookPostId;
            this.post_text = post.Text;

            if (post.ImagesListJSON != null && !isPosing)
            {
                this.ImagesURLList = JsonConvert.DeserializeObject<List<Object>>(post.ImagesListJSON);
            }

            this.isPosting = post.isPosting;
            this.isWaiting = post.isWaiting;
            this.CreateDateTime = post.CreateDateTime;
            this.UpdateDateTime = post.UpdateDateTime;
            this.DeleteDateTime = post.DeleteDateTime;
            this.SaveCreateDateTime = post.SaveCreateDateTime;
            this.SaveUpdateDateTime = post.SaveUpdateDateTime;
            this.SaveDeleteDateTime = post.SaveDeleteDateTime;
            this.WhenCreateDateTime = post.WhenCreateDateTime;
            this.status = post.status;

        }

    }
}
