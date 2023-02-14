using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Example
{
    public class AK : Gun
    {
        protected override void AddSight()
        {
        }
        protected override void AddMagazine()
        {
            Debug.Log("步枪快扩");
        }
        protected override void AddSilencer()
        {
            Debug.Log("步枪消音");
        }
        
        protected override bool NeedSight()
        {
            return false;
        }
    }
}