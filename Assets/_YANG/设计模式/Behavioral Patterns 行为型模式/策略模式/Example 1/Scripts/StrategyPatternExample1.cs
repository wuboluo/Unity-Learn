using UnityEngine;

namespace Yang.DesignPattern.Strategy.Example1
{
    public class StrategyPatternExample1 : MonoBehaviour
    {
        private void Start()
        {
            Animal dog = new Dog();
            Animal bird = new Bird();

            Debug.Log("Dog: " + dog.TryToFly());
            Debug.Log("Bird: " + bird.TryToFly());
            
            dog.SetFlyingAbility(new Flyable());
            Debug.Log("Dog: " + dog.TryToFly());
            Debug.Log("Bird: " + bird.TryToFly());
        }
    }
}