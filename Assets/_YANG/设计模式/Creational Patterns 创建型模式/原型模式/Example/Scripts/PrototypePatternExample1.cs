using UnityEngine;

namespace Yang.DesignPattern.Prototype.Example1
{
    public class PrototypePatternExample1 : MonoBehaviour
    {
        private void Start()
        {
            CloneFactory factory = new CloneFactory();
            Sheep sally = new Sheep();

            if (factory.GetClone(sally) is Sheep clonedSheep) Debug.Log(clonedSheep.ToStringEX());
        }
    }
}