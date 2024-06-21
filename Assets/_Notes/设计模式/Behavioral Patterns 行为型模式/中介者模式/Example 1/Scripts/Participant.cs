using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example1
{
    public class Participant
    {
        protected Participant(string name)
        {
            Name = name;
        }

        public Chatroom Chatroom { get; set; }
        public string Name { get; }


        public void Send(string to, string message)
        {
            Chatroom.Send(Name, to, message);
        }

        public virtual void Receive(string from, string message)
        {
            Debug.Log($"{from} to {Name}：”{message}“");
        }
    }
}