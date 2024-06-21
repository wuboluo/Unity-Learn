namespace Yang.DesignPattern.Visitor.Example
{
    public class Game : Element
    {
        protected Game(float price)
        {
            Price = price;
        }

        public float Price { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}