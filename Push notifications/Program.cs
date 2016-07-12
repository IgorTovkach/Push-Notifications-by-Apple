using System;
using System.Collections.Generic;

namespace Push_notifications
{
    class Program
    {
        static void Main(string[] args)
        {
            var payload1 = new NotificationPayload("Device Token", "Message", 1, "default");
            payload1.AddCustom("RegionID", "IDQ10150");
            string pisos = Environment.CurrentDirectory + "\\Route_My_Day_Developer.p12";
            var p = new List<NotificationPayload> { payload1 };
            var push = new PushNotification(false,
                                            pisos,
                                            "Pass1234");
            var rejected = push.SendToApple(p);
            foreach (var item in rejected)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
