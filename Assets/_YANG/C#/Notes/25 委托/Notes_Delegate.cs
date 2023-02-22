using System;
using UnityEngine;

namespace Yang.CSharp.Notes.Delegate
{
// ---------------------------------------- 委托是什么
// 委托是函数（方法）的容器
// 可以理解为表示函数（方法）的变量类型
// 用来存储、传递函数（方法）
// 委托的本质是一个类，用来定义函数（方法）的类型（返回值和参数的类型）
// 不同的函数（方法）必须对应和各自“格式”一致的委托

// ---------------------------------------- 基本语法
// delegate
// 访问修饰符 delegate 返回值 委托名(参数列表);

// 可以声明在 namespace 和 class 中
// 更多的写在 namespace

// 简单记忆委托语法：函数声明语法前面加一个 delegate

// ---------------------------------------- 定义自定义委托
// 访问修饰符不写默认为 public，在别的命名空间下也可以使用
// private 在其他命名空间就无法使用
// 一般使用 public

// 这里只是定义了规则（无参无返），并没有使用
    internal delegate void MyFun();

// 同一语句块中，委托规则的声明不能重名
    public delegate int MyFun2(int value);

// 委托是支持泛型的，可以让返回值和参数可变，方便使用
    internal delegate T MyFun3<T, K>(T t, K k);

// ---------------------------------------- 使用定义好的委托
// 委托变量是函数的容器
// 委托常用在：
// 1，作为类的成员
// 2，作为函数的参数
    internal class Test
    {
        public MyFun fun;
        public MyFun2 fun2;

        public void TestFun(MyFun fun, MyFun2 fun2)
        {
            // 先处理别的逻辑，处理完成再执行传入的函数
            int i = 1;
            i += 1;

            //fun.Invoke();
            //fun2.Invoke(i);
            this.fun = fun;
            this.fun2 = fun2;
        }


        // 增
        public void AddFun(MyFun fun, MyFun2 fun2)
        {
            this.fun += fun;
            this.fun2 += fun2;
        }

        // 删
        public void RemoveFun(MyFun fun, MyFun2 fun2)
        {
            // 多减，不会报错，无非就是不处理而已
            this.fun -= fun;
            this.fun2 -= fun2;
        }
    }

    internal class Notes_Delegate
    {
        private static void Main(string[] args)
        {
            // 专门用来装载函数的容器

            MyFun f = new MyFun(Fun);
            f.Invoke();

            MyFun f2 = Fun;
            f2();
            MyFun2 f3 = Fun2;
            f3(1);

            Test t = new Test();
            t.TestFun(Fun, Fun2);


            // ---------------------------------------- 多播委托（委托变量可以存储多个函数）
            MyFun ff = null;
            ff += Fun;
            ff += Fun3;
            ff.Invoke();

            t.AddFun(Fun, Fun2);
            t.fun();
            t.fun2(10);


            // ---------------------------------------- 系统定义好的委托
            // 无参无返回值
            Action action = new Action(Fun);
            action += Fun3;
            action.Invoke();
            // 可以指定返回值类型的 泛型委托
            Func<string> funcStr = Fun4;
            Func<int> funcInt = Fun5;

            // 可以传n个参数，且无返回值（1~16个参数）
            Action<int, string> action2 = Fun6;
            // 可以传n个参数，且有返回值（1~16个参数）
            // <参数，返回值>
            Func<int, int> fun2 = Fun2;
        }

        private static void Fun()
        {
            Debug.Log("fun");
        }

        private static int Fun2(int value)
        {
            Debug.Log(value);
            return value;
        }

        private static void Fun3()
        {
            Debug.Log("zzzz");
        }

        private static string Fun4()
        {
            return "";
        }

        private static int Fun5()
        {
            return 1;
        }

        private static void Fun6(int i, string s)
        {
        }
    }


// 总结：
// 简单理解：委托就是装载、传递函数的容器
// 可以用委托变量，来存储函数或者传递函数
// 系统提供的委托：
// Action：没有返回值，参数提供了 0~16个
// Func：有返回值，参数提供了 0~16个
}