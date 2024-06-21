using UnityEngine;

namespace Yang.Net.FTP
{
    public class Main : MonoBehaviour
    {
        private void Start()
        {
            // Upload();
            // Download();
            // Delete();
            // GetFileSize();
            // CreateDirectory();
            // GetFileList();

            print("Main Start");
        }

        private void Upload()
        {
            FtpManager.UploadFile("YangPic.png", Application.streamingAssetsPath + "/1.png", () => { print("上传完成委托函数"); });
        }

        private void Download()
        {
            FtpManager.DownloadFile("Icon.jpg", Application.persistentDataPath + "/XXX.png", () =>
            {
                print(Application.persistentDataPath);
                print("下载完成委托函数");
            });
        }

        private void Delete()
        {
            FtpManager.DeleteFile("YangPic.png", result => { print(result ? "删除成功委托函数" : "删除失败委托函数"); });
        }

        private void GetFileSize()
        {
            FtpManager.GetFileSize("Icon.jpg", length => { print($"文件大小为：{length}"); });
        }

        private void CreateDirectory()
        {
            FtpManager.CreateDirectory("ABC", result => { print(result ? "创建文件夹成功委托函数" : "创建文件夹失败委托函数"); });
        }

        private void GetFileList()
        {
            FtpManager.GetFileList("", list =>
            {
                if (list == null)
                {
                    print("获取文件列表失败");
                    return;
                }

                foreach (string fileName in list) print(fileName);
            });
        }
    }
}