using System;
using Yang.Net.Serialize;

namespace Yang.Net.S
{
    public class MessageBase : ObjectBinaryBase
    {
        public override int GetBytesNumber()
        {
            throw new NotImplementedException();
        }

        public override byte[] Writing()
        {
            throw new NotImplementedException();
        }

        public override int Reading(byte[] bytes, int beginIndex = 0)
        {
            throw new NotImplementedException();
        }

        public virtual int GetID()
        {
            return 0;
        }
    }
}