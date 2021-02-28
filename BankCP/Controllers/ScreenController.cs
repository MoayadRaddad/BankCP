using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using BusinessObjects.Models;
using BusinessAccessLayer.BALScreen;
using BankConfigurationPortal.Models;
using System.Security.Claims;
using System.Threading;

namespace BankCP.Controllers
{
    [RoutePrefix("api/Screen")]
    [Authorize]
    public class ScreenController : ApiController
    {
        public IHttpActionResult get()
        {
            try
            {
                BALScreen bALScreen = new BALScreen();
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                int bankId = Convert.ToInt32(identity.Claims.Where(c => c.Type == "BankId").Select(c => c.Value).SingleOrDefault());
                Screen screen = bALScreen.selectActiveScreenByBankId(bankId);
                if (screen == null)
                {
                    return Content(HttpStatusCode.InternalServerError, "Database Error");
                }
                if (screen.id == 0)
                {
                    return Content(HttpStatusCode.NotFound, "Item not found");
                }
                return Ok(screen);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return InternalServerError(ex);
            }
        }
    }
}
