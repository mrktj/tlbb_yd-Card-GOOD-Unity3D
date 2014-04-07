// UnityPlayerActivity and WebView integration

package com.cyou.tlyd.android.tw;


import java.util.List;

import android.app.Activity;
import android.app.ActivityManager;
import android.app.ActivityManager.RunningAppProcessInfo;
import android.content.Context;
import android.content.Intent;
import android.content.res.Configuration;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Toast;
 


import com.chartboost.sdk.Chartboost;
import com.chartboost.sdk.unity.CBPlugin;
//import com.cyou.mrd.analytics.AnalyticsUtil;
import com.facebook.Session;
import com.facebook.unity.FB;
import com.unity3d.player.UnityPlayer;

public class UnityPlayerActivity extends Activity {
	private boolean isAppForeground = true;
	private UnityPlayer mUnityPlayer;
    // webview
	private KDTLActionManager kdtlManager = null;
	private Payment payment;
	// UnityPlayer.init() should be called before attaching the view to a layout - it will load the native code.
	// UnityPlayer.quit() should be the last thing called - it will unload the native code.
	protected void onCreate (Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		//Log.i("---####################################--");
		//保持屏幕打开状态
		getWindow().setFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON,
				WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

		requestWindowFeature(Window.FEATURE_NO_TITLE);

		mUnityPlayer = new UnityPlayer(this);
		if (mUnityPlayer.getSettings ().getBoolean ("hide_status_bar", true))
			getWindow ().setFlags (WindowManager.LayoutParams.FLAG_FULLSCREEN,
			                       WindowManager.LayoutParams.FLAG_FULLSCREEN);

		int glesMode = mUnityPlayer.getSettings().getInt("gles_mode", 1);
		boolean trueColor8888 = false;
		mUnityPlayer.init(glesMode, trueColor8888);

		View playerView = mUnityPlayer.getView();
		setContentView(playerView);
		playerView.requestFocus();
		kdtlManager = new KDTLActionManager(this);
		kdtlManager.initSdk();
		//Log.i("---KDTLUnityPlayerActivity onCreate--");
		
		if(kdtlManager.getChannelID().equals(ChannelConfig.VERSION_UC)){
			payment = new Payment();
	        payment.init(this);
		}else if(kdtlManager.getChannelID().equals(ChannelConfig.VERSION_XIAOMI)){
			payment = new Payment();
	        payment.init(this);
		}else if(kdtlManager.getChannelID().equals(ChannelConfig.VERSION_WANDOUJIA_SDK)){
			payment = new Payment();
	        payment.init(this);
		}else
		{
			//Log.e("ChannelID not define in androidmanifest file");
			payment = new Payment();
			payment.init(this);
		}
		
		//zhouwei 
		CBPlugin.onCreate(this);
		
		/*
        // Instantiate MAT object with your advertiser ID and key
		mobileAppTracker = new MobileAppTracker(this, "14290","0bbd59fe5dfc24f228dcc5cd474c3101"); 
		        // Enable these options for debugging only
		//if (DEBUG) { mobileAppTracker.setAllowDuplicates(true); mobileAppTracker.setDebugMode(true); 
		//} 
        // Track install on app open
        mobileAppTracker.trackInstall();	
        */	
	}
	
