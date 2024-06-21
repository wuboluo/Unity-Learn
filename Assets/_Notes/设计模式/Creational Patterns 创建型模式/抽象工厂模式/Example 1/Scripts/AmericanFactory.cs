namespace Yang.DesignPattern.AbstractFactory.Example1
{
    // 美洲大陆
    public class AmericanFactory : ContinentFactory
    {
        // 生成食草动物 野牛
        public override Herbivore CreateHerbivore()
        {
            return new Bison();
        }

        // 生成食肉动物 狼
        public override Carnivore CreateCarnivore()
        {
            return new Wolf();
        }
    }
}