using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Timers;
using System.Reflection;
using System.Collections.Generic;

namespace SocketServer
{
    public class Server
    {

        public bool isOpen =false;

        private Socket m_socket = null;

        private int m_maxConnCount = 100000; //最大连接数

        private List<Connect> m_connList = new List<Connect>();  //链接对象

        private ProtocolBase m_proto = new ProtocolByte();  //协议类型

        private MsgHandler m_msgHandler = new MsgHandler(); //消息助手

        private System.Timers.Timer m_timer = new System.Timers.Timer(1000); //计时器 ，1秒执行一次


        public void OpenServ(string pHost, int pPort)
        {
            if (isOpen)
                return;

            OpenTimer();
            try
            {
                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress _ipAdr = IPAddress.Parse(pHost);
                IPEndPoint _ipEp = new IPEndPoint(_ipAdr, pPort);
                m_socket.Bind(_ipEp);
                m_socket.Listen(m_maxConnCount);
                m_socket.BeginAccept(OnAccept, null);
                Console.WriteLine("[服务器开启]");
                isOpen = true;
            }
            catch (Exception)
            {
                isOpen = false;
                throw;
            }
        }

        //连接回调
        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket _socket = m_socket.EndAccept(ar);
                if (CanConn() == false)
                {
                    Console.WriteLine("服务器链接已满");
                    _socket.Close();
                }
                else
                {
                    Connect _conn = new Connect();
                    _conn.InitConn(_socket);
                    m_connList.Add(_conn);
                    //异步接收消息
                    _conn.socket.BeginReceive(_conn.readBuffer, _conn.bufferCount, _conn.BufferRemain(), SocketFlags.None, OnReceive, _conn);
                    string _adr = _conn.GetAdress();
                    Console.WriteLine("[新连接] " + _adr);
                }
                //实现循环
                m_socket.BeginAccept(OnAccept, null);
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void OnReceive(IAsyncResult ar)
        {
            Connect _conn = (Connect)ar.AsyncState;
            lock (_conn)
            {
                try
                {
                    int _bufferCount = _conn.socket.EndReceive(ar);

                    if (_bufferCount <= 0)
                    {
                        _conn.DisConn();
                        if (m_connList.Contains(_conn))
                            m_connList.Remove(_conn);
                        return;
                    }

                    _conn.bufferCount += _bufferCount;
                    DeCodeData(_conn);
                    _conn.socket.BeginReceive(_conn.readBuffer, _conn.bufferCount, _conn.BufferRemain(), SocketFlags.None, OnReceive, _conn);
                }
                catch (Exception e)
                {
                    _conn.DisConn();
                    if (m_connList.Contains(_conn))
                        m_connList.Remove(_conn);
                  //  Console.WriteLine(e.Message);
                }
            }


        }

        private void DeCodeData(Connect pConn)
        {
            if (pConn.bufferCount < sizeof(Int32))
            {
                return;
            }

            //消息长度
            Array.Copy(pConn.readBuffer, pConn.lenBuffer, sizeof(Int32));
            pConn.msgLen = BitConverter.ToInt32(pConn.lenBuffer, 0);

            if (pConn.bufferCount < pConn.msgLen + sizeof(Int32))
            {
                return;
            }

            //处理消息
            ProtocolBase _proto = m_proto.DeCode(pConn.readBuffer, sizeof(Int32), pConn.msgLen);
            HandleMsg(pConn, _proto); //处理合法信息
            int _count = pConn.bufferCount - pConn.msgLen - sizeof(Int32);
            Array.Copy(pConn.readBuffer, sizeof(Int32) + pConn.msgLen, pConn.readBuffer, 0, _count);
            pConn.bufferCount = _count;
            if (pConn.bufferCount > 0)
            {
                DeCodeData(pConn);
            }

        }

        private void HandleMsg(Connect pConn, ProtocolBase pProto)
        {
            string _protoName = pProto.GetProtoName();
            string _msg = "Msg" + _protoName;
            //Console.WriteLine(_msg);
            MethodInfo _method = m_msgHandler.GetType().GetMethod(_msg);
            if (_method == null)
            {
                Console.WriteLine("[Server][HandleMsg] 方法为空： " + _msg);
                return;
            }
            Object[] _args = new Object[] { pConn, pProto };
            _method.Invoke(m_msgHandler, _args);
        }

        private void OpenTimer()
        {
            m_timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerHandler);
            m_timer.AutoReset = false; //关闭自动重置
            m_timer.Enabled = true;  //激活计时器
        }

        private void TimerHandler(object sender, ElapsedEventArgs e)
        {
            long _nowTime = Define.GetTimeStamp();
            for (int i = 0; i < m_connList.Count; i++)
            {
                Connect _conn = m_connList[i];
                if (_conn == null)
                    continue;
                if (_conn.state == Connect.State.None)
                    continue;
                if (_conn.laskTickTime < _nowTime - Define.HeartBeatTime)
                {
                    Console.WriteLine("[心跳引用断开连接]");
                    lock (_conn)
                    {
                        _conn.DisConn();
                    }

                    if (m_connList.Contains(_conn))
                        m_connList.Remove(_conn);
                }
            }

            m_timer.Start();
        }

        private bool CanConn()
        {
            if (m_connList == null)
                return false;
            if (m_connList.Count >= m_maxConnCount)
                return false;
            return true;
        }
        public void CloseServ()
        {
            Console.WriteLine("[关闭服务器]");
            isOpen = false;
            if (m_connList == null)
                return;
            for (int i = 0; i < m_connList.Count; i++)
            {
                Connect _conn = m_connList[i];
                if (_conn == null)
                    continue;
                if (_conn.state == Connect.State.None)
                    continue;
                lock (_conn)
                {
                    _conn.DisConn();
                }
            }
            m_connList.Clear();
            SQL.Instance.DisConnDB();
        }


    }
}
