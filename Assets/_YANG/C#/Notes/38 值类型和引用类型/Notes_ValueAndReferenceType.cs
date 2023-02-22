using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_ValueAndReferenceType
    {
        private static void Main(string[] args)
        {
            // ------------------------------------  基础  ------------------------------------

            #region ---------- 常见变量类型

            // 无符号整形
            byte b = 1;
            ushort us = 1;
            uint ui = 1;
            ulong ul = 1;

            // 有符号整形
            sbyte sb = 1;
            short s = 1;
            int i = 1;
            long l = 1;

            // 浮点数
            float f = 1f;
            double d = 1.1;
            decimal de = 1.1m;

            // 特殊类型
            bool bo = true;
            char c = 'A';
            string str = "string";

            // 复杂数据类型
            // enum 枚举
            // 数组（一维，二维，交错）


            // 把以上变量分类为值类型和引用类型
            // 引用类型：string，数组，类
            // 值类型：其他，结构体

            #endregion

            #region ---------- 区别

            // 1，使用上的区别
            // 值类型
            int aa = 10;
            // 引用类型
            int[] arr = { 1, 2, 3, 4, 5 };


            // 声明了一个bb，让其等于之前的aa
            int bb = aa;
            // 声明了一个 arr2，让其等于之前的arr
            int[] arr2 = arr;

            Debug.Log($"aa={aa}, bb={bb}");
            Debug.Log($"arr[0]={arr[0]}, arr2[0]={arr2[0]}");

            bb = 20;
            arr2[0] = 5;

            Debug.Log("----- 修改了 bb和arr2[0] 后：");
            Debug.Log($"aa={aa}, bb={bb}");
            Debug.Log($"arr[0]={arr[0]}, arr2[0]={arr2[0]}");


            // 值类型在相互赋值时，把内容拷贝给了对方，它变我不变
            // 引用类型的相互赋值，是让两者指向同一个值，它变我也变


            // 2，为什么有此区别
            // 值类型和引用类型存储的 内存区域不同，存储方式不同，所以造成使用上的区别

            // 值类型存储在 栈空间（系统分配，自动回收，小而快）
            // 引用类型存储在 堆空间（手动申请和释放，大而慢）


            Debug.Log("----- 将arr2 new后：");
            // new了，就是开了新房间，和之前的不再有关系了，所以arr不会有任何变化
            arr2 = new[] { 99, 3, 2, 1 };
            Debug.Log($"arr[0]={arr[0]}, arr2[0]={arr2[0]}");

            #endregion

            #region ---------- 特殊的引用类型 string

            // string 它变我不变

            string str1 = "123";
            string str2 = "123";

            // 因为 string 是应用类型，按理说，应该是它变我也变
            // 但是 string 非常的特殊，它具备值类型的特征：它变我不变
            str2 = "321";
            Debug.Log("----- string");
            Debug.Log(str1);
            Debug.Log(str2);

            // string在被重新赋值时，会在堆中重新分配空间。由c#底层特殊处理，理解为new了一个新的string
            // string虽然方便，但是缺点是：频繁赋值，会产生内存垃圾，从而触发内存回收，一定程度上影响性能（通过 StringBuilder 来优化）

            #endregion

            #region ---------- 通过断点调试，在监视窗口中查看 内存信息

            string str3 = "123";
            string str4 = str3;

            str4 = "321";
            Debug.Log("----- 断点测试");
            Debug.Log(str3);
            Debug.Log(str4);

            #endregion


            // ------------------------------------  进阶  ------------------------------------


            #region ---------- 问题一：如何判断 值类型和引用类型

            // command+B到类型的内部去查看
            // class 就是引用类型
            // struct 就是值类型

            #endregion

            #region ---------- 问题二：语句块

            // 命名空间
            // ⬇
            // 类，接口，结构体
            // ⬇
            // 函数，属性，索引器，运算符重载等（类，接口，结构体）
            // ⬇
            // 条件分支，循环

            // 上层语句块：类，结构体
            // 中层语句块：函数
            // 底层的语句块：条件分支，循环等

            // 逻辑代码写在哪里？
            // 中底层语句块（函数，条件分支，循环）

            // 变量声明在哪里？
            // 上，中，底层都能声明
            // 上层：成员变量
            // 中，底层：临时变量

            #endregion

            #region ---------- 问题三：变量的生命周期

            // 编程时大部分都是临时变量————在中底层声明的临时变量（函数，条件分支，循环语句块等）
            // 语句块之行结束后，没有被记录的对象将被回收或变成垃圾
            // 值类型：被系统自动回收
            // 引用类型：栈上用于存地址的引用被系统自动回收，堆中具体内容变成垃圾，等待下次GC回收

            // 例如：
            int i22 = 1;
            string str22 = "123";
            // 当此部分代码执行完后
            // 由于 i22 和 str22 存储在栈中，则会由系统自动回收
            // 从而 str22 和 "123" 之间的关联会断掉，导致于存储在堆中 "123" 变为垃圾
            // 但 "123" 并不会立刻回收，而是等待 c#自带的 GC垃圾回收机制的下一次触发，待那时再被回收


            // 要想不被回收或不变垃圾，必须将其记录下来
            // 如何记录？
            // 1，在更高层级记录
            // 2，使用静态全局变量记录

            #endregion

            #region ---------- 问题四：结构体中的值类型和引用类型

            // 结构体本身是值类型
            // 前提：该结构体没有作为其它类的成员
            // 在结构体中的值，栈中存储其具体的内容
            // 在结构体中的引用，堆中存储引用具体的内容

            // 引用类型始终存储在堆中
            // 真正通过结构体使用其中引用类型时只是顺藤摸瓜

            TestStruct ts = new TestStruct();
            // 此 ts 存储在栈中，并且附带存储了（i=0,tc=null）
            // 当 ts.tc 被初始化后，tc=null 变成 tc=引用的地址
            // ts.tc 始终会被存储在堆中

            #endregion

            #region ---------- 问题五：类中的值类型和引用类型

            // 类本身是引用类型
            // 在类中的值，堆中存储具体的值
            // 在类中的引用，堆中存储具体的值（但会另开一块内存存储，详见下面例子）
            // 值类型跟着大哥走，引用类型一根筋（始终有一个地址和一个房间）

            // 例如：
            TestClass tc = new TestClass();
            // 此时，tc的引用存于栈，tc的具体内容存于堆
            // 具体内容包括：
            // 1，b的值，为0
            // 2，str的引用，而 str实际的内容，则在 tc申请的这块内存外部，另外申请一块内存存储。
            //      tc中存储的 str的引用指向这块内存，充分体现引用类型的特点

            // tc申请的内存：
            ////////////////////
            //    b=0         //
            //    str = 引用   //
            ////////////////////

            // 此引用指向 ⬇⬇⬇

            // 实际存放 str的内存
            ////////////////////
            //     "123"      //
            ////////////////////

            #endregion

            #region ---------- 问题六：数组中的存储规则

            // 数组本身是引用类型
            // 值类型数组，堆中房间存具体内容
            // 引用类型数组，堆中房间存地址

            int[] arrInt = new int[5];
            // arrInt的引用存在栈中，堆中则申请一块内存，并被分为5部分分别存一个值，也就是 0（因为数组中的值并未初始化）

            object[] arrObj = new object[5];
            // arrObj的引用存在栈中，堆中同样5部分，但此时这5部分皆是一个地址，当其中一个 object被初始化后，这个地址则会指向另外申请的这块内存

            #endregion

            #region ---------- 问题七：结构体继承接口

            // 利用里氏替换原则，用接口容器装载结构体时，就存在装箱拆箱
            TestStruct2 obj1 = new TestStruct2();
            obj1.Value = 1;
            Debug.Log(obj1.Value);

            TestStruct2 obj2 = obj1;
            obj2.Value = 2;

            Debug.Log(obj1.Value);
            Debug.Log(obj2.Value);

            Debug.Log("----------");

            // 使用里氏替换
            ITest iObj1 = obj1; // 装箱 value=1
            ITest iObj2 = iObj1;
            iObj2.Value = 99;

            Debug.Log(iObj1.Value); // 99
            Debug.Log(iObj2.Value); // 99

            // 注意：值类型智能强转，引用类型才可以使用 as
            TestStruct2 obj3 = (TestStruct2)iObj1; // 拆箱

            #endregion
        }
    }

    public class TestClass
    {
        private int b = 0;
        private string str = "123";

        private TestStruct tc = new();
    }

    public struct TestStruct
    {
        public TestClass tc;
        public int i;
    }

    public interface ITest
    {
        int Value { get; set; }
    }

    public struct TestStruct2 : ITest
    {
        public int Value { get; set; }
    }
}