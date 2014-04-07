using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;

public class PurchaseRecordController : MonoBehaviour {
	
	private MainUILogic mainLogic;
	public UIScrollBar scrollBar;
	public UIDraggablePanel dragPanel;
	public GameObject gridPurchase;
	List<GameObject> items = new List<GameObject>();
	private string purchaseListItem = "PurchaseRecordItem";
	private List<PurchaseRecord> purchaseList = new List<PurchaseRecord>();
	public UILabel recordCount;
	//Transform 的缓存
    private Transform cachedTransform;
	
//	private Vector3 btnEnablePos = new Vector3(230, 5, 0);
//	private Vector3 btnDisablePos = new Vector3(10000, 5, 0);
	// Use this for initialization
	void Start () {
		mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		
		 
	}
	
	// Update is called once per frame
	void Update () {
		//FreashScroll();
	}
	
	void OnEnable()
	{
		
		FreshUI();
	}
	
	public void FreshUI()
	{
		DestroyItems();
		purchaseList = FreashPurchase();
		cachedTransform = transform;
		//设置 items的获得函数
        BagItemsPage bagItempage = cachedTransform.GetComponent<BagItemsPage>();
        if (bagItempage != null)
        {
			bagItempage.onCalculateShowPurchaseRecordItemsFunction=null;
            bagItempage.onDragonFinishedClearItemsFun=null;
            bagItempage.onShowPurchaseRecordItemsFunction=null;
            bagItempage.onCalculateShowPurchaseRecordItemsFunction += CalculateShowItems;
            bagItempage.onDragonFinishedClearItemsFun += OnDragonFinishedClearItemsFun;
            bagItempage.onShowPurchaseRecordItemsFunction += ShowCardItem;
        }
		 //开始分页显示功能
        if (cachedTransform != null)
        {
            cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages();
        }
		gridPurchase.GetComponent<UIGrid>().repositionNow = true;
		scrollBar.scrollValue = 0;
		recordCount.text = purchaseList.Count + "/100";
	}
	
