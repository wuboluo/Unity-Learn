using UnityEngine;

namespace Yang.CSharp.Notes
{
    public static class TestExpand
    {
        // 为 int 拓展了一个成员方法
        // value 代表使用该方法的实例化对象
        public static void SpeakValue(this int value)
        {
            Debug.Log("拓展方法" + value);
        }

        public static void SpeakStringInfo(this string value, string arg)
        {
            Debug.Log("拓展String");
            Debug.Log("调用方法的对象： " + value);
            Debug.Log("传的参数： " + arg);
        }
    }

    // 写法：
    // 访问修饰符 static 返回值 函数名（this 拓展类名，参数列表）
    // {
    // }

    // 注意：
    // 1，为现有非静态变量类型添加新方法
    // 2，不能为静态类拓展方法

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
}