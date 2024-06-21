namespace Yang.DesignPattern.AbstractFactory.Example1
{
    // 大陆工厂
    public abstract class ContinentFactory
    {
        // 创建 食草/食肉动物
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
    }
}