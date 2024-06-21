using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example1
{
    public class AbstractFactoryPatternExample1 : MonoBehaviour
    {
        private AnimalWorld _world;

        private void Start()
        {
            ContinentFactory africa = new AfricaFactory();
            _world = new AnimalWorld(africa);
            _world.RunFoodChain();

            ContinentFactory america = new AmericanFactory();
            _world = new AnimalWorld(america);
            _world.RunFoodChain();
        }
    }
}