using System;

namespace Yang.Event
{
    // 自定义事件数据类，派生自 EventArgs，通常所有事件数据类以-EventArgs结尾命名
    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold { get; set; }

        public DateTime TimeReached { get; set; }
    }
}