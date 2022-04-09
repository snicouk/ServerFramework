using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    /*
      连接协议
       1. 登陆处理
       2. 登出处理
       3. 心跳处理
       4. 注册处理
     */
    public partial class MsgHandler
    {

        /// <summary>
        /// 登陆消息
        /// </summary>
        public void MsgLogin(Connect pConn, ProtocolBase pProto)
        {
          //  Console.WriteLine("[登陆协议]");
            ProtocolByte _ret = (ProtocolByte)ProtocolHelper.Login(1);
            Console.WriteLine(_ret.GetProtoConent());
            pConn.Send(_ret);
        }
        /// <summary>
        /// 登出消息
        /// </summary>
        public void MsgLogout(Connect pConn, ProtocolBase pProto)
        {

        }

        /// <summary>
        /// 心跳消息
        /// </summary>
        public void MsgHeartBeat(Connect pConn, ProtocolBase pProto)
        {
            Console.WriteLine("[心跳协议]");
            pConn.laskTickTime = Define.GetTimeStamp();
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        public void MsgRegsiter(Connect pConn, ProtocolBase pProto)
        {

        }
    }

}
