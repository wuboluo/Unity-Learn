namespace Yang.DesignPattern.FactoryMethod.Structure
{
    public class ConcreteCreatorA : Creator
    {
        public override Product FactoryMethod()
        {
            return new ConcreteProductA();
        }
    }
}