using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Structure
{
    public class ProductB1 : AbstractProductB
    {
        public override void Interact(AbstractProductA a)
        {
            Debug.Log($"{GetType().Name} 和 {a.GetType().Name} 相互作用");
        }
    }
}