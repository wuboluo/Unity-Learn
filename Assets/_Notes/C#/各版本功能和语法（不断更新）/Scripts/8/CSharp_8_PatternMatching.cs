using UnityEngine;

namespace Yang.CSharp.VariousVersionsGrammar
{
    public class CSharp_8_PatternMatching : MonoBehaviour
    {
        private void Start()
        {
            #region 知识点一：模式匹配回顾

            // 模式匹配，是一种测试表达式是否具有特定特征的方法
            // 在编程里指的是，把一个不知道具体数据信息的内容，通过一些【固定的语法格式】来确定【模式数据的具体内容】的过程

            // C# 7 学过的模式匹配

            // 1，常量模式
            // (is 常量)：用于判断输入值是否等于某个值
            object o = 1;
            if (o is 1) print("o是1");

            // 2，类型模式
            // (is 类型 变量名)：如果o是int类型，那么将o的值赋值给新声明的变量i中
            if (o is int i) print(i);
            // (case 类型 变量名)：
            switch (o)
            {
                case int intValue:
                    print("o是int类型，o的值赋给新声明的变量intValue");
                    break;
                case float floatValue:
                    print("o是float类型，o的值赋给新声明的变量floatValue");
                    break;
            }

            // 3，var模式
            // 相当于将变量装入一个【和自己类型相同】的变量中
            if (o is var v) print(v);

            // var one：GetOne()的返回值，再去判断one是否大于0
            if (GetOne() is var one && one > 0)
            {
            }

            #endregion

            #region 知识点二：模式匹配增强功能——switch表达式

            // 格式：
            // 函数声明 => 变量 switch
            // {
            // 常量 => 返回值表达式,
            // 常量 => 返回值表达式,
            // ...
            // 常量 => 返回值表达式
            // };

            // 使用限制：主要是用于switch语句块中只有一句代码用于返回值时

            Vector2 pivot = GetPivot(PivotType.Left_Bottom);
            print(pivot);

            #endregion

            #region 知识点三：模式匹配增强功能——属性模式

            // 就是在常量模式的基础上判断对象上各属性
            // 用法：变量 is {属性:值, 属性:值}
            // 返回值：bool

            DiscountInfo info = new DiscountInfo("5折", true);
            if (info is { discount: "5折", isDiscount: true }) print("信息相同");
            // 相当于 if(info.discount == "5折" && info.isDiscount)

            // 可以结合switch表达式使用
            // 结合switch使用可以通过属性模式判断条件的组合
            float price = GetCost(info, 100);
            print(price);

            #endregion

            #region 知识点四：模式匹配增强功能——元组模式

            // 属性模式虽可以配合switch同时判断多个变量，但它必须是一个数据结构类对象，判断其中的变量
            // 而元组模式可以更简单的完成这样的功能，不需要声明数据结构类，直接利用元组进行判断
            float price2 = GetCost("5折", true, 50);
            print(price2);

            #endregion

            #region 知识点五：模式匹配增强功能——位置模式

            // 如果自定义类中实现了解构函数，那么可以直接用对应类对象于元组进行is判断
            if (info is ("5折", true))
                // 相当于 (info.discount == "5折" && info.isDiscount)
            {
            }

            // 同样可以配合switch表达式来处理逻辑
            float price3 = GetCost_Group(info, 30);
            print(price3);

            // 补充：配合when关键字进行逻辑处理
            // 但是最终可以省略为位置模式

            #endregion
        }

        private int GetOne()
        {
            return 1;
        }

        private Vector2 GetPivot(PivotType type)
        {
            return type switch
            {
                // => 代替 case:组合
                // _ 代替 default

                PivotType.Left_Top => new Vector2(0, 1),
                PivotType.Left_Bottom => new Vector2(0, 0),
                PivotType.Right_Top => new Vector2(1, 1),
                PivotType.Right_Bottom => new Vector2(1, 0),

                _ => new Vector2(-1, -1)
            };
        }

        private float GetCost(DiscountInfo info, float originalPrice)
        {
            return info switch
            {
                // 可以利用属性模式配合switch，同时判断多个条件
                { discount: "5折", isDiscount: true } => originalPrice * 0.5f,
                { discount: "6折", isDiscount: true } => originalPrice * 0.6f,
                { discount: "7折", isDiscount: true } => originalPrice * 0.7f,

                _ => originalPrice
            };
        }

        private float GetCost(string discount, bool isDiscount, float originalPrice)
        {
            return (discount, isDiscount) switch
            {
                // 这里使用(discount, isDiscount)和下列三种情况的元组去比较，返回不同结果
                ("5折", true) => originalPrice * 0.5f,
                ("6折", true) => originalPrice * 0.6f,
                ("7折", true) => originalPrice * 0.7f,

                _ => originalPrice
            };
        }

        private float GetCost_Group(DiscountInfo info, float originalPrice)
        {
            return info switch
            {
                // 之所以叫位置模式，是因为【info解构出来的变量位置】对应【判断用的元组内容的位置】
                ("5折", true) => originalPrice * 0.5f,
                ("6折", true) => originalPrice * 0.6f,
                ("7折", true) => originalPrice * 0.7f,

                _ => originalPrice
            };
        }
    }

    public enum PivotType
    {
        Left_Top,
        Left_Bottom,
        Right_Top,
        Right_Bottom
    }

    public class DiscountInfo
    {
        public readonly string discount;
        public readonly bool isDiscount;

        public DiscountInfo(string _discount, bool _isDiscount)
        {
            discount = _discount;
            isDiscount = _isDiscount;
        }

        public void Deconstruct(out string _discount, out bool _isDiscount)
        {
            _discount = discount;
            _isDiscount = isDiscount;
        }
    }
}