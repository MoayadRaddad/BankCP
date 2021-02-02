﻿using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BankCP.Controllers
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
                if (Session["UserObj"] != null)
                {
                    return RedirectToAction("BranchesHome", "Branches", new { bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId });
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
                    if (pUser != null && pUser.id != 0)
                    {
                        Session["UserObj"] = pUser;
                        FormsAuthentication.SetAuthCookie(pUser.userName, false);
                        return RedirectToAction("BranchesHome", "Branches", new { bankId = pUser.bankId });
                    }
                    else
                    {
                        ViewBag.loginMsg = "<script>alert('" + GlobalResource.Resources.LangText.loginMessage + "');</script>";
                        return View();
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
        [HttpGet]
        public ActionResult logout()
        {
            try
            {
                Session.Clear();
                FormsAuthentication.SignOut();
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