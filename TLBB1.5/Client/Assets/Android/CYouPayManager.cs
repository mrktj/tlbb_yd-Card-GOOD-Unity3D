using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using System;
using System.Collections.Generic; 
using System.Text; 

public class CYouPayManager : MonoBehaviour
{
#if UNITY_ANDROID
    public delegate void ProductPurchasedEventHandler(string strSpsn);
    public delegate void PurchaseErrorEventHandler(string error);
    public static event ProductPurchasedEventHandler purchaseSuccessful;
    public static event PurchaseErrorEventHandler purchaseError;
	public delegate void ProductPurchasedFinish(int nStatus, string strSpsn, string strError);
    public static ProductPurchasedFinish purchaseFinish;
	public static string GAMEID = "100003";
	public static string GAMENAME = "天龍八部-真金庸 最武侠";
    private static int   LastSendIdx = 0;
    
    // Use this for initialization
    void Start()
    {
    }
    
    private void pplog_update()
    {
        JsonData jd = GlobalSave.GetOrderTable();
        if(jd != null)
        {
            int num = jd.Count;
            Debug.Log( "pllog_update num="+num);
            Debug.Log( "pllog_update begin LastSendIdx="+LastSendIdx);

            if(LastSendIdx>=num)
            {
                LastSendIdx = 0;
            }
            for(int i = LastSendIdx;i<num; i++)
            {
                if(jd[i] != null)
                {
                    if(jd[i]["accountID"] != null)
                    {
                        string orderacc = (string)jd[i]["accountID"];
                        int orderaccint = Convert.ToInt32(orderacc);
                        int accountIDint = Convert.ToInt32(Obj_MyselfPlayer.GetMe().accountID);

                        if(orderaccint != accountIDint)
                        {
                            Debug.Log("pllog_Not_Self");
                            //不是自己的单
                            continue;
                        }
                        else
                        {
                            Debug.Log("pllog_Update_SendVarify :" +  JsonMapper.ToJson(jd[i]));
                            // 补提订单.处理丢单情况
                            PurchaseHelper.Instance().VarifyJavaOrder(  (string)jd[i]["gid"], 
                                                                        (string)jd[i]["pid"], 
                                                                        (string)jd[i]["goodsPrice"], 
                                                                        (string)jd[i]["orderId"]
                                                                      ); 
                            LastSendIdx++;
                            Debug.Log( "pllog_update mid LastSendIdx="+LastSendIdx);

                            if(LastSendIdx >= num)
                            {
                                LastSendIdx = 0;
                            }
                            Debug.Log( "pllog_update end LastSendIdx="+LastSendIdx);
                            //晕，一个逻辑帧只发一个包，在收到反馈包之前不能发第二个，所以发包不能太快。
                            break;
                        }
                    }
                }
            }
        }
    }

    void Update()
    {
		/*
        Obj_MyselfPlayer.GetMe().cyou_pay_time += Time.deltaTime;
		//Debug.Log( "CYouPayManager::Update() " + dtime );
        if (Obj_MyselfPlayer.GetMe().cyou_pay_time > 10)
        { // 10秒
            Obj_MyselfPlayer.GetMe().cyou_pay_time = 0;
            //if(AndroidConfig.isThirdSDKPlatform())
            //{
                pplog_update();
            //}
            //else
            //{
            //    sendLossOrder();
            //}
        }
        */
    }
	
