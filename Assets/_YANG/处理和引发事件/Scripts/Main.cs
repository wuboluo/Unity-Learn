using UnityEngine;
using UnityEngine.UI;

namespace Yang.Event
{
    // https://learn.microsoft.com/zh-cn/dotnet/standard/events/

    // 此示例可以理解为：
    // Counter：我
    // Main：报亭老板
    // ThresholdReachedEventArgs：我需要发给报亭老板的信息“我订了几天，哪天到期的”
    // 报亭老板每天都给我发一份报纸，并且告诉我到期的时候给他发短信，短信内容为“一共收到多少份报纸，哪天到期的”
    // 当我一共收到我订的所有报纸后，给老板发短信“一共xx张，今天到期了”

    /// EventReceiver 事件接收方
    public class Main : MonoBehaviour
    {
        public Button addButton;
        private Counter _counter;

        private void Start()
        {
            int threshold = Random.Range(0, 10);
            _counter = new Counter(threshold);

            // 事件处理程序方法订阅该事件
            _counter.ThresholdReached += ThresholdReached;

            addButton.onClick.AddListener(Add);
        }

        private void Add()
        {
            print("Add 1");
            _counter.Add(1);
        }

        // 委托 EventHandler没有返回值，并且包含两个参数（事件源的对象，事件数据的对象）
        // 事件处理程序方法，要和正在处理的事件的委托签名匹配
        private void ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            print($"在 {e.TimeReached} 时，达到阈值：{e.Threshold}");
        }
    }
}