using UnityEngine;

namespace MVC
{
    public class MainController : MonoBehaviour
    {
        public MainModelSO mainModel;
        public UpdateNumberChannelSO mainModelChannel;
        public bool useCSharpEvent;

        private MainView _mainView;

        private void Start()
        {
            mainModel.number = 0;

            _mainView = GetComponent<MainView>();
            _mainView.UpdateData(mainModel);

            // 对 view 的操作通知 controller
            _mainView.addButton.onClick.AddListener(() => AddNumberOnClick(useCSharpEvent));

            if (useCSharpEvent)
                mainModel.UpdateEventChannel += UpdateInfo;
            else
                mainModelChannel.OnEventRaised += UpdateInfo;
        }

        private void OnDestroy()
        {
            if (useCSharpEvent)
                mainModel.UpdateEventChannel -= UpdateInfo;
            else
                mainModelChannel.OnEventRaised -= UpdateInfo;
        }

        // controller 更新 view
        private void UpdateInfo(MainModelSO data)
        {
            _mainView.UpdateData(data);
        }

        // controller 更新 model
        private void AddNumberOnClick(bool isCSharpEvent)
        {
            mainModel.AddNumber(isCSharpEvent);
        }
    }
}