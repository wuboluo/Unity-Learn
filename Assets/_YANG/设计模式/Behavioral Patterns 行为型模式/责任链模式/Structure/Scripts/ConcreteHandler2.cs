using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Structure
{
    public class ConcreteHandler2 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request is >= 10 and < 20)
                Debug.Log($"{GetType().Name} handled request {request}");
            else
                Successor?.HandleRequest(request);
        }
    }
}