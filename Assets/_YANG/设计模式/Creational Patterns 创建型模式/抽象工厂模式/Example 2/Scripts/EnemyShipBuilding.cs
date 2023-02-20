namespace Yang.DesignPattern.AbstractFactory.Example2
{
    public abstract class EnemyShipBuilding
    {
        protected abstract EnemyShip MakeEnemyShip(ShipType type);

        public EnemyShip OrderShip(ShipType type)
        {
            EnemyShip ship = MakeEnemyShip(type);
            ship.MakeShip();
            ship.DisplayShip();
            ship.FollowHeroShip();
            ship.Shoot();
            
            return ship;
        }
    }
}