using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Yang.Net.S;

namespace Yang.Net.Tcp.Async
{
    public class NetAsyncManager : MonoBehaviour
    {
        // 接收消息的 缓存容器
        private readonly byte[] cacheBytes = new byte[1024 * 1024];

        // 容器中解析到的 光标 位置
        private int cacheNumber;

        // 心跳消息，发送心跳消息间隔时间
        private readonly HeartbeatMessage heartbeatMsg = new();
        private const int sendHeartbeatMsgDir = 2;

        // 客户端socket
        private Socket socket;

        // 是否和服务器处于连接状态
        private bool isConnected;

        // 收到的消息队列
        private readonly Queue<MessageBase> receiveQueue = new();
        public static NetAsyncManager Instance { get; private set; }


        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 定时发送心跳消息
            InvokeRepeating(nameof(SendHeartbeatMessage), 0, sendHeartbeatMsgDir);
        }

        private void Update()
        {
            if (receiveQueue.Count > 0)
            {
                MessageBase msg = receiveQueue.Dequeue();
                switch (msg)
                {
                    case Example_PlayerMessage playerMsg:
                        string content = $"{playerMsg.playerID}-{playerMsg.playerData.playerName}-{playerMsg.playerData.playerAtk}-{playerMsg.playerData.playerDef}";
                        Debug.Log($"收到服务器 {socket.RemoteEndPoint} 发来的消息：{content}");
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            Close();
        }

        // 连接服务器
        public void Connect(string ip, int port)
        {
            if (socket is { Connected: true }) return;

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs connectArgs = new SocketAsyncEventArgs();
            connectArgs.RemoteEndPoint = ipPoint;
            connectArgs.Completed += (_, args) =>
            {
                if (args.SocketError == SocketError.Success)
                {
                    print("连接成功");

                    // 接收消息
                    SocketAsyncEventArgs receiveArgs = new SocketAsyncEventArgs();
                    receiveArgs.SetBuffer(cacheBytes, 0, cacheBytes.Length);
                    receiveArgs.Completed += ReceiveCallback;

                    socket.ReceiveAsync(receiveArgs);
                }
                else
                {
                    print($"连接失败：{args.SocketError}");
                }
            };
            socket.ConnectAsync(connectArgs);
        }

        // 发送消息（自定义消息类）
        public void Send(MessageBase message)
        {
            if (socket is { Connected: true })
            {
                // 序列化消息
                byte[] bytes = message.Writing();

                SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
                sendArgs.SetBuffer(bytes, 0, bytes.Length);
                sendArgs.Completed += (_, args) =>
                {
                    if (args.SocketError != SocketError.Success)
                    {
                        print($"消息发送失败：{args.SocketError}");
                        Close();
                    }
                };
                // 发送消息
                socket.SendAsync(sendArgs);
            }
            else
            {
                Close();
            }
        }

        // 分包粘包发送测试
        public void SendTest(byte[] bytes)
        {
            SocketAsyncEventArgs testArgs = new SocketAsyncEventArgs();
            testArgs.SetBuffer(bytes, 0, bytes.Length);

            testArgs.Completed += (_, args) =>
            {
                if (args.SocketError != SocketError.Success)
                {
                    print($"发送消息失败：{args.SocketError}");
                    Close();
                }
            };
            socket.SendAsync(testArgs);
        }

        // 收消息完成回调
        private void ReceiveCallback(object obj, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                // 解析消息 BytesTransferred:实际接收到的字节数
                HandleReceivedMessage(args.BytesTransferred);

                // 设置缓存容器的接受索引偏移量
                args.SetBuffer(cacheNumber, args.Buffer.Length - cacheNumber);
                // 继续异步接收消息
                if (socket is { Connected: true }) socket.ReceiveAsync(args);
                else Close();
            }
            else
            {
                print($"接收消息出错：{args.SocketError}");

                // 关闭客户端连接
                Close();
            }
        }

        // 处理分包，粘包
        private void HandleReceivedMessage(int receivedNumber)
        {
            int msgID = 0;
            int currentIndex = 0;

            // 累加 容器中收到的所有信息字节的长度
            cacheNumber += receivedNumber;

            while (true)
            {
                // 每次将信息长度重置，是为了避免上一次解析的数据 影响这次的判断
                var msgLength = -1;

                // 处理解析第一条消息
                // 如果缓存容器中存放的字节长度，相比上次解析到的位置，相距8位以上（意味着可以解析消息ID和消息长度）
                if (cacheNumber - currentIndex >= 8)
                {
                    // 解析消息ID
                    msgID = BitConverter.ToInt32(cacheBytes, currentIndex);
                    currentIndex += 4;
                    // 解析消息长度
                    msgLength = BitConverter.ToInt32(cacheBytes, currentIndex);
                    currentIndex += 4;
                }

                // 如果【缓存容器的字节长度】和【当前解析到的索引位置】之间的长度>=解析的这条消息的内容长度
                // 并且 该消息的ID和长度已被成功解析
                if (cacheNumber - currentIndex >= msgLength && msgLength != -1)
                {
                    // 解析消息内容（消息体）
                    MessageBase msg = null;
                    switch (msgID)
                    {
                        case 1:
                            msg = new Example_PlayerMessage();
                            msg.Reading(cacheBytes, currentIndex);
                            break;
                    }

                    // 如果这条消息成功解析，放入 已收到的消息队列，等待 update去逐条输出
                    if (msg != null)
                    {
                        receiveQueue.Enqueue(msg);
                    }

                    // 更新容器中解析到的位置
                    currentIndex += msgLength;

                    // 如果当前解析到的位置和容器中保存的字节长度相同
                    if (currentIndex == cacheNumber)
                    {
                        // 则容器中所有消息都解析完成，重置“光标”位置，跳出循环
                        cacheNumber = 0;
                        break;
                    }
                }
                // 存在粘包
                else
                {
                    // 如果粘的这个包的ID和长度已被解析
                    if (msgLength != -1)
                        // 将“光标位置”回退8位，等于是回退到上一条消息的结尾位置
                        // 将粘的这个包的ID和长度，等待下个包收到时，重新以一条新消息处理
                        currentIndex -= 8;

                    // 把剩余没有解析的字节数组内容，移到前面来，用于缓存下次继续解析
                    Array.Copy(cacheBytes, currentIndex, cacheBytes, 0, cacheNumber - currentIndex);
                    // 更新容器中实际有用的（未被解析的）字节长度。（currentIndex是之前已经被完全解析的消息长度）
                    cacheNumber -= currentIndex;
                    break;
                }
            }
        }

        // 心跳消息
        private void SendHeartbeatMessage()
        {
            // 连入服务器后，定时发送心跳消息
            if (socket is { Connected: true }) Send(heartbeatMsg);
        }

        // 关闭连接，释放socket
        private void Close()
        {
            if (socket != null)
            {
                QuitMessage msg = new QuitMessage();
                socket.Send(msg.Writing());

                socket.Shutdown(SocketShutdown.Both);
                socket.Disconnect(false);
                socket.Close();
                socket = null;
            }
        }
    }
}