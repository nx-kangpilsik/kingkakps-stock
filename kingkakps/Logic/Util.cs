using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps
{
    static class Util
    {
        static public Int64 GetCurrentDateInt64()
        {
            return Int64.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        static public Int64 GetCurrentTimeInt64()
        {
            return Int64.Parse(DateTime.Now.ToString("HHmmss"));
        }


        // 화면번호 생산
        static int _scrNum = 5000;
        static public string GetScrNum()
        {
            if (_scrNum < 9999)
                _scrNum++;
            else
                _scrNum = 5000;

            return _scrNum.ToString();
        }

        static public Int64 DateTimeToTotalSec(DateTime dateTime)
        {
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }
    }
}
