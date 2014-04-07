using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

using card.net;

public class LotteryController : MonoBehaviour {
	
	public GameObject freeLotteryOnce;
	public UIImageButton freeButton;
	public UILabel freeLabel;
	public GameObject freeLotteryPrompt;

	public UILabel labelPointCost;//每次抽奖点数
	public UILabel labelPointHave;//当前拥有点数
	public UILabel labelLotteryTimes;//可抽奖次数
	public UISprite labelLotteryType;
	public UISlider luckySlider;
	public UILabel luckylabel;
	public GameObject luckyButton;
	public UISprite bg;
	public GameObject onePrompt;
	public GameObject tenPrompt;
	public UISprite luckyHint;
		
	public enum LOTTERY_TYPE
	{
		NONE = -1,
		FRIEND,
		DIAMOND,
		LUCKY,
	}
	
	private LOTTERY_TYPE lotteryType = LOTTERY_TYPE.NONE;
	
	private int friendLotteryOneCost;
	private int friendLotteryTenCost;
	private int diamondLotteryOneCost;
	private int diamondLotteryTenCost;
	
	private MainUILogic mainLogic;
	
	void Awake()
	{
	}
	
	void Start()
	{
	}
	
	void OnEnable()
	{
		friendLotteryOneCost = TableManager.GetGambleCostByID((int)LOTTERY_TYPE.FRIEND).OneCost;
		friendLotteryTenCost = TableManager.GetGambleCostByID((int)LOTTERY_TYPE.FRIEND).TenCost;
		diamondLotteryOneCost = TableManager.GetGambleCostByID((int)LOTTERY_TYPE.DIAMOND).OneCost;
		diamondLotteryTenCost = TableManager.GetGambleCostByID((int)LOTTERY_TYPE.DIAMOND).TenCost;
		mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		setLotteryType(mainLogic.lotteryType);
		if(lotteryType == LotteryController.LOTTERY_TYPE.DIAMOND)
		{
			int oneTime=Obj_MyselfPlayer.GetMe().dollar/diamondLotteryOneCost;
			int tenTime=Obj_MyselfPlayer.GetMe().dollar/diamondLotteryTenCost;
			if(oneTime>0)
			{
				onePrompt.transform.FindChild("Label").GetComponent<UILabel>().text=oneTime.ToString();
				onePrompt.SetActive(true);
			}
			else
			{
				onePrompt.SetActive(false);
			}
			if(tenTime>0)
			{
				tenPrompt.transform.FindChild("Label").GetComponent<UILabel>().text=tenTime.ToString();
				tenPrompt.SetActive(true);
			}
			else
			{
				tenPrompt.SetActive(false);
			}
			if (Obj_MyselfPlayer.GetMe().freeCD <= 0) //可以抽
			{
				freeButton.transform.GetComponent<BoxCollider>().enabled = true;
				freeButton.isEnabled = true;
				freeLabel.gameObject.SetActive(false);
				freeLotteryPrompt.gameObject.SetActive(true);
			}
			else
			{
				freeButton.transform.GetComponent<BoxCollider>().enabled = false;
				freeButton.isEnabled = false;
				freeLotteryPrompt.gameObject.SetActive(false);
				freeLabel.gameObject.SetActive(true);
				double timer = Obj_MyselfPlayer.GetMe().freeCD;
				freeLabel.text = ((int)timer/3600).ToString("00") + "小时" + ((int)timer%3600/60).ToString("00") + "分后开启"; 
				
			}
			freeButton.gameObject.SetActive(true);
			freeLabel.gameObject.SetActive(true);
			
			luckySlider.sliderValue = Obj_MyselfPlayer.GetMe().lotteryLuckyNum/100.0f;
			luckySlider.gameObject.SetActive(true);
			//luckylabel.text = "幸运值: "+Obj_MyselfPlayer.GetMe().lotteryLuckyNum.ToString();
			luckyButton.SetActive(true);
			luckyHint.gameObject.SetActive(true);
			if (Obj_MyselfPlayer.GetMe().lotteryLuckyNum > 0) //xinyunbaoxiang xingyuntishi
				luckyHint.spriteName = "xingyuntishi";
			else
				luckyHint.spriteName = "xinyunbaoxiang";
			luckyHint.MakePixelPerfect();
			
			if(Obj_MyselfPlayer.GetMe().lotteryLuckyNum == 100)
			{
				luckyButton.GetComponent<UIImageButton>().isEnabled=true;
				luckyButton.GetComponent<TweenScale>().enabled = true;
				//luckyButton.SetActive(true);
			}else
			{
				luckyButton.GetComponent<UIImageButton>().isEnabled=false;
				luckyButton.GetComponent<TweenScale>().enabled = false;
				//luckyButton.SetActive(false);
			}
			transform.FindChild("Buttons/TopRightBtn").gameObject.SetActive(true);
		}else
		{
			int oneTime=Obj_MyselfPlayer.GetMe().fpoint/friendLotteryOneCost;
			int tenTime=Obj_MyselfPlayer.GetMe().fpoint/friendLotteryTenCost;
			if(oneTime>0)
			{
				onePrompt.transform.FindChild("Label").GetComponent<UILabel>().text=oneTime.ToString();
				onePrompt.SetActive(true);
			}
			else
			{
				onePrompt.SetActive(false);
			}
			if(tenTime>0)
			{
				tenPrompt.transform.FindChild("Label").GetComponent<UILabel>().text=tenTime.ToString();
				tenPrompt.SetActive(true);
			}
			else
			{
				tenPrompt.SetActive(false);
			}
			freeButton.gameObject.SetActive(false);
			freeLabel.gameObject.SetActive(false);
			luckyButton.gameObject.SetActive(false);
//			luckyButton.GetComponent<UIImageButton>().isEnabled=false;
//			luckyButton.GetComponent<TweenScale>().enabled = false;
			
			//luckyButton.SetActive(false);
			luckySlider.gameObject.SetActive(false);
			transform.FindChild("Buttons/TopRightBtn").gameObject.SetActive(false);
		}
	}
	
