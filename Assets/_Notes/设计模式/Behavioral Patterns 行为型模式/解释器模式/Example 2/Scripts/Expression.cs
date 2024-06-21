namespace Yang.DesignPattern.Interpreter.Example2
{
    public abstract class Expression
    {
        public abstract string Gallons(double quantity);
        public abstract string Quarts(double quantity);
        public abstract string Pints(double quantity);
        public abstract string Cups(double quantity);
        public abstract string Tablespoons(double quantity);
    }
}