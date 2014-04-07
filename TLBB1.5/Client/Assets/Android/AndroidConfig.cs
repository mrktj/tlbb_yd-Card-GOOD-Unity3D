using UnityEngine;
using System.Collections;
using xjgame.message;

public class AndroidConfig : MonoBehaviour {
#if UNITY_ANDROID
	//public const String VERSION_NONE = "unknown";
	//public const String VERSION_TB = "android_tb";
	//public const String VERSION_91 = "android_91";
	//public const String VERSION_UC = "android_uc";
	//public const String VERSION_APPSTORE = "android_appstore";
	//public const String VERSION_DUOKU = "android_duoku";
	// 不知道什么作用 //
	public const string CONFIG_IP = "10.127.131.24:8083";
	// android测试服务器，临时用//
	//private const string SERVERLISTURL = "http://219.232.242.229:80/XJGameServer/servers.txt";//外网//
	//private const string SERVERLISTURL = "http://219.232.242.229:8081/XJGameServer/servers.txt";//UC测试//
	//private const string SERVERLISTURL = "http://10.6.155.106:8080/XJGameServer/servers.txt";//测试间网//
	// 正式服的//
	//public const string SERVERLISTURL = "http://files2.changyou.com/tlydand/servers.txt";
	
	public const string UPDATESERVER_URL = "http://files2.changyou.com";//资源自动更新链接地址,即更新资源存放的服务器地址//
	//public const string UPDATESERVER_URL = "http://10.6.155.106:8080";//资源自动更新链接地址,即更新资源存放的服务器地址//
	
	//apk包自动更新下载地址==========================================================//
	//public const string UPDATEAPK_URL = "http://files2.changyou.com/tlydand/tlyd.apk";//已写死在jar包中//
	
	public const string UPDATESERVER_ROOT = "tlyd/";//自动更新资源在服务器存放的文件夹名
	//public const string UPDATESERVER_ROOT = "tlydAndroidUpdate/";//自动更新资源在服务器存放的文件夹名
	
	// 版本 //
	public const string V_NUMBER = "1.5.0.0";//后面加的东西非int型，可能影响UC版本中对版本号的解析//
	
#if UNITY_ANDROID
		public static  string NoticeURL = ClientConfigure.getNoticeURL();
		//public const string NoticeURL = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android.html";
		public const string NoticeURL_UC = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_uc.html";
		public const string NoticeURL_360 = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_360.html";
		public const string NoticeURL_91 = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_91.html";
		public const string NoticeURL_duoku = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_duoku.html";
		public const string NoticeURL_xiaomi = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_mi.html";
	    public const string NoticeURL_wandoujia = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_wandoujia.html";
	    public const string NoticeURL_oppo = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_oppo.html";
	    public const string NoticeURL_anzhi = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_anzhi.html";
	    public const string NoticeURL_sogou = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_sougou.html";
	    public const string NoticeURL_huawei = "http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_android_huawei.html";
	
#endif
	
	public const int PLATFORM_CYOU = 0;
	public const int PLATFORM_UC = 5;//整合UC渠道代码 lihao_yd 2013-11-25
	
	private static string appkey = null;
	private static int cpid = 0;
	private static int gameid = 0;
	private static int serverid = 0;
	private static string servername = null;
	private static string apiKey = null;
	private static string appsecret = null;
	private static string UcAccount = null;
	private static string thirdLoginInfo = null;
	
	public static string GetThirdLoginInfo()
	{
		if(thirdLoginInfo == null){
			return "";
		}
		return thirdLoginInfo;
	}
	public static void SetThirdLoginInfo(string str){
		if(str == null){
			AndroidConfig.thirdLoginInfo = "";
			return;
		}
		AndroidConfig.thirdLoginInfo = str;
	}
		
	public static string GetUcAccount()
	{
		if(UcAccount == null){
			return "";
		}
		return UcAccount;
	}
	public static void SetUcAccount(string UcAccount){
		if(UcAccount == null || UcAccount.Length == 0)
			return;
		AndroidConfig.UcAccount = UcAccount;
	}
	
