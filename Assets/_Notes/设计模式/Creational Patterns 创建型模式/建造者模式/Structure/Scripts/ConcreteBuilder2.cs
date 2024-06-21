namespace Yang.DesignPattern.Builder.Structure
{
    public class ConcreteBuilder2 : Builder
    {
        private readonly Product _product = new();

        public override void BuildPartA()
        {
            _product.Add("Part X");
        }

        public override void BuildPartB()
        {
            _product.Add("Part Y");
        }

        public override Product GetResult()
        {
            return _product;
        }
    }
}