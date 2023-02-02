using System;
using UnityEngine;

namespace Yang.Event
{
    /// EventSender 事件发送方
    public sealed class Counter
    {
        // 阈值
        private readonly int _threshold;
        // 当前的值
        private int _value;

        public Counter(int threshold)
        {
            _threshold = threshold;
            Debug.Log($"随机的阈值：{_threshold}");
        }

        // 事件往往都是发送方的成员
        // 在声明事件中包括委托类型来将委托和事件相关联
        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;

        // 具体引发事件的方法
        public void Add(int increment)
        {
            _value += increment;
            if (_value >= _threshold)
            {
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs
                {
                    Threshold = _threshold,
                    TimeReached = DateTime.Now
                };
                OnThresholdReached(args);
            }
        }

        // 引发事件的方法通常会被标记为 protected virtual，以允许派生类重写引发事件
        // 派生类应始终调用基类的 OnEventName方法，以确保注册的委托接收事件
        private void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            // 1，委托是一种保存对方法的引用的类型，是通过显示所引用方法的返回类型和参数的签名来声明的
            // 2，委托是事件源和处理事件的代码之间的媒介
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            handler?.Invoke(this, e);
        }
    }
}