// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

internal static class Program
{
    // 实现Udp服务端通信，收发字符串

    private static void Main(string[] args)
    {
        // 1，创建套接字
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // 2，绑定本机地址
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        socket.Bind(ipPoint);

        // 3，接收消息
        byte[] receiveBytes = new byte[512];
        EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
        int length = socket.ReceiveFrom(receiveBytes, ref remotePoint);
        IPEndPoint? remote = remotePoint as IPEndPoint;
        Console.WriteLine($"【{remote?.Address}-{remote?.Port}】发来了：{Encoding.UTF8.GetString(receiveBytes, 0, length)}");

        // 4，发送到指定目标
        socket.SendTo(Encoding.UTF8.GetBytes("我是Udp服务器"), remotePoint); // remotePoint：已经知道发消息的是谁了，所以直接通过这个信息，发送消息给他

        // 5，释放关闭
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}