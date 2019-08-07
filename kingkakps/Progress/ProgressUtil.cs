using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps.Progress
{
    public static class ProgressUtil
    {
        public static void ResetAll(bool isRealServer)
        {
            ResetAllRedisKey(Form1.LongTermBuyListKey, isRealServer);
            ResetAllRedisKey(Form1.LongTermBuySetFlagKey, isRealServer);

            ResetAllRedisKey(Form1.UnderProfitBuyListKey, isRealServer);
            ResetAllRedisKey(Form1.UnderProfitBuySetFlagKey, isRealServer);

            ResetAllRedisKey(Form1.HavingStockInfoKey, isRealServer);
            ResetAllRedisKey(Form1.NotConcludedBuyKey, isRealServer);
            ResetAllRedisKey(Form1.NotConcludedSellKey, isRealServer);
        }

        public static void ResetLongTermBuyFlagOff(string key, bool isRealServer)
        {
            RedisConnector.FlagOff(key, isRealServer);
            LogUtil.Instance.WriteLog($"{key} 가 리셋되었습니다.");
        }

        public static void ResetAllRedisKey(string key, bool isRealServer)
        {
            RedisConnector.KeyDelete(key, isRealServer);
        }
    }
}
