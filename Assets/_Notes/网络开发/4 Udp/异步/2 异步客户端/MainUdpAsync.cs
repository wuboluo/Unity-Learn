using UnityEngine;
using UnityEngine.UI;
using Yang.Net.S;

namespace Yang.Net.Udp.Async
{
    public class MainUdpAsync : MonoBehaviour
    {
        public Button button;

        private void Start()
        {
            if (UdpNetAsyncManager.Instance == null)
            {
                GameObject obj = new GameObject("UdpNetAsyncManager");
                obj.AddComponent<UdpNetAsyncManager>();
            }

            UdpNetAsyncManager.Instance.StartClient("127.0.0.1", 8000);

            button.onClick.AddListener(() =>
            {
                Example_PlayerMessage msg = new Example_PlayerMessage
                {
                    playerID = 1,
                    playerData = new Example_PlayerData
                    {
                        playerName = "Udp异步客户端",
                        playerAtk = 1,
                        playerDef = 1
                    }
                };
                UdpNetAsyncManager.Instance.SendAsync(msg);
            });
        }
    }
}