using System;
using UnityEngine;

namespace DataPersistence_Binary
{
    public class SaveOrReadBinaryExample : MonoBehaviour
    {
        [Header("存/读自定义类对象")] public bool saveOrReadClassObject;

        private void Start()
        {
            TestClass tc = new TestClass
            {
                i = 10,
                str = "hhh"
            };

            if (saveOrReadClassObject) BinaryDataManager.Save(tc, "TC");
            else
            {
                TestClass result = BinaryDataManager.Read<TestClass>("TC");
            }
        }
    }


    [Serializable]
    public class TestClass
    {
        public int i;
        public string str;
    }
}