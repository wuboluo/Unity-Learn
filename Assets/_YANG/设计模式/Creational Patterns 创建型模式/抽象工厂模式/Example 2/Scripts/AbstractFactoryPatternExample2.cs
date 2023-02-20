using UnityEngine;

namespace Yang.DesignPattern.AbstractFactory.Example2
{
    public class AbstractFactoryPatternExample2 : MonoBehaviour
    {
        private void Start()
        {
            // 创建一个 UFO飞船工厂
            EnemyShipBuilding ufoBuilder = new UFOEnemyShipBuilding();
            
            // 一个 UFO飞船订单
            ufoBuilder.OrderShip(ShipType.UFO);
        }
    }
}