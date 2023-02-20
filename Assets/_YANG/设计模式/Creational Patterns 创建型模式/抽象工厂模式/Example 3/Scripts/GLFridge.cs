using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example3
{
    public class GLFridge : Fridge
    {
        public override void Produce()
        {
            Debug.Log("格力空调");
        }
    }
}