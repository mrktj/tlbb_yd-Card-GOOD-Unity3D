using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using card.net;
using GCGame.Table;

public class BaZhenTuController : MonoBehaviour {
	
	public Transform[] local;
	public GameObject[] gObjItems;
	public UISpriteAnimation effect;
	public Transform BtnTrans;
	private int nCountHowNumItemAlphaCom = 0;
	private int nCountHowNumItemTweenMvoeCom = 0;
	//private bool bFirstPress = true;  
	//private bool bCanPressZhongXinBtn = true;
	private bool bCanPressPrizeIcon = false;
	private GameObject curChooseObj;   //当前选中的位置
	private int[] randomAlreadyArray = new int[8]; //记录随机奖励的ID
	private int nNeedAlphaNum = 8;
	private MainController mainController = null;
	private GameObject chooseMaskBtn = null;
	//private bool bFirstEnter = true;
	private bool bReset = true;
	private int[] CountrandomArray = new int[8];  //用来中间计算的
	private int nNumRandomRwardIDs = 0;
	//已领取的状态
	private int[] RewardFlags = new int[8];  //做位标记
	
	
	//奖励类型
	private int YUANBAO_TYPE  = 0;
	//private int MONEY_TYPE = 1;
	
	
	// Use this for initialization
	void Start () {
		

		
	}
	
	void ResetRewardFlags()
	{
		RewardFlags[0] = 1;
		RewardFlags[1] = 2;
		RewardFlags[2] = 4;
		RewardFlags[3] = 8;
		RewardFlags[4] = 16;
		RewardFlags[5] = 32;
		RewardFlags[6] = 64;
		RewardFlags[7] = 128;
	}
	
	void Test()
	{
		Obj_MyselfPlayer.GetMe().BGZTimes = 1;
		Obj_MyselfPlayer.GetMe().Flags = RewardFlags[1] | RewardFlags[3];
		Obj_MyselfPlayer.GetMe().BGZRewardID = 8;
	}
	
	
	
	void OnEnable()
	{
		//Obj_MyselfPlayer.GetMe().BGZTimes = 8;
		if(mainController == null)
		{
			mainController = GameObject.Find("MainController").GetComponent<MainController>();
		}
		mainController.ShowActivityTopUI(ActivityType.E_ACTIVITY_TYPE_BAZHENTU);
		
		ResetRewardFlags();	
		//this.Test();
		
		if(bReset)
		{
			this.Reset();
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
            mainController.updateUserInfo();
        }
	}
	
	
	
	void Reset()
	{
		if(effect != null)
		{
			if(Obj_MyselfPlayer.GetMe().BGZTimes == 0)
			{
				effect.gameObject.SetActive(true);
			}
			else
			{
				effect.gameObject.SetActive(false);
			}
			
		}
		
		for(int i=0; i<gObjItems.Length; i++)
		{
			gObjItems[i].transform.FindChild("yiling").gameObject.SetActive(false);
			gObjItems[i].transform.FindChild("Mask").GetComponent<UISprite>().alpha = 0f;
			gObjItems[i].transform.FindChild("Mask").GetComponent<TweenAlpha>().enabled = false;
			gObjItems[i].transform.FindChild("yiling").gameObject.SetActive(false);
		}
		
		
		//
		//bFirstPress = true;
		/*
		if(Obj_MyselfPlayer.GetMe().BGZTimes == 0)
		{
			bCanPressZhongXinBtn = true;
		}
		else
		{
			bCanPressZhongXinBtn = false;
		}
		*/
		
		//重置奖励金币和金钱的显示
		ResetPrizeShow();
		
		//重置保存已经领取的奖励ID
		ResetCurAlready();
			
		//显示已领取状态
		ShowAlreadyGetRewardIcon();
		
		
			
		bCanPressPrizeIcon = false;
		nCountHowNumItemAlphaCom = 0;
		nCountHowNumItemTweenMvoeCom = 0;
		nNeedAlphaNum = 8;
		curChooseObj = null;
		//把已经领过的显示出已领状态
	}
	
	
	//重置奖励的金钱显示
	void ResetPrizeShow()
	{
		for(int i=0; i<8; i++)
		{
			
			SetPrizeContent(gObjItems[i], i+1);
			/*
			if(TableManager.GetBaguaByID(i+1).Type == YUANBAO_TYPE)
			{
				gObjItems[i].transform.FindChild("bg").GetComponent<UISprite>().spriteName = "coin";
				string strNum = TableManager.GetBaguaByID(i+1).Text;
				gObjItems[i].transform.FindChild("Label").GetComponent<UILabel>().text = strNum;
			}
			*/
		}
		
	}
	
