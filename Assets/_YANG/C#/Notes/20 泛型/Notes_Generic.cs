using System;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_Generic
    {
        private static void Main(string[] args)
        {
            Debug.Log("Hello World!");


            TestClass<int> t = new TestClass<int>();
            t.value = 1;

            TestClass<string> t2 = new TestClass<string>();
            t2.value = "123";

            TestClass2<int, float, string, bool, TestClass<int>> t3 = new TestClass2<int, float, string, bool, TestClass<int>>();

            Test2 tt = new Test2();
            tt.TestFun<string>("123123");

            Test2<int> tt2 = new Test2<int>();
            tt2.TestFun(10);
            tt2.TestFun("aaa");
        }
        // 泛型实现了类型参数化，达到代码重用的目的
        // 通过类型参数化来实现同一份代码上操作多种类型

        // 泛型相当于类型占位符
        // 定义类或方法时使用代替符代表变量类型
        // 当真正使用类或方法时再具体指定类型


        // -------------------------------------------------- 泛型分类
        // 泛型类和泛型接口：
        // class 类名<泛型占位字母>
        // interface 接口名<泛型占位字母>

        // 泛型函数：
        // 函数名<泛型占位字母>(参数列表)

        // 注意：泛型占位字母可以有多个，用逗号分开


        // -------------------------------------------------- 泛型类和接口
        private class TestClass<T>
        {
            public T value;
        }

        private class TestClass2<T1, T2, K, L, KEY>
        {
            public K k;
            public KEY key;
            public L l;
            public T1 t1;
            public T2 t2;
        }

        private interface ITestInterface<T>
        {
            T Value { get; set; }
        }

        private class Test : ITestInterface<int>
        {
            public int Value
            {
                get => throw new NotImplementedException();
                set => throw new NotImplementedException();
            }
        }


        // -------------------------------------------------- 泛型方法
        // 1，普通类中的泛型方法
        private class Test2
        {
            public void TestFun<T>(T value)
            {
                Debug.Log(value);
            }

            public void TestFun<T>()
            {
                // 用泛型类型，在里面做一些逻辑处理
                T t = default(T);
            }

            public T TestFun<T>(string str)
            {
                return default;
            }

            public void TestFun<T, K, M>(T t, K k, M m)
            {
            }
        }


        // 2，泛型类中的泛型方法
        private class Test2<T>
        {
            public T value;

            // 这个不算泛型方法，因为 T 是泛型类声明的时候就指定了的，不能再动态的变化
            public void TestFun(T t)
            {
            }

            public void TestFun<K>(K k)
            {
            }
        }


        // -------------------------------------------------- 泛型的作用
        // 1，不同对象的相同逻辑处理可以选择泛型
        // 2，使用泛型可以一定程度避免装箱拆箱
        // 举例：优化 ArrayList
        private class ArrayList<T>
        {
            private T[] array;

            public void Add(T value)
            {
            }

            public void Remove(T value)
            {
            }
        }


        // 总结：
        // 1，声明泛型时，他只是一个类型的占位符
        // 2，泛型真正起作用的时候，是在使用它的时候
        // 3，泛型占位符字母可以有n个，用逗号隔开
        // 4，泛型占位字母一般是大写字母
        // 5，不确定泛型类型时，获取默认值，可以使用 default
        // 6，看到<>包括的字母，肯定是泛型
    }
}