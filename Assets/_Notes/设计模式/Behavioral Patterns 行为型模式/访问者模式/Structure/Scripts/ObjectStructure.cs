using System.Collections.Generic;

namespace Yang.DesignPattern.Visitor.Structure
{
    public class ObjectStructure
    {
        private readonly List<Element> _elements = new();

        public void Attach(Element element)
        {
            _elements.Add(element);
        }

        public void Detach(Element element)
        {
            _elements.Remove(element);
        }

        public void Accept(Visitor visitor)
        {
            foreach (Element e in _elements) e.Accept(visitor);
        }
    }
}