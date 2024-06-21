namespace Yang.DesignPattern.Interpreter.Structure
{
    public abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }
}