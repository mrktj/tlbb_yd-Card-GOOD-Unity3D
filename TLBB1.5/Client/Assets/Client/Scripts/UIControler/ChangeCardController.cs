using UnityEngine;
using System.Collections;
using card.net;
using Games.CharacterLogic;
using xjgame.message;
using Module.Log;
using GCGame.Table;
using Games.LogicObject;

public class ChangeCardController : MonoBehaviour {
	
	public UIGrid grid;
	private string changeCardItem = "ChangeCardItem";
	public UILabel timerLabel;
	private string[] changeTypeStr = {"{0}元宝","{0}金币","随机锋芒卡牌","随机凌云卡牌","随机潜龙卡牌","随机至尊卡牌"};
	private MainUILogic mainLogic;
	private MainController mainController;
	private bool isBlock = false; //当客户端检测活动关闭,要请求服务器刷新状态,此时要锁住发送请求,防止请求重复发送
	public GameObject intro;
	private GameObject lastClicked;
	public UIImageButton changeConfirm;
	private bool hasCard = false;
	public GameObject timesPrompt;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable(){
		
		if(mainLogic == null)
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		if(mainController == null)
			mainController = GameObject.Find("MainController").GetComponent<MainController>();
		
		mainController.ShowActivityTopUI(ActivityType.E_ACTIVITY_TYPE_CHANGE_CARD);
		mainLogic.SetMainUIBottomBarActive(true);
		
		lastClicked = null;
		intro.SetActive(false);
		changeConfirm.isEnabled = false;
		changeConfirm.transform.FindChild("Sprite").GetComponent<UISprite>().spriteName = "xianrendian_grey";
		timesPrompt.SetActive(false);
		isBlock = false;
		int count = Obj_MyselfPlayer.GetMe().changeCardInfo.Count;
		double endTimer = Obj_MyselfPlayer.GetMe().changeCardTimer;
		LogModule.DebugLog("ChangeCard Timer = " + endTimer);
		LogModule.DebugLog("ChangeCard Count = " + count);
		if (endTimer < 0)
		{
			mainLogic.OnGGLWindow();
			return;
		}
		int days =  (int)(endTimer / 3600 / 24);
		int hour = (int) (endTimer / 3600 - days * 24);
		int minute = (int)((endTimer % 3600) / 60);
		timerLabel.text = string.Format("活动倒计时:{0}天{1}小时{2}分",days,hour,minute);
		int childCount = grid.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
        grid.GetComponent<UIGrid>().repositionNow = true;
		for(int i = 0; i < count; i++)
		{
			GameObject cardItem = ResourceManager.Instance.loadWidget(changeCardItem);
			cardItem.transform.FindChild("bg").gameObject.GetComponent<UISprite>().spriteName = "liebiao_beijing_1";
			cardItem.name = (i+1).ToString();
			cardItem.transform.parent = grid.transform;
			cardItem.transform.localScale = new Vector3(1, 1, 1);
			UIEventListener.Get(cardItem).onClick += ClickAtItem;
			ChangeCardInfo info = Obj_MyselfPlayer.GetMe().changeCardInfo[i];
			int[] changeType = {info.ChangeType_1,info.ChangeType_2,info.ChangeType_3};
			int[] changeNum = {info.ChangeNum_1,info.ChangeNum_2,info.ChangeNum_3};
			LogModule.DebugLog("--------------------------------------------------");
			LogModule.DebugLog("[ " + (i+1) + " ] - > changeType_1 = "  + info.ChangeType_1 );
			LogModule.DebugLog("[ " + (i+1) + " ] - > changeNum_1  = "  + info.ChangeNum_1 );
			LogModule.DebugLog("[ " + (i+1) + " ] - > changeType_2 = "  + info.ChangeType_2 );
			LogModule.DebugLog("[ " + (i+1) + " ] - > changeNum_2  = "  + info.ChangeNum_2 );
			LogModule.DebugLog("[ " + (i+1) + " ] - > changeType_3 = "  + info.ChangeType_3 );
			LogModule.DebugLog("[ " + (i+1) + " ] - > changeNum_3  = "  + info.ChangeNum_3 );
			LogModule.DebugLog("[ " + (i+1) + " ] - > resultID     = "  + info.ResultID );
			LogModule.DebugLog("[ " + (i+1) + " ] - > cardInfoID   = "  + info.CardInfoID );
			LogModule.DebugLog("[ " + (i+1) + " ] - > times        = "  + info.Times );
			LogModule.DebugLog("--------------------------------------------------");
//			UILabel times = cardItem.transform.FindChild("times").GetComponent<UILabel>();
//			times.text = string.Format("剩余:{0}次",info.Times);
			for (int m = 1; m <= 3; m++)
			{
				LogModule.DebugLog("ChangeCard");
				GameObject item = cardItem.transform.FindChild("item"+m).gameObject;
				UILabel label = item.transform.FindChild("Label").GetComponent<UILabel>();
				if (changeType[m-1] > 2) //换卡条件为: 卡牌
				{   
					string cardName = TableManager.GetCardByID(changeType[m-1]).Note;
					label.text = string.Format(cardName + "x" + changeNum[m-1]);
					UISprite cardIcon = item.transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
					string atlasName = TableManager.GetAppearanceByID(TableManager.GetCardByID(changeType[m-1]).Appearance).HeadIcon;
					Vector3 oldScale = cardIcon.transform.localScale;
        			AtlasManager.Instance.setHeadName(cardIcon, atlasName);
        			cardIcon.transform.localScale = oldScale;
					
					UISprite icon_bg = item.transform.FindChild("Sprite-BG").GetComponent<UISprite>();
           	 		UISprite icon_border = item.transform.FindChild("Sprite").GetComponent<UISprite>();
            		int icon_star = TableManager.GetCardByID(changeType[m-1]).Star;
            		icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
            		icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
				}
				else
				{
					string str = changeTypeStr[changeType[m-1]-1];
					label.text = string.Format(str,changeNum[m-1]);
					UISprite cardIcon = item.transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
					string name = "jinbi";
					if (changeType[m-1] == 1) //元宝
						name = "yuanbao";
        			cardIcon.spriteName = name;
//        			cardIcon.transform.localScale = new Vector3(82, 82, 1);
					
					UISprite icon_bg = item.transform.FindChild("Sprite-BG").GetComponent<UISprite>();
           	 		UISprite icon_border = item.transform.FindChild("Sprite").GetComponent<UISprite>();
            		icon_bg.spriteName = UserCardItem.littleCardFrameName[7];
            		icon_border.spriteName = UserCardItem.littleCardBorderName[7];
				}
			}
			if (info.ResultID >= 5)
				info.ResultID = 4;
			GameObject item4 = cardItem.transform.FindChild("item4").gameObject;
			UISprite icon = item4.transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
			icon.spriteName = "cardalbum_" + info.ResultID;
			item4.transform.FindChild("Label").GetComponent<UILabel>().text = changeTypeStr[info.ResultID+1];
		}
	}
	
