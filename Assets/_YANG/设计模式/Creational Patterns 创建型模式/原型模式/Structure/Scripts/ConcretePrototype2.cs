namespace Yang.DesignPattern.Prototype.Structure
{
    public class ConcretePrototype2 : Prototype
    {
        public ConcretePrototype2(string id) : base(id)
        {
        }

        public override Prototype Clone()
        {
            return MemberwiseClone() as Prototype;
        }
    }
}