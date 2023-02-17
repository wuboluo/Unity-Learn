using UnityEngine;

namespace Yang.DesignPattern.ChainOfResponsibility.Example1
{
    public class ChainOfResponsibilityPatternExample1 : MonoBehaviour
    {
        private Purchase _purchase;

        private void Start()
        {
            Approver director = new Director();
            Approver vicePresident = new VicePresident();
            Approver boss = new Boss();

            director.SetSuccessor(vicePresident);
            vicePresident.SetSuccessor(boss);

            _purchase = new Purchase(1, 75, "CaiShiChang");
            director.ProcessRequest(_purchase);

            _purchase = new Purchase(2, 8100, "JG JiTuan");
            director.ProcessRequest(_purchase);

            _purchase = new Purchase(3, 600, "QS JiTuan");
            director.ProcessRequest(_purchase);
        }
    }
}