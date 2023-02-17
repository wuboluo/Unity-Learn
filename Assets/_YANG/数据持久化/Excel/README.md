### 准备工作
1. `Api Compatibility Level -- .Net Standard 2.1`（[.NET 配置文件支持](https://docs.unity3d.com/Manual/dotnetProfileSupport.html)）<br>
2. `Rider 2021.3.3 -- NeGet Tool Window` 搜索并安装 `EPPlus 4.5.3` 版本（高于4.5.3.3之后需要许可证）
3. 在资源管理器中找对应的 dll 文件，`.Net Standard 2.1` 对应的是 `net35-EPPlus.dll`，将此类库放入工程目录中
4. 另外需要 `I18N.dll` 和 `I18N.West.dll` 也导入工程目录中，在对应的编辑器安装目录 `Editor\Data\MonoBleedingEdge\lib\mono\unityjit`

![需要的 Dll](https://user-images.githubusercontent.com/57084810/216275370-fdea68b9-c30c-492c-a4bb-110b20811827.jpg)

***

### 常见报错
1. 各种 `Library` 中的东西被其它进程占用：可能是 `EPPlus` 没有许可证，建议使用 `4.5.3` 及以下版本
2. 打包报错：[NotSupportedException: Encoding 437 data could not be found](https://blog.csdn.net/qq_33789001/article/details/115369284)
3. 其它乱七八糟的报错可以把 `Library` 文件夹删除，重新打开项目重新加载一次
4. 获得列/行的数量时，当 Excel 清空单元格内容依旧查找到的问题。如果为 `null`，跳过即可

![columnCount](https://user-images.githubusercontent.com/57084810/198955239-53079be0-ff39-49de-9558-41de0cccf230.png)<br>
![判断是否为空单元格](https://user-images.githubusercontent.com/57084810/198955243-29b1b910-aef7-40fc-889d-19647884aa32.png)
