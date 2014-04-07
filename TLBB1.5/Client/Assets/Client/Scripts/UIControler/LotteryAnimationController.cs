using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

using card.net;

public class LotteryAnimationController : MonoBehaviour {
	public GameObject layer1;
	public GameObject layer2_1;
	public GameObject layer2_2;
	public GameObject layer2_3;
	public GameObject layer3;
	public GameObject layer4_1;
	public GameObject layer4_2;
	public GameObject layer5;
	public GameObject layer6_1;
	public GameObject layer6_2;
	public GameObject layer6_3;
	public GameObject layer7_1;
	public GameObject layer7_2;
	public GameObject layer8;
	public GameObject layer9;
	
	public GameObject cardOne;
	public GameObject buttonOne;
	public GameObject buttonTen;
	public GameObject buttonClose;
	
	public GameObject lotteryAnimation;
	
	public GameObject[] cardTen;
	
	private LotteryController.LOTTERY_TYPE lotteryType = LotteryController.LOTTERY_TYPE.NONE;
	
	private int friendLotteryOneCost;
	private int friendLotteryTenCost;
	private int diamondLotteryOneCost;
	private int diamondLotteryTenCost;
	
	private MainUILogic mainLogic;
	private MainController mainController;
	private float speedRate=2f;
	
	public  static float timeCount = 0;
	public UISlider luckySlider;
	public GameObject luckyLotteryButton;
	public UILabel luckyPointLabel;
	void Awake()
	{
		mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();	
		mainController = mainLogic.mainController.GetComponent<MainController>();
		
		friendLotteryOneCost = TableManager.GetGambleCostByID((int)LotteryController.LOTTERY_TYPE.FRIEND).OneCost;
		friendLotteryTenCost = TableManager.GetGambleCostByID((int)LotteryController.LOTTERY_TYPE.FRIEND).TenCost;
		diamondLotteryOneCost = TableManager.GetGambleCostByID((int)LotteryController.LOTTERY_TYPE.DIAMOND).OneCost;
		diamondLotteryTenCost = TableManager.GetGambleCostByID((int)LotteryController.LOTTERY_TYPE.DIAMOND).TenCost;
		Application.targetFrameRate = -1;
		layer1_scale = layer1.transform.localScale;
		layer2_1_scale = layer2_1.transform.localScale;
		layer2_2_scale = layer2_2.transform.localScale;
		layer3_scale = layer3.transform.localScale;
		layer4_2position = layer4_2.transform.localPosition;
		layer8_scale = layer8.transform.localScale;
		layer9_scale = layer9.transform.localScale;
		
	}
	void Start()
	{	
		
		

	}
	void OnEnable()
	{	
		lotteryType = mainLogic.lotteryType;
		mainController.hideTopBar();
		mainController.hideBottomBar();
		//这么写动画太没效率了!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		playAnimation();
	}
	void OnDisable()
	{
		mainController.showTopBar();
		mainController.refreshTopBar();
		resetAnimation();
	}
	void Update()
	{
		timeCount+= Time.deltaTime;
		updatePlayer();
		
		if(layer6_3.activeSelf)
		{
			layer6_3.transform.RotateAround(Vector3.forward,Time.deltaTime);
		}
	}
	Vector3 layer1_scale;
	Vector3 layer2_1_scale;
	Vector3 layer2_2_scale;
	Vector3 layer3_scale;
	Vector3 layer4_2position;
	Vector3 layer8_scale;
	Vector3 layer9_scale;
	public void resetAnimation()
	{
		timeCount = 0;
		//恢复Sprite默认透明色
		/*
//		layer1.GetComponentsInChildren
		UISprite[] allLayers = lotteryAnimation.GetComponentsInChildren<UISprite>();
		for(int i =0 ; i< allLayers.Length; i++)
		{
//			allLayers[i].transform.localScale = Vector3.one;
//			allLayers[i].MakePixelPerfect();
			allLayers[i].color = new Color(255,255,255,255);
		}
		layer1.transform.localScale = layer1_scale;
		layer2_1.transform.localScale = layer2_1_scale;
		layer2_2.transform.localScale = layer2_2_scale;
		layer3.transform.localScale = layer3_scale;
		layer4_2.transform.localPosition = layer4_2position;
		layer8.transform.localScale = layer8_scale;
		layer9.transform.localScale = layer9_scale;
		//设置Sprite为不可见
		layer1.SetActive(false);
		layer2_1.SetActive(false);
		layer2_2.SetActive(false);
		layer2_3.SetActive(false);
		layer3.SetActive(false);
		layer4_1.SetActive(false);
		layer4_2.SetActive(false);
		layer6_1.SetActive(false);
		layer6_2.SetActive(false);
		layer6_3.SetActive(false);
		layer7_1.SetActive(false);
		layer7_2.SetActive(false);
		layer8.SetActive(false);
		layer9.SetActive(false);*/
		layer6_3.SetActive(false);
		cardOne.SetActive(false);
		for(int i=0;i<cardTen.Length;i++)
		{
			cardTen[i].SetActive(false);
		}
		buttonOne.SetActive(false);
		buttonTen.SetActive(false);
		buttonClose.SetActive(false);		
		luckySlider.gameObject.SetActive(false);
		luckyLotteryButton.SetActive(false);
	}
	public void playAnimation()
	{
		
		resetAnimation();
		
		int countLotteryRet = Obj_MyselfPlayer.GetMe().lotteryTempletIDs.Count;
		//cb:由于没有drawten资源，统一使用drawone
		if(countLotteryRet == 10)
		{
			AudioManager.Instance.PlayEffectSound("drawone");
		}else
		{
			AudioManager.Instance.PlayEffectSound("drawone");
		}
		animation.Play("LotteryAnimation");
		/*
		playLayer1();
		playLayer2();
		playLayer2_3();
		playLayer3();
		playLayer4();

		playLayer6();
		playLayer7();
		playLayer8();
		playLayer9();		
		*/
	}
	