	public void resetFreeLotteryInfo()
	{
		if (lotteryType != LOTTERY_TYPE.DIAMOND)
			return ; 
		freeLabel.gameObject.SetActive(true);
		if (Obj_MyselfPlayer.GetMe().freeCD <= 0) //可以抽
		{
			freeButton.transform.GetComponent<BoxCollider>().enabled = true;
			freeButton.isEnabled = true;
			freeLabel.gameObject.SetActive(false);
			freeLotteryPrompt.gameObject.SetActive(true);
		}
		else
		{
			freeButton.transform.GetComponent<BoxCollider>().enabled = false;
			freeButton.isEnabled = false;
			freeLotteryPrompt.gameObject.SetActive(false);
			freeLabel.gameObject.SetActive(true);
			double timer = Obj_MyselfPlayer.GetMe().freeCD;
			freeLabel.text = ((int)timer/3600).ToString("00") + "小时" + ((int)timer%3600/60).ToString("00") + "分后开启"; 
		}
		freeButton.gameObject.SetActive(true);
	}
	
	public void sendFreeLotteryRequest()
	{
		if(Obj_MyselfPlayer.GetMe().cardBagList.Count>=Obj_MyselfPlayer.GetMe().bagMax)
		{
//			BoxManager.showBagFullBox("您携带的侠士已经达到上限可以将侠士吸收、出售或者扩充您的背包.");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg74);
			return;
		}
		NetworkSender.Instance().freeLotteryOnce(sendFreeLotteryDone);
	}
	
	public void sendFreeLotteryDone (bool isSuccess)
	{
		Debug.Log("SendFreeLotteryDone");
		if(GameObject.FindWithTag("main_controller")!=null)
		{
			GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
		}
		mainLogic.OnLotteryAnimationWindow();
		resetFreeLotteryInfo();
	}
	
	void Update ()
	{	
		
		double timer = Obj_MyselfPlayer.GetMe().freeCD;
//		Debug.Log("Timer = " + timer);
		if(lotteryType == LotteryController.LOTTERY_TYPE.DIAMOND && timer > 0)
		{
			freeButton.transform.GetComponent<BoxCollider>().enabled = false;
			freeButton.isEnabled = false;
			freeLotteryPrompt.gameObject.SetActive(false);
			freeLabel.gameObject.SetActive(true);
			freeLabel.text = ((int)timer/3600).ToString("00") + "小时" + ((int)timer%3600/60).ToString("00") + "分后开启"; 
		}
		else if (lotteryType == LotteryController.LOTTERY_TYPE.DIAMOND)
		{
			freeButton.transform.GetComponent<BoxCollider>().enabled = true;
			freeButton.isEnabled = true;
			freeLotteryPrompt.gameObject.SetActive(true);
			freeLabel.gameObject.SetActive(false);
		}
	}
	
	public void setLotteryType(LOTTERY_TYPE type)
	{
		lotteryType = type;
		UpdateLabel();
	}

	void UpdateLabel()
	{
		switch(lotteryType)
		{
		case LOTTERY_TYPE.FRIEND:
			bg.spriteName = "xiayichoujiang_xiayi_zi";
			labelLotteryType.spriteName = "xiayichoujiang";
			labelPointCost.text =  friendLotteryOneCost.ToString();//+"侠义点数";
			labelPointHave.text = Obj_MyselfPlayer.GetMe().fpoint.ToString();// + "侠义点数";
			labelLotteryTimes.text = BestAnswer(Obj_MyselfPlayer.GetMe().fpoint, friendLotteryOneCost, friendLotteryTenCost).ToString();//+"次";
			//labelLotteryTimes.transform.localPosition = new Vector3(20.0f, 48.0f, 0.0f);
			break;
		case LOTTERY_TYPE.DIAMOND:
			bg.spriteName = "xiayichoujiang_yuanbao_zi";
			labelLotteryType.spriteName = "yuanbaochoujiang";
			labelPointCost.text = diamondLotteryOneCost.ToString();// + "元宝";
			labelPointHave.text = diamondLotteryTenCost.ToString();
//			labelPointHave.text = Obj_MyselfPlayer.GetMe().dollar.ToString();//+ "元宝";
			labelLotteryTimes.text = BestAnswer(Obj_MyselfPlayer.GetMe().dollar, diamondLotteryOneCost, diamondLotteryTenCost).ToString();//+"次";
			//labelLotteryTimes.transform.localPosition = new Vector3(-10.0f, 48.0f, 0.0f);
			break;
		}
	}
	public int BestAnswer(int point, int one_point, int ten_point)
	{
		if(point < one_point)
			return 0;
		
		int last_point = point % ten_point;
		
		int num = (int)(point/ten_point)*10 + (int)(last_point/one_point);
		
		return num;
	}
	
