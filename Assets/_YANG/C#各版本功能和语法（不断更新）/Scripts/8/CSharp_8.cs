using System;
using System.IO;
using UnityEngine;

public class CSharp_8 : MonoBehaviour
{
    private void Start()
    {
        #region 知识点一：C#8对应的Unity版本

        // Unity 2020.3 —— C# 8
        // 但是部分新内容还不在该版本中被支持
        // 以下是相对实用的知识点

        #endregion

        #region 知识点二：C#8的新增功能和语法有哪些？

        // 1，Using声明
        // 2，静态本地函数
        // 3，Null合并赋值
        // 4，解构函数Deconstruct
        // 5，模式匹配增强功能

        #endregion

        #region 知识点三：静态本地函数

        // 静态本地函数就是在本地函数前加static
        // 作用：
        // 让本地函数不能使用访问封闭范围内（也就是上层方法中）的任何变量
        // 让本地函数只能处理逻辑，避免让它通过直接改变上层变量来处理逻辑，从而导致逻辑混乱
        var res = LocalStaticMethod(1);
        print(res);

        #endregion

        #region 知识点四：Using 声明

        // using(对象声明)
        // {
        // 使用对象，语句块结束后，对象将被释放掉
        // 当语句块结束，会自动帮我们调用对象的Dispose()，让其自动销毁
        // using一般是配合内存占用比较大/有读写操作时使用
        // }

        // 例如：
        using (var sw = new StreamWriter("文件路径"))
        {
            // 对sw进行逻辑处理，sw只能在这里面使用
            sw.Write("");
            sw.Flush();
            sw.Close();

            // 在结束时，自动释放 
        }

        // 简化，省略括号
        // 在当前函数（上层语句块，甚至在if语句块中）执行完毕后，自动释放该对象
        using var md = new MyDisposeClass();

        // 注意：
        // 在使用using语法时，声明的对象必须继承System.IDisposable接口，否则报错
        // 因为必须具备Dispose方法

        #endregion

        #region 知识点五：Null 合并赋值

        // ??
        // 左边值为空的时候，使用右边的值
        string str = null;
        var str2 = str ?? "123";
        print(str2); // 123

        // ??=
        // 左边值为空时，把右边值赋值给变量
        str ??= "456";
        print(str); // 456

        str ??= "789";
        print(str); // 456
        // 注意：不为空的变量不会改变

        #endregion

        #region 知识点六：解构函数 Deconstruct

        // 可以在自定义类中声明解构函数（固定写法）
        // 可以存在多个，但参数个数不能相同
        // public void Deconstruct(out 类型 变量名, out 类型 变量名...)

        // 可以对该对象利用元组的将其具体的变量值，解构出来
        // 相当于把不同的成员变量拆分到不同的临时变量中

        var p = new Person();
        var (n, e) = p;
        print(n); // Yang
        print(e); // 383049891@qq.com

        #endregion
    }

    private int LocalStaticMethod(int i)
    {
        var b = false;

        static int Calc(int k, ref bool j)
        {
            j = true;
            return ++k;
        }

        return Calc(i, ref b);
    }
}

public class MyDisposeClass : IDisposable
{
    public void Dispose()
    {
    }
}

public class Person
{
    public readonly string email = "383049891@qq.com";
    public readonly string name = "Yang";
    public readonly int id = 1;

    // 固定方法名，参数需被out修饰
    public void Deconstruct(out string _name, out string _email)
    {
        // 内部值赋给参数
        _name = name;
        _email = email;
    }

    // 简化写法
    public void Deconstruct(out string _name, out string _email, out int _id) => (_name, _email, _id) = (name, email, id);
}