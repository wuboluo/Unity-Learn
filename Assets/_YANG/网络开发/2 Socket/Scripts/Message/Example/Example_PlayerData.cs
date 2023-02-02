using System.Text;
using Yang.Net.Serialize;

namespace Yang.Net.S
{
    // 玩家数据类
    // 并不是消息类，只是一个自定义数据结构类
    // 不需要被发送，只是用于序列化和反序列化
    public class Example_PlayerData : ObjectBinaryBase
    {
        public string playerName;
        public int playerAtk;
        public int playerDef;

        public override int GetBytesNumber()
        {
            // atk + def + nameLength + name
            return sizeof(int) + sizeof(int) + sizeof(int) + Encoding.UTF8.GetBytes(playerName).Length;
        }

        // 所有变量序列化
        public override byte[] Writing()
        {
            int index = 0;
            byte[] bytes = new byte[GetBytesNumber()];

            WriteString(bytes, playerName, ref index);
            WriteInt(bytes, playerAtk, ref index);
            WriteInt(bytes, playerDef, ref index);

            return bytes;
        }

        // 所有变量反序列化
        public override int Reading(byte[] bytes, int beginIndex = 0)
        {
            int index = beginIndex;
            playerName = ReadString(bytes, ref index);
            playerAtk = ReadInt(bytes, ref index);
            playerDef = ReadInt(bytes, ref index);

            return index - beginIndex;
        }
    }
}