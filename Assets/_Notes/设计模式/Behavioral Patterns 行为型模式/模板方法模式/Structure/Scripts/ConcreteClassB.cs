using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Structure
{
    public class ConcreteClassB : AbstractClass
    {
        protected override void PrimitiveOperation1()
        {
            Debug.Log("B 1");
        }

        protected override void PrimitiveOperation2()
        {
            Debug.Log("B 2");
        }
    }
}