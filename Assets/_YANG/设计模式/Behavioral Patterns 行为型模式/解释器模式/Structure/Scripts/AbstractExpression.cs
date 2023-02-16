namespace Yang.DesignPattern.Interpreter.Structure
{
    // 声明一个用于执行操作的接口
    public abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }
}