using ASSIGNMENT3.BAL;
using ASSIGNMENT3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Assignment_WebApi.Controllers
{

    public class UserController : ApiController
    {
        [HttpPost]
        public Object ValidateUser()
        {
            String Login = HttpContext.Current.Request["Login"];
            String Password = HttpContext.Current.Request["Password"];
            usersDTO userDto = userBO.validateUser(Login, Password);
            bool flag = false;
            Object tokken = null;
            if (userDto != null)
            {
                tokken = TokenManager.GetToken(userDto);
                flag = true;
            }
            var data = new
            {
                user = userDto,
                success = flag,
                token = tokken
            };
            return data;
        }

        [HttpPost]
        public Object RegisterUser()
        {
            userDTO user = new userDTO();
            user.id = Int32.Parse(HttpContext.Current.Request["id"]);
            user.login = HttpContext.Current.Request["login"];
            user.name = HttpContext.Current.Request["name"];
            user.password = HttpContext.Current.Request["password"];
            int id = userBO.save(user);
            bool flag = true;
            if (id == 0)
            {
                flag = false;
            }
            else
            {
                user.id = id;
            }
            var data = new
            {
                success = flag
            };
            return data;
        }

    }
}
