namespace Yang.DesignPattern.Iterator.Structure
{
    public class ConcreteIterator : Iterator
    {
        private ConcreteAggregate _aggregate;
        private int _current;
        private object _ret;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            _aggregate = aggregate;
        }
        
        public override object First()
        {
            return _aggregate[0];
        }

        public override object Next()
        {
            _ret = null;
            if (_current < _aggregate.Count - 1)
            {
                _ret = _aggregate[++_current];
            }

            return _ret;
        }

        public override bool IsDone()
        {
            return _current >= _aggregate.Count;
        }

        public override object CurrentItem()
        {
            return _aggregate[_current];
        }
    }
}