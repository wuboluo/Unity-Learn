using System;
using UnityEngine;

namespace Yang.CSharp.Notes.AnonymousFunction
{
    internal class Notes_AnonymousFunction
    {
        private static void Main(string[] args)
        {
            // ---------------------------------------- 什么是匿名函数
            // 顾名思义，没有名字的函数
            // 匿名函数的使用主要是配合委托和事件进行使用
            // 脱离委托和事件，是不会使用匿名函数的


            // ---------------------------------------- 基本语法，lambad表达式
            // delegate (参数列表)
            // {
            //     函数逻辑
            // };

            // (参数列表) =>
            // {

            // };


            // 何时使用：
            // 1，函数中传递委托参数时
            // 2，委托或事件赋值时


            // ---------------------------------------- 使用
            // 无参无返
            Action a = delegate { Debug.Log("匿名函数逻辑"); };
            // 有参
            Action<int, string> a2 = delegate(int i, string str) { Debug.Log(i + str); };
            a2(1, "zz");
            // 有返
            Func<string> f = delegate { return ""; };


            // ---------------------------------------- 一般情况下会作为 函数参数传递 or 作为函数返回值
            Test t = new Test();
            // 函数参数
            Action ac = delegate { };

            t.DoSth(50, ac);
            t.DoSth(100, delegate { });

            // 返回值
            Action fun = t.GetFun();
            fun?.Invoke();

            t.GetFun()();


            // ---------------------------------------- 匿名函数的缺点
            // 添加到委托或事件容器中后，不记录，无法单独移除
            Action ac3 = delegate { Debug.Log("匿名函数 1"); };
            ac3 += delegate { Debug.Log("匿名函数 2"); };
            ac3();
            // 因为匿名函数没有名字，所以没有办法指定移除某一个匿名函数
            // 只能清空，但是同时其它的函数也会一并清空
            ac3 = null;


            // ---------------------------------------- 闭包
            // 内层的函数可以引用包含在它外层的函数的变量
            // 及时外层函数的执行已经终止
            // 注意：该变量提供的值并非变量创建时的值，而是在父类函数范围内的最终值
        }
    }

    internal class Test
    {
        public Action action;

        // 作为参数
        public void DoSth(int i, Action fun)
        {
            Debug.Log(i);
            fun?.Invoke();
        }

        // 作为返回值
        public Action GetFun()
        {
            return delegate { Debug.Log("ff"); };
        }
    }
}