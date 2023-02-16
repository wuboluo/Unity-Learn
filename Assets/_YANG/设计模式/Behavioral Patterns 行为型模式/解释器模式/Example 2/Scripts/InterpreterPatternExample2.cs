using System;
using System.Reflection;
using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example2
{
    public class InterpreterPatternExample2 : MonoBehaviour
    {
        private void Start()
        {
            const string question1 = "2 Gallons to pints";
            AskQuestion(question1);

            const string question2 = "4 Gallons to tablespoons";
            AskQuestion(question2);
        }

        private static void AskQuestion(string question)
        {
            ConversionContext context = new ConversionContext(question);

            string fromConversion = context.FromConversion;
            string toConversion = context.ToConversion;
            double quantity = context.Quantity;

            try
            {
                // 获取转换工具类
                Type type = Type.GetType("Yang.DesignPattern.Interpreter.Example2.VolumeUnit");
                if (type != null)
                {
                    object instance = Activator.CreateInstance(type);
                 
                    // 根据转换成的单位名称来选择对应的函数
                    MethodInfo method = type.GetMethod(toConversion);
                    if (method != null)
                    {
                        string result = (string)method.Invoke(instance, new object[] { quantity });
                        Debug.Log($"Output: {quantity} {fromConversion} are {result} {toConversion}");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}