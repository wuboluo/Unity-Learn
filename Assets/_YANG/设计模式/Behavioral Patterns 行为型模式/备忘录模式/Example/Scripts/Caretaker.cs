using System.Collections.Generic;

namespace Yang.DesignPattern.Memento.Example
{
    // 制作游戏存档，将 List ——> Dictionary，记录存档标签和数据快照
    // 玩家通过选择标签进入不同存档
    // TODO：查看【麦田物语】存档方式，看看是不是备忘录模式
    
    public class Caretaker
    {
        private readonly List<Memento> _savedArticles = new();

        public void Add(Memento memento)
        {
            _savedArticles.Add(memento);
        }

        public Memento Get(int index)
        {
            return _savedArticles[index];
        }

        public int GetCountOfSavedArticles()
        {
            return _savedArticles.Count;
        }
    }
}