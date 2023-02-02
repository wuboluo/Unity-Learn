using System.Net;
using UnityEngine;

namespace Yang.Net.IP_Port
{
    public class IPAndPort : MonoBehaviour
    {
        private void Start()
        {
            #region IP类和端口类用来干什么？

            // 想要网络通讯，进行网络连接，就要找到对应的设备。
            // 那么，IP和端口号就是定位网络中设备必不可少的关键元素
            // C#中提供了对应的类来声明对应信息

            #endregion

            #region IPAddress

            // 初始化IP信息的方法
            byte[] ipAddress = { 110, 110, 110, 10 };
            // 1，用byte数组进行初始化
            IPAddress ip1 = new IPAddress(ipAddress);
            // 2，用long长整型进行初始化
            IPAddress ip2 = new IPAddress(0x6E6E6EA);
            // 3，使用字符串转换（推荐）
            IPAddress ip3 = IPAddress.Parse("110.110.110.10");

            // 特殊IP地址
            // 127.0.0.1 本机地址

            // 获取可用的IPv6地址
            // IPAddress.IPv6Any

            #endregion

            #region IPEndPoint

            // 将网络端点表示为IP地址和端口号，表现为IP地址和端口号的组合

            // 初始化方法
            IPEndPoint ipPoint1 = new IPEndPoint(ip3, 8888);
            IPEndPoint ipPoint2 = new IPEndPoint(IPAddress.Parse("110.110.110.10"), 8888);

            #endregion

            #region 总结

            // 程序标识IP信息
            IPAddress ip = IPAddress.Parse("110,110,110,10");
            // 程序表示通讯目标
            IPEndPoint ipPoint = new IPEndPoint(ip, 8888);

            #endregion
        }
    }
}