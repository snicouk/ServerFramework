
using System;

namespace SocketServer
{
    public class ServMain
    {
        static private void Main(string[] str)
        {
            Server _serv = new Server();
            // Console.WriteLine("输入host");
            string _host = "127.0.0.1";// Console.ReadLine();
                                       // Console.WriteLine("输入port");
            int _port = 8888;//int.Parse(Console.ReadLine().Trim());
            SQL sql = new SQL();
            _serv.OpenServ(_host, _port); 
            while (true)
            {
                string _str = Console.ReadLine();
                if (_str.ToLower().Equals("quit"))
                {
                    _serv.CloseServ();
                    break;
                }
            }
        }
    }
}
