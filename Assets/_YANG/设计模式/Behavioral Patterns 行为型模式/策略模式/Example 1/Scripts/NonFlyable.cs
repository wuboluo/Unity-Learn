namespace Yang.DesignPattern.Strategy.Example1
{
    public class NonFlyable : IFly
    {
        public string Fly()
        {
            return "I can't fly";
        }
    }
}