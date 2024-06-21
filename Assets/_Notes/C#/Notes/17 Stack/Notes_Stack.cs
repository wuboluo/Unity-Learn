using System.Collections;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_Stack
    {
        private static void Main(string[] args)
        {
            // -------------------------------------------------- Stack的本质
            // Stack 是C# 封装好的类
            // 本质上是一个 object 类型的数组，只是封装了特殊的存储规则
            // Stack 是栈存储容器，栈是一种先进后出的数据结构
            // 先存入的数据后获取，后存入的数据先获取
            // 先进后出


            // -------------------------------------------------- 声明，增取查改
            Stack stack = new Stack();


            // 增（压栈）
            stack.Push(1);
            stack.Push("123");
            stack.Push(true);


            // 取（弹栈）
            // 栈中不存在删除的概念，只能取
            object v = stack.Pop();
            Debug.Log(v); // True


            // 查
            // 1，栈无法查看指定位置的元素，只能查看栈顶的内容，并不会取出
            v = stack.Peek();
            Debug.Log(v); // 123
            // 2，查看元素是否存于栈中
            bool con = stack.Contains(1);
            Debug.Log(con); // True


            // 改
            // 栈无法改变其中的元素，只能压弹
            // 只能清空
            stack.Clear();


            // -------------------------------------------------- 遍历
            // 1，长度
            Debug.Log(stack.Count);

            // 2，由于栈不存在索引器，所以无法使用 for 遍历，只能使用 foreach
            //    遍历出来的顺序是，从栈顶到栈底
            foreach (object s in stack) Debug.Log(s);

            // 3，stack 转为 object数组，遍历出来的顺序也是从栈顶到栈底
            object[] arr = stack.ToArray();
            for (int i = 0; i < arr.Length; i++) Debug.Log(arr[i]);

            // 4，循环弹栈
            while (stack.Count > 0)
            {
                object o = stack.Pop();
                Debug.Log(o);
            }
        }
    }
}