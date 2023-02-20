using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example3
{
    public class MDFridge :Fridge
    {
        public override void Produce()
        {
            Debug.Log("美的空调");
        }
    }
}