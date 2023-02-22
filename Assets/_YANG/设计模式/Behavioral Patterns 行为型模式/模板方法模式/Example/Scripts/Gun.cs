using UnityEngine;

namespace Yang.DesignPattern.TemplateMethod.Example
{
    // 一个操作中的算法的框架，并将一些步骤延迟到子类中
    public abstract class Gun
    {
        // 装备为满配
        public void MakeFullyEquipped()
        {
            Debug.Log("添加配件使其变满配");

            if (NeedSight()) AddSight();
            if (NeedMagazine()) AddMagazine();
            if (NeedSilencer()) AddSilencer();

            Debug.Log("");
        }

        protected abstract void AddSight();
        protected abstract void AddMagazine();
        protected abstract void AddSilencer();

        protected virtual bool NeedSight()
        {
            return true;
        }

        protected virtual bool NeedMagazine()
        {
            return true;
        }

        protected virtual bool NeedSilencer()
        {
            return true;
        }
    }
}