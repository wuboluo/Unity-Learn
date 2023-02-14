namespace Yang.DesignPattern.Visitor.Structure
{
    public abstract class Visitor
    {
        // 为对象结构中的每个 ConcreteElement 类声明一个 Visit 操作
        // 操作的名称和签名标识向访问者发送访问请求的类。这让访问者可以确定被访问元素的具体类。然后访问者可以通过其特定的界面直接访问元素
        
        public abstract void VisitConcreteElementA(ConcreteElementA a);
        public abstract void VisitConcreteElementB(ConcreteElementB b);
    }
}