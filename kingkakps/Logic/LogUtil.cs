using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps
{
    class LogUtil
    {
        System.Windows.Forms.TextBox textBox;
        static readonly public string MessageHistoryKey = "MessageHistory";
        static readonly int MaxLogSavingSec = 60 * 60 * 24 * 7;

        private static LogUtil instance = null;
        public static LogUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogUtil();
                }
                return instance;
            }
        }

        public LogUtil(System.Windows.Forms.TextBox textBox)
        {
            instance = new LogUtil();
            instance.textBox = textBox;
        }

        private LogUtil()
        {
        }
        
        public void WriteLog(string log)
        {
            var logText = MakeLogText(DateTime.Now, log);
            WriteToLogBox(logText);
            
            var newLogs = new List<Log>();
            var serializedLogs = RedisConnector.GetString(MessageHistoryKey, Form1.IsRealServer);
            if (serializedLogs != null)
            {
                var lastHistories = JsonConvert.DeserializeObject<List<Log>>(serializedLogs);
                newLogs = lastHistories.Where(x => Util.DateTimeToTotalSec(x.time) + MaxLogSavingSec > Util.DateTimeToTotalSec(DateTime.Now)).ToList();
            }

            newLogs.Add(new Log(DateTime.Now, log));
            var newSerializeHistory = JsonConvert.SerializeObject(newLogs);
            RedisConnector.SetString(MessageHistoryKey, newSerializeHistory, Form1.IsRealServer);
        }

        public void WriteLastLogs()
        {
            var serializedLogs = RedisConnector.GetString(MessageHistoryKey, Form1.IsRealServer);
            if (serializedLogs != null)
            {
                var lastLogs = JsonConvert.DeserializeObject<List<Log>>(serializedLogs);
                var newLogs = lastLogs.Where(x => Util.DateTimeToTotalSec(x.time) + MaxLogSavingSec > Util.DateTimeToTotalSec(DateTime.Now)).ToList();

                foreach (var log in newLogs)
                {
                    var logText = MakeLogText(log.time, log.message);
                    WriteToLogBox(logText);
                }
            }
        }

        public void ResetLastLog()
        {
            RedisConnector.KeyDelete(MessageHistoryKey, Form1.IsRealServer);
        }

        public void ClearLogBox()
        {
            textBox.BeginInvoke(new Action(() => textBox.Clear()));
        }

        public string MakeLogText(DateTime time, string log)
        {
            return $"[{time.ToString("yyyy-MM-dd HH:mm:ss")}] - {log}\n";
        }

        public void WriteToLogBox(string logText)
        {
            textBox.BeginInvoke(new Action(() => textBox.AppendText(logText)));
        }
    }
}
