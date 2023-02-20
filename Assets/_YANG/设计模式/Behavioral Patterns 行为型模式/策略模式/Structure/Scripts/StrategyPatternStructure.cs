using UnityEngine;

namespace Yang.DesignPattern.Strategy.Structure
{
    public class StrategyPatternStructure : MonoBehaviour
    {
        private Context _context;

        private void Start()
        {
            _context = new Context(new ConcreteStrategyA());
            _context.ContextInterface();

            _context = new Context(new ConcreteStrategyB());
            _context.ContextInterface();

            _context = new Context(new ConcreteStrategyC());
            _context.ContextInterface();
        }
    }
}