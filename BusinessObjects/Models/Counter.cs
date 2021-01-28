using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Counter
    {
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string enName { get; set; }
        [Required]
        [MaxLength(100)]
        public string arName { get; set; }
        public bool active { get; set; }
        [Required]
        public string type { get; set; }
        public int branchId { get; set; }
    }
}
