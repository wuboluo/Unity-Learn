using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Structure
{
    public class MediatorPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            ConcreteMediator concreteMediator = new ConcreteMediator();

            ConcreteColleague1 c1 = new ConcreteColleague1(concreteMediator);
            ConcreteColleague2 c2 = new ConcreteColleague2(concreteMediator);

            concreteMediator.Colleague1 = c1;
            concreteMediator.Colleague2 = c2;

            c1.Send("Hello, I'm C1");
            c2.Send("Hi, C2");
        }
    }
}