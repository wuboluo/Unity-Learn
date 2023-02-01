using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CSharp_5_Task : MonoBehaviour
{
    public bool point2, point3, point4, point5, point6, point7;
    private CancellationTokenSource cts;

    private bool isRunning = true;

    private Task<int> t11, t22, t33;

    private void Start()
    {
        #region 知识点一：认识Task

        // 命名空间：System.Threading.Tasks
        // 类名：Task
        // Task顾名思义就是任务的意思
        // Task是在线程池基础上进行的改进，它拥有线程池的优点，同时解决了使用线程池不易控制的弊端
        // 它是基于线程池的优点对线程的封装，可以让我们更方便高效的进行多线程开发

        // 简单理解：
        // Task本质就是对线程Thread的封装，它的创建遵循线程池的优点，并且可以更方便的让我们控制线程
        // 一个Task对象就是一个线程

        #endregion

        #region 知识点二：创建无返回值的Task的三种方式

        if (point2)
        {
            // 1，直接new一个Task对象，传入委托函数并启动
            // new就等于从线程池中取出一个
            var t1 = new Task(TaskAction);
            t1.Start();

            // 2，通过Task中的Run静态方法传入委托函数
            // 通过Run创建的Task对象不需要再Start开启
            var t2 = Task.Run(TaskAction);

            // 3，通过Task.Factory中的StartNew静态方法传入委托函数
            // 同样不需要Start开启
            var t3 = Task.Factory.StartNew(TaskAction);
        }

        #endregion

        #region 知识点三：创建有返回值的Task

        if (point3)
        {
            // 和上面创建无返回值的方式一样，只不过添加一个返回类型的泛型

            // 1，new Task<T>
            t11 = new Task<int>(TaskFunc);
            t11.Start();

            // 2，Task.Run<T>
            t22 = Task.Run(TaskFunc);

            // 3，Task.Factory.StartNew<T>
            t33 = Task.Factory.StartNew(TaskFunc);

            // 获取返回值（Result）需要注意：
            // Result获取结果会阻塞线程
            // 即如果Task没有执行完成，会等待Task执行完毕后，再获取Result
            // 如果Task中是一个死循环（正如上面的例子），那么主线程就会停在下面这行获取Result处。此时主线程就会卡死（Unity无响应）
            // print(t11.Result);
        }

        #endregion

        #region 知识点四：同步执行Task

        if (point4)
        {
            // 上面都是由线程去开启的逻辑，并不会影响下面这行主线程的逻辑。这就是异步的体现
            // print("主线程执行");

            // 如果希望Task能够同步执行
            // 只需要调用Task对象中的 RunSynchronously方法。需要使用 new Task()的方式，因为 Run或StartNew在创建时就会自动启动
            var t4 = new Task(() =>
            {
                Thread.Sleep(1000);
                print("hhh");
            });

            // 异步执行：先执行主线程的内容（"主线程执行"），等待1s后执行t4（hhh）
            // t4.Start();
            // print("主线程执行");

            // 同步执行：等待1s后执行t4（hhh），再执行主线程的内容（"主线程执行"）
            t4.RunSynchronously();
            print("主线程执行");
        }

        #endregion

        #region 知识点五：Task中线程阻塞的方式（任意阻塞）

        if (point5)
        {
            // 1，Wait方法：等待任务执行完毕，再执行后面的内容
            var t5 = Task.Run(() =>
            {
                for (var i = 0; i < 5; i++) print("--- " + i);
            });
            var t6 = new Task(() =>
            {
                for (var i = 0; i < 10; i++) print("*** " + i);
            });

            // 看起来很像"同步执行"，会先把t5的内容执行完，再执行后续的内容（主线程+t6）
            // 和同步执行的区别是，同步执行只能是由new创建的才可以
            t5.Wait();
            t6.Start();

            // 2，WaitAny静态方法：传入的任意一个任务结束后，就继续执行后面的内容
            Task.WaitAny(t5, t6);

            // 3，WaitAll静态方法：传入的所有任务结束后，才继续执行后面的内容
            Task.WaitAll(t5, t6);

            print("主线程执行");
        }

        #endregion

        #region 知识点六：Task完成后继续其它Task（任务延续）

        if (point6)
        {
            var t7 = Task.Run(() =>
            {
                for (var i = 0; i < 5; i++) print("--- " + i);
            });
            var t8 = Task.Run(() =>
            {
                for (var i = 0; i < 10; i++) print("*** " + i);
            });

            // 1，Task.WhenAll(t1,t2...tn).ContinueWith(newTask)：当传入的t1~tn任务都完成后，再执行某任务
            Task.WhenAll(t7, t8).ContinueWith(ContinuationAction);
            // 合并写法：
            Task.Factory.ContinueWhenAll(new[] {t7, t8}, ContinuationAction);

            // 2，Task.WhenAny(t1,t2...tn).ContinueWith(newTask)：当传入的t1~tn任务任意一个完成后，再执行某任务
            // 和上面All一样
            Task.WhenAny(t7, t8).ContinueWith(ContinuationAction);
            Task.Factory.ContinueWhenAny(new[] {t7, t8}, ContinuationAction);
        }

        #endregion

        #region 知识点七：取消Task执行

        if (point7)
        {
            // 1，通过bool标识，控制线程内死循环的结束
            // 上文用到的isRunning

            // 2，通过CancellationTokenSource取消标识源类，来控制
            cts = new CancellationTokenSource();

            // 延迟取消
            cts.CancelAfter(5000);

            // 取消回调
            cts.Token.Register(() => { print("任务取消了"); });

            Task.Run(() =>
            {
                var i = 0;
                // cts.Cancel() 会让cts.IsCancellationRequested变为true
                while (!cts.IsCancellationRequested)
                {
                    print(i);
                    i++;
                    Thread.Sleep(1000);
                }
            });
        }

        #endregion

        #region 总结

        // 1，Task是基于Thread的封装
        // 2，Task类可以有返回值，Thread没有
        // 3，Task类可以执行后续操作，Thread不能
        // 4，Task可以更方便的取消任务，Thread相对更加单一
        // 5，Task具备ThreadPool的优点，更节约性能
        
        #endregion
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            // isRunning = false;
            // print(t11.Result);
            // print(t22.Result);
            // print(t33.Result);

            cts.Cancel();
    }

    private void OnDestroy()
    {
        isRunning = false;
    }

    private void TaskAction()
    {
        var i = 0;
        while (isRunning)
        {
            print("无返回值：" + i);
            i++;
            Thread.Sleep(1000);
        }
    }

    private int TaskFunc()
    {
        var i = 0;
        while (isRunning)
        {
            print("有返回值：" + i);
            i++;
            Thread.Sleep(1000);
        }

        return i;
    }

    private void ContinuationAction(Task t)
    {
        print("在t7和t8任务完成后，开启了一个新的任务");
        var i = 0;
        while (isRunning)
        {
            print(i);
            ++i;
            Thread.Sleep(1000);
        }
    }

    private void ContinuationAction(Task[] ts)
    {
        ContinuationAction(ts[0]);
    }
}