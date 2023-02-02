using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Yang.Net.S;

namespace Yang.Net.Udp.Async
{
    public class UdpNetAsyncManager : MonoBehaviour
    {
        // 存放服务器发来的消息的缓存容器
        private readonly byte[] cacheBytes = new byte[512];

        // 客户端是否连接
        private bool isConnected;

        // 服务器发来的消息队列
        private readonly Queue<MessageBase> receiveQueue = new();

        // 服务器
        private EndPoint serverPoint;

        // 客户端Socket
        private Socket socket;
        private IPEndPoint ipPoint;
        
        public static UdpNetAsyncManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (receiveQueue.Count > 0)
            {
                MessageBase msgBase = receiveQueue.Dequeue();
                switch (msgBase)
                {
                    case Example_PlayerMessage playerMsg:
                        print($"来自服务器 {serverPoint} 发来的消息：{playerMsg.playerData.playerName}-{playerMsg.playerID}");
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            Close();
        }

        // 启动客户端
        public void StartClient(string serverIP, int serverPort)
        {
            if (isConnected) return;

            // 记录服务器信息，方便后续交换信息和判断信息来源
            serverPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

            // 创建客户端信息
            ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(ipPoint);

                isConnected = true;

                // 异步接收消息
                ReceiveAsync();
            }
            catch (SocketException e)
            {
                print($"客户端启动出错：{e.SocketErrorCode}-{e.Message}");
            }
        }

        // 异步接收消息
        private void ReceiveAsync()
        {
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.SetBuffer(cacheBytes, 0, cacheBytes.Length); // 设置接收消息用的容器大小
            args.RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0); // 用于记录服务器信息
            args.Completed += ReceiveFromCallback; // 完成时回调

            socket.ReceiveFromAsync(args);
        }

        // 发送消息
        public void SendAsync(MessageBase message)
        {
            try
            {
                if (socket != null && isConnected)
                {
                    // 消息序列化
                    byte[] bytes = message.Writing();

                    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                    args.SetBuffer(bytes, 0, bytes.Length); // 发送的数据
                    args.Completed += SendToCallback; // 发送完成回调
                    args.RemoteEndPoint = serverPoint; // 设置远端目标
                    
                    // 异步发送
                    socket.SendToAsync(args);
                }
            }
            catch (SocketException se)
            {
                print($"发送消息出错：{se.SocketErrorCode}-{se.Message}");
            }
            catch (Exception e)
            {
                print($"发送消息出错（可能是序列化问题）：{e.Message}");
            }
        }

        // 解析消息内容
        private void ParseMessage(SocketAsyncEventArgs args)
        {
            var currentLength = 0;

            // 解析ID
            var msgID = BitConverter.ToInt32(args.Buffer, currentLength);
            currentLength += 4;

            // 解析长度
            BitConverter.ToInt32(args.Buffer, currentLength);
            currentLength += 4;

            // 解析消息体
            MessageBase msg = null;
            switch (msgID)
            {
                case 1:
                    msg = new Example_PlayerMessage();
                    // 反序列化消息体
                    msg.Reading(args.Buffer, currentLength);
                    break;
            }

            if (msg != null)
                receiveQueue.Enqueue(msg);
        }

        // 接收消息完成回调
        private void ReceiveFromCallback(object obj, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                try
                {
                    // 要是服务器发的才处理
                    if (args.RemoteEndPoint.Equals(serverPoint))
                    {
                        // 处理消息内容
                        ParseMessage(args);
                    }

                    // 再次接收消息
                    if (socket != null && isConnected)
                    {
                        args.SetBuffer(0, cacheBytes.Length);
                        socket.ReceiveFromAsync(args);
                    }
                }
                catch (SocketException s)
                {
                    print($"接收消息出错：{s.SocketErrorCode}-{s.Message}");
                    Close();
                }
                catch (Exception e)
                {
                    print($"接收消息出错（可能是反序列化问题）：{e.Message}");
                    Close();
                }
            }
            else
            {
                print($"接收消息失败：{args.SocketError}");
            }
        }

        // 发送消息完成回调
        private void SendToCallback(object obj, SocketAsyncEventArgs args)
        {
            if (args.SocketError != SocketError.Success)
            {
                print($"发送消息失败：{args.SocketError}");
            }
            else
            {
            }
        }

        // 关闭连接，释放socket
        private void Close()
        {
            if (socket != null)
            {
                isConnected = false;

                // 发送一个退出消息给服务器 让其移除记录
                QuitMessage msg = new QuitMessage();
                socket.SendTo(msg.Writing(), serverPoint);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }
    }
}