using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class User
    {
        public int id { get; set; }
        [Required(ErrorMessageResourceName = "errorUserName", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "userName", ResourceType = typeof(GlobalResource.Resources.LangText))]
        public string userName { get; set; }
        [Required(ErrorMessageResourceName = "errorPassword", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "password", ResourceType = typeof(GlobalResource.Resources.LangText))]
        public string password { get; set; }
        [Required(ErrorMessageResourceName = "errorBankName", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "bankName", ResourceType = typeof(GlobalResource.Resources.LangText))]
        public string bankName { get; set; }
        public int bankId { get; set; }
    }
}
