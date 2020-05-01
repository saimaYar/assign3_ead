using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASSIGNMENT3.DAL;
using ASSIGNMENT3.Entities;
using ASSIGNMENT3.BAL;

namespace ASSIGNMENT3.BAL
{
    public static class folderBO
    {

        public static List<folderDTO> getFolders(int userId, int prntId)
        {
            return folderDAO.getFolders(userId, prntId);
        }
        public static folderBO createFolder(String child, int userId, int parentFolder)
        {
            return folderDAO.createFolder(child, userId, parentFolder);
        }
    }
}
