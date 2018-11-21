using PPB.DBManager.Models;
using PPB.DBManager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPB.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            LoginUser loginUser = new AccountManager().AccountSignInValidation(model.UserName, model.Password);

            if (loginUser.ValidateResult.ValidateResultType == LoginUserValidateResultType.Success)
            {
                Session["CurrentLoginUser"] = loginUser;
                return RedirectToAction("Index", "Home");
            }
            else if (loginUser.ValidateResult.ValidateResultType == LoginUserValidateResultType.UserNotExist)
            {
                ModelState.AddModelError("UserName", "Username doesn't exist");
                return View(model);
            }
            else if (loginUser.ValidateResult.ValidateResultType == LoginUserValidateResultType.PasswordInvalid)
            {
                ModelState.AddModelError("Password", "Username and password doesn't match");
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            Session["CurrentLoginUser"] = null;
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}