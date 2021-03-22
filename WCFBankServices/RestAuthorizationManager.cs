using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace WCFBankServices
{
    public class RestAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string userName;
            string password;
            string bankName;
            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if ((authHeader != null) && (authHeader != string.Empty))
            {
                var svcCredentials = System.Text.ASCIIEncoding.ASCII
                    .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                    .Split(':');
                userName = svcCredentials[0];
                password = svcCredentials[1];
                bankName = svcCredentials[2];
                BusinessAccessLayer.BALLogin.BALLogin bALLogin = new BusinessAccessLayer.BALLogin.BALLogin();
                BusinessObjects.Models.User userInformation = bALLogin.UserCheck(userName, password, bankName);
                return userInformation == null ? false : true;
            }
            else
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"MyWCFService\"");
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