	private float showTime=0;
	IEnumerator showFinalButton()
	{
		yield return new WaitForSeconds(showTime);
		
		buttonOne.SetActive(true);
		buttonTen.SetActive(true);
		buttonClose.SetActive(true);	
		if(lotteryType == LotteryController.LOTTERY_TYPE.DIAMOND)
		{
			luckySlider.sliderValue = Obj_MyselfPlayer.GetMe().lotteryLuckyNum/100.0f;
			luckySlider.gameObject.SetActive(true);
			luckyPointLabel.text = "幸运值: "+Obj_MyselfPlayer.GetMe().lotteryLuckyNum.ToString();
			if(Obj_MyselfPlayer.GetMe().lotteryLuckyNum == 100)
			{
				luckyLotteryButton.SetActive(true);
				luckyLotteryButton.GetComponent<UIImageButton>().isEnabled = true;
				luckyLotteryButton.GetComponent<TweenScale>().enabled = true;
			}
			else
			{
				luckyLotteryButton.SetActive(true);
				luckyLotteryButton.GetComponent<UIImageButton>().isEnabled = false;
				luckyLotteryButton.GetComponent<TweenScale>().enabled = false;
			}
		}
		
	}

	
	private void Layer9finish2(UITweener tween)
	{
		layer9.gameObject.SetActive(false);
		showCard();
	}
	
