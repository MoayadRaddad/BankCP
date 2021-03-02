using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CustomIssueTicketButton : CustomButton
    {
        public int serviceId { get; set; }
        public CustomIssueTicketButton()
        {
            id = 0;
            enName = string.Empty;
            arName = string.Empty;
            serviceId = 0;
            screenId = 0;
            type = "IssueTicket";
        }
        public CustomIssueTicketButton(int pid, string penName, string parName, int pserviceId, int pscreenId)
        {
            id = pid;
            enName = penName;
            arName = parName;
            serviceId = pserviceId;
            screenId = pscreenId;
            type = "IssueTicket";
        }
    }
}
