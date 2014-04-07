using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

//sdk 测试通过,在官网上查阅;

public class mobileAppTracker : MonoBehaviour {
#if UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void startTrackerWithMATAdvertiserId(string MAT_ADVERTISER_ID,
		string MAT_CONVERSION_KEY);
	
	[DllImport("__Internal")]
	private static extern void trackInstall();
	
	[DllImport("__Internal")]
	private static extern void trackActionForEventIdOrName(string tranckName,bool isId);
	
	
	public static void StartTrackerWithMATAdvertiserId(string MAT_ADVERTISER_ID,
		string MAT_CONVERSION_KEY){
		if (Application.platform == RuntimePlatform.IPhonePlayer){
			startTrackerWithMATAdvertiserId(MAT_ADVERTISER_ID,MAT_CONVERSION_KEY);
			TrackInstall();
		}else{
			Debug.Log("startTrackerWithMATAdvertiserId");
		}
		
	}
	
	
	public static void TrackInstall(){
		if (Application.platform == RuntimePlatform.IPhonePlayer){
			trackInstall();
		}else{
			Debug.Log("trackInstall");
		}
	}
	
	public static void TrackActionForEventIdOrName(string tranckName,bool isId){
		if (Application.platform == RuntimePlatform.IPhonePlayer){
			trackActionForEventIdOrName(tranckName,isId);
		}else{
			Debug.Log("TrackActionForEventIdOrName");
		}
	}


	
#elif UNITY_ANDROID
	private static AndroidJavaObject cls_mobileAppTracker;
	private static AndroidJavaObject currentActivity;
	
	public static void trackEvent(string eventName,string eventValue){
		cls_mobileAppTracker.CallStatic("sendTrackingWithEvent", eventName, eventValue);
	}
	
	public static void SetAppsFlyerKey(string key)
	{
		cls_mobileAppTracker.CallStatic("setAppsFlyerKey", key);
	}
	
	public static void start(string app_id, string app_key) {
		if(currentActivity == null){
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		}
		
		if(currentActivity != null){
			cls_mobileAppTracker = new AndroidJavaObject("com.mobileapptracker.MobileAppTracker",currentActivity,app_id,app_key);
			StartAppTrack();
		}else{
			Debug.LogError("Init cls_mobole failed!");
		}
		
	}
		
	private static void StartAppTrack(){
		if(cls_mobileAppTracker != null){
			//cls_mobileAppTracker.Call("setAllowDuplicates",true);
			//cls_mobileAppTracker.Call("setDebugMode",true);
			cls_mobileAppTracker.Call<int>("trackInstall");
			Debug.Log("cls_mobileAppTracker.trackInstall");
		}else{
			Debug.LogError("cls_mobileAppTracker null error!");
		}
	}
	
	//the example is ("purchase",1.99, 'USD");
	//but i don't know the third param's format;
	public static void SendPurchases(string eventName,double money ,string currency){
		if(cls_mobileAppTracker != null){
			cls_mobileAppTracker.Call<int>("trackAction",eventName,money,currency);
		}else{
			Debug.LogError("cls_mobileAppTracker null error!");
		}
	}
	
	public static void TrackAction(string eventName,double value){
		if(cls_mobileAppTracker != null){
			cls_mobileAppTracker.Call<int>("trackAction",eventName,value);
		}else{
			Debug.LogError("cls_mobileAppTracker null error!");
		}
	}
#else
	
	
#endif
	
}
