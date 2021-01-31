using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class AllocateCounterService
    {
        public int id { get; set; }
        public int counterId { get; set; }
        public int serviceId { get; set; }
        public string serviceEnName { get; set; }
        public string serviceArName { get; set; }
        public bool isDeleted { get; set; }
    }
}
