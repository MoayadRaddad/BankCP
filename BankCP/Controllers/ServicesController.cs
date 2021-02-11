using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;
using BankCP.Models;

namespace BankConfigurationPortal.Controllers
{
    [SessionAuthorize]
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
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALService.BALService bALService = new BusinessAccessLayer.BALService.BALService();
                List<BusinessObjects.Models.Service> lstServices = bALService.selectServicesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstServices == null)
                {
                    TempData["errorMsg"] = LangText.checkConnection;
                    return RedirectToAction("login", "Login");
                }
                else if (lstServices.Count == 0)
                {
                    return View();
                }
                else if (lstServices.FirstOrDefault().id == 0)
                {
                    TempData["errorMsg"] = LangText.somethingWentWrongAlert;
                    return RedirectToAction("login", "Login");
                }
                else
                {
                    return View(lstServices);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
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
                return View();
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
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
                    service.bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId;
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
                ExceptionsWriter.saveExceptionToLogFile(ex);
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
                BusinessObjects.Models.ResultsEnum checkDeleted = bALServices.deleteServiceById(serviceId, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (checkDeleted == BusinessObjects.Models.ResultsEnum.deleted)
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
                ExceptionsWriter.saveExceptionToLogFile(ex);
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
                BusinessObjects.Models.Service service = bALServices.selectServiceById(serviceId, ((BusinessObjects.Models.User)Session["UserObj"]).bankId);
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
                    return View(service);
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
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
                    service.bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId;
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
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return View("Error");
            }
        }
        #endregion
    }
}