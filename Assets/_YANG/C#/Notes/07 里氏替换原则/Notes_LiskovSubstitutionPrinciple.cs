using UnityEngine;

namespace Yang.CSharp.Notes.LiskovSubstitutionPrinciple
{
// 里氏替换原则
// 任何父类出现的地方，子类都可以替代
// 重点：语法表现（父类容器装子类对象，因为子类对象包含了父类的所有内容）
// 作用：方便进行对象存储和管理

    internal class GameObject
    {
    }

    internal class Player : GameObject
    {
        public void PlayerAtk()
        {
            Debug.Log("Player Atk");
        }
    }

    internal class Monster : GameObject
    {
        public void MonsterAtk()
        {
            Debug.Log("Monster Atk");
        }
    }

    internal class Boss : GameObject
    {
        public void BossAtk()
        {
            Debug.Log("Boss Atk");
        }
    }

    internal class Notes_LiskovSubstitutionPrinciple
    {
        private static void Main(string[] args)
        {
            // 里氏替换原则：用父类容器装载子类对象
            GameObject player = new Player();
            GameObject monster = new Monster();
            GameObject boss = new Boss();

            GameObject[] objects = { new Player(), new Monster(), new Boss() };


            // is as

            // is：判断一个对象是否是指定类对象
            // 返回值 bool 是为真，不是为假

            // as：将一个对象转换为指定类
            // 返回值 指定类型对象
            // 成功返回执行类型对象，失败返回 null

            // 基本语法
            // 类对象 is 类名    该语句块，会有一个 bool 返回值（true false）
            // 类对象 as 类名    该语句块，会有一个对象返回值（对象 null）


            // if (player is Player p) p.PlayerAtk();
            // (player as Player).PlayerAtk();
            ((Player)player).PlayerAtk();

            foreach (var o in objects)
            {
                switch (o)
                {
                    case Player _: break;
                    case Monster _: break;
                    case Boss _: break;
                }
            }
        }
    }
}