using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Structure
{
    public class NonTerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Debug.Log("Non-Terminal.Interpret()");
        }
    }
}