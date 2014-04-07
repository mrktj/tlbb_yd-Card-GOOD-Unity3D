using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using card.net;
using GCGame.Table;

public class QxzbPvPController : MonoBehaviour {
	
	public GameObject gbRankfirst;
	public GameObject gbRankSecond;
	public GameObject gbRankThird;
	public GameObject heroInfoObj;
	public GameObject UnOpenPromptObj;
	public GameObject rewardPromptObj;
	public GameObject clearCDBtnObj;
	public GameObject heroRewardLabelObj;
	public GameObject leftTimeObj;
	public GameObject top3Obj;
	public GameObject ScrollBarObj;
	public GameObject listObj;
	public UILabel cdTimeLabel;
	public UILabel rewardLabel;
	public UILabel CurHeroRewardLabel;
	private List<GameObject> items = new List<GameObject>();
	private string listItem = "QxzbPvPCardItem";
	public  GameObject iconObj;
	public List<List<PVPPlayerInfo>> top3Infos;
	public List<List<PVPPlayerInfo>> playerInfos;
	
	public GameObject spRankObj;
	
	public UICheckbox[] checkGroupBtn;
	//public UIDraggablePanel dragPannel;
	public GameObject listParent;

	//当前的星级排行
	public int nCurStarRank;
	private const int MaxStarRank = 7;
	private const int MinStarRank = 3;
	private int nCurRankRewardID = 0;
	private MainController mainControl;
	private UILabel timeLabel;
	private bool bFirst = true;
	private bool bLockMessage = false;
	private bool bLockInitMessage = false;
	void Awake()
	{
		bFirst = true;
	}
	
	void OnDisable()
	{
		if(mainControl == null)
		{
			GameObject mainUIlogiObj = GameObject.Find("MainUILogic");
			if(mainUIlogiObj != null)
			{
				MainUILogic mainUILogic = mainUIlogiObj.GetComponent<MainUILogic>();
				if(mainUILogic != null)
				{
					mainControl = mainUILogic.mainController.GetComponent<MainController>();
				}

			}
		}
		
		if(mainControl != null)
		{
			mainControl.showTopBar();
		}
		
		
		resetItems();
		
	}
	
	// Use this for initialization
	void Start () {

		
			
	}
	
	void Update()
	{
		this.FreshTime();
		this.ShowCDAndClearBtn();
	}
	
	void OnEnable()
	{
		Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.QxzbPvP;
		
		if(mainControl == null)
		{			
			GameObject mainUIlogiObj = GameObject.Find("MainUILogic");
			if(mainUIlogiObj != null)
			{
				MainUILogic mainUILogic = mainUIlogiObj.GetComponent<MainUILogic>();
				if(mainUILogic != null)
				{
					mainControl = mainUILogic.mainController.GetComponent<MainController>();
				}

			}
		}
		
		if(mainControl != null)
		{
			mainControl.hideTopBar();
		}
		

		if(top3Infos == null)
		{
			top3Infos = new List<List<PVPPlayerInfo>>();
		}
		
		if(playerInfos == null)
		{
			playerInfos = new List<List<PVPPlayerInfo>>();
		}
		
		//top3Infos.Clear();
		//playerInfos.Clear();
		FreshCheckBtnFunction();
		nCurStarRank = Obj_MyselfPlayer.GetMe().curPvPStar;
		ShowInit(false);
		//ShowAllItems(true);
		//Obj_MyselfPlayer.GetMe().TestSetPvPPlayerDate();
		//RecallGetListFun(true);
		bLockInitMessage = true;
		SendGetQxzbPvPPlayerList();
	}
	
	private void ShowInit(bool bshow)
	{
		//刚进入界面隐藏信息
		if(top3Obj != null)
		{
			top3Obj.SetActive(bshow);
		}
		
		if(heroInfoObj != null)
		{
			heroInfoObj.SetActive(bshow);
		}
		
		if(ScrollBarObj != null)
		{
			ScrollBarObj.SetActive(bshow);
		}
	}
	
	/*
	void OnFreshInfo()
	{
		if(heroInfoObj == null)
		{
			return;
		}
		
		UILabel nameLabel = heroInfoObj.transform.FindChild("Label-Name").GetComponent<UILabel>();
		if(nameLabel != null)
		{
			nameLabel.text = Obj_MyselfPlayer.GetMe().accountName;
		}
		
		UILabel fightLabel = heroInfoObj.transform.FindChild("Label-fight-value").GetComponent<UILabel>();
		if(fightLabel != null)
		{
			//fightLabel.text = Obj_MyselfPlayer.GetMe().GetFightValue();
		}

	}
	*/
	
	private void FreshCheckBtnFunction()
	{
		if(checkGroupBtn == null)
		{
			return;
		}else if(checkGroupBtn.Length < 5)
		{
			return;
		}
		
		checkGroupBtn[0].onStateChange = onStar3StateChange;
		checkGroupBtn[1].onStateChange = onStar4StateChange;
		checkGroupBtn[2].onStateChange = onStar5StateChange;
		checkGroupBtn[3].onStateChange = onStar6StateChange;
		checkGroupBtn[4].onStateChange = onStar7StateChange;
	}
	
	private void onStar3StateChange(bool bstate)
	{
		if(bstate)
		{
			if(bFirst && Obj_MyselfPlayer.GetMe().curPvPStar > 0)
			{
				checkGroupBtn[Obj_MyselfPlayer.GetMe().curPvPStar-3].isChecked = true;
				bFirst = false;
				return;
			}
			
			nCurStarRank = 3;
			if(!bLockInitMessage)
			{
				this.ShowAllItems(true);
			}
			
		}
	}
	
	private void onStar4StateChange(bool bstate)
	{
		
		if(bstate)
		{
			if(bFirst && Obj_MyselfPlayer.GetMe().curPvPStar > 0)
			{
				checkGroupBtn[Obj_MyselfPlayer.GetMe().curPvPStar-3].isChecked = true;
				bFirst = false;
				return;
			}
			
			nCurStarRank = 4;
			if(!bLockInitMessage)
			{
				this.ShowAllItems(true);
			}
		}
	}
	
	private void onStar5StateChange(bool bstate)
	{
		if(bstate)
		{
			if(bFirst && Obj_MyselfPlayer.GetMe().curPvPStar > 0)
			{
				checkGroupBtn[Obj_MyselfPlayer.GetMe().curPvPStar-3].isChecked = true;
				bFirst = false;
				return;
			}
			
			nCurStarRank = 5;
			if(!bLockInitMessage)
			{
				this.ShowAllItems(true);
			}
		}
	}
	
