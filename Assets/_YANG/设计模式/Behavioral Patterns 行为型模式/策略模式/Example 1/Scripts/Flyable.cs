namespace Yang.DesignPattern.Strategy.Example1
{
    // 用于保存拥有飞行行为的对象
    public class Flyable : IFly
    {
        public string Fly()
        {
            return "Flying";
        }
    }
}