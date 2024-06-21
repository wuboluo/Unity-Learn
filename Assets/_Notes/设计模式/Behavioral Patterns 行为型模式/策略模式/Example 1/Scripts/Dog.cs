namespace Yang.DesignPattern.Strategy.Example1
{
    public class Dog : Animal
    {
        public Dog()
        {
            FlyingType = new NonFlyable();
        }
    }
}