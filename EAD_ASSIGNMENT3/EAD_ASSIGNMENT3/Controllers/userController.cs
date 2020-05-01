using ASSIGNMENT3.BAL;
using ASSIGNMENT3.Entities;
using EAD_ASSIGNMENT3.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EAD_ASSIGNMENT3.Controllers
{
    public class userController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateUser(String Login, String Password)
        {
            userDTO userInfo = userBO.validateUser(Login, Password);
            bool flag = false;
            if (userInfo != null)
            {
                sessionManager.User = userInfo;
                flag = true;
            }
            var data = new
            {
                success = flag
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult RegisterUser(userDTO userInfo)
        {
            int id = userBO.save(userInfo);
            bool flag = true;
            if (id == 0)
            {
                flag = false;
            }
            else
            {
                userInfo.id = id;
                sessionManager.user = userInfo;
            }
            var data = new
            {
                success = flag
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Home()
        {
            if (sessionManager.IsValidUser)
            {
                return View();
            }
            else
            {
                return Redirect("~/User/Login");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            sessionManager.ClearSession();
            return Redirect("~/User/Login");
        }
    }
}