	public static string GetChannelID()
	{
		return WebMediator.GetChannelID();
	}
	//UC代码重构 lihao_yd 2013-12-10
	/*
	public static int GetServerID()
	{
		if(serverid == 0){
			serverid = WebMediator.GetServerID();
		}
		return serverid;
	}
	public static void SetServerID(int serverid){
		if(serverid == 0)
			return;
		AndroidConfig.serverid = serverid;
	}
	public static int GetGameID()
	{
		if(gameid == 0){
			gameid = WebMediator.GetGameID();
		}
		return gameid;
	}
	public static void SetGameID(int gameid){
		if(gameid == 0)
			return;
		AndroidConfig.gameid = gameid;
	}
	public static int GetCPID()
	{
		if(cpid == 0){
			cpid = WebMediator.GetCPID();
		}
		return cpid;
	}
	public static void SetCPID(int cpid){
		if(cpid == 0)
			return;
		AndroidConfig.cpid = cpid;
	}
	public static string GetAppKey()
	{
		if(appkey == null){
			appkey = WebMediator.GetAppKey();
		}
		return appkey;
	}
	public static void SetAppKey(string appkey){
		if(appkey == null || appkey.Length == 0)
			return;
		AndroidConfig.appkey = appkey;
	}
	public static string GetApiKey()
	{
		if(apiKey == null){
			apiKey = WebMediator.GetApiKey();
		}
		return apiKey;
	}
	public static void SetApiKey(string apiKey){
		if(apiKey == null || apiKey.Length == 0)
			return;
		AndroidConfig.apiKey = apiKey;
	}
	
	public static string GetAppSecret()
	{
		if(appsecret == null){
			appsecret = WebMediator.GetAppSecret();
		}
		return appsecret;
	}
	public static void SetAppSecret(string appsecret){
		if(appsecret == null || appsecret.Length == 0)
			return;
		AndroidConfig.appsecret = appsecret;
	}
	*/
	
	public static bool isUCChannel()
	{
		return AndroidConfig.GetChannelID().Equals("android_uc");
	}
	public static bool is360Channel()
	{
		return AndroidConfig.GetChannelID().Equals("android_360");
	}
	public static bool is91Channel()
	{
		return AndroidConfig.GetChannelID().Equals("android_91");
	}
	public static bool isDuokuChannel()
	{
		return AndroidConfig.GetChannelID().Equals("android_duoku");
	}
	public static bool isXiaoMiChannel()
	{
		return AndroidConfig.GetChannelID().Equals("android_xiaomi");
	}
	public static bool isWanDouJiaChannel()
	{
		return AndroidConfig.GetChannelID().Equals("android_wandoujia_sdk");
	}
	public static bool isOPPOChannel()
	{
		return AndroidConfig.GetChannelID().Equals("android_oppo");
	}
	public static bool isAnZhiChannel()
	{
		return AndroidConfig.GetChannelID().Equals("android_anzhi");
	}
	public static bool isSogouChannel()
	{
		return AndroidConfig.GetChannelID().Equals("android_sogou_sdk");
	}
	public static bool isHuaweiChannel()
	{
		return AndroidConfig.GetChannelID().Equals("android_huawei");
	}

	//用于涉及到第三方SDK的判断 lihao_yd
	public static bool isThirdSDKPlatform()
	{
		if(is360Channel()||is91Channel()||isDuokuChannel()||isXiaoMiChannel()||
			isWanDouJiaChannel()|| isOPPOChannel()|| isAnZhiChannel()||isSogouChannel()||isHuaweiChannel()){
			return true;
		}else{
			return false;
		}
	}
	
