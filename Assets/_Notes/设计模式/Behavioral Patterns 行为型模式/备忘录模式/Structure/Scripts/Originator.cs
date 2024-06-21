using UnityEngine;

namespace Yang.DesignPattern.Memento.Structure
{
    // 发起人
    // 1，创建一个包含其内部状态快照的备忘录 —— CreateMemento()
    // 2，使用备忘录恢复其内部状态 —— SetMemento()
    public class Originator
    {
        private string _state;

        public string State
        {
            set
            {
                _state = value;
                Debug.Log("State = " + _state);
            }
        }

        public Memento CreateMemento()
        {
            return new Memento(_state);
        }

        public void SetMemento(Memento memento)
        {
            Debug.Log("Restoring state...");
            State = memento.State;
        }
    }
}