using System;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace Yang.CSharp.Notes
{
// -------------------------------------------------- 回顾
// 编译器是一种翻译程序
// 它用于将源语言程序翻译为目标语言程序

// 源语言程序：某种程序设计语言写成的，比如说 C#,C,C++,JAVA等语言编写的程序
// 目标语言程序：二进制数表示的伪机器代码写的程序

// -------------------------------------------------- 程序集
// 程序集是经由编译器编译得到的，供进一步编译执行的那个中间产物
// 在Windows系统中，它一般表现为后缀 .dll(库文件) 或者是 .exe(可执行文件)的格式

// 简单理解：
// 程序集就是我们写的一个代码集合，我们现在写的所有代码最终都会被编译器翻译为一个程序集供别人使用
// 比如一个代码库文件(.dll)或者一个可执行文件(.exe)

// -------------------------------------------------- 元数据
// 元数据就是用来描述数据的数据
// 这个概念不仅仅用于程序上，在别的领域也有元数据

// 简单理解：
// 程序中的类，类中的函数，变量等等信息就是 程序的元数据
// 有关程序以及类型的数据被称为 元数据，他们保存在程序集中

// -------------------------------------------------- 反射的概念
// 程序正在运行时，可以查看其它程序集或者自身的元数据
// 一个运行的程序查看本身或者其他程序的元数据的行为就叫做反射

// 简单理解：
// 在程序运行中，通过反射可以得到其他程序集或者自己程序集代码的各种信息
// 类，函数，变量，对象等等。实例化它们，执行它们，操作它们

// -------------------------------------------------- 反射的作用
// 因为反射可以再程序编译后获得信息，所以它提高了程序的扩展性和灵活性
// 1，程序运行时得到的所有元数据，包括元数据的特性
// 2，程序运行时，实例化对象，操作对象
// 3，程序运行时创建新对象，用这些对象执行任务

    internal class Test
    {
        private readonly int i = 1;
        public int j;
        public string str = "123";

        public Test()
        {
        }

        public Test(int i)
        {
            this.i = i;
        }

        public Test(int i, string str) : this(i)
        {
            this.str = str;
        }

        public void Spead()
        {
            Debug.Log(i);
        }
    }

    internal class Notes_Reflection
    {
        private static void Main(string[] args)
        {
            // -------------------------------------------------- 语法相关

            #region Type

            // Type（类的信息类）
            // 它是反射功能的基础！！
            // 它是访问元数据的主要方式
            // 使用 Type 的成员获取有关类型声明的信息
            // 有关类型的成员（如构造函数，方法，字段，属性和类的事件）


            // -------------------- 获取 Type
            // 1，万物之父 object中的 GetType() 可以获取对象的 type
            int a = 20;
            Type type = a.GetType();
            Debug.Log(type);
            // 2，通过 typeof，传入类名，可以获取对象的 type
            Type type2 = typeof(int);
            Debug.Log(type2);
            // 3，通过类的名字，也可以获取类型（注意：类名必须包含命名空间，不然找不到）
            Type type3 = Type.GetType("System.Int32");
            Debug.Log(type3);


            // -------------------- 得到类的程序集信息
            // 可以通过 Type 得到类型所在程序集信息
            Debug.Log(type.Assembly);


            // -------------------- 获取类中的公共成员
            Debug.Log("---------- 获取类中的公共成员");
            // 1，首先得到 Type
            Type t = typeof(Test);
            // 2，然后得到所有公共成员（需要引用命名空间 using System.Reflection）
            MemberInfo[] infos = t.GetMembers();
            for (int i = 0; i < infos.Length; i++) Debug.Log(infos[i]);


            // -------------------- 获取类的公共构造函数并调用
            Debug.Log("---------- 获取类的公共构造函数并调用");
            // 1，获取所有构造函数
            ConstructorInfo[] ctors = t.GetConstructors();
            for (int i = 0; i < ctors.Length; i++) Debug.Log(ctors[i]);

            // 2，获取其中一个构造函数，并执行
            // 得构造函数传入 Type数组：数组中内容按顺序是参数类型
            // 执行构造函数传入 object数组：表示按顺序传入的参数
            // 2-1，得到无参构造
            ConstructorInfo info = t.GetConstructor(Array.Empty<Type>()); // Array.Empty<Type>() 等同于 new Type[0]
            Test obj = info?.Invoke(null) as Test;
            Debug.Log(obj?.j);
            // 2-2，得到有参构造
            ConstructorInfo info2 = t.GetConstructor(new[] { typeof(int) });
            obj = info2?.Invoke(new object[] { 2 }) as Test;
            Debug.Log(obj?.str);

            ConstructorInfo info3 = t.GetConstructor(new[] { typeof(int), typeof(string) });
            obj = info3?.Invoke(new object[] { 4, "zzz" }) as Test;
            Debug.Log(obj?.str);


            // -------------------- 获取类的公共成员变量
            Debug.Log("---------- 获取类的公共成员变量");
            // 1，获取所有成员变量
            FieldInfo[] fieldInfos = t.GetFields();
            for (int i = 0; i < fieldInfos.Length; i++) Debug.Log(fieldInfos[i]);
            // 2，获取指定名称的公共成员变量
            FieldInfo infoJ = t.GetField("j");
            Debug.Log(infoJ);
            // 3，通过反射获取和设置对象的值
            Test test = new Test { j = 99, str = "cccc" };
            // 3-1，通过反射，获取对象的某个变量的值
            Debug.Log(infoJ.GetValue(test));
            // 3-2，通过反射，设置指定对象的某个变量的值
            infoJ.SetValue(test, -99);
            Debug.Log(infoJ.GetValue(test));


            // -------------------- 获取类的公共成员变量
            Debug.Log("---------- 获取类的公共成员变量");
            // 通过 Type 类中的 GetMethod 方法，得到类中的方法
            // MethodInfo 是方法的反射信息
            Type strType = typeof(string);
            // 1，如果存在方法重载，用 Type 数组表示参数类型
            MethodInfo[] methods = strType.GetMethods();
            for (int i = 0; i < methods.Length; i++)
            {
                // Debug.Log(methods[i]);
            }

            MethodInfo subStr = strType.GetMethod("Substring", new[] { typeof(int), typeof(int) });
            string str = "abcdef";
            // 第一个参数，指的是哪个对象需要执行这个成员方法
            object res = subStr?.Invoke(str, new object[] { 0, 3 });
            Debug.Log(res);


            // -------------------- 其它
            // Type
            // 得枚举：GetEnumNames()
            // 得事件：GetEvents()
            // 得接口：GetInterfaces()
            // 得属性：GetProperties()

            #endregion

            #region Assembly

            // 程序集类
            // 主要从来加载其它程序集，加载后才能用 Type来使用其它程序集中的内容和信息
            // 如果想要使用不是自己程序集中的内容，需要先加载程序集
            // 比如 .dll（库文件）
            // 简单的把库文件看成一种代码仓库，它提供给使用者一些可以直接拿来用的变量、函数或类

            // 三种加载程序集的函数
            // 一般来说加载在同一文件下的其它程序集
            // Assembly assembly = Assembly.Load("程序集名称");

            // 一般用来加载不在同文件下的其它程序集
            // Assembly assembly2 = Assembly.LoadFrom("包含程序集清单的文件的名称或路径");
            // Assembly assembly3 = Assembly.LoadFile("要加载的文件的完全限定路径");

            // 1，先加载一个指定程序集
            Assembly assembly = Assembly.LoadFrom(@"E:\_MyCourses\CSharpTutorial\28_List排序\bin\Debug\net5.0\28_List排序");

            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++) Debug.Log(types[i]);
            // 2，再加载程序集中的一个类对象，之后才能用反射
            Type shopItem = assembly.GetType("_28_List排序.ShopItem");
            MemberInfo[] members = shopItem.GetMembers();
            for (int i = 0; i < members.Length; i++) Debug.Log(members[i]);
            // 通过反射，实例化一个 shopItem对象
            // 首先得到枚举Type，来得到可以传入的参数
            Type myEE = assembly.GetType("_28_List排序.EE");
            FieldInfo ee = myEE.GetField("cc");
            // 直接实例化对象
            object eeObj = Activator.CreateInstance(shopItem, 10, ee.GetValue(null));
            // 得到对象中的方法，进行反射
            MethodInfo cwTest = shopItem.GetMethod("CwTest");
            while (true)
            {
                Thread.Sleep(1000);
                cwTest?.Invoke(eeObj, null);
            }

            #endregion

            #region Activator

            // 用于快速实例化对象的类
            // 用于将 Type 对象快捷实例化为对象
            // 先得到 Type，然后快速实例化一个对象
            Type testType = typeof(Test);
            // 1，无参构造
            Test testObj = Activator.CreateInstance(testType) as Test;
            Debug.Log(testObj?.str);
            // 2，有参构造
            testObj = Activator.CreateInstance(testType, 99) as Test;

            testObj = Activator.CreateInstance(testType, 55, "12223") as Test;

            #endregion
        }
    }

// 总结：

// 反射：
// 在程序运行时，通过反射可以得到其他程序集或者自己的程序集代码的各种信息
// 类，函数，对象等等。实例化、执行、操作它们

// 关键字：
// Type
// Assembly
// Avticator

// Unity引擎的基本工作机制，就是建立在反射的基础上
}