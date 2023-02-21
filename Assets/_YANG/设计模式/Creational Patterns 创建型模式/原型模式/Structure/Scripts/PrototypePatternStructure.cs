using UnityEngine;

namespace Yang.DesignPattern.Prototype.Structure
{
    public class PrototypePatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Prototype p1 = new ConcretePrototype1("I");
            if (p1.Clone() is ConcretePrototype1 c1)
            {
                Debug.Log("Cloned: " + c1.Id);
            }
            
            Prototype p2 = new ConcretePrototype1("II");
            if (p2.Clone() is ConcretePrototype1 c2)
            {
                Debug.Log("Cloned: " + c2.Id);
            }
        }
    }
}