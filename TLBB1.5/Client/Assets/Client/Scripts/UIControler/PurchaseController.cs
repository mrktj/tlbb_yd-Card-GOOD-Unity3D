using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;

public class PurchaseController : MonoBehaviour {

    private MainUILogic mainLogic;
    public UIScrollBar scrollBar;
    public UIDraggablePanel dragPanel;
    public GameObject gridPurchase;
    List<GameObject> items = new List<GameObject>();
    private string purchaseListItem = "PurchaseListItem";
    List<PurchaseInfo> purchaseList;
    public UISprite firstPurchase;
    private string purchase_Key = "Purchase_Key";

    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();

    }

    void OnEnable()
    {

#if UNITY_ANDROID		
		int level = 20;
		if(Obj_MyselfPlayer.GetMe().level >= level || WebMediator.GetNetworkType().Equals("3g")){
			Debug.Log("open other billing button");
			otherPurchaseBtn.SetActive(true);			 
		}else{
			Debug.Log("close other billing button");
			otherPurchaseBtn.SetActive(false);
		}
#elif UNITY_IPHONE
		otherPurchaseBtn.SetActive(false);
#endif			
		RefreshUI(true);
        if (mainLogic == null)
            mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
        mainLogic.mainController.GetComponent<MainController>().showBottomBar();
    }

    void OnDisable()
    {
        BoxManager.removeMessage();

        ScrollData scData = new ScrollData(scrollBar.scrollValue);
        Obj_MyselfPlayer.GetMe().SetScrollValue(purchase_Key, scData);
        DestroyItems();
    }

    void Update()
    {
        //FreashScroll();
    }

	public void RefreshUI(bool bNeedMsgBox)
    {
        if (!PurchaseHelper.Instance().BeginCheckOrder(OnFinishCheckOrder))
        {
			RequestGoodsList(bNeedMsgBox);
		}   
        //refreash top user info
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
    }

    void OnFinishCheckOrder(bool bSuccess, string result)
    {
        BoxManager.removeMessage();
        BoxManager.showMessage(result, ""); //WML MARK

        UIEventListener.Get(BoxManager.buttonYes).onClick += ReShowMessage;
    }

    void ReShowMessage(GameObject button)
    {
        RefreshUI(true);
    }

	public void UpdateFirst()
	{
		/*
		if(DeviceHelper.GetChannelID() != "App_Store")
		{
			if(Obj_MyselfPlayer.GetMe().purchaseDollar==0)
			{
				//首次
				firstPurchase.gameObject.SetActive(true);
			}
			else
			{
				//非首次
				firstPurchase.gameObject.SetActive(false);
			}
		}
		*/
		
		//海外版,使用服务器开关来控制首充界面的现实;
		if(Obj_MyselfPlayer.GetMe().giftison == 1 && Obj_MyselfPlayer.GetMe().purchaseDollar==0){
			//首次
			firstPurchase.gameObject.SetActive(true);
			
		}else{
			//非首次
			firstPurchase.gameObject.SetActive(false);
			
		}
	}


	public void RequestGoodsList(bool bNeedMsgBox)
	{
		UpdateFirst();

		if((IAPStyle)DeviceHelper.IAPStyle()==IAPStyle.PAY_APPSTORE)
		{
			//appstore
			NetworkSender.Instance().RequestAppStoreProductList(GetPurchaseDone);
		}
		else if((IAPStyle)DeviceHelper.IAPStyle()==IAPStyle.PAY_IAPPPAY)
		{
			//爱贝
			NetworkSender.Instance().RequestIAppPayProductList(GetPurchaseDone);
		}
        else if ((IAPStyle)DeviceHelper.IAPStyle() == IAPStyle.PAY_PP)
        {
            NetworkSender.Instance().RequestPPProductList(GetPurchaseDone);
        }
#if UNITY_ANDROID
        else if ((IAPStyle)DeviceHelper.IAPStyle() == IAPStyle.PAY_CHANGYOU)
        {            //cyou畅游支付平台 服务器逻辑一样  不同平台的服务器 部署配置的channelid 不同
            NetworkSender.Instance().RequestCYouPayProductList(GetPurchaseDone, bNeedMsgBox);
        }
        else if ((IAPStyle)DeviceHelper.IAPStyle() == IAPStyle.PAY_THIRD)
        {            //cyou畅游支付平台 服务器逻辑一样  不同平台的服务器 部署配置的channelid 不同
            NetworkSender.Instance().RequestCYouPayProductList(GetPurchaseDone, bNeedMsgBox);
        }else if((IAPStyle)DeviceHelper.IAPStyle() == IAPStyle.PAY_GooglePay)
        {            //google  不同平台的服务器 部署配置的channelid = 6001 不同
            NetworkSender.Instance().RequestCYouPayProductList(GetPurchaseDone, bNeedMsgBox);
		}
#endif
		else{
			Debug.LogError("IAPStyle error ");
		}
    }
	
	public void OnBindAccount(GameObject button)//2013-12-25 Wang Minglei
	{
		mainLogic.OnBindAccount();
	}
	
    //选中购买项
    public void PurchseItem(GameObject item)
    {
		if (AccountManager.userType != AccountManager.UserType.OldUser || (AccountManager.Instance.CurAccount != null && string.IsNullOrEmpty(AccountManager.Instance.CurAccount.email)))
		{
#if UNITY_ANDROID
			//只有官网渠道才提示绑定账号   lihao_yd  2014-01-18
			if(!AndroidConfig.isUCChannel()&&!AndroidConfig.isThirdSDKPlatform()){
				BoxManager.show(9,"您未绑定邮箱账号,为防止数据丢失请您先绑定邮箱账号",ClientConfigure.title);
				UIEventListener.Get(BoxManager.buttonYes).onClick += OnBindAccount;
				return;
			}
		
#else
			BoxManager.show(9,"您未绑定邮箱账号,为防止数据丢失请您先绑定邮箱账号",ClientConfigure.title);
			UIEventListener.Get(BoxManager.buttonYes).onClick += OnBindAccount;
			return;
#endif
		}
        PurchaseInfo product = new PurchaseInfo();
        for (int i = 0; i < purchaseList.Count; i++)
        {
            if (purchaseList[i].productId == item.name)
            {
                product = purchaseList[i];
                break;
            }
        }
        //		if(AccountManager.Instance.CurAccount == null)
        //		{
        //			Obj_MyselfPlayer.GetMe().isPurchase=true;
        //			mainLogic.OnBindAccount();
        //			AccountManager.Instance.completeDalegate += OnBindComplete;
        //		}
        //		else
        //		{
        //			PurchaseHelper.Instance().MakePurchase(product.productId,product.goodsId);
        //		}
#if UNITY_ANDROID
		// 防止支付取消后改BosManager一直存在，无法继续玩游戏
#else
        BoxManager.showProcessMessage("请稍候...");//WML MARK
#endif
		Debug.Log("pllog_PurchseItem MakePurchase");
        PurchaseHelper.Instance().MakePurchase(product, FinishPay);
    }

    void OnBindComplete()
    {
#if UNITY_ANDROID
		if(AndroidConfig.isLogin())
#else
        if (AccountManager.Instance.CurAccount != null)
#endif
            mainLogic.OnBindSuccess();
    }

    //显示商品信息
    public void GetPurchaseDone(bool bSuccess)
    {
        DestroyItems();
        purchaseList = Obj_MyselfPlayer.GetMe().purchaseInfoList;
        //填充商品列表
        if (purchaseList != null)
        {
			string [] purs = new string[purchaseList.Count];
            purchaseList.Sort(CompareTo);
            // 价格最高排第一个
            if (purchaseList.Count > 0)
            {
                PurchaseInfo last = purchaseList[purchaseList.Count - 1];
                purchaseList.RemoveAt(purchaseList.Count - 1);
                purchaseList.Insert(0, last);
            }

            for (int i = 0; i < purchaseList.Count; i++)
            {
                GameObject newItem = ResourceManager.Instance.loadWidget(purchaseListItem);
                newItem.transform.parent = gridPurchase.transform;
                newItem.transform.localPosition = new Vector3(0, 0, -1);
                newItem.transform.localScale = new Vector3(1, 1, 1);
                newItem.name = purchaseList[i].productId;
                UIEventListener.Get(newItem).onClick += PurchseItem;
                //				UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
                //				if(TableManager.GetCardByID(taskList[i].cardTempletID)!=null)
                //				{
                //					icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(taskList[i].cardTempletID).Appearance).HeadIcon;
                //				}
                UILabel titleLabel = newItem.transform.FindChild("Label-Title").GetComponent<UILabel>();
                titleLabel.text = purchaseList[i].goodsName.ToString();
                UILabel contentLabel = newItem.transform.FindChild("Label-Content").GetComponent<UILabel>();
                contentLabel.text = purchaseList[i].goodDec;
                UILabel payLabel = newItem.transform.FindChild("Label-Pay").GetComponent<UILabel>();
                payLabel.text = purchaseList[i].goodsPrice.ToString() + "$";
                items.Add(newItem);
				purs[i] = purchaseList[i].productId;
            }
#if UNITY_ANDROID
			//查询GooglePay的购买记录
			GooglePayManager.queryInvntoryFromIAB(purs);
#endif			
        }
        gridPurchase.GetComponent<UIGrid>().repositionNow = true;
        StartCoroutine(SetScrollValue());
    }

    public IEnumerator SetScrollValue()
    {
        yield return 0;
        FreshBar();
    }

    public void FreshBar()
    {
        if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(purchase_Key))
        {
            if (purchaseList != null && purchaseList.Count > 4)
            {
                scrollBar.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[purchase_Key].scrollValue;
            }
            else
            {
                scrollBar.scrollValue = 0;
            }
        }
    }

    //排序算法
    static public int CompareTo(PurchaseInfo goodA, PurchaseInfo goodB)
    {
        return goodA.goodsPrice.CompareTo(goodB.goodsPrice);
    }

    //支付完成
    void FinishPay(bool bSuccess, string result)
    {
        BoxManager.removeMessage();
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        BoxManager.showMessage(result, ""); //WML MARK
        UIEventListener.Get(BoxManager.getYesButton()).onClick += Refresh;
    }

    private void Refresh(GameObject button)
    {
		RefreshUI(true);
    }

    //清空列表
    private void DestroyItems()
    {
        foreach (GameObject obj in items)
        {
            Destroy(obj);
        }
        items.Clear();
    }

    //拖动幅度控制
    private void FreashScroll()
    {
        if (scrollBar.scrollValue == 0 || scrollBar.scrollValue == 1)
        {
            dragPanel.scale.y = 0.1f;
        }
        else
        {
            dragPanel.scale.y = 1;
        }
    }

    //返回商铺
    public void ReturnToShop()
    {
        mainLogic.OnShopWindow();
    }

    public void PurchaseRecord()
    {
        mainLogic.OnPurchaseRecordWindow();
    }
	
	string userId; //用户userId
	
	string groupId ; //服务器id
	
	string version;

	string appId ;//appId
	
	string appkey ;//appKey 
	
	string appsecret ;//appSecret

	public static string _appKey = "1385114279759";
	public static string _appsecret = "68a9c2a527c74b05b0325e15453ad9f7";
	public static string _appId = "32";
	
	public GameObject otherPurchaseBtn;
