﻿using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;

namespace BankConfigurationPortal.Controllers
{
    [Authorize]
    public class CountersController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get counters for selected branch from database and return counterHome view
        /// </summary>
        public ActionResult Home(int branchId)
        {
            try
            {
                if (TempData["errorMsg"] != null)
                {
                    ViewBag.errorMsg = TempData["errorMsg"];
                    TempData["errorMsg"] = null;
                }
                ViewBag.branchId = branchId;
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                List<BusinessObjects.Models.Counter> lstCounters = bALCounter.selectCountersByBranchId(branchId, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstCounters != null)
                {
                    if (lstCounters.Count > 0)
                    {
                        if (lstCounters.FirstOrDefault() != null && lstCounters.FirstOrDefault().id != -1)
                        {
                            return View(lstCounters);
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
        /// Return AddCounter view
        /// </summary>
        [HttpGet]
        public ActionResult Add(int branchId)
        {
            try
            {
                ViewBag.branchId = branchId;
                return View();
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// insert counter to database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BusinessObjects.Models.Counter counter, int branchId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                    counter.branchId = branchId;
                    counter = bALCounter.insertCounter(counter);
                    if (counter != null)
                    {
                        if (counter.id != 0)
                        {
                            return RedirectToAction("Home", "Counters", new { branchId = branchId });
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
        /// Delete counter from database
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int counterId, int branchId)
        {
            try
            {
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                BusinessObjects.Models.ResultsEnum checkDeleted = bALCounter.deleteCounterById(counterId, branchId);
                if (checkDeleted == BusinessObjects.Models.ResultsEnum.deleted)
                {
                    return RedirectToAction("Home", new { branchId = branchId });
                }
                else if (checkDeleted == BusinessObjects.Models.ResultsEnum.notAuthorize)
                {
                    TempData["errorMsg"] = LangText.notAuthorized;
                    return RedirectToAction("Home", new { branchId = branchId });
                }
                else
                {
                    TempData["errorMsg"] = LangText.itemDeleted;
                    return RedirectToAction("Home", new { branchId = branchId });
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Return EditCounter view
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int counterId, int branchId)
        {
            try
            {
                ViewBag.branchId = branchId;
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                BusinessObjects.Models.Counter counter = bALCounter.selectCounterById(counterId);
                if (counter != null)
                {
                    if (counter.id != -1)
                    {
                        if (counter.branchId == branchId)
                        {
                            return View(counter);
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
        /// edit counter and save data to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusinessObjects.Models.Counter counter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                    BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                    counter = bALCounter.updateCounter(counter);
                    if (counter != null)
                    {
                        if (counter.id != 0)
                        {
                            return RedirectToAction("Home", "Counters", new { branchId = counter.branchId });
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