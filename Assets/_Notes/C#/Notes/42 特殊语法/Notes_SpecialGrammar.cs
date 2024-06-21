using System;
using System.Collections.Generic;
using UnityEngine;

namespace Yang.CSharp.Notes.SpecialGrammar
{
    internal class Notes_SpecialGrammar
    {
        private static void Main(string[] args)
        {
            #region ---------- var 隐式类型

            // var 是一种特殊的变量类型
            // 它可以用来表示任意类型的变量

            // 注意：
            // 1，var不能作为类的成员，智能用于临时变量声明时，一般卸载函数语句块内
            // 2，var必须初始化

            int i = 1;
            string s = "123";
            int[] array = new[] { 1, 2, 3, 4 };

            #endregion

            #region ---------- 设置对象初始值

            // 声明对象时，可以直接通过写大括号的形式初始化公共成员变量和属性
            // 若构造函数为无参，()默认不写，但依旧会执行其构造函数
            Person p = new Person { sex = true, Name = "Yang" };
            Person p2 = new Person(200) { Name = "Shao" };

            #endregion

            #region ---------- 设置集合初始值

            // 声明集合对象时，也可以通过大括号直接初始化内部属性

            int[] arr2 = new[] { 1, 2, 3, 4, 5 };
            List<int> listInt = new List<int> { 1, 2, 3, 4, 5 };

            List<Person> listPerson = new List<Person>
            {
                new(),
                new(100),
                new(200)
                {
                    sex = false, Name = "Shao"
                }
            };

            Dictionary<int, string> dic = new Dictionary<int, string>
            {
                { 1, "100" },
                { 2, "200" }
            };

            #endregion

            #region ---------- 匿名类型

            // 只能存在临时变量，不能存放函数相关
            var v = new { age = 10, money = 100, name = "Yang" };
            Debug.Log(v.age); // 10

            #endregion

            #region ----------  可空类型

            // 实际上为一个特殊的泛型结构体

            // 1，值类型本身不允许被设置为空
            // 2，声明时，在值类型后面加一个 ? 可以赋值为空
            int? c = null;
            // 3，判断是否为空
            if (c.HasValue)
            {
                Debug.Log(c);
                Debug.Log(c.Value);
            }

            // 4，安全的获取可空类型值
            int? value = null;
            // 4-1，如果为空，默认返回值类型的默认值
            Debug.Log(value.GetValueOrDefault()); // 0
            // 4-2，也可以指定一个默认值（并不会给 value 本身赋值）
            Debug.Log(value.GetValueOrDefault(100)); // 100
            Debug.Log(value); // 依旧为空


            // 函数前使用 ?.
            // 相当于一种语法糖，能够帮助我们自动判断 o是否为空。若为空，则不会执行 ToString，也不会报错
            object o = null;
            string result = o?.ToString();

            int[] arrInt = null;
            Debug.Log(arrInt?[0]);

            Action action = null;
            action?.Invoke();

            #endregion

            #region ---------- 空合并操作符

            // 空合并操作符 ??
            // 左边值 ?? 右边值
            // 如果左边值为空，则返回右边值，否则返回左边值
            // 只要是可以为空的类型都可以使用

            int? vv = null;
            int ii = vv ?? 100;
            // 相当于：
            // int ii = vv == null ? 100 : vv.Value;
            Debug.Log(ii);

            #endregion

            #region ---------- 内插字符串

            string name = "Yang";
            int age = 18;

            string str = $"{name}的年龄是{age}";
            Debug.Log(str);

            #endregion
        }
    }

    internal class Person
    {
        private int money;
        public bool sex;

        public Person()
        {
        }

        public Person(int money)
        {
            this.money = money;
        }

        public string Name { get; set; }
    }
}