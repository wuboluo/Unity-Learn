using System.Collections.Generic;

namespace Yang.DesignPattern.Memento.Example
{
    public class Caretaker
    {
        private readonly List<Memento> _savedArticles = new();

        public void Add(Memento memento)
        {
            _savedArticles.Add(memento);
        }

        public Memento Get(int index)
        {
            return _savedArticles[index];
        }

        public int GetCountOfSavedArticles()
        {
            return _savedArticles.Count;
        }
    }
}