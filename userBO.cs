using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASSIGNMENT3.DAL;
using ASSIGNMENT3.Entities;

namespace ASSIGNMENT3.BAL
{
    public static class userBO
    {
        public static userDTO validateUser(String login, String password)
        {
            return userDAO.validateUser(login, password);
        }
        public static int save(userDTO user)
        {
            return userDAO.save(user);
        }
    }
}
