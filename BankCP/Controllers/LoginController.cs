using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
                ////sql transaction scope test
                //int s = BusinessAccessLayer.BALBank.BALBank.insertBankTestScope();
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
                        if (pUser.id != 0)
                        {
                            Session["UserObj"] = pUser;
                            FormsAuthentication.SetAuthCookie(pUser.userName, false);
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
                    FormsAuthentication.SignOut();
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
    }
}