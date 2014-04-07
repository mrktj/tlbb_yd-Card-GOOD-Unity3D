using UnityEngine;
using System.Collections;
using card.net;
using Games.CharacterLogic;
using GCGame.Table;
using Games.LogicObject;

public class GGLController : MonoBehaviour {
	
	public GameObject[] carditems;
	public int[] cardIDs = new int[9];
	public bool bCurtouchFirsr = true;
	public UILabel GGLFreeTime;   //免费次数
	public UILabel YuanBao;      //元宝
	public UILabel MoneyText;      //金钱
	public Animation effect01;
	public Animation effect02;
	public UISprite cardAtlasSp;
	public UISprite NewUIAtlasSp;
	private bool bLock = false;  //光效提示的时候锁了
	//private bool bMessageLock = false;  //消息通讯的时候锁住了
	
	private GameObject  curTouchCard;   //当前点击的卡牌
	private int nCountShowCardNum = 0;  //记录已经刮开了几张卡
	private MainUILogic mainLogic = null;
    private MainController mainController = null;
	private bool bFirstEnter = true;
	private bool bThisTimeFree = true;
	private bool bTodayTip = true; //判断今天是否要有 话费50元宝提示
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnEnable()
	{
        if (mainLogic == null)
        {
            mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
        }

        if (mainController == null)
        {
            mainController = GameObject.Find("MainController").GetComponent<MainController>();
        }

        mainController.ShowActivityTopUI(ActivityType.E_ACTIVITY_TYPE_GGL);
		this.FreshGGLTimesAndYuanBao();
		
		if(bFirstEnter)
		{
			this.ResetCard();
			bFirstEnter = false;
		}
		
		if(Obj_MyselfPlayer.GetMe().GGLTimes == 0)
		{
			bTodayTip = true;
		}

	}
	
	
	void OnDisable()
	{
		 if (mainController != null)
        {
            mainController.ShowtopInfo();
        }
		
		//NetworkSender.Instance().GetUserInfo(UpdateMainControllerInfo);
	}
	
	
	//更新主界面
	void UpdateMainControllerInfo(bool bSucc)
	{
		 if (mainController != null)
        {
			this.FreshGGLTimesAndYuanBao();
            mainController.updateUserInfo();
        }
	}
	

	
	//刷新刮刮乐免费次数 和 元宝、金钱个数
	void FreshGGLTimesAndYuanBao()
	{
		if(Obj_MyselfPlayer.GetMe().GGLTimes == 0)
		{
			GGLFreeTime.text = "1";
		}
		else
		{
			GGLFreeTime.text = "0";
		}
		YuanBao.text = Obj_MyselfPlayer.GetMe().dollar.ToString();
		MoneyText.text = Obj_MyselfPlayer.GetMe().money.ToString();
	}
	
	void ResetCard()
	{
		for(int i=0; i<9; i++)
		{
			cardIDs[i] = -1;
			carditems[i].transform.FindChild("Mask").gameObject.SetActive(true); 	//初始化把卡牌都隐藏起来
			carditems[i].transform.FindChild("Label").gameObject.SetActive(false);	//初始化把字符都隐藏起来
			carditems[i].transform.FindChild("NumBg").gameObject.SetActive(false);	//初始化把数字底图
		}
		bLock = false;
		//bMessageLock = false;
		bCurtouchFirsr = true;
		nCountShowCardNum = 0;
	}
	
	bool CheckIfCardsAreUnopen()
	{
		
		foreach(var cardItem in carditems)
		{
			if(ChechIFCardOpen(cardItem))
			{
				return false;
			}
		}
		
		return true;
	}
	
	//相应刮卡点击事件
	void OnGGLClick(GameObject cardBtn)
	{
		if(bLock)
		{
			return;
		}
		
		//点击单个卡牌时判断是否已经翻开可点
		if(nCountShowCardNum < 9 && ChechIFCardOpen(cardBtn))
		{
			return;
		}
		
		//全部翻开时，点击重置
		if(nCountShowCardNum >= 9)
		{
			this.ResetCard();
			return;
		}
		
		curTouchCard = cardBtn;
		
		//刚开始时的提示
		if(!CheckIfCanFree() && CheckIfCardsAreUnopen())
		{
			if(bTodayTip)
			{
				int cost = TableManager.GetScratchCostByID(2).Cost;
				BoxManager.showMessageByID((int)MessageIdEnum.Msg177, cost.ToString());
           		UIEventListener.Get(BoxManager.getYesButton()).onClick += processTouchCard;
			
				return;
			}
			

		}
		
		processTouchCard(curTouchCard);
	}
	
	//刮刮乐 over
	void OnGGLTouchOver(GameObject cardBtn)
	{
		OnGGLClick(cardBtn);
	}
	
	//判断卡是否被刮开
	bool ChechIFCardOpen(GameObject cardItem)
	{
		return !cardItem.transform.FindChild("Mask").gameObject.activeSelf;
	}
	
