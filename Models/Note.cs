﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectBoard.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int ATaskId { get; set; }
        public ATask Task { get; set; }
    }
}