using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.IO;
namespace ForexDashboard
{
    public class PricingEngine
    {
        #region Constants

        private const string AuthenticationUrl =
            "http://webrates.truefx.com/rates/connect.html?u={0}&p={1}&q=ozrates&c={2}&f=csv&s=n";

        private const string SubscribeFeedUrl = "http://webrates.truefx.com/rates/connect.html?id={0}";

        #endregion

        #region Events
        public event EventHandler<ForexEventArgs<DashboardCurrency>> PriceChanged;

        #endregion

        #region Class Members

        private string _token;
        private readonly string _subscribedFeeds;
        #endregion

        #region Class Constructor
        public PricingEngine()
        {
            var userName = ConfigurationManager.AppSettings["username"];
            var password = ConfigurationManager.AppSettings["password"];
            _subscribedFeeds = ConfigurationManager.AppSettings["subscribedpairs"];
            GetFeedToken(userName, password);
            GetPrices(_token);
        } 
        #endregion

        #region Public Methods

        #endregion

        #region Protected Methods

        protected void RaisePriceChanged(DashboardCurrency currency)
        {
            if(PriceChanged != null)
                PriceChanged(this, new ForexEventArgs<DashboardCurrency>(currency));
        }
        
        #endregion

        #region Private Methods

        public static void GetPrices(string token)
        {
            if(string.IsNullOrEmpty(token))
                throw new ArgumentException("token cannot be empty.");
            string feed = GetFeed(string.Format(SubscribeFeedUrl, token));
            if (feed.EndsWith("\n\r\n"))
                feed = feed.Replace("\n\r\n", string.Empty);
            string[] feeds = feed.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            List<DashboardCurrency> currencies = feeds.Select(DashboardCurrency.FromFeed).ToList();
        }

        private void GetFeedToken(string username, string password)
        {
            _token = GetFeed(string.Format(AuthenticationUrl, username, password, _subscribedFeeds));
        }

        static string GetFeed(string url)
        {
            string data = null;
            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead(url))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            data = reader.ReadToEnd();
                        }
                    }
                }
            }
            return data;
        }

        #endregion
    }
}
