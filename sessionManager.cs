using ASSIGNMENT3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAD_ASSIGNMENT3.security
{
    public class sessionManager
    {
        public static userDTO User
        {
            get
            {
                userDTO user = null;
                if (HttpContext.Current.Session["user"] != null)
                {
                    user = HttpContext.Current.Session["user"] as userDTO;
                }
                return user;
            }
            set
            {
                HttpContext.Current.Session["user"] = value;
            }
        }

        public static bool IsValidUser
        {
            get
            {
                if (User != null)
                {
                    return true;
                }
                return false;
            }
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }
    }
}