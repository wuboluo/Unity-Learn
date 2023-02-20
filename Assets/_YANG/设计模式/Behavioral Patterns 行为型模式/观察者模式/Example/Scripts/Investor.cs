using UnityEngine;

namespace Yang.DesignPattern.Observer.Example
{
    public class Investor : IInvestor
    {
        private readonly string _name;

        public Investor(string name)
        {
            _name = name;
        }

        private Stock Stock { get; set; }

        public void Update(Stock stock)
        {
            Debug.Log($"Notified {_name} of {stock}'s change to {stock.Price}");
        }
    }
}