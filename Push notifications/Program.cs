using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push_notifications
{
    class Program
    {
        static void Main(string[] args)
        {
            var payload1 = new NotificationPayload("Device Token", "Message", 1, "default");
            payload1.AddCustom("RegionID", "IDQ10150");

            var p = new List<NotificationPayload> { payload1 };

            var push = new PushNotification(false, "p12 file location", "password");
            var rejected = push.SendToApple(p);
            foreach (var item in rejected)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
