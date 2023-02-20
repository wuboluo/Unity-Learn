using UnityEngine;

namespace Yang.DesignPattern.Strategy.Structure
{
    public class ConcreteStrategyB : Strategy
    {
        public override void AlgorithmInterface()
        {
            Debug.Log("B");
        }
    }
}