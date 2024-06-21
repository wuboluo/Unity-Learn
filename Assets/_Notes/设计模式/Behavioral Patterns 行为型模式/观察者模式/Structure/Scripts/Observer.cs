namespace Yang.DesignPattern.Observer.Structure
{
    public abstract class Observer
    {
        // Subject 变化时，观察者做出改变的接口
        public abstract void Update();
    }
}