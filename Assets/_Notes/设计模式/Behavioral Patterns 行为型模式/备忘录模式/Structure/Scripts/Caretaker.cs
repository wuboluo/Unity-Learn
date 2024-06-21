namespace Yang.DesignPattern.Memento.Structure
{
    // 管家
    // 1，负责保管备忘录
    // 2，从不对备忘录的内容进行检查或操作
    public class Caretaker
    {
        public Memento Memento { get; set; }
    }
}