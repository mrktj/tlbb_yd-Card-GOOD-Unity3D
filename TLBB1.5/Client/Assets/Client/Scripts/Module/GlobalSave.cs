using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class GlobalSave
{
    private static string AppStoreOrder = "AppStoreOrder";
    private static string AppStoreGoodId = "AppStoreGoodId";
    private static string AppStoreRecieipt = "AppStoreRecieipt";
    private static string AppStorePid = "AppStorePid";
    private static string IAppPayOrder = "IAppPayOrder";
    private static string IAppPayGoodId = "IAppPayGoodId";
    private static string IAppPayProductID = "IAppPayProductID";
    private static string PPPayOrder = "PPPayOrder";
    private static string PPPayGoodId = "PPPayGoodId";
    private static string PPPayProductID = "PPPayProductID";
    private static string PPPayPrice = "PPPayPrice";
    private static string PPPayUserID = "PPPayUserID";
    private static string PPPayZoneID = "PPPayZoneID";

#if UNITY_ANDROID
    private static string CyouStoreOrder = "CyouStoreOrder";
    private static string CyouStoreGoodId = "CyouStoreGoodId";
    private static string CyouStoreRecieipt = "CyouStoreRecieipt";
    private static string CyouStorePid = "CyouStorePid";

    private static string CyouStoreLossTable = "CyouStoreLossTable"; // 这个里面保存丢单的单号和accountid（obj_myplayerslef里的）
    private static string CyouStoreLossTemp = "CyouStoreLossTemp"; // 临时信息
	private static string CyouStoreLossTempAccountId = "CyouStoreLossTempAccountId"; // 临时使用的账号id
	public static string CyouStoreLossTempOrderId = "CyouStoreLossTempOrderId"; //由客户端生成的
    public static string PrefOrderTable = "OrderTable"; //客户端订单记录
#endif	

    public static string NORESULT = "NULL";


    public class SOrder
    {
        public string strOder;
        public int goodId;
        public string productID;
        public string reciept;
#if UNITY_ANDROID
        public string goodName;
        public string goodPrice;
		public int 	  goodsNumber;
		public string signature;
		public string purchaseData;		
#endif
		public string price;
		public string productName;
		public string userID;	//PP
		public int zoneID;		//PP
        public string date;
        public string channel;
        public int state;

        public void Copy(SOrder otherOrder)
        {
            strOder = otherOrder.strOder;
            goodId = otherOrder.goodId;
            productID = otherOrder.productID;
            reciept = otherOrder.reciept;
#if UNITY_ANDROID
			goodPrice = otherOrder.goodPrice;
			signature = otherOrder.signature;
			purchaseData = otherOrder.purchaseData;			
#else
            price = otherOrder.price;
#endif
            productName = otherOrder.productName;
            userID = otherOrder.userID;
            zoneID = otherOrder.zoneID;
            date = otherOrder.date;
            channel = otherOrder.channel;
            state = otherOrder.state;
        }
        public string ToString()
        {
            return (strOder + '\t' +
                productName + '\t' +
                date + '\t' +
                channel + '\t' +
                state.ToString() + '\t' +
                goodId.ToString() + '\t' +
                productID + '\t' +
                reciept + '\t' +
#if UNITY_ANDROID
				goodPrice.ToString() + '\t' +
				signature + '\t' +
				purchaseData  + '\t' +
#else
                price.ToString() + '\t' +
#endif
                userID + '\t' +
                zoneID.ToString());
        }

        public bool FromString(string strData)
        {
            string[] strPart = strData.Split('\t');
            if (strPart.Length < 11)
            {
                Debug.LogError("read order file error");
                return false;
            }

            strOder = strPart[0];
            productName = strPart[1];
            date = strPart[2];
            channel = strPart[3];
            state = int.Parse(strPart[4]);
            goodId = int.Parse(strPart[5]);
            productID = strPart[6];
            reciept = strPart[7];
#if UNITY_ANDROID
			goodPrice = strPart[8];
			signature = strPart[9];
			purchaseData = strPart[10];
            userID = strPart[11];
            zoneID = int.Parse(strPart[12]);			
#else
            price = strPart[8];
            userID = strPart[9];
            zoneID = int.Parse(strPart[10]);			
#endif

            return true;

        }
    };

    public static void SetIAppPayOrder(string strOrder, int goodID, string productID)
    {
        PlayerPrefs.SetString(IAppPayOrder, strOrder);
        PlayerPrefs.SetInt(IAppPayGoodId, goodID);
        PlayerPrefs.SetString(IAppPayProductID, productID);
        PlayerPrefs.Save();
    }

    public static void CleanIAppPayOrder()
    {
        PlayerPrefs.SetString(IAppPayOrder, NORESULT);
        PlayerPrefs.SetInt(IAppPayGoodId, -1);
        PlayerPrefs.SetString(IAppPayProductID, NORESULT);
    }

    public static SOrder GetIAppPayOrder()
    {
        string strOder = PlayerPrefs.GetString(IAppPayOrder, NORESULT);
        if (strOder == NORESULT)
        {
            return null;
        }

        SOrder curOrder = new SOrder();
        curOrder.strOder = strOder;
        curOrder.goodId = PlayerPrefs.GetInt(IAppPayGoodId);
        curOrder.productID = PlayerPrefs.GetString(IAppPayProductID);
        return curOrder;
    }

    public static void SetPPPayOrder(int goodID, string productID, string orderID, int price, string userID, int zoneID)
    {
        PlayerPrefs.SetString(PPPayOrder, orderID);
        PlayerPrefs.SetInt(PPPayGoodId, goodID);
        PlayerPrefs.SetString(PPPayProductID, productID);
        PlayerPrefs.SetInt(PPPayPrice, price);
        PlayerPrefs.SetString(PPPayUserID, userID);
        PlayerPrefs.SetInt(PPPayZoneID, zoneID);
        PlayerPrefs.Save();
    }

    public static void CleanPPPayOrder()
    {
        PlayerPrefs.SetString(PPPayOrder, NORESULT);
        PlayerPrefs.SetInt(PPPayGoodId, -1);
        PlayerPrefs.SetString(PPPayProductID, NORESULT);
        PlayerPrefs.SetInt(PPPayPrice, -1);
        PlayerPrefs.SetString(PPPayUserID, NORESULT);
        PlayerPrefs.SetInt(PPPayZoneID, -1);
    }

    public static SOrder GetPPPayOrder()
    {
        string strOder = PlayerPrefs.GetString(PPPayOrder, NORESULT);
        if (strOder == NORESULT)
        {
            return null;
        }

        SOrder curOrder = new SOrder();
        curOrder.strOder = strOder;
        curOrder.goodId = PlayerPrefs.GetInt(PPPayGoodId);
        curOrder.productID = PlayerPrefs.GetString(PPPayProductID);
        //curOrder.price = PlayerPrefs.GetInt(PPPayPrice);
        curOrder.userID = PlayerPrefs.GetString(PPPayUserID);
        curOrder.zoneID = PlayerPrefs.GetInt(PPPayZoneID);
        return curOrder;
    }

    

    public static void SetAppStoreOrder(string tid, int goodID, string pid, string recieipt)
    {
        PlayerPrefs.SetString(AppStoreOrder, tid);
        PlayerPrefs.SetInt(AppStoreGoodId, goodID);
        PlayerPrefs.SetString(AppStoreRecieipt, recieipt);
        PlayerPrefs.SetString(AppStorePid, pid);
        PlayerPrefs.Save();
    }

    public static void CleanAppStoreOrder()
    {
        PlayerPrefs.SetString(AppStoreOrder, NORESULT);
        PlayerPrefs.SetInt(AppStoreGoodId, -1);
        PlayerPrefs.SetString(AppStorePid, NORESULT);
        PlayerPrefs.SetString(AppStoreRecieipt, NORESULT);
    }

    public static SOrder GetAppStoreOrder()
    {
        string strOder = PlayerPrefs.GetString(AppStoreOrder, NORESULT);
        if (strOder == NORESULT)
        {
            return null;
        }

        SOrder curOrder = new SOrder();
        curOrder.strOder = strOder;
        curOrder.goodId = PlayerPrefs.GetInt(AppStoreGoodId);
        curOrder.productID = PlayerPrefs.GetString(AppStorePid);
        curOrder.reciept = PlayerPrefs.GetString(AppStoreRecieipt);

        return curOrder;
    }

    public static bool IsFirstSetup()
    {
        int value = PlayerPrefs.GetInt("FirstSetup", -1);
        return value < 0;
    }

    public static void SetFirstSetup()
    {
        PlayerPrefs.SetInt("FirstSetup", 1);
        PlayerPrefs.Save();
    }
#if UNITY_ANDROID
#region Cyou Order
	public static void SetCyouStoreOrder(int goodID, string pid, string GPrice, string GName)
	{
        //畅游渠道很特殊，调用时还没有生成订单号，所以暂时填写为-1;
        Debug.Log("pllog_SetCyouStoreOrder:" + CyouStoreGoodId + ":" + goodID);
		PlayerPrefs.SetInt(CyouStoreGoodId, goodID);

        Debug.Log("pllog_SetCyouStoreOrder:" + CyouStorePid + ":" + pid);
		PlayerPrefs.SetString(CyouStorePid, pid);

		PlayerPrefs.Save();
	}
	
	public static void CleanCyouStoreOrder()
	{
		PlayerPrefs.SetString(CyouStoreOrder, NORESULT);
		PlayerPrefs.SetInt(CyouStoreGoodId, -1);
		PlayerPrefs.SetString(CyouStorePid, NORESULT);
		PlayerPrefs.SetString(CyouStoreRecieipt, NORESULT);
	}
	
	public static SOrder GetCyouStoreOrder()
	{
		string strOder = PlayerPrefs.GetString(CyouStoreOrder, NORESULT);
		if (strOder == NORESULT)
		{
			return null;
		}
		SOrder curOrder = new SOrder();
		curOrder.strOder = strOder;
		curOrder.goodId = PlayerPrefs.GetInt(CyouStoreGoodId);
		curOrder.productID = PlayerPrefs.GetString(CyouStorePid);
		curOrder.reciept = PlayerPrefs.GetString(CyouStoreRecieipt);
		return curOrder;
	}

    public static void GetCyouStoreLossGoodInfoTemp(out string straccount, out string strgpg)
    {
        strgpg      = PlayerPrefs.GetString(CyouStoreLossTemp);
        straccount  = PlayerPrefs.GetString(CyouStoreLossTempAccountId);
    }


	// 解决官网支付调单//
    public static void SetCyouStoreLossGoodInfoTemp( int good_id, string product_id, int good_price, long account_id )
    { // 保存如果掉单的话，恢复掉单必备的商品信息，临时信息
        string value = good_id.ToString() + ":" + product_id + ":" + good_price.ToString();
		Debug.Log( "::SetCyouStoreLossGoodInfoTemp " + value );
        PlayerPrefs.SetString(CyouStoreLossTemp, value);
        PlayerPrefs.Save();
		
		PlayerPrefs.SetString(CyouStoreLossTempAccountId, account_id.ToString());
		PlayerPrefs.Save();
    }
    public static void SetCyouStoreLossGoodInfoTempToReal( string loss_order_id, long account_id )
    { // 将临时信息转成正式信息
        string keyy = GetOrderKey(loss_order_id, account_id);
        string value = PlayerPrefs.GetString(CyouStoreLossTemp);
        if (value == null)
            value = "";
		Debug.Log( "::SetCyouStoreLossGoodInfoTempToReal " + value );
        PlayerPrefs.SetString(keyy, value);
        PlayerPrefs.Save();
    }
    public static void AddCyouStoreLossOrder( string loss_order_id, long account_id )
    {
        string nowtable = PlayerPrefs.GetString(CyouStoreLossTable);
		Debug.Log( "AddCyouStoreLossOrder 0 " + nowtable );
        if (nowtable == null)
            nowtable = "";
		Debug.Log( "AddCyouStoreLossOrder 1 " + nowtable );
        nowtable += GetOrderKey(loss_order_id, account_id);
		Debug.Log( "AddCyouStoreLossOrder 2 " + nowtable );
        PlayerPrefs.SetString(CyouStoreLossTable, nowtable);
        PlayerPrefs.Save();
		Debug.Log( "AddCyouStoreLossOrder 3 " );
    }

    public static void DelCyouStoreLossOrder( string order_id, long account_id )
    {
        string keyy = GetOrderKey(order_id, account_id);
        string nowtable = PlayerPrefs.GetString(CyouStoreLossTable);
        if (nowtable == null)
            nowtable = "";
		Debug.Log( "::DelCyouStoreLossOrder " + keyy + " " + nowtable );
        nowtable = nowtable.Replace(keyy, "");
		Debug.Log( "::DelCyouStoreLossOrder " + keyy + " " + nowtable );
		PlayerPrefs.SetString( CyouStoreLossTable, nowtable );
        PlayerPrefs.DeleteKey(keyy);
        PlayerPrefs.Save();
    }

    public static JsonData GetOrderTable()
    {
        string strorder = PlayerPrefs.GetString(PrefOrderTable);
        if(strorder == null || strorder.Equals(""))
        {
            return null;
        }
        else
        {
            JsonData data = JsonMapper.ToObject(strorder);
            if( (data.IsArray == true) && (data.Count>0) )
            {
                return data;
            } 
        }
        return null;
    }


    public static string GetCyouStoreLossTable()
    {
        return PlayerPrefs.GetString(CyouStoreLossTable);
    }
    public static string GetOrderKey( string order_id, long account_id )
    {
        return "[" + order_id + "|" + account_id.ToString() + "]";
    }
#endregion
#region UC Order
	// 解决UC支付调单//
 /*   public static void SetUCStoreLossGoodInfoTemp( int good_id, string product_id, int good_price, long account_id )
    { // 保存如果掉单的话，恢复掉单必备的商品信息，临时信息
        string value = good_id.ToString() + "|" + product_id + "|" + good_price.ToString();
		Debug.Log( "::SetUCStoreLossGoodInfoTemp " + value );
        PlayerPrefs.SetString(UCStoreLossTemp, value);
        PlayerPrefs.Save();
		
		PlayerPrefs.SetString(UCStoreLossTempAccountId, account_id.ToString());
		PlayerPrefs.Save();
    }
    public static void SetUCStoreLossGoodInfoTempToReal( string loss_order_id, long account_id )
    { // 将临时信息转成正式信息
        string keyy = GetOrderKey(loss_order_id, account_id);
        string value = PlayerPrefs.GetString(UCStoreLossTemp);
        if (value == null)
            value = "";
		Debug.Log( "::SetUCStoreLossGoodInfoTempToReal " + value );
        PlayerPrefs.SetString(keyy, value);
        PlayerPrefs.Save();
    }
    public static void AddUCStoreLossOrder( string loss_order_id, long account_id )
    {
        string nowtable = PlayerPrefs.GetString(UCStoreLossTable);
		Debug.Log( "AddUCStoreLossOrder 0 " + nowtable );
        if (nowtable == null)
            nowtable = "";
		Debug.Log( "AddUCStoreLossOrder 1 " + nowtable );
        nowtable += GetOrderKey(loss_order_id, account_id);
		Debug.Log( "AddUCStoreLossOrder 2 " + nowtable );
        PlayerPrefs.SetString(UCStoreLossTable, nowtable);
        PlayerPrefs.Save();
		Debug.Log( "AddUCStoreLossOrder 3 " );
    }

    public static void DelUCStoreLossOrder( string order_id, long account_id )
    {
        string keyy = GetOrderKey(order_id, account_id);
        string nowtable = PlayerPrefs.GetString(UCStoreLossTable);
        if (nowtable == null)
            nowtable = "";
		Debug.Log( "::DelUCStoreLossOrder " + keyy + " " + nowtable );
        nowtable = nowtable.Replace(keyy, "");
		Debug.Log( "::DelUCStoreLossOrder " + keyy + " " + nowtable );
		PlayerPrefs.SetString( UCStoreLossTable, nowtable );
        PlayerPrefs.DeleteKey(keyy);
        PlayerPrefs.Save();
    }

    public static string GetUCStoreLossTable()
    {
        return PlayerPrefs.GetString(UCStoreLossTable);
    }*/
#endregion
#endif	
}

