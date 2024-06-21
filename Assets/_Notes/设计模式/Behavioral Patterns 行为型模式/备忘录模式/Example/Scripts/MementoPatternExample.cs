using UnityEngine;

namespace Yang.DesignPattern.Memento.Example
{
    public class MementoPatternExample : MonoBehaviour
    {
        private readonly Caretaker _caretaker = new();
        private readonly Originator _originator = new();

        private int _currentArticle;
        private int _savedFiles;

        private void Start()
        {
            Save("Text 1");
            Save("Text 2");
            Save("Text 3");
            Save("Text 4");
            Save("Text 5");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) print(Undo());
            if (Input.GetKeyDown(KeyCode.RightArrow)) print(Redo());
        }

        // 存档
        private void Save(string text)
        {
            // 发起者新建一条文本，并创建为一条备忘录，由管家保管起来
            _originator.Set(text);
            _caretaker.Add(_originator.StoreInMemento());

            _savedFiles = _caretaker.GetCountOfSavedArticles();
            _currentArticle = _savedFiles - 1;
        }

        // 撤销
        private string Undo()
        {
            if (_currentArticle >= 0) _currentArticle -= 1;
            if (_currentArticle < 0) return "已是首条";

            // 通过管家获得上一条备忘录
            Memento prev = _caretaker.Get(_currentArticle);
            // 将发起者的内容更新为得到的这条备忘录的内容
            string prevArticle = _originator.RestoreFromMemento(prev);
            return prevArticle;
        }

        // 重做
        private string Redo()
        {
            if (_currentArticle < _savedFiles) _currentArticle += 1;
            if (_currentArticle >= _savedFiles) return "已是末条";

            // 通过管家获得下一条备忘录
            Memento next = _caretaker.Get(_currentArticle);
            // 将发起者的内容更新为得到的这条备忘录的内容
            string nextArticle = _originator.RestoreFromMemento(next);
            return nextArticle;
        }
    }
}