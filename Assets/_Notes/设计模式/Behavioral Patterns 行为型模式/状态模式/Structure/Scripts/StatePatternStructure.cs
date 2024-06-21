using UnityEngine;

namespace Yang.DesignPattern.State.Structure
{
    public class StatePatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Context context = new Context(new ConcreteStateA());

            context.Request();
            context.Request();
            context.Request();
            context.Request();
        }
    }
}