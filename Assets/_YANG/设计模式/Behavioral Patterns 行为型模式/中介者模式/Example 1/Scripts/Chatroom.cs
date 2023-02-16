using System.Collections.Generic;

namespace Yang.DesignPattern.Interpreter.Example1
{
    public class Chatroom : AbstractChatroom
    {
        private readonly Dictionary<string, Participant> _participants = new();

        public override void Register(Participant participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.Name] = participant;
            }

            participant.Chatroom = this;
        }

        public override void Send(string from, string to, string message)
        {
            Participant participant = _participants[to];

            participant?.Receive(from, message);
        }
    }
}