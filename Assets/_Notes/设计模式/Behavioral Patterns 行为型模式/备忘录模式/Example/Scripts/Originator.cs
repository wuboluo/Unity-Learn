namespace Yang.DesignPattern.Memento.Example
{
    public class Originator
    {
        private string Article { get; set; }

        public void Set(string article)
        {
            Article = article;
        }

        /// 将当前的内容储存为一条新的备忘录
        public Memento StoreInMemento()
        {
            return new Memento(Article);
        }

        /// 恢复内容，为传入的备忘录所记录的内容
        public string RestoreFromMemento(Memento memento)
        {
            Article = memento.Article;
            return Article;
        }
    }
}