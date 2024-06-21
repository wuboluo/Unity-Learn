namespace Yang.DesignPattern.Prototype.Example1
{
    public class CloneFactory
    {
        public IAnimal GetClone(IAnimal animalSample)
        {
            return (IAnimal)animalSample.Clone();
        }
    }
}