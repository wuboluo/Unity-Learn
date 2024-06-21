namespace Yang.DependencyInjection_Reflection
{
    public class RedText : IShowInfo
    {
        public RedText()
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