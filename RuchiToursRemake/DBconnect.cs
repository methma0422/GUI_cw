using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchiToursRemake
{
    class DBconnect
    {
        private readonly MySqlConnection connect;

        public DBconnect()
        {
            // Read connection string from App.config
            string connString = "Server=localhost;Database=rt_app;User Id=root;Password=Mang#301;";
            connect = new MySqlConnection(connString);
        }

        public void OpenConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        public void CloseConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }

        public MySqlConnection GetConnection()
        {
            return connect;
        }
    }
}
