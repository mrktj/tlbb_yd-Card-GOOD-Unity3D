using UnityEngine;
using System.Collections;
// We need this one for importing our IOS functions
using System.Runtime.InteropServices;

//sdk 已有dev_key 并测试，登陆不上tw网站进行验证.
//free4u 

public class AppsFlyer : MonoBehaviour {

#if UNITY_IPHONE
	[DllImport("__Internal")]
	private static extern void mTrackEvent(string eventName,string eventValue);
	
	[DllImport("__Internal")]
	private static extern void initSession(string appsFlyerID,string appleAppId);
	
	
	public static void trackEvent(string eventName,string eventValue){
		mTrackEvent(eventName,eventValue);
	}
	
	public static void Init(string appsFlyerID,string appleAppId){
		initSession (appsFlyerID,appleAppId);
	}

	
#elif UNITY_ANDROID
	private static AndroidJavaClass cls_AppsFlyer = new AndroidJavaClass("com.appsflyer.AppsFlyerLib");
	private static AndroidJavaClass unityPlayerClass;
	
	public static void trackEvent(string eventName,string eventValue){
		using(AndroidJavaObject context = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")
			.Call<AndroidJavaObject>("getApplicationContext")) 
		{
			cls_AppsFlyer.CallStatic("sendTrackingWithEvent", context,eventName, eventValue);
		}		
		
	}
	
	public static void SetAppsFlyerKey(string key)
	{
		cls_AppsFlyer.CallStatic("setAppsFlyerKey", key);
		sendTracking();
	}
	
	private static void sendTracking()
	{
		if(unityPlayerClass == null){
			unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		}
	 
//		 AndroidJavaObject jo = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"); 
//       AndroidJavaObject context =jo.Call<AndroidJavaObject>("getApplicationContext") ;

		using(AndroidJavaObject context = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")
			.Call<AndroidJavaObject>("getApplicationContext")) 
		{
			cls_AppsFlyer.CallStatic("sendTracking", context);
			Debug.Log("AppsFlyer sendTracking");
		}
		
	}
#else
	
	
#endif
}
