using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Structure
{
    public class ConcreteHandler1 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request is >= 0 and < 10)
                Debug.Log($"{GetType().Name} handled request {request}");
            else
                Successor?.HandleRequest(request);
        }
    }
}