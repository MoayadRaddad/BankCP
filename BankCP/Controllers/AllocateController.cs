using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankCP.Controllers
{
    public class AllocateController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService bALAllocateCounterService = new BusinessAccessLayer.BALAllocateCounterService.BALAllocateCounterService();
            List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = bALAllocateCounterService.selectAllocateCounterService(1003);
            return View(lstAllocateCounterService);
        }

        [HttpPost]
        public ActionResult Select(List<int> lstService)
        {
            lstService = lstService;
            return View(lstService);
        }
    }
}