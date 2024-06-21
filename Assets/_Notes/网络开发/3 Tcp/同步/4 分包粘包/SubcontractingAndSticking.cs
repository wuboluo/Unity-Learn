using UnityEngine;

namespace Yang.Net.Tcp.Sync
{
    public class SubcontractingAndSticking : MonoBehaviour
    {
        private void Start()
        {
            #region 什么是分包、粘包？

            // 分包、粘包是指在网络通信中由于各种因素（网络环境、API规则等）造成的消息与消息之间出现的两种状态
            // 分包：一个消息被分成多个消息进行发送
            // 粘包：一个消息和另一个消息粘在了一起

            // 注意：分包和粘包可能同时出现

            #endregion

            #region 如何解决分包、粘包的问题

            // 为消息添加头部，记录消息的长度
            // 当接收到消息时，通过消息长度来判断是否分包、粘包
            // 对消息进行拆分处理、合并处理
            // 每次只处理完整的消息

            #endregion

            #region 实践解决

            // 为所有消息添加头部信息，用于存储其消息长度
            // 根据分包、粘包的表现情况，修改接收消息处的逻辑

            #endregion
        }
    }
}