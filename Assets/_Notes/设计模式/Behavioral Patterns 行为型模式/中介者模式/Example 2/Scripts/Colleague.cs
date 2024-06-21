namespace Yang.DesignPattern.Interpreter.Example2
{
    public abstract class Colleague
    {
        private readonly IMediator _mediator;
        private int _colleagueCode;

        protected Colleague(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void SetCode(int code)
        {
            _colleagueCode = code;
        }

        public void SaleOffer(Stock stock, int shares)
        {
            _mediator.SaleOffer(stock, shares, _colleagueCode);
        }

        public void BuyOffer(Stock stock, int shares)
        {
            _mediator.BuyOffer(stock, shares, _colleagueCode);
        }
    }
}