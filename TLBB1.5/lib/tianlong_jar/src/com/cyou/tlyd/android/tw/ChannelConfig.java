package com.cyou.tlyd.android.tw;

public class ChannelConfig {

	public static final String VERSION_SANXINGAPP = "android_sanxingapp";
	public static final String VERSION_SANXINGYOUXIQUAN = "android_sanxingyouxiquan";
	public static final String VERSION_NDUOSHICHANG = "android_nduoshichang";
	public static final String VERSION_SONY = "android_sony";
	public static final String VERSION_ZHONGXING = "android_zhongxing";
	public static final String VERSION_JINLI = "android_jinli";
	public static final String VERSION_JINSHAN = "android_jinshan";
	public static final String VERSION_SHIZIMAO = "android_shizimao";
	public static final String VERSION_MUZHIWAN = "android_muzhiwan";
	
	//2013-11-19增加两个渠道包 lihao_yd
	public static final String VERSION_BAORUAN = "android_baoruan";
	public static final String VERSION_HTC = "android_htc";
	//2013-11-26增加两个渠道包 lihao_yd
	public static final String VERSION_YOUTUDOU = "android_youtudou";
	public static final String VERSION_SOUGOU = "android_sougou";
	
	 
	// 同 IOS 的DeviceHelper.mm文件中定义 //
	public static final String VERSION_CHANGYOU = "android_changyou";
	public static final String VERSION_TB = "android_tb";
	public static final String VERSION_91 = "android_91";
	public static final String VERSION_UC = "android_uc";
	public static final String VERSION_APPSTORE = "android_appstore";
	public static final String VERSION_DUOKU = "android_duoku";
	public static final String VERSION_360 = "android_360";
	public static final String VERSION_XIAOMI = "android_xiaomi";
	public static final String VERSION_WANDOUJIA = "android_wandoujia";
	
	public static final String VERSION_WANDOUJIA_SDK = "android_wandoujia_sdk";
	public static final String VERSION_OPPO = "android_oppo";
	
	public static final String VERSION_MEITU = "android_meitu";
	public static final String VERSION_PAPA = "android_papa";
	public static final String VERSION_ZAKER = "android_zaker";
	public static final String VERSION_DUOMI = "android_duomi";
	public static final String VERSION_KUWO = "android_kuwo";
	public static final String VERSION_BAISI = "android_baisi"; 
	public static final String VERSION_ANZHI = "android_anzhi"; 
	// 不同渠道设置不同值 //
	private static String version = VERSION_WANDOUJIA;//以后用不到了
	
	// 同IOS的DeviceHelper.mm //
//	public static String getChannelID() {
//		return version;
//	}

	/**
	 * 获取billing分配的Appkey,渠道支付时的透传参数,组装豌豆荚渠道的订单号,
	 *   打jar包时注意区分
	 * @author lihao_yd
	 * @return
	 */
	public static String getBillingAppkey() {
		return "1379821827020";//billing分配给天龙项目的appkey，正式//
	}
	
	public static String getTestBillingAppkey() {
		return   "1376036742169";//billing分配给天龙项目的appkey，测试//
	}
	
	public static int getServerID() {
		return serverid;
	}
	public static int getGameID() {
		return gameid;
	}
	public static int getCPID() {
		return cpid;
	}
	public static String getAppKey() {
		return appKey;
	}
	public static String getApiKey() {
		return apiKey;
	}
	public static String getAppSecret() {
		return appsecret;
	}
	// ======================UC=======================
	// 错误信息级别，记录错误日志
	public static final int LOGLEVEL_ERROR = 0;
	// 警告信息级别，记录错误和警告日志
	public static final int LOGLEVEL_WARN = 1;
	// 调试信息级别，记录错误、警告和调试信息，为最详尽的日志级别
	public static final int LOGLEVEL_DEBUG = 2;

	// 竖屏
	public static final int ORIENTATION_PORTRAIT = 0;
	// 横屏
	public static final int ORIENTATION_LANDSCAPE = 1;

	// 联调环境参数//
	public static int cpid = 21732;
	public static int gameid = 529260;
	public static int serverid = 2371;
	public static String servername = "测试服";
	public static String apiKey = "314ef85982ed3f85f9a18ffe25f16ebe";
	// 测试
	//public static String appKey = "1376036742169";
	//public static String appsecret = "c9c62757e3b3484f9391f09cf6cf77c5";	
	// 正式
	public static String appKey = "1379821827020";
	public static String appsecret = "b3dbd27c191c4897bc6dd85d1fd1d8d6";

	// 正式环境参数//
	// public static int cpid = ;
	// public static int gameid = ;
	// public static int serverid = ;
	// public static string servername = ;

	public static boolean debugMode = true;
	public static int logLevel = LOGLEVEL_DEBUG;
	public static int orientation = ORIENTATION_PORTRAIT;
	public static boolean enablePayHistory = true;
	public static boolean enableLogout = true;
	
	//多酷测试环境
	public static final String appId_DkDemo = "102";//百度多酷SDK Demo使用appId
	public static final String appKey_DkDemo = "5t6y7u8i";//百度多酷SDK Demo使用appKey
	public static final String appSecret_DkDemo = "qwertyui"; // SDK demo appsecret
	//多酷正式环境
	public static final String appId_Dk = "1720";//百度多酷appId
	public static final String appKey_Dk = "90ac530e01cf00bb01910b9800ea1d0b";//百度多酷appKey
	public static final String appSecret_Dk = "0107fe312137095d6c9009e6c6f39749"; //百度多酷appsecret
}
