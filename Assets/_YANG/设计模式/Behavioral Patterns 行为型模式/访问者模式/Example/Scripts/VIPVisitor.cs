using UnityEngine;

namespace Yang.DesignPattern.Visitor.Example
{
    // 付费访问者，购买VIP。可以同时玩免费和收费游戏
    public class VIPVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            switch (element)
            {
                case PUBG pubg:
                    Debug.Log($"VIP用户，可以玩 {pubg.GetType().Name}，此游戏售价为：{pubg.Price}");
                    break;
                
                case Forest forest:
                    Debug.Log($"VIP用户，可以玩 {forest.GetType().Name}，此游戏售价为：{forest.Price}");
                    break;
            }
        }
    }
}