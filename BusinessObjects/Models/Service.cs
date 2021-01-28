using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObjects.Models
{
    public class Service
    {
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        public bool active { get; set; }
        [Required]
        [Range(1, 100)]
        public int tickets { get; set; }
        public int bankId { get; set; }
        public bool isDeleted { get; set; }
    }
}
