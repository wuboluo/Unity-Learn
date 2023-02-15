﻿using System.Collections;
using Unity.VisualScripting;

namespace Yang.DesignPattern.Iterator.Structure
{
    public class ConcreteAggregate : Aggregate
    {
        private readonly ArrayList _items = new ArrayList();

        public override Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Count => _items.Count;

        public object this[int index]
        {
            get => _items[index];
            set => _items.Insert(index, value);
        }
    }
}