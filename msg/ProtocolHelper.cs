using System.Collections;
using System.Collections.Generic;


namespace SocketServer
{
    public class ProtocolHelper
    {
        /// <summary> 心跳协议 </summary>
        static public ProtocolBase HeartBeat()
        {
            ProtocolByte _protocol = new ProtocolByte();
            _protocol.AddString(ProtocolDefine.HeartBeat);
            return _protocol;
        }

        /// <summary> 登陆协议 </summary>
        static public ProtocolBase Login(int pResult)
        {
            ProtocolByte _protocol = new ProtocolByte();
            _protocol.AddString(ProtocolDefine.Login);
            _protocol.AddInt(pResult);
            return _protocol;
        }


        /// <summary> 登出 </summary>
        static public ProtocolBase Logout(string pID)
        {
            ProtocolByte _protocol = new ProtocolByte();
            _protocol.AddString(ProtocolDefine.Logout);
            _protocol.AddString(pID);
            return _protocol;
        }

        /// <summary> 注册协议 </summary>
        static public ProtocolBase Regsiter(int pResult)
        {
            ProtocolByte _protocol = new ProtocolByte();
            _protocol.AddString(ProtocolDefine.Regsiter);
            _protocol.AddInt(pResult);
            return _protocol;
        }

    }

}
