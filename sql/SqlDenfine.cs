using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class SqlDenfine
    {
        public const string Tabel_User = "user";

        static public string ConnStr(string pHost,string pPort,string pUser,string pPassword,string pSqlName)
        {
             string _connStr = string.Format("Database={0};Data Source={1};User Id={2};Password={3};port={4}", pSqlName, pHost, pUser, pPassword, pPort);
            return _connStr;
        }
    }
}
