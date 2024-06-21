using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Yang.Net.Udp.Async
{
    public class UdpAsyncCommonMethod : MonoBehaviour
    {
        private readonly byte[] cacheBytes = new byte[512];
        private EndPoint remotePoint;

        private void Start()
        {
            // 主要围绕收发消息进行

            #region Udp中Begin相关异步

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // BeginSendTo
            byte[] bytes = Encoding.UTF8.GetBytes("udp beginSendTo");
            remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            socket.BeginSendTo(bytes, 0, bytes.Length, SocketFlags.None, remotePoint, SendToCallback, socket);

            // BeginReceiveFrom
            socket.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref remotePoint, ReceiveFromCallback, (socket, remotePoint));

            #endregion

            #region Udp中Async相关异步

            // SendToAsync
            SocketAsyncEventArgs sendToArgs = new SocketAsyncEventArgs();
            sendToArgs.SetBuffer(bytes, 0, bytes.Length); // 设置要发送的数据
            sendToArgs.Completed += SendToAsync; // 发送完成回调
            socket.SendToAsync(sendToArgs);

            // ReceiveFromAsync
            SocketAsyncEventArgs receiveFromArgs = new SocketAsyncEventArgs();
            receiveFromArgs.SetBuffer(cacheBytes, 0, cacheBytes.Length); // 设置用于接收数据的容器
            receiveFromArgs.Completed += ReceiveFromAsync; // 接收完成回调
            socket.ReceiveFromAsync(receiveFromArgs);

            #endregion
        }


        private void SendToCallback(IAsyncResult result)
        {
            try
            {
                Socket s = result.AsyncState as Socket;
                s?.EndSendTo(result);
                print("发送成功");
            }
            catch (SocketException e)
            {
                print($"发送失败：{e.Message}");
            }
        }

        private void ReceiveFromCallback(IAsyncResult result)
        {
            try
            {
                (Socket socket, EndPoint remoteEndPoint) = ((Socket, EndPoint))result.AsyncState;

                // 返回值：接收了多少字节
                int number = socket.EndReceiveFrom(result, ref remoteEndPoint);

                // 处理消息
                // ......

                // 继续接收消息
                socket.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref remoteEndPoint, ReceiveFromCallback, (socket, remoteEndPoint));
            }
            catch (SocketException e)
            {
                print($"接收消息出错：{e.SocketErrorCode}-{e.Message}");
            }
        }

        private void SendToAsync(object obj, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                // 发送成功
            }
            // 发送失败
        }

        private void ReceiveFromAsync(object obj, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                // 接收成功

                // 接收到的字节数组内容：
                // 1，args.Buffer
                // 2，cacheBytes

                // 接收到的字节长度
                int number = args.BytesTransferred;

                // 继续接收消息
                Socket s = obj as Socket;
                // 重新设置从第几个位置开始接收、接收多少
                args.SetBuffer(0, cacheBytes.Length);
                s?.ReceiveFromAsync(args);
            }
            // 接收失败
        }
    }
}