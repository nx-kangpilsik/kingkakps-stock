using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps
{

    static class RequestQueue
    {
        static public List<SendOrder> SendOrderList = new List<SendOrder>() { };
        
        static public void SendOrderBackgroundJob()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(500);
                if (SendOrderList.Count > 0)
                {
                    var sendOrder = SendOrderList.First();
                    var ret = KHConnector.Instance.RequestOrderStock(sendOrder.sRQName, sendOrder.sScreenNo, sendOrder.nOrderType, sendOrder.sCode, sendOrder.nQty
                        , sendOrder.nPrice, sendOrder.sHogaGb, sendOrder.sOrgOrderNo);
                    if (ret != KOAErrorCode.OP_ERR_NONE)
                    {
                        MessageLog.Instance.Write($"RequestBuyStock Error : {ret.ToString()}");
                    }

                    SendOrderList.Remove(sendOrder);
                }
            }
        }
    }

    class SendOrder
    {
        public string sRQName;
        public string sScreenNo;
        public int nOrderType;
        public string sCode;
        public int nQty;
        public int nPrice;
        public string sHogaGb;
        public string sOrgOrderNo;

        public SendOrder(string name, string screenNo, int orderType, string code, int count, int price, string hogaGb, string orderNo)
        {
            sRQName = name;
            sScreenNo = screenNo;
            nOrderType = orderType;
            sCode = code;
            nQty = count;
            nPrice = price;
            sHogaGb = hogaGb;
            sOrgOrderNo = orderNo;
        }
        public void Send()
        {
            RequestQueue.SendOrderList.Add(this);
        }
    }
}
