namespace YANG.DependencyInjection_Reflection
{
    public class WhiteText: IShowInfo
    {
        public WhiteText()
        {
            Description = "White";
        }

        private string Description { get; }

        public string ShowInfo()
        {
            return Description;
        }
    }
}