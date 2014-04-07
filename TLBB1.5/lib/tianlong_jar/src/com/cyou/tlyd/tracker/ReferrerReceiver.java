/**
 * 
 */
/**
 * @author zhouwei_yd
 *
 */
package com.cyou.tlyd.tracker;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;


public class ReferrerReceiver extends BroadcastReceiver {
	@Override
	public void onReceive(Context context, Intent intent) {
		new com.koocell.free4u.sdk.ReferrerReceiver().onReceive(context,
		intent);

		//new com.mobileapptracker.Tracker().onReceive(context, intent);
		new com.appsflyer.AppsFlyerLib().onReceive(context, intent);
		//添加到mainfest中
		Log.d("SDK", "ReferrerReceiver ");
	// 您的其他代碼...
	// …...
	}
}