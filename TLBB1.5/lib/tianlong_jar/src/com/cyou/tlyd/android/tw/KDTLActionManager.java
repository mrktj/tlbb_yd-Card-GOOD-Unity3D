// UnityPlayerActivity and WebView integration

package com.cyou.tlyd.android.tw;

import java.io.BufferedReader;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.List;
import java.util.concurrent.SynchronousQueue;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.graphics.Color;
import android.location.Address;
import android.location.Criteria;
import android.location.Geocoder;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.view.Gravity;
import android.view.KeyEvent;
import android.view.View;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.ProgressBar;


import com.cyou.mrd.kdtl.web.KDTLWebView;
import com.cyou.tlyd.android.tw.R;

public class KDTLActionManager{

	private static final byte START_MAIN_ACTIVITY = 1;
	private static final byte INIT_ANALYTICS_SDK = 2;
	private static final byte END_ANALYTICS = 3;
	private static final byte SHOW_EXIT_DIALOG = 4;
	private static final byte SHOW_WEB_VIEW = 5;
	private static final byte HIDE_WEB_VIEW = 6;
	private static final byte UPDATE = 7;
	public Handler myEventHandler = null;
	//private AnalyticsUtil analyticsUtil = null;
    // JavaScript interface class for embedded WebView.
    private class JSInterface {
        public SynchronousQueue<String> mMessageQueue;

        JSInterface() {
            mMessageQueue = new SynchronousQueue<String>();
        }

        public void pushMessage(String message) {
            Log.d("WebView", message);
            try {
                mMessageQueue.put(message);
            } catch (java.lang.InterruptedException e) {
                Log.d("WebView", "Queueing error - " + e.getMessage());
            }
        }
    }

    private JSInterface mJSInterface;   // JavaScript interface (message receiver)
    private WebView mWebView;           // WebView object
    private ProgressBar mProgress;      // Progress bar
    private int mLeftMargin;            // Margins around the WebView
    private int mTopMargin;
    private int mRightMargin;
    private int mBottomMargin;
    private boolean mInitialLoad;       // Initial load flag
    private static Activity activity;

	public KDTLActionManager(Activity activity){
    	this.activity = activity;
		//analyticsUtil = new AnalyticsUtil(activity);
    	init();
        //整合UC渠道代码  lihao_yd  2013-11-26
		/*if (getChannelID().equals(ChannelConfig.VERSION_UC)) {
			UCGameSdk.initSDK(ChannelConfig.debugMode, ChannelConfig.logLevel,
					ChannelConfig.cpid, ChannelConfig.gameid,
					ChannelConfig.serverid, ChannelConfig.servername,
					ChannelConfig.enablePayHistory, ChannelConfig.enableLogout);
		}*/
//		TessarMobileSDK tessarMobileSDK = TessarMobileSDK.getInstance(activity, "天龙八部移动版", getChannelID(), "1.2.0.0", 5);
//		tessarMobileSDK.onCreate();
	}

