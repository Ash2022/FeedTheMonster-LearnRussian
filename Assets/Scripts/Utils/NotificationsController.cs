using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.SimpleAndroidNotifications;

public class NotificationsController : MonoBehaviour
{

    public static NotificationsController Instance { get; set; }


    void Start()
    {
        Debug.Log("Canceling old notifications and putting new ones");
        ScheduleAllNotificationsCancelOldOnes();

    }

    public void ScheduleNotification(int id, TimeSpan fire_delay, string title_text, string body_text, NotificationIcon icon, string large_icon, string custom_sound = null)
    {
        NotificationParams notificationParams = new NotificationParams
        {
            Id = id,
            Delay = fire_delay,
            Message = body_text,
            Sound = true,
            Multiline = true,
            Ticker = body_text,
            Vibrate = true,
            //Light = true,
            //LightOnMs = 1000,
            //LightOffMs = 1000,
            //LightColor = Color.red,
            SmallIcon = icon,
            //SmallIconColor = new Color(0f, 0.5f, 0f),
            LargeIcon = large_icon,
            ExecuteMode = NotificationExecuteMode.Inexact,

        };

        if (custom_sound != null)
            notificationParams.CustomSound = custom_sound;

        if (title_text != null)
            notificationParams.Title = title_text;

        Assets.SimpleAndroidNotifications.NotificationManager.SendCustom(notificationParams);

        Debug.LogFormat("Local Android notification scheduled for {0}: {1}", DateTime.Now + fire_delay, body_text);

    }

    public void ScheduleAllNotificationsCancelOldOnes()
    {

        Assets.SimpleAndroidNotifications.NotificationManager.CancelAll();

        ScheduleNotification(1, TimeSpan.FromHours(24), "Я соскучился! ", "Давай поиграем!", Assets.SimpleAndroidNotifications.NotificationIcon.frog,"app_icon","frog");

        ScheduleNotification(2, TimeSpan.FromHours(48), "Я соскучился! ", "Давай поиграем!", Assets.SimpleAndroidNotifications.NotificationIcon.frog,"app_icon","frog");

        ScheduleNotification(3, TimeSpan.FromHours(72), "Я соскучился! ", "Давай поиграем!", Assets.SimpleAndroidNotifications.NotificationIcon.frog,"app_icon","frog");
    }



}
