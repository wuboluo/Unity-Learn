namespace Yang.DesignPattern.AbstractFactory.Example1
{
    // 非洲大陆
    public class AfricaFactory : ContinentFactory
    {
        // 生成食草动物 羚羊
        public override Herbivore CreateHerbivore()
        {
            return new Wildebeest();
        }

        // 生成食肉动物 狮子
        public override Carnivore CreateCarnivore()
        {
            return new Lion();
        }
    }
}