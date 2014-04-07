package com.cyou.tlyd.android.tw;

import org.json.JSONArray;
import org.json.JSONObject;
import android.content.Context;
import android.content.SharedPreferences;
import android.text.TextUtils;
import android.util.Log;
/*

 * 订单号存档
 * 
 * 
 * */
public class PayLog {

	public String status	=	"";
	public String accountID	=	"";
	public String orderId	=	"";
	public String gid		=	"";
	public String pid		=	"";
	public String goodsName	=	"";
	public String goodsPrice=	"";
	public int 	  count		=	20;
	
	protected static final String TAG    	= "Payment";
	protected static final String PAYING    = "Paying";
	protected static final String PAID    	= "Paid";

	protected static final String KEY_STATUS  	= "status";
	protected static final String KEY_ACC    	= "accountID";
	protected static final String KEY_OID    	= "orderId";
	protected static final String KEY_GID    	= "gid";
	protected static final String KEY_PID    	= "pid";
	protected static final String KEY_GNAME    	= "goodsName";
	protected static final String KEY_GPRICE   	= "goodsPrice";
	protected static final String KEY_COUNT   	= "count";

	public PayLog(JSONObject jo)
	{
		try 
		{
			status			=	jo.getString(KEY_STATUS);
			accountID		=	jo.getString(KEY_ACC);
			orderId			=	jo.getString(KEY_OID);
			gid				=	jo.getString(KEY_GID);
			pid				=	jo.getString(KEY_PID);
			goodsName		=	jo.getString(KEY_GNAME);
			goodsPrice		=	jo.getString(KEY_GPRICE);
			count			=	jo.getInt(KEY_COUNT);
		}
		catch (Throwable t) 
		{
			t.printStackTrace();
			Log.e(TAG, t.getMessage(), t);
		}
	}
	
	public PayLog(String _status, String _accountID, String _orderId, String _gId, String _pId, String _goodsName, String _goodsPrice, int _count)
	{
		status			=	_status;
		accountID		=	_accountID;
		orderId			=	_orderId;
		gid				=	_gId;
		pid				=	_pId;
		goodsName		=	_goodsName;
		goodsPrice		=	_goodsPrice;
		count			=	_count;
	}
	
	public String Log()
	{
		return "status:"+status
				+" accountID:" + accountID
				+" orderId:" + orderId
				+" gid:" + gid
				+" pid:" + pid
				+" goodsName:" + goodsName
				+" goodsPrice:" + goodsPrice
				+" count:" + count
				;
	}

	public static String LogList(PayLog[] plist)
	{
		if(plist == null)
		{
			return "";
		}
		String finalstring = "";
		for(int i = 0; i<plist.length; i++)
		{
			if(plist[i] != null)
			{
				finalstring += plist[i].Log();
			}
		}
		
		return finalstring;
	}

	public JSONObject toJasonObj()
	{
		try
		{
			JSONObject jo = new JSONObject();
			if(jo != null)
			{
				jo.put(KEY_STATUS, status);
				jo.put(KEY_ACC, accountID);
				jo.put(KEY_OID, orderId);
				jo.put(KEY_GID, gid);
				jo.put(KEY_PID, pid);
				jo.put(KEY_GNAME, goodsName);
				jo.put(KEY_GPRICE, goodsPrice);
				jo.put(KEY_COUNT, count);
			}
			return jo;
		}
		catch (Throwable t) 
		{
			t.printStackTrace();
			Log.e(TAG, t.getMessage(), t);
		}
		return null;
	}

	public static PayLog[] ParseStrList(String logstr)
	{
		if(logstr.equals(""))
		{
			return null;
		}
		
		try
		{
	        JSONArray ja = new JSONArray(logstr);
	        int jlength = ja.length(); 

	        if(jlength > 0)
	        {
				PayLog plist[] = new PayLog[jlength];
				for(int i = 0; i<jlength; i++)
				{
					JSONObject jo 	= ja.getJSONObject(i);

					if(jo != null)
					{
						plist[i]	=	new PayLog(jo);
					}
				}
				return plist;
	        }
		}
        catch (Throwable t) 
		{
			t.printStackTrace();
			Log.e(TAG, t.getMessage(), t);
		}
		return null;
	}