	public static void sendLossOrder(){
		return ;
		Debug.Log( "CYouPayManager::Update()..0" );
        Obj_MyselfPlayer.GetMe().cyou_pay_time = 0;
			string keytable = GlobalSave.GetCyouStoreLossTable();
            if (keytable == "")
			{
				Debug.Log( "CYouPayManager::Update().....1" );
                return;
			}
		Debug.Log( "-----CYouPayManager::sendLossOrder begin-----" );
            keytable = keytable.Replace("[", "");
			Debug.Log( "CYouPayManager::Update().....1 " + keytable );
            string[] strSub = keytable.Split(']');
            foreach (string str in strSub)
            {
                if (str == null || str == "")
				{
					Debug.Log( "CYouPayManager::Update().....2" );
                    continue; // 如果是null或空串，则都继续
				}
                string[] str_order_account = str.Split('|');
                string order = str_order_account[0];
                string accountid = str_order_account[1];
                if (order == null || order == "" ||
                    accountid == null || accountid == "")
                { // 如果分割出来的数据不对
					Debug.Log( "CYouPayManager::Update().....3" );
                    continue;
                }
                if (accountid != Obj_MyselfPlayer.GetMe().accountID.ToString())
                { // 账号验证不合法
					Debug.Log( "CYouPayManager::Update().....4" );
                    continue;
                }
                long account = Obj_MyselfPlayer.GetMe().accountID;
                string keyy = GlobalSave.GetOrderKey(order, account);
                string info = PlayerPrefs.GetString(keyy);
                if (info == null || info == "")
                {
					Debug.Log( "CYouPayManager::Update().....5 " + keyy );
					GlobalSave.DelCyouStoreLossOrder(order, account);
                    continue; // 没有相关的掉单信息

                }
                string[] info_gid_pid_gprice = info.Split(':');
                string gid = info_gid_pid_gprice[0];
                string pid = info_gid_pid_gprice[1];
                string gprice = info_gid_pid_gprice[2];
                if (gid == null || gid == "" ||
                    pid == null || pid == "" ||
                    gprice == null || gprice == "")
                { // 数据错误
					Debug.Log( "CYouPayManager::Update().....6" );
                    continue;
                }
				Debug.Log( "CYouPayManager::Update().....7" );
				PurchaseHelper.Instance().VarifyTheLossOrder(gid, pid, gprice, order); // 补提订单.处理丢单情况
            }
		Debug.Log( "-----CYouPayManager::sendLossOrder end-----" );
	}
	
	
    public static void purchaseItem(string strID, string strName, string strPrice, int nNum)
    {
        string strGameID = GAMEID;
        string strGameName = GAMENAME;
        string strGroupID = "0";
        string strGroupName = SplashController.serverName;
        string strItemRealPrice = strPrice;
        string strItemmarketPrice = strPrice;
        string strUID = "";
		{
			strUID = AccountManager.Instance.GetCurEmail();
		}

        string strPurchaseInfo = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}",
                                    strGameID,
                                    strGameName,
                                    strGroupID,
                                    strGroupName,
                                    strID,
                                    strName,
                                    nNum.ToString(),
                                    strItemRealPrice,
                                    strItemmarketPrice,
                                    strUID);
        WebMediator.OpenCyouPayWindow(strPurchaseInfo);
        //using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        //{

        //    using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        //    {
        //        obj_Activity.Call("openPayWindow", strPurchaseInfo);
        //    }
        //}
    }
	public static string orderID = "";
	public static void purchaseUCItem(GlobalSave.SOrder order)
    {
        /*string strGameID = GAMEID;
        string strGameName = GAMENAME;
        string strGroupID = "0";
        string strGroupName = SplashController.serverName;
        string strItemRealPrice = strPrice;
        string strItemmarketPrice = strPrice;
        string strUID = "";
		{
			strUID = AccountManager.Instance.GetCurEmail();
		}
        string strPurchaseInfo = string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}:{8}:{9}",
                                    strGameID,
                                    strGameName,
                                    strGroupID,
                                    strGroupName,
                                    strID,
                                    strName,
                                    nNum.ToString(),
                                    strItemRealPrice,
                                    strItemmarketPrice,
                                    strUID);
        WebMediator.OpenCyouPayWindow(strPurchaseInfo);
		*/

        //三位随机数
        System.Random ran=new System.Random();  
        int RandKey=ran.Next(100,999); 
        Debug.Log("pllog_RandKey="+RandKey);
		if(AndroidConfig.is91Channel())
		{
			string keytemp = "101742";
			orderID = WebMediator.GetMacAddress().Replace(":", "") 
                    + RandKey.ToString()
                    + System.DateTime.Now.ToString("ddHHmmssfffff")
                    + "_"
                    + keytemp;
		}
		else if(AndroidConfig.isWanDouJiaChannel())
		{
			string keytemp = AndroidConfig.getBillingAppkey();
			orderID = WebMediator.GetMacAddress().Replace(":", "")
                    + RandKey.ToString()
                    + System.DateTime.Now.ToString("ddHHmmssfffff")
                    + "_"
                    + keytemp;
		}else if(AndroidConfig.isHuaweiChannel()){
			orderID = WebMediator.GetMacAddress().Replace(":", "") 
                    + System.DateTime.Now.ToString("mmssfffff");
		}
		else if(AndroidConfig.isSogouChannel()){
			orderID = WebMediator.GetMacAddress().Replace(":", "") 
                    + System.DateTime.Now.ToString("mmssfffff");
		}
		else
		{
			orderID = WebMediator.GetMacAddress().Replace(":", "") 
                    + RandKey.ToString()
                    + System.DateTime.Now.ToString("ddHHmmssfffff");
		}
		
		//UC渠道代码重构 lihao_yd  2013-12-10
		/*
		//构造json字符串====================
		JsonData data = new JsonData();
        data["appkey"] = AndroidConfig.GetAppKey();
        data["orderId"] = orderID;
        data["apiKey"] =AndroidConfig.GetApiKey();
		data["cpId"] =AndroidConfig.GetCPID();
		
		PlayerPrefs.SetString( GlobalSave.CyouStoreLossTempOrderId, orderID );

        string  json1= data.ToJson();
           
//		string customInfo = "[{'appkey':'"+AndroidConfig.GetAppKey()+
//			                "','orderId':'"+orderID+
//				            "','apiKey':'"+AndroidConfig.GetApiKey()+
//				            "','cpId':'"+AndroidConfig.GetCPID()+"'}]";
		*/
		
		order.strOder = orderID;
		OrderManager.Instance().AddOrder(order);
		//整合UC渠道代码 lihao_yd  2013-11-25
		/*
		if(AndroidConfig.isUCChannel()){
			UCGameSdk.pay(false,float.Parse(order.goodPrice),AndroidConfig.GetServerID(),
			Obj_MyselfPlayer.GetMe().accountID.ToString(),Obj_MyselfPlayer.GetMe().accountName,
			Obj_MyselfPlayer.GetMe().level.ToString(), json1);
		}else 
		*/
		if(AndroidConfig.isUCChannel()||AndroidConfig.isThirdSDKPlatform()){
			JsonData dataThird = new JsonData();
            dataThird["ACC"] = Obj_MyselfPlayer.GetMe().accountID;
            dataThird["OID"] = order.strOder;
            dataThird["GID"] = order.goodId.ToString();
            dataThird["PID"] = order.productID;
            dataThird["PPRICE"] = order.goodPrice;
            dataThird["PNAME"] = order.goodName;
			Debug.Log("----pay--PNAME = "+order.goodName);
			Debug.Log("----pay--PPRICE = "+order.goodPrice);
			PaySystemInterface.doSdk("doOrder",dataThird.ToJson());
		}
		
    }

    #region On Pay Return