	//重置并记录当前已经翻开的奖励
	void ResetCurAlready()
	{
		for(int i=0; i<8; i++)
		{

			randomAlreadyArray[i] = -1;
			
		}
		
		for(int i=0; i<8; i++)
		{
			if((RewardFlags[i] & Obj_MyselfPlayer.GetMe().Flags) > 0)
			{
				randomAlreadyArray[i] = i+1;
			}
		}
	}
	
	
	//显示已经获得奖励的图标
	void ShowAlreadyGetRewardIcon()
	{
		for(int i=0; i<RewardFlags.Length;i++)
		{
			if((RewardFlags[i] & Obj_MyselfPlayer.GetMe().Flags) > 0)
			{
				gObjItems[i].transform.FindChild("yiling").gameObject.SetActive(true);
			}
		}
	}
	
	
	
	//创建tweenAlpha 0-1的
	void SetTweenAlphaZeroToOne()
	{
		//开始播八个阵的显示动画
		foreach(var item in gObjItems)
		{
			GameObject gObjMask = item.transform.FindChild("Mask").gameObject;
			
			if(checkTouchIfAreadlyGetPrize(gObjMask))
			{
				nNeedAlphaNum--;
				continue;
			}
			
			TweenAlpha tweenAlpha = item.transform.FindChild("Mask").GetComponent<TweenAlpha>();
			if(tweenAlpha != null)
			{
				tweenAlpha.Reset();
				tweenAlpha.enabled = true;
				tweenAlpha.alpha = 0f;
				tweenAlpha.from = 0f;
				tweenAlpha.to = 1f;
				tweenAlpha.callWhenFinished = "OnTweenAlpahFinished";
			}
		}
	}
	
	void SetTweenAlphaOneToZero()
	{
		//开始播八个阵的显示动画
		foreach(var item in gObjItems)
		{
			Transform TransMask = item.transform.FindChild("Mask");
			TweenAlpha tweenAlpha = TransMask.GetComponent<TweenAlpha>();
			if((curChooseObj != null && curChooseObj == TransMask.gameObject)
				  || checkTouchIfAreadlyGetPrize(item.transform.FindChild("Mask").gameObject))
			{
				continue;
			}
			
			if(tweenAlpha != null)
			{
				tweenAlpha.Reset();
				tweenAlpha.enabled = true;
				tweenAlpha.alpha = 1f;
				tweenAlpha.from = 1f;
				tweenAlpha.to = 0f;
				tweenAlpha.callWhenFinished = "OnTweenAlpahFinished";
			}
		}
	}
	
	//创建坐标相关的Tween
	void CreateItweenToTarget()
	{
		for(int i = 0; i < gObjItems.Length; i++)
		{
			
			Hashtable hash=new Hashtable();
			hash.Add("time",1.0f);
			hash.Add("position",BtnTrans);
			hash.Add("delay",0.5f);
			hash.Add("oncomplete","AnimationCompleteToTarget");
			hash.Add("oncompleteparams",gObjItems[i]);
			hash.Add("oncompletetarget",gameObject);
					
					
			iTween.MoveTo(gObjItems[i],hash);
			
			//TweenPosition.Begin(gObjItems[i], 0.2f, BtnTrans.localPosition);
		}
	}
	
	
	//移动到目标的动画完成
	void AnimationCompleteToTarget()
	{
		nCountHowNumItemTweenMvoeCom++;
		if(nCountHowNumItemTweenMvoeCom == gObjItems.Length)
		{
			bCanPressPrizeIcon = true;
			StartCoroutine(waitSomeTime());
			nCountHowNumItemTweenMvoeCom = 0;
		}
	}
	
	
	//延时用
	IEnumerator  waitSomeTime()
	{
		yield return new WaitForSeconds(1.0f);
		
		SuiJiBaGuaCom();
		
	}
	
	//八卦随机完
	void SuiJiBaGuaCom()
	{
		effect.gameObject.SetActive(false);
		this.CreateItweenReturnBack();
	}

	
	
