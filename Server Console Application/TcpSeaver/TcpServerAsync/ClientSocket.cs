using System.Net.Sockets;

namespace TcpServerAsync;

public class ClientSocket
{
    private Socket? socket;
    public readonly int clientID;
    private static int clientBeginId = 1;
    
    // 上一次收到(心跳)消息的时间
    private long frontTime = -1;
    // 消息超时时间
    private const int timeOutTime = 10;

    // 缓存容器
    private readonly byte[] cacheBytes = new byte[1024];
    private int cacheNumber;
    
    public ClientSocket(Socket? socket)
    {
        this.socket = socket;
        clientID = clientBeginId;
        ++clientBeginId;

        // 开始收消息
        socket?.BeginReceive(cacheBytes, cacheNumber, cacheBytes.Length, SocketFlags.None, ReceiveCallback, null);
        ThreadPool.QueueUserWorkItem(CheckTimeOut);
    }

    // 发送消息
    public void Send(MessageBase message)
    {
        if (socket is {Connected: true})
        {
            byte[] bytes = message.Writing();
            socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, SendCallback, null);
        }
        else
        {
            Program.serverSocket?.CloseClientSocket(this);
        }
    }

    // 解析消息内容，处理分包粘包
    private void ParseMessage(int receivedNumber)
    {
        int msgID = 0;
        int currentIndex = 0;

        // 由于消息接收后是直接存储在cacheBytes中的，所以不需要进行什么拷贝操作
        // 增加收到消息的字节数量
        cacheNumber += receivedNumber;

        while (true)
        {
            // 每次将长度设置为-1，是避免上一次解析的数据影响这一次的判断
            int msgLength = -1;

            // 如果当前【待解析的字节长度】可以解析出【ID和消息长度】
            if (cacheNumber - currentIndex >= 8)
            {
                // 解析ID
                msgID = BitConverter.ToInt32(cacheBytes, currentIndex);
                currentIndex += 4;
                // 解析长度
                msgLength = BitConverter.ToInt32(cacheBytes, currentIndex);
                currentIndex += 4;
            }

            // 如果当前【待解析的字节长度】可以完整解析出【消息内容】
            // 并且该消息的 ID和长度已被解析
            if (cacheNumber - currentIndex >= msgLength && msgLength != -1)
            {
                // 解析消息体
                MessageBase? msg = null;
                switch (msgID)
                {
                    case 1:
                        msg = new Example_PlayerMessage();
                        msg.Reading(cacheBytes, currentIndex);
                        break;
                    case -1:
                        msg = new QuitMessage();
                        break;
                    case 999:
                        msg = new HeartbeatMessage();
                        break;
                }

                // 区分消息类型（自定义消息/心跳/退出...）
                if (msg != null) ThreadPool.QueueUserWorkItem(DistinguishMessageTypes, msg);

                // 移动解析光标
                currentIndex += msgLength;

                // 光标到达容器长度时，所有消息全部解析完成
                if (currentIndex == cacheNumber)
                {
                    // 重置光标
                    cacheNumber = 0;
                    break;
                }
            }
            // 存在分包
            else
            {
                // 那么我们需要把当前收到的内容记录下来
                // 有待下次接受到消息后，再做处理

                // 如果进行了 ID和长度 的解析，但是没有成功解析消息体，那么我们需要减去 currentIndex 移动的位置
                if (msgLength != -1) currentIndex -= 8;

                // 把剩余没有解析的字节数组内容，移到前面来，用于缓存下次继续解析
                Array.Copy(cacheBytes, currentIndex, cacheBytes, 0, cacheNumber - currentIndex);
                cacheNumber -= currentIndex;
                break;
            }
        }
    }

    // 区分消息类型
    private void DistinguishMessageTypes(object? obj)
    {
        switch (obj)
        {
            case QuitMessage quitMsg:
                // 收到客户端发来的主动断开的消息后，将自己添加至待关闭的socket列表中
                Program.serverSocket?.CloseClientSocket(this);
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

    // 接收消息回调
    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            if (socket is {Connected: true})
            {
                int number = socket.EndReceive(result);

                // 处理分包黏包
                ParseMessage(number);
                socket.BeginReceive(cacheBytes, cacheNumber, cacheBytes.Length - cacheNumber, SocketFlags.None, ReceiveCallback, socket);
            }
            else
            {
                Console.WriteLine("没有连接服务器，不用再继续接受消息了，关闭此客户端");
                Program.serverSocket?.CloseClientSocket(this);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine($"收消息错误：{e.SocketErrorCode}-{e.Message}");
            Program.serverSocket?.CloseClientSocket(this);
        }
    }

    // 发送消息回调
    private void SendCallback(IAsyncResult result)
    {
        try
        {
            if (socket is {Connected: true}) socket?.EndSend(result);
            else Program.serverSocket?.CloseClientSocket(this);
        }
        catch (SocketException e)
        {
            Console.WriteLine($"发送失败：{e.SocketErrorCode}-{e.Message}");
            Program.serverSocket?.CloseClientSocket(this);
        }
    }

    // 间隔时间检测接收消息是否超时
    private void CheckTimeOut(object? obj)
    {
        while (socket is {Connected: true})
        {
            // 判断是否已经开始接收消息并且上一条消息距现在为超时
            if (frontTime != -1 && DateTime.Now.Ticks / TimeSpan.TicksPerSecond - frontTime >= timeOutTime)
            {
                Program.serverSocket?.CloseClientSocket(this);
                break;
            }

            Thread.Sleep(5000);
        }
    }

    // 关闭客户端
    public void Close()
    {
        if (socket != null)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }
    }
}