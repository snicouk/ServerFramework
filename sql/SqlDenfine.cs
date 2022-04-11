using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocketServer
{
    public class SqlDenfine
    {

        private const string ConnRegex = @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|&|@|\*|!|\']";


        /// <summary>  user 数据库 </summary>
        public const string DB_User = "user";
        /// <summary>  userinfo 表 </summary>
        public const string TB_UserInfo = "userinfo";
      
      
        /// <summary>
        /// 是否合法
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        /// 
        static public bool IsSafe(string pStr)
        {
            return !Regex.IsMatch(pStr, ConnRegex);  //匹配不到，字符串合法
        }


        static public string CmdDBStr(string pHost,string pPort,string pUser,string pPassword,string pSqlName)
        {
             string _connStr = string.Format("Database={0};Data Source={1};User Id={2};Password={3};port={4}", pSqlName, pHost, pUser, pPassword, pPort);
            return _connStr;
        }
        /// <summary>
        /// cmd userinfo 表 字符串
        /// </summary>
        /// <param name="pAccount"></param>
        /// <param name="pPassword"></param>
        /// <returns></returns>
        static public string CmdTBUserInfoStr(string pAccount , string pPassword)
        {
            string _str = string.Format("select * from {0} where account ='{1}' and password='{2}';", TB_UserInfo, pAccount, pPassword);
            return _str;
        }

        /// <summary>
        /// cmd userinfo 表 中账号信息
        /// </summary>
        /// <param name="pAccount"></param>
        /// <param name="pPassword"></param>
        /// <returns></returns>
        static public string CmdTBUserInfoAccountStr(string pAccount)
        {
            string _str = string.Format("select * from {0} where account ='{1}';", TB_UserInfo, pAccount);
            return _str;
        }

        /// <summary>
        /// 在用户信息表中插入信息
        /// </summary>
        /// <param name="pAccountm"></param>
        /// <param name="pPassword"></param>
        /// <returns></returns>
        static public string CmdInsertTBUserInfoStr(string pAccountm ,string pPassword)
        {
            string _str = string.Format("insert into {0} set account='{1}',password='{2}';", TB_UserInfo, pAccountm, pPassword);
            return _str;
        }
       
    }
}
