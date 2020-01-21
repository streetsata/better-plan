using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    /// <summary>
    /// Модель для публикации поста
    /// </summary>
    public class PostViewModel
    {
        /// <summary>
        /// Текст публикуемого поста
        /// </summary>
        [Required]
        public string post_text { get; set; }
        /// <summary>
        /// ID места приклепленному к посту
        /// </summary>
        public string place { get; set; }
    }
}
