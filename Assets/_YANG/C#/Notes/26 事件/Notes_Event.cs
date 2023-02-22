using System;

namespace Yang.CSharp.Notes.Event
{
    internal class Notes_Event
    {
        // ---------------------------------------- 为什么有事件
        // 1，防止外部随意置空委托
        // 2，防止外部随意调用委托
        // 3，时间相当于对委托进行了一次封装，让其更加安全


        private static void Main(string[] args)
        {
            Test t = new Test();

            // 委托可以在外部赋值，事件不能在外部赋值，只能 +=，-=
            t.myFun = null;
            t.myFun = TestFun;

            // t.myEvent = null;
            // t.myEvent = TestFun;
            t.myEvent += TestFun;
            t.myEvent -= TestFun;


            // 委托可以在外部调用，事件不能在外部调用
            t.myFun();
            // t.myEvent();
            // 只能在类的内部封装调用
            t.DoEvent();

            Action a = TestFun;
            // 事件 是不能作为临时变量在函数中使用
            // event Action ea = TestFun;
        }

        private static void TestFun()
        {
        }
        // ---------------------------------------- 事件是什么
        // 事件是基于委托的存在
        // 事件是委托的安全包裹
        // 让委托的使用更具有安全性
        // 事件，是一种特殊的变量类型


        // ---------------------------------------- 事件是什么
        // 访问修饰符 event 委托类型 事件名;

        // 使用：
        // 1，事件是作为 成员变量存在于类中
        // 2，委托怎么用 事件就怎么用

        // 事件相对于委托的区别：
        // 不能在类外部 赋值、调用

        // 注意：它只能作为成员存在于类、接口、结构体

        private class Test
        {
            public Action myFun;

            public Test()
            {
                myFun = TestFun;
                myFun = null;

                myEvent = TestFun;
                myEvent += TestFun;
                myEvent -= TestFun;
                myEvent = null;
            }

            public event Action myEvent;

            public void DoEvent()
            {
                myEvent?.Invoke();
            }

            public void TestFun()
            {
            }
        }
    }

// 总结：
// 事件和委托的使用基本一致，事件就是特殊的委托
// 区别：
// 1，事件不能在外部赋值（不能=）只能+= -=，委托哪里都可以
// 2，事件不能在外部执行，委托哪里都可以
// 3，事件不能作为函数中的临时变量，委托可以
}