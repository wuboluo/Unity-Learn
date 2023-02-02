using System.Net;
using System.Net.Sockets;

namespace TcpServerAsync;

public class ServerSocket
{
    private Socket socket;

    private readonly Dictionary<int, ClientSocket> clientsDic = new();

    public void Start(string ip, int port, int number)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);

        try
        {
            socket.Bind(ipPoint);
            socket.Listen(number);

            // 通过异步接收客户端连入
            socket.BeginAccept(AcceptCallback, null);
        }
        catch (SocketException e)
        {
            Console.WriteLine($"启动服务器错误：{e.SocketErrorCode}-{e.Message}");
        }
    }

    private void AcceptCallback(IAsyncResult result)
    {
        try
        {
            // 获取连入的客户端
            Socket clientSocket = socket.EndAccept(result);
            ClientSocket client = new ClientSocket(clientSocket);
            // 记录客户端对象
            lock (clientsDic)
            {
                clientsDic.Add(client.clientID, client);
            }

            // 继续去让别的客户端等待连入
            socket.BeginAccept(AcceptCallback, null);
        }
        catch (SocketException e)
        {
            Console.WriteLine($"客户端连入失败：{e.SocketErrorCode}-{e.Message}");
        }
    }

    // 给所有连入的客户端发消息
    public void Broadcast(MessageBase message)
    {
        lock (clientsDic)
        {
            foreach (var client in clientsDic.Values)
            {
                client.Send(message);
            }
        }
    }

    // 关闭连接的客户端，从字典中移除
    public void CloseClientSocket(ClientSocket client)
    {
        lock (clientsDic)
        {
            client.Close();
            if (clientsDic.ContainsKey(client.clientID))
            {
                clientsDic.Remove(client.clientID);
                Console.WriteLine($"客户端 {client.clientID} 主动断开连接了");
            }
        }
    }
}