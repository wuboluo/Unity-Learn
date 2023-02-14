using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Structure
{
    public class TemplateMethodStructure : MonoBehaviour
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