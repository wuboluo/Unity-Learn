using UnityEngine;

namespace Yang.DesignPattern.Command.Structure
{
    public class CommandPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Receiver receiver = new Receiver();
            Command command = new ConcreteCommand(receiver);
            Invoker invoker = new Invoker();

            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }
    }
}