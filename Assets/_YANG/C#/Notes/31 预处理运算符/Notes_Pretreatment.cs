// 定义一个符号

#define Unity5
#define Unity2017

// 取消定义
#undef Unity5
#define IOS
#define Android
#define PC

using UnityEngine;

namespace Yang.CSharp.Notes
{
    internal class Notes_Pretreatment
    {
        private static void Main(string[] args)
        {
            // ---------------------------------------- 什么是编译器
            // 编译器是一种翻译程序
            // 它用于将源语言程序翻译为目标语言程序

            // 源语言程序：某种程序设计语言携程的，比如 C#,C++,Java等语言编写的程序
            // 目标语言程序：二进制数表示的为机器代码写的程序


            // ---------------------------------------- 什么是预处理器指令
            // 预处理器指令，指导编译器在实际编译开始之前对信息进行预处理
            // 都是以 #开始
            // 不是语句，不需要以';'结束


            // ---------------------------------------- 常见的预处理器指令
            // 1
            // #define 
            //      定义一个符号，类似一个没有值的变量
            // #undef
            //      取消define定义的符号，让其失效
            // 两者都是写在脚本文件最前面（using前面）
            // 一般配合 if指令 或者 特性使用

            // 2
            // #if
            // #else
            // #endif
            // #elif
            // 和 if语句规则一样，一般配合 #define定义的符号使用
            // 用于告诉编译器，进行编译代码的流程控制

            // 如果发现有 Unity2017 这个符号，那么其中包含的代码，就会被编译器翻译
            // 可以通过 && || 进行多种符号的组合判断
#if Unity5
            Debug.Log("Unity5");
#elif Unity2017 || IOS
            Debug.Log("Unity2017");
            // #warning 不好
            // #error 不可以
#else
            Debug.Log("other");
#endif

            // 3
            // #warning
            // #error
            // 用于告诉编译器，是报警告还是报错。一般配合 if使用
        }
    }
}