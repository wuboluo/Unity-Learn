using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example2
{
    public class Broker1 : Colleague
    {
        public Broker1(IMediator mediator) : base(mediator)
        {
            Debug.Log("Broker1 和证卷交易所签约了");
        }
    }
}