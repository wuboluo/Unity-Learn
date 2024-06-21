namespace Yang.DesignPattern.AbstractFactory.Example2
{
    public interface IEnemyShipFactory
    {
        IEnemyShipWeapon AddESGun();
        IEnemyShipEngine AddESEngine();
    }
}