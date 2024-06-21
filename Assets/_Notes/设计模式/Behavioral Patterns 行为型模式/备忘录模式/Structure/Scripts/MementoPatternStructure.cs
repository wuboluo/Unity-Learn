using UnityEngine;

namespace Yang.DesignPattern.Memento.Structure
{
    // 在不违反封装的情况下，捕获并外部化一个对象的内部状态，以便该对象以后可以恢复到该状态
    public class MementoPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Originator originator = new Originator
            {
                State = "On"
            };

            Caretaker caretaker = new Caretaker
            {
                Memento = originator.CreateMemento()
            };

            originator.State = "Off";
            originator.SetMemento(caretaker.Memento);
        }
    }
}