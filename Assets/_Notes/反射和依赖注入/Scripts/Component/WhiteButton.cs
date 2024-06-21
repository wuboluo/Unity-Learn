namespace Yang.DependencyInjection_Reflection
{
    public class WhiteButton : IShowInfo
    {
        public WhiteButton()
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