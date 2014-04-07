package com.cyou.mrd.analytics;

//import com.flurry.android.FlurryAgent;
//import com.google.analytics.tracking.android.EasyTracker;
//import com.google.analytics.tracking.android.Log;
//import com.inmobi.adtracker.androidsdk.IMAdTracker;
//import com.umeng.analytics.MobclickAgent;
//import com.umeng.fb.FeedbackAgent;

import android.app.Activity;

public class AnalyticsUtil {
	public static final String INMOBI_APPID = "7eb69505881640c297412007a5085502";
	public static final String FLURRY_APPID = "JNXS9WFPVFSRCK4CVKZ2";
	private Activity activity;
	public AnalyticsUtil(Activity activity){
		this.activity = activity;
	}
	public void initAnalyticsSdk(boolean isThirdPlatform){
		try{
			//接入SDK的第三方渠道不使用友盟，Flurry等统计功能
			if(!isThirdPlatform){
//				Log.e("-----initSdk");
//				IMAdTracker.getInstance().init(activity, INMOBI_APPID); 
//				IMAdTracker.getInstance().reportAppDownloadGoal();
//		    	FlurryAgent.onStartSession(activity, FLURRY_APPID);
//		    	MobclickAgent.onResume(activity);
//		    	EasyTracker.getInstance(activity).activityStart(activity);  // Add this method.
			}else {
//				Log.e("----isThirdPlatform = ture,do not init AnalyticsSdk");
			}
		}catch(Throwable e){
			e.printStackTrace();
		}
	}
	public void startAnalytics(){
		
	}
	public void endAnalytics(boolean isThirdPlatform){
		try{
			if(!isThirdPlatform){
//				Log.e("-----endAnalytics");
//				FlurryAgent.onEndSession(activity);
//				EasyTracker.getInstance(activity).activityStop(activity);
//				MobclickAgent.onPause(activity);
			}
		}catch(Throwable e){
			e.printStackTrace();
		}
	}
	public void startFeedBack(){
//		System.out.println("------startFeedBack");
//		FeedbackAgent agent = new FeedbackAgent(activity);
//	    agent.startFeedbackActivity();
	}
}
