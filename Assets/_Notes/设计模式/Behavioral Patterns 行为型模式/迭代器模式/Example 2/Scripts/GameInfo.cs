namespace Yang.DesignPattern.Iterator.Example2
{
    public class GameInfo
    {
        public GameInfo(string gameName, int gamePrice, bool needNet)
        {
            GameName = gameName;
            GamePrice = gamePrice;
            NeedNet = needNet;
        }

        private string GameName { get; }
        private int GamePrice { get; }
        private bool NeedNet { get; }

        public string GetGameInfo()
        {
            return $"名称：{GameName}，价格：{GamePrice}，是否需要连网：{NeedNet}";
        }
    }
}