	public static JSONArray toJasonArray(PayLog[] plist)
	{
		if(plist != null && plist.length>0)
		{
	        JSONArray ja = new JSONArray();
	        if(ja != null)
	        {
				for(int i = 0; i<plist.length; i++)
				{
					if(plist[i] != null)
					{
						JSONObject jo = plist[i].toJasonObj();
						if(jo != null)
						{
							ja.put(jo);
						}
					}
				}
	        }
	        return ja;
		}
		return null;
	}

	
	public static String toStrList(PayLog[] plist)
	{
		if(plist == null)
		{
			return "";
		}
		JSONArray ja = PayLog.toJasonArray(plist);
		if(ja != null)
		{
			return ja.toString();
		}
		return "";
	}
	
	/**
	 * 保存订单信息到硬盘
	 * @return
	 */
	public static void saveOrder(String strPayInfo) {
		try 
		{
			if (!TextUtils.isEmpty(strPayInfo)) 
			{
				SharedPreferences packagePrefs = KDTLActionManager.getActivity()
						.getSharedPreferences(KDTLActionManager.getActivity().getPackageName(),	Context.MODE_PRIVATE);
				if(packagePrefs != null)
				{
					JSONObject paramJsonObj	= new JSONObject(strPayInfo);
					if(paramJsonObj != null)
					{
						PayLog pl 			= 	new PayLog(	PAYING,
															paramJsonObj.getString("ACC"),
															paramJsonObj.getString("OID"),
															paramJsonObj.getString("GID"),
															paramJsonObj.getString("PID"),
															paramJsonObj.getString("PNAME"),
															paramJsonObj.getString("PPRICE"),
															20
															);
						if(pl != null)
						{
							JSONArray 	ja 	= null;
							JSONObject 	jo 	= pl.toJasonObj();
							if(jo != null)
							{
								Log.v(TAG,"pllog_new:"+ jo.toString());	
								String OrderTable 	= 	packagePrefs.getString("OrderTable", "");
								PayLog[] plist 		= PayLog.ParseStrList(OrderTable);
								Log.v(TAG,"pllog_save_before:"+ PayLog.LogList(plist));	
								if(plist != null)
								{
									for(int i = 0; i<plist.length; i++)
									{
										if(plist[i] != null)
										{
											if(plist[i].orderId.equals(pl.orderId) == true)
											{
												//订单号重复！！！严重错误
												Log.e(TAG,"pllog_orderid_replicate!!!!:"+ plist[i].orderId);
												return;
											}
										}
									}
										
									ja 	= PayLog.toJasonArray(plist);
									
									if(ja != null)
									{
										ja.put(jo);
									}
								}
								else
								{
									ja = new JSONArray();
									ja.put(jo);
								}
								Log.v(TAG,"pllog_save_after:"+ ja.toString());	
								
								String newOrder 	=  	ja.toString();
								Log.v(TAG,"pllog_order:"+ newOrder);			
								SharedPreferences.Editor ed = packagePrefs.edit();
								ed.putString("OrderTable", newOrder);
								ed.commit();
							}
						}
					}					
				}
			}
		}
		catch (Throwable t) 
		{
			t.printStackTrace();
			Log.e(TAG, t.getMessage(), t);
			Log.v(TAG,"pllog_Exception!!!!saveOrder");

		}
	}
	
	
	/**
	 * 支付成功，标示订单已完成
	 * @return
	 */
	public static String successOrder(String strorderid) {
		try 
		{
			if (!TextUtils.isEmpty(strorderid)) 
			{
				SharedPreferences packagePrefs = KDTLActionManager.getActivity()
						.getSharedPreferences(KDTLActionManager.getActivity().getPackageName(),	Context.MODE_PRIVATE);
				
				if(packagePrefs != null)
				{
					Log.v(TAG,"pllog_trytosucsspaid:"+ strorderid);	
					JSONObject jret = null;

					String OrderTable = packagePrefs.getString("OrderTable", "");
					PayLog[] plist = PayLog.ParseStrList(OrderTable);
					if(plist != null)
					{
						for(int i = 0; i<plist.length; i++)
						{
							if(plist[i] != null)
							{
								if(plist[i].orderId.equals(strorderid) == true)
								{
									plist[i].status = PAID;
									jret = plist[i].toJasonObj();
									Log.v(TAG,"pllog_sucsspaid:"+ plist[i].toJasonObj().toString());	
								}
							}
						}
						String finalString = PayLog.toStrList(plist);
						SharedPreferences.Editor ed = packagePrefs.edit();
						ed.putString("OrderTable", finalString);
						ed.commit();
					}
					
					if(jret != null)
					{
						return jret.toString();
					}
					else
					{
						return "";
					}
				}
			}
		}
		catch (Throwable t) 
		{
			t.printStackTrace();
			Log.e(TAG, t.getMessage(), t);
			Log.v(TAG,"pllog_Exception!!!!successOrder");
		}
		return "";
	}
	
