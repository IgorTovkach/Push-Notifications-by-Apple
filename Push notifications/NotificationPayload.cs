using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Push_notifications
{
    public class NotificationPayload
    {
        private string v;

        public NotificationPayload(Dictionary<string, object[]> customItems)
        {
            CustomItems = customItems;
        }

        public NotificationAlert Alert { get; set; }
        public string DeviceToken { get; set; }
        public int? Badge { get; set; }
        internal int PayloadId { get; set; }

        public Dictionary<string, object[]> CustomItems { get; }

        public NotificationPayload(string deviceToken, string alert)
        {
            DeviceToken = deviceToken;
            Alert = new NotificationAlert {Body = alert};
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceToken)
        {
            DeviceToken = deviceToken;
            Alert = new NotificationAlert();
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceTocken, string alert, int badge)
        {
            DeviceToken = deviceTocken;
            Alert = new NotificationAlert {Body = alert};
            Badge = badge;
            CustomItems = new Dictionary<string, object[]>();
        }

        public NotificationPayload(string deviceTocken, string alert, int badge, string v) : this(deviceTocken, alert, badge)
        {
            this.v = v;
        }

        public void AddCustom(string key, params object[] values)
        {
            if (values != null)
            {
                CustomItems.Add(key, values);
            }
        }

        public string ToJson()
        {
            var json = new JObject();
            var aps = new JObject();
            if (!Alert.IsEmpty)
            {
                if (!string.IsNullOrEmpty(Alert.Body)
                    && string.IsNullOrEmpty(Alert.ActionLocalizedKey)
                    && string.IsNullOrEmpty(Alert.LocalizedKey)
                    && (Alert.LocalizedArgs == null
                        || Alert.LocalizedArgs.Count <= 0))
                {
                    aps["alert"] = new JValue(Alert.Body);
                }
                else
                {
                    var jsonAlert = new JObject();
                    if (!string.IsNullOrEmpty(Alert.LocalizedKey))
                        jsonAlert["loc-key"] = new JValue(Alert.LocalizedKey);
                    if (Alert.LocalizedArgs != null && Alert.LocalizedArgs.Count > 0)
                        jsonAlert["loc-args"] = new JArray(Alert.LocalizedArgs.ToArray());

                    if (!string.IsNullOrEmpty(Alert.Body))
                        jsonAlert["body"] = new JValue(Alert.Body);

                    if (!string.IsNullOrEmpty(Alert.ActionLocalizedKey))
                        jsonAlert["action-loc-key"] = new JValue(Alert.ActionLocalizedKey);

                    aps["alert"] = jsonAlert;
                }
            }

            if (Badge.HasValue)
                aps["badge"] = new JValue(Badge.Value);

            json["aps"] = aps;

            foreach (var key in CustomItems.Keys)
            {
                if (CustomItems[key].Length == 1)
                    json[key] = new JValue(CustomItems[key][0]);
                else if (CustomItems[key].Length > 1)
                    json[key] = new JArray(CustomItems[key]);
            }

            var rawString = json.ToString(Formatting.None, null);

            var encodedString = new StringBuilder();
            foreach (var c in rawString)
            {
                if (c < 32 || c > 127)
                    encodedString.Append("\\u" + $"{Convert.ToUInt32(c):x4}");
                else
                    encodedString.Append(c);
            }
            return rawString; // encodedString.ToString();
        }

        public override string ToString()
        {
            return ToJson();
        }
    }
}