using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Example1
{
    public class Boss : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 10000)
                Debug.Log($"{GetType().Name} approved request #{purchase.Number}");
            else
                Successor?.ProcessRequest(purchase);
        }
    }
}