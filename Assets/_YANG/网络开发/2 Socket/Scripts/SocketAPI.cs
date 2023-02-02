using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Yang.Net.S
{
    public class SocketAPI : MonoBehaviour
    {
        private void Start()
        {
            #region Socket套接字的作用

            // 它是C#提供用于网络通信的一个类（在其它语言当中也有对应的Socket类）

            // Socket套接字是支持TCP/IP网络通信的基本操作单位
            // 一个套接字对象包含以下关键信息
            // 1，本机的IP和端口
            // 2，对方主机的IP和端口
            // 3，双方通信的协议信息

            // 一个Socket对象表示一个本地或者远程套接字信息
            // 它可以被视为一个数据通道
            // 这个通道连接于客户端和服务器之间
            // 数据的发送和接收均通过这个通道进行

            // 一般在制作长连接游戏时，会使用Socket套接字作为我们的通信方案
            // 可以抽象成一根管子，连接客户端和服务器的应用程序上，通过这个管子来传递交换信息

            #endregion

            #region Socket的类型

            // 1，流套接字
            //      主要用于实现TCP通信。提供了面向连接、可靠的、有序的、数据无差错且无重复的数据传输服务
            // 2，数据报套接字
            //      主要用于实现UDP通信。提供了无连接的通信服务，数据报的长度不能超过32KB，不提供正确性检查，不保证顺序，可能出现重发、丢失等情况
            // 3，原始套接字（不常用）
            //      主要用于实现IP数据包通信，用于直接访问协议的较低层，常用于侦听和分析数据包

            // 通过Socket的构造函数，我们可以声明不同类型的套接字
            // arg1：AddressFamily 网络寻址枚举类型，决定寻址方案
            //      InterNetWork ---- IPv4寻址
            //      InterNetworkV6 -- IPv6寻址
            // arg2：SocketType 套接字枚举类型，决定使用的套接字类型
            //      Stream --- 支持可靠、双向、基于连接的字节流（主要用于TCP通信）
            //      Dgram ---- 支持数据报，最大长度固定的无连接、不可靠的消息（主要用于UDP通信）
            // arg3：ProtocolType
            //      Tcp
            //      Udp

            Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket socketUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            #endregion

            #region Socket的常用属性

            // 1，套接字连接状态
            if (socketTcp.Connected)
            {
            }

            // 2，获取套接字的类型
            print(socketTcp.SocketType);
            // 3，获取套接字的协议类型
            print(socketTcp.ProtocolType);
            // 4，获取套接字的寻址方案
            print(socketTcp.AddressFamily);

            // 5，从网络中获取准备读取的数据量
            print(socketTcp.Available);

            // 6，获取本机EndPoint对象（IPEndPoint继承自EndPoint）
            IPEndPoint localPoint = socketTcp.LocalEndPoint as IPEndPoint;
            // 7，获取远程EndPoint对象
            IPEndPoint remotePoint = socketTcp.RemoteEndPoint as IPEndPoint;

            #endregion

            #region Socket的常用方法

            // 主要用于服务器
            // 1，绑定IP和端口
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            socketTcp.Bind(ipPoint);
            // 2，设置客户端连接的最大数量
            socketTcp.Listen(10);
            // 3，等待客户端连入
            socketTcp.Accept();

            // 主要用于客户端
            // 1，连接远程服务端
            socketTcp.Connect(IPAddress.Parse("110,110,110,10"), 9000);

            // 都用
            // 1，同步发送和接受数据
            //      socketTcp.Send();
            //      socketTcp.Receive()
            // 2，异步发送和接收数据
            //      socketTcp.SendAsync();
            //      socketTcp.ReceiveAsync();
            // 3，释放连接并关闭Socket，先于Close调用
            socketTcp.Shutdown(SocketShutdown.Both);
            // 4，关闭连接，释放所有Socket关联资源
            socketTcp.Close();

            #endregion
        }
    }
}