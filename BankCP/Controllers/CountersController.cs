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
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                List<BusinessObjects.Models.Counter> lstCounters = bALCounter.selectCountersByBranchId(branchId, bankId);
                if (lstCounters == null)
                {
                    TempData["errorMsg"] = LangText.checkConnection;
                    return RedirectToAction("Home", "Branches");
                }
                else if (lstCounters.Count == 0)
                {
                    return View();
                }
                else if (lstCounters.FirstOrDefault().id == 0)
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home", "Branches");
                }
                else
                {
                    return View(lstCounters);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                    ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                    var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                    BusinessObjects.Models.ResultsEnum checkInserted = bALCounter.insertCounter(counter, bankId);
                    if (checkInserted == BusinessObjects.Models.ResultsEnum.notInserted)
                    {
                        TempData["errorMsg"] = LangText.checkConnection;
                        return RedirectToAction("Home", "Counters", new { branchId = branchId });
                    }
                    else if (checkInserted == BusinessObjects.Models.ResultsEnum.inserted)
                    {
                        return RedirectToAction("Home", "Counters", new { branchId = branchId });
                    }
                    else
                    {
                        TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                        return RedirectToAction("Home", "Counters", new { branchId = branchId });
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
        /// Delete counter from database
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int counterId, int branchId)
        {
            try
            {
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                BusinessObjects.Models.sqlResultsEnum checkDeleted = bALCounter.deleteCounterById(counterId, bankId, branchId);
                if (checkDeleted == BusinessObjects.Models.sqlResultsEnum.success)
                {
                    return RedirectToAction("Home", new { branchId = branchId });
                }
                else
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home", new { branchId = branchId });
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                BusinessObjects.Models.Counter counter = bALCounter.selectCounterById(counterId, bankId);
                if (counter == null)
                {
                    TempData["errorMsg"] = LangText.checkConnection;
                    return RedirectToAction("Home", "Counters", new { branchId = branchId });
                }
                else if (counter.id == 0)
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home", "Counters", new { branchId = branchId });
                }
                else
                {
                    return View(counter);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                    ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                    var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                    BusinessObjects.Models.ResultsEnum checkUpdated = bALCounter.updateCounter(counter, bankId);
                    if (checkUpdated == BusinessObjects.Models.ResultsEnum.notUpdated)
                    {
                        TempData["errorMsg"] = LangText.checkConnection;
                        return RedirectToAction("Home", "Counters", new { branchId = counter.branchId });
                    }
                    else if (checkUpdated == BusinessObjects.Models.ResultsEnum.updated)
                    {
                        return RedirectToAction("Home", "Counters", new { branchId = counter.branchId });
                    }
                    else
                    {
                        TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                        return RedirectToAction("Home", "Counters", new { branchId = counter.branchId });
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
