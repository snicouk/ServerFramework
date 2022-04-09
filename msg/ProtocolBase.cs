
namespace SocketServer
{
    /// <summary>
    /// 协议基类
    ///  协议格式 ：“协议名:协议长度:协议内容”;
    /// </summary>
    public class ProtocolBase
    {
        /// <summary>
        /// 数据编码
        /// </summary>
        /// <returns></returns>
        public virtual byte[] EnCode()
        {
            return null;
        }


        /// <summary>
        /// 数据解码
        /// </summary>
        /// <returns></returns>
        public  virtual ProtocolBase DeCode(byte[] pBuffer,int pStart,int pLen)
        {
            return null;
        }

        /// <summary>
        /// 获取协议名
        /// </summary>
        /// <returns></returns>
        public virtual string GetProtoName()
        {
            return null;
        }

        /// <summary>
        /// 获取协议内容
        /// </summary>
        /// <returns></returns>
        public virtual string GetProtoConent()
        {
            return null;
        }

    }

 


}

