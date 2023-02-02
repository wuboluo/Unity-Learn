using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Yang.Net.S;

namespace Yang.Net.Tcp.Sync
{
    public class ClientBasicLogic : MonoBehaviour
    {
        public int port;

        private void Start()
        {
            #region 回顾客户端要做的事情

            // 1，创建套接字Socket
            // 2，用Connect连接服务端
            // 3，用Send和Receive收发消息
            // 4，Shutdown释放连接
            // 5，关闭套接字

            #endregion

            #region 实现客户端基本逻辑

            // 1，创建Socket
            Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 2，Connect连接服务端
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port); // 此处的IP是对方的IP，由于现在是本机测试，所以可以写本机IP
            try
            {
                socketTcp.Connect(ipPoint);
            }
            catch (SocketException e)
            {
                if (e.ErrorCode == 10061)
                    Console.WriteLine("服务器拒绝连接");
                else
                    Console.WriteLine($"连接失败：{e.ErrorCode}");
            }

            // 3，Send/Receive收发数据
            // Receive
            byte[] receiveBytes = new byte[1024];
            int receiveNumber = socketTcp.Receive(receiveBytes);
            // 优化：
            // 1，首先解析消息的ID（前4个字节获取ID）
            int messageID = BitConverter.ToInt32(receiveBytes, 0);
            switch (messageID)
            {
                case 1:
                    Example_PlayerMessage msg = new Example_PlayerMessage();
                    msg.Reading(receiveBytes, 4); // 前4个字节为messageID，所以从第4个字节开始解析
                    print($"Receive: {msg.playerID}-{msg.playerData.playerName}-{msg.playerData.playerAtk}-{msg.playerData.playerDef}");
                    break;
            }
            // print($"收到服务端收来的消息：{Encoding.UTF8.GetString(receiveBytes, 0, receiveNumber)}");
            
            // Send
            socketTcp.Send(Encoding.UTF8.GetBytes("你好，我是Yang的客户端"));

            // 4，Shutdown释放连接
            socketTcp.Shutdown(SocketShutdown.Both);

            // 5，关闭套接字
            socketTcp.Close();

            #endregion

            // 注意：客户端的 Connect Send Receive 会阻塞主线程，要等到执行完毕才会继续执行后面的内容 
        }
    }
}