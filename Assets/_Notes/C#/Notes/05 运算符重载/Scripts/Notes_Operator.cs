using System;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    public class Notes_Operator : MonoBehaviour
    {
        private void Start()
        {
            Point p1 = new Point { x = 1, y = 1 };
            Point p2 = new Point { x = 2, y = 2 };

            Point p3 = p1 + p2;
            Console.WriteLine($"{p3.x}  {p3.y}");

            Point p4 = p3 + 2;
            Console.WriteLine($"{p4.x}  {p4.y}");
        }
    }
}