using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class WebMediator : MonoBehaviour 
{

	private static WebMediator instance;
	private static bool isClearCache;
	
	private string lastRequestedUrl;
	private bool loadRequest;
	private bool visibility;
	private int leftMargin;
	private int topMargin;
	private int rightMargin;
	private int bottomMargin;

	// Message container class.
	public class WebMediatorMessage 
	{	
	    public string path;      // Message path
	    public Hashtable args;   // Argument table
	
	    public WebMediatorMessage(string rawMessage)
		{
	        // Retrieve a path.
	        var split = rawMessage.Split('?');
	        path = split[0];
	        // Parse arguments.
	        args = new Hashtable();
	        if (split.Length > 1) 
			{
	            foreach (var pair in split[1].Split('&'))
				{
	                var elems = pair.Split('=');
	                args[elems[0]] = WWW.UnEscapeURL(elems[1]);
	            }
	        }
	    }
	}
	
	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
		Install();
#endif
	}
	
	// Update is called once per frame
	void Update () 
	{
	    UpdatePlatform();
    	instance.loadRequest = false;
	
	}
	
	// Install the plugin.
	// Call this at least once before using the plugin.
	public static void Install() 
	{
	    if (instance == null) 
		{
	        GameObject master = new GameObject("WebMediator");
	        DontDestroyOnLoad(master);
	        instance = master.AddComponent<WebMediator>();
			
	        InstallPlatform();
	    }
	}

	// Set margins around the web view.
	public static void SetMargin(int left, int top, int right, int bottom) 
	{
	    instance.leftMargin = left;
	    instance.topMargin = top;
	    instance.rightMargin = right;
	    instance.bottomMargin = bottom;
		
	    ApplyMarginsPlatform();
	}

	// Visibility functions.
	public static void Show()
	{
	    instance.visibility = true;
	}
	
	public static void Hide() 
	{
	    instance.visibility = false;
	}
	
	public static bool IsVisible() {
	    return instance.visibility;
	}

	public static void SetClearCache()
	{
	    isClearCache = true;
	}

	public static void  SetCache()
	{
	    isClearCache = false;
	}

	// Load the page at the URL.
	public static void LoadUrl(string url) 
	{
	    instance.lastRequestedUrl = url;
	    instance.loadRequest = true;
	}


#if UNITY_IPHONE
	// iOS platform implementation.
	
	 [DllImport ("__Internal")] static extern private void _WebViewPluginInstall();
	 [DllImport ("__Internal")] static extern private void _WebViewPluginLoadUrl(string url, bool isClearCache);
	 [DllImport ("__Internal")] static extern private void _WebViewPluginSetVisibility(bool visibility);
	 [DllImport ("__Internal")] static extern private void _WebViewPluginSetMargins(int left, int top, int right, int bottom);
	 [DllImport ("__Internal")] static extern private string _WebViewPluginPollMessage();
	 [DllImport ("__Internal")] static extern private void _WebViewPluginMakeTransparentBackground();
	
	private static bool viewVisibility;
	
	private static void InstallPlatform() 
	{
	    _WebViewPluginInstall();
	}
	
	private static void ApplyMarginsPlatform() 
	{
	    _WebViewPluginSetMargins(instance.leftMargin, instance.topMargin, instance.rightMargin, instance.bottomMargin);
	}
	
	private static void UpdatePlatform() 
	{
	    if (viewVisibility != instance.visibility) 
		{
	        viewVisibility = instance.visibility;
	        _WebViewPluginSetVisibility(viewVisibility);
	    }
	    if (instance.loadRequest) 
		{
	        instance.loadRequest = false;
	        _WebViewPluginLoadUrl(instance.lastRequestedUrl, isClearCache);
	    }
	}
	
	public static WebMediatorMessage PollMessage()
	{
	    string message =  _WebViewPluginPollMessage();
	    return !string.IsNullOrEmpty(message) ? new WebMediatorMessage(message) : null;
	}
	
	public static void MakeTransparentWebViewBackground()
	{
	    _WebViewPluginMakeTransparentBackground();
	}

