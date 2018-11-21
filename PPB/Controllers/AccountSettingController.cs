using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPB.Controllers
{
    public class AccountSettingController : Controller
    {
        // GET: AccountSetting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult PersonalInformation()
        {
            return View();
        }
    }
}