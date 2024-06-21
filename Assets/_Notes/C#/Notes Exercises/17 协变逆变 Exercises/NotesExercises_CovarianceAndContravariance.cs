using System;

namespace Yang.CSharp.Notes.Exercises
{
    //协变
    internal delegate T TestOut<out T>();

    //逆变
    internal delegate void TestIn<in T>(T v);

    internal class Father
    {
    }

    internal class Son : Father
    {
    }

    internal class NotesExercises_CovarianceAndContravariance
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("协变逆变练习题");

            #region 练习题一

            //请描述协变逆变有什么作用

            //是用来修饰泛型替代符的  泛型委托和泛型接口中
            //1.out修饰的泛型类型 只能作为返回值类型 协变
            //  in修饰的泛型类型 只能作为参数类型   逆变

            //2.遵循里氏替换原则 用out和in修饰的泛型委托 如果类型是父子关系 那么可以相互装载
            // 协变： 父类泛型委托容器可以装 子类泛型委托容器 
            // 逆变： 子类泛型委托容器可以装 父类泛型委托容器

            #endregion

            #region 练习题二

            //通过代码说明协变和逆变的作用

            //协变 代码
            TestOut<Son> ts = () => { return new Son(); };

            TestOut<Father> tf = ts;

            Father f = tf();

            //逆变 代码
            TestIn<Father> tif = value => { };

            TestIn<Son> tis = tif;

            tis(new Son());

            #endregion
        }
    }
}