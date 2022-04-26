using System;
using MySql.Data.MySqlClient;

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
            ConnDB();
        }

        private bool ConnDB()
        {
            SqlDenfine.Init();
            string _cmdStr = SqlDenfine.CmdDBStr(m_host, m_port, m_user, m_password, SqlDenfine.DB_User);
            try
            {
                m_sqlConn = new MySqlConnection(_cmdStr);
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

        public bool IsExistUser(string pAccount, string pPassword)
        {
       
            //防止账号密码注入非法字符
            if (SqlDenfine.IsSafe(pAccount) && SqlDenfine.IsSafe(pPassword))
            {
                string _cmdStr = SqlDenfine.CmdTBUserInfoStr(pAccount, pPassword);
                MySqlCommand _cmd = new MySqlCommand(_cmdStr, m_sqlConn);
                try
                {          
                    MySqlDataReader _dataReader = _cmd.ExecuteReader();
                    bool _hasRows = _dataReader.HasRows; //如果数据库表中有这一行数据
                    _dataReader.Close();
                    return _hasRows;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CanRegister(string pAccount,string pPassword,ref bool pIsExist)
        {
            
            if (SqlDenfine.IsSafe(pAccount) && SqlDenfine.IsSafe(pPassword))
            {
                string _cmdStr = SqlDenfine.CmdTBUserInfoAccountStr(pAccount);
                MySqlCommand _cmd = new MySqlCommand(_cmdStr, m_sqlConn);
                try
                {
                    MySqlDataReader _dataReader = _cmd.ExecuteReader();
                    bool _hasRows = _dataReader.HasRows; //如果数据库表中有这一行数据
                    _dataReader.Close();
                    pIsExist = _hasRows;
                    return !_hasRows;  // 如果是false 则表明数据库没这条记录，可以注册
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Register(string pAccount ,string pPassword,ref bool pIsExist)
        {
            if(CanRegister(pAccount, pPassword, ref pIsExist))
            {
                string _cmdStr = SqlDenfine.CmdInsertTBUserInfoStr(pAccount, pPassword);
                MySqlCommand _cmd = new MySqlCommand(_cmdStr, m_sqlConn);
                try
                {
                    _cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CreateNewCharacterInfo(string pTable,string pAccount,string pName)
        {
            if (!SqlDenfine.IsSafe(pAccount))
                return false;
            string  _cmdStr = string.Format("select * from {0} where account ='{1}';", pTable, pAccount);
            MySqlCommand _cmd = new MySqlCommand(_cmdStr, m_sqlConn);
            try
            {
                MySqlDataReader _dataReader = _cmd.ExecuteReader();
                bool _hasRows = _dataReader.HasRows;
                if (_hasRows == false) //如果没有角色数据就创建
                {
                    _cmdStr = SqlDenfine.CmdInsertNewCharacterInfo(pTable, pAccount, pName, 100, 100, 500);
                    _cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool GetCharacterInfo(string pTable ,string pAccount,ref UserBaseData pData)
        {
            if (!SqlDenfine.IsSafe(pAccount))
                return false;

            string _cmdStr = string.Format("select * from {0} where account ='{1}';", pTable, pAccount);
            MySqlCommand _cmd = new MySqlCommand(_cmdStr, m_sqlConn);
            try
            {
                MySqlDataReader _dataReader = _cmd.ExecuteReader();
                bool _hasRows = _dataReader.HasRows;
                if (!_hasRows)
                    return false;
                pData = new UserBaseData();
                pData.name  = _dataReader.GetString("name");
                pData.hp = _dataReader.GetInt32("hp");
                pData.mp = _dataReader.GetInt32("mp");
                pData.combatPower = _dataReader.GetInt32("combatpower");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DisConnDB()
        {
            if (m_sqlConn == null)
                return;
            m_sqlConn.Close();
        }
    }
}
