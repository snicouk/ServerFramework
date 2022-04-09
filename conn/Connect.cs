using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace SocketServer
{
    /// <summary>
    /// 连接对象
    /// </summary>
    public class Connect
    {
        public enum State
        {
            None,
            Connected,
        }

        public State state;
        public Socket socket;


        //缓存区容量
        public const int Buffer_Size = 1024;
        //缓存区
        public byte[] readBuffer = new byte[Buffer_Size];
        public int bufferCount = 0;
        
        //粘包分包
        public int msgLen = 0;
        public byte[] lenBuffer = new byte[sizeof(Int32)];
        
        public long laskTickTime = long.MinValue;


        public Connect()
        {
            readBuffer = new byte[Buffer_Size];
        }

        public void InitConn(Socket pSocket)
        {
            readBuffer = new byte[Buffer_Size];
            socket = pSocket;
            state = State.Connected;
            bufferCount = 0;
            laskTickTime = Define.GetTimeStamp();
        }

        /// <summary>
        /// 缓存区剩余字节数
        /// </summary>
        /// <returns></returns>
        public  int BufferRemain()
        {
            return Buffer_Size - bufferCount;
        }

        /// <summary>
        /// 获取地址
        /// </summary>
        /// <returns></returns>
        public string GetAdress()
        {
            if (state == State.None || socket == null)
                return string.Empty;

            return socket.RemoteEndPoint.ToString();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConn()
        {
            if (state == State.None)
                return;
            if (socket == null)
                return;
            Console.WriteLine("[断开连接] " +GetAdress());
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            state = State.None;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="pProto"></param>
        public void Send(ProtocolBase pProto)
        {
            if (state == State.None)
                return;
            if (socket == null)
                return;

            byte[] _bytes = pProto.EnCode();
            byte[] _lenBytes = BitConverter.GetBytes(_bytes.Length);
            byte[] _sendBytes = _lenBytes.Concat(_bytes).ToArray();
            try
            {
                socket.BeginSend(_sendBytes, 0, _sendBytes.Length, SocketFlags.None, null, null);
            }
            catch
            {
                Console.WriteLine("[Connect][Send] 发送失败");
            }
        }



    }
}
