using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WCFServicesApp
{
    public partial class Services : Form
    {
        WCFServices.WCFServicesClient client;
        public Services()
        {
            try
            {
                InitializeComponent();
                //client = new WCFServices.WCFServicesClient();
                client = new WCFServices.WCFServicesClient();
                client.ClientCredentials.UserName.UserName = "admin";
                client.ClientCredentials.UserName.Password = "admin";
                var scope = new OperationContextScope(client.InnerChannel);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                var webUser = new MessageHeader<string>("admin");
                var webUserHeader = webUser.GetUntypedHeader("userName", "ns");
                OperationContext.Current.OutgoingMessageHeaders.Add(webUserHeader);
                setHeader("username", "admin");
                setHeader("password", "admin");
                setHeader("bankname", "bank1");
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }

        private void setHeader(string name, string value)
        {
            var webUser = new MessageHeader<string>(value);
            var webUserHeader = webUser.GetUntypedHeader(name, "ns");
            OperationContext.Current.OutgoingMessageHeaders.Add(webUserHeader);
        }

        private void getScreen_Click(object sender, EventArgs e)
        {
            try
            {
                var x = client.ClientCredentials.UserName;
                int bankId;
                if (string.IsNullOrEmpty(txtBankId_Screen.Text) || !int.TryParse(txtBankId_Screen.Text, out bankId))
                {
                    MessageBox.Show("Please fill bank id with a numeric number");
                    return;
                }
                var screen = client.getScreen(bankId.ToString());
                List<BusinessObjects.Models.Screen> lstScreens = new List<BusinessObjects.Models.Screen>();
                lstScreens.Add(screen);
                if (screen == null || screen.id == 0)
                {
                    gv_Screen.DataSource = null;
                    MessageBox.Show("Item not found");
                    return;
                }
                gv_Screen.DataSource = lstScreens;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }

        private void getButtons_Click(object sender, EventArgs e)
        {
            try
            {
                int bankId;
                int branchId;
                int screenId;
                if (string.IsNullOrEmpty(txtBankId.Text) || !int.TryParse(txtBankId.Text, out bankId))
                {
                    MessageBox.Show("Please fill bank id with a numeric number");
                    return;
                }
                if (string.IsNullOrEmpty(txtBranchId.Text) || !int.TryParse(txtBranchId.Text, out branchId))
                {
                    MessageBox.Show("Please fill branch id with a numeric number");
                    return;
                }
                if (string.IsNullOrEmpty(txtScreenId.Text) || !int.TryParse(txtScreenId.Text, out screenId))
                {
                    MessageBox.Show("Please fill screen id with a numeric number");
                    return;
                }
                var buttons = client.getButtons(bankId.ToString(), branchId.ToString(), screenId.ToString());
                List<BusinessObjects.Models.CustomButton> lstButtons = new List<BusinessObjects.Models.CustomButton>();
                if (buttons == null)
                {
                    MessageBox.Show("Item/s not found");
                    return;
                }
                foreach (var item in buttons.showMessageButtons)
                {
                    lstButtons.Add(new BusinessObjects.Models.CustomButton(item.id, item.enName, item.arName, item.screenId, item.type));
                }
                foreach (var item in buttons.issueTicketButtons)
                {
                    lstButtons.Add(new BusinessObjects.Models.CustomButton(item.id, item.enName, item.arName, item.screenId, item.type));
                }
                gv_Button.DataSource = lstButtons;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }
    }
}
