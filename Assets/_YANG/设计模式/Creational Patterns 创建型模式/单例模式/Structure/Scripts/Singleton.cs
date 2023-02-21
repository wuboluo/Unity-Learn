namespace Yang.DesignPattern.Singleton.Structure
{
    public class Singleton
    {
        private static Singleton _instance;

        public string Info => "Singleton Class";

        private Singleton()
        {
        }

        public static Singleton Instance()
        {
            return _instance ??= new Singleton();
        }
    }
}