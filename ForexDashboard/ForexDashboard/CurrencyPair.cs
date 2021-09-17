using System;
namespace ForexDashboard
{
    public class DashboardCurrency
    {
        #region Class Members

        #endregion

        #region Class Constructor

        #endregion

        #region public Properties

        public string CurrencyPair { private set; get; }
        public string TimeStamp { private set; get; }
        public decimal BigBid { private set; get; }
        public string BidPoint { private set; get; }
        public decimal BigOffer { private set; get; }
        public string OfferPoint { private set; get; }
        public decimal HighOfferRate { private set; get; }
        public decimal LowBidRate { private set; get; }
        public decimal OpenRate { private set; get; }
        public decimal RawMid { private set; get; }
        public decimal BidRate { private set; get; }
        public decimal OfferRate { private set; get; }
        #endregion

        #region Public Methods

        /// <summary>
        /// Format : EUR/USD,1329746416521,1.32,610,1.32,615,1.31735,1.32771,1.31754
        /// Currency-pair symbol (AUD/USD)
        ///Millisecond timestamp (1253890249578)
        ///Bid big figure (for example, 0.86)
        ///Bid points (for example, 565)
        ///Offer big figure (for example, 0.86)
        ///Offer points (for example, 583)
        ///High, the greatest offer price since the currency pair’s roll time (for example, 0.86148)
        ///Low, the smallest bid price since the currency pair’s roll time (for example, 0.87078)
        ///Open, the mid price at the currency pair’s roll time (for example, 0.86821)
        /// </summary>
        /// <param name="currencyfeed"></param>
        /// <returns></returns>
        public static DashboardCurrency FromFeed(string currencyfeed)
        {
            if (string.IsNullOrEmpty(currencyfeed))
                throw new ArgumentException("Currency feed cannot be empty.");
            string[] values = currencyfeed.Split(',');
            if (values == null || values.Length != 9)
                throw new ArgumentException("Invalid feed");
            var currency = new DashboardCurrency
                               {
                                   CurrencyPair = values[0],
                                   TimeStamp = values[1],
                                   BigBid = decimal.Parse(values[2]),
                                   BidPoint = values[3],
                                   BigOffer = decimal.Parse(values[4]),
                                   OfferPoint = values[5],
                                   HighOfferRate = decimal.Parse(values[6]),
                                   LowBidRate = decimal.Parse(values[7]),
                                   OpenRate = decimal.Parse(values[8]),
                                   BidRate = decimal.Parse(string.Concat(values[2], values[3])),
                                   OfferRate = decimal.Parse(string.Concat(values[4], values[5])),
                               };
            currency.RawMid = (currency.BidRate + currency.OfferRate)/2;
            return currency;
        }

        #endregion

        #region Private Methods
        #endregion
    }

    public class ForexEventArgs<T> : EventArgs
    {
        public ForexEventArgs(T data)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }
}
