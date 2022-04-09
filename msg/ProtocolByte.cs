using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SocketServer
{
    /// <summary>
    /// 字节流协议
    /// </summary>
    public sealed class ProtocolByte : ProtocolBase
    {
        public byte[] protoBuffer;

        public override byte[] EnCode()
        {
            return protoBuffer;
        }

        public override ProtocolBase DeCode(byte[] pBuffer, int pStart, int pLen)
        {
            ProtocolByte _proto = new ProtocolByte();
            _proto.protoBuffer = new byte[pLen];
            Array.Copy(pBuffer, pStart, _proto.protoBuffer, 0, pLen);
            return _proto;
        }

        public override string GetProtoName()
        {
            return GetString(0);
        }

        public override string GetProtoConent()
        {
            string _str = "";
            if (protoBuffer == null)
                return string.Empty;

            for (int i = 0; i < protoBuffer.Length; i++)
            {
                int n = (int)protoBuffer[i];
                _str += n.ToString();
            }
            return _str;
        }


        public void AddString(string pStr)
        {
            //int _len = pStr.Length; 
            byte[] _strByte = Encoding.UTF8.GetBytes(pStr);
            byte[] _strLenByte = BitConverter.GetBytes(_strByte.Length);
            if (protoBuffer == null)
            {
                protoBuffer = _strLenByte.Concat(_strByte).ToArray();
            }
            else
            {
                protoBuffer = protoBuffer.Concat(_strLenByte).Concat(_strByte).ToArray();
            }
        }

        public string GetString(int pStart, ref int pEnd)
        {
            if (protoBuffer == null)
                return string.Empty;
            Int32 _len = BitConverter.ToInt32(protoBuffer, pStart);
            if (protoBuffer.Length < pStart + sizeof(Int32) + _len)
                return string.Empty;
            string _str = Encoding.UTF8.GetString(protoBuffer, pStart + sizeof(Int32), _len);
            pEnd = pStart + sizeof(Int32) + _len;
            return _str;
        }

        public string GetString(int pStart)
        {
            int _end = 0;
            return GetString(pStart, ref _end);
        }

        public void AddInt(int pNum)
        {
            byte[] _numBuffer = BitConverter.GetBytes(pNum);
            if (protoBuffer == null)
                protoBuffer = _numBuffer;
            else
                protoBuffer = protoBuffer.Concat(_numBuffer).ToArray();
        }

        public int GetInt(int pStart ,ref int pEnd )
        {
            if(protoBuffer==null)
                return 0;
            if (protoBuffer.Length < pStart + sizeof(Int32))
                return 0;
            pEnd = pStart +sizeof(Int32);
            return BitConverter.ToInt32(protoBuffer,pStart);
        }

        public int GetInt(int pStart)
        {
            int _end = 0;
            return GetInt(pStart, ref _end);
        }

        public void AddFoat(float pNum)
        {
            byte[] _numBuffer = BitConverter.GetBytes(pNum);
            if (protoBuffer == null)
                protoBuffer = _numBuffer;
            else
                protoBuffer = protoBuffer.Concat(_numBuffer).ToArray();
        }

        public float GetFloat(int pStart,ref int pEnd)
        {
            if (protoBuffer == null)
                return 0;
            if (protoBuffer.Length < pStart + sizeof(float))
                return 0;
            pEnd = pStart + sizeof(float);
            return BitConverter.ToInt32(protoBuffer, pStart);
        }

        public float GetFloat(int pStart)
        {
            int _end = 0;
            return  GetFloat(pStart, ref _end);
        }
    }
}


