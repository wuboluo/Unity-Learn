using System.Collections;
using UnityEngine;

namespace Yang.CSharp.Notes
{
// ---------------------------------------- 迭代器是什么
// 迭代器（iterator）有时又称光标（）
// 是程序设计的软件设计模式
// 迭代器模式提供了一个方法顺序访问一个聚合对象中的各个元素
// 而又不暴露其内部的标识

// 在表现上来看
// 是可以在容器对象（例如链表或数组）上遍历访问的接口
// 设计人员无需关心容器对象的内存分配的实现细节
// 可以用 foreach遍历的类，都是实现了迭代器的

// ---------------------------------------- 标准迭代器的实现方法
// 关键接口：IEnumerator，IEnumerable
// 命名空间：using System.Collections
// 可以通过同时继承 IEnumerable 和 IEnumerator实现其中的方法

    internal class CustomList : IEnumerable, IEnumerator
    {
        private readonly int[] list;

        // 从 -1开始的光标，表示：数据得到了哪个位置
        private int position = -1;

        public CustomList()
        {
            list = new[] { 1, 2, 3, 4, 5 };
        }

        // ---------- IEnumerable
        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        // ---------- IEnumerator
        public object Current => list[position];

        public bool MoveNext()
        {
            ++position;
            // 是否溢出，溢出就不合法
            return position < list.Length;
        }

        // reset 是重置光标位置，一般写在获取 IEnumerator对象这个函数中
        // 用第一次重置光标位置
        public void Reset()
        {
            position = -1;
        }
    }

// ---------------------------------------- 用 yield return 语法糖实现迭代器
// yield return 是C#提供给我们的语法糖
// 作用：将复杂逻辑简单化，可以蹭加程序的可读性，从而减少程序代码出错的机会

// 关键接口：IEnumerable
// 让想要通过 foreach遍历的自定义类实现接口中的方法 GetEnumerator即可

    internal class CustomList2 : IEnumerable
    {
        private readonly int[] list;

        public CustomList2()
        {
            list = new[] { 2, 4, 6, 8 };
        }

        public IEnumerator GetEnumerator()
        {
            #region yield return 语法糖

            for (int i = 0; i < list.Length; i++)
                // yield关键字，配合迭代器使用
                // 可以理解为：暂时返回，保留当前的状态
                // 一会还会再回来

                yield return list[i];

            #endregion

            // ----- 或者通过 LINQ简化如下：
            // return list.GetEnumerator();
        }
    }

// ---------------------------------------- 用 yield return 语法糖实现迭代器

    internal class CustomList<T> : IEnumerable
    {
        private readonly T[] list;

        public CustomList(params T[] list)
        {
            this.list = list;
        }

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

    internal class Notes_Iterator
    {
        private static void Main(string[] args)
        {
            CustomList<string> clist = new CustomList<string>("123", "ss", "...");

            // foreach 本质
            // 1，会获取 in后面的这个对象的 IEnumerable，会调用对象其中的 GetEnumerator方法，来获取
            // 2，执行得到这个 IEnumerator对象中的 MoveNext方法
            // 3，只要 MoveNext方法的返回值是 true，就会去得到 Current，然后赋值给 item
            foreach (object item in clist) Debug.Log(item);

            foreach (object item in clist) Debug.Log(item);
        }
    }

// 总结：
// 迭代器就是可以让我们在外部直接通过foreach遍历对象中元素而不需要了解其结构
// 主要的方式：
// 1，传统： 继承两个接口，实现里面的方法
// 2，语法糖：yield return 去返回内容，只需要继承一个接口即可
// 3，语法糖+LINQ：GetEnumerator() 简化为 --> public IEnumerator GetEnumerator() => list.GetEnumerator();
}