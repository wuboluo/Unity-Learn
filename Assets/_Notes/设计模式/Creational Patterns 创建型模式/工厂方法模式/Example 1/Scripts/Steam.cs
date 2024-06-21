namespace Yang.DesignPattern.FactoryMethod.Example1
{
    public class Steam
    {
        public static Game DownloadGame(Games gameType)
        {
            return gameType switch
            {
                Games.PUBG => new PUBG(),
                Games.Forest => new Forest(),
                Games.GTA => new GTA(),
                _ => null
            };
        }
    }
}