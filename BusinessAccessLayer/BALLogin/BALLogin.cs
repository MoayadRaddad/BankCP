using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.BALLogin
{
    public class BALLogin
    {
        public BusinessObjects.Models.User userLogin(BusinessObjects.Models.User pUser)
        {
            try
            {
                DataAccessLayer.DALLogin.DALLogin dALLogin = new DataAccessLayer.DALLogin.DALLogin();
                return dALLogin.userLogin(pUser);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
    }
}
