using UnityEngine;

namespace Yang.DesignPattern.Iterator.Structure
{
    public class IteratorPatternStructure : MonoBehaviour
    {
        private object _item;

        private void Start()
        {
            ConcreteAggregate aggregate = new ConcreteAggregate
            {
                [0] = "A",
                [1] = "B",
                [2] = "C",
                [3] = "D"
            };

            Iterator iterator = aggregate.CreateIterator();

            _item = iterator.First();
            while (_item != null)
            {
                Debug.Log(_item);
                _item = iterator.Next();
            }
        }
    }
}