using BusinessAccessLayer.BALBank;
using BusinessAccessLayer.BALButton;
using BusinessAccessLayer.BALScreen;
using BusinessCommon.ExceptionsWriter;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace WCFBankServices
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WCFServices : IWCFServices
    {
        public Screen getScreen(string bankId)
        {
            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                var svcCredentials = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(authHeader.Substring(6))).Split(':');
                string bankName = svcCredentials[2];
                BALScreen bALScreen = new BALScreen();
                Screen screen = bALScreen.selectActiveScreenByBankName(bankName);
                if (screen == null)
                {
                    ExceptionsWriter.saveEventsAndExceptions(new FaultException("Database Error"), "Exceptions not handled", EventLogEntryType.Error);
                }
                return screen;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }

        public CustomIssueTicketAndShowMessageButtons getButtons(string bankId, string branchId, string screenId)
        {
            try
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                var svcCredentials = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(authHeader.Substring(6))).Split(':');
                string bankName = svcCredentials[2];
                BALButton bALButton = new BALButton();
                CustomIssueTicketAndShowMessageButtons lstButtons = bALButton.selectIssueTicketAndShowMessageButtonsByBankName(bankName, Convert.ToInt32(branchId), Convert.ToInt32(screenId));
                if (lstButtons == null)
                {
                    ExceptionsWriter.saveEventsAndExceptions(new FaultException("Database Error"), "Exceptions not handled", EventLogEntryType.Error);
                }
                return lstButtons;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
    }
}
