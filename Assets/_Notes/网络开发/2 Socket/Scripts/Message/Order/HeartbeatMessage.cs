namespace Yang.Net.S
{
    public class HeartbeatMessage : MessageBase
    {
        public override int GetID()
        {
            return 999;
        }

        public override int GetBytesNumber()
        {
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
            return 0;
        }
    }
}