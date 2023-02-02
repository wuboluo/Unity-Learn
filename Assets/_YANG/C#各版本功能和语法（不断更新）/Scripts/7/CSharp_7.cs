using System;
using System.Collections.Generic;
using UnityEngine;

namespace Yang.CSharpGrammar
{
    public class CSharp_7 : MonoBehaviour
    {
        private string jsonStr;

        private void Start()
        {
            #region 知识点一：C#7对应的Unity版本

            // Unity 2018.3 ——> C# 7
            // Unity 2019.4 ——> C# 7.3

            #endregion

            #region 知识点二：C#7的新增功能和语法有哪些？

            // 1，字面值改进
            // 2，out参数相关 弃元
            // 3，ref返回值
            // 4，本地函数
            // 5，抛出表达式
            // 6，元组
            // 7，模式匹配

            #endregion

            #region 知识点三：字面值改进

            // 在声明数值变量时，为了方便查看数值，可以在数值之间插入_作为分隔符
            // 作用：方便数值变量的阅读
            const int number = 1_234_567_89;
            const int number2 = 0xAB_CD_17;

            #endregion

            #region 知识点四：out变量的快捷使用，弃元

            // 1，直接在参数类型前添加out关键字
            OutMethod(out int x, out int y);
            print(x + y);

            // 当存在参数个数相同的重载时，不能使用var，需要指明具体的类型
            // OutMethod(out var x1, out var y1);

            // 2，使用弃元符号_，忽略不想使用的参数
            OutMethod(out int a, out _);

            #endregion

            #region 知识点五：ref修饰临时变量和返回值

            // 使用ref修饰临时变量和函数返回值，可以让赋值变为引用传递
            // 作用：用于修饰数据对象中的某些值类型变量

            // 1，修饰值类型临时变量
            int i = 10;
            ref int i2 = ref i;
            i2 = 20;
            print(i);

            RefStruct refs = new RefStruct(5, 5);
            ref RefStruct refs2 = ref refs;
            refs2.atk = 10;
            print(refs.atk);

            // 2，获取对象中的参数
            ref int atk = ref refs.atk;
            atk = 99;
            print(refs.atk);

            // 3，函数返回值
            int[] numbers = { 1, 2, 3, 4, 5 };
            ref int resultNumber = ref FindNumber(numbers, 5);
            resultNumber = 50;
            foreach (int t in numbers) print(t);

            #endregion

            #region 知识点六：本地函数

            // 在函数内部声明一个临时函数

            // 注意：
            // 本地函数只能在声明该函数的函数内部调用
            // 本地函数可以使用声明自己的函数中的变量

            // 作用：方便逻辑的封装
            int res = LocalMethod(1);
            print(res);

            #endregion

            #region 知识点七：抛出表达式

            // 抛出表达式，即抛出一个错误
            // throw new 一个异常类

            // 异常基类：Exception
            // throw new Exception("出错了");

            #region C#自带异常类

            // 常见：
            // IndexOutOfRangeException：当一个数组的下标超出范围时运行时引发。
            // NullReferenceException：当一个空对象被引用时运行时引发。
            // ArgumentException：方法的参数是非法的
            // ArgumentNullException： 一个空参数传递给方法，该方法不能接受该参数
            // ArgumentOutOfRangeException： 参数值超出范围
            // SystemException：其他用户可处理的异常的基本类
            // OutOfMemoryException：内存空间不够
            // StackOverflowException 堆栈溢出

            // ArithmeticException：出现算术上溢或者下溢
            // ArrayTypeMismatchException：试图在数组中存储错误类型的对象
            // BadImageFormatException：图形的格式错误
            // DivideByZeroException：除零异常
            // DllNotFoundException：找不到引用的DLL
            // FormatException：参数格式错误
            // InvalidCastException：使用无效的类
            // InvalidOperationException：方法的调用时间错误
            // MethodAccessException：试图访问思友或者受保护的方法
            // MissingMemberException：访问一个无效版本的DLL
            // NotFiniteNumberException：对象不是一个有效的成员
            // NotSupportedException：调用的方法在类中没有实现
            // InvalidOperationException：当对方法的调用对对象的当前状态无效时，由某些方法引发。

            #endregion

            // C# 7.0以后可以在更多的表达式中进行错误抛出
            // 好处：节约代码量

            // 1，空合并操作符后使用throw
            // InitJsonStr(null);
            // 2，三目运算符后使用throw
            // GetStr("1,2,3", 4);
            // 3，=>后使用throw
            Action action = () => throw new Exception("出错了，不许用这个委托");
            // action.Invoke();

            #endregion

            #region 知识点八：元组

            // 多个值的集合，相当于是一种快速构建数据结构类的方式
            // 一般在函数存在多返回值时，可以使用元组来声明返回值

            // 作用：提升开发效率，更方便的处理多返回值等需要用到的多个值时的需求

            // 1，声明（获取值：ItemX作为从左到右依次的函数，X从1开始）
            (int, float) group = (1, 0.5f);
            print(group.Item1);
            (int i, float f) group2 = new(1, 0.5f);
            print(group2.i);

            // 2，判断：数量相同才比较，类型相同才比较，每一个参数的比较是通过==比较，如果都是true，则认为两个元组相等
            print(group == group2);

            // 3-1，元组的应用——函数返回值
            (int, string) groupRes = GroupMethod(1, "hhh");
            // 元组的解构赋值：相当于把多返回值元组拆分到不同的变量中
            (int resI, string resStr) = GroupMethod(1, "hhh");
            // 丢弃参数
            (int resI2, _) = GroupMethod(1, "hhh");

            // 3-2，元组的应用——字典
            Dictionary<(int, float), string> groupDic = new Dictionary<(int, float), string>();
            groupDic.Add((1, 0.5f), "hhh");
            if (groupDic.ContainsKey((1, 0.5f))) print(groupDic[(1, 0.5f)]);

            #endregion

            #region 知识点九：模式匹配

            // 模式匹配是一种语法元素，可以测试一个值是否满足某种条件，并可以从值中提取信息
            // 在C#7中，模式匹配增强了两个现有的语言结构
            // 1-is表达式：可以在右侧写一个模式语法，而不仅仅是一个类型
            // 2-switch语句中的case

            // 1，常量模式（is 常量）：用于判断输入值是否等于某个值
            object o = 1;
            if (o is 1) print("o是1");

            // 2，类型模式（is 类型 变量名、case 类型 变量名）：用于判断输入值类型，如果类型相同，将输入值提取出来
            // 判断某一个变量是否是某一个类型，如果满足会将该变量存入你声明的变量中
            if (o is int k) print(k); // 1

            // 可以用于判断某一个类是父类的哪一个子类是分开处理
            switch (o)
            {
                case int value:
                    print("o是int类型，o的值设置给value，value=" + value);
                    break;
                case float value:
                    print("o是float类型，o的值设置给value，value=" + value);
                    break;
                default: break;
            }

            // 3，var模式
            // 用于将输入值放入于输入值相同类型的新变量中，相当于转存了一次
            const int o1 = 1;
            if (o1 is var v)
            {
                print(v.GetType().ToString() + v); // int 1
            }

            #endregion
        }

        private void OutMethod(out int x, out int y)
        {
            x = 1;
            y = 2;
        }

        private void OutMethod(out float x, out float y)
        {
            x = 1;
            y = 2;
        }

        private static ref int FindNumber(int[] numbers, int targetNumber)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == targetNumber) return ref numbers[i];
            }

            return ref numbers[0];
        }

        private static int LocalMethod(int i)
        {
            return Calc(i);

            int Calc(int k)
            {
                k++;
                return k;
            }
        }

        private void InitJsonStr(string str)
        {
            jsonStr = str ?? throw new ArgumentNullException(nameof(str));
        }

        private string GetStr(string str, int index)
        {
            string[] res = str.Split('.');
            return res.Length > index ? res[index] : throw new IndexOutOfRangeException();
        }

        private (int, string) GroupMethod(int i, string str)
        {
            return (i, str);
        }
    }

    public struct RefStruct
    {
        public int atk;
        private int def;

        public RefStruct(int atk, int def)
        {
            this.atk = atk;
            this.def = def;
        }
    }
}