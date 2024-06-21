using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Structure
{
    public abstract class AbstractClass
    {
        protected abstract void PrimitiveOperation1();
        protected abstract void PrimitiveOperation2();

        public void TemplateMethod()
        {
            PrimitiveOperation1();
            PrimitiveOperation2();
            Debug.Log("");
        }
    }
}