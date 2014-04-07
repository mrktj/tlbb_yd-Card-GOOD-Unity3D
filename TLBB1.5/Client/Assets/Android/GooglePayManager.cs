using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;

public class GooglePayManager : MonoBehaviour
{
#if UNITY_ANDROID
	public static bool isBillingSupported = false;
	public static string gppurchaseData ;
	public static string gpsignature ;
	public static string gpderID ;
	public static string gpproduceID ;
	private static List<GlobalSave.SOrder> lossLists = new List<GlobalSave.SOrder>();
	private static List<string> purchaseIds = new List<string>();
	public delegate void IABProductPurchasedFinish(int nStatus,  string strError);
	public static IABProductPurchasedFinish iabPurchaseFinish;
	
	void OnEnable()
	{
		// Listen to all events for illustration purposes
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		
		
		lossLists.Clear();
		purchaseIds.Clear();			
		//var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA1a3Da+i2nrPjYWPhFv065Qp+24xlP+hUcfkw/E1lDVCVq4z+Lu00tj3rR6sRUYTa5WsACxpFcFEtYJQWeRhNjdAXeyIZpbEOw0+5gyPRjQavLBQ6b1VHXXaAefAeVlCz0QfUv/DlW0zzFrlHN3O2z2Z5wWwzIvKzmJ/Euiev1KCvo6kMrawPXwAqWgur5c+fHBP58h2FKym2qbInSLN9tZdfoaTF7tDBDLl+Hrh8qwrLgwuJ9HzYRzD/oQ8Jh4VxgROBfMEh19U3/mLLc220J0lrqc5s+E2Mbl6V5kiq7lARyJGVVy29LR6Jm+Jbd9LbV+BwDOeOUdPTlE7POg1BzwIDAQAB";
	    var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAm7QgZALTxa3KEHECIUKA4OKP9jf4D3mEYNyy01ikPjQFWQsl7tjgyiKMExroijekPx+GmdDmRNZzcYC/+QMNhet0MVmaxJprHZWv2vOuwZNW+a877obJhvDeNEz+x1fn4p6yNMntII+ZXZOihO5Lq54nHeoXDK67b58YOQ0YS4sEnQvoDlUjypAWY7f46BVDgxTOP+3oznF1yWavk7+M3Z+uV3SvJ0yKAoRErZVc/k9iJXHydzVgMM2n5QBr1sOJzJY1ip1VThLuXOu9brl0Dpl3PFlknK5dOyeDvMPw/nYcm//qEH5trwGp8Sk0pgy0bUE1MjE/J3PddhTuMxXvlQIDAQAB";
		GoogleIAB.init(key);
		
		GoogleIAB.enableLogging(true);		
		Debug.Log("add google play in application billing");
	
	}


