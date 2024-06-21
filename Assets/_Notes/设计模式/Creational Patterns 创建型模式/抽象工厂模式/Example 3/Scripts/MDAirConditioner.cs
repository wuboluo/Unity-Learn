using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example3
{
    public class MDAirConditioner : AirConditioner
    {
        public override void Produce()
        {
            Debug.Log("美的冰箱");
        }
    }
}