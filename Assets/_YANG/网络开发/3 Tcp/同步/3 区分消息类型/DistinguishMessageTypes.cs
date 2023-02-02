using UnityEngine;

namespace Yang.Net.Tcp.Sync
{
    public class DistinguishMessageTypes : MonoBehaviour
    {
        private void Start()
        {
            #region 如何发送之前的的自定义类的二进制信息

            // 1，继承 ObjectBinaryBase
            // 2，实现其中的序列化、反序列化、获取字节等相关方法
            // 3，实现自定义类数据时，序列化
            // 4，接收自定义类数据时，反序列化

            #endregion

            #region 如何区分消息

            // 解决方案：
            // 为发送的消息添加标识，比如添加在消息头部
            // 标识表示消息ID（int,short,byte,long）都可以，视情况而定
            
            // 例如：
            // 前四个字节为消息ID（int），后面为消息内容，如下所示
            // ####******************************
            // 接收消息时，先把前四个字节取出来解析为消息ID，再根据消息ID进行反序列化

            #endregion

            #region 实践

            // 创建步骤：
            // 1，创建消息基类，继承 ObjectBinaryBase，基类添加获取消息ID的方法或者属性
            // 2，让想要被发送的消息继承该类，实现序列化和反序列化的方法
            // 3，修改客户端和服务端收发消息的逻辑

            #endregion
        }
    }
}