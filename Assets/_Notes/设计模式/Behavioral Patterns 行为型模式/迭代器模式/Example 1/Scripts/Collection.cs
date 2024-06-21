using System.Collections;

namespace Yang.DesignPattern.Iterator.Example1
{
    public class Collection : IAbstractCollection
    {
        private readonly ArrayList _items = new();

        public int Count => _items.Count;

        public object this[int index]
        {
            get => _items[index];
            set => _items.Add(value);
        }

        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }
    }
}