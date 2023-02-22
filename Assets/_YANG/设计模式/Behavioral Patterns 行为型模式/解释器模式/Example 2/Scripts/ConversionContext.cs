using System.Globalization;
using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example2
{
    public class ConversionContext
    {
        public ConversionContext(string inputContext)
        {
            Debug.Log("Input: " + inputContext);
            ConversionQues = inputContext;
            // 将问题以空格分份
            string[] partsOfQues = ConversionQues.Split(" ");

            if (partsOfQues.Length >= 4)
            {
                FromConversion = GetCapitalized(partsOfQues[1]);
                ToConversion = GetCapitalized(partsOfQues[3]);
                double.TryParse(partsOfQues[0], out double quanta);
                Quantity = quanta;
            }
        }

        // 需求
        private string ConversionQues { get; }

        // 被转换的单位
        public string FromConversion { get; }

        // 转换成的单位
        public string ToConversion { get; }

        // 被转换的数量
        public double Quantity { get; }

        // 首字母大写
        private static string GetCapitalized(string word)
        {
            // 将单位名规范化，仅首字母大写+结尾加's'（转成符合对应函数的名字）
            word = word.ToLower();
            word = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word);
            if (!word.EndsWith("s")) word += "s";

            return word;
        }
    }
}