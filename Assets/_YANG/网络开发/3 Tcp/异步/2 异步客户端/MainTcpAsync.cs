using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Yang.Net.S;

namespace Yang.Net.Tcp.Async
{
    public class MainTcpAsync : MonoBehaviour
    {
        public Button button;
        public Button btn1, btn2, btn3;

        // public InputField input;

        private void Start()
        {
            if (NetAsyncManager.Instance == null)
            {
                GameObject obj = new GameObject("NetAsyncManager");
                obj.AddComponent<NetAsyncManager>();
            }

            NetAsyncManager.Instance.Connect("127.0.0.1", 8000);

            // 正常发送消息
            button.onClick.AddListener(() =>
            {
                Example_PlayerMessage playerMsg = new Example_PlayerMessage
                {
                    playerID = 0,
                    playerData = new Example_PlayerData
                    {
                        playerName = "正常消息",
                        playerAtk = 0,
                        playerDef = 0
                    }
                };
                NetAsyncManager.Instance.Send(playerMsg);
            });

            // 分包消息测试
            btn1.onClick.AddListener(async () =>
            {
                Example_PlayerMessage msg = new Example_PlayerMessage
                {
                    playerID = 2,
                    playerData = new Example_PlayerData
                    {
                        playerName = "分包消息",
                        playerAtk = 2,
                        playerDef = 2
                    }
                };

                byte[] bytes = msg.Writing();
                byte[] bytes1 = new byte[10];
                byte[] bytes2 = new byte[bytes.Length - 10];

                // 分成两个包
                Array.Copy(bytes, 0, bytes1, 0, 10);
                Array.Copy(bytes, 10, bytes2, 0, bytes.Length - 10);

                // 发送
                NetAsyncManager.Instance.SendTest(bytes1);
                await Task.Delay(500);
                NetAsyncManager.Instance.SendTest(bytes2);
            });

            // 粘包消息测试
            btn2.onClick.AddListener(() =>
            {
                Example_PlayerMessage msg = new Example_PlayerMessage
                {
                    playerID = 31,
                    playerData = new Example_PlayerData
                    {
                        playerName = "粘包消息1",
                        playerAtk = 31,
                        playerDef = 31
                    }
                };
                Example_PlayerMessage msg2 = new Example_PlayerMessage
                {
                    playerID = 32,
                    playerData = new Example_PlayerData
                    {
                        playerName = "粘包消息2",
                        playerAtk = 32,
                        playerDef = 32
                    }
                };

                // 两个消息的字节长度和
                byte[] bytes = new byte[msg.GetBytesNumber() + msg2.GetBytesNumber()];
                // 将两个消息序列化的字节拷贝进 合并的字节数组中，注意第二条消息拷贝的起点，应为第一条消息的长度
                msg.Writing().CopyTo(bytes, 0);
                msg2.Writing().CopyTo(bytes, msg.GetBytesNumber());

                NetAsyncManager.Instance.SendTest(bytes);
            });

            // 分包粘包消息测试
            btn3.onClick.AddListener(async () =>
            {
                Example_PlayerMessage msg = new Example_PlayerMessage
                {
                    playerID = 41,
                    playerData = new Example_PlayerData
                    {
                        playerName = "分包粘包消息1",
                        playerAtk = 41,
                        playerDef = 41
                    }
                };
                Example_PlayerMessage msg2 = new Example_PlayerMessage
                {
                    playerID = 42,
                    playerData = new Example_PlayerData
                    {
                        playerName = "分包粘包消息2",
                        playerAtk = 42,
                        playerDef = 42
                    }
                };

                // 序列化
                byte[] bytes1 = msg.Writing();
                byte[] bytes2 = msg2.Writing();

                // 将消息2分包
                byte[] bytes2_1 = new byte[10];
                byte[] bytes2_2 = new byte[bytes2.Length - 10];
                Array.Copy(bytes2, 0, bytes2_1, 0, 10);
                Array.Copy(bytes2, 10, bytes2_2, 0, bytes2.Length - 10);

                // 消息1和消息2前半部分粘包
                byte[] bytes = new byte[bytes1.Length + bytes2_1.Length];
                bytes1.CopyTo(bytes, 0);
                bytes2_1.CopyTo(bytes, bytes1.Length);
                
                NetAsyncManager.Instance.SendTest(bytes);
                await Task.Delay(500);
                NetAsyncManager.Instance.SendTest(bytes2_2);
            });
        }
    }
}