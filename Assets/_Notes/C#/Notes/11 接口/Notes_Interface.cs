using System;

namespace Yang.CSharp.Notes.Interface
{
    // 接口是行为的抽象规范
    // 它也是一种自定义类型
    // interface
    
    // 接口是抽象行为的 “基类”
    
    // 声明规范：
    // 1，不包含成员变量
    // 2，只包含方法，属性，索引器，事件
    // 3，成员不能被实现（C#8.0以上实现了）
    // 4，成员可以不用写访问修饰符，不能是私有的
    // 5，接口不能继承类，但是可以继承另一个接口
    
    // 使用规范：
    // 1，类可以继承多个接口
    // 2，类继承接口后，必须实现接口中所有成员
    
    // 特点：
    // 1，它和类声明类似
    // 2，接口是用来被继承的
    // 3，接口不能被实例化，但是可以作为容器储存对象

    internal interface IFly
    {
        string Name { get; set; }
        int this[int index] { get; set; }
        void Fly();
        event Action DoSth;
    }

    internal class Animal
    {
    }

    // 1，类可以继承一个类，n个接口
    // 2，继承了接口后，必须实现其中的内容，并且必须是 public 的
    internal class Person : Animal, IFly
    {
        // 3，实现的接口函数，可以加 virtual 再在子类重写
        public virtual void Fly()
        {
        }

        public string Name { get; set; }

        public int this[int index]
        {
            get => 0;
            set { }
        }

        public event Action DoSth;
    }

// 接口继承接口，不需要实现。待类继承接口后，类自己去实现所有内容
    internal interface IWalk
    {
        void Walk();
    }

    internal interface IMove : IFly, IWalk
    {
    }

    internal class Test : IMove
    {
        public int this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string Name
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public event Action DoSth;

        public void Fly()
        {
            throw new NotImplementedException();
        }

        public void Walk()
        {
            throw new NotImplementedException();
        }
    }

// 显示实现接口
// 当一个类继承两个接口，但是接口中存在着同名方法时
// 注意：显示实现接口时，不能写访问修饰符

    internal interface IAtk
    {
        void Atk();
    }

    internal interface ISuperAtk
    {
        void Atk();
    }

    internal class PLayer : IAtk, ISuperAtk
    {
        // 显示实现接口，就是用接口名.方法名
        void IAtk.Atk()
        {
        }

        void ISuperAtk.Atk()
        {
        }

        public void Atk()
        {
        }
    }

    internal class Notes_Interface
    {
        private static void Main(string[] args)
        {
            // 4，接口也遵循里氏替换原则
            IFly f = new Person();

            IMove im = new Test();
            IWalk iw = new Test();

            PLayer p = new PLayer();
            (p as IAtk).Atk();
            (p as ISuperAtk).Atk();

            p.Atk();
        }
    }


// 总结：
// 继承类：对象间的继承，包括特征行为等
// 继承接口：是行为间的继承，继承接口的行为规范，按照规范去实现内容
// 由于接口也是遵循里氏替换原则，所以可以用接口容器装对象
// 那么就可以实现，装载各种毫无关系但却有相同行为的对象

// 注意：
// 1，接口只包含 成员方法，属性，索引器，事件。都不实现，都没有访问修饰符
// 2，一个类可以继承多个接口，但是只能继承一个类
// 3，接口可以继承接口，相当于进行行为合并，待子类继承时再去实现具体的行为
// 4，接口可以被显示实现，主要用于实现不同接口中的同名函数的不同表现
// 5，实现的接口方法可以加 virtual，之后再被子类去实现
}