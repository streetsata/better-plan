using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class PostsGetAllDto
    {
        /// <summary>
        /// ID поста в БД
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Id поста в facebook
        /// </summary>
        public string FacebookPostId { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Время сохранения поста в БД
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public DateTime SaveCreateDateTime { get; set; }

        /// <summary>
        /// Статус публикации
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public Status Status { get; set; }
    }
}
