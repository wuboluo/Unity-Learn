using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example1
{
    public class MediatorPatternExample1 : MonoBehaviour
    {
        private void Start()
        {
            Chatroom chatroom = new Chatroom();

            Participant zs = new Customer("张三");
            Participant ls = new Customer("李四");
            Participant kf = new Server("客服");

            chatroom.Register(zs);
            chatroom.Register(ls);
            chatroom.Register(kf);

            zs.Send("客服", "售后问题");
            ls.Send("客服", "这件还有货吗？");
            kf.Send("张三", "您好，请问是需要退换货吗？");
            kf.Send("李四", "您好，这件暂时缺货哦");
        }
    }
}