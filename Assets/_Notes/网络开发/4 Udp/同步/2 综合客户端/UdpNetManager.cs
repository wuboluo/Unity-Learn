using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using Yang.Net.S;

namespace Yang.Net.Udp.Sync
{
    public class UdpNetManager : MonoBehaviour
    {
        // 接收数据的容器
        private readonly byte[] cacheBytes = new byte[512];

        // 待处理的收发消息队列，交给多线程去处理，Unity主线程只管 往里放/从中取
        private readonly Queue<MessageBase> receiveQueue = new();
        private readonly Queue<MessageBase> sendQueue = new();

        // 客户端socket是否连接
        private bool isConnected;

        private EndPoint serverPoint;
        private Socket socket;
        public static UdpNetManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            // 切换场景不被移除
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (receiveQueue.Count > 0)
            {
                MessageBase msg = receiveQueue.Dequeue();
                switch (msg)
                {
                    case Example_PlayerMessage playerMsg:
                        print($"{playerMsg.playerData.playerName}-{playerMsg.playerData.playerAtk}-{playerMsg.playerData.playerDef}");
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            Close();
        }

        // 启动客户端
        public void StartClient(string remoteIP, int remotePort)
        {
            // 如果当前客户端已开启，就不要再开了
            if (isConnected) return;

            // 记录服务器地址，发消息和收消息要用
            serverPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);

            IPEndPoint clientPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(clientPoint);
                isConnected = true;
                print("Udp客户端启动");

                ThreadPool.QueueUserWorkItem(ReceiveMessage);
                ThreadPool.QueueUserWorkItem(SendMessage);
            }
            catch (Exception e)
            {
                print($"启动Socket错误：{e.Message}");
            }
        }

        // 将新消息放入待发的队列中
        public void Send(MessageBase message)
        {
            sendQueue.Enqueue(message);
        }

        // 接收消息
        private void ReceiveMessage(object obj)
        {
            EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);

            while (isConnected)
                if (socket is { Available: > 0 })
                    try
                    {
                        socket.ReceiveFrom(cacheBytes, ref remotePoint);

                        // 如果不是服务器发来的消息，不处理。避免垃圾消息
                        if (!remotePoint.Equals(serverPoint)) continue;

                        // 处理服务器发来的消息
                        int currentIndex = 0;

                        // ID和消息内容长度
                        int msgID = BitConverter.ToInt32(cacheBytes, currentIndex);
                        currentIndex += 4;
                        int msgLength = BitConverter.ToInt32(cacheBytes, currentIndex);
                        currentIndex += 4;

                        // 消息内容
                        MessageBase msg = null;
                        switch (msgID)
                        {
                            // 退出
                            case -1:
                                break;

                            // 心跳
                            case 999:
                                break;

                            case 1:
                                msg = new Example_PlayerMessage();
                                msg.Reading(cacheBytes, currentIndex);
                                break;
                        }

                        if (msg != null) receiveQueue.Enqueue(msg);
                    }
                    catch (SocketException se)
                    {
                        print($"接收消息错误：{se.SocketErrorCode}-{se.Message}");
                    }
                    catch (Exception e)
                    {
                        print($"接收消息错误，但非网络问题：{e.Message}");
                    }
        }

        // 发送消息
        private void SendMessage(object obj)
        {
            while (isConnected)
                if (sendQueue.Count > 0)
                    try
                    {
                        socket.SendTo(sendQueue.Dequeue().Writing(), serverPoint);
                    }
                    catch (SocketException e)
                    {
                        print($"发送消息错误：{e.SocketErrorCode}-{e.Message}");
                    }
        }

        // 关闭
        private void Close()
        {
            if (socket != null)
            {
                isConnected = false;
                // 发送一个退出消息给服务器，让其移除连接记录
                socket.SendTo(new QuitMessage().Writing(), serverPoint);

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
        }
    }
}