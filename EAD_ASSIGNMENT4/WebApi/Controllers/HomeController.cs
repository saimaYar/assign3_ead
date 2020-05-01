using ASSIGNMENT3.DAL;
using ASSIGNMENT3.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Assignment_WebApi.Controllers
{
    public class HomeController : ApiController
    {
        [Authorize]
        [HttpPost]
        public Object GetFolders()
        {
            var identity = User.Identity as ClaimsIdentity;
            Object data = null;
            if (identity != null)
            {
                int userId = Int32.Parse(HttpContext.Current.Request["userId"]);
                int parentFId = Int32.Parse(HttpContext.Current.Request["parentFId"]);
                List<folderDAO> foldersList = folderBO.getFolders(userId, parentFId);
                data = new
                {
                    folders = foldersList,
                };
            }
            return data;
        }


        [Authorize]
        [HttpPost]
        public Object CreateFolders()
        {
            var identity = User.Identity as ClaimsIdentity;
            Object data = null;
            String child = HttpContext.Current.Request["child"];
            int userId = Int32.Parse(HttpContext.Current.Request["userId"]);
            int parentFolder = Int32.Parse(HttpContext.Current.Request["parentFolder"]);
            if (identity != null)
            {
                int id = 0;
                bool flag = false;
                folderDTO folder = folderBO.createFolder(child, userId, parentFolder);
                if (folder != null)
                {
                    id = folder.folderId;
                    flag = true;
                }
                data = new
                {
                    success = flag,
                    folderId = id
                };
            }
            return data;
        }
    }
}
