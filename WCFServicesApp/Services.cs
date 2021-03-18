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
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
            }
        }

        private void getScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkUserInfoFill())
                {
                    SetCredentials(textBankId.Text, txtUserName.Text, txtPassword.Text);
                    var screen = client.getScreen(textBankId.Text);
                    if (screen == null || screen.id == 0)
                    {
                        gv_Screen.DataSource = null;
                        MessageBox.Show("Item not found");
                        return;
                    }
                    List<BusinessObjects.Models.Screen> lstScreens = new List<BusinessObjects.Models.Screen>();
                    lstScreens.Add(screen);
                    gv_Screen.DataSource = lstScreens;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Access is denied.")
                {
                    ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                }
                MessageBox.Show(ex.Message);
            }
        }

        private void getButtons_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkUserInfoFill())
                {
                    int branchId;
                    int screenId;
                    if (string.IsNullOrEmpty(textBranchId.Text) || !int.TryParse(textBranchId.Text, out branchId))
                    {
                        MessageBox.Show("Please fill branch id with a numeric number");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtScreenId.Text) || !int.TryParse(txtScreenId.Text, out screenId))
                    {
                        MessageBox.Show("Please fill screen id with a numeric number");
                        return;
                    }
                    SetCredentials(textBankId.Text, txtUserName.Text, txtPassword.Text);
                    var buttons = client.getButtons(textBankId.Text, branchId.ToString(), screenId.ToString());
                    List<BusinessObjects.Models.CustomButton> lstButtons = new List<BusinessObjects.Models.CustomButton>();
                    if (buttons == null || (buttons.issueTicketButtons == null && buttons.showMessageButtons == null))
                    {
                        MessageBox.Show("Item/s not found");
                        return;
                    }
                    if (buttons.showMessageButtons != null)
                    {
                        foreach (var item in buttons.showMessageButtons)
                        {
                            lstButtons.Add(new BusinessObjects.Models.CustomButton(item.id, item.enName, item.arName, item.screenId, item.type));
                        }
                    }
                    if (buttons.issueTicketButtons != null)
                    {
                        foreach (var item in buttons.issueTicketButtons)
                        {
                            lstButtons.Add(new BusinessObjects.Models.CustomButton(item.id, item.enName, item.arName, item.screenId, item.type));
                        }
                    }
                    gv_Button.DataSource = lstButtons;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "Access is denied.")
                {
                    ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                }
                MessageBox.Show(ex.Message);
            }
        }

        private void setHeader(string name, string value)
        {
            var webUser = new MessageHeader<string>(value);
            var webUserHeader = webUser.GetUntypedHeader(name, "ns");
            OperationContext.Current.OutgoingMessageHeaders.Add(webUserHeader);
        }

        public void SetCredentials(string bankId, string userName, string password)
        {
            client = new WCFServices.WCFServicesClient();
            client.ClientCredentials.UserName.UserName = userName;
            client.ClientCredentials.UserName.Password = password;
            var scope = new OperationContextScope(client.InnerChannel);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            setHeader("username", userName);
            setHeader("password", password);
            setHeader("bankid", bankId);
        }

        private bool checkUserInfoFill()
        {
            int bankId;
            if (string.IsNullOrEmpty(textBankId.Text) || !int.TryParse(textBankId.Text, out bankId))
            {
                MessageBox.Show("Please fill bank id with a numeric number");
                return false;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Please fill username");
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please fill password");
                return false;
            }
            return true;
        }
    }
}
