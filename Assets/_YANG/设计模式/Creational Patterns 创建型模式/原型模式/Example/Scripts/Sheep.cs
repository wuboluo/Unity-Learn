using System;
using UnityEngine;

namespace Yang.DesignPattern.Prototype.Example1
{
    public class Sheep : IAnimal
    {
        public Sheep()
        {
            Debug.Log("Make new Sheep! ");
        }

        public object Clone()
        {
            Sheep sheep;

            try
            {
                sheep = MemberwiseClone() as Sheep;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + " Error cloning sheep");
                throw;
            }

            return sheep ?? new Sheep();
        }

        public string ToStringEX()
        {
            return "I'M A SHEEP";
        }
    }
}