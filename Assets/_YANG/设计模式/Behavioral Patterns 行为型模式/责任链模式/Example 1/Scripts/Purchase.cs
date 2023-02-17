namespace Yang.DesignPattern.ChainOfResponsibility.Example1
{
    // 采购
    public class Purchase
    {
        // 采购的物品代号
        public Purchase(int number, int amount, string purpose)
        {
            Number = number;
            Amount = amount;
            Purpose = purpose;
        }

        public int Number { get; }

        // 采购的物品数量
        public int Amount { get; }

        // 采购的物品用途
        public string Purpose { get; }
    }
}