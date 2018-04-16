using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class SimulationCase
    {
        public int DayNo { get; set; }
        public int RandomNewsDayType { get; set; }
        public Enums.DayType NewsDayType { get; set; }
        public int RandomDemand { get; set; }
        public int Demand { get; set; }
        public double DailyCost { get; set; }
        public double SalesProfit { get; set; }
        public double LostProfit { get; set; }
        public double ScrapProfit { get; set; }
        public double DailyNetProfit { get; set; }
        public void CalcLostProfit(double DemandNumbers, double PaperPurchasedByDealer, double NewsPaperPurchasePrice, double NewsPaperSellingPrice)
        {
            double res = DemandNumbers - PaperPurchasedByDealer;
            LostProfit = (res * NewsPaperSellingPrice) - (res * NewsPaperPurchasePrice);

        }
    }
}
