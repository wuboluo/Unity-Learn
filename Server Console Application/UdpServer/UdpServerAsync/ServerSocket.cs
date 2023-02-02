using System.Net;
using System.Net.Sockets;

namespace UdpServerAsync;

public class ServerSocket
{
    // 用于接收消息的容器
    private readonly byte[] cacheBytes = new byte[512];

    // 记录和服务器互动过的客户端
    private readonly Dictionary<string, Client> clients = new();

    // 服务器是否开启连接
    private bool isConnected;

    // 服务器Socket
    private Socket? socket;

    // 开启服务器
    public void StartServer(string serverIP, int serverPort)
    {
        if (isConnected) return;

        EndPoint ipPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

        // 初始化服务器socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        try
        {
            socket.Bind(ipPoint);
            isConnected = true;

            // 开始等待客户端的消息，并检测客户端发消息间隔是否超时
            ReceiveMessage(ipPoint);
        }
        catch (SocketException e)
        {
            Console.WriteLine($"服务器开启失败：{e.Message}");
        }
    }

    // 接收消息，超时检测
    private void ReceiveMessage(EndPoint remotePoint)
    {
        // 异步接收消息
        socket?.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref remotePoint, ReceiveFromCallback, remotePoint);
        // 信息接收超时检测
        ThreadPool.QueueUserWorkItem(CheckTimeout);
    }

    // 消息接收超时检测
    private void CheckTimeout(object? obj)
    {
        List<string> delList = new List<string>();
        while (isConnected)
        {
            // 每30s检查一次，有没有客户端没有再发送过消息了
            Thread.Sleep(30000);

            // 得到当前系统时间
            var nowTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

            // 超过10秒没有收到消息的客户端，认为不再连接，需要将其移除
            delList.AddRange(from c in clients.Values where nowTime - c.frontTime >= 10 select c.key);

            // 移除认为不再连接的客户端
            foreach (var t in delList) RemoveClient(t);

            // 清空待移除列表
            delList.Clear();
        }
    }

    // 给指定客户端发送消息
    private void SendMessage(MessageBase message, IPEndPoint ipPoint)
    {
        try
        {
            byte[] bytes = message.Writing();
            socket?.BeginSendTo(bytes, 0, bytes.Length, SocketFlags.None, ipPoint, SendToCallback, null);
        }
        catch (SocketException se)
        {
            Console.WriteLine($"BeginSendTo时错误：{se.SocketErrorCode}-{se.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"BeginSendTo时错误（可能是序列化问题）:{e.Message}");
        }
    }

    // 接收消息完成时回调
    private void ReceiveFromCallback(IAsyncResult result)
    {
        EndPoint? remoteEndPoint = result.AsyncState as IPEndPoint;

        try
        {
            if (remoteEndPoint != null)
            {
                socket?.EndReceiveFrom(result, ref remoteEndPoint);

                // ipEndPoint不为空
                if (remoteEndPoint is IPEndPoint clientIpPoint)
                {
                    string ip = clientIpPoint.Address.ToString();
                    int port = clientIpPoint.Port;
                    string key = ip + port;

                    Console.WriteLine(key);

                    // 客户端对象接收消息，并解析（面向对象，内部处理）
                    if (clients.ContainsKey(key))
                    {
                        clients[key].ReceiveMessage(cacheBytes);
                    }
                    else
                    {
                        // 如果这个客户端目前不处于服务器的连接列表，添加进去
                        clients.Add(key, new Client(ip, port));
                        clients[key].ReceiveMessage(cacheBytes);
                    }

                    // 继续接受消息
                    socket?.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref remoteEndPoint, ReceiveFromCallback, remoteEndPoint);
                }
            }
        }
        catch (SocketException s)
        {
            Console.WriteLine($"接受消息出错：{s.SocketErrorCode}-{s.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"接受消息出错（非Socket错误）：{e.Message}");
        }
    }

    // 发送消息完成时回调
    private void SendToCallback(IAsyncResult result)
    {
        try
        {
            socket?.EndSendTo(result);
        }
        catch (SocketException se)
        {
            Console.WriteLine($"EndSendTo时错误：{se.SocketErrorCode}-{se.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"EndSendTo时错误（可能是序列化问题）:{e.Message}");
        }
    }

    // 给所有保持连接的客户端广播消息 
    public void Broadcast(MessageBase message)
    {
        foreach (Client c in clients.Values)
        {
            SendMessage(message, c.clientPoint);
        }
    }

    // 移除认为不处于连接的客户端
    public void RemoveClient(string clientID)
    {
        if (clients.ContainsKey(clientID))
        {
            Console.WriteLine($"客户端 {clients[clientID].clientPoint} 被移除了");
            clients.Remove(clientID);
        }
    }

    // 关闭服务器，释放Socket
    public void Close()
    {
        if (socket != null)
        {
            isConnected = false;
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }
    }
}