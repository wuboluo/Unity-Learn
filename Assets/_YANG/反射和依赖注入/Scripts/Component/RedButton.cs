namespace YANG.DependencyInjection_Reflection
{
    public class RedButton : IShowInfo
    {
        public RedButton()
        {
            Description = "Red";
        }

        private string Description { get; }

        public string ShowInfo()
        {
            return Description;
        }
    }
}