using System;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;

namespace iOS
{
    internal class iOSService
    {
        public static void PushForiApple()
        {
            var config = new ApnsConfiguration(
                ApnsConfiguration.ApnsServerEnvironment.Sandbox,
                Environment.CurrentDirectory + "\\Route_My_Day_Developer.p12",
                "Pass1234");
            var apnsBroker = new ApnsServiceBroker(config);

            apnsBroker.OnNotificationFailed += (notification, aggregateEx) =>
            {
                aggregateEx.Handle(ex =>
                {
                    var exception = ex as ApnsNotificationException;
                    if (exception != null)
                    {
                        var notificationException = exception;

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        Console.WriteLine(
                            $"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");
                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException           
                        Console.WriteLine($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += notification => { Console.WriteLine("Apple Notification Sent!"); };

            // Start the broker
            apnsBroker.Start();

            var deviceToken = "e811d318728b7f8922b3f826bb582815b9fecebdd07f15992e9383ead416bfa4";
            // Queue a notification to send
            apnsBroker.QueueNotification(new ApnsNotification
            {
                DeviceToken = deviceToken,
                Payload =
                    JObject.Parse("{\"aps\":{\"alert\":\"" + "Blyaaaaaa" + "\",\"badge\":1,\"sound\":\"default\"}}")
            });

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            apnsBroker.Stop();
        }
    }
}