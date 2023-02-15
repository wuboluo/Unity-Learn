using System.Collections;
using UnityEngine;

namespace Yang.DesignPattern.Iterator.Example2
{
    public class IteratorPatternExample2 : MonoBehaviour
    {
        private void Start()
        {
            Steam steam = new Steam();
            steam.AddGame("PUBG", 98, true);
            steam.AddGame("Forest", 38, false);
            steam.AddGame("GTA V", 238, true);

            IEnumerator steamIterator = steam.GetIterator();
            while (steamIterator.MoveNext())
            {
                if (steamIterator.Current is GameInfo info)
                    Debug.Log(info.GetGameInfo());
            }
        }
    }
}