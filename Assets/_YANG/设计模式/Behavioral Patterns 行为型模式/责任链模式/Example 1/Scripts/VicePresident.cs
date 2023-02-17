using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Example1
{
    public class VicePresident : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 1000)
                Debug.Log($"{GetType().Name} approved request #{purchase.Number}");
            else
                Successor?.ProcessRequest(purchase);
        }
    }
}