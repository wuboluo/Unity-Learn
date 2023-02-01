using System;
using System.Collections.Generic;

namespace DataPersistence_Binary
{
    // 如果要使用C#自带的序列化二进制方法
    // 声明类时需要添加 [Serializable] 特性
    
    [Serializable]
    public class Person
    {
        public int age = 18;
        public string name = "XY";

        public int[] ints = { 1, 2, 3, 4, 5 };
        public List<int> list = new() { 1, 2, 3 };

        public Dictionary<int, string> Dic = new()
        {
            { 1, "A" },
            { 2, "B" },
            { 3, "C" }
        };

        public MyStruct ms = new(10, "结构体");
        public MyClass mc = new();
    }

    [Serializable]
    public struct MyStruct
    {
        public int number;
        public string str;

        public MyStruct(int i, string s)
        {
            number = i;
            str = s;
        }
    }

    [Serializable]
    public class MyClass
    {
        public string str = "类";
    }
}