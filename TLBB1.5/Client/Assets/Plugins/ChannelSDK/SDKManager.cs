using UnityEngine;
using System.Collections;
using System;

public class SDKManager : MonoBehaviour {
	
	private static  SDKManager instance;
	
	public static void Install()
	{
	    if (instance == null) 
		{
	        GameObject master = new GameObject("SDKManager");
	        DontDestroyOnLoad(master);
	        instance = master.AddComponent<SDKManager>();
			
	        InstallPlatform();
	    }
	}
	
	private static  void InstallPlatform()
	{
#if UNITY_EDITOR
		Debug.Log("Calculate SDK Loaded Success");
#elif UNITY_ANDROID
		
		try{
			//Partytrack.start(999,"a47a0091f2bceb1fe62d2c35d1e7b36c");
			
			//mobileAppTracker
			//mobileAppTracker.start("14290","0bbd59fe5dfc24f228dcc5cd474c3101");		
			
			//AppsFlyer
			AppsFlyer.SetAppsFlyerKey("ArionkcSN5TLiBtLuwGaf5");
			
			//chartboots
			Chartboost.CBManager cbManager = new GameObject("CBManager").AddComponent<Chartboost.CBManager>();
			DontDestroyOnLoad(cbManager);
			Chartboost.CBBinding.init();
			Debug.Log("Channel SDK Loaded Success");
		}catch(Exception e){
			Debug.LogError("SDK Loaded Error" + e.ToString());
			//throw e;
		}
#elif UNITY_IPHONE
		try{		
			//mobileAppTracker
			//mobileAppTracker.StartTrackerWithMATAdvertiserId("14290","0bbd59fe5dfc24f228dcc5cd474c3101");
			
			//AppsFlyer
			AppsFlyer.Init("ArionkcSN5TLiBtLuwGaf5","744943894");
			
			//chartboot
			Chartboost.CBManager cbManager = new GameObject("CBManager").AddComponent<Chartboost.CBManager>();
			DontDestroyOnLoad(cbManager.gameObject);
			Chartboost.CBBinding.init("5307acc52d42da0e78f0e491","69602b6cd8c90c07653624385947a1b2f4826c9e");

			Debug.Log("Channel SDK Loaded Success");
		}catch(Exception e){
			Debug.LogError("SDK Loaded Error" + e.ToString());
			//throw e;
		}
#endif
	}
	
	public static void  trackEvent(string eventName,string eventValue)
	{
#if UNITY_EDITOR
		Debug.Log("sendTrackingWithEvent:"+eventName+" value:"+eventValue);
#elif UNITY_ANDROID	|| UNITY_IPHONE	
		//AppsFlyer
		AppsFlyer.trackEvent(eventName,eventValue);
		Debug.Log("AppsFlyer sendTrackingWithEvent:"+eventName+" value:"+eventValue);
#endif		
	}
	void ApplicationQuit(){
		instance = null;
	}
}
