using System;
using UnityEngine;

namespace Yang.MVC
{
    [CreateAssetMenu(menuName = "MVC/Model/MainModel")]
    public class MainModelSO : ScriptableObject
    {
        public UpdateNumberChannelSO mainModelChannel;
        public int number;

        public event Action<MainModelSO> UpdateEventChannel;

        public void AddNumber(bool useCSharpEvent)
        {
            number += 1;
            UpdateInfo(useCSharpEvent);
        }

        // 当 model 数据发生变化时通知 controller
        private void UpdateInfo(bool useCSharpEvent)
        {
            if (useCSharpEvent)
                UpdateEventChannel?.Invoke(this);
            else
                mainModelChannel.Raise(this);
        }
    }
}