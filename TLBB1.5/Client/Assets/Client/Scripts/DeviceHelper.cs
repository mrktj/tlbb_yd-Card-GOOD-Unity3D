using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using card;

public class DeviceHelper
{
	public static string CHANNEL_PP = "pp";
	
    [DllImport("__Internal")]
    private static extern string _getNetworkProvider();

    public static string GetNetWorkProvider()
    {
#if UNITY_ANDROID
		return WebMediator.GetOperator();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getNetworkProvider() : "UNKNOWN";
#endif
    }

    [DllImport("__Internal")]
    private static extern string _getUserContry();

    public static string GetUserContry()
    {
#if UNITY_ANDROID
		return WebMediator.GetCountry();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getUserContry() : "";
#endif
    }

    [DllImport("__Internal")]
    private static extern string _getUserLocale();

    public static string GetUserLocale()
    {
#if UNITY_ANDROID
		return WebMediator.GetArea();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getUserLocale() : "";
#endif
    }

    [DllImport("__Internal")]
    private static extern bool _isJailbroken();

    public static bool IsJailbroken()
    {
        return Application.platform == RuntimePlatform.IPhonePlayer ? _isJailbroken() : false;
    }

    [DllImport("__Internal")]
    private static extern int _getNetworkState();

    public static string GetNetworkState()
    {
#if UNITY_ANDROID
		return WebMediator.GetNetworkType();
#else
        int state = Application.platform == RuntimePlatform.IPhonePlayer ? _getNetworkState() : 3;

        if (state <= 0)
        {
            return "disconnect";
        }
        else if (state == 1)
        {
            return "wwlan";
        }
        else if (state == 2)
        {
            return "wifi";
        }
        else
        {
            return "unknown";
        }
#endif
    }

    [DllImport("__Internal")]
    private static extern string _getSystemName();

    public static string GetSystemName()
    {
#if UNITY_ANDROID
		return "Android";
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getSystemName() : "win32";
#endif
    }

    [DllImport("__Internal")]
    private static extern string _getDeviceName();

    public static string GetDeviceName()
    {
#if UNITY_ANDROID
		return WebMediator.GetModel();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getDeviceName() : "";
#endif
    }

    [DllImport("__Internal")]
    private static extern string _getMacAddress();

    public static string GetMacAddress()
    {
#if UNITY_ANDROID
		return WebMediator.GetMacAddress();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getMacAddress() : Utils.TestMacAddress();
#endif
    }

    [DllImport("__Internal")]
    private static extern string _getChannelID();

    public static string GetChannelID()
    {
#if UNITY_ANDROID
		return AndroidConfig.GetChannelID();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getChannelID() : "win32";
#endif
    }

    [DllImport("__Internal")]
    private static extern void _setupAnalytics();

