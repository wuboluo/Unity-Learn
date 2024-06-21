namespace Yang.DesignPattern.Iterator.Example1
{
    public interface IAbstractIterator
    {
        bool IsDone { get; }
        Item CurrentItem { get; }
        Item First();
        Item Next();
    }
}