﻿namespace Yang.DesignPattern.Interpreter.Example2
{
    public class StockOffer
    {
        public int StockShares { get; }
        public Stock Stock { get; }
        public int ColleagueCode { get; }

        public StockOffer(int numOfShares, Stock stock, int colleagueCode)
        {
            StockShares = numOfShares;
            Stock = stock;
            ColleagueCode = colleagueCode;
        }
    }
}