namespace Yang.DesignPattern.Iterator.Structure
{
    // 定义用于访问和遍历元素的接口
    public abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();
    }
}