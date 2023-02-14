using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignPattern.Strategy.Example2
{
    public class ShellSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            Debug.Log("————— 希尔排序");
        }
    }
}