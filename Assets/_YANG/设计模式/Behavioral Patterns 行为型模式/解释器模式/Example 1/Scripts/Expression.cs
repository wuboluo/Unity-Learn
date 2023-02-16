namespace Yang.DesignPattern.Interpreter.Example1
{
    public abstract class Expression
    {
        public void Interpret(Context context)
        {
            if (context.Input.Length == 0) return;

            if (context.Input.StartsWith(One()))
            {
                context.Output += 1 * Multiplier();
                context.Input = context.Input[1..];
            }

            if (context.Input.StartsWith(Two()))
            {
                context.Output += 2 * Multiplier();
                context.Input = context.Input[1..];
            }

            if (context.Input.StartsWith(Three()))
            {
                context.Output += 3 * Multiplier();
                context.Input = context.Input[1..];
            }

            if (context.Input.StartsWith(Four()))
            {
                context.Output += 4 * Multiplier();
                context.Input = context.Input[1..];
            }
        }

        protected abstract string One();
        protected abstract string Two();
        protected abstract string Three();
        protected abstract string Four();

        // 乘法
        protected abstract int Multiplier();
    }
}