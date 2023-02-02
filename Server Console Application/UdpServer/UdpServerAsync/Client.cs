using System.Net;
using System.Net.Sockets;

namespace UdpServerAsync;

public class Client
{
    // 记录连入的客户端信息
    public readonly IPEndPoint clientPoint;

    // 此客户端惟一标识符（IP+端口）
    public readonly string key;

    // 上一次收到消息的时间
    public long frontTime = -1;

    public Client(string ip, int port)
    {
        key = ip + port;
        clientPoint = new IPEndPoint(IPAddress.Parse(ip), port);
    }

    // 接收消息
    public void ReceiveMessage(byte[] bytes)
    {
        // 因为数组是引用类型，别的客户端可能也会用到服务器中存放消息的容器
        // 所以需要将此客户端收到的这个消息取出来，放到自己的容器中，再慢慢做处理
        byte[] cacheBytes = new byte[bytes.Length];
        bytes.CopyTo(cacheBytes, 0);

        // 记录收到消息时候的系统时间
        frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

        // 多线程处理消息内容
        ThreadPool.QueueUserWorkItem(ParseMessage, cacheBytes);
    }

    // 解析消息内容
    private void ParseMessage(object? obj)
    {
        try
        {
            // 数据字节
            byte[]? bytes = obj as byte[];
            int currentIndex = 0;

            if (bytes != null)
            {
                // 消息ID
                int msgID = BitConverter.ToInt32(bytes, currentIndex);
                currentIndex += 4;
                // 消息长度
                int msgLength = BitConverter.ToInt32(bytes, currentIndex);
                currentIndex += 4;
                // 消息内容
                switch (msgID)
                {
                    case 1:
                        Example_PlayerMessage playerMsg = new Example_PlayerMessage();

                        // 反序列化
                        playerMsg.Reading(bytes, currentIndex);
                        // 输出
                        Console.WriteLine($"收到消息：{playerMsg.playerData.playerName}-{playerMsg.playerID}");
                        break;

                    case -1:
                        QuitMessage quitMsg = new QuitMessage();

                        // 处理退出
                        Program.serverSocket?.RemoveClient(key);
                        break;
                }
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"处理消息时出错：{e.Message}");

            // 如果出错，就让服务器不再记录这个客户端
            Program.serverSocket?.RemoveClient(key);
        }
    }
}