using UnityEngine;

namespace Yang.CSharp.Notes
{
    // 多态按字面的意思就是多种状态
    // 让继承同一父类的子类们，再执行相同方法时有不同的表现（状态）
    // 主要目的：同一父类的对象，执行相同行为（方法）有不同的表现
    // 解决的问题：让同一个对象有惟一行为的特征

    internal class Father
    {
        public void SpeakName()
        {
            Debug.Log("Father的方法");
        }
    }

    internal class Son : Father
    {
        public new void SpeakName()
        {
            Debug.Log("Son的方法");
        }
    }

    internal class GameObject
    {
        private string name;

        protected GameObject(string name)
        {
            this.name = name;
        }

        // 虚函数 可以被子类重写
        public virtual void Atk()
        {
            Debug.Log("Atk...");
        }
    }

    internal class Player : GameObject
    {
        public Player(string name) : base(name)
        {
        }

        public override void Atk()
        {
            // base：父类
            // 可以通过 base 保留父类的行为
            // base.Atk();
            Debug.Log("Player Atk");
        }
    }

    internal class Monster : GameObject
    {
        public Monster(string name) : base(name)
        {
        }

        public override void Atk()
        {
            Debug.Log("Monster Atk");
        }
    }

    internal class Notes_PolymorphismVOB
    {
        private static void Main(string[] args)
        {
            Father f = new Son();
            f.SpeakName();
            ((Son)f).SpeakName();


            GameObject p = new Player("XY");
            p.Atk();

            GameObject m = new Monster("Mo");
            m.Atk();
        }
    }
}