using StackExchange.Redis;

namespace kingkakps
{
    static class RedisConnector
    {
        private static ConnectionMultiplexer RedisConnection;
        private static IDatabase db;

        static public bool Initialize(string host, int port)
        {
            RedisConnection = ConnectionMultiplexer.Connect(host + ":" + port);
            if (RedisConnection.IsConnected)
            {
                db = RedisConnection.GetDatabase();
                return true;
            }

            return false;
        }

        static public bool SetString(string key, string val, bool isRealServer)
        {
            if (!isRealServer)
            {
                key = "TestMode_" + key;
            }

            return db.StringSet(key, val);
        }
        static public string GetString(string key, bool isRealServer)
        {
            if (!isRealServer)
            {
                key = "TestMode_" + key;
            }

            return db.StringGet(key);
        }

        static public bool KeyDelete(string key, bool isRealServer)
        {
            if (!isRealServer)
            {
                key = "TestMode_" + key;
            }

            return db.KeyDelete(key);
        }

        static public bool SetFlagOff(string key, bool isRealServer)
        {
            if (!isRealServer)
            {
                key = "TestMode_" + key;
            }

            return db.StringSet(key, "0");
        }

        static public bool SetFlagOn(string key, bool isRealServer)
        {
            if (!isRealServer)
            {
                key = "TestMode_" + key;
            }

            return db.StringSet(key, "1");
        }

        static public bool IsFlagOn(string key, bool isRealServer)
        {
            if (!isRealServer)
            {
                key = "TestMode_" + key;
            }

            if (db.StringGet(key).IsNullOrEmpty)
            {
                return false;
            }

            return db.StringGet(key) == "1" ? true : false;
        }
    }
}
