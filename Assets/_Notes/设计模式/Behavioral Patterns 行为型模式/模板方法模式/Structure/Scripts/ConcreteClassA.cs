using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Structure
{
    public class ConcreteClassA : AbstractClass
    {
        protected override void PrimitiveOperation1()
        {
            Debug.Log("A 1");
        }

        protected override void PrimitiveOperation2()
        {
            Debug.Log("A 2");
        }
    }
}