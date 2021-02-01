using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankCP.Controllers
{
    public class ServicesController : Controller
    {
        public ActionResult ServiceHome()
        {
            try
            {
                BusinessAccessLayer.BALService.BALService bALService = new BusinessAccessLayer.BALService.BALService();
                List<BusinessObjects.Models.Service> lstServices = bALService.selectServicesByBankId(((BusinessObjects.Models.User)Session["UserObj"]).bankId);
                if (lstServices != null)
                {
                    return View(lstServices);
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
        public ActionResult AddService()
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
        public ActionResult AddService(BusinessObjects.Models.Service service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId;
                    BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                    service = bALServices.insertService(service);
                    if (service != null && service.id != 0)
                    {
                        return RedirectToAction("ServiceHome", "Services");
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
        public ActionResult DeleteService(int serviceId)
        {
            try
            {
                BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                if (bALServices.deleteServiceById(serviceId) != 0)
                {
                    return RedirectToAction("ServiceHome", "Services");
                }
                else
                {
                    ViewBag.connectionMsg = "<script>alert('" + GlobalResource.Resources.LangText.checkConnection + "');</script>";
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
        public ActionResult EditService(BusinessObjects.Models.Service Service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                    Service = bALServices.updateService(Service);
                    return RedirectToAction("ServiceHome", "Services");
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