using UnityEngine;

namespace Yang.DesignPattern.Iterator.Example1
{
    public class IteratorPatternExample1 : MonoBehaviour
    {
        private Item _item;

        private void Start()
        {
            Collection collection = new Collection
            {
                [0] = new Item("0"),
                [1] = new Item("1"),
                [2] = new Item("2"),
                [3] = new Item("3"),
                [4] = new Item("4")
            };

            Iterator iterator = collection.CreateIterator();
            iterator.Step = 1;

            for (_item = iterator.First(); !iterator.IsDone; _item = iterator.Next())
            {
                Debug.Log(_item.Name);
            }
        }
    }
}