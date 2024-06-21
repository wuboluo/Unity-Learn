namespace Yang.DesignPattern.Visitor.Structure
{
    // 具体元素 B
    public class ConcreteElementB : Element
    {
        public void OperatorB()
        {
        }

        // 实现一个以访问者为参数的 Accept 操作
        public override void Accept(Visitor visitor)
        {
            visitor.VisitConcreteElementB(this);
        }
    }
}