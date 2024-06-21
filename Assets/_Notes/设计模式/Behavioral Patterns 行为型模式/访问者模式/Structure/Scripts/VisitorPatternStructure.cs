using UnityEngine;

namespace Yang.DesignPattern.Visitor.Structure
{
    public class VisitorPatternStructure : MonoBehaviour
    {
        // 封装一些作用于某种数据结构中的各元素的操作，它可以在不改变数据结构的前提下定义作用于这些元素的新的操作
        private void Start()
        {
            ObjectStructure os = new ObjectStructure();
            os.Attach(new ConcreteElementA());
            os.Attach(new ConcreteElementB());

            ConcreteVisitor1 cv1 = new ConcreteVisitor1();
            ConcreteVisitor2 cv2 = new ConcreteVisitor2();

            os.Accept(cv1);
            os.Accept(cv2);
        }
    }
}