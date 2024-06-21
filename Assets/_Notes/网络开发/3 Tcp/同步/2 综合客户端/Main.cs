using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Yang.Net.S;

namespace Yang.Net.Tcp.Sync
{
    public class Main : MonoBehaviour
    {
        public Button button;

        // 测试分包粘包按钮
        public Button btn1, btn2, btn3;

        private void Start()
        {
            if (TcpNetManager.Instance == null)
            {
                GameObject obj = new GameObject("NetAsyncManager");
                obj.AddComponent<TcpNetManager>();
            }

            TcpNetManager.Instance.ConnectServer("127.0.0.1", 8000);

            button.onClick.AddListener(() =>
            {
                Example_PlayerMessage playerMsg = new Example_PlayerMessage
                {
                    playerID = 101,
                    playerData = new Example_PlayerData
                    {
                        playerName = "Client-Yang",
                        playerAtk = 10,
                        playerDef = 10
                    }
                };

                TcpNetManager.Instance.Send(playerMsg);
            });

            // 分包
            btn1.onClick.AddListener(async () =>
            {
                Example_PlayerMessage playerMsg1 = new Example_PlayerMessage
                {
                    playerID = 1,
                    playerData = new Example_PlayerData
                    {
                        playerName = "Client-分包测试信息",
                        playerAtk = 1,
                        playerDef = 1
                    }
                };

                // 手动分包
                byte[] bytes = playerMsg1.Writing();
                byte[] bytes1 = new byte[10];
                byte[] bytes2 = new byte[bytes.Length - 10];
                // 给两个分包添加数据
                Array.Copy(bytes, 0, bytes1, 0, 10);
                Array.Copy(bytes, 10, bytes2, 0, bytes.Length - 10);

                TcpNetManager.Instance.SendTest(bytes1);
                await Task.Delay(500); // 500ms
                TcpNetManager.Instance.SendTest(bytes2);
            });

            // 粘包
            btn2.onClick.AddListener(() =>
            {
                Example_PlayerMessage playerMsg1 = new Example_PlayerMessage
                {
                    playerID = 1,
                    playerData = new Example_PlayerData
                    {
                        playerName = "Client-粘包测试信息1",
                        playerAtk = 1,
                        playerDef = 1
                    }
                };
                Example_PlayerMessage playerMsg2 = new Example_PlayerMessage
                {
                    playerID = 2,
                    playerData = new Example_PlayerData
                    {
                        playerName = "Client-粘包测试信息2",
                        playerAtk = 2,
                        playerDef = 2
                    }
                };

                // 手动粘包
                byte[] bytes = new byte[playerMsg1.GetBytesNumber() + playerMsg2.GetBytesNumber()];
                playerMsg1.Writing().CopyTo(bytes, 0);
                playerMsg2.Writing().CopyTo(bytes, playerMsg1.GetBytesNumber());

                TcpNetManager.Instance.SendTest(bytes);
            });

            // 分包粘包
            btn3.onClick.AddListener(Call);
        }

        private static async void Call()
        {
            Example_PlayerMessage playerMsg1 = new Example_PlayerMessage { playerID = 1, playerData = new Example_PlayerData { playerName = "Client-分包粘包测试信息1", playerAtk = 1, playerDef = 1 } };
            Example_PlayerMessage playerMsg2 = new Example_PlayerMessage { playerID = 2, playerData = new Example_PlayerData { playerName = "Client-分包粘包测试信息2", playerAtk = 2, playerDef = 2 } };

            byte[] bytes1 = playerMsg1.Writing(); // 消息A
            byte[] bytes2 = playerMsg2.Writing(); // 消息B

            // 手动分包
            byte[] bytes2_1 = new byte[10];
            byte[] bytes2_2 = new byte[bytes2.Length - 10];
            // 给两个分包添加数据
            Array.Copy(bytes2, 0, bytes2_1, 0, 10);
            Array.Copy(bytes2, 10, bytes2_2, 0, bytes2.Length - 10);

            // 消息A和消息B前部分的粘包
            byte[] bytes = new byte[bytes1.Length + bytes2_1.Length];
            bytes1.CopyTo(bytes, 0);
            bytes2_1.CopyTo(bytes, bytes1.Length);

            TcpNetManager.Instance.SendTest(bytes);
            await Task.Delay(500); // 500ms
            TcpNetManager.Instance.SendTest(bytes2_2);
        }
    }
}