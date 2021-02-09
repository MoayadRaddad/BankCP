﻿using BusinessCommon.ExceptionsWriter;
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
                if (check != BusinessObjects.Models.ResultsEnum.error)
                {
                    if (check == BusinessObjects.Models.ResultsEnum.notAuthorize)
                    {
                        ViewBag.errorMsg = LangText.notAuthorized;
                    }
                    else if (check != BusinessObjects.Models.ResultsEnum.filled)
                    {
                        ViewBag.errorMsg = LangText.itemDeleted;
                    }
                    return View();
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
                    BusinessObjects.Models.ResultsEnum insertedCheck = bALAllocateCounterService.insertAllocateCounterService(lstServiceAllocate.AllocateId, lstServiceAllocate.counterId, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                    if (insertedCheck == BusinessObjects.Models.ResultsEnum.inserted)
                    {
                        return RedirectToAction("Home", new { counterId = lstServiceAllocate.counterId });
                    }
                    else
                    {
                        ViewBag.errorMsg = LangText.itemDeleted;
                        return View();
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
                var lstAllocate = bALAllocateCounterService.selectAllocateCounterService(counterId, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstAllocate != null)
                {
                    if (lstAllocate.Count > 0)
                    {
                        if (lstAllocate.FirstOrDefault() != null && lstAllocate.FirstOrDefault().id != -1)
                        {
                            return View(lstAllocate);
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
        /// Delete allocated service for current selected counter
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int id, int counterId)
        {
            try
            {
                BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                BusinessObjects.Models.ResultsEnum DeletedCheck = bALAllocateCounterService.deleteAllocateCounterService(id, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (DeletedCheck == BusinessObjects.Models.ResultsEnum.deleted)
                {
                    return RedirectToAction("Home", new { counterId = counterId });
                }
                else if (DeletedCheck == BusinessObjects.Models.ResultsEnum.notAuthorize)
                {
                    TempData["errorMsg"] = LangText.notAuthorized;
                    return RedirectToAction("Home", new { counterId = counterId });
                }
                else
                {
                    TempData["errorMsg"] = LangText.itemDeleted;
                    return RedirectToAction("Home", new { counterId = counterId });
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
                BusinessAccessLayer.BALService.BALService bALService = new BusinessAccessLayer.BALService.BALService();
                List<Models.ServiceAllocate> lstServiceAllocate = new List<ServiceAllocate>();
                List<BusinessObjects.Models.Service> lstServices = bALService.selectServicesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = bALAllocateCounterService.selectAllocateCounterService(counterId, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstAllocateCounterService != null)
                {
                    if (lstAllocateCounterService.Count == 0 || lstAllocateCounterService.FirstOrDefault().id > 0)
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
                        if(lstAllocateCounterService.FirstOrDefault().id == 0)
                        {
                            ViewBag.AllocateId = new SelectList(lstServiceAllocate, "id", System.Globalization.CultureInfo.CurrentCulture.ToString() == "en" ? "enName" : "arName");
                            return BusinessObjects.Models.ResultsEnum.deleted;
                        }
                        else
                        {
                            ViewBag.AllocateId = new SelectList(lstServiceAllocate, "id", System.Globalization.CultureInfo.CurrentCulture.ToString() == "en" ? "enName" : "arName");
                            return BusinessObjects.Models.ResultsEnum.notAuthorize;
                        }
                    }
                }
                else
                {
                    ViewBag.AllocateId = new SelectList(lstServiceAllocate, "id", System.Globalization.CultureInfo.CurrentCulture.ToString() == "en" ? "enName" : "arName");
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