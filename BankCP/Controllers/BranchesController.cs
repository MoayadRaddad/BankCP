using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;

namespace BankCP.Controllers
{
    [Authorize]
    public class BranchesController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get branches for current user bank from database and return brancheshome view
        /// </summary>
        [HttpGet]
        public ActionResult BranchesHome()
        {
            try
            {
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                List<BusinessObjects.Models.Branch> lstBranches = bALBranches.selectBranchesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstBranches != null && bALCommon.checkExist("tblBanks", ((BusinessObjects.Models.User)Session["UserObj"]).bankId))
                {
                    return View(lstBranches);
                }
                else
                {
                    ViewBag.connectionMsg = LangText.checkConnection;
                    return RedirectToAction("login", "Login");
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Return addbranch view
        /// </summary>
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
                return View("Error");
            }
        }
        /// <summary>
        /// insert branch to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBranch(BusinessObjects.Models.Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    branch.bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId;
                    BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                    branch = bALBranches.insertBranch(branch);
                    if (branch == null || branch.id == 0)
                    {
                        ViewBag.connectionMsg = LangText.checkConnection;
                    }
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
                return View("Error");
            }
        }
        /// <summary>
        /// Delete branch from database
        /// </summary>
        [HttpPost]
        public ActionResult DeleteBranch(int branchId)
        {
            try
            {
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                bALBranches.deleteBranchById(branchId);
                return RedirectToAction("BranchesHome", "Branches");
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Return editbranch view
        /// </summary>
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
                    ViewBag.connectionMsg = LangText.itemDeleted;
                    return RedirectToAction("BranchesHome", "Branches");
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// edit branch and save data to database
        /// </summary>
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
                    if (branch == null)
                    {
                        ViewBag.connectionMsg = LangText.itemDeleted;
                    }
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
                return View("Error");
            }
        }
        #endregion
    }
}