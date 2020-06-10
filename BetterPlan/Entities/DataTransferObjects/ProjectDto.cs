using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }


        /// <summary>
        /// Описание проекта
        /// </summary>
        private string Descreption { get; set; }

        /// <summary>
        /// Функционал проекта (Facebook включен по умолчанию)
        /// </summary>
        private bool IsFacebook { get; set; } = true;
    }
}
