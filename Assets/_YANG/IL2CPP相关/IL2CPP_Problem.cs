using System.Collections.Generic;
using UnityEngine;

public class IL2CPP_Problem : MonoBehaviour
{
    private void Start()
    {
        #region 知识点一：安装Unity IL2CPP打包工具

        // Edit-ProjectSettings-Player-Configuration-ScriptingBackend-IL2CPP 
        // 如果当前版本的Unity没有安装IL2CPP打包相关工具，需要在UnityHub-安装-对应Unity版本-设置-添加模块-Windows/ Build Support(IL2CPP)

        #endregion

        #region 知识点二：IL2CPP打包存在的问题：类型裁剪

        // 问题：
        // IL2CPP在打包时，会自动对Unity工程的DLL进行裁剪，将代码中没有引用到的类型裁剪掉，以达到减小发布后包体大小的目的
        // 然而在实际使用过程中，很多类型有可能被意外裁剪掉，造成运行时抛出找不到某个类型的错误
        // 特别是通过反射等方式，在编译时无法得知的函数被调用，在运行时都可能会遇到上述问题

        // 解决方案：
        // 官方文档：https://docs.unity3d.com/2021.3/Documentation/Manual/ManagedCodeStripping.html

        // 1：设置托管代码剥离
        // Edit-ProjectSettings-Player-Optimization-ManagedStrippingLevel-Minimal
        //      （Disable：Mono模式才能设置为不删除代码。）
        //      Minimal：Unity仅在UnityEngine和.NET类库中搜索未使用的代码。Unity不会删除任何用户编写的代码。此设置最不可能导致任何意外的运行时行为。
        //      Low：Unity在一些用户编写的程序集和所有UnityEngine和.NET类库中搜索未使用的代码。使用一组规则最大限度地减少了意外后果的可能性，例如反射。
        //      Medium：Unity部分搜索所有程序集以查找无法访问的代码。
        //      High：Unity对所有程序集执行广泛搜索以查找无法访问的代码。优先考虑减小大小而不是代码稳定性，并尽可能多地删除代码。选择该模式一般需使用link.xml配合。

        // 2：使用Unity提供的link.xml
        // 在Unity工程的Assets目录中（或任何子目录中）建立一个叫link.xml的文件

        #endregion

        #region 知识点三：IL2CPP打包存在的问题：泛型

        // IL2CPP和Mono最大的区别是：不能再运行时动态生成代码和类型。
        // 泛型相关的内容，如果没有在打包之前显示使用一次想要的泛型类型，那么编译过后就会报找不到此类型的错。

        // 例如：
        // List<A>和List<B>中的 A 和 B 是自定义的类，必须在打包前显示调用过，IL2CPP才能保留它们。
        // 如果在热更新中使用了List<C>，但是它并没有在打包前显示使用，此时就会报错。
        // 主要原因就是 JIT和AOT 两个编译模式的不同造成的。
        // Mono：JIT (Just-In-Time - 实时编译）
        // IL2CPP：AOT (Ahead-Of-Time - 预先编译）
        List<A> listA = new List<A>();
        List<B> listB = new List<B>();

        // 解决方案：
        // 泛型类：声明一个类，然后在这个类中声明一些public的泛型类变量。
        // 泛型方法：随便写一个静态方法，在将这个泛型方法在其中调用一下。这个静态方法无需被调用。
        // 这样做的目的就是在预先编译之前让IL2CPP知道我们需要使用这些内容

        #endregion

        #region 总结

        // 对于目前开发的新项目，都建议使用 IL2CPP 的模式来进行打包
        // 主要原因是它的效率要高于 Mono，同时它自带裁剪功能，包体也会相对小一些
        // 在测试时出现无法识别类型的问题时，就需要根据上述的解决办法处理

        #endregion
    }
}

public class A
{
}

public class B
{
}

public class C
{
}

public class IL2CPP_Replenish
{
    public Dictionary<int, string> dic = new();
    public List<A> listA;
    public List<B> listB;
    public List<C> listC;

    public void Test<T>(T info)
    {
    }

    public static void StaticTest()
    {
        IL2CPP_Replenish replenish = new IL2CPP_Replenish();
        replenish.Test(1);
    }
}