#elif UNITY_ANDROID
	// Android platform implementation.
		
	private static AndroidJavaClass unityPlayerClass;

	private static void InstallPlatform() 
	{
		if(unityPlayerClass == null){
			unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		}
	}

	private static void ApplyMarginsPlatform() { }

	private static void UpdatePlatform() 
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass!=null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("updateWebView", instance.lastRequestedUrl!=null ? instance.lastRequestedUrl : "", instance.loadRequest, instance.visibility, instance.leftMargin, instance.topMargin, instance.rightMargin, instance.bottomMargin);
				}
			}
		//}
	}

	public static WebMediatorMessage PollMessage()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass!=null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					string message = activity.Call<string>("pollWebViewMessage");
					return message!=null ? new WebMediatorMessage(message) : null;
				}
			}
			return null;
		//}
	}

	public static void MakeTransparentWebViewBackground()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass!=null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("makeTransparentWebViewBackground");
				}
			}
		//}
	}
	
	public static void ShowWebView(string url)
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass!=null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("showWebView", url);
				}
			}
		//}
	}
	
	public static void HideWebView()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass!=null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("hideWebView");
				}
			}
		//}
	}
	
	public static void initSdk()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("initSdk");
					Debug.Log("----initSdk()--");
				}
			}
		//}
	}
	public static void endAnalytics()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("endAnalytics");
					Debug.Log("----endAnalytics()--");
				}
			}
		//}
	}
	//Umeng FeedBack for Android--added at 2013/9/23 19:30
	public static void OpenFeedBack()
	{
		if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
				    //Debug.Log("----startFeedBack()=====Unity-Debug.Log-WebMediator--");
					activity.Call("startFeedBack");
					
				}
			}
	}
	public static void OpenCyouPayWindow(string strPurchaseInfo)
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("openPayWindow", strPurchaseInfo);
				}
			}
		//}
	}
	
	public static string GetArea()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getArea");
				}
			}
			return "BeiJing";
		//}
	}
	public static string GetCountry()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getCountry");
				}
			}
			return "China";
		//}
	}
	public static string GetDeviceSystem()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getDeviceSystem");
				}
			}
			return "Android 4.0";
		//}
	}
	
	public static void ShowExitPanel()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("showExitPanel");
				}
			}
		//}
	}
		public static string GetMacAddress()
	{
		//Debug.Log("----GetMacAddress()=====Unity-Debug.Log-WebMediator--");
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass!=null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getMacAddress");
				}
			}
			return "2c:27:d7:46:50:22";
		//}
	}
	
	public static string GetNetworkType()
	{
		string networkType = "disconnect";
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass!=null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					networkType = activity.Call<string>("getNetworkType").ToLower();
				}
			}
#if UNITY_EDITOR
			return "wifi";
#else
		    if(networkType.Equals("2g"))
			    networkType = "disconnect";
			return networkType;
#endif

		//}
	}
	
	public static string GetModel()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getModel");
				}
			}
			return "Samsung";
		//}
	}
	
	public static string GetOperator()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getOperator");
				}
			}
			return "China Unicom";
		//}
	}
	
	public static string GetChannelID()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getChannelID");
				}
			}
			return "android_googleplay";
		//}
	}
	
	public static string GetServerListUrl()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getServerListUrl");
				}
			}
			return "http://219.232.242.229/ServerList/servers.txt";
		//}
	}
	
	//UC渠道代码重构 相应参数已从java端直接配置 lihao_yd  2013-12-10
	/*
	public static int GetServerID()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<int>("getServerID");
				}
			}
			return -1;
		//}
	}
	
	public static int GetGameID()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<int>("getGameID");
				}
			}
			return -1;
		//}
	}
	public static int GetCPID()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<int>("getCPID");
				}
			}
			return -1;
		//}
	}
	public static string GetAppKey()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getAppKey");
				}
			}
			return "";
		//}
	}
	public static string GetApiKey()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getApiKey");
				}
			}
			return "";
		//}
	}
	public static string GetAppSecret()
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					return activity.Call<string>("getAppSecret");
				}
			}
			return "";
		//}
	}
	*/
	
	
	public static void CopyAssetsFile(string assetsFile, string targetFile)
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("copyAssetsFile", assetsFile, targetFile);
				}
			}
		//}
	}
	public static void ShowUpdate(string version)
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("showUpdate",version);
				}
			}
		//}
	}

	public static void RemovePayLog(string OID)
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("removePayLog",OID);
				}
			}
		//}
	}

	public static void MinusOncePayLog(string OID)
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null){
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
	                Debug.Log("pllog_AndroidJavaObject MinusOncePayLog");
					activity.Call("minusOnceOrder",OID);
				}
			}
		//}
	}

	public static void SavePayLog(string strPayInfo)
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null)
			{
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("savePayLog",strPayInfo);
				}
			}
		//}
	}

	public static void SuccessPayLog(string strOderID)
	{
		//using(AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		//{
			if(unityPlayerClass != null)
			{
				using(AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) 
				{
					activity.Call("successOrder",strOderID);
				}
			}
		//}
	}

	

#else 
	// Unity Editor implementation.
	
	private static void InstallPlatform() { }
	private static void UpdatePlatform() { }
	private static void ApplyMarginsPlatform() { }
	public static WebMediatorMessage PollMessage()  { return null; }
	public static void MakeTransparentWebViewBackground() { }
#endif
	
#if UNITY_ANDROID	
	public delegate void CallBackCloseWebview();
	public CallBackCloseWebview callBackCloseWebView = null;
	
	public static void SetOnCallBackCloseWebView(CallBackCloseWebview onCall){
		instance.callBackCloseWebView = onCall;
	}
	
	public void OnCloseWebView(string str){
		
		Debug.Log("Close WebView");
		if(callBackCloseWebView != null){
			instance.callBackCloseWebView();
			instance.callBackCloseWebView = null;
		}
	}	
#endif	
}
