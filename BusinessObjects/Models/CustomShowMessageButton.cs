using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CustomShowMessageButton : CustomButton
    {
        public string messageAR { get; set; }
        public string messageEN { get; set; }
        public CustomShowMessageButton()
        {
            id = 0;
            enName = string.Empty;
            arName = string.Empty;
            messageAR = string.Empty;
            messageEN = string.Empty;
            screenId = 0;
            type = "ShowMessage";
        }
        public CustomShowMessageButton(int pid, string penName, string parName, string pmessageAR, string pmessageEN, int pdcreenId)
        {
            id = pid;
            enName = penName;
            arName = parName;
            messageAR = pmessageAR;
            messageEN = pmessageEN;
            screenId = pdcreenId;
            type = "ShowMessage";
        }
    }
}
