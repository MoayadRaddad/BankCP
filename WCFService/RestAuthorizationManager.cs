using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
    public class RestAuthorizationManager : ServiceAuthorizationManager
    {
        /// <summary>  
        /// Method source sample taken from here: http://bit.ly/1hUa1LR  
        /// </summary>  
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string userName;
            string password;
            string bankName;
            string client = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
            if (string.IsNullOrEmpty(client))
            {
                //Extract the Authorization header, and parse out the credentials converting the Base64 string:  
                var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if ((authHeader != null) && (authHeader != string.Empty))
                {
                    var svcCredentials = System.Text.ASCIIEncoding.ASCII
                        .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                        .Split(':');
                    userName = svcCredentials[0];
                    password = svcCredentials[1];
                    bankName = "bank1";
                }
                else
                {
                    //No authorization header was provided, so challenge the client to provide before proceeding:  
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"MyWCFService\"");
                    //Throw an exception with the associated HTTP status code equivalent to HTTP status 401  
                    throw new WebFaultException(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                userName = getHeader("username");
                password = getHeader("password");
                bankName = getHeader("bankname");
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