	private void showCard()
	{
		float timeCount = 0;
		
		int countLotteryRet = Obj_MyselfPlayer.GetMe().lotteryTempletIDs.Count;
		
		if(countLotteryRet == 1)//单次抽奖
		{
			cardOne.SetActive(true);
			
			if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.LOTTERY){
				GuideLottery.Instance.NextStep();
				Debug.LogWarning("*** LOTTERY *** Animation Nextstep");
			}
			
			//UITexture cardSprite = cardOne.transform.FindChild("Panel/Texture").GetComponent<UITexture>();
			//AtlasManager.Instance.setBodyByTempletID(cardSprite,Obj_MyselfPlayer.GetMe().lotteryTempletIDs[0]);		
			cardOne.name = Obj_MyselfPlayer.GetMe().lotteryTempletIDs[0].ToString();
			cardOne.GetComponent<CardLarge>().SetCardTemplateID(Obj_MyselfPlayer.GetMe().lotteryTempletIDs[0]);
			
			showTime = 2.0f;
		}else if(countLotteryRet == 10)//抽10张
		{
			for(int i=0;i<10;i++)
			{
				GameObject cardObj = cardTen[i];
				cardObj.SetActive(true);
				UISprite sprite =cardObj.transform.FindChild("Sprite").GetComponent<UISprite>();
				AtlasManager.Instance.setHeadByTempletID(sprite,Obj_MyselfPlayer.GetMe().lotteryTempletIDs[i]);
				sprite.MakePixelPerfect();
				//------------------------卡牌背景及外框--------------------------------
				//2013-10-12 Jack Wen
				UISprite icon_bg = cardObj.transform.FindChild("BG").GetComponent<UISprite>();
				UISprite icon_border = cardObj.transform.FindChild("topfram").GetComponent<UISprite>();
				int icon_star = TableManager.GetCardByID(Obj_MyselfPlayer.GetMe().lotteryTempletIDs[i]).Star;
				icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
				icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
				//--------------------------------------------------------------------
				cardObj.name = Obj_MyselfPlayer.GetMe().lotteryTempletIDs[i].ToString();
				
				timeCount+=0.2f;
				for(int subi = 0; subi < cardObj.transform.childCount;subi++)
				{
					GameObject subObj = cardObj.transform.GetChild(subi).gameObject;
					TweenAlpha ta = subObj.GetComponent<TweenAlpha>();
					
					if(ta==null)
					{
						ta = subObj.AddComponent<TweenAlpha>();
					}
					ta.Reset();
					ta.method = UITweener.Method.Linear;
					ta.style = UITweener.Style.Once;
					ta.duration = 1;
					ta.from = 0;
					ta.to = 1;
					addNewPlayer(ta,timeCount);					
				}
					

			}
			showTime = 5.0f;
			
		}else
		{
			//BoxManager.showMessage("服务器返回抽奖卡牌数有问题");  
			Debug.LogError("服务器返回抽奖卡牌数有问题");
		}
		
		
		layer6_3.SetActive(true);
		StartCoroutine("showFinalButton");
	}
	private void playLayer9()
	{
		TweenAlpha alpha1 = layer9.AddComponent<TweenAlpha>();
		alpha1.style = UITweener.Style.Once;
//		alpha1.delay = 0.5f;
		alpha1.duration = 0.1f*speedRate;
		alpha1.from = 0.2f;
		alpha1.to = 1f;
		alpha1.onFinished += Layer9finish1;
		addNewPlayer(alpha1,0.5f);//.Play(true);		
	}
	
	private void Layer9finish1(UITweener tween)
	{
		TweenAlpha alpha1 = layer9.AddComponent<TweenAlpha>();
		alpha1.style = UITweener.Style.Once;
		alpha1.duration = 0.5f*speedRate;
		alpha1.from = 1f;
		alpha1.to = 0f;
		alpha1.onFinished += Layer9finish2;
		addNewPlayer(alpha1,0);//.Play(true);	
		
		TweenScale scale = layer9.AddComponent<TweenScale>();
		scale.style = UITweener.Style.Once;
		scale.duration = 0.5f*speedRate;
		scale.from = new Vector3(layer9_scale.x,layer9_scale.y,1);
		scale.to = new Vector3(layer9_scale.x*2,layer9_scale.y*2,1);
		addNewPlayer(scale,0f);//.Play(true);		
	}
	
	private void playLayer8()
	{
		TweenAlpha alpha1 = layer8.AddComponent<TweenAlpha>();
		alpha1.style = UITweener.Style.Once;
		alpha1.duration = 0.1f*speedRate;
		alpha1.from = 0.2f;
		alpha1.to = 1f;
		alpha1.onFinished += Layer8finish1;
		addNewPlayer(alpha1,0);//.Play(true);			
	}
	
	private void Layer8finish1(UITweener tween)
	{
		TweenAlpha alpha1 = layer8.AddComponent<TweenAlpha>();
		alpha1.style = UITweener.Style.Once;
		alpha1.duration = 0.5f*speedRate;
		alpha1.from = 1f;
		alpha1.to = 0f;
		alpha1.onFinished += finishLayer;
		addNewPlayer(alpha1,0);//.Play(true);	
		
		TweenScale scale = layer8.AddComponent<TweenScale>();
		scale.style = UITweener.Style.Once;
		scale.duration = 0.5f*speedRate;
		scale.from = new Vector3(layer8_scale.x,layer8_scale.y,1);
		scale.to = new Vector3(layer8_scale.x*2,layer8_scale.y*2,1);
		addNewPlayer(scale,0f);//.Play(true);
	}
	private void playLayer7()
	{
		TweenAlpha alpha1 = layer7_1.AddComponent<TweenAlpha>();
		alpha1.style = UITweener.Style.Once;
		alpha1.duration = 0.9f*speedRate;
		alpha1.from = 1f;
		alpha1.to = 1f;
		alpha1.onFinished += finishLayer;
		addNewPlayer(alpha1,0);//.Play(true);
		
		TweenAlpha alpha2 = layer7_2.AddComponent<TweenAlpha>();
		alpha2.style = UITweener.Style.Once;
		alpha2.duration = 0.9f*speedRate;
		alpha2.from = 1f;
		alpha2.to = 1f;
		alpha2.onFinished += finishLayer;
		addNewPlayer(alpha2,0);//.Play(true);	
	}
	
	private void playLayer6()
	{
		TweenAlpha alpha = layer6_1.AddComponent<TweenAlpha>();
		alpha.style = UITweener.Style.Once;
//		alpha.delay = 0.1f;
		alpha.duration = 0.4f*speedRate;
		alpha.from = 1f;
		alpha.to = 0;
		alpha.onFinished += finishLayer;
		addNewPlayer(alpha,0.1f);//.Play(true);	
		
		TweenAlpha alpha1 = layer6_2.AddComponent<TweenAlpha>();
		alpha1.style = UITweener.Style.Once;
//		alpha1.delay = 0.5f;
		alpha1.duration = 0.8f*speedRate;
		alpha1.from = 1f;
		alpha1.to = 1;
		alpha1.onFinished += finishLayer;
		addNewPlayer(alpha1,0.1f);//.Play(true);	
		
//		TweenRotation rot = layer6_3.AddComponent<TweenRotation>();
//		rot.style = UITweener.Style.Loop;
////		rot.delay = 1.0f;
//		rot.duration = 2f*speedRate;
//		rot.from = new Vector3(0,0,0);
//		rot.to = new Vector3(0,0,-360);
////		rot.onFinished += finishLayer;
//		addNewPlayer(rot,1.0f);//.Play(true);	
		
//		Hashtable tab = new Hashtable
//		iTween.RotateBy(layer6_3.gameObject)
	}
	
