using UnityEngine;

namespace Yang.DesignPattern.Strategy.Structure
{
    public class ConcreteStrategyA : Strategy
    {
        public override void AlgorithmInterface()
        {
            Debug.Log("A");
        }
    }
}