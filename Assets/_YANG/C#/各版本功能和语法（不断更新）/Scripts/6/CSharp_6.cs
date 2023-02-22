using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

namespace Yang.CSharp.VariousVersionsGrammar
{
    public class CSharp_6 : MonoBehaviour
    {
        private void Start()
        {
            #region 知识点一：C#6的新增功能和语法有哪些？

            // 1，=>运算符
            // 2，Null传播器（?）
            // 3，字符串内插
            // 4，静态导入
            // 5，异常筛选其
            // 6，nameof运算符

            #endregion

            #region 知识点二：补充——静态导入

            // 用法：在引用命名空间下，在using关键字后面加入static关键字
            // 作用：无需指定类型名称即可访问其静态成员和嵌套类型
            // 好处：节约代码量，可以写出更简洁的代码

            int max = Max(10, 20);
            MyClass2.StaticMethod();
            MyClass2.InternalClass ic = new MyClass2.InternalClass();

            #endregion

            #region 知识点三：补充——异常筛选器

            // 用法：在异常捕获语句块中的catch语句后通过加入when关键字来筛选异常
            //      when(表达式)该表达式返回值必须为bool值，如果是true则执行异常处理，否则不执行
            // 作用：用于筛选异常
            // 好处：帮助我们更准确的排查异常，根据异常类型进行对应的处理

            try
            {
                // 用于检查异常的语句块
            }
            catch (Exception e) when (e.Message.Contains("301"))
            {
            }
            catch (Exception e) when (e.Message.Contains("404"))
            {
            }
            catch (Exception e)
            {
                print(e.Message);
                // ignored
            }

            #endregion

            #region 知识点四：补充——nameof运算符

            // 用法：nameof(变量、类型、成员)，通过该表达式可以将他们的名称转为字符串
            // 作用：可以得到变量、类型、成员等信息的具体字符串名称
            // ReSharper disable once EntityNameCapturedOnly.Local
            int number;
            print(nameof(number)); // number
            print(nameof(List<int>)); // List
            print(nameof(List<int>.Add)); // Add
            print(nameof(UnityEngine.AI)); // AI

            #endregion
        }
    }

    public class MyClass2
    {
        public static void StaticMethod()
        {
        }

        public class InternalClass
        {
        }
    }
}