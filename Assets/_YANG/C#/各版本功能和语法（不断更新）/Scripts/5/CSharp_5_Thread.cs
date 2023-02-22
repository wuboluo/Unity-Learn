using System.Threading;
using UnityEngine;

namespace Yang.CSharp.VariousVersionsGrammar
{
    public class CSharp_5_Thread : MonoBehaviour
    {
        public bool threadContent;
        public bool threadPoolContent;
        private Thread thread;

        private void Start()
        {
            #region 知识点一：C#5的新增功能和语法有哪些？

            // 1，调用方信息特性（C#进阶-特性）
            // 2，异步方法async和await

            // 学习async和await之前需要补充知识点：
            // 1，线程和线程池
            // 2，Task类

            #endregion

            #region 知识点二：回顾——线程

            // 1，Unity支持多线程
            // 2，Unity中开启的多线程不能使用主线程的对象
            // 3，Unity中开启多线程之后一定记住关闭

            if (threadContent)
            {
                print("Unity主线程执行");
                thread = new Thread(ThreadStart);
                thread.Start();
            }

            #endregion

            #region 知识点三：补充——线程池

            // 命名空间：System.Threading
            // 类名：ThreadPool（线程池）

            // 在多线程的应用程序开发中，频繁的创建删除线程会带来性能消耗，产生内存垃圾
            // 为了避免这种开销C#推出了线程池ThreadPool类

            // ThreadPool中有若干数量的线程，如果有任务需要处理时，会从线程池中获取一个空闲的线程来执行任务
            // 任务执行完毕后线程不会销毁，而是被线程池回收以供后续任务使用
            // 当线程池中所有的线程都在忙碌时，又有新任务要处理时，线程池才会新建一个线程来处理该任务
            // 如果线程池数量达到设置的最大值时，任务会排队，等待其它任务释放线程后再执行
            // 线程池能减少线程的创建，节省开销，可以减少GC垃圾回收的触发

            // 线程池相当于就是一个专门装线程的缓存池
            // 优点：节省开销，减少线程的创建，进而有效减少GC触发
            // 缺点：不能控制线程池中线程的执行顺序，也不能获取线程池中线程取消/异常/完成的通知


            if (threadPoolContent)
            {
                // ThreadPool是一个静态类
                // 1，获取可用的工作线程数和I/O线程数
                ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);
                print(workerThreads);
                print(completionPortThreads);

                // 2，更改线程池中工作线程的最大/最小数目和I/O线程的最大数目
                // 大于次数的请求将保持排队状态，直到线程池线程变为可用
                // 更改成功返回true，失败返回false
                if (ThreadPool.SetMaxThreads(20, 20)) print("更改最大数目成功");
                if (ThreadPool.SetMinThreads(5, 5)) print("更改最小数目成功");

                // 3，获取线程池中工作线程的最大/最小数目和I/O线程的最大数目
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
                print(maxWorkerThreads);
                print(maxCompletionPortThreads);

                ThreadPool.GetMinThreads(out int minWorkerThreads, out int minCompletionPortThreads);
                print(minWorkerThreads);
                print(minCompletionPortThreads);

                // 4，将方法排入队列以便执行，当线程池中线程变得可用时执行
                // 这里就能看出缺点，执行顺序不能控制
                for (int i = 0; i < 10; i++)
                    ThreadPool.QueueUserWorkItem(obj =>
                    {
                        // obj就是传入的第二个参数
                        print($"第{obj}个任务");
                    }, i);

                print("主线程执行");
            }

            #endregion

            #region 总结

            // 线程池就相当于C#提供的线程对象池
            // 优点：提高性能，节约内存
            // 缺点：不能控制池内线程的执行顺序，不能获取线程取消/异常/完成的通知

            #endregion
        }

        private void OnDestroy()
        {
            if (threadContent)
                // 停止线程
                thread.Abort();
        }

        private void ThreadStart()
        {
            while (true)
            {
                print("1");
                Thread.Sleep(1000);
            }
        }
    }
}