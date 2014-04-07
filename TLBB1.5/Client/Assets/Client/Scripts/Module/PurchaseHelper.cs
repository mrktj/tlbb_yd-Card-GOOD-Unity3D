using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using card.net;
using GCGame.Table;
using Games.CharacterLogic;

public enum IAPStyle
{
    PAY_ONLINE = 1,
    PAY_IAPPPAY = 2,
    PAY_APPSTORE = 3,
	PAY_PP = 4,
#if UNITY_ANDROID
	PAY_CHANGYOU = 5,
	PAY_THIRD = 6,
	PAY_GooglePay = 7,
#endif
}

public enum PurchaseTip
{
    TIP_NOTSUPPORT = (int)MessageIdEnum.Msg119,
    TIP_BUYFAIL = (int)MessageIdEnum.Msg120,
    TIP_VARIFYFAIL = (int)MessageIdEnum.Msg121,
    TIP_BUYSUCCESS = (int)MessageIdEnum.Msg129,
    TIP_NOTREVEIVED = (int)MessageIdEnum.Msg125,
    TIP_SERVERERROR = (int)MessageIdEnum.Msg126,
    TIP_ORDEREXIST = (int)MessageIdEnum.Msg127,
    TIP_PARAMERROR = (int)MessageIdEnum.Msg128,
    TIP_UNKNOWERROR = (int)MessageIdEnum.Msg123,
    TIP_PAYCANCEL = (int)MessageIdEnum.Msg122,
    TIP_VARIFYORDERFAIL = (int)MessageIdEnum.Msg124,
};
class PurchaseHelper
{
    public delegate void OnPayFinish(bool bSuccess, string result);
    private static OnPayFinish m_delFinishPay = null;

    public delegate void OnCheckOrderFinish();
    private static OnCheckOrderFinish m_delFinishCheck;

    public static int PayVarifyResult;
    public static string PayVarifyResultOrderID;

    private float m_iapTimer = 0;
    private static PurchaseHelper m_instance = null;
    public static PurchaseHelper Instance()
    {
        if (m_instance == null) m_instance = new PurchaseHelper();
        return m_instance;
    }

    public void Update(float dt)
    {
        if (m_varifyTimer > 0)
        {
            m_varifyTimer -= dt;
            if (m_varifyTimer <= 0)
            {
                Debug.Log("pllog_m_varifyTimer="+m_varifyTimer);
                Debug.Log("pllog_m_varifyCount="+m_varifyCount);
                DoVarify();
            }
        }

        if (m_iapTimer > 0)
        {
            m_iapTimer -= dt;
            if (m_iapTimer <= 0)
            {
                //BoxManager.removeMessage();
            }
        }
    }
    private string GetDicTip(PurchaseTip tipID)
    {
        Tab_Popup msg = TableManager.GetPopupByID((int)tipID);
        if (msg == null)
        {
            Debug.LogError("Msg is Null ID: " + (int)tipID);
            return "";
        }
        return msg.Content;
        
    }

    private void FinishPay(bool bSuccess, PurchaseTip tipID)
    {
        OnPayFinish temp = m_delFinishPay;
        m_delFinishPay = null;
        if (null != temp) temp(bSuccess, GetDicTip(tipID));
    }

    private void FinishPay(bool bSuccess, string strTip)
    {
        OnPayFinish temp = m_delFinishPay;
        m_delFinishPay = null;
        if (null != temp) temp(bSuccess, strTip);
    }

    private void FinishCheckOrder()
    {
        OnCheckOrderFinish temp = m_delFinishCheck;
        m_delFinishCheck = null;
        if (null != temp) temp();
    }
	
