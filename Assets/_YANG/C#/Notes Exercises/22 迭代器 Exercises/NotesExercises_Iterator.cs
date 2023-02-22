using System;
using System.Collections;

namespace Yang.CSharp.Notes.Exercises
{
    #region 练习题

    //请为一个自定义类
    //用两种方法让其可以被foreach遍历

    internal class CustomList : IEnumerable
    {
        private readonly int[] list;

        public CustomList()
        {
            list = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < list.Length; i++) yield return list[i];
        }
    }

    internal class CustomList2 : IEnumerable, IEnumerator
    {
        private readonly string[] list;

        private int position = -1;

        public CustomList2()
        {
            list = new[] { "123", "321", "666", "7777" };
        }

        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        public object Current => list[position];

        public bool MoveNext()
        {
            ++position;
            return position < list.Length;
        }

        public void Reset()
        {
            position = -1;
        }
    }

    #endregion

    internal class NotesExercises_Iterator
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("迭代器练习题");

            CustomList list = new CustomList();
            foreach (int item in list) Console.WriteLine(item);

            CustomList2 list2 = new CustomList2();
            foreach (string item in list2) Console.WriteLine(item);
            foreach (string item in list2) Console.WriteLine(item);
        }
    }
}