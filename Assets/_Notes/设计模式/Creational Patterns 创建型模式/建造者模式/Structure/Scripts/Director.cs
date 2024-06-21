namespace Yang.DesignPattern.Builder.Structure
{
    public static class Director
    {
        public static void Construct(Builder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }
}