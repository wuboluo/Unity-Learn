// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

internal static class Program
{
    private static Socket? socketTcp;
    private static readonly List<Socket?> clientSockets = new();

    private static bool isClose;


    private static void Main(string[] args)
    {
        // 使用Socket和多线程实现服务端服务多个客户端
        // 1，允许多个客户端连入服务端
        // 2，可以分别和多个客户端进行通信


        // 1，建立Socket 绑定 监听
        socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
        socketTcp.Bind(ipPoint);
        socketTcp.Listen(10);
        Console.WriteLine("服务端绑定监听结束，等待客户端连入");

        // 2，等待客户端连接（特殊处理）
        Thread acceptThread = new Thread(AcceptClientConnect);
        acceptThread.Start();

        // 3，收发消息（特殊处理）
        Thread receiveThread = new Thread(ReceiveClientMessage);
        receiveThread.Start();

        // 4，关闭
        while (true)
        {
            string? input = Console.ReadLine();
            if (input == "quit")
            {
                foreach (Socket? cs in clientSockets)
                {
                    cs?.Shutdown(SocketShutdown.Both);
                    cs?.Close();
                }

                clientSockets.Clear();
                isClose = true;

                break;
            }
            
            // 前两位为 B: 认为是在发消息
            if (input?[..2] == "B:")
            {
                foreach (Socket? cs in clientSockets)
                {
                    cs?.Send(Encoding.UTF8.GetBytes(input[2..]));
                }
            }
        }
    }

    // 接收客户端连接
    private static void AcceptClientConnect()
    {
        while (!isClose)
        {
            Socket? s = socketTcp?.Accept();
            clientSockets.Add(s);
            s?.Send(Encoding.UTF8.GetBytes("欢迎连接服务端"));
        }
    }

    // 接收各个客户端发来的消息
    private static void ReceiveClientMessage()
    {
        Socket? clientSocket;
        byte[] result = new byte[1024 * 1024];
        int receiveNumber;
        int index;

        while (!isClose)
        {
            for (index = 0; index < clientSockets.Count; index++)
            {
                clientSocket = clientSockets[index];

                // 如果该Socket有可以接受的消息，返回值就是字节数
                if (clientSocket is {Available: > 0})
                {
                    // 客户端即使没有发消息来，这句话也会执行
                    receiveNumber = clientSocket.Receive(result);

                    // 不能直接在这处理消息，因为处理消息会造成阻塞，影响后续消息的处理
                    // 所以开一个专门处理消息的线程，使用线程池提高性能
                    ThreadPool.QueueUserWorkItem(HandleMessage!, (clientSocket, Encoding.UTF8.GetString(result, 0, receiveNumber)));
                }
            }
        }
    }


    // 处理消息
    private static void HandleMessage(object state)
    {
        (Socket socket, string message) = ((Socket, string)) state;
        Console.WriteLine($"收到客户端 {socket.RemoteEndPoint} 发来的消息：{message}");
    }
}