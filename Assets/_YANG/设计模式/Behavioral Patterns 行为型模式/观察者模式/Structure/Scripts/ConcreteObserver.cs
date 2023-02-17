using UnityEngine;

namespace Yang.DesignPattern.Observer.Structure
{
    public class ConcreteObserver : Observer
    {
        private readonly string _name;
        private readonly ConcreteSubject _subject;
        private string _observerState;

        public ConcreteObserver(ConcreteSubject subject, string name)
        {
            _subject = subject;
            _name = name;
        }

        public override void Update()
        {
            _observerState = _subject.SubjectState;
            Debug.Log($"Observer {_name}'s new state is {_observerState}");
        }
    }
}