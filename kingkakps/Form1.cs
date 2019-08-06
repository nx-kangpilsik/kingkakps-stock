﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections;
using System.Linq;

namespace kingkakps
{
    public partial class Form1 : Form
    {
        ManualResetEvent LoginEvent = new ManualResetEvent(false);
        List<Thread> threads = new List<Thread>();

        static public readonly string LongTermBuyListKey = "LongTermBuyList";
        static public readonly string LongTermBuySetKey = "LongTermBuySet";

        static public readonly string UnderProfitBuyListKey = "UnderProfitBuyList";
        static public readonly string UnderProfitBuySetKey = "UnderProfitBuySet";


        static public readonly string HavingStockInfoKey = "HavingStockInfo";
        static public readonly string NotConcludedBuyKey = "NotConcludedBuy";
        static public readonly string NotConcludedSellKey = "NotConcludedSell";

        static public readonly double BuyLossPercent = -5.0;
        static public readonly int BuyMoneyLoss = 10000;

        int AccountMoney = 0;
        int TestMode = 0;
        static public bool IsRealServer = false;

        public Form1()
        {
            InitializeComponent();
            RedisConnector.Initialize("127.0.0.1", 6379);

            axKHOpenAPI1.OnEventConnect += axKHOpenAPI_OnEventConnect;
            axKHOpenAPI1.OnReceiveTrData += axKHOpenAPI_OnReceiveTrData;
            axKHOpenAPI1.OnReceiveChejanData += axKHOpenAPI_OnReceiveChejanData;
            axKHOpenAPI1.OnReceiveMsg += axKHOpenAPI_OnReceiveMsg;
            axKHOpenAPI1.OnReceiveConditionVer += axKHOpenAPI_OnReceiveConditionVer;
            axKHOpenAPI1.OnReceiveTrCondition += axKHOpenAPI_OnReceiveTrCondition;

            new KHConnector(axKHOpenAPI1);
            new MessageLog(MessageLogBox);

            KHConnector.Instance.Connect();

            // 메인 로직 업데이트
            Thread progressThread = new Thread(Progress);
            progressThread.Start();
            threads.Add(progressThread);
                        
            // 머니 업데이트
            Thread updateRemainMoney = new Thread(UpdateRemainMoney);
            updateRemainMoney.Start();
            threads.Add(updateRemainMoney);

            // Queue 에 들어있는 것 백그라운드 작업
            Thread sendOrder = new Thread(RequestQueue.SendOrderBackgroundJob);
            sendOrder.Start();
            threads.Add(sendOrder);
        }

        public void Progress()
        {
            LoginEvent.WaitOne();

            if (IsRealServer)
            {
                RealProgress();
            }
            else
            {
                TestProgress();
            }
        }

        public void TestProgress()
        {
            LoginEvent.WaitOne();
            // 여기부터 로직 구현
            //var test = KHConnector.Instance.RequestPERData("1");
            //KHConnector.Instance.RequestUpperLimitList();
            //KHConnector.Instance.GetKospi200Point();

            // 조건식을 가져온다.
            KHConnector.Instance.GetConditionLoad();

            // 최근 로그를 메세지에 쓴다.
            MessageLog.Instance.LastLogShow();

            //ResetLongTermBuyList(LongTermBuySetKey, IsRealServer);
            //RedisConnector.KeyDelete(HavingStockInfoKey, IsRealServer);
            //RedisConnector.KeyDelete(NotConcludedBuyKey, IsRealServer);
            //RedisConnector.KeyDelete(NotConcludedSellKey, IsRealServer);


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
                    if (RedisConnector.IsFlagOn(LongTermBuySetKey, IsRealServer))
                    {
                        // 키 리스트 가져와서.
                        var buyCodes = JsonConvert.DeserializeObject<Dictionary<string, BuyStockInfo>>(RedisConnector.GetString(LongTermBuyListKey, IsRealServer));

                        foreach (var buyCode in buyCodes.Values)
                        {
                            // 구매하고.
                            var order = new SendOrder("주식주문", Util.GetScrNum(), 1, buyCode.code, buyCode.count , 0, "03", "");
                            order.Send();
                        }

                        // 키 삭제.
                        RedisConnector.KeyDelete(LongTermBuyListKey, IsRealServer);

                        // LongTermBuySetKey 플래그를 끈다.
                        RedisConnector.SetFlagOff(LongTermBuySetKey, IsRealServer);
                    }
                }

