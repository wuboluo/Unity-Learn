using System.Net.Sockets;

namespace TcpServerExercisesOOP;

public class ClientSocket
{
    private static int clientBeginId = 1;

    public readonly int clientID;
    private Socket? socket;

    // 用于处理分包时，缓存的字节数组 和 字节数组长度
    private readonly byte[] cacheBytes = new byte[1024 * 1024];
    private int cacheNumber;

    // 上一次收到(心跳)消息的时间
    private long frontTime = -1;

    // 消息超时时间
    private const int timeOutTime = 10;

    // socket != null && socket.Connected == true
    private bool Connected => socket is {Connected: true};

    public ClientSocket(Socket? socket)
    {
        this.socket = socket;
        clientID = clientBeginId;
        ++clientBeginId;
    }

    // 间隔时间检测接收消息是否超时
    private void CheckTimeOut()
    {
        // 判断是否已经开始接收消息并且上一条消息距现在为超时
        if (frontTime != -1 && DateTime.Now.Ticks / TimeSpan.TicksPerSecond - frontTime >= timeOutTime)
        {
            // 认为该客户端已断开，将其添加至待关闭的队列
            Program.serverSocket?.AddDelSocket(this);
        }
    }

    // 关闭
    public void Close()
    {
        socket?.Shutdown(SocketShutdown.Both);
        socket?.Close();

        socket = null;
    }

    // 发送
    public void Send(MessageBase message)
    {
        if (Connected)
        {
            try
            {
                socket?.Send(message.Writing());
            }
            catch (Exception e)
            {
                Console.WriteLine($"发消息出错：{e.Message}");
                Program.serverSocket?.AddDelSocket(this);
            }
        }
        else
        {
            Program.serverSocket?.AddDelSocket(this);
        }
    }

    // 接收
    public void Receive()
    {
        if (!Connected)
        {
            Program.serverSocket?.AddDelSocket(this);
            return;
        }

        try
        {
            if (socket?.Available > 0)
            {
                byte[] result = new byte[1024];

                int receiveNumber = socket.Receive(result);
                ParseMessage(result, receiveNumber);
            }
            
            // 检测是否超时
            CheckTimeOut();
        }
        catch (Exception e)
        {
            Console.WriteLine($"接收消息出错：{e.Message}");

            // 解析错误也认为要将这个客户端断开
            Program.serverSocket?.AddDelSocket(this);
        }
    }

    // 区分消息类型
    private void DistinguishMessageTypes(object? obj)
    {
        MessageBase? message = obj as MessageBase;
        switch (message)
        {
            case QuitMessage quitMsg:
                // 收到客户端发来的主动断开的消息后，将自己添加至待关闭的socket列表中
                Program.serverSocket?.AddDelSocket(this);
                break;

            case HeartbeatMessage heartbeatMsg:
                // 记录收到心跳消息的时间
                frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond; // 得到系统时间对应的秒数
                Console.WriteLine($"收到客户端 {socket?.RemoteEndPoint} 的心跳信息");
                break;

            case Example_PlayerMessage playerMsg:
                string msgContent = $"{playerMsg.playerID}-{playerMsg.playerData.playerName}-{playerMsg.playerData.playerAtk}-{playerMsg.playerData.playerDef}";
                Console.WriteLine($"收到客户端 {socket?.RemoteEndPoint} 发来的消息：{msgContent}");
                break;
        }
    }

    /// <summary>
    ///     处理接收的消息可能出现分包、粘包的问题
    /// </summary>
    /// <param name="rBytes">收到的这条消息的字节数组（可能存在分包粘包）</param>
    /// <param name="rNumber">收到的这条消息的总长度（可能存在分包粘包）</param>
    private void ParseMessage(byte[] rBytes, int rNumber)
    {
        var msgID = 0;
        int currentIndex = 0; // 当前解析到的位置

        // 收到消息时，先查看是否已有缓存的消息字节数组。如果有，就直接拼接到后面（缓存数组的尾部）
        rBytes.CopyTo(cacheBytes, cacheNumber);
        cacheNumber += rNumber;

        while (true)
        {
            // 每次将信息长度重置，是为了避免上一次解析的数据 影响这次的判断
            var msgLength = -1; // 消息长度

            if (cacheNumber >= currentIndex)
            {
                // 解析ID
                msgID = BitConverter.ToInt32(cacheBytes, currentIndex);
                currentIndex += 4;
                // 解析长度
                msgLength = BitConverter.ToInt32(cacheBytes, currentIndex);
                currentIndex += 4;
            }

            // 如果这条消息减去8依旧大于消息长度，就认为存在完整的一条消息
            if (cacheNumber - currentIndex >= msgLength && msgLength != -1)
            {
                // 解析内容
                MessageBase? msg = null;
                switch (msgID)
                {
                    case -1:
                        // 退出消息，不存在消息内容，无需反序列化
                        msg = new QuitMessage();
                        break;

                    case 999:
                        // 心跳消息，不存在消息内容，无需反序列化
                        msg = new HeartbeatMessage();
                        break;

                    case 1:
                        msg = new Example_PlayerMessage();
                        msg.Reading(cacheBytes, currentIndex);
                        break;
                }

                if (msg != null) ThreadPool.QueueUserWorkItem(DistinguishMessageTypes, msg);
                currentIndex += msgLength;

                // 如果当前解析到的位置和缓存的总长度一致，就认为缓存的字节都被解析完成了，跳出即可
                if (currentIndex == cacheNumber)
                {
                    cacheNumber = 0;
                    break;
                }
            }
            // 不存在一条完整的消息。
            // 此时记录下前半段不完整的内容放进缓存中，跳出循环，等待下次收到消息后，再做处理
            else
            {
                // 如果进行了 ID和长度 的解析，但是没有成功解析消息内容。就需要减去currentIndex移动的位置，使得下次解析的时候从头处理
                if (msgLength != -1) currentIndex -= 8;

                // 就是把剩余没有解析的字节数组内容，移到最前，作为新的缓存内容，等待下次解析
                Array.Copy(cacheBytes, currentIndex, cacheBytes, 0, cacheNumber - currentIndex);
                cacheNumber -= currentIndex;

                break;
            }
        }
    }
}