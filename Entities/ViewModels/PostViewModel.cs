using Microsoft.AspNetCore.Http;
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
        public String post_text { get; set; }

        /// <summary>
        /// ID места приклепленному к посту
        /// </summary>
        public String place { get; set; }

        /// <summary>
        /// действие к посту
        /// </summary>
        public String action { get; set; }

        /// <summary>
        /// действие к посту2
        /// </summary>
        public String objectAction { get; set; }

        /// <summary>
        /// иконка к посту 
        /// </summary>
        public String icon { get; set; }

        /// <summary>
        /// List картинок
        /// </summary>
        public List<IFormFile> ImagesListJSON { get; set; }





    }
}
