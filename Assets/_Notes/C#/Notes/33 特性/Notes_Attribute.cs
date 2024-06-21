using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Yang.CSharp.Notes
{
// ---------------------------------------- 特性是什么
// 特性是一种允许我们向程序的程序集添加元数据的语言结构
// 它是用于保存程序结构信息的某种特殊类型的类

// 特性提供功能强大的方法以将声明信息与 C#代码（类型，方法，属性等）相关联
// 特性与程序实体关联后，即可在运行时使用反射查询特性信息

// 特性的目的是告诉编译器把程序结构的某组元数据嵌入程序集中
// 它可以放置在几乎所有的声明中（类，变量，函数等等）

// 简单理解：
// 特性本质是个类
// 我们可以利用特性类为元数据添加额外信息
// 比如一个类，成员变量，成员方法等等为他们添加更多的额外信息
// 之后可以通过反射来获取这些额外信息

// ---------------------------------------- 自定义特性
// 继承特性基类
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    internal class MyCustomAttribute : Attribute
    {
        // 特性中的成员，一般根据需求来写
        public string info;

        public MyCustomAttribute(string info)
        {
            this.info = info;
        }

        public void TestFun()
        {
            Console.WriteLine("特性的方法");
        }
    }

// ---------------------------------------- 特性的使用
// 基本语法：
// [特姓名(参数列表)]
// 本质上就是在调用特性类的构造函数
// 写在 类，函数，变量上一行。表示他们具有该特性信息

    [MyCustom("自定义计算类")]
    [MyCustom("自定义计算类")]
    internal class MyClass
    {
        [MyCustom("这是一个成员变量")] public int value;

        //[MyCustom("用于xxx的函数")]
        //public void TestFun([MyCustom("这里是参数")] int a) { }

        public void TestFun(int a)
        {
        }
    }

// ---------------------------------------- 限制自定义特性的适用范围
// 通过为特性类 加特性，限制其使用范围

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
// arg1：AttributeTargets ———— 代表能用在哪些地方
// arg2：AllowMultiple ———— 是否允许多个特性实例用在同一个目标上
// arg3：Inherited ———— 特性是否能被派生类和重写成员继承
    public class MyCustom2Attribute : Attribute
    {
    }

// ---------------------------------------- 系统自带特性————过时特性
// 过时特性 [Obsolete]
// 用于提示用户，使用的方法等成员已经过时，建议使用新方法
// 一般加在函数前的特性

    internal class AttributeTestClass
    {
        // arg1：调用过时方法时，提示的内容
        // arg2：true（调用该方法时会报错） false（调用该方法时会警告）
        [Obsolete("OldSpeak过时啦，请使用Speak", false)]
        public void OldSpeak(string str)
        {
            Console.WriteLine("OldSpeak");
        }

        public void Speak()
        {
            Console.WriteLine("Speak");
        }

        public void SpeakCaller(string str, [CallerFilePath] string fileName = "", [CallerLineNumber] int line = 0, [CallerMemberName] string target = "")
        {
            Console.WriteLine(str);
            Console.WriteLine(fileName);
            Console.WriteLine(line);
            Console.WriteLine(target);
        }
    }

// ---------------------------------------- 系统自带特性————调用者信息特性
// 哪个文件调用
// [CallerFilePath]
// 哪一行调用
// [CallerLineNumber]
// 哪个函数调用
// [CallerMemberName]

// 需要命名空间   using System.Runtime.CompilerServices
// 一般作为函数参数的特性

// ---------------------------------------- 系统自带特性————条件编译特性
// [Conditional]
// 它会和预处理指令 #define配合使用

// 需要引入命名空间   using System.Diagnostics
// 主要可以用在一些调试代码上
// 有时想执行有时不想执行的代码

// ---------------------------------------- 系统自带特性————外部Dll包函数特性
// [DllImport]

// 用来标记非 .NET(C#)的函数，表明该函数在一个外部的 Dll中定义
// 一般用来调用 C或者C++的Dll包写好的方法
// 需要引用命名空间   using System.Runtime.InteropServices

    internal class Notes_Attribute
    {
        [DllImport("Test.dll")]
        public static extern int Add(int a, int b);

        [Conditional("Fun")]
        private static void Fun()
        {
            Console.WriteLine("Fun执行");
        }

        [Obsolete("Obsolete")]
        private static void Main(string[] args)
        {
            #region 特性的使用

            MyClass mc = new MyClass();
            Type t = mc.GetType();

            // 判断是否使用了某个特性
            // arg1：特性的类型   args2：代表是否搜索继承连（属性和事件忽略此参数）
            if (t.IsDefined(typeof(MyCustomAttribute), false)) Console.WriteLine("该类型应用了 MyCustom 特性");

            // 获取 Type元数据中的所有特性
            object[] arr = t.GetCustomAttributes(true);
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] is MyCustomAttribute)
                {
                    MyCustomAttribute myca = arr[i] as MyCustomAttribute;
                    Console.WriteLine(myca.info);
                    myca.TestFun();
                }


            AttributeTestClass tc = new AttributeTestClass();
            tc.OldSpeak("123");
            tc.Speak();
            tc.SpeakCaller("zzzzzzz");


            Fun();

            #endregion
        }
    }

// 总结：
// 特性是用于：为元数据再添加更多的额外信息（变量，方法等）
// 我们可以通过反射获取这些额外的数据，来进行一些特殊的处理
// 自定义特性，继承 Attribute类

// Unity引擎中很多地方都用到了特性来进行一些特殊处理
}