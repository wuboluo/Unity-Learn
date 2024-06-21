namespace Yang.DesignPattern.AbstractFactory.Example2
{
    public class UFOEnemyShipFactory : IEnemyShipFactory
    {
        public IEnemyShipWeapon AddESGun()
        {
            return new EnemyShipUFOGun();
        }

        public IEnemyShipEngine AddESEngine()
        {
            return new EnemyShipUFOEngine();
        }
    }
}