	void Update ()
	{
		double timer = Obj_MyselfPlayer.GetMe().changeCardTimer;
//		Debug.LogError("Timer = " + timer);
		if(timer > 0)
		{
			int days =  (int)(timer / 3600 / 24);
			int hour = (int) (timer / 3600 - days * 24);
			int minute = (int)((timer % 3600) / 60);
			timerLabel.text = string.Format("活动倒计时:{0}天{1}小时{2}分",days,hour,minute);
		}
		else if (!isBlock)
		{
			isBlock = true;
			NetworkSender.Instance().AskActivity(RefrshActivityState);
		}
	}
	
	private void RefrshActivityState(bool isSuccess)
	{
		if (Obj_MyselfPlayer.GetMe().isChangeCardOpen)
			mainLogic.OnChangeCardContoller();
		else
			BoxManager.showMessageByID((int)MessageIdEnum.Msg208);
			UIEventListener.Get(BoxManager.buttonYes).onClick += CloseChangeCard;
	}
	private void CloseChangeCard(GameObject button)
	{
		mainLogic.OnGGLWindow();
	}
	
	public void ChangeBtnPress()
	{
		hasCard = false;
		if (lastClicked == null)
			return;
		if (Obj_MyselfPlayer.GetMe().changeCardTimer <= 0)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg208);
			UIEventListener.Get(BoxManager.buttonYes).onClick += CloseChangeCard;
			return;
		}
		ChangeCardInfo info = Obj_MyselfPlayer.GetMe().changeCardInfo[int.Parse(lastClicked.name)-1];
		int times = info.Times;
		if (times <= 0)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg204);
			return;
		}
		int[] type = {info.ChangeType_1,info.ChangeType_2,info.ChangeType_3};
		int[] count = {info.ChangeNum_1,info.ChangeNum_2,info.ChangeNum_3};
		for (int i = 0; i < 3; i++)
		{
			if (type[i] == 1) //元宝
			{
				if (Obj_MyselfPlayer.GetMe().dollar < count[i])
				{
					BoxManager.showMessageByID((int)MessageIdEnum.Msg52);
					return;
				}
			}
			else if (type[i] == 2) //金币
			{
				if (Obj_MyselfPlayer.GetMe().money < count[i])
				{
					NetworkSender.Instance().buyGold(BuyGoldFinish,1);
					return;
				}
			}
			else if (type[i] > 2) //卡牌
			{
				hasCard = true;
				int myCard = 0;
				int myProtect = 0;
				int myBattle = 0;
				int myNewPvPBattle = 0;
				foreach(UserCardItem item in Obj_MyselfPlayer.GetMe().cardBagList)
				{
					if (item.templateID == type[i])
					{
						myCard++;
						if (item.fightIndex != 0)
							myNewPvPBattle++;
						if (item.IsInFightArray())
							myBattle++;
						if (item.isProtected)
							myProtect++;
					}
				}
				if (myCard < count[i]) //缺少必要的卡
				{
					BoxManager.showMessageByID((int)MessageIdEnum.Msg212,TableManager.GetCardByID(type[i]).Star.ToString(),TableManager.GetCardByID(type[i]).Note);
					return;
				}
				if (myProtect > 0 && (( myCard - myProtect) < count[i])) //消耗卡牌中包含保护卡牌
				{
					BoxManager.showMessageByID((int)MessageIdEnum.Msg209);
					return;
				}
				if (myBattle > 0 && (myCard - myBattle - myProtect) < count[i]) //消耗卡牌中包含上阵卡牌
				{
//					Obj_MyselfPlayer.GetMe().materialChangeCard = 
					BoxManager.showMessageByID((int)MessageIdEnum.Msg210);
					return;
				}
				if (myCard > count[i]) //已有卡牌多于消耗卡牌
				{
					BoxManager.showMessageByID((int)MessageIdEnum.Msg211,TableManager.GetCardByID(type[i]).Star.ToString(),TableManager.GetCardByID(type[i]).Note);
					UIEventListener.Get(BoxManager.buttonYes).onClick += GoChangeCard;
					return;
				}
			}
		}
		GoChangeCard(null);
	}
	
	public void GoChangeCard(GameObject button)
	{
		if (lastClicked == null)
			return;
		ChangeCardInfo info = Obj_MyselfPlayer.GetMe().changeCardInfo[int.Parse(lastClicked.name)-1];
		int[] type = {info.ChangeType_1,info.ChangeType_2,info.ChangeType_3};
		int[] count = {info.ChangeNum_1,info.ChangeNum_2,info.ChangeNum_3};
		int[,] result = new int[3,100];
		for (int i = 0; i < 3; i++)
		{
			if (type[i] == 1 || type[i] == 2) //条件为 元宝 或 金币
			{
				result[i,0] = count[i];
				result[i,1] = -1;

			}
			else if (type[i] > 2) //如果是卡牌
			{
				int n = 0;
				for (n = 0; n < count[i]; n++)
				{
					int x = 0;
					for (int m = 0; m < Obj_MyselfPlayer.GetMe().cardBagList.Count; m++)
					{
						if (Obj_MyselfPlayer.GetMe().cardBagList[m].templateID == type[i])
						{
							result[i,x] =  (int)Obj_MyselfPlayer.GetMe().cardBagList[m].cardID;
							x++;
						}
					}
				}
				result[i,n+1] = -1;
			}
		}

		
		NetworkSender.Instance().RequestChangeCard(ChangeCardDone,info.CardInfoID,hasCard,result);
	}
	
	public void ChangeCardDone(bool isSuccess)
	{
		if (isSuccess)
		{
			LogModule.DebugLog("Change Card Done Success!");
		}
	}
	
	public void BuyGoldFinish(bool isSucess)
	{
		if (isSucess)
		{
			mainLogic.GetComponent<MainUILogic>().SendMessage("refreshTopBar");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
			Debug.Log("Buy Gold Finish");
		}
		else
			Debug.LogError("Buy Gold Error");
	}
	
	public void OnCardIntroPress()
	{
		intro.SetActive(true);
	}
	
	public void ReturnPress()
	{
		intro.SetActive(false);
	}
	
	public void ClickAtItem(GameObject item)
	{
		if (lastClicked != null)
			lastClicked.transform.FindChild("bg").GetComponent<UISprite>().spriteName = "liebiao_beijing_1";
		lastClicked = item;
		timesPrompt.SetActive(true);
		ChangeCardInfo info = Obj_MyselfPlayer.GetMe().changeCardInfo[int.Parse(lastClicked.name)-1];
		timesPrompt.transform.FindChild("Label").GetComponent<UILabel>().text = info.Times.ToString();
		changeConfirm.isEnabled = true;
		changeConfirm.transform.FindChild("Sprite").GetComponent<UISprite>().spriteName = "xianrendian";
		item.transform.FindChild("bg").GetComponent<UISprite>().spriteName = "liebiao_beijing_2";
	}
	
}
