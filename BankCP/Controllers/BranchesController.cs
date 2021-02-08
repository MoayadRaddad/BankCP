using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;

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
                if(TempData["errorMsg"] != null)
                {
                    ViewBag.errorMsg = TempData["errorMsg"];
                    TempData["errorMsg"] = null;
                }
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                List<BusinessObjects.Models.Branch> lstBranches = bALBranches.selectBranchesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstBranches != null)
                {
                    if(lstBranches.Count > 0)
                    {
                        if (lstBranches.FirstOrDefault() != null && lstBranches.FirstOrDefault().id != -1)
                        {
                            return View(lstBranches);
                        }
                        else
                        {
                            ViewBag.errorMsg = LangText.notAuthorized;
                            return View();
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewBag.errorMsg = LangText.checkConnection;
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
                    branch = bALBranches.insertBranch(branch);
                    if (branch != null)
                    {
                        if (branch.id != 0)
                        {
                            return RedirectToAction("Home", "Branches");
                        }
                        else
                        {
                            ViewBag.errorMsg = LangText.notAuthorized;
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.errorMsg = LangText.checkConnection;
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
                else if (checkDeleted == BusinessObjects.Models.ResultsEnum.notAuthorize)
                {
                    TempData["errorMsg"] = LangText.notAuthorized;
                    return RedirectToAction("Home");
                }
                else
                {
                    TempData["errorMsg"] = LangText.itemDeleted;
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
                BusinessObjects.Models.Branch branch = bALBranches.selectBranchById(branchId);
                if (branch != null)
                {
                    if(branch.id != -1)
                    {
                        if(branch.bankId == ((BusinessObjects.Models.User)Session["UserObj"]).bankId)
                        {
                            return View(branch);
                        }
                        else
                        {
                            ViewBag.errorMsg = LangText.notAuthorized;
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.errorMsg = LangText.itemDeleted;
                        return View();
                    }
                }
                else
                {
                    ViewBag.errorMsg = LangText.checkConnection;
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
                    BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                    BusinessAccessLayer.BALBranches.BALBranches bALBranches = new BusinessAccessLayer.BALBranches.BALBranches();
                    branch = bALBranches.updateBranch(branch);
                    if (branch != null)
                    {
                        if(branch.id != 0)
                        {
                            return RedirectToAction("Home", "Branches");
                        }
                        else
                        {
                            ViewBag.errorMsg = LangText.itemDeleted;
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.errorMsg = LangText.checkConnection;
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