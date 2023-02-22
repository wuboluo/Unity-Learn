using UnityEngine;
#pragma warning disable CS0414

namespace Yang.CSharp.Notes.Static
{
    // -------------------------------------------------------------------------------- 静态成员
    // static
    // 可修饰成员变量，方法，属性等
    
    // 可直接用类名点出使用
    // 程序中不能无中生有，我们要使用的对象、变量、函数都是要在内存中分配内存空间
    // 之所以要实例化对象，目的就是分配内存空间，在程序中产生一个抽象的对象
    
    // 静态成员的特点：
    // 程序开始运行时，就会分配内存空间，所以可以直接使用
    // 静态成员和程序同生共死。只要使用了它，直到程序结束时内存空间才会被释放
    // 所以一个静态成员就会有自己惟一的一个 “内存小房间”，也因此有了惟一性
    // 在任何地方使用都是用的小房间里的内容，改变了它也是改变小房间的内容

    internal class Test
    {
        public const float G = 9.8f; // const 必须初始化

        public const float PI = 3.1415926f;
        public const int testInt = 100;

        public static float CaleCircle(float r)
        {
            // 成员变量只能将对象实例化出来后，才能点出来使用，不能无中生有
            // 静态方法中不能使用是非静态成员，否则会报错
            // 类似于生命周期，静态函数早于成员变量

            //Debug.Log(testInt);
            Test t = new Test();
            Debug.Log(testInt);

            return PI * r * r;
        }

        public static void TestFun()
        {
            // 非静态函数可以使用静态成员，在程序运行时，就已经为静态成员分配了内存，等于进行了实例，所以没有无中生有
            Debug.Log(PI);
            Debug.Log("123");
        }
    }

    // 静态变量：
    // 1，常用惟一变量的声明
    // 2，方便获取的对象声明
    
    // 静态方法：
    // 常用的惟一的方法声明（比如：相同规则的数学计算相关函数）
    
    // 常量和静态变量
    // const（常量）可以理解为特殊的 static
    // 相同点：
    // 都可以通过类名点出使用
    // 不同点：
    // 1，const必须初始化，不能修改，static没有这个规则
    // 2，const只能修饰变量，static可以修饰很多

    // -------------------------------------------------------------------------------- 静态类
    internal static class TestStatic
    {
        public static int testInt = 0;
        public static int TestIndex { get; set; }

        public static void TestFun()
        {
        }
    }

    // 静态构造函数：
    // 1，不能使用访问修饰符
    // 2，不能有参数
    // 3，只会自动调用一次
    
    // 作用：在静态构造函数中初始化 静态变量
    internal static class StaticClass
    {
        public static readonly int testInt;
        public static readonly int testInt2;

        static StaticClass()
        {
            Debug.Log("静态构造函数");
            testInt = 10;
            testInt2 = 5;
        }
    }

    internal class Test2
    {
        public const int testInt = 30;

        static Test2()
        {
            Debug.Log("静态构造");
        }

        public Test2()
        {
            Debug.Log("构造函数");
        }
    }

    public class Notes_Static : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("--------------------静态成员--------------------");

            Debug.Log(Test.PI);
            Debug.Log(Test.CaleCircle(2));
            Debug.Log(Test.G);

            Test t = new Test();
            Debug.Log(Test.testInt);
            Test.TestFun();

            Debug.Log("--------------------静态类--------------------");

            Debug.Log(StaticClass.testInt);
            Debug.Log(StaticClass.testInt2);


            Debug.Log(Test2.testInt);
            Test2 tt = new Test2();
            Test2 tt2 = new Test2();
        }
    }
}