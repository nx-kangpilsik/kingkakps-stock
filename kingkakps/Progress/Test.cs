using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps
{
    public class Test
    {
        public static void Progress()
        {
            // 여기부터 로직 구현
            //var test = KHConnector.Instance.RequestPERData("1");
            //KHConnector.Instance.RequestUpperLimitList();
            //KHConnector.Instance.GetKospi200Point();

            // 조건식을 가져온다.
            KHConnector.Instance.GetConditionExpression();

            // 최근 로그를 메세지에 쓴다.
            LogUtil.Instance.WriteLastLogs();
            
            while (true)
            {
                // 내일은 일단 다 팔자
                // TODO : kingkakps 2019/01/15 테스트 판매 코드. 
                if (090000 < Util.GetCurrentTimeInt64() && Util.GetCurrentTimeInt64() < 093000)
                {
                    //var serializeInfo = RedisConnector.GetString(HavingStockInfoKey, IsRealServer);
                    //if (serializeInfo != null)
                    //{
                    //    var havingStockInfoList = JsonConvert.DeserializeObject<List<AutoUpdateStockInfo>>(serializeInfo);
                    //    foreach (var info in havingStockInfoList)
                    //    {
                    //        var order = new SendOrder("주식주문", Util.GetScrNum(), 2, info.code, Int32.Parse(info.count), 0, "03", "");
                    //        order.Send();
                    //    }
                    //}
                }


                if (093000 < Util.GetCurrentTimeInt64() && Util.GetCurrentTimeInt64() < 100000)
                {
                    // LongTermBuySetKey 플래그가 켜져있다면
                    if (RedisConnector.IsFlagOn(Form1.LongTermBuySetKey, Form1.IsRealServer))
                    {
                        // 키 리스트 가져와서.
                        var buyCodes = JsonConvert.DeserializeObject<Dictionary<string, BuyStockInfo>>(RedisConnector.GetString(Form1.LongTermBuyListKey, Form1.IsRealServer));

                        foreach (var buyCode in buyCodes.Values)
                        {
                            // 구매하고.
                            var order = new Order("주식주문", Util.GetScrNum(), 1, buyCode.code, buyCode.count, 0, "03", "");
                            order.Send();
                        }

                        // 키 삭제.
                        RedisConnector.KeyDelete(Form1.LongTermBuyListKey, Form1.IsRealServer);

                        // LongTermBuySetKey 플래그를 끈다.
                        RedisConnector.FlagOff(Form1.LongTermBuySetKey, Form1.IsRealServer);
                    }
                }

                // 장 종료될때쯤 주문 잘 안된애들 취소.
                if (160000 < Util.GetCurrentTimeInt64() && Util.GetCurrentTimeInt64() < 161000)
                {
                    {
                        var serializeInfo = RedisConnector.GetString(Form1.NotConcludedBuyKey, Form1.IsRealServer);
                        if (serializeInfo != null)
                        {
                            var NotConcludedBuyInfoList = JsonConvert.DeserializeObject<List<AutoUpdateNotConcludedStockInfo>>(serializeInfo);
                            foreach (var info in NotConcludedBuyInfoList)
                            {
                                var order = new Order("주식주문", Util.GetScrNum(), 3, info.code, int.Parse(info.count), 0, "", info.originalOrderNo);
                                order.Send();
                            }
                        }
                    }
                    {
                        var serializeInfo = RedisConnector.GetString(Form1.NotConcludedSellKey, Form1.IsRealServer);
                        if (serializeInfo != null)
                        {
                            var NotConcludedSellInfoList = JsonConvert.DeserializeObject<List<AutoUpdateNotConcludedStockInfo>>(serializeInfo);
                            foreach (var info in NotConcludedSellInfoList)
                            {
                                var order = new Order("주식주문", Util.GetScrNum(), 4, info.code, int.Parse(info.count), 0, "", info.originalOrderNo);
                                order.Send();
                            }
                        }
                    }
                }

                {// 리스트 뽑아내기
                    if (180000 < Util.GetCurrentTimeInt64() && Util.GetCurrentTimeInt64() < 181000)
                    {
                        var isUnderProfitBuySet = RedisConnector.IsFlagOn(Form1.UnderProfitBuySetKey, Form1.IsRealServer);
                        if (!isUnderProfitBuySet)
                        {
                            var serializeInfo = RedisConnector.GetString(Form1.HavingStockInfoKey, Form1.IsRealServer);
                            if (serializeInfo != null)
                            {
                                var UnderProfitBuyList = new List<BuyStockInfo>();
                                var havingStockInfoList = JsonConvert.DeserializeObject<List<AutoUpdateStockInfo>>(serializeInfo);
                                foreach (var info in havingStockInfoList)
                                {
                                    if (info.profitPercent < Form1.BuyLossPercent)
                                    {
                                        //var savingStockInfo = new BuyStockInfo(info.code, info.profitPercent, info.price, BuyMoneyLoss/info.price, "");
                                        //UnderProfitBuyList.Add(savingStockInfo);
                                    }
                                    //var savingInfo = JsonConvert.SerializeObject(savingStockInfoList);
                                    //RedisConnector.SetString(HavingStockInfoKey, savingInfo, IsRealServer);
                                }

                                RedisConnector.FlagOn(Form1.UnderProfitBuySetKey, Form1.IsRealServer);
                            }
                        }
                    }

                    if (181000 < Util.GetCurrentTimeInt64() && Util.GetCurrentTimeInt64() < 182000)
                    {
                        var isLongTermBuySet = RedisConnector.IsFlagOn(Form1.LongTermBuySetKey, Form1.IsRealServer);
                        if (!isLongTermBuySet)
                        {
                            var ret = KHConnector.Instance.SendCondition(ConditionName.최근결산PER30, 1, 0);

                            if (ret == 1) LogUtil.Instance.WriteLog($"{ConditionName.최근결산PER30} 조건식 조건 셋팅 실행...");
                            else LogUtil.Instance.WriteLog($"{ConditionName.최근결산PER30} 조건식 조건 셋팅 실행 실패");

                        }
                    }
                }

                System.Threading.Thread.Sleep(60000);
            }
        }
    }
}
