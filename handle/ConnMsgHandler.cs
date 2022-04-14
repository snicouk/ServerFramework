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
            ProtocolByte _proto = pProto as ProtocolByte;
          
            int _start = 0;
            string _protoName = _proto.GetString(_start, ref _start);
            string _account = _proto.GetString(_start, ref _start);
            string _password = _proto.GetString(_start, ref _start);

            ProtocolByte _returnLoginProto = null;
          
            if (SQL.Instance.IsExistUser(_account, _password))
            {        
                _returnLoginProto = (ProtocolByte)ProtocolHelper.Login(1);
            }
            else
            {
                _returnLoginProto = (ProtocolByte)ProtocolHelper.Login(-1);
            }

            pConn.Send(_returnLoginProto);

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
           // Console.WriteLine("[心跳协议]");
            pConn.laskTickTime = Define.GetTimeStamp();
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        public void MsgRegister(Connect pConn, ProtocolBase pProto)
        {
            ProtocolByte _proto = (ProtocolByte)pProto;
            int _start =0;
            string _protoName = _proto.GetString(_start, ref _start);
            string _account = _proto.GetString(_start, ref _start);
            string _password = _proto.GetString(_start, ref _start);

            ProtocolByte _returnProto = null;
            bool _isExsit = false;
            //如果不存在
            if(SQL.Instance.Register(_account, _password,ref _isExsit))
            {
                _returnProto = (ProtocolByte)ProtocolHelper.Regsiter(1);
            }
            else
            {
                if (_isExsit)
                {
                    _returnProto = (ProtocolByte)ProtocolHelper.Regsiter(0);
                }
                else
                {
                    _returnProto = (ProtocolByte)ProtocolHelper.Regsiter(-1);
                }          
            }
            pConn.Send(_returnProto);
        }


    }

}
