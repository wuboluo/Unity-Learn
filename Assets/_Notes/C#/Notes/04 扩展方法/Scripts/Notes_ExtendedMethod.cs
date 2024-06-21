using UnityEngine;

namespace Yang.CSharp.Notes
{
    public class Notes_ExtendedMethod : MonoBehaviour
    {
        private void Start()
        {
            const int i = 1;
            i.SpeakValue();

            const string str = "string";
            str.SpeakStringInfo("111");
        }
    }
}