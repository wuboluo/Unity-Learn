using UnityEngine;

// 问题：（利用模式匹配中的类型模式结合switch语法来制作）
// 写一个Monster类，它派生出Boss和Goblin两个类
// Boss有技能，小怪有攻击
// 随机生成10个怪，装载到数组中
// 遍历这个数组，调用他们的攻击方法，如果是boss就释放技能

namespace Yang.CSharp.VariousVersionsGrammar
{
    public class CSharp_7_Questions : MonoBehaviour
    {
        private void Start()
        {
            Monster[] monsters = new Monster[10];
            for (int i = 0; i < monsters.Length; i++)
            {
                monsters[i] = RandomCreateMonster();
                print(monsters[i].GetType());

                switch (monsters[i])
                {
                    case Boss:
                        Boss.Skill();
                        break;
                    case Goblin:
                        Goblin.Attack();
                        break;
                }
            }
        }

        private static Monster RandomCreateMonster()
        {
            float res = Random.value;

            if (res >= 0.5f) return new Boss();
            return new Goblin();
        }
    }


    public class Monster
    {
    }

    public class Boss : Monster
    {
        public static void Skill()
        {
            Debug.Log("Boss Skill");
        }
    }

    public class Goblin : Monster
    {
        public static void Attack()
        {
            Debug.Log("Goblin Attack");
        }
    }
}