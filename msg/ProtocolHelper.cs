using System.Collections;
using System.Collections.Generic;


namespace SocketServer
{
    public class ProtocolHelper
    {
        /// <summary> ����Э�� </summary>
        static public ProtocolBase HeartBeat()
        {
            ProtocolByte _protocol = new ProtocolByte();
            _protocol.AddString(ProtocolDefine.HeartBeat);
            return _protocol;
        }

        /// <summary> ��½Э�� </summary>
        static public ProtocolBase Login(int pResult)
        {
            ProtocolByte _protocol = new ProtocolByte();
            _protocol.AddString(ProtocolDefine.Login);
            _protocol.AddInt(pResult);
            return _protocol;
        }


        /// <summary> �ǳ� </summary>
        static public ProtocolBase Logout(string pID)
        {
            ProtocolByte _protocol = new ProtocolByte();
            _protocol.AddString(ProtocolDefine.Logout);
            _protocol.AddString(pID);
            return _protocol;
        }

        /// <summary> ע��Э�� </summary>
        static public ProtocolBase Regsiter(int pResult)
        {
            ProtocolByte _protocol = new ProtocolByte();
            _protocol.AddString(ProtocolDefine.Regsiter);
            _protocol.AddInt(pResult);
            return _protocol;
        }

    }

}
