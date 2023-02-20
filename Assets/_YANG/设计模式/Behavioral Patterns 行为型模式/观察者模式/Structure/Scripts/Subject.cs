using System.Collections.Generic;

namespace Yang.DesignPattern.Observer.Structure
{
    public abstract class Subject
    {
        // 知道它的观察者。任意数量的观察者对象都可以观察一个主题
        private readonly List<Observer> _observers = new();
        

        // 提供用于【附加】和【分离】 Observer 对象的接口
        public void Attach(Observer observer)
        {
            _observers.Add(observer);
        }
        public void Detach(Observer observer)
        {
            _observers.Remove(observer);
        }
        

        public void Notify()
        {
            foreach (Observer o in _observers) o.Update();
        }
    }
}