namespace Yang.DesignPattern.AbstractFactory.Example1
{
    // 食肉动物
    public abstract class Carnivore
    {
        // 进食，吃掉一个食草动物
        public abstract void Eat(Herbivore herbivore);
    }
}