//	private void playLayer5()
//	{
//		
//	}
	
	private void playLayer4()
	{

		
//		layer4_1.SetActive(true);
		TweenAlpha alpha = layer4_1.AddComponent<TweenAlpha>();
		alpha.style = UITweener.Style.Once;
//		alpha.delay = 0.1f;
		alpha.duration = 0.4f*speedRate;
		alpha.from = 1f;
		alpha.to = 0;
		alpha.onFinished += finishLayer;
		addNewPlayer(alpha,0.1f);//.Play(true);
		
		
//		layer4_2.SetActive(true);
		TweenAlpha alpha1 = layer4_2.AddComponent<TweenAlpha>();
		alpha1.style = UITweener.Style.Once;
//		alpha.delay = 0.1f;
		alpha1.duration = 0.4f*speedRate;
		alpha1.from = 1f;
		alpha1.to = 1;
		alpha1.onFinished += layer4finish1;
		addNewPlayer(alpha1,0.1f);//.Play(true);
	}
	
	private void layer4finish1(UITweener tween)
	{
		TweenPosition pos = layer4_2.AddComponent<TweenPosition>();
		pos.style = UITweener.Style.Once;
//		pos.delay = 0.5f;
		pos.duration = 0.4f*speedRate;
		pos.from = layer4_2position;
		pos.to = new Vector3(layer4_2position.x,layer4_2position.y + 70,layer4_2position.z);
		pos.onFinished = finishLayer;
		addNewPlayer(pos,0.5f);//.Play(true);		
	}
	private void playLayer3()
	{
		
//		layer3.SetActive(true);
		
		
		TweenScale scale = layer3.AddComponent<TweenScale>();
		scale.style = UITweener.Style.Once;
//		scale.delay = 0.5f;
		scale.duration = 0.4f*speedRate;
		scale.from = new Vector3(layer3_scale.x*0.53f,layer3_scale.y*0.53f,1);
		scale.to = new Vector3(layer3_scale.x,layer3_scale.y,1);
		layer3.transform.localScale = scale.from;
		addNewPlayer(scale,0.5f);//.Play(true);

		TweenAlpha alpha = layer3.AddComponent<TweenAlpha>();
		alpha.style = UITweener.Style.Once;
//		alpha.delay = 0.5f;
		alpha.duration = 0.4f*speedRate;
		alpha.from = 0.2f;
		alpha.to = 1;
		alpha.onFinished += finishLayer;
		addNewPlayer(alpha,0.5f);//.Play(true);	
		
		
		
	
	}
	private void finishLayer(UITweener tween)
	{
		tween.gameObject.SetActive(false);
	}

	private void playLayer2_3()
	{
		TweenAlpha alpha = layer2_3.AddComponent<TweenAlpha>();
		alpha.style = UITweener.Style.Once;
//		alpha.delay = 0.6f;
		alpha.duration = 0.4f*speedRate;
		alpha.from = 0f;
		alpha.to = 1;
		alpha.onFinished += layer2_3finish1;
		addNewPlayer(alpha,0.6f);//.Play(true);			
	}
	private void layer2_3finish1(UITweener tween)
	{
		TweenAlpha alpha = layer2_3.AddComponent<TweenAlpha>();
		alpha.style = UITweener.Style.Once;
		alpha.duration = 0.4f*speedRate;
		alpha.from = 1f;
		alpha.to = 0;
		alpha.onFinished += finishLayer;
		addNewPlayer(alpha,0);//alpha.Play(true);		
	}
	private void playLayer2()
	{

//		layer2_1.SetActive(true);
		TweenScale scale1 = layer2_1.AddComponent<TweenScale>();
		scale1.style = UITweener.Style.Once;
		scale1.duration = 0.2f*speedRate;
		scale1.from = new Vector3(layer2_1_scale.x,layer2_1_scale.y*0.49f,1);
		scale1.to = new Vector3(layer2_1_scale.x,layer2_1_scale.y*1.58f,1);
		addNewPlayer(scale1,0);//.Play(true);
		
//		layer2_2.SetActive(true);
		TweenScale scale2 = layer2_2.AddComponent<TweenScale>();
		scale2.style = UITweener.Style.Once;
		scale2.duration = 0.2f*speedRate;
		scale2.from = new Vector3(layer2_2_scale.x,layer2_2_scale.y*0.49f,1);
		scale2.to = new Vector3(layer2_2_scale.x,layer2_2_scale.y*1.58f,1);
		scale2.onFinished += layer2finish1;	
		addNewPlayer(scale2,0);//.Play(true);		
	
	}
	private void layer2finish1(UITweener tween)
	{
//		layer2_1.SetActive(true);
		TweenScale scale1 = layer2_1.AddComponent<TweenScale>();
		scale1.style = UITweener.Style.Once;
		scale1.duration = 0.2f*speedRate;
		scale1.from = new Vector3(layer2_1_scale.x,layer2_1_scale.y*1.58f,1);
		scale1.to = new Vector3(layer2_1_scale.x*0.066f,layer2_1_scale.y*1.24f,1);
		scale1.onFinished += finishLayer;	
		addNewPlayer(scale1,0);//.Play(true);	
		
//		layer2_2.SetActive(true);
		TweenScale scale2 = layer2_2.AddComponent<TweenScale>();
		scale2.style = UITweener.Style.Once;
		scale2.duration = 0.2f*speedRate;
		scale2.from = new Vector3(layer2_2_scale.x,layer2_2_scale.y*1.58f,1);
		scale2.to = new Vector3(layer2_2_scale.x*0.066f,layer2_2_scale.y*1.24f,1);
		scale2.onFinished += finishLayer;		
		addNewPlayer(scale2,0);//scale2.Play(true);		

		layer1finish1(tween);
	
	}

	private void  playLayer1()
	{
		Debug.Log("playLayer1");

		TweenScale layer1Scale = layer1.AddComponent<TweenScale>();
		layer1Scale.style = UITweener.Style.Once;
		layer1Scale.duration = 0.2f*speedRate;
		layer1Scale.from = new Vector3(layer1_scale.x,layer1_scale.y*0.49f,1);
		layer1Scale.to = new Vector3(layer1_scale.x,layer1_scale.y*1.58f,1);
//		layer1Scale.onFinished += layer1finish1;	
//		layer1Scale.Play(true);
		addNewPlayer(layer1Scale,0);
	}
	private void layer1finish1(UITweener tween)
	{
		Debug.Log("layer1finish1");
		TweenScale layer1Scale = layer1.AddComponent<TweenScale>();
		layer1Scale.style = UITweener.Style.Once;
		layer1Scale.duration = 0.2f*speedRate;
		layer1Scale.from = new Vector3(layer1_scale.x,layer1_scale.y*1.58f,1);
		layer1Scale.to = new Vector3(layer1_scale.x*0.066f,layer1_scale.y*1.24f,1);
		layer1Scale.onFinished += finishLayer;
		addNewPlayer(layer1Scale,0);
//		layer1Scale.Play(true);
	}


	
	
	
	
