using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps
{
    class KHConnector
    {
        AxKHOpenAPILib.AxKHOpenAPI conn;

        private static KHConnector instance = null;
        public static KHConnector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KHConnector();
                }
                return instance;
            }
        }
        
        private KHConnector()
        {
        }

        public KHConnector(AxKHOpenAPILib.AxKHOpenAPI conn)
        {
            instance = new KHConnector();
            instance.conn = conn;
        }
        
        static string AccountNo;
        public void SetAccountNo(string accountNo)
        {
            AccountNo = accountNo;
        }

        public int Connect()
        {
            return conn.CommConnect();
        }

        //"ACCOUNR_CNT", "ACCNO", "USER_ID", "USER_NAME"
        public string GetLoginInfo(string input)
        {
            return conn.GetLoginInfo(input);
        }

        // 계좌평가현황요청
        public int RequestAccountInfo()
        {
            conn.SetInputValue("계좌번호", AccountNo);
            conn.SetInputValue("비밀번호", "kps2401");
            conn.SetInputValue("상장폐지조회구분", "0");
            conn.SetInputValue("비밀번호입력매체구분", "00");

            return conn.CommRqData("계좌평가현황요청", "OPW00004", 0, "0");
        }

        // 미체결 목록
        // "1" 매수, "2" 매도
        public int RequestNotConcludedOrder(string orderType)
        {
            conn.SetInputValue("계좌번호", AccountNo);
            conn.SetInputValue("체결구분", "1");                           // 체결구분 = 0:체결+미체결조회, 1:미체결조회, 2:체결조회
            conn.SetInputValue("매매구분", orderType);                           // 매매구분 = 0:전체, 1:매도, 2:매수

            var ret = 0;
            if (orderType == "1") ret = conn.CommRqData("매도미체결", "opt10075", 0, "1");
            if (orderType == "2") ret = conn.CommRqData("매수미체결", "opt10075", 0, "2");

            return ret;
        }

        public int RequestOrderStock(string name, string screenNo, int orderType, string code, int count, int price, string hogaGb, string orderNo)
        {
            return conn.SendOrder(name, screenNo, AccountNo, orderType, code, count, price, hogaGb, orderNo);
        }

        // 고저PER요청
        //PER구분 = 1:코스피저PER, 2:코스피고PER, 3:코스닥저PER, 4:코스닥고PER
        public int RequestPERData(string type)
        {
            conn.SetInputValue("PER구분", type);

            return conn.CommRqData("고저PER요청", "otp10026", 0, Util.GetScrNum());
        }

        // 상하한가요청 
        public int RequestUpperLimitList()
        {
            //시장구분 = 000:전체, 001:코스피, 101:코스닥
            conn.SetInputValue("시장구분", "001");
            //상하한구분 = 1:상한, 2:상승, 3:보합, 4: 하한, 5:하락, 6:전일상한, 7:전일하한
            conn.SetInputValue("상하한구분", "1");
            //정렬구분 = 1:종목코드순, 2:연속횟수순(상위100개), 3:등락률순
            conn.SetInputValue("정렬구분", "2");
            //종목조건 = 0:전체조회,1:관리종목제외, 3:우선주제외, 4:우선주 + 관리종목제외, 5:증100제외, 6:증100만 보기, 7:증40만 보기, 8:증30만 보기, 9:증20만 보기, 10:우선주 + 관리종목 + 환기종목제외
            conn.SetInputValue("종목조건", "1");
            //거래량구분 = 00000:전체조회, 00010:만주이상, 00050:5만주이상, 00100:10만주이상, 00150:15만주이상, 00200:20만주이상, 00300:30만주이상, 00500:50만주이상, 01000:백만주이상
            conn.SetInputValue("거래량구분", "00000");
            //신용조건 = 0:전체조회, 1:신용융자A군, 2:신용융자B군, 3:신용융자C군, 4:신용융자D군, 9:신용융자전체
            conn.SetInputValue("신용조건", "0");
            //매매금구분 = 0:전체조회, 1:1천원미만, 2:1천원~2천원, 3:2천원~3천원, 4:5천원~1만원, 5:1만원이상, 8:1천원이상
            conn.SetInputValue("매매금구분", "0");

            return conn.CommRqData("상하한가요청", "opt10017", 0, Util.GetScrNum());
        }

        // 코스피200지수요청
        public int GetKospi200Point()
        {
            //종목코드 = 코스피200 지수 선물만 가능
            conn.SetInputValue("종목코드", "코스피200");
            //기준일자 = YYYYMMDD, 8자리
            //conn.SetInputValue("기준일자", $"{DateTime.Now.ToString("yyyyMMdd")}");
            conn.SetInputValue("기준일자", $"20190107");

            return conn.CommRqData("코스피200지수요청", "opt50037", 0, Util.GetScrNum());
        }

        // 조건식 리스트 가져오기
        public int GetConditionLoad()
        {
            return conn.GetConditionLoad();
        }

        public int SendCondition(string conditionName, int index, int searchType)
        {
            return conn.SendCondition(Util.GetScrNum(), conditionName, index, searchType);
        }

        public int GetStockBasicInfo(string code)
        {
            conn.SetInputValue("종목코드", code);

            return conn.CommRqData("주식기본정보요청", "opt10001", 1, Util.GetScrNum());
        }
    }
}
