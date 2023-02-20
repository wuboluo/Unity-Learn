using UnityEngine;

namespace Yang.DesignPattern.FactoryMethod.Example1
{
    public abstract class Game
    {
        protected string name;
        protected float size;

        public void Download()
        {
            Debug.Log($"The {name} was downloaded, size: {size}GB");
        }
    }
}