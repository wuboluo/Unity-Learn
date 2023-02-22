using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example2
{
    public abstract class EnemyShip
    {
        // 引擎
        protected IEnemyShipEngine Engine;
        public string Name;

        // 武器
        protected IEnemyShipWeapon Weapon;

        // 制造飞船
        public abstract void MakeShip();

        // 显示飞船
        public void DisplayShip()
        {
            Debug.Log($"{Name} is on the screen");
        }

        // 跟随英雄
        public void FollowHeroShip()
        {
            Debug.Log($"{Name} follows hero ship with {Engine.ToStringEx()}");
        }

        // 射击
        public void Shoot()
        {
            Debug.Log($"{Name} shoots and does {Weapon.ToStringEx()}");
        }

        public string ToStringEx()
        {
            return $"The {Name} has a speed of {Engine.ToStringEx()} a firepower of {Weapon.ToStringEx()}";
        }
    }
}