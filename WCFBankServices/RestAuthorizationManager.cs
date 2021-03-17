using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WCFBankServices
{
    public class RestAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string userName;
            string password;
            string bankName;
            string client = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            if (string.IsNullOrEmpty(client))
            {
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if ((authHeader != null) && (authHeader != string.Empty))
                {
                    var svcCredentials = System.Text.ASCIIEncoding.ASCII
                        .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                        .Split(':');
                    userName = svcCredentials[0];
                    password = svcCredentials[1];
                    bankName = WebOperationContext.Current.IncomingRequest.Headers["bankname"];
                }
                else
                { 
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"MyWCFService\"");
                    throw new WebFaultException(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                BusinessAccessLayer.BALBank.BALBank bALBank = new BusinessAccessLayer.BALBank.BALBank();
                userName = getHeader("username");
                password = getHeader("password");
                bankName = bALBank.getBankById(Convert.ToInt32(getHeader("bankid"))).name;
            }
            BusinessAccessLayer.BALLogin.BALLogin bALLogin = new BusinessAccessLayer.BALLogin.BALLogin();
            BusinessObjects.Models.User userInformation = bALLogin.UserCheck(userName, password, bankName);
            if (userInformation == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private string getHeader(string name)
        {
            return OperationContext.Current.IncomingMessageHeaders.FindHeader(name, "ns") > -1 ? OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(name, "ns") : "";
        }
    }

}
