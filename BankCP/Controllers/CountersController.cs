using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankCP.Controllers
{
    public class CountersController : Controller
    {
        public ActionResult CounterHome(int branchId)
        {
            try
            {
                Session["BranchId"] = branchId;
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                List<BusinessObjects.Models.Counter> lstCounters = bALCounter.selectCountersByBranchId((int)Session["BranchId"]);
                if (lstCounters != null)
                {
                    return View(lstCounters);
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
        public ActionResult AddCounter()
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
        public ActionResult AddCounter(BusinessObjects.Models.Counter counter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                    counter.branchId = Convert.ToInt32(Session["branchId"]);
                    counter = bALCounter.insertCounter(counter);
                    if (counter != null && counter.id != 0)
                    {
                        return RedirectToAction("CounterHome", "Counters", new { branchId = Convert.ToInt32(Session["branchId"]) });
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
        public ActionResult DeleteCounter(int counterId)
        {
            try
            {
                BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                if (bALCounter.deleteCounterById(counterId) != 0)
                {
                    return RedirectToAction("CounterHome", "Counters", new { branchId = Convert.ToInt32(Session["branchId"]) });
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
        public ActionResult EditCounter(int counterId)
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
        public ActionResult EditCounter(BusinessObjects.Models.Counter counter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALCounter.BALCounter bALCounter = new BusinessAccessLayer.BALCounter.BALCounter();
                    counter = bALCounter.updateCounter(counter);
                    return RedirectToAction("CounterHome", "Counters", new { branchId = Convert.ToInt32(Session["branchId"]) });
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