using UnityEngine;

namespace Yang.DesignPattern.Visitor.Structure
{
    // 实现 Visitor 声明的每个操作
    public class ConcreteVisitor2 : Visitor
    {
        public override void VisitConcreteElementA(ConcreteElementA a)
        {
            Debug.Log($"{GetType().Name} 访问了 {a.GetType().Name}");
        }

        public override void VisitConcreteElementB(ConcreteElementB b)
        {
            Debug.Log($"{GetType().Name} 访问了 {b.GetType().Name}");
        }
    }
}