using UnityEngine;

public class Relationship_CSharp_Unity : MonoBehaviour
{
    private void Start()
    {
        #region 知识点一：各Unity版本支持的C#版本

        // Unity 2021.2 —— C# 9
        // Unity 2020.3 —— C# 8
        // Unity 2019.4 —— C# 7.3
        // Unity 2017   —— C# 6
        // Unity 5.5    —— C# 4

        #endregion

        #region 知识点二：为什么不同Unity版本支持的C#版本不同？

        // 主要是因为不用Unity版本使用的 C#编译器和脚本运行时版本不同

        // 比如：Unity2020.3使用的脚本运行时版本等效于.Net 4.6，编译器为Roslyn（罗斯林编译器）
        // 随着Unity的更新，一般会采用较新的编译器和运行时版本
        // 新版本的脚本运行时将为Unity带来大量的新版C#功能和.Net的功能，也就意味着可以支持更高版本的C#

        #endregion

        #region 知识点三：不同版本的C#对我们来说有什么意义？

        // 新功能往往带来更简洁的代码，方便的语法糖和更高级的用法
        // 节约代码量

        #endregion

        #region 知识点四：Unity的.Net API兼容级别

        // PlayerSettings-OtherSettings-Api Compatibility Level 设置API兼容级别
        // 官方文档：https://learn.microsoft.com/zh-cn/dotnet/standard/net-standard?tabs=net-standard-1-0

        // .Net 4.x（特殊需求时）
        // 具备较为完整的.Net API，甚至包括一些无法跨平台的API
        // 如果应用主要针对Windows平台，并且会用到.Net Standard 2.0中没有的功能，可以使用

        // .Net Standard 2.0（建议使用）
        // 标准的.Net API合集，相对4.x包含更少的内容，可以减小最终可执行文件大小
        // 具备更好的跨平台支持

        // .Net Standard 2.0配置文件大小是.Net 4.x的一半
        // 一般使用.Net Standard 2.0

        #endregion
    }
}