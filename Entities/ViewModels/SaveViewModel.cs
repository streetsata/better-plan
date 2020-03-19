using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace Entities.ViewModels
{
    public class SaveViewModel
    {
        /// <summary>
        /// ID поста в БД
        /// </summary>
        public Int32? PostId { get; set; }

        /// <summary>
        /// Текст публикуемого поста
        /// </summary>
        public String post_text { get; set; }


    }
}
