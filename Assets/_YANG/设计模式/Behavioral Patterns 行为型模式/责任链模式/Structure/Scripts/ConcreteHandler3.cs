using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Structure
{
    public class ConcreteHandler3 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request is >= 20 and < 30)
                Debug.Log($"{GetType().Name} handled request {request}");
            else
                Successor?.HandleRequest(request);
        }
    }
}