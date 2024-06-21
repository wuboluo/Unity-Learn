namespace Yang.DesignPattern.ChainOfResponsibility.Example1
{
    // 审批人抽象类
    public abstract class Approver
    {
        protected Approver Successor;

        public void SetSuccessor(Approver successor)
        {
            Successor = successor;
        }

        public abstract void ProcessRequest(Purchase purchase);
    }
}