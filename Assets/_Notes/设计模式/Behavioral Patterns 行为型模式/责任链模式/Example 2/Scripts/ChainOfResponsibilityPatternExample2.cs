using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Example2
{
    public class ChainOfResponsibilityPatternExample2 : MonoBehaviour
    {
        private void Start()
        {
            IChain calcAdd = new NumbersAddition();
            IChain calcSub = new NumbersSubtraction();
            IChain calcMul = new NumbersMultiplication();
            IChain calcDiv = new NumbersDivision();

            calcAdd.SetNextChain(calcSub);
            calcSub.SetNextChain(calcMul);
            calcMul.SetNextChain(calcDiv);

            Numbers n1 = new Numbers(3, 5, CalculationType.Addition);
            calcAdd.Calculate(n1);

            Numbers n2 = new Numbers(6, 2, CalculationType.Multiplication);
            calcAdd.Calculate(n2);

            Numbers n3 = new Numbers(12, 3, CalculationType.Subtraction);
            calcAdd.Calculate(n3);
        }
    }
}