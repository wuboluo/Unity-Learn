using UnityEngine;

namespace Yang.CSharp.Notes
{
    public class Notes_PartialClass : MonoBehaviour
    {
        private void Start()
        {
            Person p = new Person();
            Person.Body body = new Person.Body();

            Student s = new Student();
            Student.Speak();
        }
        
        // 内部类：在一个类中在声明一个类
        // 特点：使用者要用包裹者点出自己
        // 作用：亲密关系的表现
        // 注意：访问修饰符作用很大
        private class Person
        {
            public int age;
            public Body body;
            public int name;

            public class Body
            {
                private Arm leftArm;
                private Arm rightArm;

                private class Arm
                {
                }
            }
        }

        // 分部类：把一个类分成几步声明
        // partial 
        // 作用：分部描述一个类，增加程序拓展性
        // 注意：
        // 1，分部类可以写在多个脚本文件中
        // 2，分部类的访问修饰符要一致
        // 3，分部类不能有重复成员

        private partial class Student
        {
            public string name;
            public bool sex;
        }

        private partial class Student
        {
            public int number;

            public static void Speak()
            {
            }
        }
    }
}