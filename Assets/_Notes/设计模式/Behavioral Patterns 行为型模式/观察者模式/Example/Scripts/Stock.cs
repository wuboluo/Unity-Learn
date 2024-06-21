using System.Collections.Generic;

namespace Yang.DesignPattern.Observer.Example
{
    // 库存，被观察对象
    public abstract class Stock
    {
        private readonly List<IInvestor> _investors = new();

        protected Stock(float price)
        {
            Price = price;
        }

        public float Price { get; set; }

        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }

        public void Detach(IInvestor investor)
        {
            _investors.Remove(investor);
        }

        public void Notify()
        {
            foreach (IInvestor investor in _investors) investor.Update(this);
        }
    }
}