                // 장 종료될때쯤 주문 잘 안된애들 취소.
                if (160000 < Util.GetCurrentTimeInt64() && Util.GetCurrentTimeInt64() < 161000)
                {
                    {
                        var serializeInfo = RedisConnector.GetString(NotConcludedBuyKey, IsRealServer);
                        if (serializeInfo != null)
                        {
                            var NotConcludedBuyInfoList = JsonConvert.DeserializeObject<List<AutoUpdateNotConcludedStockInfo>>(serializeInfo);
                            foreach (var info in NotConcludedBuyInfoList)
                            {
                                var order = new SendOrder("주식주문", Util.GetScrNum(), 3, info.code, int.Parse(info.count), 0, "", info.originalOrderNo);
                                order.Send();
                            }
                        }
                    }
                    {
                        var serializeInfo = RedisConnector.GetString(NotConcludedSellKey, IsRealServer);
                        if (serializeInfo != null)
                        {
                            var NotConcludedSellInfoList = JsonConvert.DeserializeObject<List<AutoUpdateNotConcludedStockInfo>>(serializeInfo);
                            foreach (var info in NotConcludedSellInfoList)
                            {
                                var order = new SendOrder("주식주문", Util.GetScrNum(), 4, info.code, int.Parse(info.count), 0, "", info.originalOrderNo);
                                order.Send();
                            }
                        }
                    }
                }

                {// 리스트 뽑아내기
                    if (180000 < Util.GetCurrentTimeInt64() && Util.GetCurrentTimeInt64() < 181000)
                    {
                        var isUnderProfitBuySet = RedisConnector.IsFlagOn(UnderProfitBuySetKey, IsRealServer);
                        if (!isUnderProfitBuySet)
                        {
                            var serializeInfo = RedisConnector.GetString(HavingStockInfoKey, IsRealServer);
                            if (serializeInfo != null)
                            {
                                var UnderProfitBuyList = new List<BuyStockInfo>();
                                var havingStockInfoList = JsonConvert.DeserializeObject<List<AutoUpdateStockInfo>>(serializeInfo);
                                foreach (var info in havingStockInfoList)
                                {
                                    if (info.profitPercent < BuyLossPercent)
                                    {
                                        //var savingStockInfo = new BuyStockInfo(info.code, info.profitPercent, info.price, BuyMoneyLoss/info.price, "");
                                        //UnderProfitBuyList.Add(savingStockInfo);
                                    }
                                    //var savingInfo = JsonConvert.SerializeObject(savingStockInfoList);
                                    //RedisConnector.SetString(HavingStockInfoKey, savingInfo, IsRealServer);
                                }

                                RedisConnector.SetFlagOn(UnderProfitBuySetKey, IsRealServer);
                            }
                        }
                    }
                    
                    if (181000 < Util.GetCurrentTimeInt64() && Util.GetCurrentTimeInt64() < 182000)
                    {
                        var isLongTermBuySet = RedisConnector.IsFlagOn(LongTermBuySetKey, IsRealServer);
                        if (!isLongTermBuySet)
                        {
                            var ret = KHConnector.Instance.SendCondition(ConditionName.최근결산PER30, 1, 0);

                            if (ret == 1) MessageLog.Instance.Write($"{ConditionName.최근결산PER30} 조건식 조건 셋팅 실행...");
                            else MessageLog.Instance.Write($"{ConditionName.최근결산PER30} 조건식 조건 셋팅 실행 실패");

                        }
                    }
                }

