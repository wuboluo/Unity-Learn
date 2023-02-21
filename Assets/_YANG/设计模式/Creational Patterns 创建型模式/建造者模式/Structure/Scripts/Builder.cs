namespace Yang.DesignPattern.Builder.Structure
{
    // 指定用于创建 Product 对象的部分的抽象接口
    public abstract class Builder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();

        public abstract Product GetResult();
    }
}