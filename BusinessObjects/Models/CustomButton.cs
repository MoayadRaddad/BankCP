using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CustomButton
    {
        public int id { get; set; }
        public string enName { get; set; }
        public string arName { get; set; }
        public int screenId { get; set; }
        public string type { get; set; }
        public CustomButton() { }
        public CustomButton(int pid, string penName, string parName, int pscreenId, string ptype)
        {
            id = pid;
            enName = penName;
            arName = parName;
            screenId = pscreenId;
            type = ptype;
        }
    }
}
