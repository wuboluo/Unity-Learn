using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Structure
{
    public class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(Mediator mediator) : base(mediator)
        {
        }

        public void Send(string message)
        {
            _mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Debug.Log($"C1 gets message: {message}");
        }
    }
}