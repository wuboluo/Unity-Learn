using UnityEngine;

namespace Yang.DesignPattern.Strategy.Example2
{
    public class StrategyPatternExample2 : MonoBehaviour
    {
        private void Start()
        {
            SortedList studentRecords = new SortedList();
            
            studentRecords.Add("Samuel");
            studentRecords.Add("Jimmy");
            studentRecords.Add("Sandra");
            studentRecords.Add("Vivek");
            studentRecords.Add("Anna");

            studentRecords.SetSortStrategy(new QuickSort());
            studentRecords.Sort();

            studentRecords.SetSortStrategy(new ShellSort());
            studentRecords.Sort();

            studentRecords.SetSortStrategy(new MergeSort());
            studentRecords.Sort();
        }
    }
}