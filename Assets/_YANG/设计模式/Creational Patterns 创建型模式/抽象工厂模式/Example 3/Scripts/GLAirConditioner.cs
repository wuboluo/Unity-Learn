using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example3
{
    public class GLAirConditioner : AirConditioner
    {
        public override void Produce()
        {
            Debug.Log("美的冰箱");
        }
    }
}