using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankCP.Controllers
{
    public class BranchesController : Controller
    {
        [HttpGet]
        public ActionResult BranchesHome()
        {
            try
            {
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                List<BusinessObjects.Models.Branch> lstBranches = bALBranches.selectBranchesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstBranches != null)
                {
                    return View(lstBranches);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return RedirectToAction("login", "Login");
            }
        }
        [HttpGet]
        public ActionResult AddBranch()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return RedirectToAction("login", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBranch(BusinessObjects.Models.Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                    branch.bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId;
                    branch = bALBranches.insertBranch(branch);
                    if (branch != null && branch.id != 0)
                    {
                        return RedirectToAction("BranchesHome", "Branches");
                    }
                    else
                    {
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
                return RedirectToAction("login", "Login");
            }
        }
        [HttpPost]
        public ActionResult DeleteBranch(int branchId)
        {
            try
            {
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                if (bALBranches.deleteBranchById(branchId) != 0)
                {
                    return RedirectToAction("BranchesHome", "Branches");
                }
                else
                {
                    TempData["msg"] = "<script>alert('Please check your connection');</script>";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return RedirectToAction("login", "Login");
            }
        }
        [HttpGet]
        public ActionResult EditBranch(int branchId)
        {
            try
            {
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                BusinessObjects.Models.Branch branch = bALBranches.selectBranchesById(branchId);
                if (branch != null)
                {
                    return View(branch);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return RedirectToAction("login", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBranch(BusinessObjects.Models.Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                    branch = bALBranches.updateBranch(branch);
                    return RedirectToAction("BranchesHome", "Branches");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return RedirectToAction("login", "Login");
            }
        }
    }
}