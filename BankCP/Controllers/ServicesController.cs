﻿using BusinessCommon.ExceptionsWriter;
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
    public class ServicesController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get services for current user bank from database and return ServiceHome view
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
                CustomerServiceModel servicesModel = GetServices(1);
                if (servicesModel.Services == null)
                {
                    TempData["errorMsg"] = LangText.checkConnection;
                    return RedirectToAction("login", "Login");
                }
                else if (servicesModel.Services.Count == 0)
                {
                    return View(servicesModel);
                }
                else if (servicesModel.Services.FirstOrDefault().id == 0)
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("login", "Login");
                }
                else
                {
                    return View(servicesModel);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Home(int currentPageIndex)
        {
            CustomerServiceModel servicesModel = GetServices(currentPageIndex);
            if (servicesModel.Services == null)
            {
                TempData["errorMsg"] = LangText.checkConnection;
                return RedirectToAction("login", "Login");
            }
            else if (servicesModel.Services.Count == 0)
            {
                return View(servicesModel);
            }
            else if (servicesModel.Services.FirstOrDefault().id == 0)
            {
                TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                return RedirectToAction("login", "Login");
            }
            else
            {
                return View(servicesModel);
            }
        }
        /// <summary>
        /// Return AddService view
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                BusinessObjects.Models.Service service = new BusinessObjects.Models.Service();
                service.minimumServiceTime = 45;
                service.maximumServiceTime = 300;
                return View(service);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return View("Error");
            }
        }
        /// <summary>
        /// insert service to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BusinessObjects.Models.Service service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                    var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                    service.bankId = bankId;
                    BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                    BusinessObjects.Models.ResultsEnum checkInserted = bALServices.insertService(service);
                    if (checkInserted == BusinessObjects.Models.ResultsEnum.notInserted)
                    {
                        TempData["errorMsg"] = LangText.checkConnection;
                        return RedirectToAction("Home", "Services");
                    }
                    else if (checkInserted == BusinessObjects.Models.ResultsEnum.inserted)
                    {
                        return RedirectToAction("Home", "Services");
                    }
                    else
                    {
                        TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                        return RedirectToAction("Home", "Services");
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
        /// Delete service from database
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int serviceId)
        {
            try
            {
                BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                BusinessObjects.Models.sqlResultsEnum checkDeleted = bALServices.deleteServiceById(serviceId, bankId);
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
        /// Return EditService view
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int serviceId)
        {
            try
            {
                BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                BusinessObjects.Models.Service service = bALServices.selectServiceById(serviceId, bankId);
                if (service == null)
                {
                    TempData["errorMsg"] = LangText.checkConnection;
                    return RedirectToAction("Home", "Services");
                }
                else if (service.id == 0)
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("Home", "Services");
                }
                else
                {
                    service.minimumServiceTime = service.minimumServiceTime != 0 ? service.minimumServiceTime : 45;
                    service.maximumServiceTime = service.maximumServiceTime != 0 ? service.maximumServiceTime : 300;
                    return View(service);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return View("Error");
            }
        }
        /// <summary>
        /// edit service and save data to database
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusinessObjects.Models.Service service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
                    var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
                    service.bankId = bankId;
                    BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                    BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                    BusinessObjects.Models.ResultsEnum checkUpdated = bALServices.updateService(service);
                    if (checkUpdated == BusinessObjects.Models.ResultsEnum.notUpdated)
                    {
                        TempData["errorMsg"] = LangText.checkConnection;
                        return RedirectToAction("Home", "Services");
                    }
                    else if (checkUpdated == BusinessObjects.Models.ResultsEnum.updated)
                    {
                        return RedirectToAction("Home", "Services");
                    }
                    else
                    {
                        TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                        return RedirectToAction("Home", "Services");
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

        #region Methods
        private CustomerServiceModel GetServices(int currentPage)
        {
            int maxRows = 7;
            BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
            BusinessAccessLayer.BALService.BALService bALService = new BusinessAccessLayer.BALService.BALService();
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            var bankId = Convert.ToInt32(principal.FindFirst("BankId").Value);
            List<BusinessObjects.Models.Service> lstServices = bALService.selectServicesByBankId(bankId);
            CustomerServiceModel servicesModel = new CustomerServiceModel();
            servicesModel.Services = lstServices.ToList().Skip((currentPage - 1) * maxRows).Take(maxRows).ToList();
            double pageCount = (double)((decimal)lstServices.Count() / Convert.ToDecimal(maxRows));
            servicesModel.PageCount = (int)Math.Ceiling(pageCount);
            servicesModel.CurrentPageIndex = currentPage;
            return servicesModel;
        }
        #endregion
    }
}