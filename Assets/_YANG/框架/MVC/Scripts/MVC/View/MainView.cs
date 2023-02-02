using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yang.MVC
{
    public class MainView : MonoBehaviour
    {
        public TextMeshProUGUI numberText;
        public Button addButton;

        // 只负责 view 值的更改
        public void UpdateData(MainModelSO data)
        {
            numberText.text = data.number.ToString();
        }
    }
}