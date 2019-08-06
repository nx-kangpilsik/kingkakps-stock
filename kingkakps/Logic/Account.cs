using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kingkakps.Logic
{
    public static class Account
    {
        public static void ReqestToServer()
        {
            Form1.LoginEvent.WaitOne();

            while (true)
            {
                KHConnector.Instance.RequestAccountInfo();

                //미체결 요청 한다.
                KHConnector.Instance.RequestNotConcludedOrder("1");
                KHConnector.Instance.RequestNotConcludedOrder("2");

                System.Threading.Thread.Sleep(30000);
            }
        }
    }
}
