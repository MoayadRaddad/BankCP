using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace BankCP.Controllers
{
    [AllowAnonymous]
    public class LangController : Controller
    {
        /// <summary>
        /// change language for the site by changing culture.
        /// </summary>
        public ActionResult Change(string lang)
        {
            try
            {
                if (lang != null)
                {

                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

                }
                HttpCookie cookie = new HttpCookie("Language");
                cookie.Value = lang;
                Response.Cookies.Add(cookie);

                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
    }
}