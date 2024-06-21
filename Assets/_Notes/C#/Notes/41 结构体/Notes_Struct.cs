using UnityEngine;

namespace Yang.CSharp.Notes.Struct
{
    #region ---------- 基本概念

// 结构体 是一种自定义变量类型，类似枚举需要自己定义
// 它是数据和函数的集合
// 在结构体中，可以声明各种变量和方法

// 作用：用来表现存在关系的数据集合，比如用结构体表现学生，动物，人类等

    #endregion

    #region ---------- 基本语法

// 1，结构体一般写在 namespace语句块中
// 2，结构体关键字 struct

// 注意：结构体名字规范，帕斯卡命名法

    #endregion

    #region ---------- 实例

// 表现学生数据的结构体
    internal struct Student
    {
        // 1，在结构体中声明的变量 不能直接初始化

        // 2，变量类型可以是任意类型，包括结构体。但不能是自己的结构体
        // private Student s;   报错：类型为'Student'的字段's'会导致结构布局中的循环

        public int age;
        public bool sex;
        public int number;
        public string name;
        public SS ss;

        // 结构体中的函数：主要用来表现这个数据结构的行为
        // 注意：在结构体中的方法，目前不需要加 static关键字

        #region ---------- 构造函数

        // 1，没有返回值
        // 2，函数名必须和结构体名相同
        // 3，必须有参数（如果有变量存在的话）
        // 4，如果声明了构造函数，那么必须在其中对所有变量数据初始化

        // 构造函数一般是为了在外面方便初始化的

        public Student(int age, bool sex, int number, string name)
        {
            this.age = age;
            this.sex = sex;
            this.number = number;
            this.name = name;
        }

        // 此构造函数意义不大。构造函数重载详见 L14_结构体和类
        public Student(int age)
        {
            this.age = age;
            sex = false;
            number = 0;
            name = "";
        }

        #endregion

        public void Speak()
        {
            // 函数中可以直接使用结构体内部声明的变量
            Debug.Log($"我的名字{name}，我的年龄{age}");
        }
    }

    internal struct SS
    {
    }

    #endregion

    internal class Notes_Struct
    {
        private static void Main(string[] args)
        {
            #region ---------- 使用

            // 如果不将 s1中所有变量初始化，则不能使用此结构体对象（意味着  s1.Speak()  会报错）
            Student s1;
            s1.age = 10;
            s1.sex = true;
            s1.number = 10;
            s1.name = "Yang";
            s1.Speak();

            Student s2 = new Student(20, false, 20, "Yan9");
            s2.Speak();

            #endregion
        }
    }
}