	public static bool isLogin(){
		//整合UC渠道代码 lihao_yd 2013-11-25
		//if(isUCChannel())
		//{
		//	return !string.IsNullOrEmpty(UCGameSdk.getSid());
		//}else 
		if(isUCChannel()||isThirdSDKPlatform()){
			return !string.IsNullOrEmpty(GetThirdLoginInfo());
		}
		return AccountManager.Instance.CurAccount != null;
	}
	public static string GetConfigIp()
	{
		return AndroidConfig.CONFIG_IP;
	}
	public static string GetServerListUrl()
	{
		//Debug.Log("ServerListUrl = "+WebMediator.GetServerListUrl());
		return WebMediator.GetServerListUrl();
		/*
		if(AndroidConfig.isUCChannel()){
			//return "http://219.232.242.229:8081/XJGameServer/servers.txt";// UC测试 //
			//return "http://files2.changyou.com/tlydand_uc/servers.txt";// UC正式 //
			//return "http://10.6.153.251:9090/XJGameServer/servers.txt";//10月29日半夜测试UC大礼包//
			return "http://10.6.155.106:8080/XJGameServer/servers.txt";//玄武岛测试间服务器//
		}else{
			//return "http://219.232.242.229:80/XJGameServer/servers.txt";// 官网测试 //
			//return "http://files2.changyou.com/tlydand/servers.txt";// 官网正式 //
			//return "http://10.6.153.251:9090/XJGameServer/servers.txt";
			return "http://10.6.155.106:8080/XJGameServer/servers.txt";//玄武岛测试间服务器//
		}
		*/
		//return AndroidConfig.SERVERLISTURL;
	}
	public static string GetVersionNumber()
	{
		return ClientConfigure.GetClientVersion();
	}
	
	public static bool IsUseConfigIp()
	{
		return true;
	}
	
	public static string GetUpdateServerUrl()
	{
		return AndroidConfig.UPDATESERVER_URL;
	}
	
	public static string GetUpdateServerRoot()
	{
		return AndroidConfig.UPDATESERVER_ROOT;
	}
	
	public static string GetSdcardRoot()
	{
		using (AndroidJavaClass cls = new AndroidJavaClass("com.cyou.mrd.libs.ALSystemLib"))
		{
			return cls.CallStatic<string>("getSdcardRoot");
		}
		return "";
	}
	
	public static void showFloatButton()
	{
		if(isUCChannel()||isThirdSDKPlatform()){
			PaySystemInterface.doSdk("showFloatButton",null);
		}
	}
	public static void hideFloatButton()
	{
	    if(isUCChannel()||isThirdSDKPlatform()){
			PaySystemInterface.doSdk("hideFloatButton",null);
		}
	}
	
	public static string getNoticeUrl()
	{
		if(AndroidConfig.isUCChannel())
		{
			return NoticeURL_UC;
		}else if(AndroidConfig.is360Channel()){
			return NoticeURL_360;
		}else if(AndroidConfig.is91Channel()){
			return NoticeURL_91;
		}else if(AndroidConfig.isDuokuChannel()){
			return NoticeURL_duoku;
		}else if(AndroidConfig.isXiaoMiChannel()){
			return NoticeURL_xiaomi;
		}
		else if(AndroidConfig.isWanDouJiaChannel()){
			return NoticeURL_wandoujia;
		}
		else if(AndroidConfig.isOPPOChannel()){
			return NoticeURL_oppo;
		}
		else if(AndroidConfig.isHuaweiChannel()){
			return NoticeURL_huawei;
		}
		else if(AndroidConfig.isAnZhiChannel()){
			return NoticeURL_anzhi;
		}
		else if(AndroidConfig.isSogouChannel()){
			return NoticeURL_sogou;
		}
		else
		{
			return NoticeURL;
		}
	}
	
	public static bool showLoginUI(){
		if(isUCChannel()||isThirdSDKPlatform())
		{
			// UC 未登录则弹出登录窗口 //
			if(!AndroidConfig.isLogin())
			{
				Debug.Log("----showLoginUI");
				AccountManager.Instance.ShowLoginUI();
				//----------------添加服务器记录---------------------
				PlayerPrefs.SetString("LastServer", card.net.HTTPClientAPI.uri.ToString());
				return true;
			}
		}
		return false;
	}
	
