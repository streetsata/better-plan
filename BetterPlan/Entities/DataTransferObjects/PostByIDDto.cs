using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class PostByIDDto
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
        /// Время изменения поста в БД
        /// </summary>
        public DateTime? SaveUpdateDateTime { get; set; }

        /// <summary>
        /// Время создания поста на Facebook
        /// </summary>

        public DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// Время обновления(изменения) поста на Facebook
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// Время удаления поста на Facebook
        /// </summary>
        public DateTime? DeleteDateTime { get; set; }

        /// <summary>
        /// Время отложенного постинга
        /// </summary>
        public DateTime? WhenCreateDateTime { get; set; }

        /// <summary>
        /// Публиковать ли пост
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public bool IsPosting { get; set; }

        /// <summary>
        /// Признак отложенного поста
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public bool IsWaiting { get; set; }

        /// <summary>
        /// Статус публикации
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public Status Status { get; set; }
    }
}
