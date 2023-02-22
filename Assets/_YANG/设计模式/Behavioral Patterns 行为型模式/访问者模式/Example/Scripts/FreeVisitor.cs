using UnityEngine;

namespace Yang.DesignPattern.Visitor.Example
{
    // 白嫖访问者，不花钱。只能玩免费游戏，不能玩收费游戏
    public class FreeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            switch (element)
            {
                case PUBG pubg:
                    Debug.Log($"白嫖用户，可以玩 {pubg.GetType().Name}，此游戏售价为：{pubg.Price}，免费");
                    break;

                case Forest forest:
                    Debug.Log($"白嫖用户，不可以玩 {forest.GetType().Name}，此游戏售价为：{forest.Price}，需要购买");
                    break;
            }
        }
    }
}