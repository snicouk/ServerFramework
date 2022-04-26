

namespace SocketServer
{
    /*
     角色信息处理助手
     */
    public partial class MsgHandler
    {
        public void MsgCreateCharacter(Connect pConn, ProtocolBase pProto)
        {
            ProtocolByte _proto = (ProtocolByte)pProto;
            int _start = 0;
            string _protoName = _proto.GetString(_start, ref _start);
            int _districtId = _proto.GetInt(_start, ref _start);
            string _characterName = _proto.GetString(_start, ref _start);
            string _table = _districtId == 0 ? "huahao_characterinfo" : "yaoming_characterinfo";
            ProtocolByte _returnProto = new ProtocolByte();
            _returnProto.AddString("CreateCharacter");
            if (SQL.Instance.CreateNewCharacterInfo(_table, pConn.user.account, _characterName))
            {
                _returnProto.AddInt(1);
            }
            else
            {
                _returnProto.AddInt(-1);
            }
            pConn.Send(_returnProto);
        }

        public void MsgGetCharacterInfo(Connect pConn, ProtocolBase pProto)
        {
            ProtocolByte _proto = (ProtocolByte)pProto;
            int _start = 0;
            string _protoName = _proto.GetString(_start, ref _start);
            int _districtId = _proto.GetInt(_start, ref _start);
            ProtocolByte _returnProto = new ProtocolByte();
            _returnProto.AddString("GetCharacterInfo");
            UserBaseData _data = null;
            string _table = _districtId == 0 ? "huahao_characterinfo" : "yaoming_characterinfo";
            if (SQL.Instance.GetCharacterInfo(_table, pConn.user.account, ref _data))
            {
                _returnProto.AddInt(1);
                _returnProto.AddString(_data.name);
                _returnProto.AddInt(_data.hp);
                _returnProto.AddInt(_data.mp);
                _returnProto.AddInt(_data.combatPower);
            }
            else
            {
                _returnProto.AddInt(-1);
            }
            pConn.Send(_returnProto);
        }
    }
}
