using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("projects")]
    public class Project
    {
        public Guid ProjectId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public DateTime DateTimeCreation { get; set; }

        /// <summary>
        /// Описание проекта
        /// </summary>
        private string Descreption { get; set; }

        /// <summary>
        /// Функционал проекта (Facebook включен по умолчанию)
        /// </summary>
        private bool IsFacebook { get; set; } = true;

        // Nav
        public ICollection<FacebookUser> FacebookUsers { get; set; }
    }
}
