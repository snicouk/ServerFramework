using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SocketServer
{
    public class SQL
    {
        private string m_host = "127.0.0.1";
        private string m_port = "3306";
        private string m_user = "root";
        private string m_password = "123456";
        private MySqlConnection m_sqlConn = null;
        static public SQL Instance = null;

        public SQL()
        {
            Instance = this;
            Conn();
        }

        private bool Conn()
        {
            string _connStr = SqlDenfine.ConnStr(m_host,m_port,m_user,m_password,SqlDenfine.Tabel_User);
            try
            {
                m_sqlConn = new MySqlConnection(_connStr);
                m_sqlConn.Open();
                Console.WriteLine("数据库打开成功");
                return true;
            }
            catch
            {
                Console.WriteLine("数据库打开失败");
                return false;
            }
        }

        public void DisConn()
        {
            if (m_sqlConn == null)
                return;
            m_sqlConn.Close();
        }
    }
}
