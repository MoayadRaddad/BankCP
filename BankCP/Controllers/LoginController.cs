using BankConfigurationPortal.Models;
using BusinessCommon.ExceptionsWriter;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BankConfigurationPortal.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Redirect users that already loged in or return login view
        /// </summary>
        [HttpGet]
        public ActionResult login()
        {
            try
            {
                if (AuthenticationManager.User.Identity.IsAuthenticated == true)
                {
                    return RedirectToAction("Home", "Branches");
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return View("Error");
            }
        }
        /// <summary>
        /// Geting information from user and return branches action if user authorized
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(BusinessObjects.Models.User pUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALLogin.BALLogin bALLogin = new BusinessAccessLayer.BALLogin.BALLogin();
                    pUser = bALLogin.userLogin(pUser);
                    if (pUser != null)
                    {
                        if (pUser.id != 0 && owinCookieAuthorization(pUser))
                        {
                            return RedirectToAction("Home", "Branches");
                        }
                        else
                        {
                            ViewBag.loginMsg = GlobalResource.Resources.LangText.loginMessage;
                            return View();
                        }
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return View("Error");
            }
        }
        /// <summary>
        /// Clear session and signout from form authentication
        /// </summary>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult logout()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                }
                return RedirectToAction("login");
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return View("Error");
            }
        }
        #endregion

        #region Methods
        private bool owinCookieAuthorization(BusinessObjects.Models.User user)
        {
            try
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.userName));
                claims.Add(new Claim("BankId", user.bankId.ToString()));
                claims.Add(new Claim("BankName", user.bankName));
                 var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return false;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        #endregion
    }
}
