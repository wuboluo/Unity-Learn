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
            // 把值类型用引用类型装箱
            // 栈内存会迁移到堆内存

            // 拆箱：
            // 把引用类型存储的值类型取出来
            // 堆内存会迁移到栈内存

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