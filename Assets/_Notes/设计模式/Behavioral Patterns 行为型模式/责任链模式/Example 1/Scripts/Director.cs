using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Example1
{
    // 主管
    public class Director : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 100)
                Debug.Log($"{GetType().Name} approved request #{purchase.Number}");
            else
                Successor?.ProcessRequest(purchase);
        }
    }
}