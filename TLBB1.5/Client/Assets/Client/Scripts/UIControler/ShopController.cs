using UnityEngine;
using System.Collections;
using card.net;
using System;
using Games.CharacterLogic;
using GCGame.Table;

public class ShopController : MonoBehaviour {
	
	//王明磊 下面数值需和Shop_Notice表ID列一致,勿修改
	enum ShopNoticePicType {
		DefaultImage = -2,
		FirstPurchase = -1,
	};
	
	public UITexture shopBottomPic;
	
	// WML 用于匹配用户点击的购买按钮,顺序和PB定义一致,用来代表PB里的type
	//required int32 type = 2;	//1 扩充背包、2 购买体力、3 扩充好友、4 购买PVP（未开放）
	public enum BuyType {
		BuyBag = 1, // type = 1
		BuyPower,
		BuyFriend,
		BuyPVP = 5, // type = 4
		BuyQxzbPVP = 6, 
	}
	
	public enum LotteryType {
		Friend = 0,
		Diamond,
	}
	
	private MainUILogic mainLogic;
	
	// Use this for initialization
	void Start () {
		mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable(){
		if(DeviceHelper.GetChannelID() == "App_Store")
		{
			shopBottomPic.gameObject.SetActive(false);
		}
		else
		{
			shopBottomPic.gameObject.SetActive(true);
		}
//		if(Obj_MyselfPlayer.GetMe().giftison == 1)
//		{
//			shopBottomPic.gameObject.SetActive(true);
//		}
//		else
//		{
//			shopBottomPic.gameObject.SetActive(false);
//		}
		int pastDays = Obj_MyselfPlayer.GetMe().daysAfterFirstLogin;
		Tab_ShopNotice shopNotice;
		int purchaseDollar = Obj_MyselfPlayer.GetMe().purchaseDollar;
		if (purchaseDollar <= 0) //如果还没首充 显示首充提示
			shopNotice = TableManager.GetShopNoticeByID((int)ShopNoticePicType.FirstPurchase);
		else //如果已经首冲
		{
			shopNotice = TableManager.GetShopNoticeByID(pastDays);
			if (shopNotice == null) //如果已经首充,而且无特殊设定
				shopNotice = TableManager.GetShopNoticeByID((int)ShopNoticePicType.DefaultImage);
		}
		Debug.Log("Days : " + pastDays + shopNotice.PicSprite);
//		AtlasManager.Instance.SetShopBottomTexture(shopBottomPic,shopNotice.PicSprite);
		FreashFriendNum();
		FreashDiamondNum();
	}
	
	public void AddFriend()
	{
		if (Obj_MyselfPlayer.GetMe().friendNumMax  >= Obj_MyselfPlayer.FriendMaxLimit)
		{
//			BoxManager.showMessage("已达上限，不能购买更多好友上限"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg133);
			return ;
		}
		//购买好友功能未开放呢..
		//BuySth(string.Format("购买5个好友上限共花费{0}元宝",Obj_MyselfPlayer.BuyCost),(int)BuyType.BuyFriend); 
		
		Obj_MyselfPlayer.GetMe().BuyCost = 50;
		BuySth((int)MessageIdEnum.Msg131,(int)BuyType.BuyFriend);
	}
	public void AddBag()
	{
		if (Obj_MyselfPlayer.GetMe().bagMax  >= Obj_MyselfPlayer.BagMaxLimit)
		{
//			BoxManager.showMessage("已达上限，不能购买更多背包上限"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg101);
			return ;
		}
//		BuySth(string.Format("购买5个背包上限共花费{0}元宝",Obj_MyselfPlayer.BuyCost),(int)BuyType.BuyBag);
		Obj_MyselfPlayer.GetMe().BuyCost = 50;
		BuySth((int)MessageIdEnum.Msg69,(int)BuyType.BuyBag);
	}
	public void AddPower()
	{
		NetworkSender.Instance().buyPower(BuySthDone);
//		BuySth(string.Format("您的体力为{0},是否要消耗{1}元宝恢复满体力",power,Obj_MyselfPlayer.BuyCost),(int)BuyType.BuyPower);
//		BuySth((int)MessageIdEnum.Msg72,(int)BuyType.BuyPower);
	}
		
	public void AddPVP()
	{
		int openLev = 16;
		if(Obj_MyselfPlayer.GetMe().level < openLev)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg111,openLev.ToString());
			return;
		}
//		BoxManager.showMessage("功能暂未开放");
//		BoxManager.showMessageByID((int)MessageIdEnum.Msg18);
		Obj_MyselfPlayer.GetMe().BuyCost = 50;
		BuySth((int)MessageIdEnum.Msg130,(int)BuyType.BuyPVP);
	}
	
	
	public void AddQxzbPVP()
	{
		int openLev = 16;
		if(Obj_MyselfPlayer.GetMe().level < openLev)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg111,openLev.ToString());
			return;
		}
		long nleftTime = Obj_MyselfPlayer.GetMe().qxzbStarTime - ((long)Time.time - Obj_MyselfPlayer.GetMe().cursysTime);
		
		//群雄争霸未开启提示
		if(nleftTime >0)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg241);
			return;
		}
		
		//群雄争霸次数已满
		/*if(Obj_MyselfPlayer.GetMe().nQxzbFightTime == 10)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg242);
			return;
		}*/
		
		Obj_MyselfPlayer.GetMe().BuyCost = 20;
		BuySth((int)MessageIdEnum.Msg200,(int)BuyType.BuyQxzbPVP);
	}
	
	//WML --扩充背包 扩充好友 补充体力 购买PVP 统一接口
	public void BuySth (int msgID, int type)
	{
		BoxManager.showMessageByID(msgID);
		BoxManager.getYesButton().name = type.ToString();
		UIEventListener.Get(BoxManager.getYesButton()).onClick += BuySthSure;
	}
	public void BuySthSure(GameObject button)
	{
		if (Obj_MyselfPlayer.GetMe().dollar < Obj_MyselfPlayer.GetMe().BuyCost)
		{
//			BoxManager.showMessage("当前元宝不足");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg73);
			UIEventListener.Get(BoxManager.getYesButton()).onClick += GoRecharge;
			return;
		}
		NetworkSender.Instance().buySth(BuySthDone,Convert.ToInt32(button.name));
	}
	public void BuySthDone (bool isSuccess)
	{
		if (isSuccess)
		{
			mainLogic.SendMessage("refreshTopBar");
//			BoxManager.showMessage("购买成功!");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
		}
		else
		{
			Debug.LogError("Shop Buy Error !");
		}
		FreashDiamondNum();
	}
	
	public void GoRecharge(GameObject button)
	{
		GameRechargeUI();
	}
	
	public void GameRechargeUI()
	{
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn58).ToString());
//		BoxManager.showMessage("功能暂未开放");  
		//BoxManager.showMessageByID((int)MessageIdEnum.Msg18);
		mainLogic.OnPurchaseWindow();
