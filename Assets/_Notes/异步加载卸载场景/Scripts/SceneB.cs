using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Yang.LoadScene
{
    public class SceneB : MonoBehaviour
    {
        public Button loadSceneBtn;

        private void Start()
        {
            loadSceneBtn.onClick.AddListener(UnloadSceneAsync);
        }

        private void OnDestroy()
        {
            loadSceneBtn.onClick.RemoveListener(UnloadSceneAsync);
        }

        private void UnloadSceneAsync()
        {
            SceneManager.UnloadSceneAsync("SceneB").completed += OnUnloadSceneAsyncComplete;
        }

        private static void OnUnloadSceneAsyncComplete(AsyncOperation obj)
        {
            Debug.Log("SceneB 异步卸载完成");
        }
    }
}