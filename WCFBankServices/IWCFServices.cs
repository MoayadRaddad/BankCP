using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace WCFBankServices
{
    [ServiceContract]
    public interface IWCFServices
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getScreen/{bankId}")]
        Screen getScreen(string bankId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "getButtons/{bankId}/{branchId}/{screenId}")]
        CustomIssueTicketAndShowMessageButtons getButtons(string bankId, string branchId, string screenId);
    }
}
