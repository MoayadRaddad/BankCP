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
using System.ServiceModel.Channels;
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
                    BusinessObjects.Models.Screen screen = new BusinessObjects.Models.Screen();
                    string credentials = SetCredentials(txtUserName.Text, txtPassword.Text, txtBankName.Text);
                    using (OperationContextScope scope =
                              new OperationContextScope(client.InnerChannel))
                    {
                        HttpRequestMessageProperty request = new HttpRequestMessageProperty();
                        request.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + credentials;
                        OperationContext.Current.OutgoingMessageProperties.Add(HttpRequestMessageProperty.Name, request);
                        screen = client.getScreen(txtBankName.Text);
                    }
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
                    string credentials = SetCredentials(txtUserName.Text, txtPassword.Text, txtBankName.Text);
                    BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons buttons = new BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons();
                    using (OperationContextScope scope =
                              new OperationContextScope(client.InnerChannel))
                    {
                        HttpRequestMessageProperty request = new HttpRequestMessageProperty();
                        request.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " + credentials;
                        OperationContext.Current.OutgoingMessageProperties.Add(HttpRequestMessageProperty.Name, request);
                        buttons = client.getButtons(txtBankName.Text, branchId.ToString(), screenId.ToString());
                    }
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


        private bool checkUserInfoFill()
        {
            try
            {
                if (string.IsNullOrEmpty(txtBankName.Text))
                {
                    MessageBox.Show("Please fill bank name");
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
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return false;
            }
        }

        private string SetCredentials(string userName, string password, string bankName)
        {
            try
            {
                client = new WCFServices.WCFServicesClient();
                return EncodeBasicAuthenticationCredentials(userName, password, bankName);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }

        private string EncodeBasicAuthenticationCredentials(string username, string password, string bankName)
        {
            try
            {
                string credentials = username + ":" + password + ":" + bankName;
                var asciiCredentials = (from c in credentials
                                        select c <= 0x7f ? (byte)c : (byte)'?').ToArray();
                return Convert.ToBase64String(asciiCredentials);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
    }
}
