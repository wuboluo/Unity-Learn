namespace Yang.DesignPattern.Strategy.Example1
{
    public class Animal
    {
        protected IFly FlyingType;

        public string TryToFly()
        {
            return FlyingType.Fly();
        }

        public void SetFlyingAbility(IFly newFlyingType)
        {
            FlyingType = newFlyingType;
        }
    }
}