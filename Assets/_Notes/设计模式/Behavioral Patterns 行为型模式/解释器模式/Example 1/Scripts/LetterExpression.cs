namespace Yang.DesignPattern.Interpreter.Example1
{
    public class LetterExpression : Expression
    {
        protected override string One()
        {
            return "A";
        }

        protected override string Two()
        {
            return "B";
        }

        protected override string Three()
        {
            return "C";
        }

        protected override string Four()
        {
            return "D";
        }

        protected override int Multiplier()
        {
            return 100;
        }
    }
}