//------------再抽！！------------
	public void lotteryOne()
	{
		if(Obj_MyselfPlayer.GetMe().cardBagList.Count>=Obj_MyselfPlayer.GetMe().bagMax)
		{
//			BoxManager.showBagFullBox("您携带的侠士已经达到上限可以将侠士吸收、出售或者扩充您的背包.");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg74);
			return;
		}
		switch(lotteryType)
		{
		case LotteryController.LOTTERY_TYPE.FRIEND:
			if(Obj_MyselfPlayer.GetMe().fpoint<friendLotteryOneCost)
			{
//				BoxManager.showMessage("侠义点数不足");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg51);
				return;
			}
			break;
		case LotteryController.LOTTERY_TYPE.DIAMOND:
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
		case LotteryController.LOTTERY_TYPE.FRIEND:
			if(Obj_MyselfPlayer.GetMe().fpoint<friendLotteryTenCost)
			{
//				BoxManager.showMessage("侠义点数不足");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg51);
				return;
			}
			break;
		case LotteryController.LOTTERY_TYPE.DIAMOND:
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
//		mainLogic.OnLotteryAnimationWindow();
		if(isSuccess)
		{
			playAnimation();
		}
	}
	
	public void backToPreviousWindow()
	{
		mainLogic.OnShopWindow();
	}
	
	public List<LotteryPlayer> players = new List<LotteryPlayer>();
	public void addNewPlayer(UITweener tween,float delayTime)
	{
		Debug.Log("add new player:"+tween.gameObject.name);
		LotteryPlayer player = new LotteryPlayer(tween,delayTime*speedRate);
		players.Add(player);
	}
	public void updatePlayer()
	{
		List<LotteryPlayer> removes = new List<LotteryPlayer>();
		foreach(LotteryPlayer player in players)
		{
			if(player.startTime + player.delayTime <= timeCount)
			{
				player.tween.gameObject.SetActive(true);
				player.tween.Play(true);
//				player.obj.SendMessage("Play",true);
				removes.Add(player);
			}
		}
		foreach(LotteryPlayer removePlayer in removes)
		{
			players.Remove(removePlayer);
		}
		removes.Clear();
		removes = null;
	}
	
	
	
	public class LotteryPlayer
	{
		public UITweener tween;
		public float delayTime;
		public float startTime;
		public LotteryPlayer(UITweener tween,float delayTime)
		{
			this.tween = tween;
			this.delayTime = delayTime;
			this.startTime = LotteryAnimationController.timeCount;
		}
	}
	
	public void ShowCardDetail(GameObject button)
	{
		if(GuideManager.Instance.currentStep != GuideManager.GUIDE_STEP.LOTTERY)
		{
			BoxManager.showCardInfoMessage(-1,int.Parse(button.name));
		}
	}
	public void OnLuckyLottery(GameObject button)
	{
		NetworkSender.Instance().lotteryCard(lotteryCardDone,LotteryController.LOTTERY_TYPE.LUCKY,1);
	}
}
