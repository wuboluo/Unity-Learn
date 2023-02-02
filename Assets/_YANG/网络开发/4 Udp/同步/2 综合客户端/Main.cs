using UnityEngine;
using UnityEngine.UI;
using Yang.Net.S;

namespace Yang.Net.Udp.Sync
{
    public class Main : MonoBehaviour
    {
        public Button button;

        private void Start()
        {
            if (UdpNetManager.Instance == null)
            {
                var obj = new GameObject("UdpNetManager");
                obj.AddComponent<UdpNetManager>();
            }

            UdpNetManager.Instance.StartClient("127.0.0.1", 8000);

            button.onClick.AddListener(() =>
            {
                Example_PlayerMessage playerMsg = new Example_PlayerMessage
                {
                    playerID = 200,
                    playerData = new Example_PlayerData
                    {
                        playerName = "这里是Udp客户端",
                        playerAtk = 200,
                        playerDef = 200
                    }
                };

                UdpNetManager.Instance.Send(playerMsg);
            });
        }
    }
}