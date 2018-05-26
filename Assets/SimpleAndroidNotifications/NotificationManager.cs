using System;
using UnityEngine;

#if UNITY_ANDROID && !UNITY_EDITOR

using System.Linq;

#endif

namespace Assets.SimpleAndroidNotifications
{
    public static class NotificationManager
    {
        public const string FullClassName = "com.hippogames.simpleandroidnotifications.Controller";
        public const string MainActivityClassName = "com.unity3d.player.UnityPlayerActivity";

        /// <summary>
        /// Schedule simple notification without app icon.
        /// </summary>
        public static int Send(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = 0, bool silent = false)
        {
            return SendCustom(new NotificationParams
            {
                Id = NotificationIdHandler.GetNotificationId(),
                Delay = delay,
                Title = title,
                Message = message,
                Ticker = message,
                Sound = !silent,
                Vibrate = !silent,
                Light = true,
                SmallIcon = smallIcon,
                SmallIconColor = smallIconColor,
                LargeIcon = "",
                ExecuteMode = NotificationExecuteMode.Inexact
            });
        }

        /// <summary>
        /// Schedule notification with app icon.
        /// </summary>
        public static int SendWithAppIcon(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = 0, bool silent = false)
        {
            return SendCustom(new NotificationParams
            {
                Id = NotificationIdHandler.GetNotificationId(),
                Delay = delay,
                Title = title,
                Message = message,
                Ticker = message,
                Sound = !silent,
                Vibrate = !silent,
                Light = true,
                SmallIcon = smallIcon,
                SmallIconColor = smallIconColor,
                LargeIcon = "app_icon",
                ExecuteMode = NotificationExecuteMode.Inexact
            });
        }

        /// <summary>
        /// Schedule customizable notification.
        /// </summary>
        public static int SendCustom(NotificationParams notificationParams)
        {
            #if UNITY_EDITOR

            Debug.LogWarning("Simple Android Notifications are not supported for current platform. Build and play this scene on android device!");

            #elif UNITY_ANDROID

            var p = notificationParams;
            var delay = (long) p.Delay.TotalMilliseconds;
            var repeatInterval = p.Repeat ? (long) p.RepeatInterval.TotalMilliseconds : 0;
            var vibration = string.Join(",", p.Vibration.Select(i => i.ToString()).ToArray());

            new AndroidJavaClass(FullClassName).CallStatic("SetNotification", p.Id, p.GroupName ?? "", p.GroupSummary ?? "", p.ChannelId, p.ChannelName, delay, Convert.ToInt32(p.Repeat), repeatInterval, p.Title, p.Message, p.Ticker, Convert.ToInt32(p.Multiline),
                Convert.ToInt32(p.Sound), p.CustomSound ?? "", Convert.ToInt32(p.Vibrate), vibration, Convert.ToInt32(p.Light), p.LightOnMs, p.LightOffMs, ColotToInt(p.LightColor), p.LargeIcon ?? "", GetSmallIconName(p.SmallIcon), ColotToInt(p.SmallIconColor), (int) p.ExecuteMode, p.CallbackData, MainActivityClassName);
            
            NotificationIdHandler.AddScheduledNotificaion(p.Id);

            #elif UNITY_IPHONE

            var notification = new UnityEngine.iOS.LocalNotification
            {
                hasAction = false,
                alertBody = notificationParams.Message,
                fireDate = DateTime.Now.Add(notificationParams.Delay)
            };

            UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notification);

            #endif

            return notificationParams.Id;
        }

        /// <summary>
        /// Cancel notification by id.
        /// </summary>
        public static void Cancel(int id)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            new AndroidJavaClass(FullClassName).CallStatic("CancelNotificationEx", id);

            NotificationIdHandler.RemoveScheduledNotificaion(id);

            #endif
        }

        /// <summary>
        /// Cancel all notifications.
        /// </summary>
        public static void CancelAll()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            new AndroidJavaClass(FullClassName).CallStatic("CancelAllNotificationsEx");

            NotificationIdHandler.RemoveAllScheduledNotificaions();

            #endif
        }

        /// <summary>
        /// Return notification callback if app was launched from notification (and null otherwise).
        /// </summary>
        public static NotificationCallback GetNotificationCallback()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            var hasExtra = intent.Call<bool>("hasExtra", "Notification.Id");

            if (hasExtra)
            {
                var extras = intent.Call<AndroidJavaObject>("getExtras");

                return new NotificationCallback
                {
                    Id = extras.Call<int>("getInt", "Notification.Id"),
                    Data = extras.Call<string>("getString", "Notification.CallbackData")
                };
            }

            #endif

            return null;
        }

        private static int ColotToInt(Color color)
        {
            var smallIconColor = (Color32) color;
            
            return smallIconColor.r * 65536 + smallIconColor.g * 256 + smallIconColor.b;
        }

        private static string GetSmallIconName(NotificationIcon icon)
        {
            return "anp_" + icon.ToString().ToLower();
        }
    }
}