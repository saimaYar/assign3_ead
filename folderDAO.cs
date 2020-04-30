using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASSIGNMENT3.Entities;

namespace ASSIGNMENT3.DAL
{
   public static class folderDAO
    {
        public static List<folderDTO> getFolders(int userId, int prntfId)
        {
            String query = "";
            List<folderDTO> foldersList = new List<folderDTO>();
            if (prntfId == 0)
            {
                query = String.Format("SELECT * FROM folderTable where id='{0}' and parentFolderId IS NULL", userId);
            }
            else
            {
                query = String.Format("SELECT * FROM folderTable where id='{0}' and parentFolderId='{1}'", userId, prntfId);
            }
            using (sqlConn connection = new sqlConn())
            {
                var reader = connection.ExcueteReader(query);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        folderDTO folder = new folderDTO();
                        folder.folderId = reader.GetInt32(0);
                        folder.folderName = reader.GetString(1);
                        if (prntfId == 0)
                        {
                            folder.parentFolderId = 0;
                        }
                        else
                        {
                            folder.parentFolderId = reader.GetInt32(2);
                        }
                        folder.id = reader.GetInt32(3);
                        foldersList.Add(folder);
                    }
                }
                else
                {
                    foldersList = null;
                }
                return foldersList;
            }

        }

        public static folderDTO createFolder(String child, int uid, int parentFolder)
        {
            String query = "";
            String query1 = "";
            folderDTO folder = new folderDTO();
            folder.folderName = child;
            folder.parentFolderId = parentFolder;
            folder.id = uid;
            if (parentFolder == 0)
            {
                query = String.Format("SELECT * FROM folderTable where folderName='{0}' and parentFolderId is NULL", child);
            }
            else
            {
                query = String.Format("SELECT * FROM folderTable where folderName='{0}' and parentFolderId='{1}'", child, parentFolder);
            }
            using (sqlConn connection = new sqlConn())
            {
                var reader = connection.ExcueteReader(query);
                if (reader.Read())
                {
                    return null;
                }
                else
                {
                    using (sqlConn connection1 = new sqlConn())
                    {
                        if (parentFolder == 0)
                        {
                            query1 = String.Format("INSERT INTO folderTable (folderName,id) VALUES ('{0}','{1}')", child, uid);
                        }
                        else
                        {
                            query1 = String.Format("INSERT INTO folderTable (folderName,parentFolderId,id) VALUES ('{0}','{1}','{2}')", child, parentFolder, uid);
                        }

                        int retValue = connection1.ExcueteQuery(query1);

                        if (retValue == 1)
                        {
                            String sql = "SELECT folderId FROM userInfo.folders ORDER BY folderId DESC LIMIT 1";
                            var result = connection1.ExcueteScalar(sql);
                            int id = Int32.Parse(result.ToString());
                            folder.folderId = id;
                            return folder;
                        }
                    }
                }
            }

            return null;
        }
    }
}
