namespace Yang.DesignPattern.Memento.Structure
{
    // 理想情况下，只允许生成备忘录的发起者访问备忘录的内部状态
    public class Memento
    {
        public Memento(string state)
        {
            State = state;
        }

        public string State { get; }
    }
}