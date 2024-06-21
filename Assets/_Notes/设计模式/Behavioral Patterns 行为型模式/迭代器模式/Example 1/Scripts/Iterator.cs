namespace Yang.DesignPattern.Iterator.Example1
{
    public class Iterator : IAbstractIterator
    {
        private readonly Collection _collection;
        private int _current;

        public Iterator(Collection collection)
        {
            _collection = collection;
        }

        public int Step { get; set; }

        public Item First()
        {
            _current = 0;
            return _collection[_current] as Item;
        }

        public Item Next()
        {
            _current += Step;

            if (!IsDone)
                return _collection[_current] as Item;
            return null;
        }

        public bool IsDone => _current >= _collection.Count;

        public Item CurrentItem => _collection[_current] as Item;
    }
}