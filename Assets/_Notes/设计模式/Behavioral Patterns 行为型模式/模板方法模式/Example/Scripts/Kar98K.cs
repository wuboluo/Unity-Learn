using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Example
{
    public class Kar98K : Gun
    {
        protected override void AddSight()
        {
            Debug.Log("8x瞄准镜");
        }

        protected override void AddMagazine()
        {
            Debug.Log("狙击枪快扩");
        }

        protected override void AddSilencer()
        {
            Debug.Log("狙击枪消音");
        }
    }
}