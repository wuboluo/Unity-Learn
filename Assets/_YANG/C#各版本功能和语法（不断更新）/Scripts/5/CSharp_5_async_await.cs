using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CSharp_5_async_await : MonoBehaviour
{
    private CancellationTokenSource cts;
    
    private void Start()
    {
        #region 知识点一：什么是同步和异步？

        // 同步和异步主要用于修饰方法

        // 同步方法：
        // 当一个方法被调用时，调用者需要等待该方法执行完毕后才能继续执行
        // 异步方法：
        // 当一个方法被调用时立刻返回，并获取一个线程执行该方法内部的逻辑，调用者不用等待该方法执行完毕

        // 简单理解异步编程：
        // 我们会把一些不需要立刻得到结果且耗时的逻辑设置为异步执行，这样可以提高程序的运行效率
        // 避免由于复杂逻辑带来的线程阻塞

        #endregion

        #region 知识点二：什么时候需要异步编程？

        // 需要处理的逻辑会严重影响主线程执行的流畅性时，需要使用异步编程
        // 比如：
        // 1，复杂逻辑计算（AStar）
        // 2，网络下载，网络通讯
        // 3，资源加载
        // ...等等

        #endregion

        #region 知识点三：异步方法async和await

        // async和await一般需要配合使用
        // async用于修饰函数、Lambda表达式、匿名函数
        // await用于在函数中和async配对使用，主要作用是等待某个逻辑结束
        // 当执行到await的这行代码时，逻辑会返回函数外部继续执行，直到等待的内容执行结束后，再继续执行异步函数内部的逻辑
        // 在一个async异步函数中可以有多个await等待关键字

        MethodAsync();
        print("主线程逻辑执行");

        // 例如：
        // 1，复杂逻辑计算（利用Task新开线程进行计算，计算完毕后再使用，比如寻路）
        CalcPathAsync(transform);
        // 2，计时器
        TimerAsync();
        // 3，资源加载（Addressable的资源异步加载是可以使用async和await的）
        
        
        // 注意：
        // Unity中大部分异步方法（例如Resources.LoadAsync()）是不支持async和await的，我们只有使用协同程序进行使用
        // 但是存在第三方插件，可以让这些方法支持异步关键字（Github：https://github.com/modesttree/Unity3dAsyncAwaitUtil）
        
        // 虽然Unity中的各种异步加载对异步方法支持不太好
        // 但是当用到.Net库中提供的一些API时（方法名后面带有Async的），可以考虑使用异步方法
        // 1，Web访问：HttpClient
        // 2，文件使用：StreamReader, StreamWriter, JsonSerializer, XmlReader, XmlWriter...等等
        // 3，图像处理：BitmapEncoder, BitmapDecoder
        
        #endregion
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cts.Cancel();
        }
    }

    // 使用async修饰异步方法注意事项：
    // 1，在异步方法中使用await关键字（不适用编译器会警告但不会报错），否则异步方法会以同步方式执行
    // 2，异步方法名称建议以Async结尾
    // 3，异步方法返回值只能是void,Task,Task<T>
    // 4，异步方法中不能声明使用ref和out修饰的变量
    private async void MethodAsync()
    {
        print("进入异步方法");

        // 使用await等待异步内容执行完毕（一般配合Task使用）

        // 遇到await关键字时：
        // 1，异步方法将被挂起
        // 2，将控制权返回给调用者
        // 3，当await修饰内容异步执行结束后，继续通过调用者线程执行后面内容
        await Task.Run(() => { Thread.Sleep(3000); });

        print("异步方法后面的逻辑");
    }

    private async void CalcPathAsync(Transform t)
    {
        print("开始处理寻路逻辑");

        int value = 10;
        await Task.Run(() =>
        {
            // 处理复杂逻辑计算，此处使用休眠模拟
            Thread.Sleep(1000);
            value = 50;
            
            // 注意：这里面是多线程的部分，不能使用主线程的内容
        });

        t.position = new Vector2(value, value);
        print("寻路计算完毕，value=" + value);
    }

    private async void TimerAsync()
    {
        cts = new CancellationTokenSource();
        int i = 0;
        while (!cts.IsCancellationRequested)
        {
           print(i);
           await Task.Delay(1000);
           i++;
        }
    }
}