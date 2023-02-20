using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example1
{
    public class Wolf : Carnivore
    {
        public override void Eat(Herbivore herbivore)
        {
            Debug.Log($"{GetType().Name} eats {herbivore.GetType().Name}");
        }
    }
}