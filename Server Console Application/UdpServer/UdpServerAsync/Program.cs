namespace UdpServerAsync;

public static class Program
{
    public static ServerSocket? serverSocket;

    private static void Main(string[] args)
    {
        serverSocket = new ServerSocket();
        serverSocket.StartServer("127.0.0.1", 8000);

        Console.WriteLine("服务器已开启");

        while (true)
        {
            string? input = Console.ReadLine();
            if (input?[2..] == "1")
            {
                Example_PlayerMessage msg = new Example_PlayerMessage
                {
                    playerID = 1,
                    playerData = new Example_PlayerData()
                    {
                        playerName = "Udp异步服务器",
                        playerAtk = 1,
                        playerDef = 1
                    }
                };

                serverSocket.Broadcast(msg);
            }
        }
    }
}