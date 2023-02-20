namespace Yang.DesignPattern.AbstractFactory.Example3
{
    // 工厂抽象接口
    public interface IFactory
    {
        // 生产冰箱
        Fridge GetFridge();
        
        // 生产空调
        AirConditioner GetAriConditioner();
    }
}