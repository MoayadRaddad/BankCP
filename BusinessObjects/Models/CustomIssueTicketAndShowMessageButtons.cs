using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CustomIssueTicketAndShowMessageButtons
    {
        public List<CustomIssueTicketButton> issueTicketButtons { get; set; }
        public List<CustomShowMessageButton> showMessageButtons { get; set; }
    }
}
