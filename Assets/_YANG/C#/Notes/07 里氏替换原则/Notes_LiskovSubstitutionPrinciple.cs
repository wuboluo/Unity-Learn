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

            foreach (GameObject o in objects)
            {
                switch (o)
                {
                    case Player: break;
                    case Monster: break;
                    case Boss: break;
                }
            }
            
            // () 和 as 的区别：
            
            // () 显式转换
            // 这种转换方式称为显式转换（或强制转换），它告诉编译器你希望把一个对象从一种类型转换为另一种类型。
            // 如果转换是有效的，操作将成功，
            // 如果转换无效，将引发一个`InvalidCastException`。
            
            // as 安全类型转换
            // as 关键字用于安全类型转换。
            // 如果类型转换是合法的，它会执行转换，如果不合法，它不会抛出异常，而是返回`null`。
            // 这意味着使用`as`操作符时，你需要处理可能得到`null`的情况。

            // 性能：
            // 当你确定转换一定会成功时，显式转换会略微快一点，因为它不需要进行为返回 null 所必须的额外检查。
            // 然而，如果你使用显式转换并遇到了无效的转换，捕获异常的成本会远远超过 as 操作符的成本。

            // 应用场景：
            // 如果你确定一个对象一定是目标类型，或者你希望在转换失败时得到一个异常，那么应该使用显式转换。
            // 如果你不确定对象是否能够转换，并且你希望在转换失败时简单地得到 null ，那么使用 as 操作符更合适。
        }
    }
}