namespace Yang.DesignPattern.Singleton.Structure
{
    public class Singleton
    {
        private static Singleton _instance;

        private Singleton()
        {
        }

        public string Info => "Singleton Class";

        public static Singleton Instance()
        {
            return _instance ??= new Singleton();
        }
    }
}