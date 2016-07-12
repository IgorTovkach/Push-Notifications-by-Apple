﻿using System.Collections.Generic;

namespace Push_notifications
{
    public class NotificationAlert
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationAlert()
        {
            Body = null;
            ActionLocalizedKey = null;
            LocalizedKey = null;
            LocalizedArgs = new List<object>();
        }

        /// <summary>
        /// Body Text of the Notification's Alert
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Action Button's Localized Key
        /// </summary>
        public string ActionLocalizedKey { get; set; }

        /// <summary>
        /// Localized Key
        /// </summary>
        public string LocalizedKey { get; set; }

        /// <summary>
        /// Localized Argument List
        /// </summary>
        public List<object> LocalizedArgs { get; set; }

        public void AddLocalizedArgs(params object[] valuesObjects)
        {
            LocalizedArgs.AddRange(valuesObjects);
        }

        /// <summary>
        /// Сhecks for void fields
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (!string.IsNullOrEmpty(Body) 
                    || !string.IsNullOrEmpty(ActionLocalizedKey) 
                    || !string.IsNullOrEmpty(LocalizedKey) 
                    || (LocalizedArgs != null && LocalizedArgs.Count>0))
                    return false;
                return true;
            }
        }
    }
}