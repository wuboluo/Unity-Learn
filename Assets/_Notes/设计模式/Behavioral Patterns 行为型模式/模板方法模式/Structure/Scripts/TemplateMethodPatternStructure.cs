using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Structure
{
    public class TemplateMethodPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            AbstractClass aA = new ConcreteClassA();
            aA.TemplateMethod();

            AbstractClass aB = new ConcreteClassB();
            aB.TemplateMethod();
        }
    }
}