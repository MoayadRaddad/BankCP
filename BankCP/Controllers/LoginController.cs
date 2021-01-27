using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankCP.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(BusinessObjects.Models.User pUser)
        {
            if(ModelState.IsValid)
            {
                BusinessAccessLayer.BALLogin.BALLogin bALLogin = new BusinessAccessLayer.BALLogin.BALLogin();
                pUser = bALLogin.userLogin(pUser);
                if (pUser != null && pUser.id != 0)
                {
                    Session["UserObj"] = pUser;
                    return RedirectToAction("BranchesHome", "Branches");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Bank name, username or password is not correct');</script>";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}