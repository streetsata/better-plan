using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Entities.Models
{
    public class Post
    {
        /// <summary>
        /// ID поста в БД
        /// </summary>
        [Key]
        public long PostId { get; set; }

        /// <summary>
        /// Id поста в facebook
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id поста в facebook
        /// </summary>
        public string FacebookPostId { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// ID места приклепленному к посту
        /// </summary>
        public string Place { get; set; }

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
        /// Удален ли пост
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// List картинок
        /// </summary>
        public String ImagesListJSON { get; set; }

        public Post(string userId, PostViewModel postViewModel)
        {
            this.Text = postViewModel.post_text;
            this.Place = postViewModel.place;
            this.UserId = userId;
        }
    }
}
