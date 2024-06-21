namespace Yang.DesignPattern.Visitor.Structure
{
    // 元素
    public abstract class Element
    {
        // 定义一个接受访问者作为参数的操作
        public abstract void Accept(Visitor visitor);
    }
}