	void CreateItweenReturnBack()
	{
		for(int i = 0; i < gObjItems.Length; i++)
		{
			
			Hashtable hash=new Hashtable();
			hash.Add("time",1.0f);
			hash.Add("position",local[i]);
			hash.Add("delay",0.5f);
			hash.Add("oncomplete","AnimationCompleteReturnBack");
			hash.Add("oncompleteparams",gObjItems[i]);
			hash.Add("oncompletetarget",gameObject);
					
			iTween.MoveTo(gObjItems[i],hash);
		
		}
	}
	
	void AnimationCompleteReturnBack()
	{
		nCountHowNumItemTweenMvoeCom++;
		if(nCountHowNumItemTweenMvoeCom == gObjItems.Length)
		{
			for(int i = 0; i < gObjItems.Length; i++)
			{
				Debug.Log(gObjItems[i].transform.position);
			}
		}
		
	}
	
	
	//点击中间按钮
	void OnBaGuaZhenClick()
	{
		if(!bReset)
		{
			return;
		}
		/*
		if(!bFirstPress)
		{
			return;
		}
		*/
		
		/*
		if(Obj_MyselfPlayer.GetMe().BGZTimes != 0)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg188);
			return;
		}
		*/
		//bFirstPress = false;
		//bCanPressZhongXinBtn = false;
		//onMessageBaguaZhenAsk(true);
		//说明抽奖结束
		if(Obj_MyselfPlayer.GetMe().Flags == 255)
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg189);
			return;
		}
		
		
		NetworkSender.Instance().SendBGZPB(onMessageBaguaZhenAsk);
		
		//测试
		//onMessageBaguaZhenAsk(true);
	}
	
	//点击八卦阵发送的消息返回
	public void onMessageBaguaZhenAsk(bool bSuc)
	{
		bReset = false;
		SetTweenAlphaZeroToOne();
	}
	
	
	//判断当前点击的是否已经领取了
	public bool checkTouchIfAreadlyGetPrize(GameObject btnMask)
	{
		if(btnMask == null)
		{
			return true;
		}
		
		return btnMask.transform.parent.FindChild("yiling").gameObject.activeSelf;
	}
	
	//设置奖励图标的显示内容
	public void SetPrizeContent(GameObject item, int nRewardID)
	{
		if(TableManager.GetBaguaByID(nRewardID).Type == YUANBAO_TYPE)
		{
			item.transform.FindChild("bg").GetComponent<UISprite>().spriteName = "gold";
		}
		else
		{
			item.transform.FindChild("bg").GetComponent<UISprite>().spriteName = "coin";
		}
		
		string strNum = TableManager.GetBaguaByID(nRewardID).Text;
		item.transform.FindChild("Label").GetComponent<UILabel>().text = strNum;
		
	}
	
	//确定是否能用此奖励ID
	bool CheckIfCanUse(int rewardID)
	{

		if(rewardID == Obj_MyselfPlayer.GetMe().BGZRewardID)
		{
			return false;
		}
		
		for(int i=0; i<randomAlreadyArray.Length; i++)
		{
			if(randomAlreadyArray[i]>0 && randomAlreadyArray[i] == rewardID)
			{
				return false;
			}
		}

		
		return true;
	}
	
	//填入刚刚随的东西
	void FillRewardRandomArray(int rewardID)
	{
		/*
		if(rewardID >=1 && rewardID <=8)
		{
			randomAlreadyArray[rewardID-1] = 1;
		}
		*/
		
		for(int i=0; i<randomAlreadyArray.Length; i++)
		{
			if(randomAlreadyArray[i] < 0)
			{
				randomAlreadyArray[i] = rewardID;
				return;
			}
		}
		
		/*
		for(int i=0; i<randomAlreadyArray.Length; i++)
		{
			if(randomAlreadyArray[i] < 0)
			{
				randomAlreadyArray[i] = rewardID;
				return;
			}
		}
		*/
	}
	
	//判断是否RewardRandomArray已经填满
	bool IfFillRewardRandomArrayFull()
	{
		for(int i=0; i<randomAlreadyArray.Length; i++)
		{
			if(randomAlreadyArray[i] < 0)
			{
				return false;
			}
		}
		
		return true;
	}
	
	bool CheckrandomAlreadyArrayContain(int RewardID)
	{
		for(int i=0; i<randomAlreadyArray.Length; i++)
		{
			if(randomAlreadyArray[i] == RewardID)
			{
				return true;
			}
		}
		
		return false;
	}
	
	//填充CountrandomArray
	void FillCountrandomArray(int nRewardID)
	{
		for(int i=0; i<CountrandomArray.Length; i++)
		{
			if(CountrandomArray[i] <0)
			{
				CountrandomArray[i] = nRewardID;
				return;
			}
		}
	}
	
	//重置randomArray
	void OnResetCountRandomArray()
	{
		for(int i=0; i<CountrandomArray.Length; i++)
		{
			CountrandomArray[i] = -1;
		}
		
		for(int j=1; j<=8;j++)
		{
			if(!CheckrandomAlreadyArrayContain(j))
			{
				this.FillCountrandomArray(j);
			}
		}
		
		nNumRandomRwardIDs = 0;
		for(int i=0; i<CountrandomArray.Length; i++)
		{
			if(CountrandomArray[i] > 0)
			{
				nNumRandomRwardIDs++;
			}
			
		}
		
	}
	
	
	//
	void ComfirmNetWork(bool bSuc)
	{
		bReset = true;
		
		GameObject gObgMaskBtn = chooseMaskBtn;
		GameObject chooseItem = gObgMaskBtn.transform.parent.gameObject;
		
		SetPrizeContent(chooseItem, Obj_MyselfPlayer.GetMe().BGZRewardID);
		FillRewardRandomArray(Obj_MyselfPlayer.GetMe().BGZRewardID);
		//随机放置其他的
		for(int j=0; j<gObjItems.Length; j++)
		{
			//如果是已经领取的 和 刚刚点击领取的都不参与随机 
			if(checkTouchIfAreadlyGetPrize(gObjItems[j].transform.FindChild("Mask").gameObject) 
				  || chooseItem == gObjItems[j])
			{
				continue;
			}
			
			if(!IfFillRewardRandomArrayFull())
			{
				this.OnResetCountRandomArray();
				//随一个现在能用的ID
				int nRandom = Random.Range(0,nNumRandomRwardIDs-1);
				int nRandID = CountrandomArray[nRandom];
				/*
				do
				{
					nRandID = Random.Range(1,8);
		
				}while(!CheckIfCanUse(nRandID));
				*/
				
				SetPrizeContent(gObjItems[j], nRandID);
				FillRewardRandomArray(nRandID);
			}
		}
		
		
		bCanPressPrizeIcon = false;
		curChooseObj = gObgMaskBtn;
		TweenAlpha tweenAlpha = gObgMaskBtn.transform.GetComponent<TweenAlpha>();
		if(tweenAlpha != null)
		{
			tweenAlpha.Reset();
			tweenAlpha.enabled = true;
			tweenAlpha.alpha = 1f;
			tweenAlpha.from = 1f;
			tweenAlpha.to = 0f;
			tweenAlpha.duration = 1.0f;
			tweenAlpha.callWhenFinished = "OnChooseIconTweenAlphaCom";
		}
	}
	
	//点击领取
	void OnClickPrizeIcon(GameObject gObgMaskBtn)
	{
		if(!bCanPressPrizeIcon)
		{
			return;
		}
		
		if(checkTouchIfAreadlyGetPrize(gObgMaskBtn))
		{
			return;
		}
		
		
		//当前点击的那个item
		chooseMaskBtn = gObgMaskBtn;
		//ComfirmNetWork(true);
		NetworkSender.Instance().GetUserInfo(ComfirmNetWork);
	}
	
	void OnChooseIconTweenAlphaCom()
	{
		curChooseObj.transform.parent.FindChild("yiling").gameObject.SetActive(true);
		//bFirstPress = true;
		SetTweenAlphaOneToZero();
	}
	
	
	
	
	void OnTweenAlpahFinished(UITweener tweener)
	{
		TweenAlpha tweenAlpha = (TweenAlpha)tweener;
		
		//大于0.5说明现在是从0开始到1的
		if(tweenAlpha.alpha > 0.5)
		{
			nCountHowNumItemAlphaCom++;
		}
		
		if(nCountHowNumItemAlphaCom == nNeedAlphaNum)
		{
			this.CreateItweenToTarget();
			nCountHowNumItemAlphaCom = 0;
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
