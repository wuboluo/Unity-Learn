using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignPattern.Builder.Example1
{
    // 交通工具
    public class Vehicle
    {
        private readonly string _vehicleType;

        private readonly Dictionary<string, string> parts = new();

        public Vehicle(string vehicleType)
        {
            _vehicleType = vehicleType;
        }

        public string this[string key]
        {
            set => parts[key] = value;
        }

        public void Show()
        {
            Debug.Log("\n -----");
            Debug.Log($"Type:  {_vehicleType}");
            Debug.Log($"Frame:  {parts["frame"]}");
            Debug.Log($"Engine:  {parts["engine"]}");
            Debug.Log($"Wheels:  {parts["wheels"]}");
            Debug.Log($"Doors:  {parts["doors"]}");
        }
    }
}