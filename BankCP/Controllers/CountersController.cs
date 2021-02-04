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
    public class CountersController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get counters for selected branch from database and return counterHome view
        /// </summary>
        public ActionResult CounterHome(int branchId, string errorMsg = null)
        {
            try
            {
                ViewBag.branchId = branchId;
                ViewBag.itemDeleted = errorMsg;
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                List<BusinessObjects.Models.Counter> lstCounters = bALCounter.selectCountersByBranchId(branchId);
                if (lstCounters != null)
                {
                    if(bALCommon.checkExist("tblBranches", branchId))
                    {
                        return View(lstCounters);
                    }
                    else
                    {
                        return RedirectToAction("BranchesHome", "Branches", new { errorMsg = LangText.itemDeleted });
                    }
                }
                else
                {
                    return View("Error");
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
        public ActionResult AddCounter(int branchId)
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
        public ActionResult AddCounter(BusinessObjects.Models.Counter counter, int branchId)
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
                        if(counter.id != 0)
                        {
                            return RedirectToAction("CounterHome", "Counters", new { branchId = branchId });
                        }
                        else
                        {
                            return RedirectToAction("CounterHome", "Counters", new { branchId = branchId, errorMsg = LangText.checkConnection });
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
        /// Delete counter from database
        /// </summary>
        [HttpPost]
        public ActionResult DeleteCounter(int counterId, int branchId)
        {
            try
            {
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                bALCounter.deleteCounterById(counterId);
                return RedirectToAction("CounterHome", "Counters", new { branchId = branchId });
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
        public ActionResult EditCounter(int counterId, int branchId)
        {
            try
            {
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                BusinessObjects.Models.Counter counter = bALCounter.selectCountersById(counterId);
                if (counter != null)
                {
                    return View(counter);
                }
                else
                {
                    return RedirectToAction("CounterHome", "Counters", new { branchId = branchId, errorMsg = LangText.checkConnection });
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
        public ActionResult EditCounter(BusinessObjects.Models.Counter counter)
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
                        if(bALCommon.checkExist("tblCounters", counter.id))
                        {
                            return RedirectToAction("CounterHome", "Counters", new { branchId = counter.branchId });
                        }
                        else
                        {
                            return RedirectToAction("CounterHome", "Counters", new { branchId = counter.branchId, errorMsg = LangText.itemDeleted });
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
        #endregion
    }
}