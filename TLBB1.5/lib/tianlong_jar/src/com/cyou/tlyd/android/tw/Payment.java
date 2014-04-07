package com.cyou.tlyd.android.tw;

import it.sauronsoftware.base64.Base64;

import java.io.File;

import org.apache.http.HttpHost;
import org.json.JSONObject;

import com.unity3d.player.UnityPlayer;

import android.app.Activity;
import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Bundle;
import android.os.Environment;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.widget.Toast;

public class Payment {
	public    static final int MSG_FUNC_CALL       = 301;

	public    static final int MSG_FUNC_DOLOGIN    = 101;
	protected static final String TAG              = "Payment";
	public    static final String mParamKey        = "PARAMKEY";
	public    static final String mFunctionKey     = "FUNCTIONKEY";
	protected static Payment mThis                 = null;
	protected String  mMac                         = null;
	protected Handler mHandler                     = null;

	protected static Activity mActivity    = null;

	public void onFuncCall(String func, String paramJson) {
		//override for call your function
		Log.d(TAG, "onFuncCall not implement!!!");
		
		if(func.equalsIgnoreCase("doOrder"))
		{
			PayLog.saveOrder(paramJson);
		}
		
		//增加显示和隐藏悬浮窗的调用 若渠道需要实现 在子类实现相应方法
		/*if(func.equalsIgnoreCase("doLogin")){//弹出登录
		doSdkLogin(isLandScape,true);
	}else if(func.equalsIgnoreCase("doLogout")){//注销
		doSdkSwitchAccount(isLandScape,true);
	}else if(func.equalsIgnoreCase("onLoginVerify")){//登录回调
		onLoginVerify(paramJson);
	}else if(func.equalsIgnoreCase("doOrder")){发起支付
		doSdkPay(isLandScape, true,paramJson);
	}else if(func.equalsIgnoreCase("refreshToken")){//360专有方法 
		refreshToken(paramJson);
	}else if(func.equalsIgnoreCase("showFloatButton")){//显示悬浮窗
		showFloatButton(paramJson);
	}else if(func.equalsIgnoreCase("hideFloatButton")){//隐藏悬浮窗
		hideFloatButton(paramJson);
	}*/
	}
	public void init(Activity activity){
		mActivity = activity;
		mThis     = this;
		mHandler= new Handler(){
			public void handleMessage(Message msg) {
				try {
					switch(msg.what){
					case MSG_FUNC_CALL:
						Bundle bundle = msg.getData();
						String func = bundle.getString(mFunctionKey);
						String param= bundle.getString(mParamKey);
						onFuncCall(func, param);
						break;
					}
				} catch (Throwable t) {
					t.printStackTrace();
				}
			}
		};
	}
	//回调到c# 固定到方法OnCallResult里面解析json处理
	public static void unity3dSendMessage(String funcAndJson) {
		Log.d(TAG, "----send message to Unity3D, message data=" + funcAndJson.toString() );
		UnityPlayer.UnitySendMessage("Camera", "OnCallResult", funcAndJson);
	}
	//因为UnitySendMessage只接收两个参数 所以把方法名组装到json里面
	public static void unity3dCallback(String func,String json) {
		try {
			JSONObject jobj = new JSONObject();
			jobj.put("func", func);
			jobj.put("json", json);

			unity3dSendMessage(jobj.toString());
		} catch (Throwable e) {
			e.printStackTrace();
			Log.e(TAG, e.getMessage(), e);
		}
	}
	//请求调用
	private void doCall(final String func, String paramJson){
		Message msg = new Message();
		msg.what = MSG_FUNC_CALL;
		Bundle bundle = new Bundle();
		bundle.putString(mParamKey,    paramJson);
		bundle.putString(mFunctionKey, func);
		msg.setData(bundle);
		mHandler.sendMessage(msg);
	}
	//c#调用到java
	public static void jniCall(String func, String paramJson){
		Log.d(TAG, "----jniCall:"+func);
		mThis.doCall(func, paramJson);
	};
	//------------------------------------------------------------------------

	//根据定单信息生成定单票据
	public String makeTransReceipt(String transId, String productId, long productNum, long productPrice, String payWay){
		String receipt1 = "{'orderId':'"+transId+"', 'groupId':'0','goodsNumber':"+productNum+",'goodsRegisterId':'"+productId+"','goodsPrice':"+productPrice+",'uid':"+getMacAddress()+",'payWay':"+payWay+"}"; 
		return Base64.encode(receipt1);
	}

	public String getMacAddress(){
		if(null != mMac && mMac.length()>0)return mMac;
		WifiManager wifi = (WifiManager)mActivity.getSystemService(Context.WIFI_SERVICE);
		WifiInfo info = wifi.getConnectionInfo();
		return mMac=info.getMacAddress();
	}

	public static String doGetOrderSavePath(){
		String path = "";
		try{
			//判断sd卡是否存在
			if (Environment.getExternalStorageState().equals(Environment.MEDIA_MOUNTED)) {
				path = Environment.getExternalStorageDirectory().getAbsolutePath();
			}
		}catch(Throwable t){}

		/*屏蔽，防止一切丢单的可能
	    if(null == path){
	        //内置卡的app目录会随着应用御载而删除
	        File file = mActivity.getFilesDir();
	        path = file.getPath();
	    }*/

		if (path.length() > 0) {
			Log.d(TAG, "get save path:"+path);
			path = path+"/cyou.mc/";
			File file = new File(path);
			if(!file.exists()){
				file.mkdirs();
			}

			path = path+"order360/";
			file = new File(path);
			if(!file.exists()){
				file.mkdirs();
			}
		}
		return path;
	}
	//-----------------------------------------------------------------------------------------------
	//网络检查
	/**
	 * 获取Http代理host。使用手机2G/3G网络时需要用到。
	 * 
	 * @param ctx
	 * @return
	 */
	public static HttpHost getHttpProxy(Context ctx) {
		ConnectivityManager connMgr = (ConnectivityManager) ctx
				.getSystemService("connectivity");
		NetworkInfo netInfo = connMgr.getActiveNetworkInfo();
		if ((netInfo != null) && (netInfo.isAvailable())
				&& (netInfo.getType() == 0)) {
			String str = android.net.Proxy.getDefaultHost();
			int i = android.net.Proxy.getDefaultPort();
			if (str != null) {
				return new HttpHost(str, i);
			}
		}
		return null;
	}

	/**
	 * 检测是否有网络
	 * 
	 * @param act
	 * @return
	 */
	public static boolean isNetworkAvailable(Context context) {
		ConnectivityManager cm = (ConnectivityManager) context
				.getSystemService(Context.CONNECTIVITY_SERVICE);
		NetworkInfo info = cm.getActiveNetworkInfo();
		if (info != null && info.getState() == NetworkInfo.State.CONNECTED)
			return true;
		return false;
	}
}


