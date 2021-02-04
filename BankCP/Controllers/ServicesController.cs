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
    public class ServicesController : Controller
    {
        #region ActionMethods
        /// <summary>
        /// Get services for current user bank from database and return ServiceHome view
        /// </summary>
        [HttpGet]
        public ActionResult ServiceHome(string errorMsg = null)
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
        /// Return AddService view
        /// </summary>
        [HttpGet]
        public ActionResult AddService()
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
        public ActionResult AddService(BusinessObjects.Models.Service service)
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
                            return RedirectToAction("ServiceHome", "Services");
                        }
                        else
                        {
                            return RedirectToAction("ServiceHome", "Services", new { errorMsg = LangText.itemDeleted });
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
        public ActionResult DeleteService(int serviceId)
        {
            try
            {
                BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                bALServices.deleteServiceById(serviceId);
                return RedirectToAction("ServiceHome", "Services");
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
        public ActionResult EditService(int serviceId)
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
                    return RedirectToAction("ServiceHome", "Services", new { errorMsg = LangText.itemDeleted });
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
        public ActionResult EditService(BusinessObjects.Models.Service Service)
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
                            return RedirectToAction("ServiceHome", "Services");
                        }
                        else
                        {
                            return RedirectToAction("ServiceHome", "Services", new { errorMsg = LangText.itemDeleted });
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