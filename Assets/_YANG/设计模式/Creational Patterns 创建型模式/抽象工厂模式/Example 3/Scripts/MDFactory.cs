namespace Yang.DesignPattern.AbstractFactory.Example3
{
    // 美的工厂实现类
    public class MDFactory : IFactory
    {
        public Fridge GetFridge()
        {
            return new MDFridge();
        }

        public AirConditioner GetAriConditioner()
        {
            return new MDAirConditioner();
        }
    }
}