using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps
{
    class Log
    {
        public DateTime time;
        public string message;

        public Log(DateTime time, string message)
        {
            this.time = time;
            this.message = message;
        }
    }

    class BuyStockInfo
    {
        public string name;
        public string code;
        public int price;
        public double per;
        public int count;

        public BuyStockInfo(string _name, string _code, int _price, double _per, int _count)
        {
            this.name = _name;
            this.code = _code;
            this.price = _price;
            this.per = _per;
            this.count = _count;
        }
        public BuyStockInfo()
        {
        }
    }

    // 자동 갱신되는 저장해둘 정보들
    class AutoUpdateStockInfo
    {
        public string stockName;
        public string code;
        public int count;
        public int currentPrice;
        public int evaluationAmount;
        public int profitPrice;
        public double profitPercent;
        public int purchasePrice;
        public int paymentBalance;
        public int todayBuyCount;
        public int todaySellCount;
        public string originalOrderNo;

        public AutoUpdateStockInfo(string _stockName, string _code, int _count, int _currentPrice, int _evaluationAmount
            , int _profitPrice, double _profitPercent, int _purchasePrice, int _paymentBalance, int _todayBuyCount, int _todaySellCount
            , string _originalOrderNo)
        {
            this.stockName = _stockName;
            var replacement = _code.Replace("A", "");
            this.code = replacement;
            this.count = _count;
            this.currentPrice = _currentPrice;
            this.evaluationAmount = _evaluationAmount;
            this.profitPrice = _profitPrice;
            this.profitPercent = _profitPercent;
            this.purchasePrice = _purchasePrice;
            this.paymentBalance = _paymentBalance;
            this.todayBuyCount = _todayBuyCount;
            this.todaySellCount = _todaySellCount;
            this.originalOrderNo = _originalOrderNo;
        }
        public AutoUpdateStockInfo()
        {

        }
    }

    class AutoUpdateNotConcludedStockInfo
    {
        public string code;
        public string count;
        public string originalOrderNo;

        public AutoUpdateNotConcludedStockInfo(string _code, string _count, string _originalOrderNo)
        {
            var replacement = _code.Replace("A", "");
            this.code = replacement;
            this.count = _count;
            this.originalOrderNo = _originalOrderNo;
        }
        public AutoUpdateNotConcludedStockInfo()
        {

        }
    }
}
