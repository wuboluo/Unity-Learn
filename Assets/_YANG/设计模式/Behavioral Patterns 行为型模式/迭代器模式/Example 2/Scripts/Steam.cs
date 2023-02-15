using System.Collections;
using System.Collections.Generic;

namespace Yang.DesignPattern.Iterator.Example2
{
    public class Steam : IGameIterator
    {
        private readonly List<GameInfo> _games;

        public Steam()
        {
            _games = new List<GameInfo>();
        }

        public void AddGame(string name, int price, bool needNet)
        {
            GameInfo newGame = new GameInfo(name, price, needNet);
            _games.Add(newGame);
        }

        public IEnumerator GetIterator()
        {
            return _games.GetEnumerator();
        }
    }
}