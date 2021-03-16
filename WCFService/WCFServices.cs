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
using System.Text;
using System.Threading;

namespace WCFService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WCFServices : IWCFServices
    {
        public Screen getScreen(string bankId)
        {
            try
            {
                BALScreen bALScreen = new BALScreen();
                Screen screen = bALScreen.selectActiveScreenByBankId(Convert.ToInt32(bankId));
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
                BALButton bALButton = new BALButton();
                CustomIssueTicketAndShowMessageButtons lstButtons = bALButton.selectIssueTicketAndShowMessageButtons(Convert.ToInt32(bankId), Convert.ToInt32(branchId), Convert.ToInt32(screenId));
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

        public List<Screen> GetScreens()
        {
            List<Screen> players = new List<Screen>();
            players.Add(new Screen { name = "Peyton"});
            players.Add(new Screen { name = "Drew"});
            return players;
        }
    }
}