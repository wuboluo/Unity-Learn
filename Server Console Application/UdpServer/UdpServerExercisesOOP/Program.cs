// See https://aka.ms/new-console-template for more information

namespace UdpServerExercises;

internal static class Program
{
    #region Udp 服务器要求

    // 如同 Tcp 一样让 Udp 服务器可以服务多个客户端

    // 需要具备的功能：
    // 1，区分消息类型（不需要处理分包，粘包）
    // 2，能接受多个客户端的消息
    // 3，能主动给发来消息的客户端发消息（记录客户端信息）
    // 4，主动记录上一次收到客户端消息的时间。长时间未收消息，主动移除纪律的客户端

    // 分析
    // 1，Udp是无连接的，如何记录连入的客户端？
    // 2，Udp收发消息都是通过一个socket来进行处理，应该如何处理收发消息？
    // 3，如果不使用心跳消息，如何记录上次收到消息的时间？

    #endregion

    public static ServerSocket serverSocket;
    
    private static void Main(string[] args)
    {
        serverSocket = new ServerSocket();
        serverSocket.Start("127.0.0.1", 8000);

        Console.WriteLine("Udp服务器已启动");

        while (true)
        {
            string? input = Console.ReadLine();
            if (input?[2..] == "1")
            {
                Example_PlayerMessage playerMsg = new Example_PlayerMessage()
                {
                    playerID = 100,
                    playerData = new Example_PlayerData()
                    {
                        playerName = "广播：这里是Udp服务器",
                        playerAtk = 100,
                        playerDef = 100
                    }
                };
                serverSocket.Broadcast(playerMsg);
            }
        }
    }
}