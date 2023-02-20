using UnityEngine;

namespace Yang.DesignPattern.FactoryMethod.Structure
{
    public class FactoryMethodPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Creator[] creators = new Creator[2];

            creators[0] = new ConcreteCreatorA();
            creators[1] = new ConcreteCreatorB();

            foreach (Creator creator in creators)
            {
                Product product = creator.FactoryMethod();
                Debug.Log("Created " + product.GetType().Name);
            }
        }
    }
}