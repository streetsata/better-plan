using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    /// <summary>
    /// Модель для удаления поста
    /// </summary>
    public class DeletePostViewModel
    {
        /// <summary>
        /// ID поста на Facebook
        /// </summary>
        [Required]
        public string FacebookPostId { get; set; }
    }
}
