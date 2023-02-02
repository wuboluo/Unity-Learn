// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;
using Tcp.Sync;

// 新语法会省略这部分，直接书写Main方法中的逻辑
// class Program ...
// static void Main(string[] args) ...

#region 回顾服务端需要做的事

// 1，创建套接字Socket
// 2，用Bind方法将套接字与本地地址绑定
// 3，用Listen方法监听，设置允许客户端连入的最大数量
// 4，用Accept方法等待客户端连接
// 5，建立连接，Accept方法返回新套接字
// 6，用Send和Receive相关方法收发数据
// 7，用Shutdown方法释放连接
// 8，关闭套接字

#endregion


#region 实现服务端基本逻辑

// 1，-------------------- 创建Socket(TCP)
var socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

// 2，-------------------- Bind绑定本机的地址和Socket
try
{
    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
    socketTcp.Bind(ipPoint);
}
catch (Exception e)
{
    Console.WriteLine("Bind错误：" + e);
    return;
}

// 3，-------------------- Listen监听，设置最大连接数量
socketTcp.Listen(10);
Console.WriteLine("服务端绑定监听结束，等待客户端连入");

// 4，-------------------- 用Accept等待客户端连入
// 5，-------------------- 建立连接，Accept返回新的Socket（这个Socket才是真正通信的“管道”）
Socket socketClient = socketTcp.Accept(); // 这里是阻塞式的方法，会卡住主线程，直到等到有客户端连入
Console.WriteLine("有客户端连入");

// 6，-------------------- Send/Receive收发数据
// 发送
// socketClient.Send(Encoding.UTF8.GetBytes("欢迎连入服务端"));
// 发送自定义类数据
Example_PlayerMessage msg = new Example_PlayerMessage
{
    playerID = 10,
    playerData = new Example_PlayerData
    {
        playerName = "Yang",
        playerAtk = 100,
        playerDef = 80
    }
};
socketClient.Send(msg.Writing());

// 接收
byte[] result = new byte[1024];
var receiveNumber = socketClient.Receive(result); // 返回值为实际接收到的字节数
Console.WriteLine($"接收到了 {socketClient.RemoteEndPoint} 发来的消息，消息内容为：{Encoding.UTF8.GetString(result, 0, receiveNumber)}");

// 7，-------------------- Shutdown释放连接
socketClient.Shutdown(SocketShutdown.Both);

// 8，-------------------- 关闭Socket
socketClient.Close();

#endregion


Console.WriteLine("按任意键退出");
Console.ReadKey();