using System.Net;

namespace UdpServerExercises;

// 用来记录和服务器通信过的 IP和端口
public class Client
{
    public readonly IPEndPoint point;
    public string id;

    // 上一次收到消息的时间
    public long frontTime;

    public Client(string ip, int port)
    {
        id = ip + port;

        // 记录客户端信息
        point = new IPEndPoint(IPAddress.Parse(ip), port);
    }

    // 处理消息
    public void ReceiveMessage(byte[] bytes)
    {
        // 为了避免处理消息时，又接受了新的消息，所以要把信息数据拷贝出来，把服务器中存放消息的容器腾出地方
        byte[] cacheBytes = new byte[512];
        bytes.CopyTo(cacheBytes, 0);

        // 记录收到消息的系统时间，单位为秒
        frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;

        // 处理消息内容
        ThreadPool.QueueUserWorkItem(ParseMessage, cacheBytes);
    }

    // 多线程处理消息
    private void ParseMessage(object? obj)
    {
        try
        {
            // 获得传进来的信息字节数组
            byte[]? bytes = obj as byte[];

            // 处理到的位置（光标位置）
            int currentIndex = 0;

            // 处理ID、内容长度、内容
            if (bytes != null)
            {
                int msgID = BitConverter.ToInt32(bytes, currentIndex);
                currentIndex += 4;

                int msgLength = BitConverter.ToInt32(bytes, currentIndex);
                currentIndex += 4;

                switch (msgID)
                {
                    // 退出
                    case -1:
                        QuitMessage quitMsg = new QuitMessage();
                        Program.serverSocket.RemoveClient(id);
                        break;

                    // 心跳
                    case 999:
                        break;

                    case 1:
                        Example_PlayerMessage playerMsg = new Example_PlayerMessage();
                        playerMsg.Reading(bytes, currentIndex);
                        Console.WriteLine($"{playerMsg.playerData.playerName}-{playerMsg.playerData.playerAtk}-{playerMsg.playerData.playerDef}");
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"客户端对象处理消息出错：{e.Message}");
            
            // 如果出错，就将该客户端移除
            Program.serverSocket.RemoveClient(id);
        }
    }
}