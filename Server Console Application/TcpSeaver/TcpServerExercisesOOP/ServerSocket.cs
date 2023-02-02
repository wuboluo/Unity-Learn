using System.Net;
using System.Net.Sockets;

namespace TcpServerExercisesOOP;

public class ServerSocket
{
    // 连接的所有客户端Socket
    private readonly Dictionary<int, ClientSocket> clientsDic = new();

    // 等待被移除关闭的客户端
    private readonly List<ClientSocket> delSockets = new();

    private bool isClose;

    // 服务端Socket
    private Socket? socket;

    // 开启服务器端
    public void Start(string ip, int port, int maxCount)
    {
        isClose = false;

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        socket.Bind(ipPoint);
        socket.Listen(maxCount);

        ThreadPool.QueueUserWorkItem(Accept);
        ThreadPool.QueueUserWorkItem(Receive);
    }

    // 关闭服务器端
    public void Close()
    {
        isClose = true;

        lock (clientsDic)
        {
            foreach (ClientSocket client in clientsDic.Values) client.Close();
            clientsDic.Clear();
        }

        socket?.Shutdown(SocketShutdown.Both);
        socket?.Close();

        socket = null;
    }


    // 接受客户端连入
    private void Accept(object? obj)
    {
        while (!isClose)
        {
            try
            {
                Socket? connectSocket = socket?.Accept();

                // 当连入一个客户端时，记录下它
                ClientSocket client = new ClientSocket(connectSocket);
                lock (clientsDic)
                {
                    clientsDic.Add(client.clientID, client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"客户端连入报错：{e.Message}");
            }
        }
    }

    // 接受客户端消息
    private void Receive(object? obj)
    {
        while (!isClose)
        {
            lock (clientsDic)
            {
                if (clientsDic.Count > 0)
                {
                    foreach (ClientSocket client in clientsDic.Values) client.Receive();

                    CloseDelSockets();
                }
            }
        }
    }

    // 广播消息
    public void Broadcast(MessageBase message)
    {
        lock (clientsDic)
        {
            foreach (ClientSocket client in clientsDic.Values) client.Send(message);
        }
    }

    // 将要断开的客户端添加至 待关闭的客户端集合，统一处理，避免在foreach中移除造成报错
    public void AddDelSocket(ClientSocket clientSocket)
    {
        if (!delSockets.Contains(clientSocket))
            delSockets.Add(clientSocket);
    }

    // 将需要被断开的客户端断开并从列表移除
    private void CloseDelSockets()
    {
        if (delSockets.Count > 0)
        {
            foreach (ClientSocket ds in delSockets) CloseClientSocket(ds);
            delSockets.Clear();
        }
    }

    // 关闭客户端的连接，并从字典中移除
    private void CloseClientSocket(ClientSocket clientSocket)
    {
        // 多线程同时访问一个内存空间的时候，会出问题。所以在每次使用clientsDic的时候，锁住它
        lock (clientsDic)
        {
            clientSocket.Close();
            if (clientsDic.ContainsKey(clientSocket.clientID))
            {
                clientsDic.Remove(clientSocket.clientID);
                Console.WriteLine($"客户端 {clientSocket.clientID} 主动断开连接了");
            }
        }
    }
}