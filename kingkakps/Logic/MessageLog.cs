using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps
{
    class MessageLog
    {
        System.Windows.Forms.TextBox textBox;
        static readonly public string MessageHistoryKey = "MessageHistory";
        static readonly int MaxLogSavingSec = 60 * 60 * 24 * 7;

        private static MessageLog instance = null;
        public static MessageLog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MessageLog();
                }
                return instance;
            }
        }

        public MessageLog(System.Windows.Forms.TextBox textBox)
        {
            instance = new MessageLog();
            instance.textBox = textBox;
        }

        private MessageLog()
        {
        }
        
        public void Write(string log)
        {
            var logText = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] - {log}\n";
            textBox.BeginInvoke(new Action(() => textBox.AppendText(logText)));

            var serializeHistory = RedisConnector.GetString(MessageHistoryKey, Form1.IsRealServer);

            List<MessageHistory> newHistories;
            if (serializeHistory != null)
            {
                var lastHistories = JsonConvert.DeserializeObject<List<MessageHistory>>(serializeHistory);
                newHistories = lastHistories.Where(x => Util.DateTimeToTotalSec(x.LogTime) + MaxLogSavingSec > Util.DateTimeToTotalSec(DateTime.Now)).ToList();
            }
            else
            {
                newHistories = new List<MessageHistory>();
            }

            newHistories.Add(new MessageHistory(DateTime.Now, log));
            var newSerializeHistory = JsonConvert.SerializeObject(newHistories);
            RedisConnector.SetString(MessageHistoryKey, newSerializeHistory, Form1.IsRealServer);
        }

        public void LastLogShow()
        {
            var serializedHistory = RedisConnector.GetString(MessageHistoryKey, Form1.IsRealServer);
            if (serializedHistory != null)
            {
                var lastHistories = JsonConvert.DeserializeObject<List<MessageHistory>>(serializedHistory);
                var newHistories = lastHistories.Where(x => Util.DateTimeToTotalSec(x.LogTime) + MaxLogSavingSec > Util.DateTimeToTotalSec(DateTime.Now)).ToList();

                foreach (var history in newHistories)
                {
                    var logText = $"[{history.LogTime.ToString("yyyy-MM-dd HH:mm:ss")}] - {history.Message}\n";
                    textBox.BeginInvoke(new Action(() => textBox.AppendText(logText)));
                }
            }
        }

        public void ResetLastLog()
        {
            RedisConnector.KeyDelete(MessageHistoryKey, Form1.IsRealServer);
        }

        public void Clear()
        {
            textBox.BeginInvoke(new Action(() => textBox.Clear()));
        }
    }
}