	public bool BeginCheckOrder(OnPayFinish delPayFinish)
	{
		GlobalSave.SOrder curSaveOrder = CheckOrder();
		if(null == curSaveOrder)
		{
			return false;
		}

		m_delFinishPay = delPayFinish;
		m_curOrder.productID = curSaveOrder.productID;
        m_curOrder.goodId = curSaveOrder.goodId;
		m_curOrder.reciept = curSaveOrder.reciept;
        m_curOrder.strOder = curSaveOrder.strOder;
#if UNITY_ANDROID
		m_curOrder.goodPrice = curSaveOrder.goodPrice;
		m_curOrder.signature = curSaveOrder.signature;
		m_curOrder.purchaseData = curSaveOrder.purchaseData;		
#else
        m_curOrder.price = curSaveOrder.price;
#endif
        m_curOrder.userID = curSaveOrder.userID;
        m_curOrder.zoneID = curSaveOrder.zoneID;
		BeginVarify();
		
		
		return true;
	}

    public bool BeginCheckOrder(GlobalSave.SOrder curOrder, OnPayFinish delPayFinish)
    {
        if (null == curOrder)
        {
            return false;
        }

        m_delFinishPay = delPayFinish;
        m_curOrder.productID = curOrder.productID;
        m_curOrder.goodId = curOrder.goodId;
        m_curOrder.reciept = curOrder.reciept;
        m_curOrder.strOder = curOrder.strOder;
        ;
#if UNITY_ANDROID
		m_curOrder.goodPrice = curOrder.goodPrice;
		m_curOrder.signature = curOrder.signature;
		m_curOrder.purchaseData = curOrder.purchaseData;
#else
        m_curOrder.price = curOrder.price;
#endif
        m_curOrder.userID = curOrder.userID;
        m_curOrder.zoneID = curOrder.zoneID;
        BeginVarify();

        return true;
    }
    public GlobalSave.SOrder CheckOrder()
    {
        GlobalSave.SOrder leftOrder = null;
        if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_APPSTORE)
        {
            leftOrder = GlobalSave.GetAppStoreOrder();
            return leftOrder;
            
        }
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_IAPPPAY)
        {
            leftOrder = GlobalSave.GetIAppPayOrder();
           return leftOrder;
        }
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_PP)
        {
            leftOrder = GlobalSave.GetPPPayOrder();
            return leftOrder;
        }
#if UNITY_ANDROID
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_CHANGYOU)
        {
            GlobalSave.GetCyouStoreOrder();
        }
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_THIRD)
        {
            GlobalSave.GetCyouStoreOrder();
        }else if(DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_GooglePay){
			
			GlobalSave.GetCyouStoreOrder();
		}
#endif
        return null;
        
    }

    public void RemoveSaveOrder(string orderID)
    {
        GlobalSave.SOrder curSaveOrder = CheckOrder();
        if (null != curSaveOrder)
        {
            if (curSaveOrder.strOder == orderID)
            {
                if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_APPSTORE)
                {
                    GlobalSave.CleanAppStoreOrder();
#if UNITY_IPHONE
                    //结束本次交易 by HeGuangyu
                    //Debug.Log("RemoveSaveOrder(), finish Pending Transaction " + orderID);
                    //StoreKitBinding.finishPendingTransactions();
                    //end HeGuangyu
#endif
                }
                else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_IAPPPAY)
                {
                    GlobalSave.CleanIAppPayOrder();
                }
                else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_PP)
                {
                    GlobalSave.CleanPPPayOrder();
                }
#if UNITY_ANDROID
                else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_CHANGYOU)
                {
                    GlobalSave.CleanCyouStoreOrder();
                }
                else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_THIRD)
                {
                    GlobalSave.CleanCyouStoreOrder();
                }else if(DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_GooglePay){
					
					GlobalSave.CleanCyouStoreOrder();
				}
#endif
            }
        }
    }
    private GlobalSave.SOrder m_curOrder = new GlobalSave.SOrder();
