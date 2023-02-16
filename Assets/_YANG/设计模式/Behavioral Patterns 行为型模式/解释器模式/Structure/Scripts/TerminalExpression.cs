using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Structure
{
    // 终端表达式
    // 实现与文法中的终结符关联的解释操作
    // 句子中的每个终结符号都需要一个实例
    public class TerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Debug.Log("Terminal.Interpret()");
        }
    }
}