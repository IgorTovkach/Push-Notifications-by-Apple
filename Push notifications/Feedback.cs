using System;

namespace Push_notifications
{
    public class Feedback
    {
        public Feedback()
        {
            DeviceToken = string.Empty;
            Timestamp = DateTime.MinValue;
        }

        public string DeviceToken { get; set; }
        public DateTime Timestamp { get; set; }
    }
}