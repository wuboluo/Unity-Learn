namespace Yang.DesignPattern.Interpreter.Example1
{
    public class RomanNumeralsExpression : Expression
    {
        protected override string One()
        {
            return "Ⅰ";
        }

        protected override string Two()
        {
            return "Ⅱ";
        }

        protected override string Three()
        {
            return "Ⅲ";
        }

        protected override string Four()
        {
            return "Ⅳ";
        }

        protected override int Multiplier()
        {
            return 10;
        }
    }
}