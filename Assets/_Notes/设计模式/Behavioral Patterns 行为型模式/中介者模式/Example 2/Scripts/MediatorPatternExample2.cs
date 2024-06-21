using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example2
{
    public class MediatorPatternExample2 : MonoBehaviour
    {
        private void Start()
        {
            // nyse：纽约证卷交易所
            StockMediator nyse = new StockMediator();

            Broker1 broker1 = new Broker1(nyse);
            Broker2 broker2 = new Broker2(nyse);

            nyse.AddColleague(broker1);
            nyse.AddColleague(broker2);

            broker1.SaleOffer(Stock.Microsoft, 100);
            broker1.BuyOffer(Stock.Apple, 50);

            broker2.SaleOffer(Stock.Google, 80);
            broker2.BuyOffer(Stock.Microsoft, 110);

            nyse.PrintStockOfferings();
        }
    }
}