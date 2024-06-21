using System.Collections;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_Queue
    {
        private static void Main(string[] args)
        {
            // -------------------------------------------------- Queue的本质
            // Queue 是C# 封装好的类
            // 本质上是一个 object 类型的数组，只是封装了特殊的存储规则
            // Queue 是队列存储容器，队列是一种先进先出的数据结构
            // 先存入的数据先获取，后存入的数据后获取
            // 先进先出


            // -------------------------------------------------- 声明，增取查改
            Queue queue = new Queue();


            // 增
            queue.Enqueue(1);
            queue.Enqueue("123");
            queue.Enqueue(true);


            // 取
            // 队列中不存在删除的概念，只能取
            object v = queue.Dequeue();
            Debug.Log(v); // 1


            // 查
            // 1，查看队列头部元素，但不会移除
            v = queue.Peek();
            Debug.Log(v); // 123
            // 2，查看元素是否存于队列中
            bool con = queue.Contains(1);
            Debug.Log(con); // False


            // 改
            // 队列无法改变其中的元素，只能进出
            // 只能清空
            queue.Clear();


            // -------------------------------------------------- 遍历
            // 同 Stack，见 17_Stack
        }
    }
}