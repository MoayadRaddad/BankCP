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
        public ActionResult Home(string errorMsg = null)
        {
            try
            {
                ViewBag.itemDeleted = errorMsg;
                BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                BusinessAccessLayer.BALService.BALService bALService = new BusinessAccessLayer.BALService.BALService();
                List<BusinessObjects.Models.Service> lstServices = bALService.selectServicesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstServices != null )
                {
                    if(bALCommon.checkExist("tblBanks", ((BusinessObjects.Models.User)Session["UserObj"]).bankId))
                    {
                        return View(lstServices);
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
                        if(service.id != 0)
                        {
                            return RedirectToAction("Home", "Services");
                        }
                        else
                        {
                            return RedirectToAction("Home", "Services", new { errorMsg = LangText.itemDeleted });
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
        /// Delete service from database
        /// </summary>
        [HttpPost]
        public ActionResult Delete(int serviceId)
        {
            try
            {
                BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                bALServices.deleteServiceById(serviceId);
                return RedirectToAction("Home", "Services");
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
                BusinessObjects.Models.Service service = bALServices.selectServicesById(serviceId);
                if (service != null)
                {
                    return View(service);
                }
                else
                {
                    return RedirectToAction("Home", "Services", new { errorMsg = LangText.itemDeleted });
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
        public ActionResult Edit(BusinessObjects.Models.Service Service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALCommon.BALCommon bALCommon = new BusinessAccessLayer.BALCommon.BALCommon();
                    BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                    Service = bALServices.updateService(Service);
                    if (Service != null)
                    {
                        if(bALCommon.checkExist("tblService", Service.id))
                        {
                            return RedirectToAction("Home", "Services");
                        }
                        else
                        {
                            return RedirectToAction("Home", "Services", new { errorMsg = LangText.itemDeleted });
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