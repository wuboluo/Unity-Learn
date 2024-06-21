using UnityEngine;

namespace Yang.DesignPattern.FactoryMethod.Example1
{
    public class FactoryMethodExample1 : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Game game = Steam.DownloadGame(Games.PUBG);
                Download(game);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Game game = Steam.DownloadGame(Games.Forest);
                Download(game);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Game game = Steam.DownloadGame(Games.GTA);
                Download(game);
            }
        }

        private static void Download(Game game)
        {
            game.Download();
        }
    }
}