	public void lotteryOne()
	{
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.LOTTERY)
			GuideLottery.Instance.NextStep();//抽奖指引 SELECT_3
		
		
		if(Obj_MyselfPlayer.GetMe().cardBagList.Count>=Obj_MyselfPlayer.GetMe().bagMax)
		{
//			BoxManager.showBagFullBox("您携带的侠士已经达到上限可以将侠士吸收、出售或者扩充您的背包.");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg74);
			return;
		}
		switch(lotteryType)
		{
		case LOTTERY_TYPE.FRIEND:

			if(Obj_MyselfPlayer.GetMe().fpoint<friendLotteryOneCost)
			{
//				BoxManager.showMessage("侠义点数不足");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg51);
				return;
			}
			break;
		case LOTTERY_TYPE.DIAMOND:
			if(Obj_MyselfPlayer.GetMe().dollar<diamondLotteryOneCost)
			{
//				BoxManager.showMessage("元宝不足"); 
				BoxManager.showMessageByID((int)MessageIdEnum.Msg52);
				return;
			}
			break;
		}
		NetworkSender.Instance().lotteryCard(lotteryCardDone,lotteryType,1);
	}
	
	public void lotteryTen()
	{
		if(Obj_MyselfPlayer.GetMe().cardBagList.Count>=Obj_MyselfPlayer.GetMe().bagMax)
		{
//			BoxManager.showBagFullBox("您携带的侠士已经达到上限可以将侠士吸收、出售或者扩充您的背包.");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg74);
			return;
		}
		switch(lotteryType)
		{
		case LOTTERY_TYPE.FRIEND:
			if(Obj_MyselfPlayer.GetMe().fpoint<friendLotteryTenCost)
			{
//				BoxManager.showMessage("侠义点数不足");  
				BoxManager.showMessageByID((int)MessageIdEnum.Msg51);
				return;
			}
			break;
		case LOTTERY_TYPE.DIAMOND:
			if(Obj_MyselfPlayer.GetMe().dollar<diamondLotteryTenCost)
			{
//				BoxManager.showMessage("元宝不足");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg52);
				return;
			}
			break;
		}
		NetworkSender.Instance().lotteryCard(lotteryCardDone,lotteryType,10);
		
	}		
	
	public void lotteryCardDone(bool isSuccess)
	{
//		BoxManager.showMessage(Obj_MyselfPlayer.GetMe().lotteryTempletIDs.ToString());
		//抽奖流程已经结束
		if(GameObject.FindWithTag("main_controller")!=null)
		{
			GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
		}

		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.LOTTERY)
		{
//			GuideManager.Instance.getCurrentGuideWindow().GetComponent<GuideLotteryController>().OnClickGuideItem(null);
			GuideManager.Instance.SetTempletID(Obj_MyselfPlayer.GetMe().lotteryTempletIDs[0]);
			sendFinishStep();
		}else
		{
			mainLogic.OnLotteryAnimationWindow();
		}	
		
	}
		
	public void sendFinishStep()
	{
//		NetworkSender.Instance().guideFinishStep(recFinishStep,GuideManager.GUIDE_STEP.LOTTERY);
		//GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.LOTTERY);
		recFinishStep(true);
	}
	public void recFinishStep(bool isSuccess)
	{
		mainLogic.OnLotteryAnimationWindow();
	}
	
	public void backToPreviousWindow()
	{
//		mainLogic.ReturnToMainUI();
		mainLogic.OnShopWindow();
		//mainLogic.backToPreviousWindow();
	}
	
	public void OnRecharge()
	{
//		BoxManager.showMessage("功能暂未开放");
//		BoxManager.showMessageByID((int)MessageIdEnum.Msg18);
		if (mainLogic == null)
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		mainLogic.OnPurchaseWindow();
	}
	
	
	
	//for guide
	public GameObject getBtnOne()
	{
		return transform.FindChild("Buttons/OneBtn").gameObject;
	}
	
	public void OnLuckyButtonClick(GameObject button)
	{
		NetworkSender.Instance().lotteryCard(lotteryCardDone,LOTTERY_TYPE.LUCKY,1);
	}
}
