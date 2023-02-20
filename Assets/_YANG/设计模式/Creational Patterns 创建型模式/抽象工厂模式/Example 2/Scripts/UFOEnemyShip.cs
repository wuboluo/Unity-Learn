using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example2
{
    public class UFOEnemyShip : EnemyShip
    {
        private readonly IEnemyShipFactory _factory;

        public UFOEnemyShip(IEnemyShipFactory factory)
        {
            _factory = factory;
        }

        public override void MakeShip()
        {
            Debug.Log("Making enemy ship " + Name);
            Weapon = _factory.AddESGun();
            Engine = _factory.AddESEngine();
        }
    }
}