#if UNITY_ANDROID
	private float makePurchaseTimeCount;
    public void MakePurchase(PurchaseInfo curPurchaseInfo, OnPayFinish delPayFinish)
    {
		if(Time.time - makePurchaseTimeCount < 2){
			return;
		}
		makePurchaseTimeCount = Time.time;
		
        Debug.Log("----try purchase");
        m_curOrder.productID = curPurchaseInfo.productId;
        m_curOrder.goodId = curPurchaseInfo.goodsId;
		m_curOrder.goodName = curPurchaseInfo.goodsName;
		m_curOrder.goodPrice = curPurchaseInfo.goodsPrice.ToString();// int to string
		Debug.Log("----lee-1-MakePurchase-m_curOrder.goodPrice=="+m_curOrder.goodPrice);
        //m_curOrder.userID = Obj_MyselfPlayer.GetMe().a.uid;
		//m_curOrder.zoneID = 0;

		if(curPurchaseInfo.goodsNumber == 0){
			m_curOrder.goodsNumber = 1;
		}else{
			m_curOrder.goodsNumber = curPurchaseInfo.goodsNumber;
		}
		m_curOrder.channel = AndroidConfig.GetChannelID();
        PayVarifyResult = -10;
        m_delFinishPay = delPayFinish;
		
		int nPrice = (int)(curPurchaseInfo.goodsPrice);
		/*
		double dPrice = Convert.ToDouble(m_curOrder.goodPrice);
		int nPrice = (int)(dPrice * 100);
		if(nPrice <= 0)
		{
			nPrice = 1;
		}
		
		m_curOrder.goodPrice = nPrice.ToString();
		Debug.Log("----lee-2-MakePurchase-m_curOrder.goodPrice=="+m_curOrder.goodPrice);
		*/
		
        if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_APPSTORE)
        {
            MakeAppStorePurchase();
        }
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_IAPPPAY)
        {
            m_curOrder.strOder = OrderManager.Instance().GenerateOrderString(m_curOrder.goodId);
            MakeIAppPurchase();
        }
		else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_CHANGYOU)
		{
			//2013-11-26 18:30 lihao_yd 修改官网包充值记录显示价格不对的bug
			//nPrice/=100;
			//m_curOrder.goodPrice = nPrice.ToString();    //改为只在充值记录显示的时候除100
			Debug.Log("----lee-3-MakePurchase-m_curOrder.goodPrice=="+m_curOrder.goodPrice);
            GlobalSave.SetCyouStoreLossGoodInfoTemp(m_curOrder.goodId, m_curOrder.productID, nPrice, Obj_MyselfPlayer.GetMe().accountID); // 如果掉单，这里先保存商品信息(临时信息)
			MakeCyouPurchase();
		}
		else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_THIRD)
		{
			nPrice/=100;
			m_curOrder.goodPrice = nPrice.ToString();
			Debug.Log("--proPrice = "+nPrice.ToString());
            GlobalSave.SetCyouStoreLossGoodInfoTemp(m_curOrder.goodId, m_curOrder.productID, nPrice, Obj_MyselfPlayer.GetMe().accountID); // 如果掉单，这里先保存商品信息(临时信息)
			MakeUCPurchase();
		}
		else if(DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_GooglePay){
			//不知道临时订单的机制,而且GooglePlay的补单方式不采用国内的方式;
			Debug.Log("--proPrice = "+m_curOrder.goodPrice.ToString());
			MakeGooglePurchase();
		}
        else
        {
            FinishPay(false, PurchaseTip.TIP_NOTSUPPORT);
        }	
    }	
#else
    public void MakePurchase(PurchaseInfo curProduct , OnPayFinish delPayFinish)
    {
		if(null == curProduct)
		{
			FinishPay(false, PurchaseTip.TIP_NOTSUPPORT);
			return;
		}
        else if (CheckOrder() != null)
        {
            FinishPay(false, "有未处理的订单，请返回刷新商店列表验证订单");
            return;
        }

        Debug.Log("try purchase id:" + curProduct.productId + "goodid: " + curProduct.goodsId.ToString());
        m_curOrder.productID = curProduct.productId;
        m_curOrder.goodId = curProduct.goodsId;
		m_curOrder.price = curProduct.goodsPrice.ToString();
		m_curOrder.productName = curProduct.goodsName;
        m_curOrder.userID = Obj_MyselfPlayer.GetMe().uid;
		m_curOrder.zoneID = 0;
        PayVarifyResult = -10;
        m_delFinishPay = delPayFinish;
        if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_APPSTORE)
        {
            MakeAppStorePurchase();
        }
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_IAPPPAY)
        {
           m_curOrder.strOder = OrderManager.Instance().GenerateOrderString(m_curOrder.goodId);
           OrderManager.Instance().AddOrder(m_curOrder);
           BoxManager.removeMessage();
           MakeIAppPurchase();
        }
		else if(DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_PP)
		{
            m_curOrder.strOder = OrderManager.Instance().GenerateOrderString(m_curOrder.goodId);
            OrderManager.Instance().AddOrder(m_curOrder);
            MakePPPurchase();
			
		}
        else
        {
            FinishPay(false, PurchaseTip.TIP_NOTSUPPORT);
        }
    }
