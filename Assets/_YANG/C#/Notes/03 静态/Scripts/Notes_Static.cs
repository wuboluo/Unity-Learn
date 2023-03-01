using UnityEngine;

namespace Yang.CSharp.Notes.Static
{
    public class Notes_Static : MonoBehaviour
    {
        private void Start()
        {
            // 普通类中：
            Debug.Log("-------------------- 常量");
            Debug.Log(TestClass.PI);
            Debug.Log(TestClass.G);

            Debug.Log("-------------------- 静态变量");
            Debug.Log(TestClass.staticNumber);

            Debug.Log("-------------------- 静态方法");
            Debug.Log(TestClass.CaleCircle(2));

            Debug.Log("-------------------- 非静态方法");
            TestClass tc = new TestClass();
            Debug.Log(tc.number);
            tc.NonStaticFunction();

            // 静态类中： 
            Debug.Log("-------------------- 静态成员");
            Debug.Log(TestStaticClass.testInt);
            Debug.Log(TestStaticClass.testInt2);
        }
    }
}