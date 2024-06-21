using UnityEngine;

namespace Yang.CSharp.Notes.PackingAndUnpacking
{
    // -------------------------------------------------------------------------------- 万物之父
    
    // object
    // 概念：object 是所有类型的基类，它是一个类（引用类型）
    
    // 作用：
    // 1，可以利用里氏替换原则，用 object 容器装所有对象
    // 2，可以用来表示不确定类型，作为函数参数类型

    internal class Father
    {
    }

    internal class Son : Father
    {
        public void Speak()
        {
            Debug.Log("Son");
        }
    }

    internal class Notes_PackingAndUnpacking
    {
        private static void Main(string[] args)
        {
            Father son = new Son();
            if (son is Son s) s.Speak();

            // 引用类型 => 用 is as 来判断和转换
            object o = new Son();
            if (o is Son s2) s2.Speak();

            // 值类型 => 强转
            object o2 = 1f;
            float f1 = (float)o2;

            // string 类型
            object str = "1223";
            string str2 = (string)str;
            string str3 = str as string;
            string str4 = str.ToString();

            object arr = new int[10];
            int[] arr2 = (int[])arr;
            int[] arr3 = arr as int[];


            // -------------------------------------------------------------------------------- 装箱拆箱
            // 发生条件：
            // 用 object存值类型（装箱）
            // 再把 object转为值类型（拆箱）

            // 装箱：
            // 1.在托管堆中分配内存。需要注意的是，由于是将值类型进行引用类型化，因为分配的内存空间除了值类型各个字段所需的内存之外，
            //   还要加上托管堆所有对象都有的两个额外成员（类型对象指针和同步索引块）所需的内存。
            // 2.将值类型的字段复制到新分配的堆内存中。
            // 3.返回对象地址，即对象的引用。值类型成了引用类型。

            // 拆箱：
            // 1.获取已经装箱的对象中各个字段的地址（这个过程即是拆箱）
            // 2.将已经装箱的对象中各个字段的值从托管堆上复制到线程栈的新的值类型对象上去

            // 好处：不确定类型时可以方便参数的存储和传递
            // 坏处：存在内存迁移，增加性能消耗


            // 装箱
            object v = 3;
            // 拆箱
            int value = (int)v;

            TestFun(1, 2, "3", 4f, new Son());
        }

        private static void TestFun(params object[] array)
        {
        }
    }
}