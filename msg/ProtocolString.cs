
using System.Text;

namespace SocketServer
{
    /// <summary>
    /// 字符串协议
    /// </summary>
    public sealed class ProtocolString : ProtocolBase
    {
        /// <summary>
        /// 协议
        /// </summary>
        public string protoStr;

        public override ProtocolBase DeCode(byte[] pBuffer, int pStart, int pLen)
        {
            ProtocolString _proto = new ProtocolString();
            _proto.protoStr = Encoding.UTF8.GetString(pBuffer, pStart, pLen);
            return _proto;
        }

        public override byte[] EnCode()
        {
            byte[] _buffer = Encoding.UTF8.GetBytes(protoStr);
            return _buffer;
        }

        public override string GetProtoName()
        {
            if (protoStr.Length == 0)
                return string.Empty;
            string[] _spl = protoStr.Split(':');
            if(_spl.Length<2)
                return string.Empty;
            return _spl[0];
        }

        public override string GetProtoConent()
        {
            return protoStr;
        }
    }

}