#if UNITY_ANDROID
	public void PurchaseOther()
	{
		//TODO: invoke gash webviwe
		Debug.Log("PurchaseController PurchaseOther");
		
		appId = _appId;
		appkey = _appKey;
		appsecret = _appsecret;
		
		userId = Obj_MyselfPlayer.GetMe().uid;
		groupId = Obj_MyselfPlayer.GetMe().areaId.ToString();
		version = ClientConfigure.VersionNumber.ToString();
		Debug.Log("APPID:"+appId);
		Debug.Log("appkey:"+appkey);
		Debug.Log("appsecret:"+appsecret);
		Debug.Log("userId:"+userId);
		Debug.Log("groupId:"+groupId);
		Debug.Log("version:"+version);
		
		KDTLWebView.ShowWebView(ClientConfigure.getOtherPurchaseUrl()+"?userId="+userId
												+"&groupId="+groupId
												+"&appId="+appId
												+"&appkey="+appkey
												+"&appsecret="+appsecret
												+"&version="+version
												,PurchaseOver);
	}
	
	public void PurchaseOver(){
		NetworkSender.Instance().GetUserInfo(UpdateUserData);
	}
	
	public void UpdateUserData(bool success){
		if(success){
			//refreash top user info
			GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
		}
	}
#endif	
}
