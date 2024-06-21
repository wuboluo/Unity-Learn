using UnityEngine;

namespace Yang.DesignPattern.Observer.Example
{
    public class ObserverPatternExample : MonoBehaviour
    {
        private void Start()
        {
            IBM ibm = new IBM(120.00f);
            ibm.Attach(new Investor("AA"));
            ibm.Attach(new Investor("BB"));

            ibm.Price = 120.10f;
            ibm.Notify();

            ibm.Price = 120.75f;
            ibm.Notify();
        }
    }
}