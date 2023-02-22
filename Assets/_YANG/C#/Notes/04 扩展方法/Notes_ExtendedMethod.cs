using UnityEngine;

namespace Yang.CSharp.Notes
{
    public class Notes_ExtendedMethod : MonoBehaviour
    {
        private void Start()
        {
            const int i = 1;
            i.SpeakValue();

            const string str = "string";
            str.SpeakStringInfo("111");
        }
    }
    
    // 为现有非静态变量类型添加新方法
    // 不能为静态类拓展方法

    // 作用：
    // 1，提升程序拓展性
    // 2，不需要在对象中重新写方法
    // 3，不需要继承来添加方法
    // 4，为别人封装的类型写额外方法
    
    // 特点：
    // 1，一定是写在静态方法
    // 2，一定是个静态函数
    // 3，第一个参数为拓展目标
    // 4，第一个参数用this修饰

    // 访问修饰符 static 返回值 函数名（this 拓展类名，参数列表）

    internal static class Tools
    {
        // 为int拓展了一个成员方法
        // 成员方法是需要实例化对象后才使用的
        // value 代表使用该方法的实例化对象

        public static void SpeakValue(this int value)
        {
            Debug.Log("拓展方法" + value);
        }

        public static void SpeakStringInfo(this string str, string str1)
        {
            Debug.Log("拓展String");
            Debug.Log("调用方法的对象： " + str);
            Debug.Log("传的参数： " + str1);
        }
    }
}