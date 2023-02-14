namespace Yang.DesignPattern.Visitor.Structure
{
    // 具体元素 A
    public class ConcreteElementA : Element
    {
        public void OperatorA()
        {
        }

        // 实现一个以访问者为参数的 Accept 操作
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementA(this);
        }
    }
}