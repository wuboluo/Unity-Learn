using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using Yang.Net.S;

// 1，客户端的网络连接不会影响主线程
// 2，可以随时和服务端进行连接

namespace Yang.Net.Tcp.Sync
{
    public class TcpNetManager : MonoBehaviour
    {
        // 用于接收消息的队列【中间容器】，子线程将收到的消息放进去，主线程从中取
        private readonly Queue<MessageBase> receiveMsgQueue = new();

        // 发送的消息队列。主线程将待发的消息放进去，发送线程从中取
        private readonly Queue<MessageBase> sendMsgQueue = new();

        // 用于处理分包时，缓存的字节数组 和 字节数组长度
        private readonly byte[] cacheBytes = new byte[1024 * 1024];
        private int cacheNumber;

        // 客户端Socket
        private Socket clientSocket;

        // 心跳消息，发送心跳消息间隔时间
        private readonly HeartbeatMessage heartbeatMsg = new();
        private const int sendHeartbeatMsgDir = 2;

        // 是否和服务器处于连接状态
        private bool isConnected;

        [Header("测试心跳检测超时关闭连接")] public bool heartbeatTimeout;

        public static TcpNetManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            // 切换场景不被移除
            DontDestroyOnLoad(gameObject);

            // 心跳消息
            InvokeRepeating(nameof(SendHeartbeatMessage), 0, sendHeartbeatMsgDir);
        }

        private void Update()
        {
            // 如果【中间容器】中存在新消息，打印出来
            if (receiveMsgQueue.Count > 0)
            {
                MessageBase msg = receiveMsgQueue.Dequeue();
                switch (msg)
                {
                    case Example_PlayerMessage playerMsg:
                        string content = $"{playerMsg.playerID}-{playerMsg.playerData.playerName}-{playerMsg.playerData.playerAtk}-{playerMsg.playerData.playerDef}";
                        Debug.Log($"收到服务器 {clientSocket.RemoteEndPoint} 发来的消息：{content}");
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            Close();
        }

        // 连接服务端
        public void ConnectServer(string ip, int port)
        {
            if (isConnected) return;

            // 不为空就new一个
            clientSocket ??= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 连接服务器
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            try
            {
                // 连接
                clientSocket.Connect(ipPoint);
                isConnected = true;

                // 开启发送线程
                ThreadPool.QueueUserWorkItem(SendMessage);
                // 开启接收线程
                ThreadPool.QueueUserWorkItem(ReceiveMessage);

                Debug.Log("已连接服务器");
            }
            catch (SocketException se)
            {
                string log = se.ErrorCode == 10061
                    ? "服务器拒绝连接"
                    : "服务器连接失败，" + $"{se.ErrorCode}-{se.Message}";

                Console.WriteLine(log);
            }
        }

        // 发送消息，实际上是将待发的消息放入队列，等待发送线程依次去发送
        public void Send(MessageBase msg)
        {
            // 主线程往里放消息
            sendMsgQueue.Enqueue(msg);
        }

        // 发送线程从消息队列中依次取出并发送
        private void SendMessage(object obj)
        {
            while (isConnected)
            {
                if (sendMsgQueue.Count > 0)
                {
                    clientSocket.Send(sendMsgQueue.Dequeue().Writing());
                }
            }
        }

        // 接收消息
        private void ReceiveMessage(object obj)
        {
            // 由于多线程不能访问主线程的内容
            // 所以接收到的消息会放到一个【中间容器】中，再由主线程去【中间容器】中取出使用
            while (isConnected)
            {
                if (clientSocket.Available > 0)
                {
                    // 使用临时变量去接收，会增加性能消耗，但会节约内存
                    byte[] receiveBytes = new byte[1024 * 1024];
                    int receiveNumber = clientSocket.Receive(receiveBytes);
                    HandleReceivedMessage(receiveBytes, receiveNumber);
                }
            }
        }

        /// <summary>
        ///     处理接收的消息可能出现分包、粘包的问题
        /// </summary>
        /// <param name="rBytes">收到的这条消息的字节数组（可能存在分包粘包）</param>
        /// <param name="rNumber">收到的这条消息的总长度（可能存在分包粘包）</param>
        private void HandleReceivedMessage(byte[] rBytes, int rNumber)
        {
            int msgID = 0;
            int currentIndex = 0; // 当前解析到的位置

            // 收到消息时，先查看是否已有缓存的消息字节数组。如果有，就直接拼接到后面（缓存数组的尾部）
            rBytes.CopyTo(cacheBytes, cacheNumber);
            cacheNumber += rNumber;

            while (true)
            {
                // 每次将信息长度重置，是为了避免上一次解析的数据 影响这次的判断
                int msgLength = -1; // 消息长度

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
                    MessageBase msg = null;
                    switch (msgID)
                    {
                        case 1:
                            msg = new Example_PlayerMessage();
                            msg.Reading(cacheBytes, currentIndex);
                            break;
                    }
                    
                    // 如果这条消息成功解析，放入 已收到的消息队列，等待 update去逐条输出
                    if (msg != null) receiveMsgQueue.Enqueue(msg);
                    // 更新容器中解析到的位置
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

        private void Close()
        {
            if (clientSocket != null)
            {
                print("客户端主动断开连接");

                if (heartbeatTimeout)
                {
                    // 主动给服务器发送一条【断开连接】的消息
                    QuitMessage quitMsg = new QuitMessage();
                    // 使用socket的阻塞式同步发送。如果使用线程发送，可能还没发送成功，下面就把线程断了
                    clientSocket.Send(quitMsg.Writing());

                    // 关闭socket
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Disconnect(false);
                    clientSocket.Close();
                }

                clientSocket = null;

                // 关闭发送/接收消息线程
                isConnected = false;
            }
        }

        // 用于测试直接发字节数组的方法
        public void SendTest(byte[] bytes)
        {
            clientSocket.Send(bytes);
        }

        private void SendHeartbeatMessage()
        {
            // 连入服务器后，定时发送心跳消息
            if (isConnected)
            {
                Send(heartbeatMsg);
            }
        }
    }
}