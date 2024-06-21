namespace Yang.DesignPattern.Strategy.Example1
{
    public class Bird : Animal
    {
        public Bird()
        {
            FlyingType = new Flyable();
        }
    }
}