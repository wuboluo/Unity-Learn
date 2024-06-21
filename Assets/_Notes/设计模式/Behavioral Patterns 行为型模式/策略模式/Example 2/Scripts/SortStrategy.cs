using System.Collections.Generic;

namespace Yang.DesignPattern.Strategy.Example2
{
    public abstract class SortStrategy
    {
        public abstract void Sort(List<string> list);
    }
}