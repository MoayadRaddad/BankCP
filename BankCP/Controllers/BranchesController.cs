using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;
using BankCP.Models;

namespace BankConfigurationPortal.Controllers
{
    [Authorize]
    [SessionAuthorize]
    public class BranchesController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get branches for current user bank from database and return brancheshome view
        /// </summary>
        [HttpGet]
        public ActionResult Home()
        {
            try
            {
                if (TempData["errorMsg"] != null)
                {
                    ViewBag.errorMsg = TempData["errorMsg"];
                    TempData["errorMsg"] = null;
                }
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                List<BusinessObjects.Models.Branch> lstBranches = bALBranches.selectBranchesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstBranches == null)
                {
                    ViewBag.errorMsg = LangText.checkConnection;
                    return View();
                }
                else if (lstBranches.Count == 0)
                {
                    return View();
                }
                else if (lstBranches.FirstOrDefault().id == 0)
                {
                    ViewBag.errorMsg = LangText.somethingWentWrongAlert;
                    return View();
                }
                else
                {
                    return View(lstBranches);
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
        public ActionResult Add()
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
        public ActionResult Add(BusinessObjects.Models.Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    branch.bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId;
                    BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                    BusinessObjects.Models.ResultsEnum checkInserted = bALBranches.insertBranch(branch);
                    if (checkInserted == BusinessObjects.Models.ResultsEnum.notInserted)
                    {
                        ViewBag.errorMsg = LangText.checkConnection;
                        return View();
                    }
                    else if (checkInserted == BusinessObjects.Models.ResultsEnum.inserted)
                    {
                        return RedirectToAction("Home", "Branches");
                    }
                    else
                    {
                        ViewBag.errorMsg = LangText.somethingWentWrongAlert;
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
        /// Delete branch from database
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int branchId)
        {
            try
            {
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                BusinessObjects.Models.ResultsEnum checkDeleted = bALBranches.deleteBranchById(branchId, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (checkDeleted == BusinessObjects.Models.ResultsEnum.deleted)
                {
                    return RedirectToAction("Home");
                }
                else
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home");
                }
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
        public ActionResult Edit(int branchId)
        {
            try
            {
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                BusinessObjects.Models.Branch branch = bALBranches.selectBranchById(branchId, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (branch == null)
                {
                    ViewBag.errorMsg = LangText.checkConnection;
                    return View();
                }
                else if (branch.id == 0)
                {
                    ViewBag.errorMsg = LangText.somethingWentWrongAlert;
                    return View();
                }
                else
                {
                    return View(branch);
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
        public ActionResult Edit(BusinessObjects.Models.Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    branch.bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId;
                    BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                    BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                    BusinessObjects.Models.ResultsEnum checkUpdated = bALBranches.updateBranch(branch);
                    if (checkUpdated == BusinessObjects.Models.ResultsEnum.notUpdated)
                    {
                        ViewBag.errorMsg = LangText.checkConnection;
                        return View();
                    }
                    else if (checkUpdated == BusinessObjects.Models.ResultsEnum.updated)
                    {
                        return RedirectToAction("Home", "Branches");
                    }
                    else
                    {
                        ViewBag.errorMsg = LangText.somethingWentWrongAlert;
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
        #endregion
    }
}