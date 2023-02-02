using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Yang.LoadScene
{
    public class SceneA : MonoBehaviour
    {
        public Button loadSceneBtn;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            loadSceneBtn.onClick.AddListener(LoadSceneAsync);
        }

        private void LoadSceneAsync()
        {
            SceneManager.LoadSceneAsync("SceneB", LoadSceneMode.Additive).completed += OnLoadSceneAsyncComplete;
        }

        private static void OnLoadSceneAsyncComplete(AsyncOperation obj)
        {
        }
    }
}