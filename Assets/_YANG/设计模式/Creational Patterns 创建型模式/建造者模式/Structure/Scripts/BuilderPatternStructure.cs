using UnityEngine;

namespace Yang.DesignPattern.Builder.Structure
{
    public class BuilderPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Builder b1 = new ConcreteBuilder1();
            Builder b2 = new ConcreteBuilder2();

            Director.Construct(b1);
            Product p1 = b1.GetResult();
            p1.Show();

            Director.Construct(b2);
            Product p2 = b2.GetResult();
            p2.Show();
        }
    }
}