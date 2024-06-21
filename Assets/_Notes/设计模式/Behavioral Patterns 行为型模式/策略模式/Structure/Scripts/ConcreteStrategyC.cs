using UnityEngine;

namespace Yang.DesignPattern.Strategy.Structure
{
    public class ConcreteStrategyC : Strategy
    {
        public override void AlgorithmInterface()
        {
            Debug.Log("C");
        }
    }
}