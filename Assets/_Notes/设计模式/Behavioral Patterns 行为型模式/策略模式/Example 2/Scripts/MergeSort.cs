using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignPattern.Strategy.Example2
{
    public class MergeSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            Debug.Log("————— 归并排序");
        }
    }
}