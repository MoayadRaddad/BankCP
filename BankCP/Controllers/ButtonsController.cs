using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using BusinessObjects.Models;
using BusinessAccessLayer.BALButton;
using BankConfigurationPortal.Models;
using System.Threading;
using System.Security.Claims;
using System.Web;
using Newtonsoft.Json;

namespace BankCP.Controllers
{
    [RoutePrefix("api/Buttons")]
    [Authorize]
    public class ButtonsController : ApiController
    {
        [Route("{branchId}/{screenId}")]
        public IHttpActionResult get(int branchId, int screenId)
        {
            try
            {
                BALButton bALButton = new BALButton();
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                int bankId = Convert.ToInt32(identity.Claims.Where(c => c.Type == "BankId").Select(c => c.Value).SingleOrDefault());
                CustomIssueTicketAndShowMessageButtons lstButtons = bALButton.selectIssueTicketAndShowMessageButtons(bankId, branchId, screenId);
                if (lstButtons == null)
                {
                    return Content(HttpStatusCode.InternalServerError, "Database Error");
                }
                if (lstButtons.issueTicketButtons.Count == 0 && lstButtons.showMessageButtons.Count == 0)
                {
                    return Content(HttpStatusCode.NotFound, "Items not found");
                }
                return Ok(lstButtons);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return InternalServerError(ex);
            }
        }
    }
}