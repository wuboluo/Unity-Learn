using System.Collections.Generic;

namespace Yang.DesignPattern.Visitor.Example
{
    public class Steam
    {
        private readonly List<Game> _games = new();

        /// 附加
        public void Attach(Game game)
        {
            _games.Add(game);
        }

        /// 脱离
        public void Detach(Game game)
        {
            _games.Remove(game);
        }

        /// 接收
        public void Accept(IVisitor visitor)
        {
            foreach (Game g in _games) g.Accept(visitor);
        }
    }
}