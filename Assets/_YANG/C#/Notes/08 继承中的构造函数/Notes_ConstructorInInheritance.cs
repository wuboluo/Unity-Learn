using UnityEngine;

namespace Yang.CSharp.Notes.ConstructorInInheritance
{
    // -------------------------------------------------------------------------------- 构造函数
    // 构造函数：
    // 实例化对象时调用的函数
    // 主要用来初始化成员变量
    // 每个类都会有一个默认的无参构造函数
    
    // 访问修饰符 类名()
    // {
    // }
    
    // 不写返回值
    // 函数名和类名相同
    // 访问修饰符根据需求而定，一般为 public
    // 构造函数可以重载
    // 可以用 this语法重用代码
    
    // 注意：
    // 有参构造函数会顶掉默认的无参构造函数
    // 如果保留无参构造需重载出来

    internal class Test
    {
        private int testInt;
        private string testStr;

        // 如果有重载的构造函数，不手动写出无参的，将无法通过无参实例对象
        public Test()
        {
        }

        public Test(int i)
        {
            testInt = i;
        }

        // 先执行 this(i) 再执行这个方法的内容
        public Test(int i, string str) : this(i)
        {
            testStr = str;
        }
    }

    // -------------------------------------------------------------------------------- 继承中的构造函数
    // 特点：
    // 当声明一个子类对象时，先执行父类的构造函数，再执行子类的构造函数
    
    // 注意：
    // 1，父类的无参构造，很重要（子类实例化时，默认自动调用父类的无参构造，如果被顶掉会报错）
    // 2，子类可以通过 base 关键字代表父类，调用父类构造函数

    internal class GameObject
    {
        protected GameObject()
        {
            Debug.Log("父类的构造函数");
        }
    }

    internal class Player : GameObject
    {
        public Player()
        {
            Debug.Log("子类的构造函数");
        }
    }

    internal class Father
    {
        //public Father() { }

        protected Father(int i)
        {
            Debug.Log("Father 有参构造");
        }
    }

    internal class Son : Father
    {
        private Son(int i) : base(i)
        {
            Debug.Log("Son 一个参数的构造函数");
        }

        public Son(int i, string str) : this(i)
        {
            Debug.Log("Son 两个参数的构造函数");
        }
    }

    internal class Notes_ConstructorInInheritance
    {
        private static void Main(string[] args)
        {
            Player p = new Player();
            Son s = new Son(1, "123");
        }
    }
}