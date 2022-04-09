using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    /// <summary>
    /// 游戏区
    /// </summary>
    public class District
    {
        /// <summary>
        /// 区名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 区ID
        /// </summary>
        public int ID { get; private set; }
        //连接对象
        public List<Connect> connList;
        //最大连接数
        public int maxConnCount = 0;

        public bool isClose;

        public District(DistrictInfo pInfo)
        {
            Name = pInfo.Name;
            ID = pInfo.ID;
            maxConnCount = pInfo.maxCount;
            connList = new List<Connect>();
            //for (int i = 0; i < maxConnCount; i++)
            //    connList.Add(new Connect());
        }
       
        public bool IsFull()
        {
            if (connList.Count >= maxConnCount)
                return true;
            else
            {
                return false;
            }
        }



    }
}