#endif

   

    private void MakeAppStorePurchase()
    {
        Debug.Log("appstore purchase " + m_curOrder.productID);
        m_iapTimer = 15.0f;
        IAPHelper.Instance().Buy(m_curOrder.productID, AppStorePayResult);
    }

    void AppStorePayResult(bool bSuccess)
    {
        m_iapTimer = 0;
        if (bSuccess)
        {
            m_curOrder.reciept = IAPHelper.CurProduct.receipt;
            m_curOrder.productID = IAPHelper.CurProduct.pid;
            m_curOrder.strOder = IAPHelper.CurProduct.tid;
            GlobalSave.SetAppStoreOrder(m_curOrder.strOder, m_curOrder.goodId, m_curOrder.productID, m_curOrder.reciept);
            OrderManager.Instance().AddOrder(m_curOrder);
            Debug.Log("appstore pay success");
            BeginVarify();
           
        }
        else
        {
            Debug.Log("appstore pay fail");
            FinishPay(false, PurchaseTip.TIP_BUYFAIL);
        }
    }

    private void MakeIAppPurchase()
    {
        Debug.Log("iappbuy purchase " + m_curOrder.productID);
        IAppPayManager.purchaseFinish = IAppPayResult;
        DeviceHelper.IAppPayBuy(m_curOrder.strOder, int.Parse(m_curOrder.productID));
    }
	
	void MakePPPurchase()
	{
		PPHelper.purchaseFinish = PPPayResult;
        Debug.Log("iappbuy purchase " + m_curOrder.productID + m_curOrder.price + m_curOrder.strOder + m_curOrder.productName+m_curOrder.userID+m_curOrder.zoneID);
		DeviceHelper.PPBuy(int.Parse( m_curOrder.price), m_curOrder.strOder, m_curOrder.productName, m_curOrder.userID, m_curOrder.zoneID);
		BoxManager.removeMessage();
		
	}
	
	void PPPayResult(string result)
	{
		if(result == "0")
		{
			// success
            GlobalSave.SetPPPayOrder(m_curOrder.goodId, m_curOrder.productID, m_curOrder.strOder, int.Parse(m_curOrder.price), m_curOrder.userID, m_curOrder.zoneID);
			Debug.Log("varify PP pay");
            BeginVarify();
			
		}
		else
		{        
			FinishPay(false, PurchaseTip.TIP_BUYFAIL);
		}
	}
    void IAppPayResult(string result)
    {
        Debug.Log("iapppay success" + result);
        if (result == "0")
        {
            // 购买成功
            GlobalSave.SetIAppPayOrder(m_curOrder.strOder, m_curOrder.goodId, m_curOrder.productID);
            Debug.Log("varify iapp pay");
            BeginVarify();
        }
        else
        {
            if (result == "1")
            {
                FinishPay(false, PurchaseTip.TIP_VARIFYFAIL);
            }
            else if (result == "2")
            {
                FinishPay(false, PurchaseTip.TIP_PAYCANCEL);
            }
            else if (result == "3")
            {
                FinishPay(false, PurchaseTip.TIP_BUYFAIL);
            }
            else
            {
                FinishPay(false, PurchaseTip.TIP_UNKNOWERROR);
            }
        }
    }
