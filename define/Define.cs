using System;

namespace SocketServer
{
    public class Define
    {
        public const int HeartBeatTime = 30;

        static public long GetTimeStamp()
        {
            TimeSpan _ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(_ts.TotalSeconds);
        }
    }
}
