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

namespace BankCP.Controllers
{
    [RoutePrefix("api/Button")]
    [BasicAuthentication]
    public class ButtonController : ApiController
    {
        [Route("{branchId}/{screenId}")]
        public IHttpActionResult getButtonsForBranch(int branchId, int screenId)
        {
            try
            {
                BALButton bALButton = new BALButton();
                int bankId = Convert.ToInt32(Thread.CurrentPrincipal.Identity.Name);
                List<CustomButton> lstButtons = bALButton.selectButtonsbyBranchIdAnsScreenId(bankId, branchId, screenId);
                if (lstButtons == null)
                {
                    return Content(HttpStatusCode.InternalServerError, "Database Error");
                }
                if (lstButtons.Count == 0)
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