#if UNITY_ANDROID
 	
	void MakeGooglePurchase(){
		GooglePayManager.iabPurchaseFinish = GooglePayResult;
		GooglePayManager.purchaseGooglePlay(m_curOrder.productID);
	}
	
	void GooglePayResult(int nStatus,string errorStr){
		Debug.Log("GooglePayResult nStatus = " + errorStr);
		if (0 == nStatus)
		{
			if (null != m_curOrder)
			{
				m_curOrder.strOder = GooglePayManager.gpderID;
				m_curOrder.signature = GooglePayManager.gpsignature;
				m_curOrder.purchaseData = GooglePayManager.gppurchaseData;
				OrderManager.Instance().AddOrder(m_curOrder);
				Debug.Log("google pay success");
				BeginVarify();
			}
		}
		else
		{
			//TODO
			FinishPay(false, PurchaseTip.TIP_BUYFAIL);
			Debug.Log("xlym:purchase failed with error: " + errorStr);
		}
	}
	
	void MakeCyouPurchase()
	{
		CYouPayManager.purchaseFinish = CYouAppPayResult;	
		CYouPayManager.purchaseItem(m_curOrder.productID, m_curOrder.goodName, m_curOrder.goodPrice, m_curOrder.goodsNumber);
	}
	void CYouAppPayResult(int nStatus, string strSpsn, string strError)
	{
		Debug.Log("CYouAppPayResult nStatus = " + nStatus);
		if (0 == nStatus)
		{
			if (null != m_curOrder)
			{
				m_curOrder.reciept = strSpsn;
				m_curOrder.strOder = strSpsn;
				BeginVarify();
			}
		}
		else
		{
			//TODO
			FinishPay(false, PurchaseTip.TIP_BUYFAIL);
			Debug.Log("xlym:purchase failed with error: " + strError);
		}
	}
	public void AddCyouOrder(string orderId){
		m_curOrder.strOder = orderId;
		OrderManager.Instance().AddOrder(m_curOrder);
	}
	public void MakeUCPurchase()
	{
		//CYouPayManager.purchaseFinish = UCAppPayResult;	
		CYouPayManager.purchaseUCItem(m_curOrder);
	}
	public void UCAppPayResult(int nStatus, string strError)
	{
		Debug.Log("CYouAppPayResult nStatus = " + nStatus);
		if (0 == nStatus)
		{
			if (null != m_curOrder)
			{
				m_curOrder.reciept = CYouPayManager.orderID;
				m_curOrder.strOder = CYouPayManager.orderID;
				BeginVarify();
			}
		}
		else
		{
			//TODO
			FinishPay(false, PurchaseTip.TIP_BUYFAIL);
			Debug.Log("xlym:purchase failed with error: " + strError);
		}
	}
#endif

    private int m_varifyCount = 0;
    private float m_varifyTimer = 0;
    private int CountMax = 5;
    void BeginVarify()
    {
        m_varifyCount = CountMax;
        DoVarify();
        
    }

    void DoVarify()
    {
        Debug.Log("DoVarify" + "varify left count:" + m_varifyCount.ToString());

        Debug.Log("goodid id:" + m_curOrder.goodId + "productID: " + m_curOrder.productID + "recipt : " +m_curOrder.reciept + "Order id: " + m_curOrder.strOder.ToString());

        if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_APPSTORE)
        {
            NetworkSender.Instance().VarifyAppstoreOrder(VarifyResult, m_curOrder.goodId, m_curOrder.productID,
                m_curOrder.reciept,
                m_curOrder.strOder);
        }
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_IAPPPAY)
        {
            NetworkSender.Instance().VarifyIAppPayOrder(VarifyResult, m_curOrder.goodId, m_curOrder.productID, m_curOrder.strOder);
        }
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_PP)
        {
            //NetworkSender.Instance().VarifyPPPayOrder(VarifyResult, m_curOrder.goodId, m_curOrder.productID, m_curOrder.strOder, m_curOrder.price, m_curOrder.userID, m_curOrder.zoneID);
        }
