using System.Globalization;

namespace Yang.DesignPattern.Interpreter.Example2
{
    // 容积单位
    //    1 gallon
    //  = 4 quarts
    //  = 8 pints
    //  = 16 cups
    //  = 256 tablespoons

    public class VolumeUnit : Expression
    {
        public override string Gallons(double quantity)
        {
            return quantity.ToString(CultureInfo.InvariantCulture);
        }

        public override string Quarts(double quantity)
        {
            return (quantity * 4).ToString(CultureInfo.InvariantCulture);
        }

        public override string Pints(double quantity)
        {
            return (quantity * 8).ToString(CultureInfo.InvariantCulture);
        }

        public override string Cups(double quantity)
        {
            return (quantity * 16).ToString(CultureInfo.InvariantCulture);
        }

        public override string Tablespoons(double quantity)
        {
            return (quantity * 256).ToString(CultureInfo.InvariantCulture);
        }
    }
}