using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignPattern.Strategy.Example2
{
    public class QuickSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            Debug.Log("————— 快速排序");
        }
    }
}