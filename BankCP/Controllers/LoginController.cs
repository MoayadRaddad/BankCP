using BankConfigurationPortal.Models;
using BusinessCommon.ExceptionsWriter;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BankConfigurationPortal.Controllers
{
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
                if (TempData["errorMsg"] != null)
                {
                    ViewBag.errorMsg = TempData["errorMsg"];
                    TempData["errorMsg"] = null;
                    Session.Clear();
                }
                if (Session["UserObj"] != null)
                {
                    return RedirectToAction("Home", "Branches");
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
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
                            Session["UserObj"] = pUser;
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
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Clear session and signout from form authentication
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult logout()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Session.Clear();
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignOut("ApplicationCookie");
                }
                return RedirectToAction("login");
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        #endregion

        #region Methods
        private bool owinCookieAuthorization(BusinessObjects.Models.User user)
        {
            try
            {
                var claims = new[] { new Claim(ClaimTypes.Name, user.userName) };
                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                identity.AddClaim(new Claim(ClaimTypes.Role, "admins"));
                var context = Request.GetOwinContext();
                var authManager = context.Authentication;
                authManager.SignIn(new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                }, identity);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return false;
            }
        }
        #endregion
    }
}