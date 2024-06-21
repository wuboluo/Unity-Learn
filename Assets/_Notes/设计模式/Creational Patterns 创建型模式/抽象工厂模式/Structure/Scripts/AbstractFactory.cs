namespace Yang.DesignPattern.AbstractFactory.Structure
{
    public abstract class AbstractFactory
    {
        // 创建产品 A、B
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }
}