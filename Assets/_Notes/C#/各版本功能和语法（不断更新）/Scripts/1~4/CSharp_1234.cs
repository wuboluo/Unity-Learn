using UnityEngine;

namespace Yang.CSharp.VariousVersionsGrammar
{
    public class CSharp_1234 : MonoBehaviour
    {
        private void Start()
        {
            #region 知识点一：最低支持的C#版本

            // 只要是 Unity5.5 及以上的版本，就支持 C#4

            #endregion

            #region 知识点二：C#1~4 功能和语法有哪些？

            // 主要内容（不是所有的）
            // C# 1：委托，事件
            // C# 2：泛型，匿名方法，迭代器，可空类型
            // C# 3：隐式类型（var），对象集合初始化，Lambda表达式，匿名类型，自动实现属性，拓展方法，分部类，Linq
            // C# 4：泛型的协变逆变，命名和可选参数，动态类型

            #endregion

            #region 知识点三：补充——命名和可选参数

            // 有了命名参数，我们将不用匹配参数在所调用方法中的顺序
            // 每个参数可以按照参数名字进行指定
            Test3(1, 0.5f, true);
            Test3(b: true, f: 0.5f, i: 1);

            // 命名参数可以配合可选参数使用，做到跳过其中的默认参数直接赋值后面的默认参数
            Test33(1, 0.5f, false);
            Test33(1, b: false);

            // 好处：更方便的调用函数，少写一些重载

            #endregion

            #region 知识点四：补充——动态类型

            // 关键字：dynamic

            // 作用：
            // 通过dynamic类型标识的变量，其使用和对其成员的引用，会绕过编译时类型检查，改为在运行时解析这些操作
            // 在大多数情况下，dynamic和object行为类似
            // 任何非null表达式都可以转换为dynamic类型

            // dynamic和object的区别：
            // 编译器不会对包含dynamic的表达式的操作进行解析或类型检查
            // 编译器将有关的操作信息打包在一起，之后这些信息会用于在运行时评估操作
            // 在此过程中，dynamic的变量会编译为object的变量
            // 因此，dynamic类型只在编译时存在，在运行时不存在

            // 注意：
            // 1，使用dynamic功能需要将Unity的.Net API兼容级别切换为 .Net 4.x 或 .Net Framework
            // 2，IL2CPP不支持C# dynamic关键字。它需要JIT编译，而IL2CPP无法实现
            // 3，动态类型是无法自动补全方法的，在书写时一定要保证方法的拼写正确性，不建议使用

            // 好处：
            // 动态类型可以节约代码量，当不确定对象类型，但是确定对象成员时，可以使用动态类型
            // 通过反射处理某些功能时，也可以考虑使用动态类型来替换它

            // 例如：
            object obj = 1;
            dynamic dyn = 2;
            dyn += 2;

            print(obj.GetType());
            print(dyn.GetType());
            print(dyn);

            dynamic temp = new MyClass();
            temp.Method();

            #endregion
        }

        private static void Test3(int i, float f, bool b)
        {
        }

        private static void Test33(int i, float f = 0.5f, bool b = true)
        {
        }
    }

    public class MyClass
    {
        public void Method()
        {
            Debug.Log("MyClass Method");
        }
    }
}