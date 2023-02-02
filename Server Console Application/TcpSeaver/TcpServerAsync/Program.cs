// See https://aka.ms/new-console-template for more information

namespace TcpServerAsync;

internal static class Program
{
    public static ServerSocket? serverSocket;

    public static void Main(string[] args)
    {
        serverSocket = new ServerSocket();
        serverSocket.Start("127.0.0.1", 8000, 10);
        Console.WriteLine("开启服务器成功");

        while (true)
        {
            var order = Console.ReadLine();

            if (order?[2..] == "1")
            {
                Example_PlayerMessage playerMsg = new Example_PlayerMessage()
                {
                    playerID = 1000,
                    playerData = new Example_PlayerData()
                    {
                        playerName = "Server Yang",
                        playerAtk = 1000,
                        playerDef = 1000
                    }
                };

                serverSocket.Broadcast(playerMsg);
            }
        }
    }
}