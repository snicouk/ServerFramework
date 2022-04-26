using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class User
    {
        public string account;
        public UserBaseData baseData;

        public User(string pAccount)
        {
            account = pAccount;
        }

        public void Dispose()
        {
            account = string.Empty;
            baseData = null;
        }
    }
}
