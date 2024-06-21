namespace Yang.DesignPattern.Visitor.Example
{
    public interface IVisitor
    {
        // 每个 Visitor 访问者，访问不同的游戏，有不同的表现
        void Visit(Element element);
    }
}