#if OLD
    void OnResultRet(string strMsg)
    {
        int nStatus = -1;
        int nTradeStatus = -1;
        string strSpsn = new string('\0', 5);
        StrParse(strMsg, out nStatus, out nTradeStatus, out strSpsn);
        //请求成功
        if (nStatus == 0)
        {            //付费成功
            if (nTradeStatus == 0)
            {
                if (null != purchaseSuccessful)
                {
                    purchaseSuccessful(strSpsn);
                }
            }
            else
            {
                if (null != purchaseError)
                {
                    purchaseError("购买失败"); //TODO 字典替换                
                }
            }
        }
        else
        {
            if (null != purchaseError)
            {
                purchaseError("请求失败"); //TODO 字典替换            
            }
        }
    }
#endif
	void OnResultRet(string strMsg)
	{
		int nStatus = -1;
		int nTradeStatus = -1;
		string strSpsn = new string('\0', 5);
		StrParse(strMsg, out nStatus, out nTradeStatus, out strSpsn);
		Debug.Log( "------OnResultRet strMsg=" + strMsg + " strSpsn=" + strSpsn );
        //请求成功
        if (nStatus == 0)
        {            //付费成功
            if(nTradeStatus == 0)
            {
                //支付成功通知java层，标示
                Debug.Log("pllog_Cyou_success:"+strSpsn);
                WebMediator.SuccessPayLog(strSpsn);

				// 转移到OnOrderRet
				//PurchaseHelper.Instance().AddCyouOrder(strSpsn);
				if (null != purchaseFinish)               
				{                   
					purchaseFinish(nStatus, strSpsn, "");               
				}
            }
            else
            {                
				if (null != purchaseFinish)                
				{                    
					purchaseFinish(-1, strSpsn, "购买失败"); //TODO 字典替换                
				}
            }
        }
        else
        {            
			if (null != purchaseFinish)            
			{                
				purchaseFinish(-1, strSpsn, "请求失败"); //TODO 字典替换            
			}
        }
	}
			
    void OnOrderRet(string strMsg)
    {
        int nStatus = -1;
        int nTradeStatus = -1;
        string strSpsn = new string('\0', 5);
        StrParse(strMsg, out nStatus, out nTradeStatus, out strSpsn);
		
        //TODO 		
		Debug.Log( "------OnOrderRet strMsg=" + strMsg + " strSpsn=" + strSpsn );

        /*
        //畅游支付，存单
        {
            string strAccountid, strgpg;
            GlobalSave.GetCyouStoreLossGoodInfoTemp(out strAccountid, out strgpg);
            if( strAccountid != null &&
                strgpg  != null
                )
            {
                Debug.Log("pllog_Cyou_strAccountid:"+strAccountid);
                Debug.Log("pllog_Cyou_strgpg:"+strgpg);

                int s32Accountid        = Convert.ToInt32(strAccountid);
                int s32SelfAccountid    = Convert.ToInt32(Obj_MyselfPlayer.GetMe().accountID);

                if(s32Accountid == s32SelfAccountid)
                {
                    string[] info_gid_pid_gprice = strgpg.Split('|');
                    string gid = info_gid_pid_gprice[0];
                    string pid = info_gid_pid_gprice[1];
                    string gprice = info_gid_pid_gprice[2];

                    Debug.Log("pllog_Cyou_gid:"+gid);
                    Debug.Log("pllog_Cyou_pid:"+pid);
                    Debug.Log("pllog_Cyou_gprice:"+gprice);

                    JsonData cydata     = new JsonData();
                    cydata["ACC"]       = Obj_MyselfPlayer.GetMe().accountID;
                    cydata["OID"]       = strSpsn;//畅游渠道的订单号是由sdk自己生成的，在这里第一次从java中传出
                    cydata["GID"]       = gid;
                    cydata["PID"]       = pid;
                    cydata["PPRICE"]    = gprice;
                    cydata["PNAME"]     = "CYProduct";

                    WebMediator.SavePayLog(cydata.ToJson());
                    Debug.Log("pllog_Cyou_SavePayLog:"+cydata.ToJson());
                }

            }
            GlobalSave.SOrder order = PurchaseHelper.Instance().CheckOrder();
        }
        */
		// 订单记录
		PurchaseHelper.Instance().AddCyouOrder(strSpsn);
        //GlobalSave.AddCyouStoreLossOrder(strSpsn, Obj_MyselfPlayer.GetMe().accountID); // 这里先行设置如果掉单，的单号
        GlobalSave.SetCyouStoreLossGoodInfoTempToReal(strSpsn, Obj_MyselfPlayer.GetMe().accountID); // 将保存的临时信息转成正式信息
    }
	
	void OnPayClose(string strMsg)
	{
		BoxManager.removeMessage();
	}
    #endregion

    void StrParse(string strInput, out int nStatus, out int nTradeStatus, out string strSpsn)
    {
        nStatus = -1;
        nTradeStatus = -1;
        strSpsn = "";
        const int KEYVALUE_NUM = 2;
        string[] strSub = strInput.Split(',');
        foreach (string str in strSub)
        {
            string[] strArray = str.Split(':');
            if (strArray.Length == KEYVALUE_NUM)
            {
                if (strArray[0] == "status" || strArray[0] == " status" )
                {
                    nStatus = int.Parse(strArray[1]);
                }
                else if ( strArray[0] == "tradeStatus" || strArray[0] == " tradeStatus")
                {
                    nTradeStatus = int.Parse(strArray[1]);
                }
                else if ( strArray[0] == "spsn" || strArray[0] == " spsn")
                {
                    strSpsn = strArray[1];
                }
            }
            else
            {
                Debug.LogWarning("Error Return String: " + strInput);
                break;
            }
        }
    }
#endif
}
