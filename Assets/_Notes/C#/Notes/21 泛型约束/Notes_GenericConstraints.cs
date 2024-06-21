using System;

namespace Yang.CSharp.Notes
{
// -------------------------------------------------- 让泛型的类型有一定的限制
// where
// where T : 约束类型

// 1，值类型     where T : struct
// 2，引用类型     where T : class
// 3，存在无参公共构造函数的非抽象类型      where T : new()
// 4，某个类本身或者其派生类     where T : 类名
// 5，某个接口的派生类型     where T : 接口名
// 6，另一个泛型类型本身或者派生类     where T : 另一个泛型字母

// -------------------- 值类型约束
    internal class Test1<T> where T : struct
    {
        public T value;

        public void Fun<K>(K k) where K : struct
        {
        }
    }

// -------------------- 引用类型约束
    internal class Test2<T> where T : class
    {
        public T value;

        public void Fun<K>(K k) where K : class
        {
        }
    }

// -------------------- 存在无参公共构造函数
    internal class Test3<T> where T : new()
    {
        public T value;

        public void Fun<K>(K k) where K : new()
        {
        }
    }

    internal abstract class Test1
    {
    }

    internal class Test2
    {
    }

    internal class Test3
    {
        public Test3(int a)
        {
        }
    }

// -------------------- 某个类本身或者其派生类
    internal class Test4<T> where T : Test1
    {
        public T value;

        public void Fun<K>(K k) where K : Test1
        {
        }
    }

    internal class Test11 : Test1
    {
    }

// -------------------- 某个接口的派生类型
    internal class Test5<T> where T : IFly
    {
        public T value;

        public void Fun<K>(K k) where K : IFly
        {
        }
    }

    internal interface IFly
    {
    }

    internal class Test55 : IFly
    {
    }

// -------------------- 另一个泛型类型本身或者派生类
    internal class Test6<T, U> where T : U
    {
        public T value;

        public void Fun<K, V>(K k) where K : V
        {
        }
    }

// -------------------------------------------------- 约束的组合使用
// 一般 new() 被组合时，放到最后一位
    internal class Test7<T> where T : class, new()
    {
    }

// -------------------------------------------------- 多个泛型有约束
    internal class Test8<T, K> where T : class, new() where K : struct
    {
    }

// 总结：

// 泛型约束：让类型有一定限制
// class
// struct
// new()
// 类名
// 接口名
// 另一个泛型字母

// 注意：
// 1，可以组合使用，new()出现放在最后
// 2，多个泛型约束用 where 连接

    internal class Notes_GenericConstraints
    {
        private static void Main(string[] args)
        {
            Test1<int> t1 = new Test1<int>();
            t1.Fun(1.3f);

            Test2<Random> t2 = new Test2<Random>();
            t2.Fun(new object());

            // Test1：必须是具有无参公共构造函数的非抽象类型。结构体无参构造函数不会被顶掉，所以结构体也可以被传入
            Test3<Test2> t3 = new Test3<Test2>();
            Test3<int> t33 = new Test3<int>();

            Test4<Test11> t4 = new Test4<Test11>();

            Test5<IFly> t5 = new Test5<IFly>();
            t5.value = new Test55();

            Test6<Test55, IFly> t6 = new Test6<Test55, IFly>();
        }
    }
}