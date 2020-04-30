using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASSIGNMENT3.DAL
{
  internal  class sqlConn :IDisposable
    {
        String _connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        sqlConn connection = null;


        public sqlConn()
        {
            connection = new sqlConn(_connStr);
            connection.Open();
        }


        public int ExcueteQuery(String sqlQuery)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);
            int res = command.ExecuteNonQuery();
            return res;
        }


        public Object ExcueteScalar(String sqlQuery)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);
            return command.ExecuteScalar();
        }

        public MySqlDataReader ExcueteReader(String sqlQuery)
        {
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);
            return command.ExecuteReader();
        }

        public void Dispose()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
