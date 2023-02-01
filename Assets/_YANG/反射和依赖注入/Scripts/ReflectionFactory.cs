using System.IO;
using System.Reflection;
using System.Xml;
using UnityEngine;

namespace YANG.DependencyInjection_Reflection
{
    public static class ReflectionFactory
    {
        private static readonly string ButtonType;
        private static readonly string TextType;

        private static readonly string MyAssemblyName;

        static ReflectionFactory()
        {
            MyAssemblyName = "YANG.DependencyInjection_Reflection";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Path.Combine(Application.streamingAssetsPath, @"反射和依赖注入_配置\Config.xml"));
            XmlNode config = xmlDocument.ChildNodes[1];

            ButtonType = config.ChildNodes[0].ChildNodes[0].Value;
            TextType = config.ChildNodes[1].ChildNodes[0].Value;
        }

        public static IShowInfo MakeButton()
        {
            return Assembly.Load(MyAssemblyName).CreateInstance($"{MyAssemblyName}.{ButtonType}") as IShowInfo;
        }

        public static IShowInfo MakeText()
        {
            return Assembly.Load(MyAssemblyName).CreateInstance($"{MyAssemblyName}.{TextType}") as IShowInfo;
        }
    }
}