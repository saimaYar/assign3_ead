using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASSIGNMENT3.Entities;

namespace ASSIGNMENT3.DAL
{
   public static class userDAO
    {
        public static userDTO validateUser(String login, String password)
        {
            String query = String.Format("SELECT * FROM userInfo.user where login='{0}' and password='{1}'", login, password);
            using (sqlConn connection = new sqlConn())
            {
                userDTO user = new userDTO();
                var reader = connection.ExcueteReader(query);
                if (reader.Read())
                {
                    user.id = reader.GetInt32(0);
                    user.login = reader.GetString(1);
                    user.name = reader.GetString(2);
                    user.password = reader.GetString(3);
                }
                else
                {
                    user = null;
                }
                return user;
            }
        }

        public static int save(userDTO user)
        {
            String query = String.Format("INSERT INTO userInfo.user(name, login, password) VALUES('{0}', '{1}', '{2}')", user.name, user.login, user.password);
            query = query + "; Select LAST_INSERT_ID()";
            using (sqlConn connection = new sqlConn())
            {
                int userId = 0;
                if (user.id == 0)
                {
                    try
                    {
                        var obj = connection.ExcueteScalar(query);
                        String obj1 = obj.ToString();
                        userId = Int32.Parse(obj1);
                    }
                    catch (Exception)
                    {
                        userId = 0;
                    }
                }
                return userId;
            }

        }
    }
}
