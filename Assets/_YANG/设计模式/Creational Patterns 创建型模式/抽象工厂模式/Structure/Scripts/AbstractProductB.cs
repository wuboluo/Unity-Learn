namespace Yang.DesignPattern.AbstractFactory.Structure
{
    public abstract class AbstractProductB
    {
        // 产品B 和某个产品A 相互作用
        public abstract void Interact(AbstractProductA a);
    }
}