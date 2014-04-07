using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class ResUpdate {
	
#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void InitUpdate(); 
	 
#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void SetAppWorkDir(string dir); 
	 
#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void SetResourceUrl(string url); 

#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void SetAppRoot(string root); 

#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void SetResourceFormat(string format); 

#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void UpdateTick();

#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern int GetPercent();

#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void SetIsUpdate(bool isupdate);
	
#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void ExitUpdate();

#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern int GetCurrentDownNumber();
	
#if UNITY_IPHONE || UNITY_XBOX360
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern int GetTotalDownNumber();
#if UNITY_IPHONE || UNITY_XBOX36
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern int GetPushEventType();
	
#if UNITY_IPHONE || UNITY_XBOX36
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern uint GetDownSize();
	
#if UNITY_IPHONE || UNITY_XBOX36
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern void SetAppVersion(string version);
	
#if UNITY_IPHONE || UNITY_XBOX36
	[DllImport ("__Internal")]
#else
	[DllImport ("CymrdResUpdate")]
#endif
	public static extern string GetAppVersion();
}