	public static void directShowLoginUI()
	{
		Debug.Log("----directShowLoginUI");
		//if(AndroidConfig.isUCChannel()){
			// 显示登录 //
			AccountManager.Instance.ShowLoginUI();
		//}
	}
	
	public static string getBindAccount(){
		if(AndroidConfig.isUCChannel()||isThirdSDKPlatform()){
			return AndroidConfig.GetUcAccount();//第三方账号//
		}else{
			return Games.CharacterLogic.AccountInfo.Base64Decode(AccountManager.Instance.CurAccount.email);
		}
	}
	public static string getLoginAccountID(){
		//整合UC渠道代码 lihao_yd 2013-11-25
		//if(AndroidConfig.isUCChannel()){
		//	return UCGameSdk.getSid();
		//}else 
		if(AndroidConfig.isUCChannel()||isThirdSDKPlatform()){
			return GetThirdLoginInfo();
		}
		return null;
	}
	
	public static bool channelLogin(){
		//整合UC渠道代码 lihao_yd  2013-11-25
		//if(AndroidConfig.isUCChannel()){
			// UC登录//
		//	UCGameSdk.login(UCConfig.enableGameAccount, null);
		//	return true;
		//}else 
		if(isUCChannel()||isThirdSDKPlatform()){
			//清空登陆数据
			AndroidConfig.SetThirdLoginInfo("");
			PaySystemInterface.doSdk("doLogin",null);
			return true;
		}
		return false;
	}
	public static bool showAccountManageUI(){
		if(AndroidConfig.isUCChannel()||isThirdSDKPlatform()){
			// UC登录窗//
			//UCGameSdk.logout();
			return true;
		}
		return false;
	}
	
	public static void switchAccountRefresh(GameObject accountsUI){
		if(AndroidConfig.isUCChannel()||isThirdSDKPlatform())
		{
			accountsUI.GetComponent<CyouAccounts>().SwitchRefresh();
		}
	}
	
	public static void SCVerifyCharge(byte[] data){
		SCCYouPayVerifyChargeRet cyouPayResult = new SCCYouPayVerifyChargeRet();
		cyouPayResult.ParseFrom(data);
		PurchaseHelper.PayVarifyResult = cyouPayResult.Result;
		PurchaseHelper.PayVarifyResultOrderID = cyouPayResult.OrderId;
		Debug.Log("pllog_ResultDollar = "+cyouPayResult.PlayerDollar);
		Games.CharacterLogic.Obj_MyselfPlayer.GetMe().dollar = cyouPayResult.PlayerDollar;
	}
	
	public static bool versionWrong(string version){
		
		if(string.IsNullOrEmpty(version)||version.Contains("null")){
			return false;
		}
		Debug.Log("--version = "+version);
		if(true){//如果有渠道不需要更新 在这里添加
			WebMediator.ShowUpdate(version);
			return true;
		}
		return false;
	}
	//获取billing分配的appkey，豌豆荚渠道订单号用//
	public static string getBillingAppkey(){
		
		Debug.Log("----Unity3D getBillingAppkey-for-wdj-orderId");

		using (AndroidJavaClass cls = new AndroidJavaClass("com.cyou.tlyd.android.tw.ChannelConfig"))
		{
			return cls.CallStatic<string>("getBillingAppkey");
		}
	}
	
	public static int getIAPStyle(){
		return (int)IAPStyle.PAY_GooglePay;//GooglePay 方式,唯一的方式,不用做判断;
		
		if(AndroidConfig.isUCChannel()||isThirdSDKPlatform()){
			return (int)IAPStyle.PAY_THIRD;//IAPStyle.PAY_UC;
		}
		return (int)IAPStyle.PAY_CHANGYOU;
		
	}
#endif
}
