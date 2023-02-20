using UnityEngine;

namespace Yang.DesignPattern.Command.Structure
{
    public class Receiver
    {
        public void Action()
        {
            Debug.Log("Call Receiver.Action()");
        }
    }
}