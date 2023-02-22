using UnityEngine;
using System.Collections.Generic;

namespace Yang.CSharp.Notes
{

    class Notes_LinkedList : MonoBehaviour
    {
        private void Start()
        {
            // -------------------------------------------------- LinkedList的本质
            // LinkedList 是C# 封装好的类
            // 本质上是一个可变类型的泛型双向链表

            // -------------------------------------------------- 声明，增取查改
            LinkedList<int> linkedList = new LinkedList<int>();

            // 增
            // 1，在链表尾部添加元素
            linkedList.AddLast(10);
            // 2，在链表头部添加元素
            linkedList.AddFirst(20);
            // 3，在某一个节点之后添加一个节点
            // 要指定节点，需要先得到一个节点
            var n = linkedList.Find(10);
            linkedList.AddAfter(n, 15);
            // 3，在某一个节点之前添加一个节点
            linkedList.AddBefore(n, 5);


            // 删
            // 1，移除头节点
            linkedList.RemoveFirst();
            // 2，移除尾节点
            linkedList.RemoveLast();
            // 3，移除指定节点
            // 无法通过位置直接移除
            linkedList.Remove(20);
            // 4，清空
            linkedList.Clear();


            // 查
            // 1，头节点
            var first = linkedList.First;
            // 2，尾节点
            var last = linkedList.Last;
            // 3，找到指定值的节点
            // 无法直接通过下标获取中间元素，只有遍历找指定位置元素
            var node = linkedList.Find(10);
            // 4，判断是否存在
            var con = linkedList.Contains(10);


            // 改
            // 要先得再改，得到节点再改变其中的值
            linkedList.First.Value = 100;


            // -------------------------------------------------- 遍历
            // 1，foreach
            foreach (var item in linkedList) Debug.Log(item);

            // 2，从头到尾
            var nowNode = linkedList.First;
            while (nowNode.Next != null)
            {
                Debug.Log(nowNode.Value);
                nowNode = nowNode.Next;
            }

            // 3，从尾到头
            nowNode = linkedList.Last;
            while (nowNode != null)
            {
                Debug.Log(nowNode.Value);
                nowNode = nowNode.Previous;
            }
        }
    }
}