                System.Threading.Thread.Sleep(60000);
            }
        }

        public void RealProgress()
        {
            
        }

        public void UpdateRemainMoney()
        {
            LoginEvent.WaitOne();

            while (true)
            {
                KHConnector.Instance.RequestAccountInfo();

                //미체결 요청 한다.
                KHConnector.Instance.RequestNotConcludedOrder("1");
                KHConnector.Instance.RequestNotConcludedOrder("2");

                System.Threading.Thread.Sleep(30000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //자동매매 정지
            foreach (var thread in threads)
            {
                thread.Abort();
            }
            Application.Exit();
        }

        // 커넥션 연결 됐을때
        private void axKHOpenAPI_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            textBox1.Text = KHConnector.Instance.GetLoginInfo("USER_ID").Trim();
            textBox2.Text = KHConnector.Instance.GetLoginInfo("ACCNO").Trim().Split(';')[TestMode];
            textBox3.Text = KHConnector.Instance.GetLoginInfo("GetServerGubun").Trim();

            if (textBox3.Text == "1")
            {
                textBox3.Text = "모의투자 서버";
                IsRealServer = false;
            }
            else
            {
                textBox3.Text = "실제 서버";
                IsRealServer = true;
            }

            KHConnector.Instance.SetAccountNo(textBox2.Text);

            LoginEvent.Set();
        }

        private void axKHOpenAPI_OnReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            if (e.sGubun == "0")
            {
                MessageLog.Instance.Write("=========================================");
                MessageLog.Instance.Write("주문/체결시간 : " + axKHOpenAPI1.GetChejanData(908));
                MessageLog.Instance.Write("종목명 : " + axKHOpenAPI1.GetChejanData(302));
                MessageLog.Instance.Write("주문수량 : " + axKHOpenAPI1.GetChejanData(900));
                MessageLog.Instance.Write("체결수량 : " + axKHOpenAPI1.GetChejanData(930));
                MessageLog.Instance.Write("체결가격 : " + axKHOpenAPI1.GetChejanData(910));
                MessageLog.Instance.Write("=========================================");
            }
            else if (e.sGubun == "1")
            {
                MessageLog.Instance.Write("=========================================");
                MessageLog.Instance.Write("종목코드 : " + axKHOpenAPI1.GetChejanData(9001));
                MessageLog.Instance.Write("보유수량 : " + axKHOpenAPI1.GetChejanData(930));
                MessageLog.Instance.Write("예수금 : " + axKHOpenAPI1.GetChejanData(951));
                MessageLog.Instance.Write("=========================================");
            }
            else if (e.sGubun == "3")
            {
                MessageLog.Instance.Write("=========================================");
                MessageLog.Instance.Write("구분 : 특이신호");
                MessageLog.Instance.Write("=========================================");
            }
        }

        private void axKHOpenAPI_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            // 30초마다 리프래쉬 될 구문
            if (e.sRQName == "계좌평가현황요청")
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("==계좌평가현황== 최종 갱신:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                AccountMoney = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "예수금").Trim());
                textBox4.Text = AccountMoney.ToString();

                listBox1.Items.Add($"D+2추정예수금 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "D+2추정예수금").Trim()));
                listBox1.Items.Add($"유가잔고평가액 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "유가잔고평가액").Trim()));
                listBox1.Items.Add($"예탁자산평가액 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "예탁자산평가액").Trim()));
                listBox1.Items.Add($"총매입금액 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총매입금액").Trim()));
                listBox1.Items.Add($"추정예탁자산 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "추정예탁자산").Trim()));
                listBox1.Items.Add($"당일투자원금 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "당일투자원금").Trim()));
                listBox1.Items.Add($"당월투자원금 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "당월투자원금").Trim()));
                listBox1.Items.Add($"누적투자원금 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "누적투자원금").Trim()));
                listBox1.Items.Add($"당일투자손익 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "당일투자손익").Trim()));
                listBox1.Items.Add($"당월투자손익 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "당월투자손익").Trim()));
                listBox1.Items.Add($"당일손익율 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "당일손익율").Trim()));
                listBox1.Items.Add($"당월손익율 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "당월손익율").Trim()));
                listBox1.Items.Add($"누적손익율 = " + Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "누적손익율").Trim()));


                var count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                dataGridView1.Rows.Clear();

                var savingStockInfoList = new List<AutoUpdateStockInfo>();
                for (int i = 0; i < count; ++i)
                {
                    var stockName = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                    var stockCode = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim();
                    var stockCount = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량").Trim()).ToString();
                    var currentPrice = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim()).ToString();
                    var evaluationAmount = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평가금액").Trim()).ToString();
                    var profitPrice = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익금액").Trim());
                    var profitPercent = Double.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익율").Trim());
                    var purchaseAmount = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매입금액").Trim()).ToString();
                    var purchasePrice = (Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매입금액").Trim())/ Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량").Trim())).ToString();
                    var paymentBalance = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "결제잔고").Trim()).ToString();
                    var todayBuyCount = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "금일매수수량").Trim()).ToString();
                    var todaySellCount = Int32.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "금일매도수량").Trim()).ToString();

                    dataGridView1.Rows.Add(
                        stockName,
                        stockCode,
                        stockCount,
                        currentPrice,
                        evaluationAmount,
                        profitPrice,
                        profitPercent,
                        purchasePrice,
                        paymentBalance,
                        todayBuyCount,
                        todaySellCount
                        );

                    var savingStockInfo = new AutoUpdateStockInfo(stockName, stockCode, Int32.Parse(stockCount), Int32.Parse(currentPrice),
                        Int32.Parse(evaluationAmount), profitPrice, profitPercent, Int32.Parse(purchasePrice), Int32.Parse(paymentBalance), Int32.Parse(todayBuyCount)
                        , Int32.Parse(todaySellCount), "");
                    savingStockInfoList.Add(savingStockInfo);
                }
                var savingInfo = JsonConvert.SerializeObject(savingStockInfoList);
                RedisConnector.SetString(HavingStockInfoKey, savingInfo, IsRealServer);

                groupBox3.Text = TextCollection.GroupBox3 + "   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (e.sRQName == "매수미체결")
            {
                dataGridView2.Rows.Clear();
                int nCnt = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                var savingStockInfoList = new List<AutoUpdateNotConcludedStockInfo>();
                for (int i = 0; i < nCnt; i++)
                {
                    var stockName = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "종목명").Trim();
                    var stockCode = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "종목코드").Trim();
                    var currentPrice = Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "현재가").Trim()).ToString();
                    var orderPrice = Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "주문가격").Trim()).ToString();
                    var notConcludedCount = Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "미체결수량").Trim()).ToString();
                    var orderCount = Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "주문수량").Trim()).ToString();
                    var originalOrderNumber = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "원주문번호").Trim().ToString();

                    dataGridView2.Rows.Add(
                        stockName,
                        stockCode,
                        currentPrice,
                        orderPrice,
                        notConcludedCount,
                        orderPrice,
                        originalOrderNumber
                        );

                    var savingStockInfo = new AutoUpdateNotConcludedStockInfo(stockCode, notConcludedCount, originalOrderNumber);
                    savingStockInfoList.Add(savingStockInfo);
                }
                var savingInfo = JsonConvert.SerializeObject(savingStockInfoList);
                RedisConnector.SetString(NotConcludedBuyKey, savingInfo, IsRealServer);

                groupBox4.Text = TextCollection.GroupBox4 + "   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (e.sRQName == "매도미체결")
            {
                dataGridView3.Rows.Clear();
                int nCnt = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                var savingStockInfoList = new List<AutoUpdateNotConcludedStockInfo>();
                for (int i = 0; i < nCnt; i++)
                {
                    var stockName = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "종목명").Trim();
                    var stockCode = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "종목코드").Trim();
                    var currentPrice = Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "현재가").Trim()).ToString();
                    var orderPrice = Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "주문가격").Trim()).ToString();
                    var notConcludedCount = Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "미체결수량").Trim()).ToString();
                    var orderCount = Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "주문수량").Trim()).ToString();
                    var originalOrderNumber = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, i, "원주문번호").Trim().ToString();

                    dataGridView3.Rows.Add(
                        stockName,
                        stockCode,
                        currentPrice,
                        orderPrice,
                        notConcludedCount,
                        orderPrice,
                        originalOrderNumber
                        );

                    var savingStockInfo = new AutoUpdateNotConcludedStockInfo(stockCode, notConcludedCount, originalOrderNumber);
                    savingStockInfoList.Add(savingStockInfo);
                }
                var savingInfo = JsonConvert.SerializeObject(savingStockInfoList);
                RedisConnector.SetString(NotConcludedSellKey, savingInfo, IsRealServer);
                groupBox5.Text = TextCollection.GroupBox5 + "   " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (e.sRQName == "고저PER요청")
            {
                // 알수없는 오류남.
            }
            else if (e.sRQName == "상하한가요청")
            {
                // TODO : kingkakps 
                // 지금은 딱히 필요한 기능이 아님
                // 나중에 구현하자.
            }
            else if (e.sRQName == "코스피200지수요청")
            {
                int nCnt = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                var day = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, 0, "일자").Trim();
                var stockCode = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, 0, "코스피200").Trim();
            }
            else if (e.sRQName == "주식기본정보요청")
            {
                // LongTermBuyListKey 에 코드 리스트 저장.
                var serializeCodeInfos = RedisConnector.GetString(LongTermBuyListKey, IsRealServer);
                var codeInfos = JsonConvert.DeserializeObject<Dictionary<string, BuyStockInfo>>(serializeCodeInfos);

                var eCode = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, 0, "종목코드").Trim();
                if (codeInfos.TryGetValue(eCode, out var value))
                {
                    value.name = axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, 0, "종목명").Trim();
                    value.per = double.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, 0, "PER").Trim());
                    value.price = Math.Abs(Int32.Parse(axKHOpenAPI1.CommGetData(e.sTrCode, "", e.sRQName, 0, "현재가").Trim().ToString()));
                    value.count = Config.LongTermBuyPer / value.price;
                }

                var newSerializeCodeInfos = JsonConvert.SerializeObject(codeInfos);
                RedisConnector.SetString(LongTermBuyListKey, newSerializeCodeInfos, IsRealServer);
                // LongTermBuySetKey 플래그를 켠다.
                RedisConnector.SetFlagOn(LongTermBuySetKey, IsRealServer);
            }
        }

        private void axKHOpenAPI_OnReceiveConditionVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {
            if (e.lRet == 1)                                              // 조건식 저장이 성공이면
            {
                var strConditionVers = axKHOpenAPI1.GetConditionNameList().Trim();   // 조건식 리스트 호출하기    
                //MessageLog.Instance.Write(ConditionVers);

                var conditionVers = strConditionVers.Split(';').Where(x=> x != "").ToList();

                foreach (var conditionVer in conditionVers)
                {
                    var spCon = conditionVer.Split('^');
                    var nIndex = Int32.Parse(spCon[0]);               // spCon[0]에는 000,001과 같이 인덱스값이 들어있음  
                    var strConditionName = spCon[1];               // spCon[1]에는 조건명이 들어있음      
                }
            }
        }

        private void axKHOpenAPI_OnReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {
            if (e.strCodeList == "")
            {
                return;
            }

            var codeNameList = e.strCodeList.Split(';').Where(x => x != "").ToList();

            if (e.strConditionName == ConditionName.최근결산PER30)
            {
                var longTermBuySetInfoList = new Dictionary<string, BuyStockInfo>();
                foreach(var codeName in codeNameList)
                {
                    var longTermBuySetInfo = new BuyStockInfo();
                    longTermBuySetInfo.code = codeName;
                    longTermBuySetInfoList.Add(codeName, longTermBuySetInfo);
                }
                var serializeCodeList = JsonConvert.SerializeObject(longTermBuySetInfoList);
                // LongTermBuyListKey 에 코드 리스트 저장.
                RedisConnector.SetString(LongTermBuyListKey, serializeCodeList, IsRealServer);
                
                foreach(var codeName in codeNameList)
                {
                    KHConnector.Instance.GetStockBasicInfo(codeName);
                    System.Threading.Thread.Sleep(400);
                }
                
            }
        }

        private void axKHOpenAPI_OnReceiveMsg(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {
            // 계좌 조회는 0번 사용
            if (e.sScrNo == "0")
            {

            }
            else if (e.sMsg.Equals(" 조회가 완료되었습니다."))
            {

            }
            else
            {
                MessageLog.Instance.Write($"{e.sScrNo} : {e.sMsg}");
            }
        }

        static public void ResetLongTermBuyList(string key, bool isRealServer)
        {
            RedisConnector.SetFlagOff(key, isRealServer);
            MessageLog.Instance.Write($"{key} 가 리셋되었습니다.");
        }
    }
}