	/**
	 * 从硬盘删除订单信息
	 * @return
	 */
	public static void removeOrder(String strorderid) {
		try 
		{
			if (!TextUtils.isEmpty(strorderid)) 
			{
				SharedPreferences packagePrefs = KDTLActionManager.getActivity()
						.getSharedPreferences(KDTLActionManager.getActivity().getPackageName(),	Context.MODE_PRIVATE);
				
				if(packagePrefs != null)
				{
					Log.v(TAG,"pllog_trytoremoveOrder:"+ strorderid);	

					String OrderTable = packagePrefs.getString("OrderTable", "");
					PayLog[] plist = PayLog.ParseStrList(OrderTable);
					if(plist != null)
					{
						for(int i = 0; i<plist.length; i++)
						{
							if(plist[i] != null)
							{
								if(plist[i].orderId.equals(strorderid) == true)
								{
									Log.v(TAG,"pllog_sucssRemove:"+ plist[i].toJasonObj().toString());	
									plist[i] = null;
								}
							}
						}
						String finalString = PayLog.toStrList(plist);
						SharedPreferences.Editor ed = packagePrefs.edit();
						ed.putString("OrderTable", finalString);
						ed.commit();
					}
				}
			}
		}
		catch (Throwable t) 
		{
			t.printStackTrace();
			Log.e(TAG, t.getMessage(), t);
			Log.v(TAG,"pllog_Exception!!!!removeOrder");
		}
	}

	
	/**
	 * 订单发送成功一次
	 * @return
	 */
	public static void minusOnceOrder(String strorderid) {
		try 
		{
			if (!TextUtils.isEmpty(strorderid)) 
			{
				SharedPreferences packagePrefs = KDTLActionManager.getActivity()
						.getSharedPreferences(KDTLActionManager.getActivity().getPackageName(),	Context.MODE_PRIVATE);
				
				if(packagePrefs != null)
				{
					Log.v(TAG,"pllog_trytominusOrder:"+ strorderid);	

					String OrderTable = packagePrefs.getString("OrderTable", "");
					PayLog[] plist = PayLog.ParseStrList(OrderTable);
					if(plist != null)
					{
						for(int i = 0; i<plist.length; i++)
						{
							if(plist[i] != null)
							{
								if(plist[i].orderId.equals(strorderid) == true)
								{
									plist[i].count--;
									if(plist[i].count == 0)
									{
										//发送次数够多了，删掉自己
										Log.v(TAG,"pllog_minusOnceRemove:"+ plist[i].toJasonObj().toString());	
										plist[i] = null;
									}
									Log.v(TAG,"pllog_sucssminusOnceOrder:"+ plist[i].toJasonObj().toString());	
								}
							}
						}
						String finalString = PayLog.toStrList(plist);
						SharedPreferences.Editor ed = packagePrefs.edit();
						ed.putString("OrderTable", finalString);
						ed.commit();
					}
				}
			}
		}
		catch (Throwable t) 
		{
			t.printStackTrace();
			Log.e(TAG, t.getMessage(), t);
			Log.v(TAG,"pllog_Exception!!!!minusOnceOrder");
		}
	}

}
