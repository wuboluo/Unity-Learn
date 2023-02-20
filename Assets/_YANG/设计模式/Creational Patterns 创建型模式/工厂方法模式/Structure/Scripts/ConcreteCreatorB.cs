namespace Yang.DesignPattern.FactoryMethod.Structure
{
    public class ConcreteCreatorB : Creator
    {
        public override Product FactoryMethod()
        {
            return new ConcreteProductB();
        }
    }
}