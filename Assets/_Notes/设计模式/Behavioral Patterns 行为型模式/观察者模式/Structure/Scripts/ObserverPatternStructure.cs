using UnityEngine;

namespace Yang.DesignPattern.Observer.Structure
{
    public class ObserverPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            // 一个具体的主题（被观察对象）
            ConcreteSubject s = new ConcreteSubject();

            // 同时被三个观察者订阅
            s.Attach(new ConcreteObserver(s, "X"));
            s.Attach(new ConcreteObserver(s, "Y"));
            s.Attach(new ConcreteObserver(s, "Z"));

            // 主题变更
            s.SubjectState = "ABC";
            // 通知所有观察者
            s.Notify();

            s.SubjectState = "666";
            s.Notify();
        }
    }
}