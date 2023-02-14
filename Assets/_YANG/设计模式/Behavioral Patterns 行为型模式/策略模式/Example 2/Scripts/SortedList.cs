using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignPattern.Strategy.Example2
{
    public class SortedList
    {
        private SortStrategy _sortStrategy;
        private readonly List<string> _list = new();

        // 定义一系列算法，封装每个算法，并使它们可以互换
        public void SetSortStrategy(SortStrategy sortStrategy)
        {
            _sortStrategy = sortStrategy;
        }

        public void Add(string name)
        {
            _list.Add(name);
        }

        public void Sort()
        {
            _sortStrategy.Sort(_list);
            foreach (var name in _list)
            {
                Debug.Log(name);
            }
        }
    }
}