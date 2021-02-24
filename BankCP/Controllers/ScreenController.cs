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

namespace BankCP.Controllers
{
    [RoutePrefix("api/Screen")]
    [BasicAuthentication]
    public class ScreenController : ApiController
    {
        [Route("{bankId}")]
        public IHttpActionResult getActiveScreenByBankId(int bankId)
        {
            try
            {
                BALScreen bALScreen = new BALScreen();
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