	@Override
	public void onStart(){
		super.onStart();
		Chartboost.sharedChartboost().onStart(this);
		Chartboost.sharedChartboost().startSession();
	}
	
	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent data) {
	  super.onActivityResult(requestCode, resultCode, data);
	  Session.getActiveSession().onActivityResult(this, requestCode, resultCode, data);
	}

    @Override
    public void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        FB.SetIntent(intent);
    }
	
	protected void onDestroy ()
	{
		Chartboost.sharedChartboost().onDestroy(this);
		mUnityPlayer.quit();
		kdtlManager.onDestory();
		super.onDestroy();
	}

	// onPause()/onResume() must be sent to UnityPlayer to enable pause and resource recreation on resume.
	protected void onPause()
	{
		super.onPause();
		mUnityPlayer.pause();
	}
	@Override
	protected void onStop() {
		
		
		Chartboost.sharedChartboost().onStop(this);
		// TODO Auto-generated method stub
		super.onStop();
		if(!isAppOnForeground()){//app进入后台
			isAppForeground = false;
		}
	}
	protected void onResume()
	{
		super.onResume();
		mUnityPlayer.resume();
	}
	/**
	 * 判断App是否在前台运行
	 * @return
	 */
	public boolean isAppOnForeground() {
		ActivityManager activityManager = (ActivityManager) getApplicationContext()
				.getSystemService(Context.ACTIVITY_SERVICE);
		String packageName = getApplicationContext().getPackageName();
		List<RunningAppProcessInfo> appProcesses = activityManager.getRunningAppProcesses();
		if (appProcesses == null)
			return false;
		for (RunningAppProcessInfo appProcess : appProcesses) {
			if (appProcess.processName.equals(packageName)
					&& appProcess.importance == RunningAppProcessInfo.IMPORTANCE_FOREGROUND) {
				return true;
			}
		}
		return false;
	} 
	public void onConfigurationChanged(Configuration newConfig)
	{
		super.onConfigurationChanged(newConfig);
		mUnityPlayer.configurationChanged(newConfig);
	}
	public void onWindowFocusChanged(boolean hasFocus)
	{
		super.onWindowFocusChanged(hasFocus);
		mUnityPlayer.windowFocusChanged(hasFocus);
	}

	// Pass any keys not handled by (unfocused) views straight to UnityPlayer
	public boolean onKeyMultiple(int keyCode, int count, KeyEvent event)
	{
		return mUnityPlayer.onKeyMultiple(keyCode, count, event);
	}
	public boolean onKeyDown(int keyCode, KeyEvent event)
	{
		return mUnityPlayer.onKeyDown(keyCode, event);
	}
	public boolean onKeyUp(int keyCode, KeyEvent event)
	{
		return mUnityPlayer.onKeyUp(keyCode, event);
	}

	// webview
    public void updateWebView(final String lastRequestedUrl, final boolean loadRequest, final boolean visibility, final int leftMargin, final int topMargin, final int rightMargin, final int bottomMargin) {
    	kdtlManager.updateWebView(lastRequestedUrl, loadRequest, visibility, leftMargin, topMargin, rightMargin, bottomMargin);
    }

    public String pollWebViewMessage() {
        return kdtlManager.pollWebViewMessage();
    }
    
    // Transparent background
    public void makeTransparentWebViewBackground() {
    	kdtlManager.makeTransparentWebViewBackground();
    }
    public void showWebView(String url){
    	kdtlManager.showWebView(url);
    }
    
    public void hideWebView(){
    	kdtlManager.hideWebView();
    }
    public String getMacAddress() {
    	return kdtlManager.getMacAddress();
    }
    public String getNetworkType(){
    	return kdtlManager.getNetworkType();
    }
    
    public String getModel(){
    	return kdtlManager.getModel();
    }
    
    public String getOperator(){
    	return kdtlManager.getOperator();
    }
    public void initSdk(){
    	kdtlManager.initSdk();
    }
    public void startFeedBack(){
    	kdtlManager.startFeedBack();
    }
    public void startAnalytics(){
    }
    public void endAnalytics(){
    	// 转移到onDestroy
    	//kdtlManager.endAnalytics();
    }

	public void openPayWindow(String strPurchaseInfo) {
		kdtlManager.openPayWindow(strPurchaseInfo);
	}
	
	public String getArea(){
		return kdtlManager.getArea();
	}
	public String getCountry(){
		return kdtlManager.getCountry();
	}

	public String getDeviceSystem(){
		return kdtlManager.getDeviceSystem();
	}
	
	public void showExitPanel(){
		kdtlManager.showExitPanel();
	}
	
	public String getChannelID(){
		return kdtlManager.getChannelID();
	}
	
	public int getServerID(){
		return kdtlManager.getServerID();
	}
	public int getGameID(){
		return kdtlManager.getGameID();
	}
	public int getCPID(){
		return kdtlManager.getCPID();
	}
	public String getAppKey() {
		return kdtlManager.getAppKey();
	}
	public String getApiKey() {
		return kdtlManager.getApiKey();
	}
	public void copyAssetsFile(String assetsFile, String targetFile) {
		kdtlManager.copyAssetsFile(assetsFile, targetFile);
	}
	public void showUpdate(String version){
		kdtlManager.showUpdate(version);
	}
	public String getServerListUrl(){
		return kdtlManager.getServerListUrl();
	}
	
	public void removePayLog(String OID){
		kdtlManager.removePayLog(OID);
	}

	public void minusOnceOrder(String OID){
		kdtlManager.minusOnceOrder(OID);
	}

	public void savePayLog(String strPayLog){
		kdtlManager.savePayLog(strPayLog);
	}

	public void successOrder(String strOderID){
		kdtlManager.successOrder(strOderID);
	}


} 
