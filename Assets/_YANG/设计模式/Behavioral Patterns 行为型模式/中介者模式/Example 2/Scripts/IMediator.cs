namespace Yang.DesignPattern.Interpreter.Example2
{
    // 中介
    public interface IMediator
    {
        // 添加同事（参与者）
        void AddColleague(Colleague colleague);
        
        // 出售某股票的股份
        void SaleOffer(Stock stock, int shares, int code);
        
        // 购入某股票的股份
        void BuyOffer(Stock stock, int shares, int code);
    }
}