    public void init() {
        // Create a WebView and make layout.
        mWebView = new WebView(activity);
        FrameLayout layout = new FrameLayout(activity);
        activity.addContentView(layout, new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT));
        layout.addView(mWebView, new FrameLayout.LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT, Gravity.NO_GRAVITY));
        mWebView.setFocusableInTouchMode(true);
        // Basic settings of WebView.
        WebSettings webSettings = mWebView.getSettings();
        webSettings.setSupportZoom(false);
        webSettings.setJavaScriptEnabled(true);
        //webSettings.setPluginsEnabled(true);
        //webSettings.setCacheMode(WebSettings.LOAD_NO_CACHE);
        // Set a dummy WebViewClient (which enables loading a new page in own WebView).
        mWebView.setWebViewClient(new WebViewClient(){
       		public boolean shouldOverrideUrlLoading(WebView view, String url) {
    			//这里实现的目标是在网页中继续点开一个新链接，还是停留在当前程序中
    			view.loadUrl(url);
    			return super.shouldOverrideUrlLoading(view, url);
    		}
        });
        // Add a progress bar.
        mProgress = new ProgressBar(activity, null, android.R.attr.progressBarStyleHorizontal);
        layout.addView(mProgress, new FrameLayout.LayoutParams(LayoutParams.FILL_PARENT, 5));
        mProgress.setMax(100);
        mProgress.setVisibility(View.GONE);
        mWebView.setWebChromeClient(new WebChromeClient() {
            public void onProgressChanged(WebView view, int progress) {
                if (progress < 100) {
                    mProgress.setVisibility(View.VISIBLE);
                    mProgress.setProgress(progress);
                } else {
                    mProgress.setVisibility(View.GONE);
                }
            }
        });
        // Create a JavaScript interface and bind the WebView to it.
        mJSInterface = new JSInterface();
        mWebView.addJavascriptInterface(mJSInterface, "UnityInterface");
        // Start in invisible state.
        mWebView.setVisibility(View.GONE);
        
        myEventHandler = new Handler() {
			public void handleMessage(Message msg) {
				switch (msg.what) {
				case START_MAIN_ACTIVITY:
					//Intent intent = new Intent(activity, MainActivity.class);
					//intent.putExtra(MainActivity.PURCHASE_INFO, msg.obj.toString());
					//activity.startActivity(intent);
					break;
				case INIT_ANALYTICS_SDK:
					//analyticsUtil.initAnalyticsSdk(isThirdPlatform());
					break;
				case END_ANALYTICS:
					//analyticsUtil.endAnalytics(isThirdPlatform());
					break;
				case SHOW_EXIT_DIALOG:
					showExitDialog();
					break;
				case SHOW_WEB_VIEW:
					Intent intentWeb = new Intent(activity, KDTLWebView.class);
					intentWeb.putExtra(KDTLWebView.URL, msg.obj.toString());
					activity.startActivity(intentWeb);
					break;
				case HIDE_WEB_VIEW:
					if (KDTLWebView.activity != null) {
						KDTLWebView.activity.finish();
						KDTLWebView.activity = null;
					}
					break;
				case UPDATE:
					String str = msg.getData().getString("version");
					if(str!=null&&str.length()>0){
						String version[] = str.split("&");
						if(version.length>0){
							if(version[1]!=null&&version[1].length()>0){
								final String versionUrl = version[1];
								if(versionUrl!=null&&versionUrl.length()>0){
									Log.e("---UPDATE", versionUrl);
									AlertDialog.Builder builder = new Builder(activity);
									builder.setTitle("更新提示");
									builder.setMessage("检测到新版本，请到官网更新程序包...");
									builder.setPositiveButton("确定",
											new DialogInterface.OnClickListener() {
												@Override
												public void onClick(DialogInterface dialog, int which) {
													dialog.dismiss();
													Uri uri = Uri.parse(versionUrl);
				                                    Intent intent = new Intent(Intent.ACTION_VIEW,uri);
				                                    activity.startActivity(intent);
				                                    activity.finish();
												}
											});
									builder.setNegativeButton("关闭",
											new DialogInterface.OnClickListener() {
												@Override
												public void onClick(DialogInterface dialog, int which) {
													dialog.dismiss();
												    activity.finish();
												}
											});
									builder.create().show();
								}
							}
						}
					}
					break;
				}
				super.handleMessage(msg);
			}
		};
    }

    public void onDestory(){
		endAnalytics();
    }
    
    public void updateWebView(final String lastRequestedUrl, final boolean loadRequest, final boolean visibility, final int leftMargin, final int topMargin, final int rightMargin, final int bottomMargin) {
        // Process load requests.
        if (lastRequestedUrl != null && (loadRequest || !mInitialLoad)) {
        	activity.runOnUiThread(new Runnable() {
                public void run() {
                    mWebView.loadUrl(lastRequestedUrl);
                }
            });
            mInitialLoad = true;
        }
        // Process changes in margin amounts.
        if (leftMargin != mLeftMargin || topMargin != mTopMargin || rightMargin != mRightMargin || bottomMargin != mBottomMargin) {
            mLeftMargin = leftMargin;
            mTopMargin = topMargin;
            mRightMargin = rightMargin;
            mBottomMargin = bottomMargin;
            activity.runOnUiThread(new Runnable() {
                public void run() {
                    // Apply a new layout to the WebView.
                    FrameLayout.LayoutParams params = new FrameLayout.LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.FILL_PARENT, Gravity.NO_GRAVITY);
                    params.setMargins(mLeftMargin, mTopMargin, mRightMargin, mBottomMargin);
                    mWebView.setLayoutParams(params);
                }
            });
        }
        // Process changes in visibility.
        if (visibility != (mWebView.getVisibility() == View.VISIBLE)) {
        	activity.runOnUiThread(new Runnable() {
                public void run() {
                    if (visibility) {
                        // Show and set focus.
                        mWebView.setVisibility(View.VISIBLE);
                        mWebView.requestFocus();
                    } else {
                        // Hide.
                        mWebView.setVisibility(View.GONE);
                    }
                }
            });
        }
    }
    public String pollWebViewMessage() {
        return mJSInterface.mMessageQueue.poll();
    }
    // Transparent background
    public void makeTransparentWebViewBackground() {
        mWebView.setBackgroundColor(Color.TRANSPARENT);
    }
    
    public void showWebView(String url){
		Message msg = new Message();
		msg.what = SHOW_WEB_VIEW;
		msg.obj = url;
		myEventHandler.sendMessage(msg);
    }
    
    public void hideWebView(){
		Message msg = new Message();
		msg.what = HIDE_WEB_VIEW;
		myEventHandler.sendMessage(msg);
    }
    
	public String getMacAddress() {
		WifiManager wifiManager = (WifiManager) activity.getSystemService(Context.WIFI_SERVICE);
		if(wifiManager == null){
			return "";
		}
		String macAddress = wifiManager.getConnectionInfo().getMacAddress();
		if(macAddress == null){
			try {
				// 记录了MAC地址的文件/sys/class/net/wlan0/address
				FileReader fr = new FileReader("/sys/class/net/wlan0/address");
				BufferedReader br = new BufferedReader(fr, 8192);
				while ((macAddress = br.readLine()) != null) {
					break;
				}
				br.close();
				fr.close();
			} catch (Throwable e) {
			}
		}

		return macAddress;
	}
	
	// 获取当前联网方式 
	public String getNetworkType(){
		// 获取当前联网方式
		ConnectivityManager cm = (ConnectivityManager) activity.getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo info = cm.getActiveNetworkInfo();
		String networkTypeStr = "2G";
		if (info != null) {
			int type = info.getType();
			if (type == ConnectivityManager.TYPE_WIFI) {
				networkTypeStr = "WIFI";
			} else if (type == ConnectivityManager.TYPE_MOBILE) {
				networkTypeStr = "3G";// 3G
			} else {
				networkTypeStr = "2G";// 2G
			}
		}
		return networkTypeStr;
	}
	
	public String getModel(){
		String facturer = null;
		try {
			facturer = Build.MANUFACTURER;
		} catch (Throwable e) {
			facturer = "";
		}
		
		return facturer + " " + android.os.Build.MODEL;
	}
	
	public String getOperator(){
		 TelephonyManager telManager = (TelephonyManager) activity.getSystemService(Context.TELEPHONY_SERVICE);
      /** 获取SIM卡的IMSI码
       * SIM卡唯一标识：IMSI 国际移动用户识别码（IMSI：International Mobile Subscriber Identification Number）是区别移动用户的标志，
       * 储存在SIM卡中，可用于区别移动用户的有效信息。IMSI由MCC、MNC、MSIN组成，其中MCC为移动国家号码，由3位数字组成，
       * 唯一地识别移动客户所属的国家，我国为460；MNC为网络id，由2位数字组成，
       * 用于识别移动客户所归属的移动网络，中国移动为00，中国联通为01,中国电信为03；MSIN为移动客户识别码，采用等长11位数字构成。
       * 唯一地识别国内GSM移动通信网中移动客户。所以要区分是移动还是联通，只需取得SIM卡中的MNC字段即可
       */
		String imsi = telManager.getSubscriberId();
		if (imsi != null) {
			if (imsi.startsWith("46000") || imsi.startsWith("46002")
					|| imsi.startsWith("46007")) {// 因为移动网络编号46000下的IMSI已经用完，所以虚拟了一个46002编号，134/159号段使用了此编号
				// 中国移动
				return "China Mobile";
			} else if (imsi.startsWith("46001")) {
				return "China Unicom";
			} else if (imsi.startsWith("46003")) {
				return "China Telecom";
			} else {
				return "Other";
			}
		}
		return "";
	}
	
	public void openPayWindow(String strPurchaseInfo)
	{
		Message msg = new Message();
		msg.what = START_MAIN_ACTIVITY;
		msg.obj = strPurchaseInfo;
		myEventHandler.sendMessage(msg);
	}
	
	public void initSdk()
	{
		Message msg = new Message();
		msg.what = INIT_ANALYTICS_SDK;
		myEventHandler.sendMessage(msg);
	}
	public void showUpdate(String version)
	{
		Message msg = new Message();
		msg.what = UPDATE;
		Bundle b = new Bundle();
		b.putString("version", version);
		msg.setData(b);
		myEventHandler.sendMessage(msg);
	}
	public void endAnalytics()
	{

		//analyticsUtil.endAnalytics(isThirdPlatform());
		
	}
	public void startFeedBack()
	{
		//analyticsUtil.startFeedBack();
	}
	public String getArea(){
		try{
			getCNBylocation();
		}catch(Throwable e){
		}
		if (cityName == null) {
			return "";
		}
		return cityName;
	}
	public String getCountry(){
		String country = null;
		try{
			country = activity.getResources().getConfiguration().locale.getCountry();
		}catch(Throwable e){
			country = "";
		}
		return country;
	}

	public String getDeviceSystem(){
		return "Android "+android.os.Build.VERSION.RELEASE;
	}
	
	public void showExitPanel()
	{
		Message msg = new Message();
		msg.what = SHOW_EXIT_DIALOG;
		myEventHandler.sendMessage(msg);
	}
	
	public String getChannelID(){
		ApplicationInfo appInfo;
		String channel = null;
		try {
			appInfo = activity.getPackageManager()
					.getApplicationInfo(activity.getPackageName(),
							PackageManager.GET_META_DATA);
			channel = appInfo.metaData.getString("ChannelID");
		} catch (NameNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return channel;
	}
	/**
	 * 是否为第三方平台（接入SDK）
	 *    
	 * @author lihao_yd
	 * @return
	 */
	public boolean isThirdPlatform(){
		String channelStr = getChannelID();
		if(channelStr.equals(ChannelConfig.VERSION_360)||channelStr.equals(ChannelConfig.VERSION_91)||channelStr.equals(ChannelConfig.VERSION_DUOKU)||
				channelStr.equals(ChannelConfig.VERSION_WANDOUJIA_SDK)||channelStr.equals(ChannelConfig.VERSION_XIAOMI)){
			return true;
		}
		return false;
	}
	
	public int getServerID(){
		return ChannelConfig.getServerID();
	}
	public int getGameID(){
		return ChannelConfig.getGameID();
	}
	public int getCPID(){
		return ChannelConfig.getCPID();
	}
	public String getAppKey() {
		return ChannelConfig.getAppKey();
	}
	public String getApiKey() {
		return ChannelConfig.getApiKey();
	}
	// ///////////////////////////////////////////////////////////
	// 退出提示框
	// ///////////////////////////////////////////////////////////
	/** 是否需要在退出确认框时暂停游戏 */
	private boolean isShow;
	public void showExitDialog() {
		if(isShow){
			return;
		}
		AlertDialog.Builder builder = new Builder(activity);
		builder.setTitle(activity.getString(R.string.notice));
		builder.setMessage(activity.getString(R.string.exit_sure));
		builder.setCancelable(false);
		builder.setPositiveButton(activity.getString(R.string.ok),
				new DialogInterface.OnClickListener() {
					@Override
					public void onClick(DialogInterface dialog, int which) {
						dialog.dismiss();
						Intent intent = new Intent(Intent.ACTION_MAIN);
			            intent.addCategory(Intent.CATEGORY_HOME);  
			            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);  
			            activity.startActivity(intent);
			            android.os.Process.killProcess(android.os.Process.myPid());
						isShow = false;
					}
				});
		/*
		builder.setNegativeButton(activity.getString(R.string.cancel),
				new DialogInterface.OnClickListener() {
					@Override
					public void onClick(DialogInterface dialog, int which) {
						dialog.dismiss();
						isShow = false;
					}
				});
				*/
		builder.setOnKeyListener(new DialogInterface.OnKeyListener() {
			@Override
			public boolean onKey(DialogInterface dialog, int keyCode, KeyEvent event) {
				if (keyCode == KeyEvent.KEYCODE_BACK && event.getRepeatCount() == 0) {
					//dialog.dismiss();
					//isShow = false;
					dialog.dismiss();
					Intent intent = new Intent(Intent.ACTION_MAIN);
		            intent.addCategory(Intent.CATEGORY_HOME);  
		            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);  
		            activity.startActivity(intent);
		            android.os.Process.killProcess(android.os.Process.myPid());
					isShow = false;					
				}
				return false;
			}
		});
		Log.i("", "----showExitDialog----");
		builder.create().show();
		isShow = true;
	}
	
	// *****得到地理位置*****
	public static String cityName = null;  //城市名    
    private static Geocoder geocoder = null;   //此对象能通过经纬度来获取相应的城市等信息
	public void getCNBylocation() {
		geocoder = new Geocoder(activity);
		// 用于获取Location对象，以及其他
		LocationManager locationManager;
		String serviceName = Context.LOCATION_SERVICE;
		// 实例化一个LocationManager对象
		locationManager = (LocationManager) activity.getSystemService(serviceName);
		// provider的类型
		String provider = LocationManager.NETWORK_PROVIDER;
		Criteria criteria = new Criteria();
		criteria.setAccuracy(Criteria.ACCURACY_LOW); // 低精度
		criteria.setAltitudeRequired(false); // 不要求海拔
		criteria.setBearingRequired(false); // 不要求方位
		criteria.setCostAllowed(false); // 不允许有话费
		criteria.setPowerRequirement(Criteria.POWER_LOW); // 低功耗

		// 通过最后一次的地理位置来获得Location对象
		Location location = locationManager.getLastKnownLocation(provider);

		String queryed_name = updateWithNewLocation(location);
		if ((queryed_name != null) && (0 != queryed_name.length())) {
			cityName = queryed_name;
		}
		// 只获取一次，不用刷新
//		/*
//		 * 第二个参数表示更新的周期，单位为毫秒；第三个参数的含义表示最小距离间隔，单位是米 设定每360秒进行一次自动定位
//		 */
//		locationManager.requestLocationUpdates(provider, 360000, 1000, locationListener);
//		// 移除监听器，在只有一个widget的时候，这个还是适用的
//		locationManager.removeUpdates(locationListener);
	}

	/**
	 * 方位改变时触发，进行调用
	 */
	private final static LocationListener locationListener = new LocationListener() {
		String tempCityName;

		public void onLocationChanged(Location location) {

			tempCityName = updateWithNewLocation(location);
			if ((tempCityName != null) && (tempCityName.length() != 0)) {

				cityName = tempCityName;
			}
		}

		public void onProviderDisabled(String provider) {
			tempCityName = updateWithNewLocation(null);
			if ((tempCityName != null) && (tempCityName.length() != 0)) {

				cityName = tempCityName;
			}
		}

		public void onProviderEnabled(String provider) {
		}

		public void onStatusChanged(String provider, int status, Bundle extras) {
		}
	};

	/**
	 * 更新location
	 * 
	 * @param location
	 * @return cityName
	 */
	private static String updateWithNewLocation(Location location) {
		String mcityName = "";
		double lat = 0;
		double lng = 0;
		List<Address> addList = null;
		if (location != null) {
			lat = location.getLatitude();
			lng = location.getLongitude();
		} else {

			System.out.println("无法获取地理信息");
		}

		try {
			addList = geocoder.getFromLocation(lat, lng, 1); // 解析经纬度
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		if (addList != null && addList.size() > 0) {
			for (int i = 0; i < addList.size(); i++) {
				Address add = addList.get(i);
				mcityName += add.getLocality();
			}
		}
		if (mcityName.length() != 0) {

			return mcityName.substring(0, (mcityName.length() - 1));
		} else {
			return mcityName;
		}
	}

	/**
	 * 通过经纬度获取地址信息的另一种方法
	 * 
	 * @param latitude
	 * @param longitude
	 * @return 城市名
	 */
	public static String GetAddr(String latitude, String longitude) {
		String addr = "";
		/*
		 * 也可以是http://maps.google.cn/maps/geo?output=csv&key=abcdef&q=%s,%s，
		 * 不过解析出来的是英文地址 密钥可以随便写一个key=abc
		 * output=csv,也可以是xml或json，不过使用csv返回的数据最简洁方便解析
		 */
		String url = String.format(
				"http://ditu.google.cn/maps/geo?output=csv&key=abcdef&q=%s,%s",
				latitude, longitude);
		URL myURL = null;
		URLConnection httpsConn = null;
		try {

			myURL = new URL(url);
		} catch (MalformedURLException e) {
			e.printStackTrace();
			return null;
		}

		try {

			httpsConn = (URLConnection) myURL.openConnection();

			if (httpsConn != null) {
				InputStreamReader insr = new InputStreamReader(
						httpsConn.getInputStream(), "UTF-8");
				BufferedReader br = new BufferedReader(insr);
				String data = null;
				if ((data = br.readLine()) != null) {
					String[] retList = data.split(",");
					if (retList.length > 2 && ("200".equals(retList[0]))) {
						addr = retList[2];
					} else {
						addr = "";
					}
				}
				insr.close();
			}
		} catch (IOException e) {

			e.printStackTrace();
			return null;
		}
		return addr;
	}
	
	/**
	 * resFile 原文件 相对于assets目录路径
	 * desFile 目标文件 可写目录中的文件
	 * @param assetsFile
	 * @param targetFile
	 */
	public void copyAssetsFile(String assetsFile, String targetFile){
		InputStream is = null;
		OutputStream os = null;
		try {
			is = activity.getAssets().open(assetsFile);
			os = new FileOutputStream(targetFile);
			byte[] buffer = new byte[1024];
			int length;
			while ((length = is.read(buffer)) > 0) {
				os.write(buffer, 0, length);
			}
			os.flush();
		} catch (Throwable e) {
			e.printStackTrace();
		} finally {
			try {
				is.close();
				os.close();
			} catch (Throwable e) {
				e.printStackTrace();
			}
		}
	}

    public static Activity getActivity() {
		return activity;
	}

	public static void setActivity(Activity activity) {
		KDTLActionManager.activity = activity;
	}
	
	public	String getPackName()
	{
		if(activity != null)
		{
			return activity.getPackageName();
		}
		return "error PackName";
	}
	public String getServerListUrl(){
		ApplicationInfo appInfo;
		String url = null;
		try {
			appInfo = activity.getPackageManager()
					.getApplicationInfo(activity.getPackageName(),
							PackageManager.GET_META_DATA);
			url = appInfo.metaData.getString("ServerListUrl");
		} catch (NameNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return url;
	}
	
	public void removePayLog(String OID)
	{
		PayLog.removeOrder(OID);
	}
	
	public void minusOnceOrder(String OID)
	{
		PayLog.minusOnceOrder(OID);
	}
	
	public void savePayLog(String strPayLog)
	{
		PayLog.saveOrder(strPayLog);
	}
	
	public void successOrder(String strOderID)
	{
		PayLog.successOrder(strOderID);
	}
	
} 
