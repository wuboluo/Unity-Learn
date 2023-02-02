public class MessageBase : ObjectBinaryBase
{
    public override int GetBytesNumber()
    {
        throw new System.NotImplementedException();
    }

    public override byte[] Writing()
    {
        throw new System.NotImplementedException();
    }

    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        throw new System.NotImplementedException();
    }

    public virtual int GetID()
    {
        return 0;
    }
}