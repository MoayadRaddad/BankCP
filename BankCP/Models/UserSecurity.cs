using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BankConfigurationPortal.Models
{
    public class UserSecurity
    {
        public static BusinessObjects.Models.User Login(string userName, string password)
        {
            try
            {
                BusinessAccessLayer.BALLogin.BALLogin bALLogin = new BusinessAccessLayer.BALLogin.BALLogin();
                return bALLogin.UserCheck(userName, password);
            }
            catch(Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
    }
}