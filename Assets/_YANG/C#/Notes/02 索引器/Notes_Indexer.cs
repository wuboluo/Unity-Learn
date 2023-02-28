using System;
using UnityEngine;

namespace Yang.CSharp.Notes.Indexer
{
    public class Notes_Indexer : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("索引器");

            // 索引器使用
            Person p = new Person { [0] = new("YANG", 18) };
            Debug.Log(p[0].Name);
            Debug.Log(p["age"]);

            // [^index] 默认代表数组从后数，第几位，index>=1

            int[] arr = { 1, 2, 3, 4, 5 };
            Debug.Log(arr[new Index(1, true)]);
        }
    }

    // 让对象可以像数组一样通过索引访问其中元素，使程序看起来更直观，更容易编写

    // 访问修饰符 返回值 this[参数类型 参数名，参数类型 参数名]
    // {
    //      get;
    //      set;
    // }
    internal class Person
    {
        private readonly int age;
        private readonly string name;
        private Person[] friends;

        public string Name => name;

        public Person()
        {
            
        }

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        // 索引器可以重载
        public string this[string str]
        {
            get
            {
                if (str == "name")
                {
                    return name;
                }

                return str == "age" ? age.ToString() : "";
            }
        }


        public Person this[int index]
        {
            // 索引器中可以添加逻辑
            get
            {
                if (friends == null || friends.Length < index - 1) return null;
                return friends[index];
            }
            set
            {
                if (friends == null)
                {
                    friends = new[] { value };
                }
                else if (index > friends.Length - 1)
                {
                    // friends[friends.Length - 1] = value;
                    friends[^1] = value;
                }

                friends[index] = value;
            }
        }
    }

    // 总结
    // 可以以'[]'的形式访问访问自定义类中的元素，自定义规则，访问时和数组一样
    // 比较适用于 在类中有数组变量时使用，可以方便的访问和进行逻辑处理

    // 结构体同样支持索引器
}