#if UNITY_ANDROID
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_CHANGYOU)
        {      
				NetworkSender.Instance().VerifyCYouPurchase(VarifyResult, m_curOrder.goodId, m_curOrder.productID, m_curOrder.strOder, m_curOrder.goodPrice, AndroidConfig.PLATFORM_CYOU, true);
        }
        else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_THIRD)
        {      
				NetworkSender.Instance().VerifyCYouPurchase(VarifyResult, m_curOrder.goodId, m_curOrder.productID, m_curOrder.strOder, m_curOrder.goodPrice, AndroidConfig.PLATFORM_UC, true);// 0是UC //
        }
		else if(DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_GooglePay){
			NetworkSender.Instance().GooglePayVerifyCYouPurchase(VarifyResult,m_curOrder.strOder,
				m_curOrder.signature,m_curOrder.purchaseData,true);
		}
#endif
        else
        {
            FinishPay(false, PurchaseTip.TIP_VARIFYORDERFAIL);
        }
    }
#if UNITY_ANDROID
    private void AndroidVarifyResult(bool bSuccess)
    {
        if(bSuccess == true)
        {
            /*
            0：  验证成功，且billingserver订单状态改为已加钱，客户端不需处理。 客户端删除订单
            1：  订单存在但未加钱，客户端删除订单
            -1： 订单验证失败,订单删除。
            -2： 平台错误下单暂时不成功，稍后重试。客户端删除订单
            -3:  重复订单,订单已加钱。 客户端删除订单
            -4:  参数不完整。客户端需要重新发起，并检查参数是否正确。订单重发                                                                                订单重发                                                              
            */
            Debug.Log("pllog_VarifyResult PayVarifyResult="+PayVarifyResult+" PayVarifyResultOrderID="+PayVarifyResultOrderID);
            if(PayVarifyResult == -4 || PayVarifyResult == -2)
            {
                //订单重发，什么处理都不用做，系统会每分钟轮训
                //订单发送次数-4，每个订单最多发送20次。
                Debug.Log("pllog_VarifyResult MinusOncePayLog");
                WebMediator.MinusOncePayLog(PayVarifyResultOrderID);

            }
            else if(PayVarifyResult == 0 || 
                    PayVarifyResult == 1 ||
                    PayVarifyResult == -1 ||
                    PayVarifyResult == -3
                    )
            {
                //删除订单
                WebMediator.RemovePayLog(PayVarifyResultOrderID);
            }
        }
    }
