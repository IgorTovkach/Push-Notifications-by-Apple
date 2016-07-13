using System;
using System.Collections.Generic;

namespace Push_notifications
{
    class Program
    {
        static void Main(string[] args)
        {
            string _token = "1ff44815234a8adf0e8bdb5be0772211cd3055cc1dc7bcd203f5665ad7817bb6b281cefe78c25c992dad4";
            var payload1 = new NotificationPayload(_token, "Pisos", 1, "default");
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