	//
	void YuanBaoPrompt(GameObject btn)
	{
		mainLogic.OnPurchaseWindow();
	}
	
	
	//判断是否可以免费抽奖
	bool CheckIfCanFree()
	{
		if(Obj_MyselfPlayer.GetMe().GGLTimes == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	
	void processTouchCard(GameObject cardObj)
	{
		
		if(!CheckIfCanFree() && CheckIfCardsAreUnopen())
		{
			if(bTodayTip)
			{
				bTodayTip = false;
			}
		}
		
		int cost = TableManager.GetScratchCostByID(2).Cost;
		//元宝不足
		if(Obj_MyselfPlayer.GetMe().dollar < cost && !CheckIfCanFree() && CheckIfCardsAreUnopen())
		{

				BoxManager.showMessageByID((int)MessageIdEnum.Msg178);
       	 		UIEventListener.Get(BoxManager.getYesButton()).onClick += YuanBaoPrompt;
				return;
			
			
		}
		
		if(CheckIfCardsAreUnopen())
		{
			NetworkSender.Instance().RequestGuaGuaLe(OnMessageGGLResultReturn);
			//bMessageLock = true;

			bThisTimeFree = CheckIfCanFree();

			//NetworkSender.Instance().GetUserInfo(OnMessageGetYuanBao);
			
			return;
		}
		
		
		//点击完9个后，刷新奖励
		if(nCountShowCardNum == 8)
		{
			//bLock = true;
			Tab_Scratch scratchTab = TableManager.GetScratchByID(Obj_MyselfPlayer.GetMe().GGLRewardID);
			if(scratchTab.PrizeType == -1) //提示没中奖
			{
				//显示点击的卡牌
				this.ShowCard(curTouchCard);
				BoxManager.showMessageByID((int)MessageIdEnum.Msg180);
				UIEventListener.Get(BoxManager.getYesButton()).onClick += ConfirmBtnProcess;
				return;
			
			}
			
			NetworkSender.Instance().GetUserInfo(ComfirmNetWork);
			return;
		}
		
		//显示点击的卡牌
		this.ShowCard(curTouchCard);
	}
	
	
	//光效播放完的回调
	void EffectCom()
	{
		Tab_Scratch scratchTab = TableManager.GetScratchByID(Obj_MyselfPlayer.GetMe().GGLRewardID);
		if(scratchTab.PrizeType == 0)//提示中了多少元宝
		{
			int prizeNum = TableManager.GetScratchByID(Obj_MyselfPlayer.GetMe().GGLRewardID).Value;
			BoxManager.showMessageByID((int)MessageIdEnum.Msg179, prizeNum.ToString());
			UIEventListener.Get(BoxManager.getYesButton()).onClick += ConfirmBtnProcess;
		}
		else if(scratchTab.PrizeType == 1)//提示中了多少金币
		{
			int prizeNum = TableManager.GetScratchByID(Obj_MyselfPlayer.GetMe().GGLRewardID).Value;
			BoxManager.showMessageByID((int)MessageIdEnum.Msg185, prizeNum.ToString());
			UIEventListener.Get(BoxManager.getYesButton()).onClick += ConfirmBtnProcess;
		}
		else if(scratchTab.PrizeType == 2)
		{
			//Tab_Appearance appear = TableManager.GetAppearanceByID(scratchTab.Value);
			//string name = LanguageManger.GetWords(appear.Name);
			//BoxManager.showMessageByID((int)MessageIdEnum.Msg186, name);
			
			if(Obj_MyselfPlayer.GetMe().GGLRewardID == 1)
			{
				BoxManager.showMessageByID((int)MessageIdEnum.Msg186, "七星虚竹");
			}
			else if(Obj_MyselfPlayer.GetMe().GGLRewardID == 11)
			{
				BoxManager.showMessageByID((int)MessageIdEnum.Msg186, "黑棋子");
			}
			
			if(BoxManager.getYesButton() == null)
			{
				Debug.Log("yun yun yun  yun ");
			}
			UIEventListener.Get(BoxManager.getYesButton()).onClick += ConfirmBtnProcess;
		}
		
		
		
	}
	
	//确认网络是否正常
	void ComfirmNetWork(bool bSuc)
	{
		
		bLock = true;
		this.FreshGGLTimesAndYuanBao();
        mainController.updateUserInfo();
		
		//显示点击的卡牌
		this.ShowCard(curTouchCard);
		
		effect01.gameObject.SetActive(true);
		effect01.Play("gglEffect",PlayMode.StopAll);
	}
	
	void ConfirmBtnProcess(GameObject btn)
	{
		bLock = false;
		
		effect02.gameObject.SetActive(false);
	}
	
	void ShowCard(GameObject cardObj)
	{
		if(cardObj == null)
		{
			return;
		}
		nCountShowCardNum++;
		
		cardObj.transform.FindChild("Mask").gameObject.SetActive(false);
		cardObj.transform.FindChild("Label").gameObject.SetActive(true);	
		cardObj.transform.FindChild("NumBg").gameObject.SetActive(true);
	}
	
	
	//计算刮刮乐卡
	void CountGGLCard()
	{
		//有奖励的情况
		if(Obj_MyselfPlayer.GetMe().GGLRewardID > 0)
		{
			int num=0;
			
			
			//先把中奖的卡牌随机到3个不同的位置
			while(num < 3)
			{
				int randomIndex = Random.Range(0,8);
				if(cardIDs[randomIndex] < 0)
				{
					cardIDs[randomIndex] = Obj_MyselfPlayer.GetMe().GGLRewardID;
					num++;
				}
			}
			
			//把剩下的卡牌随机到6个不同的位置
			for(int i=0; i<9; i++)
			{
				//此处已经有卡牌了
				if(cardIDs[i] > 0)
				{
					continue;
				}
				
				//奖励的等级
				int randomcardID = 0;
				int nrandIDMax = TableManager.GetScratch().Count;
				do{
					randomcardID = Random.Range(1,nrandIDMax);
					
				}while(CheckCardIDAlreadyHasTwo(randomcardID));
				
				cardIDs[i] = randomcardID;
			}
		}
		else
		{
			for(int i=0; i<9; i++)
			{
				//此处已经有卡牌了
				if(cardIDs[i] > 0)
				{
					continue;
				}
				
				//奖励的等级
				int randomcardID = 0;
				int nrandIDMax = TableManager.GetScratch().Count;
				do{
					randomcardID = Random.Range(1,nrandIDMax);
					
				}while(CheckCardIDAlreadyHasTwo(randomcardID));
				
				 cardIDs[i] = randomcardID;
			}
		}
		
		
		//显示cardIcon
		for(int i=0; i<9; i++)
		{
			
			Tab_Scratch scratchTab = TableManager.GetScratchByID(cardIDs[i]);
			Tab_ScratchPrize scratchprizeTab = TableManager.GetScratchPrizeByID(scratchTab.Prize);
			//if(scratchTab.PrizeType == 1)  //金钱
			//{
				
			//}
			//else if(scratchTab.PrizeType == 0) //元宝
			//{
				
			//}
			if(scratchTab.PrizeType == 0 || scratchTab.PrizeType == 1)
			{
				carditems[i].transform.FindChild("Label").GetComponent<UILabel>().text = scratchTab.Value.ToString();
			
				string strIconName = scratchprizeTab.HeadIcon;
				carditems[i].transform.FindChild("card").gameObject.SetActive(true);
				carditems[i].transform.FindChild("card2").gameObject.SetActive(false);
			
				carditems[i].transform.FindChild("card").GetComponent<UISprite>().spriteName = strIconName;
				carditems[i].transform.FindChild("card").GetComponent<UISprite>().MakePixelPerfect();
			}
			else if(scratchTab.PrizeType == 2)
			{
				carditems[i].transform.FindChild("Label").GetComponent<UILabel>().text = "1";
			
				string strIconName = scratchprizeTab.HeadIcon;
				carditems[i].transform.FindChild("card2").gameObject.SetActive(true);
				carditems[i].transform.FindChild("card").gameObject.SetActive(false);
			
				carditems[i].transform.FindChild("card2").GetComponent<UISprite>().spriteName = strIconName;
				carditems[i].transform.FindChild("card2").GetComponent<UISprite>().MakePixelPerfect();
				
				/*
				Transform trans = carditems[i].transform.FindChild("card");
				if(trans != null)
				{
					trans.GetComponent<UISprite>().atlas = cardAtlasSp.atlas;
					trans.GetComponent<UISprite>().spriteName = strIconName;
					trans.localPosition = new Vector3(trans.position.x, trans.position.y, -5);
				}
				*/
			}
			
		}
	}
	
	
	
	//用来检查随机到的卡牌是否已经存在两个了？
	bool CheckCardIDAlreadyHasTwo(int cardId)
	{
		int hasNum = 0;
		for(int i=0; i<9; i++)
		{
			if(cardIDs[i] == cardId)
			{
				hasNum ++;
				if(hasNum == 2)
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	void OnMessageGetYuanBao(bool isSucceed)
	{
		this.FreshGGLTimesAndYuanBao();
	}
	
	void OnMessageGGLResultReturn(bool isSucceed)
	{
		
		if(!bThisTimeFree)
		{
			Obj_MyselfPlayer.GetMe().dollar -= TableManager.GetScratchCostByID(2).Cost;
		}
		
		bCurtouchFirsr = false;
		//bMessageLock = false;
		this.FreshGGLTimesAndYuanBao();
		this.CountGGLCard();
		this.ShowCard(curTouchCard);
	}
}
