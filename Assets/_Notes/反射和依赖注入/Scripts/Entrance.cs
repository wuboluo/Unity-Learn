using UnityEngine;

namespace Yang.DependencyInjection_Reflection
{
    public class Entrance : MonoBehaviour
    {
        private void Start()
        {
            IShowInfo button = ReflectionFactory.MakeButton();
            Debug.Log($"按钮颜色为：{button.ShowInfo()}");

            IShowInfo text = ReflectionFactory.MakeText();
            Debug.Log($"文本颜色为：{text.ShowInfo()}");
        }
    }
}