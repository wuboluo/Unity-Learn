using UnityEngine;

namespace Yang.DesignPattern.Singleton.Structure
{
    public class SingletonPatternStructure : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log(Singleton.Instance().Info);
        }
    }
}