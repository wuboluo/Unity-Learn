﻿using System;

namespace Yang.CSharp.Notes.Exercises.StorageType
{
    internal class NotesExercises_StorageType
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("顺序存储和链式存储练习题");

            #region 练习题一

            //请说出常用的数据结构有哪些

            #region 答案

            //数组、栈、队列、链表、树、图、堆、散列表

            #endregion

            #endregion

            #region 练习题二

            //请描述顺序存储和链式存储的区别

            #region 答案

            //顺序存储：内存中用一组地址连续的存储单元存储线性表(连续地址存储)
            //链式存储：内存中用一组任意的存储单元存储线性表(任意地址存储)

            #endregion

            #endregion

            #region 练习题三

            //请尝试自己实现一个双向链表
            //并提供以下方法和属性
            //数据的个数，头节点，尾节点
            //增加数据到链表最后
            //删除指定位置节点

            #endregion

            LinkedList<int> link = new LinkedList<int>();
            link.Add(1);
            link.Add(2);
            link.Add(3);
            link.Add(4);
            //正向遍历
            LinkedNode<int> node = link.Head;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.nextNode;
            }

            //反向遍历
            node = link.Last;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.frontNode;
            }

            link.RemoveAt(0);
            //正向遍历
            node = link.Head;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.nextNode;
            }

            //反向遍历
            node = link.Last;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.frontNode;
            }

            Console.WriteLine("***************");
            link.RemoveAt(1);
            //正向遍历
            node = link.Head;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.nextNode;
            }

            //反向遍历
            node = link.Last;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.frontNode;
            }

            Console.WriteLine("***************");
            link.RemoveAt(1);
            //正向遍历
            node = link.Head;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.nextNode;
            }

            //反向遍历
            node = link.Last;
            while (node != null)
            {
                Console.WriteLine(node.value);
                node = node.frontNode;
            }

            Console.WriteLine("***************");
            link.RemoveAt(1);
        }
    }

    internal class LinkedNode<T>
    {
        public LinkedNode<T> frontNode;
        public LinkedNode<T> nextNode;
        public T value;

        public LinkedNode(T value)
        {
            this.value = value;
        }
    }

    internal class LinkedList<T>
    {
        public int Count { get; private set; }

        public LinkedNode<T> Head { get; private set; }

        public LinkedNode<T> Last { get; private set; }

        public void Add(T value)
        {
            //新加节点
            LinkedNode<T> node = new LinkedNode<T>(value);
            if (Head == null)
            {
                Head = node;
                Last = node;
            }
            else
            {
                //添加到尾部
                Last.nextNode = node;
                //尾部添加的节点 记录自己的上一个节点是谁
                node.frontNode = Last;
                //让当前新加的变成最后一个节点
                Last = node;
            }

            //加了一个节点
            ++Count;
        }

        public void RemoveAt(int index)
        {
            //首先判断 有没有越界
            if (index >= Count || index < 0)
            {
                Console.WriteLine("只有{0}个节点，请输入合法位置", Count);
                return;
            }

            int tempCount = 0;
            LinkedNode<T> tempNode = Head;
            while (true)
            {
                //找到了对应位置的节点 然后移除即可
                if (tempCount == index)
                {
                    //当前要移除的节点的上一个节点 指向自己的下一个节点
                    if (tempNode.frontNode != null) tempNode.frontNode.nextNode = tempNode.nextNode;
                    if (tempNode.nextNode != null) tempNode.nextNode.frontNode = tempNode.frontNode;
                    //如果是头节点 那需要改变头节点的指向
                    if (index == 0)
                        //如果头节点被移除 那头节点就变成了头节点的下一个
                        Head = Head.nextNode;
                    else if (index == Count - 1)
                        //如果尾节点被移除了 那尾结点就变成了尾结点的上一个
                        Last = Last.frontNode;
                    //移除了一个元素 就应该短一截
                    --Count;
                    break;
                }

                //每次循环完过后 要让当前临时节点 等于下一个节点
                tempNode = tempNode.nextNode;
                ++tempCount;
            }
        }
    }
}