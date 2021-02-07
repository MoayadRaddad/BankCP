using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;
using BankConfigurationPortal.Models;

namespace BankConfigurationPortal.Controllers
{
    [Authorize]
    public class AllocateController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get counters for services that are not allocated to current selected counter from database and return AllocateCounterServiceHome view
        /// </summary>
        [HttpGet]
        public ActionResult Home(int counterId, string errorMsg = null)
        {
            try
            {
                ViewBag.itemDeleted = errorMsg;
                BusinessObjects.Models.ResultsEnum check = FillAllocateBag(counterId);
                if (check != BusinessObjects.Models.ResultsEnum.error)
                {
                    if (check == BusinessObjects.Models.ResultsEnum.filled)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Home", "Branches", new { errorMsg = LangText.itemDeleted });
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
        /// Insert selected services and review new data changes
        /// </summary>
        [HttpPost]
        public ActionResult Home(ServiceAllocate lstServiceAllocate)
        {
            try
            {
                if (lstServiceAllocate.AllocateId != null && lstServiceAllocate.AllocateId.Count > 0)
                {
                    BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                    BusinessObjects.Models.ResultsEnum insertedCheck = bALAllocateCounterService.insertAllocateCounterService(lstServiceAllocate.AllocateId, lstServiceAllocate.counterId);
                    if (insertedCheck == BusinessObjects.Models.ResultsEnum.inserted)
                    {
                        return RedirectToAction("Home", new { counterId = lstServiceAllocate.counterId });
                    }
                    else
                    {
                        return RedirectToAction("Home", new { counterId = lstServiceAllocate.counterId, errorMsg = LangText.itemDeleted });
                    }
                }
                else
                {
                    return RedirectToAction("Home", new { counterId = lstServiceAllocate.counterId });
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
        /// Delete allocated service for current selected counter
        /// </summary>
        [HttpPost]
        public ActionResult Delete(BusinessObjects.Models.AllocateCounterService allocateCounterService)
        {
            try
            {
                BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                BusinessObjects.Models.ResultsEnum DeletedCheck = bALAllocateCounterService.deleteAllocateCounterService(allocateCounterService.id);
                if (DeletedCheck == BusinessObjects.Models.ResultsEnum.notDeleted)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Home", new { counterId = allocateCounterService.counterId });
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
        public BusinessObjects.Models.ResultsEnum FillAllocateBag(int counterId)
        {
            try
            {
                ViewBag.counterId = counterId;
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                List<Models.ServiceAllocate> lstServiceAllocate = new List<ServiceAllocate>();
                List<BusinessObjects.Models.Service> lstServices = bALAllocateCounterService.selectNotAllocateServicesByBankId((((BusinessObjects.Models.User)Session["UserObj"]).bankId));
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = bALAllocateCounterService.selectAllocateCounterService(counterId);
                if (lstAllocateCounterService != null)
                {
                    if (bALCommon.checkExist("tblCounters", counterId))
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
                        return BusinessObjects.Models.ResultsEnum.filled;
                    }
                    else
                    {
                        return BusinessObjects.Models.ResultsEnum.notFilled;
                    }
                }
                else
                {
                    return BusinessObjects.Models.ResultsEnum.error;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.error;
            }
        }
        #endregion
    }
}