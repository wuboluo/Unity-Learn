using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignPattern.Builder.Structure
{
    public class Product
    {
        private readonly List<string> parts = new();

        public void Add(string part)
        {
            parts.Add(part);
        }

        public void Show()
        {
            Debug.Log("\nProduct Parts");
            foreach (var part in parts) Debug.Log(part);
        }
    }
}