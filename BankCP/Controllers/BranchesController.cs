using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;
using BankConfigurationPortal.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace BankConfigurationPortal.Controllers
{
    [Authorize]
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
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                List<BusinessObjects.Models.Branch> lstBranches = bALBranches.selectBranchesByBankId(bankId);
                if (lstBranches == null)
                {
                    TempData["errorMsg"] = LangText.checkConnection;
                    return RedirectToAction("login", "Login");
                }
                else if (lstBranches.Count == 0)
                {
                    return View();
                }
                else if (lstBranches.FirstOrDefault().id == 0)
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("login", "Login");
                }
                else
                {
                    return View(lstBranches);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                    ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                    branch.bankId= Convert.ToInt32(principal.FindFirst("BankId").Value);
                    BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                    BusinessObjects.Models.ResultsEnum checkInserted = bALBranches.insertBranch(branch);
                    if (checkInserted == BusinessObjects.Models.ResultsEnum.notInserted)
                    {
                        TempData["errorMsg"] = LangText.checkConnection;
                        return View();
                    }
                    else if (checkInserted == BusinessObjects.Models.ResultsEnum.inserted)
                    {
                        return RedirectToAction("Home", "Branches");
                    }
                    else
                    {
                        ViewBag.errorMsg = LangText.somethingWentWrongAlert;
                        return RedirectToAction("Home", "Branches");
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
        /// Delete branch from database
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int branchId)
        {
            try
            {
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                BusinessObjects.Models.sqlResultsEnum checkDeleted = bALBranches.deleteBranchById(branchId, bankId);
                if (checkDeleted == BusinessObjects.Models.sqlResultsEnum.success)
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
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                BusinessObjects.Models.Branch branch = bALBranches.selectBranchById(branchId, bankId);
                if (branch == null)
                {
                    TempData["errorMsg"] = LangText.checkConnection;
                    return RedirectToAction("Home", "Branches");
                }
                else if (branch.id == 0)
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home", "Branches");
                }
                else
                {
                    return View(branch);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                    ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                    branch.bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                    BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                    BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                    BusinessObjects.Models.ResultsEnum checkUpdated = bALBranches.updateBranch(branch);
                    if (checkUpdated == BusinessObjects.Models.ResultsEnum.notUpdated)
                    {
                        TempData["errorMsg"] = LangText.checkConnection;
                        return RedirectToAction("Home", "Branches");
                    }
                    else if (checkUpdated == BusinessObjects.Models.ResultsEnum.updated)
                    {
                        return RedirectToAction("Home", "Branches");
                    }
                    else
                    {
                        TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                        return RedirectToAction("Home", "Branches");
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
        #endregion
    }
}