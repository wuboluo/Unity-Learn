namespace Yang.Net.S
{
    public class QuitMessage : MessageBase
    {
        public override int GetID()
        {
            return -1;
        }

        public override int GetBytesNumber()
        {
            // 消息ID + 消息长度
            return 4 + 4;
        }

        public override byte[] Writing()
        {
            int index = 0;
            byte[] bytes = new byte[GetBytesNumber()];
            WriteInt(bytes, GetID(), ref index);
            WriteInt(bytes, 0, ref index);

            return bytes;
        }

        public override int Reading(byte[] bytes, int beginIndex = 0)
        {
            // 没有任何内容需要反序列化
            return 0;
        }
    }
}