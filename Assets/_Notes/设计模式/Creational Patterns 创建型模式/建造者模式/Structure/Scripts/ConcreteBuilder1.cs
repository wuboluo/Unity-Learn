namespace Yang.DesignPattern.Builder.Structure
{
    public class ConcreteBuilder1 : Builder
    {
        private readonly Product _product = new();

        public override void BuildPartA()
        {
            _product.Add("Part A");
        }

        public override void BuildPartB()
        {
            _product.Add("Part B");
        }

        public override Product GetResult()
        {
            return _product;
        }
    }
}