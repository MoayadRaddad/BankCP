using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankConfigurationPortal.Models
{
    public class ServiceAllocate
    {
        public int id { get; set; }
        [Required(ErrorMessageResourceName = "errorEnName", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "enName", ResourceType = typeof(GlobalResource.Resources.LangText))]
        [MaxLength(100, ErrorMessageResourceName = "errorMax", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        public string enName { get; set; }
        [Required(ErrorMessageResourceName = "errorArName", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "arName", ResourceType = typeof(GlobalResource.Resources.LangText))]
        [MaxLength(100, ErrorMessageResourceName = "errorMax", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        public string arName { get; set; }
        public int counterId { get; set; }
        public List<int> AllocateId { get; set; }
        public ServiceAllocate(){ }

        public ServiceAllocate(int pid, string penName, string parName, int pcounterId, List<int> pAllocateId)
        {
            id = pid;
            enName = penName;
            arName = parName;
            counterId = pcounterId;
            AllocateId = pAllocateId;
        }
    }
}