#if UNITY_ANDROID
		CYouPayManager.sendLossOrder();
#endif
	}
	
	public void ReturnToMainUI()
	{
		mainLogic.ReturnToMainUI();
	}
	public void OnFriendLotteryWindow()
	{
		mainLogic.OnFriendLotteryWindow();
	}
	public void OnDiamondLotteryWindow()
	{
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.LOTTERY)
			GuideLottery.Instance.NextStep();//抽奖指引 SELECT_2
		mainLogic.OnDiamondLotteryWindow();
	}
	
	//for guide
	public GameObject getDiamondLotteryButton()
	{
		return transform.FindChild("Buttons/DiamondLotteryBtn").gameObject;
	}
	
	//update info of lottery num
	void FreashFriendNum(){
		UILabel fnum = transform.FindChild("LotteryInfo/FriendInfo/Label").GetComponent<UILabel>();
		int friendLotteryOneCost = TableManager.GetGambleCostByID((int)LotteryController.LOTTERY_TYPE.FRIEND).OneCost;
		int friendLotteryTenCost = TableManager.GetGambleCostByID((int)LotteryController.LOTTERY_TYPE.FRIEND).TenCost;
		int point = Obj_MyselfPlayer.GetMe().fpoint;
		int t_fn = BestAnswer(point, friendLotteryOneCost, friendLotteryTenCost, LotteryType.Friend);
		if(t_fn > 0){
			transform.FindChild("LotteryInfo/FriendInfo").gameObject.SetActive(true);
			fnum.text = t_fn.ToString();
		}
		else
			transform.FindChild("LotteryInfo/FriendInfo").gameObject.SetActive(false);
	}
	//by cb:因在新手引导里clone按钮的同时也要克隆这个角标，所以把这个移动到了元宝抽奖的button里
	void FreashDiamondNum(){
		UILabel dnum = transform.FindChild("Buttons/DiamondLotteryBtn/DiamondInfo/Label").GetComponent<UILabel>();
		int diamondLotteryOneCost = TableManager.GetGambleCostByID((int)LotteryController.LOTTERY_TYPE.DIAMOND).OneCost;
		int diamondLotteryTenCost = TableManager.GetGambleCostByID((int)LotteryController.LOTTERY_TYPE.DIAMOND).TenCost;
		int point = Obj_MyselfPlayer.GetMe().dollar;
		int t_dn = BestAnswer(point, diamondLotteryOneCost, diamondLotteryTenCost, LotteryType.Diamond);
		if(t_dn > 0){
			transform.FindChild("Buttons/DiamondLotteryBtn/DiamondInfo").gameObject.SetActive(true);
			dnum.text = t_dn.ToString();
			Debug.Log("diamond > 0");
		}
		else{
			transform.FindChild("Buttons/DiamondLotteryBtn/DiamondInfo").gameObject.SetActive(false);
			Debug.Log("diamond = 0");
		}
	}
	
	public int BestAnswer(int point, int one_point, int ten_point, LotteryType type)
	{
		if(point < one_point)
		{
			if (Obj_MyselfPlayer.GetMe().freeCD <= 0 && type == LotteryType.Diamond) //免费抽一次 开启
				return 1;
			return 0;
		}
		int last_point = point % ten_point;
		
		int num = (int)(point/ten_point)*10 + (int)(last_point/one_point);
		if (Obj_MyselfPlayer.GetMe().freeCD <= 0 && type == LotteryType.Diamond) //免费抽一次 开启
			num++;
		return num;
	}
	
	public void BuyGold ()
	{
		NetworkSender.Instance().buyGold(BuySthDone,0);
	}

}
