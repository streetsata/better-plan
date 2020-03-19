﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    /// <summary>
    /// Модель для изменения поста
    /// </summary>
    public class EditPostViewModel
    {
        /// <summary>
        /// ID поста на Facebook
        /// </summary>
        [Required]
        public string FacebookPostId { get; set; }
        /// <summary>
        /// Изменяемый текст в посте
        /// </summary>
        [Required]
        public string edit_text { get; set; }

    }
}