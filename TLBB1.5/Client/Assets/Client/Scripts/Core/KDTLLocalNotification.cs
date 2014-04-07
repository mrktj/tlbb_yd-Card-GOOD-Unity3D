//
//  KDTLLocalNotification.cs
//  Iphone Push
//
//  Created by JackWen on 14-1-7.
//  Copyright (c) 2014年 JackWen. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System;
using GCGame.Table;

public class KDTLLocalNotification : MonoBehaviour {
	
	private void Awake() {
#if UNITY_IPHONE
        ClearNotifications();
#endif
        DontDestroyOnLoad( gameObject );
        Debug.Log("KDTL_LocalNotification");
    }
	
	private void OnApplicationPause( bool pause )
    {
#if UNITY_IPHONE
        AddNotification(pause);
#endif
    }
    /////////////////////////////////////////////////////////////////////

    // 添加本地推送信息
    // @params: alterAction - 推送标题
    // @params: alterBody - 推送信息
    // @params: fireDate - 推送发送日
    // @return: true添加成功，false添加失败
#if UNITY_IPHONE
    public bool Add (string alterAction, string alterBody, System.DateTime fireDate, bool loop_day) {
        LocalNotification notification = new LocalNotification();
        //notification.alertAction = alterAction;
        notification.alertBody = alterBody;
        notification.fireDate = fireDate;
		notification.soundName = LocalNotification.defaultSoundName;
		
		if (loop_day) {
			notification.repeatInterval = CalendarUnit.Day;
		}
		else {
			notification.repeatInterval = CalendarUnit.Weekday;
		}
		
        NotificationServices.ScheduleLocalNotification( notification );

        Debug.Log( "Add local notification, fire date: " + fireDate );

        return true;
    }

    // 清理本地推送信息
    public void ClearNotifications()
    {
        NotificationServices.ClearLocalNotifications();
        NotificationServices.CancelAllLocalNotifications();
    }
	
	//根据当前时间，以及目标星期、目标具体时间，返回距离时间
	public DateTime Now2Week(int week, int time){
		DateTime _today = DateTime.Now;
		int _hour = time / 60;
		int _min = time % 60;
		int _week = 1;
		
		if (_today.DayOfWeek == DayOfWeek.Monday) {
			_week = 1;
		} else if (_today.DayOfWeek == DayOfWeek.Tuesday) {
			_week = 2;
		} else if (_today.DayOfWeek == DayOfWeek.Wednesday) {
			_week = 3;
		} else if (_today.DayOfWeek == DayOfWeek.Thursday) {
			_week = 4;
		} else if (_today.DayOfWeek == DayOfWeek.Friday) {
			_week = 5;
		} else if (_today.DayOfWeek == DayOfWeek.Saturday) {
			_week = 6;
		} else if (_today.DayOfWeek == DayOfWeek.Sunday) {
			_week = 7;
		}
		int _time = _today.Hour*12 + _today.Minute;
		int tempDay;
		if (_time < time && week == _week ) {
			tempDay = 0;
		} else {
			tempDay = (week > _week)?(week-_week):(week-_week+7);
		}
		_today = _today.AddDays(tempDay);
		
		DateTime _resTime = new DateTime(_today.Year, _today.Month, _today.Day, _hour, _min, 0);
		if (week == 0) {
			DateTime _resDayTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, _hour, _min, 0);
			return _resDayTime;
		}
		return _resTime;
	}
	
	public void AddNotification(bool addNote){
		if (addNote) {
			foreach (DictionaryEntry de in TableManager.GetPush()) {
				Tab_Push tp = de.Value as Tab_Push;
				if (tp == null)	continue;
				Tab_Language _body = TableManager.GetLanguageByID(tp.Text);
				if (_body == null)	continue;
				DateTime _fireDay = Now2Week(tp.Week, tp.Time);
				bool _dayloop = false;
				if (tp.Week == 0){
					_dayloop = true;
				}
				Add("", _body.Chinese, _fireDay, _dayloop);
			}
		} else {
			ClearNotifications();
		}
	}
#endif
    ////////////////////////////////////////////////////////////////////
}
