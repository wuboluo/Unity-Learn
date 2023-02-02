public class Example_PlayerMessage : MessageBase
{
    public int playerID;
    public Example_PlayerData playerData;

    public override byte[] Writing()
    {
        int index = 0;
        int bytesNumber = GetBytesNumber();
        byte[] bytes = new byte[bytesNumber];

        // 消息ID
        WriteInt(bytes, GetID(), ref index);
        // 消息体长度（实际存的是消息内容的总长度，所以-8）
        WriteInt(bytes, bytesNumber - 8, ref index);
        // 消息内容
        WriteInt(bytes, playerID, ref index);
        WriteData(bytes, playerData, ref index);

        return bytes;
    }

    // 不需要先解析messageID。
    // 因为在调用这个方法之前，会先把messageID解析出来，用来判断使用哪个自定义类来反序列化，之后再解析剩余成员变量等数据
    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        playerID = ReadInt(bytes, ref index);
        playerData = ReadData<Example_PlayerData>(bytes, ref index);

        return index - beginIndex;
    }

    public override int GetBytesNumber()
    {
        return 4 + // message消息ID
               4 + // 消息体的长度
               4 + playerData.GetBytesNumber(); // message成员变量（playerID字节数组的长度 + playerData内容）
    }

    // 自定义的消息ID，多少都可以。主要用于区分是哪个消息类
    public override int GetID()
    {
        return 1;
    }
}