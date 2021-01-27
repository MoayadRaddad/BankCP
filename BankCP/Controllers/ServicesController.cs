using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankCP.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult ServiceHome()
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

        [HttpGet]
        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddService(BusinessObjects.Models.Service service)
        {
            if (ModelState.IsValid)
            {
                BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
                service.bankId = ((BusinessObjects.Models.User)Session["UserObj"]).bankId;
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

        [HttpPost]
        public ActionResult DeleteService(int serviceId)
        {
            BusinessAccessLayer.BALService.BALService bALServices = new BusinessAccessLayer.BALService.BALService();
            if (bALServices.deleteServiceById(serviceId) != 0)
            {
                return RedirectToAction("ServiceHome", "Services");
            }
            else
            {
                TempData["msg"] = "<script>alert('Please check your connection');</script>";
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditService(int serviceId)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditService(BusinessObjects.Models.Service Service)
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
    }
}