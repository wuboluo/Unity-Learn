using System.Collections.Generic;
using UnityEngine;

namespace Yang.DesignPattern.Interpreter.Example2
{
    public class StockMediator : IMediator
    {
        private readonly List<Colleague> _colleagues;
        private readonly List<StockOffer> _sellOffers;
        private readonly List<StockOffer> _buyOffers;

        private int _colleagueCodes;
        private bool _stockSold, _stockBought;

        public StockMediator()
        {
            _colleagues = new List<Colleague>();
            _sellOffers = new List<StockOffer>();
            _buyOffers = new List<StockOffer>();
        }

        public void AddColleague(Colleague colleague)
        {
            _colleagues.Add(colleague);
            _colleagueCodes++;
            colleague.SetCode(_colleagueCodes);
        }

        public void SaleOffer(Stock stock, int shares, int code)
        {
            _stockSold = false;

            for (var i = 0; i < _buyOffers.Count; i++)
            {
                StockOffer offer = _buyOffers[i];
                if (offer.Stock == stock && offer.StockShares == shares)
                {
                    Debug.Log($"{shares} 出售给同事（Code：{code}） {stock} 的股份");

                    _buyOffers.Remove(offer);
                    _stockSold = true;
                }

                if (_stockSold) break;
            }

            if (!_stockSold)
            {
                Debug.Log($"库存增加 {shares}份 {stock} 的股票");
                StockOffer offer = new StockOffer(shares, stock, code);
                _sellOffers.Add(offer);
            }
        }

        public void BuyOffer(Stock stock, int shares, int code)
        {
            _stockBought = false;

            for (var i = 0; i < _sellOffers.Count; i++)
            {
                StockOffer offer = _sellOffers[i];
                if (offer.Stock == stock && offer.StockShares == shares)
                {
                    Debug.Log($"{shares} 购入同事（Code：{code}） {stock} 的股份");

                    _sellOffers.Remove(offer);
                    _stockBought = true;
                }

                if (_stockBought) break;
            }

            if (!_stockBought)
            {
                Debug.Log($"库存减少 {shares}份 {stock} 的股票");
                StockOffer offer = new StockOffer(shares, stock, code);
                _buyOffers.Add(offer);
            }
        }

        public void PrintStockOfferings()
        {
            Debug.Log("");
            Debug.Log("今日各股票股份变化情况：");

            foreach (StockOffer offer in _sellOffers)
            {
                Debug.Log($"第 {offer.ColleagueCode} 位经纪人共出售 {offer.StockShares}份 {offer.Stock}股票");
            }

            foreach (StockOffer offer in _buyOffers)
            {
                Debug.Log($"第 {offer.ColleagueCode} 位经纪人共买入 {offer.StockShares}份 {offer.Stock}股票");
            }
        }
    }
}