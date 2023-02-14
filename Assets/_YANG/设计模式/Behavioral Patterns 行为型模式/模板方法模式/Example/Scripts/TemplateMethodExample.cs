using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Example
{
    public class TemplateMethodExample : MonoBehaviour
    {
        private void Start()
        {
            Gun ak = new AK();
            Gun kar98K = new Kar98K();

            ak.MakeFullyEquipped();
            kar98K.MakeFullyEquipped();
        }
    }
}