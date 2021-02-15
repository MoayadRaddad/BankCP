using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankConfigurationPortal.Models
{
    public class CustomerServiceModel
    {
        public List<BusinessObjects.Models.Service> Services { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }
    }
}