namespace Yang.DesignPattern.AbstractFactory.Example3
{
    // 格力工厂实现类
    public class GLFactory : IFactory
    {
        public Fridge GetFridge()
        {
            return new GLFridge();
        }

        public AirConditioner GetAriConditioner()
        {
            return new GLAirConditioner();
        }
    }
}