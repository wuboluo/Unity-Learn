using UnityEngine;

namespace Yang.CSharp.Notes.Static
{
    public class TestClass
    {
        public const float G = 9.8f;
        public const float PI = 3.1415926f;

        public static readonly int staticNumber;
        public readonly int number;

        // 静态构造函数
        static TestClass()
        {
            Debug.Log("静态构造");
            staticNumber = 5;

            // 作用：
            // 在静态构造函数中初始化 静态变量

            // 注意：
            // 1，不能使用访问修饰符
            // 2，不能有参数
            // 3，只会自动调用一次
        }

        public TestClass()
        {
            Debug.Log("构造函数");
            number = 100;
        }

        // 静态方法
        public static float CaleCircle(float r)
        {
            // 成员变量只能将对象实例化出来后，才能点出来使用，不能无中生有
            // 静态方法中不能使用是非静态成员，否则会报错
            // 类似于生命周期，静态函数早于成员变量

            return PI * r * r;
        }

        // 非静态方法
        public void NonStaticFunction()
        {
            // 非静态函数可以使用静态成员
            // 在程序运行时，就已经为静态成员分配了内存，等于进行了实例，所以没有无中生有
            
            Debug.Log("ABC");
        }
    }

    #region 常量和静态变量

    // const（常量）可以理解为特殊的 static
    // 相同点：都可以通过类名点出使用
    // 不同点：
    //      1，const必须初始化，不能修改，static没有这个规则
    //      2，const只能修饰变量，static可以修饰很多

    #endregion

    #region 静态成员

    // 可修饰成员变量，方法，属性等

    // 可直接用类名点出使用
    // 程序中不能无中生有，我们要使用的对象、变量、函数都是要在内存中分配内存空间
    // 之所以要实例化对象，目的就是分配内存空间，在程序中产生一个抽象的对象

    // 特点：
    // 1，程序开始运行时，就会分配内存空间，所以可以直接使用
    // 静态成员和程序同生共死。只要使用了它，直到程序结束时内存空间才会被释放
    // 2，所以一个静态成员就会有自己惟一的一个“内存小房间”，也因此有了惟一性
    // 在任何地方使用都是用的小房间里的内容，改变了它也是改变小房间的内容

    #endregion
}