    public static void SetupAnalytics()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _setupAnalytics();
        }

    }

    [DllImport("__Internal")]
    private static extern void _initState();

    public static void InitState()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _initState();
        }

    }

    [DllImport("__Internal")]
    private static extern bool _useConfigIp();

    public static bool IsUseConfigIp()
    {
#if UNITY_ANDROID
		return AndroidConfig.IsUseConfigIp();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _useConfigIp() : false;
#endif
    }

    [DllImport("__Internal")]
    private static extern string _getConfigIp();

    public static string GetConfigIp()
    {
#if UNITY_ANDROID
		return AndroidConfig.GetConfigIp();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getConfigIp() : "";
#endif
    }

    [DllImport("__Internal")]
    private static extern void _iapppayBuy(string szOrder, int orderID);

    public static bool IAppPayBuy(string szOrder, int orderID)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _iapppayBuy(szOrder, orderID);
            return true;
        }

        return false;
    }
	
	

    [DllImport("__Internal")]
    private static extern int _iapSytle();

    public static int IAPStyle()
    {
#if UNITY_ANDROID
		return AndroidConfig.getIAPStyle();
#else		
        return Application.platform == RuntimePlatform.IPhonePlayer ? _iapSytle() : 3;
#endif		
    }

    [DllImport("__Internal")]
    private static extern void _openUMFeed();

    public static void OpenUMFeed()
    {
#if UNITY_ANDROID
		WebMediator.OpenFeedBack();
#else
		if(Application.platform == RuntimePlatform.IPhonePlayer) _openUMFeed();
#endif
    }

    [DllImport("__Internal")]
    private static extern void _sendTrackPurchase();

    public static void SendTrackPurchase()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) _sendTrackPurchase();
    }

    [DllImport("__Internal")]
    private static extern void _sendTrackLogin();

    public static void SendTrackLogin()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) _sendTrackLogin();
    }

    [DllImport("__Internal")]
    private static extern string _getServerListUrl();

    public static string GetServerListUrl()
    {
#if UNITY_EDITOR
		return ClientConfigure.getServersListURL();
		//国内畅游渠道服务器列表,1.5;劳资也没办法啊;
#elif UNITY_ANDROID
		return ClientConfigure.getServersListURL();
#else
        return ClientConfigure.getiOSServerLIstURL();
#endif
    }

    [DllImport("__Internal")]
    private static extern string _getUpdateServerUrl();

    public static string GetUpdateServerUrl()
    {
#if UNITY_ANDROID
		return AndroidConfig.GetUpdateServerUrl();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getUpdateServerUrl() : "http://files2.changyou.com:80";
#endif
        
    }

    [DllImport("__Internal")]
    private static extern string _getUpdateServerRoot();

    public static string GetUpdateServerRoot()
    {
#if UNITY_ANDROID
		return AndroidConfig.GetUpdateServerRoot();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getUpdateServerRoot() : "tlydtest/";
#endif
    }

	[DllImport("__Internal")]
    private static extern string _getVersionNumber();

    public static string GetVersionNumber()
    {
#if UNITY_ANDROID
		return AndroidConfig.GetVersionNumber();
#else
		return Application.platform == RuntimePlatform.IPhonePlayer ? _getVersionNumber() : ClientConfigure.GetClientVersion();
#endif
    }
	
	[DllImport("__Internal")]
    private static extern string _prepareToUpdate();

    public static void PrepareToUpdate()
    {
        if(Application.platform == RuntimePlatform.IPhonePlayer) _prepareToUpdate() ;
    }
	
	// PP login
	[DllImport("__Internal")]
    private static extern void _ppLogin();

    public static void PPLogin()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) _ppLogin();
    }

    [DllImport("__Internal")]
    private static extern void _ppCenter();

    public static void PPCenter()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) _ppCenter();
    }

	[DllImport("__Internal")]
    private static extern bool _ppBuy(int price, string strOrder, string title, string userID, int zoneID);

    public static bool PPBuy(int price, string strOrder, string title, string userID, int zoneID)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _ppBuy(price, strOrder, title, userID, zoneID);
            return true;
        }

        return false;
    }

    [DllImport("__Internal")]
    private static extern string _getNoticeUrl();

    public static string GetNoticeUrl()
    {
#if UNITY_ANDROID
		return AndroidConfig.getNoticeUrl();
		
#else
		//return Application.platform == RuntimePlatform.IPhonePlayer ? _getNoticeUrl() : "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_ios.html";
		return  ClientConfigure.getIOSNoticeURL();
#endif
    }

    [DllImport("__Internal")]
    private static extern void _openVersionUpdateUrl();

    //Žò¿ªžüÐÂœçÃæ£¬Õý°æiosµœAppstore,ÆäËüµœ¶ÔÓŠµÄÍøÒ³
    public static void OpenVersionUpdateUrl()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _openVersionUpdateUrl();
        }
    }

    [DllImport("__Internal")]
    private static extern void _autoLockScreen(bool auto);

    public static void AutoLockScreen(bool auto)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _autoLockScreen(auto);
        }
    }
	
    [DllImport("__Internal")]
    private static extern void _showExitDialog(string title ,string message);
	
	public static void ShowExitDialog(){
		
#if UNITY_IPHONE
	_showExitDialog("title","message");
#elif UNITY_ANDROID
	WebMediator.ShowExitPanel();
#endif	
	
	}
}