	void OnDisable()
	{
		Debug.Log("delete google play in application billing");
		// Remove all event handlers
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	public static void queryInvntoryFromIAB(string[] skus) 
	{
		if(!isBillingSupported)
		{
			Debug.Log("google billing is not supported");
			return;
		}
		if(purchaseIds.Count == 0){
			purchaseIds.AddRange(skus);
		}
		lossLists.Clear();
		Debug.Log("CyouPayManager queryInvntoryFromIAB");
		GoogleIAB.queryInventory(skus);
		
	}
	
	public static void purchaseGooglePlay(string productID)
	{
		if(!isBillingSupported)
		{
			Debug.Log("google billing is not supported");
			//BoxManager.showMessage("該手機不支持GooglePlay支付,請選擇其他支付","溫馨提示");
			return;
		}
		Debug.Log("GooglePlayManager purchaseGooglePla sku:"+productID);
		GoogleIAB.purchaseProduct( productID, "payload that gets stored and returned" );
	}	
	
	void billingSupportedEvent()
	{
		Debug.Log( "billingSupportedEvent" );
		isBillingSupported = true;
	}


	void billingNotSupportedEvent( string error )
	{
		Debug.Log( "billingNotSupportedEvent: " + error );
		isBillingSupported = false;
	}


	void queryInventorySucceededEvent( List<GooglePurchase> purchases, List<GoogleSkuInfo> skus )
	{
		Debug.Log( string.Format( "queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count ) );
		Prime31.Utils.logObject( purchases );
		Prime31.Utils.logObject( skus );
		foreach(GooglePurchase purchase in purchases){
			//消费剩余产品;这类产品在googleplay的状态是已购买未消费,而在本地状态是未购买,
			PurchaseInfo purchaseInfo = null;
			foreach(PurchaseInfo temp in Obj_MyselfPlayer.GetMe().purchaseInfoList){
				if(temp.productId == purchase.productId){
					purchaseInfo = temp;
					break;

				}
			}
			
			gpderID = purchase.orderId;
			gpproduceID = purchase.productId;
			gpsignature = purchase.signature;
			gppurchaseData = purchase.originalJson;
			
			if(purchaseInfo != null){
				GlobalSave.SOrder m_curOrder = new GlobalSave.SOrder();
				m_curOrder.goodId = purchaseInfo.goodsId;
				m_curOrder.productID = purchaseInfo.productId;
				m_curOrder.goodPrice = purchaseInfo.goodsPrice.ToString();
				m_curOrder.goodName = purchaseInfo.goodsName;
				if(purchaseInfo.goodsNumber == 0){
					m_curOrder.goodsNumber = 1;
				}else{
					m_curOrder.goodsNumber = purchaseInfo.goodsNumber;
				}
				m_curOrder.channel = AndroidConfig.GetChannelID();				
						
				m_curOrder.strOder = gpderID;
				m_curOrder.purchaseData = gppurchaseData;
				m_curOrder.signature = gpsignature;
				lossLists.Add(m_curOrder);
			}
			
		}
		if(lossLists.Count != 0){
			BeginCheckGooglePay(null);
		}
	}
	
	private void BeginCheckGooglePay(GameObject go){
		if(lossLists.Count != 0){
			GlobalSave.SOrder low = lossLists[0];
			
			//在本地没有订单的情况下;至少帮助玩家添加订单;然后进行自动验证;
			if(!OrderManager.Instance().HaveOrder(low.strOder)){
				BoxManager.showMessage("正在进行补单,请稍后","");
				OrderManager.Instance().AddOrder(low);
				PurchaseHelper.Instance().BeginCheckOrder(low,FinishCheck);
			}

			GoogleIAB.consumeProduct(low.productID);
			lossLists.RemoveAt(0);
		}else{
			GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
		}
	}
	
	public void FinishCheck(bool isSuccess,string result)
	{
		BoxManager.showMessage(result,"");
		UIEventListener.Get(BoxManager.getYesButton()).onClick += BeginCheckGooglePay;
	}	

	void queryInventoryFailedEvent( string error )
	{
		Debug.Log( "queryInventoryFailedEvent: " + error );
	}


	void purchaseCompleteAwaitingVerificationEvent( string purchaseData, string signature )
	{
		Debug.Log( "purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature );
	    gppurchaseData = purchaseData;
		gpsignature = signature;
	}

	void purchaseSucceededEvent( GooglePurchase purchase )
	{
		Debug.Log( "purchaseSucceededEvent: " + purchase );
		gpderID = purchase.orderId;
		gpproduceID = purchase.productId;
		Debug.Log("order id is :"+gpderID);
		iabPurchaseFinish(0,"purchase Success");
		GoogleIAB.consumeProduct(purchase.productId);		
	}


	void purchaseFailedEvent( string error )
	{
		Debug.Log( "purchaseFailedEvent: " + error );
		if(error.Equals("Unable to buy item (response: 7:Item Already Owned)")||
			error.Contains("purchase an item that has already been purchased"))
		{
			//GoogleIAB.purchaseProduct(cyproduceID);
			//检查是否存在未消费的产品,进行验证消费处理;
			if(purchaseIds.Count == 0){
				Debug.LogWarning("doesn't have purchases,add pid");
				List<string> pids = new List<string>();
				for(int i=0;i<Obj_MyselfPlayer.GetMe().purchaseInfoList.Count;i++){
					pids.Add(Obj_MyselfPlayer.GetMe().purchaseInfoList[i].productId);
				}
				Debug.Log("requery purchases");
				queryInvntoryFromIAB(pids.ToArray());
			}else{
				Debug.Log("requery purchases");
				queryInvntoryFromIAB(purchaseIds.ToArray());
			}
		}
		else
		   iabPurchaseFinish(-1,error);		
	}


	void consumePurchaseSucceededEvent( GooglePurchase purchase )
	{
		Debug.Log( "consumePurchaseSucceededEvent: " + purchase );
				
	}


	void consumePurchaseFailedEvent( string error )
	{
		Debug.Log( "consumePurchaseFailedEvent: " + error );
	}
#endif
}