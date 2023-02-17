using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example2
{
    public class Broker2 : Colleague
    {
        public Broker2(IMediator mediator) : base(mediator)
        {
            Debug.Log("Broker2 和证卷交易所签约了");
        }
    }
}