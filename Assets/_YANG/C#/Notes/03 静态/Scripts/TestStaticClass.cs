using UnityEngine;

namespace Yang.CSharp.Notes.Static
{
    public static class TestStaticClass
    {
        public static readonly int testInt;
        public static readonly int testInt2;

        // 静态构造函数
        static TestStaticClass()
        {
            Debug.Log("静态构造函数");
            testInt = 10;
            testInt2 = 5;
        }
    }
}