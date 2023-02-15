using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace Yang.AssetLoading.AssetBundle
{
    public class FileCopier
    {
        /// 复制文件 StreamingPath —> PersistentPath
        [Obsolete("Obsolete")]
        public static void CopyToPersistentFromStreaming(MonoBehaviour mono, string fileName, Action overCallback)
        {
            mono.StartCoroutine(CopyToPersistentFromStreamingCor(fileName, overCallback));
        }

        [Obsolete("Obsolete")]
        private static IEnumerator CopyToPersistentFromStreamingCor(string fileName, Action overCallback)
        {
            string streamingPath = Application.streamingAssetsPath + "/" + fileName;
            string persistentPath = Application.persistentDataPath + "/" + fileName;

            WWW www = new WWW(streamingPath);
            yield return www;

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("copy www.error:" + www.error);
            }
            else
            {
                if (File.Exists(persistentPath)) File.Delete(persistentPath);

                using FileStream fs = File.Create(persistentPath);
                fs.Write(www.bytes, 0, www.bytes.Length);
                fs.Flush();
                fs.Close();
            }

            www.Dispose();
            overCallback?.Invoke();
        }
    }
}