using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example1
{
    public class InterpreterPatternExample1 : MonoBehaviour
    {
        private void Start()
        {
            const string code = "DⅡⅣACⅠB";

            Context context = new Context(code);
            List<Expression> tree = new List<Expression>
            {
                new LetterExpression(),
                new RomanNumeralsExpression()
            };

            foreach (Expression exp in tree) exp.Interpret(context);

            Debug.Log($"{code} = {context.Output}");
        }
    }
}