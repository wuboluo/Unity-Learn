using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Yang.Net.Tcp.Async
{
    public class TcpAsyncCommonMethod : MonoBehaviour
    {
        private readonly byte[] resultBytes = new byte[1024];

        private void Start()
        {
            #region 异步方法和同步方法的区别

            // 同步方法：方法中逻辑执行完毕后，再继续执行后面的方法
            // 异步方法：方法中逻辑可能还没有执行完毕，就继续执行后面的内容

            // 异步方法的本质：
            // 往往异步方法当中都会使用多线程执行某部分逻辑
            // 因为不需要等待方法中逻辑执行完毕就可以继续执行下面的逻辑

            // 注意：Unity中的协同程序中的某些异步方法，有的使用的是多线程，有的使用的是迭代器分布执行

            #endregion

            #region 举例说明异步方法原理

            // 例如：实现一个异步倒计时

            // 1，线程回调
            CountDownAsync(5, () => print("倒计时结束"));
            print("异步方法后的逻辑");

            // 2，async-await
            // 会等待线程执行完毕，继续执行后面的逻辑
            // 相对第一种方式，可以让函数分步执行
            CountDownAsync(5);
            print("异步方法后的逻辑 async-await");

            #endregion

            #region Socket-Tcp 通信中的异步方法一（Begin开头）

            // 回调函数参数：IAsyncResult
            // AsyncState：调用异步方法时传入的参数，需要转换
            // AsyncWaitHandle：用于同步等待

            Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 1，服务器相关（BeginAccept-EndAccept）
            socketTcp.BeginAccept(AcceptCallback, socketTcp);

            // 2，客户端相关（BeginConnect-EndConnect）
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            socketTcp.BeginConnect(ipPoint, ConnectCallback, socketTcp);

            // 3，通用
            // 接收消息（BeginReceive-EndReceive）
            socketTcp.BeginReceive(resultBytes, 0, resultBytes.Length, SocketFlags.None, ReceiveCallback, socketTcp);
            // 发送消息（BeginSend-EndSend）
            byte[] bytes = Encoding.UTF8.GetBytes("Yang");
            socketTcp.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, SendCallback, socketTcp);

            #endregion

            #region Socket-Tcp 通信中的异步方法二（Async结尾）

            // 关键变量类型：SocketAsyncEventArgs
            // 它会作为Async异步方法的传入值，需要通过它进行一些关键参数的赋值


            // 1，服务器相关（AcceptAsync）
            SocketAsyncEventArgs eAccept = new SocketAsyncEventArgs();
            eAccept.Completed += (socket, args) =>
            {
                if (args.SocketError == SocketError.Success)
                {
                    // 获取连入的客户端socket
                    Socket clientSocket = args.AcceptSocket;
                    (socket as Socket)?.AcceptAsync(args);
                }
                else
                {
                    print($"连入客户端失败：{args.SocketError}");
                }
            };
            socketTcp.AcceptAsync(eAccept);

            // 2，客户端相关（ConnectAsync）
            SocketAsyncEventArgs eConnect = new SocketAsyncEventArgs();
            eConnect.Completed += (socket, args) =>
            {
                if (args.SocketError == SocketError.Success)
                {
                    // 连接成功
                }
                else
                {
                    print($"连接失败：{args.SocketError}");
                }
            };
            socketTcp.ConnectAsync(eConnect);

            // 3，通用
            // 发送消息（SendAsync）
            SocketAsyncEventArgs eSend = new SocketAsyncEventArgs();
            byte[] bytes2 = Encoding.UTF8.GetBytes("Yang");
            eSend.SetBuffer(bytes2, 0, bytes2.Length);
            eSend.Completed += (socket, args) =>
            {
                if (args.SocketError == SocketError.Success)
                {
                    print("发送成功");
                }
                else
                {
                }
            };
            socketTcp.SendAsync(eSend);

            // 接收消息（ReceiveAsync）
            SocketAsyncEventArgs eReceive = new SocketAsyncEventArgs();
            eReceive.SetBuffer(new byte[1024 * 1024], 0, 1024 * 1024); // 设置接收数据的容器，偏移位置，容量
            eReceive.Completed += (socket, args) =>
            {
                if (args.SocketError == SocketError.Success)
                {
                    // 收取存在容器当中的字节
                    // buffer:容器 BytesTransferred:收取了多少字节
                    string str = Encoding.UTF8.GetString(args.Buffer, 0, args.BytesTransferred);
                    args.SetBuffer(0, args.Buffer.Length);
                    // 再接收下一条消息
                    (socket as Socket)?.ReceiveAsync(args);
                }
            };
            socketTcp.ReceiveAsync(eReceive);

            #endregion

            #region 总结

            // C#网络通信中，异步主要存在两种方案：
            // 1，Begin开头
            // 内部开多线程，通过回调形式返回结果，需要和End相关方法，配合使用
            
            // 2，Async结尾
            // 内部开多线程，通过回调形式返回结果，依赖SocketAsyncEventArgs对象配合使用，更加方便
            
            #endregion
        }

        private static void CountDownAsync(int second, UnityAction callback)
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    print(second);
                    Thread.Sleep(1000);
                    --second;
                    if (second == 0) break;
                }

                callback?.Invoke();
            });
            thread.Start();

            print("开始倒计时");
        }

        private static async void CountDownAsync(int second)
        {
            print("倒计时开始");

            await Task.Run(() =>
            {
                while (true)
                {
                    print(second);
                    Thread.Sleep(1000);
                    --second;
                    if (second == 0) break;
                }
            });

            print("倒计时结束");
        }

        // 服务器接收客户端连入回调
        private static void AcceptCallback(IAsyncResult result)
        {
            try
            {
                // 获取传入的参数（第二个参数socketTcp，其实就是result.AsyncState这个变量）
                Socket s = result.AsyncState as Socket;
                // 通过调用EndAccept就可以得到连入的客户端Socket
                Socket clientSocket = s?.EndAccept(result);

                // 注意：这里不是递归，异步方法执行完就结束了
                s?.BeginAccept(AcceptCallback, s);
            }
            catch (SocketException e)
            {
                print($"接收客户端连入错误：{e.SocketErrorCode}-{e.Message}");
            }
        }

        // 客户端连入服务器回调
        private static void ConnectCallback(IAsyncResult result)
        {
            Socket s = (Socket)result.AsyncState;
            try
            {
                s?.EndConnect(result);
            }
            catch (SocketException e)
            {
                print($"连接出错：{e.SocketErrorCode}-{e.Message}");
            }
        }

        // 客户端/服务器接收消息回调
        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                if (result.AsyncState is Socket s)
                {
                    // 收到了多少字节
                    int num = s.EndReceive(result);
                    // 消息处理（示例）
                    string exampleStr = Encoding.UTF8.GetString(resultBytes, 0, num);

                    // 继续接收（注意不是递归）
                    s.BeginReceive(resultBytes, 0, resultBytes.Length, SocketFlags.None, ReceiveCallback, s);
                }
            }
            catch (SocketException e)
            {
                print($"接收消息错误：{e.SocketErrorCode}-{e.Message}");
            }
        }

        // 客户端/服务器发送消息回调
        private static void SendCallback(IAsyncResult result)
        {
            try
            {
                if (result.AsyncState is Socket s)
                {
                    // 成功发送的字节数，一般用不到，因为是整体发送数据
                    int successSendNumber = s.EndSend(result);
                    print("发送成功");
                }
            }
            catch (SocketException e)
            {
                print($"发送消息错误：{e.SocketErrorCode}-{e.Message}");
            }
        }
    }
}