	private void onStar6StateChange(bool bstate)
	{
		if(bstate)
		{
			if(bFirst && Obj_MyselfPlayer.GetMe().curPvPStar > 0)
			{
				checkGroupBtn[Obj_MyselfPlayer.GetMe().curPvPStar-3].isChecked = true;
				bFirst = false;
				return;
			}
			
			nCurStarRank = 6;
			if(!bLockInitMessage)
			{
				this.ShowAllItems(true);
			}
		}
	}
	
	private void onStar7StateChange(bool bstate)
	{
		if(bstate)
		{
			if(bFirst && Obj_MyselfPlayer.GetMe().curPvPStar > 0)
			{
				checkGroupBtn[Obj_MyselfPlayer.GetMe().curPvPStar-3].isChecked = true;
				bFirst = false;
				return;
			}
			
			nCurStarRank = 7;
			if(!bLockInitMessage)
			{
				this.ShowAllItems(true);
			}
		}
	}
	
	
	
	//private void OnActivate(bool bCheck)
	//{
	//	Debug.Log(bCheck);
	//}
	
	/*
	private void SetCheckBtnOn(GameObject btn)
	{
		if(checkGroupBtn != null)
		{
			foreach(GameObject check in checkGroupBtn)
			{
				
				if(check == btn)
				{
					check
				}
				
			}
		}

	}
	*/
	
	private void onClearCD(GameObject objBtn)
	{
		BoxManager.showMessageByID((int)MessageIdEnum.Msg193);
		//BoxManager.getYesButton().name = type.ToString();
		UIEventListener.Get(BoxManager.getYesButton()).onClick += ClearCDComfirm;
	}
	
