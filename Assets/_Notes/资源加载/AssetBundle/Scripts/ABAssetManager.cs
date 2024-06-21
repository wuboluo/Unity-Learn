using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Yang.AssetLoading.AssetBundle
{
    public class ABAssetManager : MonoBehaviour
    {
        // 加载过的AB资源
        private Dictionary<string, ABInfo> _loadedAbAssets;

        // 实例出来的AB资源物体
        private List<GameObject> _loadedObjs;

        private void Awake()
        {
            _loadedAbAssets = new Dictionary<string, ABInfo>();
            _loadedObjs = new List<GameObject>();
        }

        /// 从服务器上下载资源
        public void DownloadAsset(string uri, string savePath, Action overCallback)
        {
            StartCoroutine(DownloadAssetCor(uri, savePath, overCallback));
        }

        /// 下载资源协程
        private static IEnumerator DownloadAssetCor(string uri, string savePath, Action overCallback)
        {
            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.isDone)
                {
                    byte[] results = request.downloadHandler.data;

                    // 判断下载路径所在文件夹是否存在，不存在就创建一个
                    string folder = savePath[..savePath.LastIndexOf(Path.DirectorySeparatorChar)];
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    // 写入文件
                    File.WriteAllBytes(savePath, results);

                    overCallback?.Invoke();
                }
            }
            else
            {
                Debug.Log(request.error);
            }
        }

        /// 检查本地是否存在指定资源，不存在则从本地加载
        public void LoadAsset(string path, string abName, Action<ABInfo> overCallback)
        {
            // 本地存在此AB资源
            if (CheckAssetExisting(abName)) overCallback?.Invoke(_loadedAbAssets[abName]);
            // 从本地加载
            else StartCoroutine(LoadAssetCor(path, abName, overCallback));
        }

        /// 加载资源协程
        private IEnumerator LoadAssetCor(string path, string abName, Action<ABInfo> overCallback)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, 1024 * 4, false);
            AssetBundleCreateRequest bundleLoadRequest = UnityEngine.AssetBundle.LoadFromStreamAsync(stream);
            yield return bundleLoadRequest;

            UnityEngine.AssetBundle loadedAssetBundle = bundleLoadRequest.assetBundle;
            if (null == loadedAssetBundle)
            {
                Debug.Log("AB is null, load failed");
                yield break;
            }

            Debug.Log("Download AB name: " + abName);
            AssetBundleRequest assetLoadRequest = loadedAssetBundle.LoadAssetAsync<GameObject>(abName);
            yield return assetLoadRequest;

            // 将此AB资源记录到字典中
            ABInfo info = new ABInfo
            {
                AbName = abName,
                Go = assetLoadRequest.asset as GameObject,
                Bundle = loadedAssetBundle
            };

            _loadedAbAssets.Add(abName, info);

            stream.Dispose();
            overCallback?.Invoke(info);
        }

        /// 将实例出来的所有物体记录下来
        public void RecordInstantiateObject(GameObject go)
        {
            _loadedObjs.Add(go);
        }

        /// 释放所有资源
        public void ReleaseAllAsset(Action overCallback)
        {
            // 销毁加载的资源
            foreach (GameObject go in _loadedObjs) Destroy(go);

            // 卸载AB资源
            foreach (KeyValuePair<string, ABInfo> info in _loadedAbAssets) info.Value.Bundle.Unload(true);

            _loadedObjs.Clear();
            _loadedAbAssets.Clear();
            Resources.UnloadUnusedAssets();

            overCallback?.Invoke();
        }

        /// 检查本地是否存在某个AB资源
        private bool CheckAssetExisting(string abName)
        {
            return _loadedAbAssets.ContainsKey(abName);
        }
    }
}