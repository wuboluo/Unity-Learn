using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Yang.CSharp.Notes.Exercises
{
    internal class NotesExercises_MyLinkedList
    {
        private static void Main(string[] args)
        {
            Debug.Log("LinkedList练习题");
            
            #region 练习题
            //使用 LinkedList，向其中加入10个随机整形变量
            //正向遍历一次打印出信息
            //反向遍历一次打印出信息
            
            LinkedList<int> linkedList = new LinkedList<int>();
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                linkedList.AddLast(r.Next(1, 101));
            }
            
            LinkedListNode<int> nowNode = linkedList.First;
            while (nowNode != null)
            {
                Debug.Log(nowNode.Value);
                nowNode = nowNode.Next;
            }
            
            Debug.Log("********************");
            nowNode = linkedList.Last;
            while (nowNode != null)
            {
                Debug.Log(nowNode.Value);
                nowNode = nowNode.Previous;
            }
            
            #endregion
        }
    }
}