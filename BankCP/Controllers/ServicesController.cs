using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GlobalResource.Resources;

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
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALService.BALService bALService = new BusinessAccessLayer.BALService.BALService();
                List<BusinessObjects.Models.Service> lstServices = bALService.selectServicesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstServices != null)
                {
                    if (lstServices.Count > 0)
                    {
                        if (lstServices.FirstOrDefault() != null && lstServices.FirstOrDefault().id != -1)
                        {
                            return View(lstServices);
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
                    service = bALServices.insertService(service);
                    if (service != null)
                    {
                        if (service.id != 0)
                        {
                            return RedirectToAction("Home", "Services");
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
                else if (checkDeleted == BusinessObjects.Models.ResultsEnum.notAuthorize)
                {
                    TempData["errorMsg"] = LangText.notAuthorized;
                    return RedirectToAction("Home");
                }
                else
                {
                    TempData["errorMsg"] = LangText.itemDeleted;
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
                BusinessObjects.Models.Service service = bALServices.selectServiceById(serviceId);
                if (service != null)
                {
                    if (service.id != -1)
                    {
                        if (service.bankId == ((BusinessObjects.Models.User)Session["UserObj"]).bankId)
                        {
                            return View(service);
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
                    BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                    BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                    service = bALServices.updateService(service);
                    if (service != null)
                    {
                        if (service.id != 0)
                        {
                            return RedirectToAction("Home", "Services");
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