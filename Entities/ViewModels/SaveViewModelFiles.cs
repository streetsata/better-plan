using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace Entities.ViewModels
{
    public class SaveViewModelFiles
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
        /// List картинок
        /// </summary>
        public List<IFormFile> ImagesListIFormFile { get; set; }

        /// <summary>
        /// List картинок(array bte)
        /// </summary>
        public List<Object> ImagesURLList { get; set; }

    }
}
