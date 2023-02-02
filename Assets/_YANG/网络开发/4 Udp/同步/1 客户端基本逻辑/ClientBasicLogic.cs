using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Yang.Net.Udp.Sync
{
    public class ClientBasicLogic : MonoBehaviour
    {
        private void Start()
        {
            #region 实现Udp客户端通信，收发字符串

            // 1，创建套接字
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // 2，绑定本机地址
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
            socket.Bind(ipPoint);

            // 3，发送到指定目标
            IPEndPoint remoteIpPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000); // 本机测试时，注意远端端口不要和此socket绑定的不一样
            socket.SendTo(Encoding.UTF8.GetBytes("我是Udp客户端"), remoteIpPoint);

            // 4，接收消息
            byte[] receiveBytes = new byte[512];
            EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0); // remotePoint：主要是用于记录 谁发消息给我， 传入函数内部会自动赋值。在声明时的ip和端口没用
            int length = socket.ReceiveFrom(receiveBytes, ref remotePoint);
            IPEndPoint remote = remotePoint as IPEndPoint;
            print($"【{remote?.Address}-{remote?.Port}】发来了：{Encoding.UTF8.GetString(receiveBytes, 0, length)}");

            // 5，释放关闭
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            #endregion
        }
    }
}