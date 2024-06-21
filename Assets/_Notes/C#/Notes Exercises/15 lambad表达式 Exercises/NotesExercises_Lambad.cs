using System;

namespace Yang.CSharp.Notes.Exercises
{
    internal class NotesExercises_Lambad
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("lambad表达式 练习题");

            GetFun()();
            GetFun2()();
            GetFun3()();
        }

        #region 练习题

        //有一个函数，会返回一个委托函数，这个委托函数中只有一句打印代码
        //之后执行返回的委托函数时，可以打印出1 ~10
        private static Action GetFun()
        {
            Action action = null;
            for (int i = 1; i <= 10; i++)
            {
                int index = i;
                action += () => { Console.WriteLine(index); };
            }

            return action;
        }

        private static Action GetFun2()
        {
            Action action = null;
            for (int i = 1; i <= 10; i++)
            {
                int index = i;
                action += delegate { Console.WriteLine(index); };
            }

            return action;
        }

        private static Action GetFun3()
        {
            Action action = null;
            for (int i = 1; i <= 10; i++)
            {
                int index = i;
                action += () => { Show(index); };
            }

            return action;
        }

        private static void Show(int i)
        {
            Console.WriteLine(i);
        }

        #endregion
    }
}