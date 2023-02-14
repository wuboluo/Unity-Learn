using UnityEngine;

namespace Yang.DesignPattern.Visitor.Example
{
    public class VisitorPatternExample : MonoBehaviour
    {
        private void Start()
        {
            Steam steam = new Steam();
            steam.Attach(new PUBG());
            steam.Attach(new Forest());
            
            // 相同的 Visitor 对不同的数据产生不同的行为
            // 不同的 Visitor 对相同的数据产生不同的行为
            steam.Accept(new VIPVisitor());
            steam.Accept(new FreeVisitor());
        }
    }
}