#endif
    private void VarifyResult(bool bSuccess)
    {
        Debug.Log("recharge result" +PayVarifyResult.ToString());
#if UNITY_ANDROID
        //AndroidVarifyResult(bSuccess);
		//GlobalSave.DelCyouStoreLossOrder(PayVarifyResultOrderID, Obj_MyselfPlayer.GetMe().accountID); // 交易成功，取消掉单的单号
#endif
		Debug.Log( "::VarifyResult " + PayVarifyResultOrderID );

        if (!bSuccess)
        {
            FinishPay(false, PurchaseTip.TIP_VARIFYORDERFAIL);
        }
        else
        {
            if (PayVarifyResult == 0)
            {
                RemoveSaveOrder(PayVarifyResultOrderID);
                OrderManager.Instance().SetOrderState(PayVarifyResultOrderID, OrderManager.OrderState.STATE_SUCCESS);
                if (Obj_MyselfPlayer.GetMe().purchaseDollar == 0)
                {
                    //DeviceHelper.SendTrackPurchase();
					
					
                    Obj_MyselfPlayer.GetMe().purchaseDollar = 1;
                }
				
                FinishPay(true, PurchaseTip.TIP_BUYSUCCESS);
				
				//支付追踪
#if UNITY_ANDROID				
				SDKManager.trackEvent("purchase",m_curOrder.goodPrice);
#elif UNITY_IPHONE
				SDKManager.trackEvent("purchase",m_curOrder.price );
#endif			
				
#if UNITY_ANDROID                
                //验证成功之后，就不要再继续发包验证了。
                m_varifyCount = 0;
                m_varifyTimer = 0;
#endif
            }
            else if (PayVarifyResult == -3)
            {
                RemoveSaveOrder(PayVarifyResultOrderID);
                OrderManager.Instance().SetOrderState(PayVarifyResultOrderID, OrderManager.OrderState.STATE_SUCCESS);
                FinishPay(false, PurchaseTip.TIP_ORDEREXIST);
            }
            else if (PayVarifyResult == 1)
            {
                RemoveSaveOrder(PayVarifyResultOrderID);
                OrderManager.Instance().SetOrderState(PayVarifyResultOrderID, OrderManager.OrderState.STATE_SUCCESS);
                FinishPay(false, PurchaseTip.TIP_NOTREVEIVED);
            }
            else
            {
                if (m_varifyCount > 0)
                {
                    Debug.Log("pllog_wait varify again");
                    m_varifyCount--;
                    m_varifyTimer = (CountMax - m_varifyCount);
                    BoxManager.showMessageByID((int)MessageIdEnum.Msg104);
                }
               
                else if (PayVarifyResult == -1)
                {
                    RemoveSaveOrder(PayVarifyResultOrderID);
                    OrderManager.Instance().SetOrderState(PayVarifyResultOrderID, OrderManager.OrderState.STATE_FAIL);
                    FinishPay(false, PurchaseTip.TIP_VARIFYORDERFAIL);
                }
                else if (PayVarifyResult == -2)
                {
                    OrderManager.Instance().SetOrderState(PayVarifyResultOrderID, OrderManager.OrderState.STATE_UNKNOWN);
                    FinishPay(false, PurchaseTip.TIP_SERVERERROR);
                }
                else if (PayVarifyResult == -4)
                {
                    RemoveSaveOrder(m_curOrder.strOder);
                    OrderManager.Instance().SetOrderState(PayVarifyResultOrderID, OrderManager.OrderState.STATE_UNKNOWN);
                    FinishPay(false, PurchaseTip.TIP_PARAMERROR);
                }
                else
                {
                    OrderManager.Instance().SetOrderState(PayVarifyResultOrderID, OrderManager.OrderState.STATE_UNKNOWN);
                    FinishPay(false, PurchaseTip.TIP_UNKNOWERROR);
                }
            }
        }
    }

#if UNITY_ANDROID
    public void FakeFinishPay(bool bSuccess, string result)
    { // 假的支付完成
        BoxManager.removeMessage();
        GameObject mainLogic = GameObject.Find("/MainUILogic");
        if(mainLogic != null)
        {
            MainUILogic muil = mainLogic.GetComponent<MainUILogic>();
            if(muil != null)
            {
                muil.RefreshRecordList();
            }
        }
        if(bSuccess == true)
        {
            Debug.Log("pllog_UpdateUserInfo dollar="+Games.CharacterLogic.Obj_MyselfPlayer.GetMe().dollar);
            GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
            BoxManager.showMessage(result, ""); //WML MARK
        }
    }
    public void VarifyTheLossOrder( string gid, string pid, string gprice, string order )
    { 
		int platform = 0;
		// UC???? lihao_yd
		if(AndroidConfig.isUCChannel()){
			platform = AndroidConfig.PLATFORM_UC;
		}else{
			platform = AndroidConfig.PLATFORM_CYOU;
		}
		m_delFinishPay = FakeFinishPay;
    	m_curOrder.strOder = order;
        NetworkSender.Instance().VerifyCYouPurchase(VarifyResult,
            Convert.ToInt32(gid),
            pid,
            order,
            gprice,
            platform, 
            false);
    }

    public void VarifyJavaOrder( string gid, string pid, string gprice, string order )
    { 
        if(string.IsNullOrEmpty(gid))
        {
            gid = "0";
        }
        m_delFinishPay = FakeFinishPay;
        NetworkSender.Instance().VerifyCYouPurchase(VarifyResult,
            Convert.ToInt32(gid),
            pid,
            order,
            gprice,
            0,
            false);
    }

#endif
}

