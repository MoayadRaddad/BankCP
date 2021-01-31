using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankCP.Controllers
{
    public class AllocateCounterServiceController : Controller
    {
        [HttpGet]
        public ActionResult AllocateCounterServiceHome(int counterId, int serviceId)
        {
            try
            {
                Session["counterId"] = counterId;
                List<BusinessObjects.Models.Service> lstServices = new List<BusinessObjects.Models.Service>();
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = new List<BusinessObjects.Models.AllocateCounterService>();
                if (Session["lstServices"] == null)
                {
                    BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                    lstServices = bALAllocateCounterService.selectNotAllocateServicesByBankId((((BusinessObjects.Models.User)Session["UserObj"]).bankId));
                    lstAllocateCounterService = bALAllocateCounterService.selectAllocateCounterService(Convert.ToInt32(Session["counterId"]));
                    Session["lstAllocateCounterService"] = lstAllocateCounterService;
                    foreach (var item in lstServices)
                    {
                        if (lstAllocateCounterService.Where(x => x.serviceId == item.id).FirstOrDefault() != null)
                        {
                            item.isDeleted = true;
                        }
                    }
                    Session["lstServices"] = lstServices;
                }
                else if (serviceId != 0)
                {
                    lstServices = (List<BusinessObjects.Models.Service>)Session["lstServices"];
                    BusinessObjects.Models.Service service = lstServices.Where(x => x.id == serviceId && x.isDeleted == false).FirstOrDefault();
                    if (service != null)
                    {
                        Session["serviceEnName"] = service.enName;
                        Session["serviceArName"] = service.arName;
                        Session["serviceId"] = service.id;
                        int index = lstServices.IndexOf(service);
                        service.isDeleted = true;
                        lstServices[index] = service;
                    }
                }
                else
                {
                    lstServices = (List<BusinessObjects.Models.Service>)Session["lstServices"];
                }
                if (lstServices != null)
                {
                    return View(lstServices.Where(x => x.isDeleted != true).ToList());
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
        public ActionResult AllocateCounterServiceHome(int serviceId)
        {
            try
            {
                return RedirectToAction("AllocateCounterServiceHome", new { counterId = Convert.ToInt32(Session["counterId"]), serviceId = serviceId });
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return RedirectToAction("login", "Login");
            }
        }
        [HttpGet]
        public ActionResult _AllocateCounterServiceList()
        {
            try
            {
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = new List<BusinessObjects.Models.AllocateCounterService>();
                if (Session["serviceId"] != null)
                {
                    lstAllocateCounterService = (List<BusinessObjects.Models.AllocateCounterService>)Session["lstAllocateCounterService"];
                    lstAllocateCounterService.Add(new BusinessObjects.Models.AllocateCounterService { id = 0, counterId = Convert.ToInt32(Session["counterId"]), serviceId = Convert.ToInt32(Session["serviceId"]), serviceEnName = Session["serviceEnName"].ToString(), serviceArName = Session["serviceArName"].ToString() });
                    Session["serviceId"] = null;
                    Session["serviceEnName"] = null;
                    Session["serviceArName"] = null;
                }
                else
                {
                    lstAllocateCounterService = (List<BusinessObjects.Models.AllocateCounterService>)Session["lstAllocateCounterService"];
                }
                if (lstAllocateCounterService != null)
                {
                    return View(lstAllocateCounterService.Where(x => x.isDeleted != true).ToList());
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
        public ActionResult insertDeleteAllocateCounterService()
        {
            try
            {
                if (Session["lstAllocateCounterService"] != null)
                {
                    BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
                    List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = (List<BusinessObjects.Models.AllocateCounterService>)Session["lstAllocateCounterService"];
                    int insertedCheck = bALAllocateCounterService.insertDeleteAllocateCounterService(lstAllocateCounterService);
                    if (insertedCheck == 1)
                    {
                        return RedirectToAction("CounterHome", "Counters", new { branchId = Session["branchId"] });
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
        public ActionResult DeleteAllocateCounterService(BusinessObjects.Models.AllocateCounterService allocateCounterServiceId)
        {
            try
            {
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = (List<BusinessObjects.Models.AllocateCounterService>)Session["lstAllocateCounterService"];
                BusinessObjects.Models.AllocateCounterService item = lstAllocateCounterService.Where(x => x.id == allocateCounterServiceId.id && x.counterId == allocateCounterServiceId.counterId && x.serviceId == allocateCounterServiceId.serviceId).FirstOrDefault();
                lstAllocateCounterService.Remove(item);
                Session["lstAllocateCounterService"] = lstAllocateCounterService;
                List<BusinessObjects.Models.Service> lstServices = (List<BusinessObjects.Models.Service>)Session["lstServices"];
                lstServices.Where(x => x.id == allocateCounterServiceId.serviceId).FirstOrDefault().isDeleted = false;
                Session["lstServices"] = lstServices;
                return RedirectToAction("AllocateCounterServiceHome", new { counterId = Session["counterId"], serviceId = 0 });
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return RedirectToAction("login", "Login");
            }
        }
    }
}