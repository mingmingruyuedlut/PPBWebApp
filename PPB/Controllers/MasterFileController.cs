using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPB.Controllers
{
    public class MasterFileController : Controller
    {
        // GET: MasterFile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlcMgmt()
        {
            return View();
        }
    }
}