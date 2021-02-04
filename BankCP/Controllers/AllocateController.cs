using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;
using BankCP.Models;

namespace BankCP.Controllers
{
    [Authorize]
    public class AllocateController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get counters for services that are not allocated to current selected counter from database and return AllocateCounterServiceHome view
        /// </summary>
        [HttpGet]
        public ActionResult AllocateCounterServiceHome(int counterId)
        {
            try
            {
                if (FillAllocateBag(counterId))
                {
                    return View();
                }
                else
                {
                    ViewBag.connectionMsg = LangText.checkConnection;
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
        /// Insert selected services and review new data changes
        /// </summary>
        [HttpPost]
        public ActionResult AllocateCounterServiceHome(ServiceAllocate lstServiceAllocate)
        {
            try
            {
                if (lstServiceAllocate.AllocateId != null && lstServiceAllocate.AllocateId.Count > 0)
                {
                    BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                    int insertedCheck = bALAllocateCounterService.insertAllocateCounterService(lstServiceAllocate.AllocateId, lstServiceAllocate.counterId);
                    if (insertedCheck == 1)
                    {
                        if (FillAllocateBag(lstServiceAllocate.counterId))
                        {
                            return RedirectToAction("AllocateCounterServiceHome", new { counterId = lstServiceAllocate.counterId });
                        }
                        else
                        {
                            ViewBag.connectionMsg = LangText.checkConnection;
                            return RedirectToAction("BranchesHome", "Branches");
                        }
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
                return View("Error");
            }
        }
        /// <summary>
        /// Partial view to return current allocated services to seleted counter
        /// </summary>
        [HttpGet]
        public ActionResult _AllocateCounterServiceList(int counterId)
        {
            try
            {
                BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                var lstAllocate = bALAllocateCounterService.selectAllocateCounterService(counterId);
                if (lstAllocate != null)
                {
                    return View(lstAllocate);
                }
                else
                {
                    ViewBag.connectionMsg = LangText.checkConnection;
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
        /// Delete allocated service for current selected counter
        /// </summary>
        [HttpPost]
        public ActionResult DeleteAllocateCounterService(BusinessObjects.Models.AllocateCounterService allocateCounterService)
        {
            try
            {
                BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                int DeletedCheck = bALAllocateCounterService.deleteAllocateCounterService(allocateCounterService.id);
                if (DeletedCheck == 0)
                {
                    return View();
                }
                else
                {
                    if (FillAllocateBag(allocateCounterService.counterId))
                    {
                        return RedirectToAction("AllocateCounterServiceHome", new { counterId = allocateCounterService.counterId });
                    }
                    else
                    {
                        ViewBag.connectionMsg = LangText.checkConnection;
                        return RedirectToAction("BranchesHome", "Branches");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Get counters for services that are not allocated to current selected counter
        /// </summary>
        public bool FillAllocateBag(int counterId)
        {
            try
            {
                ViewBag.counterId = counterId;
                BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                List<Models.ServiceAllocate> lstServiceAllocate = new List<ServiceAllocate>();
                List<BusinessObjects.Models.Service> lstServices = bALAllocateCounterService.selectNotAllocateServicesByBankId((((BusinessObjects.Models.User)Session["UserObj"]).bankId));
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = bALAllocateCounterService.selectAllocateCounterService(counterId);
                if (lstAllocateCounterService != null)
                {
                    foreach (var item in lstServices)
                    {
                        if (lstAllocateCounterService.Where(x => x.serviceId == item.id).FirstOrDefault() != null)
                        {
                            item.isDeleted = true;
                        }
                        else
                        {
                            lstServiceAllocate.Add(new ServiceAllocate(item.id, item.enName, item.arName, counterId, null));
                        }
                    }
                    ViewBag.AllocateId = new SelectList(lstServiceAllocate, "id", System.Globalization.CultureInfo.CurrentCulture.ToString() == "en" ? "enName" : "arName");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return false;
            }
        }
        #endregion
    }
}