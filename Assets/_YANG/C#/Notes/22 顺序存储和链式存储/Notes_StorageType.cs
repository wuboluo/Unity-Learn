using UnityEngine;

namespace Yang.CSharp.Notes.StorageType
{
    internal class Notes_StorageType
    {
        private static void Main(string[] args)
        {
            // -------------------------------------------------- 数据结构
            // 数据结构是计算机存储、组织数据的方式（规则）
            // 数据结构是指相互之间存在一种或多种特定关系的数据元素的集合
            // 比如自定义一个类，也可以成为一种数据结构，自己定义的数据组合规则

            // 不要把数据结构想的太复杂，简单的理解：
            // 就是人定义的 存储数据 和 表示数据之间的关系 的规则

            // 常用数据结构（前辈总结和制定的一些经典规则）
            // 数组，栈，队列，链表，树，图，堆，散列表(hashtable)


            // -------------------------------------------------- 线性表
            // 线性表是一种数据结构，是有n个具有相同特性的数据元素的有限序列
            // 比如 数组，ArrayList，stack，queue，链表等

            // 顺序存储和链式存储，是数据结构中的两种存储结构


            // -------------------------------------------------- 顺序存储
            // 数组，stack，queue，list，ArrayList
            // 只是数组，stack，queue的组织规则不同而已

            // 顺序存储：用一组地址连续的存储单元一次存储线性表的各个数据元素


            // -------------------------------------------------- 链式存储
            // 单向链表、双向链表、循环链表
            // 链式存储（链接存储）
            // 用一组任意的存储单元存储线性表中的各个数据元素


            LinkedList<int> link = new LinkedList<int>();
            link.Add(1);
            link.Add(2);
            link.Add(3);

            LinkedNode<int> node = link.head;
            while (node != null)
            {
                Debug.Log(node.value);
                node = node.nextNode;
            }

            link.Remove(3);
            node = link.head;
            while (node != null)
            {
                Debug.Log(node.value);
                node = node.nextNode;
            }
        }
    }

// -------------------------------------------------- 实现简单的单向链表

// 单向链表节点
    internal class LinkedNode<T>
    {
        // 存储下一个元素是谁，相当于“钩子”
        public LinkedNode<T> nextNode;
        public T value;

        public LinkedNode(T value)
        {
            this.value = value;
        }
    }

// 单向链表类，管理节点，添加移除等
    internal class LinkedList<T>
    {
        public LinkedNode<T> head;
        public LinkedNode<T> last;


        public void Add(T value)
        {
            LinkedNode<T> node = new LinkedNode<T>(value);

            if (head == null)
            {
                head = node;
                last = node;
            }
            else
            {
                last.nextNode = node;
                last = node;
            }
        }

        public void Remove(T value)
        {
            if (head == null) return;
            if (head.value.Equals(value))
            {
                head.nextNode = head;

                // 如果 head被移除，发现 head变空，说明只有一个节点，那么 last也要置空
                if (head == null) last = null;
            }


            LinkedNode<T> tempNode = head;
            while (tempNode.nextNode != null)
            {
                if (tempNode.nextNode.value.Equals(value))
                {
                    // 让当前找到的这个元素的上一个节点 指向 自己的下一个节点
                    tempNode.nextNode = tempNode.nextNode.nextNode;
                    break;
                }

                if (tempNode.nextNode.nextNode.Equals(tempNode.nextNode))
                {
                    tempNode.nextNode = tempNode;
                    break;
                }


                tempNode = tempNode.nextNode;
            }
        }
    }

// -------------------------------------------------- 顺序存储和链式存储的优缺点
// 增：链式 计算上 优于顺序 （中间插入时，链式不用像顺序一样去移动位置）
// 删：链式 计算上 优于顺序 （中间删除时，链式不用像顺序一样去移动位置）
// 查：顺序 使用上 优于链式 （数组可以直接通过下标得到元素，链式需要遍历）
// 改：顺序 使用上 优于链式 （数组可以直接通过下标得到元素，链式需要遍历）
}