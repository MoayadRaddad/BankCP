using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessObjects.Models
{
    public class Service
    {
        public int id { get; set; }
        [Required(ErrorMessageResourceName = "errorEnName", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "enName", ResourceType = typeof(GlobalResource.Resources.LangText))]
        [MaxLength(100, ErrorMessageResourceName = "errorMax", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        public string enName { get; set; }
        [Required(ErrorMessageResourceName = "errorArName", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "arName", ResourceType = typeof(GlobalResource.Resources.LangText))]
        [MaxLength(100 , ErrorMessageResourceName = "errorMax", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        public string arName { get; set; }
        public bool active { get; set; }
        [Required(ErrorMessageResourceName = "errorTickets", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "ticket", ResourceType = typeof(GlobalResource.Resources.LangText))]
        [Range(1, 100, ErrorMessageResourceName = "errorRange", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        public int maxNumOfTickets { get; set; }
        [Required(ErrorMessageResourceName = "errorMinTime", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "MinimumServiceTime", ResourceType = typeof(GlobalResource.Resources.LangText))]
        [Range(30, 999999, ErrorMessageResourceName = "errorRangeMinTime", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [LessThan("maximumServiceTime", ErrorMessageResourceName = "LessThanMax", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        public int minimumServiceTime { get; set; }
        [Required(ErrorMessageResourceName = "errorMaxTime", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [Display(Name = "MaximumServiceTime", ResourceType = typeof(GlobalResource.Resources.LangText))]
        [Range(30, 999999, ErrorMessageResourceName = "errorRangeMaxTime", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        [GreaterThan("minimumServiceTime", ErrorMessageResourceName = "GreatThanMin", ErrorMessageResourceType = typeof(GlobalResource.Resources.LangText))]
        public int maximumServiceTime { get; set; }
        public int bankId { get; set; }
        public bool isDeleted { get; set; }
    }
}
