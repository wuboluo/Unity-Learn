namespace Yang.DesignPattern.Iterator.Structure
{
    // 定义用于创建 Iterator 对象的接口
    public abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }
}