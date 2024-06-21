using System.Collections;
using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Structure
{
    public class InterpreterPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Context context = new Context();

            ArrayList list = new ArrayList
            {
                new TerminalExpression(),
                new NonTerminalExpression(),
                new TerminalExpression(),
                new TerminalExpression()
            };

            foreach (object exp in list) (exp as AbstractExpression)?.Interpret(context);
        }
    }
}