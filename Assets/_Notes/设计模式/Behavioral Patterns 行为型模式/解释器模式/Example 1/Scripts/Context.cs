namespace Yang.DesignPattern.Interpreter.Example1
{
    public class Context
    {
        public Context(string input)
        {
            Input = input;
        }

        public string Input { get; set; }
        public int Output { get; set; }
    }
}