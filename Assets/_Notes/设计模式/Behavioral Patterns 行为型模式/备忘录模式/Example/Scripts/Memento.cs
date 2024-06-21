namespace Yang.DesignPattern.Memento.Example
{
    public class Memento
    {
        public Memento(string article)
        {
            Article = article;
        }

        public string Article { get; }
    }
}