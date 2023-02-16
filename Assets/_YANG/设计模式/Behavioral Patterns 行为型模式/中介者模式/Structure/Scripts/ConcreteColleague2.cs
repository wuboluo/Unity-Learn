using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Structure
{
    public class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(Mediator mediator) : base(mediator)
        {
        }

        public void Send(string message)
        {
            _mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Debug.Log($"C2 gets message: {message}");
        }
    }
}