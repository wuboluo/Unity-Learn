namespace Yang.DesignPattern.ChainOfResponsibility.Example2
{
    public class Numbers
    {
        public Numbers(float number1, float number2, CalculationType calculationWanted)
        {
            Number1 = number1;
            Number2 = number2;
            CalculationWanted = calculationWanted;
        }

        public float Number1 { get; }
        public float Number2 { get; }

        public CalculationType CalculationWanted { get; }
    }
}