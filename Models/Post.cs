﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterPlan.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Post_id { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public string Place { get; set; }
        public string Action_id { get; set; }
        public string Icon_id { get; set; }
        public string Object_id { get; set; }
    }
}