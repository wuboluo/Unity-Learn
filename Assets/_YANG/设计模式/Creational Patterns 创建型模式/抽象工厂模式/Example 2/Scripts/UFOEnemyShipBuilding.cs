namespace Yang.DesignPattern.AbstractFactory.Example2
{
    public class UFOEnemyShipBuilding : EnemyShipBuilding
    {
        private EnemyShip _ship;

        protected override EnemyShip MakeEnemyShip(ShipType type)
        {
            if (type == ShipType.UFO)
            {
                IEnemyShipFactory factory = new UFOEnemyShipFactory();
                _ship = new UFOEnemyShip(factory);
                _ship.Name = "UFO";
            }

            return _ship;
        }
    }
}