using System.Net;
using System.Net.Sockets;

namespace UdpServerExercises;

public class ServerSocket
{
    // 自定义规则，key为IP+端口
    private readonly Dictionary<string, Client> clients = new();

    private bool isClose;
    private Socket? socket;

    public ServerSocket()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    }

    // 开启服务器
    public void Start(string ip, int port)
    {
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);

        try
        {
            if (socket != null)
            {
                lock (socket)
                {
                    socket?.Bind(ipPoint);
                }
            }

            isClose = false;

            // 消息接收的处理
            ThreadPool.QueueUserWorkItem(ReceiveMessage);
            // 超时检测
            ThreadPool.QueueUserWorkItem(CheckTimeout);
        }
        catch (SocketException e)
        {
            Console.WriteLine($"Udp服务器开启错误：{e.Message}");
        }
    }

    // 多线程检查消息接受间隔
    private void CheckTimeout(object? obj)
    {
        long nowTime;
        List<string> delClients = new List<string>();
        while (true)
        {
            // 每30s检测一次，有没有长时间未收到消息的客户端，有就移除
            Thread.Sleep(30000);

            nowTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            // 超过10s未收到消息的客户端，添加至待移除的列表
            delClients.AddRange(from c in clients.Values where nowTime - c.frontTime >= 10 select c.id);

            // 移除超时的客户端
            foreach (string t in delClients) RemoveClient(t);

            // 清空待移除列表
            delClients.Clear();
        }
    }

    // 接收消息
    private void ReceiveMessage(object? obj)
    {
        // 接收消息的容器
        byte[] bytes = new byte[512];
        // 记录发送者信息
        EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);

        while (!isClose)
        {
            if (socket != null)
            {
                lock (socket)
                {
                    if (socket.Available > 0)
                    {
                        socket.ReceiveFrom(bytes, ref remotePoint);

                        // 如果明确变量不会为空，在后面加上 ! 取消引用，可以避免系统空性检查
                        IPEndPoint remote = (remotePoint as IPEndPoint)!;
                        string ip = remote.Address.ToString();
                        int port = remote.Port;
                        string key = ip + port;

                        // 处理消息。不要在这处理，会卡住并且影响后面消息的接收，应交给客户端对象处理
                        if (clients.ContainsKey(key))
                        {
                            clients[key].ReceiveMessage(bytes);
                        }
                        else
                        {
                            clients.Add(key, new Client(ip, port));
                            clients[key].ReceiveMessage(bytes);
                        }
                    }
                }
            }
        }
    }

    // 发送消息，给指定目标
    private void SendMessage(MessageBase message, EndPoint remotePoint)
    {
        try
        {
            if (socket != null)
            {
                lock (socket)
                {
                    socket.SendTo(message.Writing(), remotePoint);
                }
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"发消息错误：{e.SocketErrorCode}-{e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"发消息错误，可能是序列化错误：{e.Message}");
        }
    }

    // 广播消息
    public void Broadcast(MessageBase message)
    {
        foreach (Client c in clients.Values) SendMessage(message, c.point);
    }

    // 移除对应客户端
    public void RemoveClient(string key)
    {
        if (clients.ContainsKey(key))
        {
            Console.WriteLine($"客户端 {clients[key].point} 被移除了");
            clients.Remove(key);
        }
    }

    // 关闭连接，释放socket
    private void Close()
    {
        isClose = true;

        if (socket != null)
        {
            lock (socket)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }
    }
}