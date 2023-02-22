using System.Collections;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_ArrayList
    {
        private static void Main(string[] args)
        {
            // -------------------------------------------------- ArrayList的本质
            // ArrayList 是C# 封装好的类
            // 本质上是一个 object 类型的数组


            // -------------------------------------------------- 声明，增删查改
            ArrayList array = new ArrayList
            {
                1,
                "123",
                true,
                new()
            };

            ArrayList arr2 = new ArrayList();
            // 批量增加，把另一个list的内容加到后面
            array.AddRange(arr2);
            // 插入
            array.Insert(1, "abc");


            // 移除指定元素，从头找，找到删
            array.Remove(1);
            // 移除指定位置
            array.RemoveAt(0);
            // 清空
            array.Clear();


            // 查找指定位置
            Debug.Log(array[0]);
            // 查看元素是否存在
            bool con = array.Contains("123");
            // 正向查找元素位置，找到返回索引，未找到返回-1
            int index = array.IndexOf(true);
            // 反向查找元素位置，索引仍为从头开始数
            int lastIndex = array.LastIndexOf(true);


            // 修改
            array[0] = 999;


            // -------------------------------------------------- 遍历
            Debug.Log(array.Count); // 长度
            Debug.Log(array.Capacity); // 容量，避免产生过多的垃圾

            for (int i = 0; i < array.Count; i++) Debug.Log(array[i]);

            // 迭代器遍历
            foreach (object v in array) Debug.Log(v);


            // -------------------------------------------------- 装箱拆箱
            // ArrayList 本质上是一个可以自动扩容的 object数组
            // 由于用 object 储存对象，自然存在装箱拆箱
            // 往其中进行值类型存储时就是在装箱，当值类型对象取出来转换使用时，就存在拆箱

            int number = 1;
            array[0] = number; // 装箱
            number = (int)array[0]; // 拆箱
        }
    }
}