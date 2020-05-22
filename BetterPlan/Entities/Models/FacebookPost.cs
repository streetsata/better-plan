﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
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

    [Table("facebook_posts")]
    public class FacebookPost
    {
        /// <summary>
        /// ID поста в БД
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Id User
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public string UserId { get; set; }

        /// <summary>
        /// Id поста в facebook
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public string FacebookPostId { get; set; }

        /// <summary>
        /// Текст поста
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public string Text { get; set; }

        /// <summary>
        /// Время сохранения поста в БД
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public DateTime? SaveCreateDateTime { get; set; }

        /// <summary>
        /// Время изменения поста в БД
        /// </summary>
        public DateTime? SaveUpdateDateTime { get; set; }

        /// <summary>
        /// Время удаления поста в БД
        /// </summary>
        public DateTime? SaveDeleteDateTime { get; set; }

        /// <summary>
        /// Время создания поста на Facebook
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
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
        /// Удален ли пост в БД
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// Путь к изображению
        /// </summary>
        public string ImagesListJSON { get; set; }

        /// <summary>
        /// Время отложенного постинга
        /// </summary>
        public DateTime? WhenCreateDateTime { get; set; }

        /// <summary>
        /// Публиковать пост
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public bool isPosting { get; set; }

        /// <summary>
        /// Признак отложенного поста
        /// </summary>
        [Required(ErrorMessage = "Property is required")]
        public bool isWaiting { get; set; }

        /// <summary>
        /// Статус публикации
        /// </summary>
        public Status status { get; set; }

        // Nav
        [ForeignKey(nameof(FacebookUser))]
        public Guid FacebookUserId { get; set; }
        public FacebookUser FacebookUser { get; set; }
    }
}