	private void ClearCDComfirm(GameObject button)
	{
		if(Obj_MyselfPlayer.GetMe().dollar < 20 )
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg11);
			return;
		}
		
		NetworkSender.Instance().SendClearQxzbPvPCD(ReCallClearCD);
	}
	
	private void ReCallClearCD(bool bSuccess)
	{
		NetworkSender.Instance().GetQxzbPvPPlayerInfoList(RecallGetUserInfo);
	}
	
	private void RecallGetUserInfo(bool bSuccess)
	{
		NetworkSender.Instance().GetUserInfo(GetUserInfo);
	}
	//发送获得PVP玩家列表
	private void SendGetQxzbPvPPlayerList()
	{
		bLockMessage = true;
		NetworkSender.Instance().GetQxzbPvPPlayerInfoList(RecallGetListFun);
	}
	
	/*
	public void OnDragFinished (UIDraggablePanel.DragonMode dragonMode)
	{
		if(dragonMode == UIDraggablePanel.DragonMode.Left)
		{
			nCurStarRank++;
			if(nCurStarRank >= MaxStarRank)
			{
				nCurStarRank = MaxStarRank;
				return;
			}
		}
		else if(dragonMode == UIDraggablePanel.DragonMode.Right)
		{
			nCurStarRank++;
			if(nCurStarRank <= MinStarRank)
			{
				nCurStarRank = MinStarRank;
				return;
			}
		}
		else
		{
			return;
		}
		
		this.ShowAllItems(true);
	}
	*/
	
	
	//显示玩家信息
	private void ShowHeroInfo()
	{
		UserCardItem leaderCard = Obj_MyselfPlayer.GetMe().GetQxzbStarLeader(nCurStarRank);
		

		//当前星级没有设置阵型
		Transform heroDesCribTrans = gameObject.transform.FindChild("HeroInfo/heroDescrib");
		Transform lbpromptTrans = gameObject.transform.FindChild("HeroInfo/Label-prompt");
		
		if(IsQxzbOpen())
		{
			
			if(leftTimeObj != null)
			{
				leftTimeObj.SetActive(true);
			}
			
			if(heroDesCribTrans != null)
			{
				//heroDesCribTrans.FindChild("CardIcon/CardIconBtn/ranksp").gameObject.SetActive(true);
				if(spRankObj != null)
				{
					spRankObj.SetActive(true);
				}
				heroDesCribTrans.FindChild("Label-Rank-value").gameObject.SetActive(true);
			}
			
			//沒有leader的時候的顯示(已經開啟了)
			if(leaderCard == null)
			{
				if(heroDesCribTrans != null)
				{
					heroDesCribTrans.gameObject.SetActive(false);
				}
			
				if(lbpromptTrans != null)
				{	
					lbpromptTrans.gameObject.SetActive(true);
				}
				
				
				if(CurHeroRewardLabel != null)
				{

					int nRewardID = this.GetRankRwardIDByStar(nCurStarRank);
					string strReward = this.GetRewardDescrib(nRewardID);
						//string.Format("当前可领奖励: {0}", strReward);
					if(heroRewardLabelObj != null )
					{
						heroRewardLabelObj.SetActive(true);
					}
					if(strReward == "")
					{
						CurHeroRewardLabel.text = "无";
					}
					else
					{
						CurHeroRewardLabel.text = strReward;
					}
						
				}
			}
			else //有leader的時候的顯示(已經開啟了)
			{
				
				if(heroDesCribTrans != null)
				{
					heroDesCribTrans.gameObject.SetActive(true);
				}
			
				if(lbpromptTrans != null)
				{	
					lbpromptTrans.gameObject.SetActive(false);
				}
				
				//獎勵的顯示
				if(CurHeroRewardLabel != null)
				{

					int nRewardID = this.GetRankRwardIDByStar(nCurStarRank);
					string strReward = this.GetRewardDescrib(nRewardID);
						//string.Format("当前可领奖励: {0}", strReward);
					if(heroRewardLabelObj != null )
					{
						heroRewardLabelObj.SetActive(true);
					}
					if(strReward == "")
					{
						CurHeroRewardLabel.text = "无";
					}
					else
					{
						CurHeroRewardLabel.text = strReward;
					}
						
				}
				
			}
		}
		else
		{
			
			if(leftTimeObj != null)
			{
				leftTimeObj.SetActive(false);
			}
			
			//沒有leader的時候的顯示(還沒開啟)
			if(leaderCard == null)
			{
				if(heroDesCribTrans != null)
				{
					heroDesCribTrans.gameObject.SetActive(false);
				}
				
				if(lbpromptTrans != null)
				{
					lbpromptTrans.gameObject.SetActive(false);
				}
				
				if(heroRewardLabelObj != null)
				{
					heroRewardLabelObj.SetActive(false);
				}
			}
			else
			{
				if(heroDesCribTrans != null)
				{
					heroDesCribTrans.gameObject.SetActive(true);
				}
				
				if(Obj_MyselfPlayer.GetMe().heroQxzbPvPRank[nCurStarRank - 3] > 0)
				{
					if(spRankObj != null)
					{
						spRankObj.SetActive(true);
					}
					heroDesCribTrans.FindChild("Label-Rank-value").gameObject.SetActive(true);
				}
				else
				{
					if(spRankObj != null)
					{
						spRankObj.SetActive(false);
					}
					//heroDesCribTrans.FindChild("CardIcon/CardIconBtn/ranksp").gameObject.SetActive(false);
					heroDesCribTrans.FindChild("Label-Rank-value").gameObject.SetActive(false);
				}
				
				
				if(lbpromptTrans != null)
				{
					lbpromptTrans.gameObject.SetActive(false);
				}
				
				if(heroRewardLabelObj != null)
				{
					heroRewardLabelObj.SetActive(false);
				}
			}
			
			/*
			if(lbpromptTrans != null)
			{
				lbpromptTrans.gameObject.SetActive(false);
			}
			

			if(heroRewardLabelObj != null)
			{
				heroRewardLabelObj.SetActive(false);
			}
			*/
		
		}
		
		
		/*
		if(leaderCard != null)
		{
			if(heroDesCribTrans != null)
			{
				heroDesCribTrans.gameObject.SetActive(true);
			}
		}
		else
		{
			if(heroDesCribTrans != null)
			{
				heroDesCribTrans.gameObject.SetActive(false);
			}
		}
		*/
		
		
		
		
		//显示heroInfo
		
		if(leaderCard != null)
		{
			
			
			Tab_Card tabCard = TableManager.GetCardByID(leaderCard.templateID);
			if(tabCard != null)
			{
				//头像
				if(iconObj != null)
				{
					Transform iconTrans = iconObj.transform.FindChild("Sprite-Icon");
					if(iconTrans != null)
					{
						UISprite spIcon = iconTrans.GetComponent<UISprite>();
						if(spIcon != null)
						{
							
							Tab_Appearance appearanceTab = TableManager.GetAppearanceByID(tabCard.Appearance);
							if(appearanceTab != null)
							{
								spIcon.spriteName = appearanceTab.HeadIcon;
							}
						}
					}
				}
				
				
				if(iconObj != null)
				{
					Transform iconOutsideTrans = iconObj.transform.FindChild("sp-Outside");
					if(iconOutsideTrans != null)
					{
						UISprite spOutside = iconOutsideTrans.GetComponent<UISprite>();
						if(spOutside != null)
						{
							spOutside.spriteName = UserCardItem.littleCardBorderName[tabCard.Star];
						}
					}
				
				}
				
				
				//名字
				Transform nameTrans = gameObject.transform.FindChild("HeroInfo/heroDescrib/Label-Name");
				if(nameTrans != null)
				{
					UILabel lbName = nameTrans.GetComponent<UILabel>();
					if(lbName != null)
					{
						lbName.text = Obj_MyselfPlayer.GetMe().accountName;
					}
				}
				
				//战斗力
				Transform fightValueTrans = gameObject.transform.FindChild("HeroInfo/heroDescrib/Label-fight-value");
				if(fightValueTrans != null)
				{
					UILabel lbFightValue = fightValueTrans.GetComponent<UILabel>();
					if(lbFightValue != null)
					{
						lbFightValue.text = this.CountFightByStar(tabCard.Star).ToString();
					}
				}
				
				//排名
				Transform rankValueTrans = gameObject.transform.FindChild("HeroInfo/heroDescrib/Label-Rank-value");
				if(rankValueTrans != null)
				{
					UILabel lbRankValue = rankValueTrans.GetComponent<UILabel>();
					if(lbRankValue != null)
					{
						lbRankValue.text = Obj_MyselfPlayer.GetMe().heroQxzbPvPRank[nCurStarRank-3].ToString();
					}
				}
				
				//头像点击响应函数
				//Transform iconBtnTrans = gameObject.transform.FindChild("HeroInfo/heroDescrib/CardIcon/CardIconBtn");
				if(iconObj != null)
				{
					iconObj.name = leaderCard.cardID.ToString();
					UIEventListener.Get(iconObj).onClick = onClickShowHeroCardInfo;
				}
			}

		}
		
		
		//元宝
		Transform yuanbaoTrans = gameObject.transform.FindChild("HeroInfo/Label-yuanbao-value");
		if(yuanbaoTrans != null)
		{
			UILabel lbyuanbao = yuanbaoTrans.GetComponent<UILabel>();
			if(lbyuanbao != null)
			{
				lbyuanbao.text = Obj_MyselfPlayer.GetMe().dollar.ToString();
			}
		}
		
		//金钱
		Transform moneyTrans = gameObject.transform.FindChild("HeroInfo/Label-money-value");
		if(moneyTrans != null)
		{
			UILabel lbMoney = moneyTrans.GetComponent<UILabel>();
			if(lbMoney != null)
			{
				lbMoney.text = Obj_MyselfPlayer.GetMe().money.ToString();
			}
		}
		
		
		//当前剩余次数
		Transform leftTimeTrans = gameObject.transform.FindChild("HeroInfo/leftTimeObj/Label-LeftTime-Value");
		if(leftTimeTrans != null)
		{
			UILabel lbLeftTime = leftTimeTrans.GetComponent<UILabel>();
			if(lbLeftTime != null)
			{
				lbLeftTime.text = Obj_MyselfPlayer.GetMe().nQxzbFightTime.ToString();
			}
		}
	}
	
	
	//计算战斗力
	private int CountFightByStar(int nStar)
	{
		
		int nowstreng=0;
		UserCardItem[] cardArray = Obj_MyselfPlayer.GetMe().GetBagQxzbCardFightArrayByStar(nStar);
		foreach(UserCardItem card in cardArray)
		{
			if(card != null)
			{
				nowstreng += ((card.GetAttack()+card.GetHp())/100 + card.level*UserCardItem.startFactor[card.quality]);
			}
		}
		
		return nowstreng;
	}
	
	//判断群雄争霸是否开启
	private bool IsQxzbOpen()
	{
		
		//活动没开启时的显示
		long leftEndTime = Obj_MyselfPlayer.GetMe().qxzbEndTime - ((long)Time.time- Obj_MyselfPlayer.GetMe().cursysTime);
		long leftStartTime = Obj_MyselfPlayer.GetMe().qxzbStarTime - ((long)Time.time- Obj_MyselfPlayer.GetMe().cursysTime);
		
		if(leftStartTime > 0)
		{
			return false;
		}
		
		return true;
	}
	
	//玩家自身信息和提醒显示
	private void SetFreshPromptAndHeroInfoShowState(bool bShowHeroInfo)
	{
		Transform heroDesCribTrans = gameObject.transform.FindChild("HeroInfo/heroDescrib");
		Transform lbpromptTrans = gameObject.transform.FindChild("HeroInfo/Label-prompt");
		
		
		if(heroDesCribTrans != null)
		{
			heroDesCribTrans.gameObject.SetActive(bShowHeroInfo);
		}
		
		if(lbpromptTrans != null)
		{
			lbpromptTrans.gameObject.SetActive(!bShowHeroInfo);
		}
		
		if(CurHeroRewardLabel != null)
		{
			if(bShowHeroInfo)
			{
				int nRewardID = this.GetRankRwardIDByStar(nCurStarRank);
				string strReward = this.GetRewardDescrib(nRewardID);
				//string.Format("当前可领奖励: {0}", strReward);
				if(heroRewardLabelObj != null)
				{
					heroRewardLabelObj.SetActive(true);
				}
				if(strReward == "")
				{
					CurHeroRewardLabel.text = "无";
				}
				else
				{
					CurHeroRewardLabel.text = strReward;
				}
				
			}
			else
			{
				if(heroRewardLabelObj != null)
				{
					heroRewardLabelObj.SetActive(false);
				}
				CurHeroRewardLabel.text = "";
			}
			
		}
		
		if(clearCDBtnObj != null)
		{
			clearCDBtnObj.SetActive(bShowHeroInfo);
		}
		
	}
	
	//刷新cd时间 和清楚cd的按钮的显示
	private void ShowCDAndClearBtn()
	{
		long coldTime = Obj_MyselfPlayer.GetMe().coldTime;
		long lastSysTime = Obj_MyselfPlayer.GetMe().curCDsysTime;
		long leftCdTime =  coldTime - ((long)Time.time - lastSysTime);
		
		
		
		if(leftCdTime > 0 )
		{
			
			long secondNum = 1;
			long minNum = 60 * secondNum;
			long hourNum = 60 * minNum;
			
			
	
			long hour = leftCdTime/hourNum;
			long min = (leftCdTime - hourNum*hour)/minNum;
			long second = leftCdTime - hourNum*hour - minNum*min;
			
			if(heroRewardLabelObj != null)
			{
				heroRewardLabelObj.SetActive(false);
			}
			
			if(cdTimeLabel != null)
			{
				cdTimeLabel.gameObject.SetActive(true);
				cdTimeLabel.text = "冷却时间: "+hour+"小时"+min+"分"+second+"秒";
			}
			
			if(clearCDBtnObj != null)
			{
				clearCDBtnObj.SetActive(true);
				clearCDBtnObj.GetComponent<UIImageButton>().enabled = true;
			}
		
		}
		else
		{
			//if(nCurRankRewardID > 0)
			//{
			 
				if(heroRewardLabelObj != null)
				{
					if(heroRewardLabelObj.activeSelf == false)
					{
						if(IsQxzbOpen())
						{
							heroRewardLabelObj.SetActive(true);
						}
						else
						{
							heroRewardLabelObj.SetActive(false);
						}
					}
					
				}
			//}
			//else
			//{
			//	if(heroRewardLabelObj != null)
			//	{
			//		heroRewardLabelObj.SetActive(false);
			//	}
			//}
			
			
			if(cdTimeLabel != null)
			{
				cdTimeLabel.gameObject.SetActive(false);
			}
			
			if(clearCDBtnObj != null)
			{
				clearCDBtnObj.SetActive(false);
				clearCDBtnObj.GetComponent<UIImageButton>().enabled = false;
			}
		}
	}
	
	
	
	private bool CheckIfHasRewardByStar(int nStar)
	{
		if(nStar>7 || nStar<3)
		{
			return false;
		}
		
		if(Obj_MyselfPlayer.GetMe().heroQxzbPvPRewardID[nStar-3] > 0)
		{
			return true;
		}
		
		return false;
		//public int[] heroQxzbPvPRank = new int[5];
		//public int[] heroQxzbPvPRewardID = new int[5];
	}
	
	
	//当前排名的奖励id
	private int GetRankRwardIDByStar(int nstar)
	{
		int curRank = Obj_MyselfPlayer.GetMe().heroQxzbPvPRank[nstar-3];
		int nRewardID = 0;
		int tabNum = TableManager.GetPvpnewReward().Count;
		for(int i = 1; i<= tabNum; i++)
		{
			Tab_PvpnewReward pvpNewRewardTab =  TableManager.GetPvpnewRewardByID(i);
			if(curRank <= pvpNewRewardTab.RankMin
				  && curRank >= pvpNewRewardTab.RankMax)
			{
				nRewardID = i;
				break;
			}
		}
		
		return nRewardID;
	}
	
	private int GetRewardIDByStar(int nStar)
	{
		return Obj_MyselfPlayer.GetMe().heroQxzbPvPRewardID[nStar - 3];
	}
	
	private string GetRewardTypeDescrib(int nRwardType)
	{
		string strRewardType = "";
		switch(nRwardType)
		{
			case 0: strRewardType = "领导力:";break;
			case 1: strRewardType = "元宝:";break;
			case 2: strRewardType = "金币:";break;
			case 3: strRewardType = "强身丹:";break;
			case 4: strRewardType = "大力丸:";break;
		}
		
		return strRewardType;
	}
	
	private string GetRewardDescrib(int nRewardID)
	{
		//int nCurRankRwardID = this.GetRewardIDByStar(nCurStarRank);
		int nCurRankRwardID = nRewardID;
		if(nCurRankRwardID <= 0)
		{
			return "";
		}
		
		string strRewardDescrib = "";
		Tab_PvpnewReward pvpNewReward = TableManager.GetPvpnewRewardByID(nCurRankRwardID);

		if(pvpNewReward.Reward1Num > 0)
		{
			strRewardDescrib += GetRewardTypeDescrib(pvpNewReward.GetRewardbyIndex(0)) +pvpNewReward.Reward1Num.ToString() +" ";
		}
		
		if(pvpNewReward.Reward2Num > 0)
		{
			strRewardDescrib += GetRewardTypeDescrib(pvpNewReward.GetRewardbyIndex(1)) +pvpNewReward.Reward2Num.ToString() +" ";
		}
		
		if(pvpNewReward.Reward3Num > 0)
		{
			strRewardDescrib += GetRewardTypeDescrib(pvpNewReward.GetRewardbyIndex(2)) +pvpNewReward.Reward3Num.ToString() +" ";
		}
		
		if(pvpNewReward.Reward4Num > 0)
		{
			strRewardDescrib += GetRewardTypeDescrib(pvpNewReward.GetRewardbyIndex(3)) +pvpNewReward.Reward4Num.ToString() +" ";
		}
		
		if(pvpNewReward.Reward5Num > 0)
		{
			strRewardDescrib += GetRewardTypeDescrib(pvpNewReward.GetRewardbyIndex(4)) +pvpNewReward.Reward5Num.ToString() +" ";
		}
		
		return strRewardDescrib;
	}
	
	//做领奖提醒的显示
	private void ShowGetRewardPrompt(bool bShow)
	{
		if(UnOpenPromptObj != null
				&& rewardPromptObj != null)
		{
			// 恭喜/n您在上期群雄争霸活动中战绩不凡，获得了{0}！
			Tab_Language tabLanguage = TableManager.GetLanguageByID(100090);
			if(tabLanguage != null 
				  && rewardLabel != null)
			{
				int rewardID = this.GetRewardIDByStar(nCurStarRank);
				string strPromptReward = tabLanguage.Chinese;
				rewardLabel.text = string.Format(strPromptReward, this.GetRewardDescrib(rewardID));
				//rewardLabel.text = "asdasdasdasd\n123123123123";
			}
			
			rewardPromptObj.SetActive(bShow);
			UnOpenPromptObj.SetActive(!bShow);

		}
	}
	
	private void ShowAllItems(bool bUpdate)
	{
		
		Obj_MyselfPlayer.GetMe().curPvPStar = nCurStarRank;
		
		//活动没开启时的显示
		long leftEndTime = Obj_MyselfPlayer.GetMe().qxzbEndTime - ((long)Time.time- Obj_MyselfPlayer.GetMe().cursysTime);
		long leftStartTime = Obj_MyselfPlayer.GetMe().qxzbStarTime - ((long)Time.time- Obj_MyselfPlayer.GetMe().cursysTime);
		
		nCurRankRewardID = this.GetRankRwardIDByStar(nCurStarRank);
		
		//活动还没开启时
		if(leftStartTime > 0)
		{
			
			if(CheckIfHasRewardByStar(nCurStarRank))
			{
				this.ShowGetRewardPrompt(true);
			}
			else
			{
				this.ShowGetRewardPrompt(false);
			}
			
			
			if(listObj != null
				&& ScrollBarObj != null)
			{
				listObj.SetActive(false);
				ScrollBarObj.SetActive(false);
			}
			
			this.ShowHeroInfo();
			
			if(top3Infos.Count > 0)
			{
				this.ShowTop3Items(top3Infos[nCurStarRank-MinStarRank]);
			}
			
		}//活动已开启时
		else
		{
			if(CheckIfHasRewardByStar(nCurStarRank))
			{
				
				if(listObj != null
					&& ScrollBarObj != null)
				{
					listObj.SetActive(false);
					ScrollBarObj.SetActive(false);
				}
				
				this.resetItems();
				this.ShowGetRewardPrompt(true);
			}
			else
			{
				if(UnOpenPromptObj != null)
				{
					UnOpenPromptObj.SetActive(false);
				}
				
				if(rewardPromptObj != null)
				{
					rewardPromptObj.SetActive(false);
				}
			
				if(heroRewardLabelObj != null)
				{
					heroRewardLabelObj.SetActive(true);
				}
				
				
				if(listObj != null
					&& ScrollBarObj != null)
				{
					listObj.SetActive(true);
					ScrollBarObj.SetActive(true);
				}
				
				if(playerInfos.Count > 0 
					 && top3Infos.Count > 0)
				{
					this.ShowItems(playerInfos[nCurStarRank-MinStarRank]);
					this.ShowTop3Items(top3Infos[nCurStarRank-MinStarRank]);
					if(bUpdate)
					{
						this.UpdatePanel();
					}
				}
				
				this.ShowHeroInfo();
			}
			
		}
		
	}
	
	
	//刷新列表显示
	private void UpdatePanel()
	{
		/*
		if(listParent != null)
		{
			UIDraggablePanel dragPanel = listParent.transform.parent.GetComponent<UIDraggablePanel>();
			if(dragPanel != null)
			{
				if(dragPanel.verticalScrollBar != null)
				{
					float a = dragPanel.verticalScrollBar.scrollValue;
				}
				else
				{
					Debug.Log("yun le aaaaaaaaaaaaaaaaaaaaa");
				}
			}
		}
		*/
	    listParent.transform.parent.GetComponent<UIDraggablePanel>().verticalScrollBar.scrollValue = 0;
        listParent.transform.parent.GetComponent<UIDraggablePanel>().UpdateScrollbars(true);
        listParent.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
         
		UIGrid[] grids = listParent.transform.parent.GetComponentsInChildren<UIGrid>();
		
		if(grids != null)
		{
			for(int i=0; i < grids.Length; i++)
			{
				if(grids[i] != null && grids[i].gameObject.activeSelf)
				{
					grids[i].repositionNow = true;
					grids[i].Reposition();
				}
			}
		 }

         listParent.transform.parent.SendMessage("UpdateDrawcalls");
         
	}
		//清理Items
	public void resetItems()
	{
		foreach(GameObject item in items)
		{
            if (item != null)
            {
                //item.transform.parent = null;
                //Destroy(item);
				GameObject fightBtn = item.transform.FindChild("fightBtn").gameObject;
           		fightBtn.SetActive(false);
				CardListItemPool.Instance.DestroyItemAndPushToPool(item, listItem);
            }
		}
		items.Clear();
	}
	
	
	
	//显示
	private void ShowItems(List<PVPPlayerInfo> pvpList)
	{
		this.resetItems();
		foreach(PVPPlayerInfo pvpInfo in pvpList)
		{
			//挑战列表不显示自己
			//if(pvpInfo.nlGUID == Obj_MyselfPlayer.GetMe().accountID)
			//{
			//	continue;
			//}
			
			GameObject newItem = CardListItemPool.Instance.GetListItem(listItem);
			if(listParent != null)
			{
				newItem.transform.parent = listParent.transform;
			}
			
			if(pvpInfo == null)
			{
				BoxManager.showMessageByID((int)MessageIdEnum.Msg61, "pvpInfo == null");
				return;
			}
			
			
	   	    newItem.name = pvpInfo.nlGUID.ToString();
			newItem.transform.localPosition = new Vector3(0, 0, -2);
       		newItem.transform.localScale = new Vector3(1, 1, 1);
			
			
			//Tab_Card tabCard = TableManager.GetCardByID(pvpInfo.nTempleID);
			
			UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
			if(nameLabel != null)
			{
				nameLabel.text = pvpInfo.strName;
			}
			
			UILabel rankLabel = newItem.transform.FindChild("Labels/Label-Rank-Value").GetComponent<UILabel>();
			if(rankLabel != null)
			{
				rankLabel.text = pvpInfo.nRank.ToString();
			}
			
			UILabel fightLabel = newItem.transform.FindChild("Labels/Label-Fight-Value").GetComponent<UILabel>();
			if(fightLabel != null)
			{
				fightLabel.text = pvpInfo.nFight.ToString();
			}
			
			UILabel levelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
			if(levelLabel != null)
			{
				levelLabel.text = pvpInfo.nLev.ToString();
			}
			
			UserCardItem cardLeader = Obj_MyselfPlayer.GetMe().GetQxzbStarLeader(nCurStarRank);
			int nTempleID = 0;
			if(cardLeader != null && Obj_MyselfPlayer.GetMe().accountID == pvpInfo.nlGUID)
			{
				nTempleID = cardLeader.templateID;
			}
			else
			{
				nTempleID = pvpInfo.nTempleID;
			}
			
			Tab_Card tabCard = TableManager.GetCardByID(nTempleID);
			if(tabCard != null)
			{
				Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
				if(tabAppearance != null)
				{
					UISprite spIcon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
					if(spIcon != null)
					{
						spIcon.spriteName = tabAppearance.HeadIcon;
						spIcon.MakePixelPerfect();
					}
				}
				
				UISprite spOutside = newItem.transform.FindChild("CardIcon/CardIconBtn/sp-Outside").GetComponent<UISprite>();
				if(spOutside != null)
				{
					spOutside.spriteName = UserCardItem.littleCardBorderName[tabCard.Star];
					spOutside.MakePixelPerfect();
				}
			}
			
			Transform fightBtnTrans = newItem.transform.FindChild("fightBtn");
			if(fightBtnTrans != null)
			{
				if(pvpInfo.nlGUID == Obj_MyselfPlayer.GetMe().accountID)
				{
					fightBtnTrans.gameObject.SetActive(false);
				}
				else
				{
					fightBtnTrans.gameObject.SetActive(true);
				}
				
				UIImageButton imgBtn =  fightBtnTrans.GetComponent<UIImageButton>();	
				if(imgBtn != null)
				{
					//有冷却时间
					if(Obj_MyselfPlayer.GetMe().coldTime > 0)
					{
						imgBtn.enabled = false;
					}
					else
					{
						imgBtn.enabled = true;
					}
				}

				UIEventListener.Get(fightBtnTrans.gameObject).onClick = ClickFightBtn;
			}
			
			
			Transform playerIconBtn = newItem.transform.FindChild("CardIcon/CardIconBtn");
			if(playerIconBtn != null)
			{
				UIEventListener.Get(playerIconBtn.gameObject).onClick = onClickShowPlayerInfo;
			}
       	 	
			items.Add(newItem);
		}
		
	}
	
	
	//点击领取按钮
	private void ClickGetRewardBtn(GameObject btn)
	{
		Obj_MyselfPlayer.GetMe().nGetRewardCurStar = nCurStarRank;
		NetworkSender.Instance().GetQxzbReward(GetRewardReturn);
	}
	
	private void GetRewardReturn(bool bSucc)
	{
		long leftEndTime = Obj_MyselfPlayer.GetMe().qxzbEndTime - ((long)Time.time- Obj_MyselfPlayer.GetMe().cursysTime);
		long leftStartTime = Obj_MyselfPlayer.GetMe().qxzbStarTime - ((long)Time.time- Obj_MyselfPlayer.GetMe().cursysTime);
		
		if(rewardPromptObj != null
			&&  UnOpenPromptObj != null)
		{
			if(leftEndTime > 0)
			{
				rewardPromptObj.SetActive(false);
				UnOpenPromptObj.SetActive(false);
				SendGetQxzbPvPPlayerList();
			}
			else if(leftStartTime > 0)
			{
				rewardPromptObj.SetActive(false);
				UnOpenPromptObj.SetActive(true);
			}
			
		}
        if (Obj_MyselfPlayer.GetMe().get_result == 0) //领取成功
        {
            Obj_MyselfPlayer.GetMe().heroQxzbPvPRewardID[Obj_MyselfPlayer.GetMe().nGetRewardCurStar - 3] = 0;
            Obj_MyselfPlayer.GetMe().get_result = -1;
        }
		NetworkSender.Instance().GetQxzbPvPPlayerInfoList(GetQxzbDataInfoReturn);
	}
	
	private void GetQxzbDataInfoReturn(bool bsucc)
	{
		NetworkSender.Instance().GetUserInfo(GetBaseDateReturn);
	}
	
	private void  GetBaseDateReturn(bool bsucc)
	{
		BoxManager.showMessageByID((int)MessageIdEnum.Msg239);
		ShowHeroInfo();
	}
	
	private void ClickFightBtn(GameObject btn)
	{
		
		List<UserCardItem> cardList = Obj_MyselfPlayer.GetMe().GetBagCardListByStar(nCurStarRank);
		if(cardList.Count == 0)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg240);
			return;
		}
		
		if(Obj_MyselfPlayer.GetMe().nQxzbFightTime <= 0)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg192);
			return;
		}
		
		long coldTime = Obj_MyselfPlayer.GetMe().coldTime;
		long lastSysTime = Obj_MyselfPlayer.GetMe().curCDsysTime;
		long leftCdTime =  coldTime - ((long)Time.time - lastSysTime);
		
		if(leftCdTime > 0)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg202);
			return;
		}
		
		
		string strName = btn.transform.parent.name;
		long lGuid = long.Parse(strName);
		Obj_MyselfPlayer.GetMe().curPvPStar = nCurStarRank;
		Obj_MyselfPlayer.GetMe().lQxzbChoosePlayerGUID = lGuid;
		Obj_MyselfPlayer.GetMe().enemyGUID = long.Parse(strName);
		
		PVPPlayerInfo playerInfo = this.GetPvPPlayerInfoByGuid(Obj_MyselfPlayer.GetMe().enemyGUID, nCurStarRank);
		if(playerInfo != null)
		{
			Obj_MyselfPlayer.GetMe().enemyName = playerInfo.strName;
		}
		
		if(mainControl != null)
		{
			mainControl.OpenQxzbPvPBattleBefore();
		}
		
	}
	
	
	private PVPPlayerInfo GetPvPPlayerInfoByGuid(long lGuid, int nStar)
	{
		List<PVPPlayerInfo[]> CardList = Obj_MyselfPlayer.GetMe().qxzbPvPCardList;
		
		PVPPlayerInfo pinfo = null;
		foreach(PVPPlayerInfo[] infoArray in CardList)
		{
			foreach(PVPPlayerInfo playerInfo in infoArray)
			{
				if(playerInfo != null && lGuid == playerInfo.nlGUID)
				{
					Tab_Card cardTab = TableManager.GetCardByID(playerInfo.nTempleID);
					if(cardTab != null 
						 && cardTab.Star == nStar)
					{
						return playerInfo;
					}
				}
			}
		}
		
		return null;
	}
	
	private void ShowTop3Items(List<PVPPlayerInfo> pvptop3List)
	{
		if(pvptop3List == null)
		{
			return;
		}
		
		if(pvptop3List.Count <3)
		{
			return;
		}
		
		this.SetTop3Item(gbRankfirst, pvptop3List[0]);
		this.SetTop3Item(gbRankSecond, pvptop3List[1]);
		this.SetTop3Item(gbRankThird, pvptop3List[2]);
	}
	
	private void FreshTime()
	{
		long passTime = (long)Time.time - Obj_MyselfPlayer.GetMe().cursysTime;
		if(passTime > 0)
		{
			long leftTime = 0;
			if(Obj_MyselfPlayer.GetMe().qxzbStarTime > 0)
			{
				leftTime = Obj_MyselfPlayer.GetMe().qxzbStarTime - passTime;
				
					
				if(leftTimeObj != null)
				{
					leftTimeObj.SetActive(false);
				}

			}
			else if(Obj_MyselfPlayer.GetMe().qxzbEndTime > 0)
			{
				leftTime = Obj_MyselfPlayer.GetMe().qxzbEndTime - passTime;
				
				if(leftTimeObj != null)
				{
					leftTimeObj.SetActive(true);
				}
			}
			
			if(leftTime < 0)
			{
				leftTime = 0;
				if(!bLockMessage)
				{
					bLockMessage = true;
					//TestRecallGetListFun(true);
					SendGetQxzbPvPPlayerList();
				}
				
			}
				
			ShowTime(leftTime);
		}
	}
	
	//活动开启时间显示
	private void ShowTime(long lTime)
	{
		
		long secondNum = 1;
		long minNum = 60 * secondNum;
		long hourNum = 60 * minNum;
		long dayNum = 24 * hourNum;
		
		
		long day = lTime/dayNum;
		long hour = (lTime - dayNum*day)/hourNum;
		long min = (lTime - dayNum*day - hourNum*hour)/minNum;
		long second = lTime - dayNum*day - hourNum*hour - minNum*min;
		
		if(timeLabel == null)
		{
			timeLabel = transform.FindChild("Top3Info/Label-Time").GetComponent<UILabel>();
		}
		
		
		if(timeLabel != null && Obj_MyselfPlayer.GetMe().qxzbStarTime > 0)
		{
			timeLabel.text = "距离活动开启: "+day+"天"+hour+"小时"+min+"分"+second+"秒";
		}
		else if(timeLabel != null && Obj_MyselfPlayer.GetMe().qxzbEndTime > 0)
		{
			timeLabel.text = "距离活动结束: "+day+"天"+hour+"小时"+min+"分"+second+"秒";
		}
		else
		{
			timeLabel.text = "";
		}
		
		//long time = Obj_MyselfPlayer.GetMe().
	}
	
	private void  SetTop3Item(GameObject gbTop, PVPPlayerInfo playerInfo)
	{
		if(gbTop == null || playerInfo == null)
		{
			return;
		}
		
		
		//显示的信息非玩家自己
		if(playerInfo.nlGUID != Obj_MyselfPlayer.GetMe().accountID)
		{
			gbTop.name = playerInfo.nlGUID.ToString();
			
			Tab_Card tabCard = TableManager.GetCardByID(playerInfo.nTempleID);
			if(tabCard != null)
			{
				Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
				if(tabAppearance != null)
				{
					UISprite spIcon = gbTop.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
					if(spIcon != null)
					{
						spIcon.gameObject.SetActive(true);
						spIcon.spriteName = tabAppearance.HeadIcon;
						spIcon.MakePixelPerfect();
					}
				}
				
				UILabel labName = gbTop.transform.FindChild("Label").GetComponent<UILabel>();
				if(labName != null)
				{
					labName.text = playerInfo.strName;
				}
				
				UISprite spOutside = gbTop.transform.FindChild("CardIcon/CardIconBtn/sp-Outside").GetComponent<UISprite>();
				if(spOutside != null)
				{
					spOutside.spriteName = UserCardItem.littleCardBorderName[tabCard.Star];
					spOutside.MakePixelPerfect();
				}
				
			}
			
			Transform playerIconBtn = gbTop.transform.FindChild("CardIcon/CardIconBtn");
			if(playerIconBtn != null)
			{
				UIEventListener.Get(playerIconBtn.gameObject).onClick = onClickShowPlayerInfo;
			}
		}
		else//显示玩家自己
		{
			gbTop.name = Obj_MyselfPlayer.GetMe().accountID.ToString();
			UserCardItem leaderCard = Obj_MyselfPlayer.GetMe().GetQxzbStarLeader(nCurStarRank);
			if(leaderCard != null)
			{
				Tab_Card tabCard = TableManager.GetCardByID(leaderCard.templateID);
				if(tabCard != null)
				{
					Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
					if(tabAppearance != null)
					{
						UISprite spIcon = gbTop.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
						if(spIcon != null)
						{
							spIcon.gameObject.SetActive(true);
							spIcon.spriteName = tabAppearance.HeadIcon;
							spIcon.MakePixelPerfect();
						}
					}
					
					UILabel labName = gbTop.transform.FindChild("Label").GetComponent<UILabel>();
					if(labName != null)
					{
						labName.text = Obj_MyselfPlayer.GetMe().accountName;
					}
					
					UISprite spOutside = gbTop.transform.FindChild("CardIcon/CardIconBtn/sp-Outside").GetComponent<UISprite>();
					if(spOutside != null)
					{
						spOutside.spriteName = UserCardItem.littleCardBorderName[tabCard.Star];
						spOutside.MakePixelPerfect();
					}
					
				}
				
				
				Transform playerIconBtn = gbTop.transform.FindChild("CardIcon/CardIconBtn");
				if(playerIconBtn != null)
				{
					UIEventListener.Get(playerIconBtn.gameObject).onClick = OnClickShowTop3HeroCardInfo;
				}
				/*
				if(gbTop != null)
				{
					UIEventListener.Get(gbTop).onClick = onClickShowHeroCardInfo;
				}
				*/
			}
			
		}
		
		
	}
	
	//刚进界面的时候显示的
	private void enterShowItem()
	{
		ShowInit(true);
		/*
			    && playerInfos.Count >0
			      && top3Infos.Count >0)
			      */
		if(playerInfos != null 
			 && top3Infos != null)
		{
			checkGroupBtn[Obj_MyselfPlayer.GetMe().curPvPStar - 3].isChecked = true;
			nCurStarRank = Obj_MyselfPlayer.GetMe().curPvPStar;
			nCurRankRewardID = this.GetRankRwardIDByStar(nCurStarRank);
			this.ShowAllItems(true);
		}
	}
	
	private void CountTop3AndPlayerInfos()
	{
		if(top3Infos != null)
		{
			top3Infos.Clear();
		}
		
		if(playerInfos != null)
		{
			playerInfos.Clear();
		}
		
		List<PVPPlayerInfo[]> CardList = Obj_MyselfPlayer.GetMe().qxzbPvPCardList;
		foreach(PVPPlayerInfo[] playerInfoArray in CardList)
		{
			if(playerInfoArray != null)
			{
				List<PVPPlayerInfo> pvpTop3 = new List<PVPPlayerInfo>();
				List<PVPPlayerInfo> pvpPlayerList = new List<PVPPlayerInfo>();
				foreach(PVPPlayerInfo playerInfo in playerInfoArray)
				{
					if(playerInfo != null)
					{
						if(playerInfo.nRank <= 3
							 && playerInfo.nRank >= 1 && !CheckIfContainPlayerInfo(pvpTop3,playerInfo))
						{
							pvpTop3.Add(playerInfo);
						}
						else
						{
							pvpPlayerList.Add(playerInfo);
						}
					}
				}
				
				pvpTop3.Sort(ComparePvP);
				pvpPlayerList.Sort(ComparePvP);
				
				top3Infos.Add(pvpTop3);
				playerInfos.Add(pvpPlayerList);
				
				//top3Infos.Sort(ComparePvP);
				//playerInfos.Sort(ComparePvP);
			}
		}
	}
	
	private void TestRecallGetListFun(bool bSuccess)
	{
		Obj_MyselfPlayer.GetMe().qxzbStarTime = 1000;
		Obj_MyselfPlayer.GetMe().qxzbEndTime = 0;
		Obj_MyselfPlayer.GetMe().cursysTime = (long)Time.time;
		
		bLockMessage = false;
		if(!bSuccess)
		{
			return;
		}
		
		Obj_MyselfPlayer.GetMe().qxzbPvPCardList.Clear();
		this.CountTop3AndPlayerInfos();
		
		
		if(Obj_MyselfPlayer.GetMe().nQxzbIfShowFightReward)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg230, Obj_MyselfPlayer.GetMe().nQxzbMoney.ToString());
			Obj_MyselfPlayer.GetMe().nQxzbIfShowFightReward = false;
			//NetworkSender.Instance().GetUserInfo(GetUserInfo);
			
		}
		this.enterShowItem();
	}
	
	private void RecallGetListFun(bool bSuccess)
	{
		
		bLockInitMessage = false;
		bLockMessage = false;
		if(!bSuccess)
		{
			return;
		}
		
		this.CountTop3AndPlayerInfos();
		
		
		if(Obj_MyselfPlayer.GetMe().nQxzbIfShowFightReward)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg230, Obj_MyselfPlayer.GetMe().nQxzbMoney.ToString());
			UIEventListener.Get(BoxManager.getYesButton()).onClick = FreshUserInfo;
			Obj_MyselfPlayer.GetMe().nQxzbIfShowFightReward = false;
			//NetworkSender.Instance().GetUserInfo(GetUserInfo);
			
		}
		this.enterShowItem();
	}
	
	private void FreshUserInfo(GameObject btn)
	{
		NetworkSender.Instance().GetUserInfo(GetUserInfo);
		Debug.Log("Ok  Beng a a a a a a aa ");
	}
	
	private void GetUserInfo(bool bsucc)
	{
		//刷新信息
		ShowHeroInfo();
	}
	
	
	
	static public int ComparePvP(PVPPlayerInfo playerInfoA, PVPPlayerInfo playerInfoB)
	{
        return -1 * playerInfoB.nRank.CompareTo(playerInfoA.nRank);
	}
	
	
	//检查list里是否有相同排名的玩家信息
	private bool CheckIfContainPlayerInfo(List<PVPPlayerInfo> pvpList, PVPPlayerInfo playerInfo)
	{
		if(playerInfos == null || pvpList == null)
		{
			return false;
		}
		
		foreach(PVPPlayerInfo info in pvpList)
		{
			if(info.nRank == playerInfo.nRank)
			{
				return true;
			}
		}
		
		return false;
	}
	
	private void OnClickShowTop3HeroCardInfo(GameObject btnObj)
	{
		UserCardItem leaderCard = Obj_MyselfPlayer.GetMe().GetQxzbStarLeader(nCurStarRank);
		if(leaderCard != null)
		{
			BoxManager.showCardInfoMessage(leaderCard);
		}
	}
	
	private void onClickShowHeroCardInfo(GameObject btnObj)
	{
		long nGuid = long.Parse(btnObj.name);
		UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(nGuid);
		if(card != null)
		{
			BoxManager.showCardInfoMessage(card);
		}
	}
	
	private void OnOpenQxzbInstructionWindow()
	{
		mainControl.OpenQxzbInstruciton();
	}
	
	private void OnOpenQxzbRewardInstructionWindow()
	{
		mainControl.OpenQxzbRewardInstruciton();
	}
	
	private void onClickShowPlayerInfo(GameObject btnObj)
	{
		
		long nGuid = long.Parse(btnObj.transform.parent.parent.name);
		PVPPlayerInfo playerInfo = this.GetPvPPlayerInfoByGuid(nGuid, nCurStarRank);
		
		UserCardItem cardItem = Obj_MyselfPlayer.GetMe().GetQxzbStarLeader(nCurStarRank);
		if(cardItem != null && Obj_MyselfPlayer.GetMe().accountID == nGuid)
		{
			BoxManager.showCardInfoMessage(cardItem);
		}
		else if(playerInfo != null )
		{
			UserCardItem card = new UserCardItem();
		    card.cardID = playerInfo.nlGUID;
		    card.templateID = playerInfo.nTempleID;
		    card.level = playerInfo.nLev;
		    card.skillLevel = playerInfo.skill_level;
		    card.addQualityAtt = playerInfo.add_quality_att;
		    card.addQualityHp = playerInfo.add_quality_hp;
		    card.skillStudyId = playerInfo.studySkillId;
		    card.skillStudyLev = playerInfo.studySkillLev;
		    BoxManager.showCardInfoMessage(card);
		}

	}
	
	
}
