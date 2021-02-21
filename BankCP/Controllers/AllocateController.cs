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
    public class AllocateController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get counters for services that are not allocated to current selected counter from database and return AllocateCounterServiceHome view
        /// </summary>
        [HttpGet]
        public ActionResult Home(int counterId)
        {
            try
            {
                ViewBag.counteId = counterId;
                if (TempData["errorMsg"] != null)
                {
                    ViewBag.errorMsg = TempData["errorMsg"];
                    TempData["errorMsg"] = null;
                }
                BusinessObjects.Models.ResultsEnum check = FillAllocateBag(counterId);

                if (check == BusinessObjects.Models.ResultsEnum.filled)
                {
                    return View();
                }
                else if (check == BusinessObjects.Models.ResultsEnum.error)
                {
                    return View("Error");
                }
                else
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home", "Branches");
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                    ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                    var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                    BusinessObjects.Models.ResultsEnum insertedCheck = bALAllocateCounterService.insertAllocateCounterService(lstServiceAllocate.AllocateId, lstServiceAllocate.counterId, bankId);
                    if (insertedCheck != BusinessObjects.Models.ResultsEnum.inserted)
                    {
                        TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    }
                }
                return RedirectToAction("Home", new { counterId = lstServiceAllocate.counterId });

            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                var lstAllocate = bALAllocateCounterService.selectAllocateCounterService(counterId, bankId);
                if (lstAllocate == null)
                {
                    TempData["errorMsg"] = LangText.checkConnection;
                    return RedirectToAction("Home", new { counterId = counterId });
                }
                else if (lstAllocate.Count > 0 && lstAllocate.FirstOrDefault().id == 0)
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home", new { counterId = counterId });
                }
                else
                {
                    return View(lstAllocate);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return View("Error");
            }
        }
        /// <summary>
        /// Delete allocated service for current selected counter
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int id, int counterId)
        {
            try
            {
                BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                BusinessObjects.Models.sqlResultsEnum DeletedCheck = bALAllocateCounterService.deleteAllocateCounterService(id, counterId, bankId);
                if (DeletedCheck == BusinessObjects.Models.sqlResultsEnum.success)
                {
                    return RedirectToAction("Home", new { counterId = counterId });
                }
                else
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home", new { counterId = counterId });
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                BusinessAccessLayer.BALService.BALService bALService = new BusinessAccessLayer.BALService.BALService();
                List<Models.ServiceAllocate> lstServiceAllocate = new List<ServiceAllocate>();
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                List<BusinessObjects.Models.Service> lstServices = bALService.selectServicesByBankId(bankId);
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = bALAllocateCounterService.selectAllocateCounterService(counterId, bankId);
                if (lstAllocateCounterService == null)
                {
                    ViewBag.AllocateId = new SelectList(lstServiceAllocate, "id", System.Globalization.CultureInfo.CurrentCulture.ToString() == "en" ? "enName" : "arName");
                    return BusinessObjects.Models.ResultsEnum.error;
                }
                else if (lstAllocateCounterService.Count == 0 || lstAllocateCounterService.FirstOrDefault().id > 0)
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
                    ViewBag.AllocateId = new SelectList(lstServiceAllocate, "id", System.Globalization.CultureInfo.CurrentCulture.ToString() == "en" ? "enName" : "arName");
                    return BusinessObjects.Models.ResultsEnum.notFound;

                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return BusinessObjects.Models.ResultsEnum.error;
            }
        }
        #endregion
    }
}