	public void ShowCardItem(PurchaseRecord purchase)
    {
		GameObject newItem = CardListItemPool.Instance.GetListItem(purchaseListItem);
		newItem.SetActive(false);
		GameObject buttons = newItem.transform.FindChild("Buttons").gameObject;
		GameObject checkBtn = buttons.transform.GetChild(0).gameObject;
		checkBtn.SetActive(true);
		newItem.transform.parent = gridPurchase.transform;
		newItem.transform.localPosition = new Vector3(0,0,-1);
		newItem.transform.localScale = new Vector3(1,1,1);
		UILabel goodID = newItem.transform.FindChild("Labels/Label-ID-Value").GetComponent<UILabel>();
		goodID.text = purchase.goodsId;
		UILabel price = newItem.transform.FindChild("Labels/Label-Price-Value").GetComponent<UILabel>();
#if UNITY_ANDROID
		//2013-11-27 11:30 lihao_yd 修改官网包充值记录显示价格不对的bug
		if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_CHANGYOU){
			float cyouPayMoney = purchase.goodsPrice/100;
			price.text = cyouPayMoney+"$";
			Debug.Log("----lee-PurchaseRecord-showUI-PAY_CHANGYOU-price.text=="+cyouPayMoney);
		}else if (DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_THIRD){
			price.text = purchase.goodsPrice.ToString()+"$";
			Debug.Log("----lee-PurchaseRecord-showUI-PAY_THIRD-price.text=="+purchase.goodsPrice.ToString());
		}else if(DeviceHelper.IAPStyle() == (int)IAPStyle.PAY_GooglePay){
			price.text = purchase.goodsPrice.ToString()+"$";
			Debug.Log("----lee-PurchaseRecord-showUI-GooglePay-price.text==" + price.text);
		}
#else
		price.text = purchase.goodsPrice.ToString()+"$";
#endif
		UILabel channel = newItem.transform.FindChild("Labels/Label-Channel-Value").GetComponent<UILabel>();
		channel.text = purchase.channel;
		UILabel state = newItem.transform.FindChild("Labels/Label-State-Value").GetComponent<UILabel>();
		if(purchase.goodState == 0)
		{
			state.text = "[d07771]订单未完成";
		}
		else if(purchase.goodState == 1)
		{
			state.text = "[7bc26a]订单已完成";
		}
		else if(purchase.goodState == 2)
		{
			state.text = "[7a7a78]订单已失效";
		}
		UILabel time = newItem.transform.FindChild("Labels/Label-Time-Value").GetComponent<UILabel>();
		time.text = purchase.date;
		checkBtn.name = purchase.goodsId;
		if(purchase.goodState == 0)
		{
			//checkBtn.transform.localPosition = btnEnablePos;
			checkBtn.SetActive(true);
			//checkBtn.GetComponent<UIImageButton>().isEnabled = true;
			UIEventListener.Get(checkBtn).onClick+=SelectPurchaseRecord;
		}
		else if(purchase.goodState == 1)
		{
			//checkBtn.GetComponent<UIImageButton>().isEnabled = false;
			checkBtn.SetActive(false);
			//checkBtn.transform.localPosition = btnDisablePos;
		}
		else if(purchase.goodState == 2)
		{
			//checkBtn.GetComponent<UIImageButton>().isEnabled = false;
			checkBtn.SetActive(false);
			//checkBtn.transform.localPosition = btnDisablePos;
		}
		newItem.SetActive(true);
		items.Add(newItem);
	}
	
	public void SelectPurchaseRecord(GameObject item)
	{
		foreach(PurchaseRecord purchase in purchaseList)
		{
			if(purchase.goodsId == item.name)
			{
				List<GlobalSave.SOrder> orderList = OrderManager.Instance().OrderList();
				if(orderList != null)
				{
					for(int i=0;i< orderList.Count;i++)
					{
						if(orderList[i].strOder == purchase.goodsId)
						{
							PurchaseHelper.Instance().BeginCheckOrder(orderList[i],FinishCheck);
							break;
						}
					}
				}
				break;
			}
		}
	}
	
	public void FinishCheck(bool isSuccess,string result)
	{
		BoxManager.showMessage(result,"");
		GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
		FreshUI();
	}
	
	public List<PurchaseRecord>  CalculateShowItems()
    {
		return purchaseList;
	}
	
	//拖拽完的调用函数
    public void OnDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode dragonMode)
    {
        //清理上一页的Items
        this.resetItems();
    }
	
	private List<PurchaseRecord> FreashPurchase()
	{
		List<PurchaseRecord> resultList = new List<PurchaseRecord>();
		List<GlobalSave.SOrder> orderList = OrderManager.Instance().OrderList();
		if(orderList != null)
		{
			for(int i=0;i<orderList.Count;i++)
			{
				PurchaseRecord result = new PurchaseRecord();
				result.goodsId = orderList[i].strOder;
#if UNITY_ANDROID
				result.goodsPrice = float.Parse(orderList[i].goodPrice);
				Debug.Log("----lee-PurchaseRecord-goodsPrice=="+result.goodsPrice);
				// 修改渠道名，去掉"android_"//
				if(string.IsNullOrEmpty(orderList[i].channel))
					orderList[i].channel = "unknown";
				result.channel = orderList[i].channel.ToLower();
				Debug.Log("----lee-PurchaseRecord-channel=="+result.channel);
				if("unknown".Equals(result.channel)){
					result.channel = "cyou";
				}
				
				//2013-12-13 10:30 lihao_yd 修改豌豆荚渠道充值记录显示遮盖问题  TT 1330
				else if("android_wandoujia_sdk".Equals(result.channel)){
					result.channel = "wdj_sdk";
				}
				else if(result.channel.StartsWith("android_")){
					result.channel = result.channel.Substring("android_".Length);
				}
					
#else
				result.goodsPrice = float.Parse(orderList[i].price);
				result.channel = orderList[i].channel;
#endif
				result.date = orderList[i].date;
				result.goodState = orderList[i].state;
				resultList.Add(result);
			}
		}
		return resultList;
	}
	
	void OnDisable()
	{
		scrollBar.scrollValue=0;
		DestroyItems();
	}
	
	//拖动幅度控制
	private void FreashScroll()
	{	
		if(scrollBar.scrollValue == 0 || scrollBar.scrollValue == 1)
		{
			dragPanel.scale.y = 0.1f;
		}
		else
		{
			dragPanel.scale.y = 1;
		}
	}
	
	//选中购买项
	public void PurchseItem(GameObject item)
	{
	}
	
	//排序算法
    static public int CompareTo(PurchaseRecord goodA, PurchaseRecord goodB)
    {
		//按时间排序
		return goodA.date.CompareTo(goodB.date);
    }
	
	//清空列表
	private void DestroyItems()
	{
		foreach(GameObject obj in items)
		{
			Destroy(obj);
		}
		items.Clear();
	}
	
	//清理Items
	public void resetItems()
	{
		foreach(GameObject item in items)
		{
            if (item != null)
            {
				CardListItemPool.Instance.DestroyItemAndPushToPool(item, purchaseListItem);
            }
		}
		items.Clear();
	}
	
	//返回商铺
	public void ReturnToPurchase()
	{
		mainLogic.OnPurchaseWindow();
	}
}
