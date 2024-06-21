using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Structure
{
    public class ChainOfResponsibilityPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Handler h1 = new ConcreteHandler1();
            Handler h2 = new ConcreteHandler2();
            Handler h3 = new ConcreteHandler3();
            h1.SetSuccessor(h2);
            h2.SetSuccessor(h3);

            int[] requests = { 2, 6, 34, 9, 16, 24, 29 };
            foreach (int request in requests) h1.HandleRequest(request);
        }
    }
}