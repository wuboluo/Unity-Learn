using UnityEngine;

namespace Yang.DesignPattern.State.Structure
{
    public class Context
    {
        private State _state;

        public Context(State state)
        {
            State = state;
        }

        public State State
        {
            set
            {
                _state = value;
                Debug.Log($"State: {_state.GetType().Name}");
            }
        }

        public void Request()
        {
            _state.Handle(this);
        }
    }
}