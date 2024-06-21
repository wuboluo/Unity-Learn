using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Example2
{
    public class NumbersAddition : IChain
    {
        private IChain _nextChain;

        public void SetNextChain(IChain nextChain)
        {
            _nextChain = nextChain;
        }

        public void Calculate(Numbers numbers)
        {
            if (numbers.CalculationWanted == CalculationType.Addition)
                Debug.Log($"Addition：{numbers.Number1} + {numbers.Number2} = {numbers.Number1 + numbers.Number2}");
            else
                _nextChain?.Calculate(numbers);
        }
    }
}