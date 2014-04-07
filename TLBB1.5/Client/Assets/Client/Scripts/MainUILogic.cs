using UnityEngine;
using System.Collections;
using System.Collections.Generic;
////using Module.Log;
using card;
using Games.CharacterLogic;
using Games.LogicObject;
using card.net;
using GCGame.Table;
public class MainUILogic : MonoBehaviour {
	public GameObject windowPanel;
	private Dictionary<ChildIndex,GameObject> controllers;
	
	public GameObject mainController;
//	public GameObject battleEndWindow;
//	private GameObject pvelistinstance;
	static private ChildIndex lastPreviousWindowIndex= ChildIndex.NONE;
	private ChildIndex currentWindowIndex = ChildIndex.NONE;
	
    public enum ChildIndex
    {
		NONE = -1,
		
		MailController,
		
		CardInfoController,
        TeamController,
		HeroesController,
		ShopController,
		FriendController,
		FriendInfoController,
		SelectTeamHeaderController,
		SelectTeamMemberController,
		FriendShipSettleController,
		FriendAddController,
		CardSellController,
		TaskController,
		SendMailController,
		MailCheckController,
		HelpWindowController,
		SystemSetController,
		RaiderWindowController,
		ChangeNameController,
		BindAccountController,
		BindSuccessController,
		CYRegisterController,
		HeroInfoController,
		HeroInfoDetailController,
		
		PVEMainController,
		PVESubController,
		BattleBeforeController,
		SelectAssistController,
		BattelEndContorller,
		CardUpdateController,
		CardEvolutionController,
		CardStrengthenController,
		SelectHeroController,
		LotteryController,
		LotteryAnimationController,
		BattleFailController,
		PurchaseController,
		PvPShop,
		PvPController,
		QxzbPvPController,
		PvPTotalController,
		PurchaseRecordController,
		GGLController,
        MonthCardController,
		BaZhenTuController,
		ImproveController,
        SkillController,
        SelectSkillHeroController,
        SkillLearnController,
        SkillUpdateController,
        WorldBossController,
		PVPBattleBeforeController,
		PVPTeamController,
		PVPSelectTeamMemberController,
        WorldBossRankController,
        WorldBossAwardController,
		PataController,
		QxzbInstructionController,
		QxzbRewardInstructionController,
		ChangeCardController,
    }
	
	public UserFriend friendInfo;
	public int sendMailState;//1-from friend info; 2-from mail window
	
	public static bool needResetLogin = false;
	
	//新手引导第一次结束后，当次要一直让武林按钮闪烁
	public void flashWulin()
	{
		Debug.Log("flashWulin");
		MainController.needFlashWulin = true;
		MainController mc = mainController.GetComponent<MainController>();
		mc.startFlashWuLin();
	}
	public void flashLunJian()
	{
		Debug.Log("flashLunJian");
		MainController.needFlashLunJian = true;
		MainController mc = mainController.GetComponent<MainController>();
		mc.startFlashLunJian();
	}
	void Awake()
	{
//		GameManager.Instance.initGame();
		controllers = new Dictionary<ChildIndex,GameObject>();
		GuideManager.Instance.resetRootPanel();
	}

	// Use this for initialization
	void Start () {
        if (needResetLogin)
        {
            //用户超时重新登录返回，需要清除之前的数据，避免直接进入战斗结束画面
           // Obj_MyselfPlayer.GetMe().battleData = null;
            needResetLogin = false;
        }
		//²¥·ÅÖ÷œçÃæÒôÀÖ
		//AudioController.Play(SoundResource.SoundRes.background.ToString());
		AudioManager.Instance.PlayBackgroundMusic(SoundResource.SoundRes.background.ToString());

        bool isAfterBattle = false;
        if (Obj_MyselfPlayer.GetMe().battleData != null)
        {
            if (Obj_MyselfPlayer.GetMe().battleData.isPlayed)
            {
                Obj_MyselfPlayer.GetMe().battleData.isPlayed = true;

                //pvp战斗后，没有战斗结算界面				
                if (Obj_MyselfPlayer.GetMe().battleType == Games.Battle.BattleType.PVP)
                {
                    ActiveWindow(ChildIndex.PvPController);
                }//群雄争霸PvP战斗，后没有结算界面
				else if(Obj_MyselfPlayer.GetMe().battleType == Games.Battle.BattleType.QxzbPvP)
				{
					Obj_MyselfPlayer.GetMe().nQxzbIfShowFightReward = true;
					ActiveWindow(ChildIndex.QxzbPvPController);
					
				}
                else
                {
                    mainController.SendMessage("hideBottomBar");
                    Debug.Log("xlym:ActiveWindow = BattelEndContorller");
                    ActiveWindow(ChildIndex.BattelEndContorller);
                }

				isAfterBattle=true;
//				for (int i = 0; i < childControllers.Length; i++)
//	            {
//	                childControllers[i].SetActive(false);
//	            }
//				GameObject battleEnd = (GameObject)Instantiate(battleEndWindow);
//				battleEnd.transform.parent = mainController.transform.parent;
//				battleEnd.transform.localPosition = new Vector3(0,0,0);
//				battleEnd.transform.localScale = new Vector3(1,1,1);
//				battleEnd.GetComponent<BattleEndController>().mainUILogic = gameObject;
				
//				battleEnd.GetComponent<BattleEndController>().returnFunctionName = "LoadPveBossList";
			}
//			return;
		}else
		{
			Obj_MyselfPlayer.GetMe().battleData = new BattleRoundData();
		}
		
		Debug.Log("xlym:isAfterBattle = "+isAfterBattle);
		if(!isAfterBattle)
		{
            CheckPublicNoticeShow();
			//Èç¹ûÃ»ÓÐÉÏÃæµÄÕœ¶·£¬²ÅœøÈëteamÒ³Ãæ£¬±ÜÃâÁœŽÎÇÐ»»Ò³Ãæ//
			Debug.Log("xlym:ActiveWindow = TeamController");
			ActiveWindow(ChildIndex.TeamController);
			if(Obj_MyselfPlayer.GetMe().isSuspendinpata == true){
				ActiveWindow(ChildIndex.PataController);
				Obj_MyselfPlayer.GetMe().isSuspendinpata = false;
			}
		}
		
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void refreshTopBar()
	{
		mainController.SendMessage("refreshTopBar");
	}
	
	//获得上一次窗口的索引
	public ChildIndex GetLastWindowIndex()
	{
		return lastPreviousWindowIndex;
	}
	
	//»ØµœÖ÷œçÃæ--
    public void ReturnToMainUI()
    {
		//王明磊 : 统计模块代码 -> Statistics
		//如果不是Guide阶段,需要统计此按钮的点击信息
		if (!GameManager.Instance.isGuideMode())
		{
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn38).ToString());
		}
//		mainController.SendMessage("showBottomBar");
		mainController.SendMessage("showBottomBar");
		Obj_MyselfPlayer.GetMe().ResetCultivateData();
        ActiveWindow(ChildIndex.TeamController);
//        mainController.GetComponent<MainController>().ShowtopInfo();
		mainController.SendMessage("ShowtopInfo");
    }

    void LoadCardInfoUI()
    {
		CardInfoController.ShowCardInfo();
        //ActiveWindow(ChildIndex.CardInfoController);
    }
	
	public void onHeroWindow()
	{
		ActiveWindow(ChildIndex.HeroesController);
	}
		
	void OnTeamSlotChanged(GameObject gameobject)
	{
		UISprite portraiticon = gameobject.GetComponentsInChildren<UISprite>()[2];
		portraiticon.spriteName = "touxiang_33";
	}
	
	//æåŒææUI
	public void LoadBattleUIScene()
	{
		LoadingSliderKit.AsycLoadScene(Utils.UI_NAME_Battle,RollingNotice.GetRandomNotice());
		//LoadingSliderKit.sceneNameToLoad = Utils.UI_NAME_Battle;
		//Application.LoadLevel("loading");
		//GameManager.Instance.LoadLevel(Utils.UI_NAME_Battle);
	}
	
	//Žóž±±Ÿ--
    void LoadPveSceneList()
    {
        ActiveWindow(ChildIndex.PVEMainController);
    }
	
	public void LoadMainToPveSceneList()
    {
		//王明磊 : 统计模块代码 -> Statistics
		//如果不是Guide阶段,需要统计此按钮的点击信息
		if (!GameManager.Instance.isGuideMode())
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn39).ToString());
		Obj_MyselfPlayer.GetMe().copyType=CopyType.NORMAL;
        ActiveWindow(ChildIndex.PVEMainController);
    }
	
	public void onBattleFailWindow()
	{
		ActiveWindow(ChildIndex.BattleFailController);
	}
	
	//Ð¡ž±±Ÿ--
	public void LoadPveBossList()
    {
        ActiveWindow(ChildIndex.PVESubController);
		if(Obj_MyselfPlayer.GetMe().level == 6
			&&Obj_MyselfPlayer.GetMe().lastLevel == 5
			&& !PlayerPrefs.HasKey(Obj_MyselfPlayer.GetMe().accountID.ToString()+"_huodong_daobaifeng")
		){
			
			PlayerPrefs.SetInt(Obj_MyselfPlayer.GetMe().accountID.ToString()+"_huodong_daobaifeng",1);
			PlayerPrefs.Save();
			Debug.Log("_huodong_daobaifeng");
			PopGetHuoDongBox();
		}		
    }
	
	#region huodong
	void PopGetHuoDongBox(){
		NetworkSender.Instance().RequestHuodongGift(CallBackGitHuoDong);

		//test
		/*
		Obj_MyselfPlayer.GetMe().HasHuodong = true;
		Obj_MyselfPlayer.GetMe().HuodongMiaoshu = "这是测试,淡定";
		Obj_MyselfPlayer.GetMe().yunyinHuoDongLists.Add(new YunyingHuodong(1,1086,1));
		Obj_MyselfPlayer.GetMe().yunyinHuoDongLists.Add(new YunyingHuodong(1,1081,1));
		CallBackGitHuoDong(true);		
		*/
	}
	
	public void CallBackGitHuoDong(bool success){
		if(success){
			if(Obj_MyselfPlayer.GetMe().HasHuodong){
				OnShowCardGift();
			}else{
				Debug.Log("_huodong_daobaifeng no");
			}
		}else{
			Debug.Log("_huodong_daobaifeng error");
		}
	}
	
//	private static string cardHuodongMiaoshu1 = "我乃是大理鎮南王段正淳,看少俠骨骼驚奇,必是萬中無一的絕世高手,讓我來助少俠一臂之力!";
//	private static string cardHuodongMiaoshu2 = "妾身乃是大理鎮南王的妻子刀白鳳,道號“玉虛散人”,我和段郎合璧可施展合體技【愛恨交加】,威力不可小覷哦!";
	public void OnShowCardGift(){
		if(Obj_MyselfPlayer.GetMe().yunyinHuoDongLists.Count != 0){
			YunyingHuodong yunyinghuodong = Obj_MyselfPlayer.GetMe().yunyinHuoDongLists[0];
			Obj_MyselfPlayer.GetMe().yunyinHuoDongLists.RemoveAt(0);
			
			if(yunyinghuodong.ItemType == 1)// 类型，1，卡牌，2，金钱，3，道具， 4，元宝
			{
				GuideDirector.Instance.guidePlayer = GuideDirector.Instance.transform.FindChild("GuidePlayer").GetComponent<GuidePlayer>();
				GuideDirector.Instance.guidePlayer.SetPlayer(yunyinghuodong.ItemValue);
				string str;
				if(yunyinghuodong.ItemValue >= 1079 && yunyinghuodong.ItemValue <= 1083){
					//str = cardHuodongMiaoshu1;
					str = LanguageManger.GetWords(180000);
				}else if(yunyinghuodong.ItemValue >= 1084 && yunyinghuodong.ItemValue <= 1086){
					//str = cardHuodongMiaoshu2;
					str = LanguageManger.GetWords(180001);
				}else{
					str = LanguageManger.GetWords( TableManager.GetAppearanceByID(TableManager.GetCardByID(yunyinghuodong.ItemValue).Appearance).Story );
				}
				
				GuideDirector.Instance.guidePlayer.ShowLabel_temp(str);
				GuideDirector.Instance.guidePlayer.SetZhaoActive(true);

				if(Obj_MyselfPlayer.GetMe().yunyinHuoDongLists.Count != 0){
					GuideDirector.Instance.guidePlayer.nextStep = OnShowCardGift;
				}else{ 
					GuideDirector.Instance.guidePlayer.nextStep = OnCloseGuide;
				}
			}else{
				if(Obj_MyselfPlayer.GetMe().yunyinHuoDongLists.Count != 0){
					OnShowCardGift();
				}else{
					GuideDirector.Instance.guidePlayer.nextStep = OnCloseGuide;
				}
			}

		}
	}
	
	public void OnCloseGuide(){
		GameObject huodongOver = GuideDirector.Instance.gameObject;
		Destroy(huodongOver);

		BoxManager.show((int)BoxManager.MessageType.NONE,Obj_MyselfPlayer.GetMe().HuodongMiaoshu,ClientConfigure.title);
		UIEventListener.Get(BoxManager.buttonYes).onClick += UpdateUserData;
		
	}
	
	
	public void UpdateUserData(GameObject go){
		NetworkSender.Instance().GetUserInfo(OnGotoriendShipSettleWindow);
	}
	
	private void OnGotoriendShipSettleWindow(bool success){
		if(success){
			Debug.Log("Update User Info ok");
		}else{
			Debug.LogError("Update User Info error");
		}
	}	
	#endregion	
	
	
	//åæ--
	public void onUpdateWindow()
	{
		ActiveWindow(ChildIndex.CardUpdateController);
	}
	//è¿å--
	public void onEvolutionWindow()
	{
		ActiveWindow(ChildIndex.CardEvolutionController);
	}
	//åŒºå--
	public void OnCardStrengthenWindow()
	{
		ActiveWindow(ChildIndex.CardStrengthenController);
	}
	
	public void OnCultivateSelectWindow()
	{
		ActiveWindow(ChildIndex.SelectHeroController);
	}
	//éæ©å©æå¥œå--
	void OnSelectAssistWindow()
	{
		NetworkSender.Instance().askRandomAssistanceList(OnSelectAssistWindowDone);
	}
	void OnSelectAssistWindowDone(bool bSuccess)
	{
		ActiveWindow(ChildIndex.SelectAssistController);
	}
	public void OnBattleBefore()
	{
		ActiveWindow(ChildIndex.BattleBeforeController);
	}
	
	void OnPvPShop()
	{
		ActiveWindow(ChildIndex.PvPShop);
	}
	
	public void OnShopWindow()
	{
		mainController.SendMessage("showTopBar");//.showTopBar();
		mainController.SendMessage("showBottomBar");//.showBottomBar();	
		//王明磊 : 统计模块代码 -> Statistics
		//如果不是Guide阶段,需要统计此按钮的点击信息
		if (!GameManager.Instance.isGuideMode())
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn41).ToString());
		ActiveWindow(ChildIndex.ShopController);
	}
	
	public void OnPurchaseRecordWindow()
	{
		ActiveWindow(ChildIndex.PurchaseRecordController);
	}
	
	public void OnPurchaseWindow()
	{
		ActiveWindow(ChildIndex.PurchaseController);
	}

	public void OnFriendWindow()
	{
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn42).ToString());
		NetworkSender.Instance().getFriendsList(OnFriendWindowDone);
	}
	public void OnFriendWindowDone(bool bSuccess)
	{
		UnActiveWindow(ChildIndex.FriendController);
		ActiveWindow(ChildIndex.FriendController);
	}
	
	public void OnGGLWindow()
	{
		ActiveWindow(ChildIndex.GGLController);
	}
	
	void LoadFriendInfoWindow()
	{
		ActiveWindow(ChildIndex.FriendInfoController);
	}

//	void OnBattleBeforeDone(bool bSuccess)
//	{
//		
//		
//	}
	
	public void LoadSelectHeaderWindow()
	{
		ActiveWindow(ChildIndex.SelectTeamHeaderController);
	}
	
	public void LoadPvPSelectHeaderWindow()
	{
		PVPSelectTeamMemberController.isSelectLeader = true;
		ActiveWindow(ChildIndex.PVPSelectTeamMemberController);
	}
	
	public void LoadSelectMemberWindow()
	{
		ActiveWindow(ChildIndex.SelectTeamMemberController);
	}
	
	public void LoadPvPSelectMemberWindow()
	{
		PVPSelectTeamMemberController.isSelectLeader = false;
		ActiveWindow(ChildIndex.PVPSelectTeamMemberController);
	}
	
	public void OnFriendShipSettleWindow()
	{
		ActiveWindow(ChildIndex.FriendShipSettleController);
	}
	void OnFriendAddWindow()
	{
		ActiveWindow(ChildIndex.FriendAddController);
	}
	void OnCardSellWindow()
	{
		ActiveWindow(ChildIndex.CardSellController);
	}
	public void OnTaskInfoWindow()
	{
		ActiveWindow(ChildIndex.TaskController);
	}

	public void OnSendMailWindow()
	{
		ActiveWindow(ChildIndex.SendMailController);
	}
	public void OnMailWindow()
	{
		ActiveWindow(ChildIndex.MailController);
	}
	public void OnImproveWindow()
	{
		ActiveWindow(ChildIndex.ImproveController);
	}

	public void OnMailCheckWindow()
	{
		ActiveWindow(ChildIndex.MailCheckController);
	}
	public void OnHelpWindow()//2013-7-31 Jack Wen
	{
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn43).ToString());
		//BoxManager.showMessage("¹ŠÄÜÔÝÎŽ¿ª·Å");
		ActiveWindow(ChildIndex.HelpWindowController);
	}
	public void OnSystemSet()//2013-7-31 Jack Wen 
	{
		ActiveWindow(ChildIndex.SystemSetController);
	}
	public void OnRaider()//2013-7-31 Jack Wen
	{
		ActiveWindow(ChildIndex.RaiderWindowController);
	}
    public void OnCard() 
    {
        //todo:
    }
	public void OnChangeName()//2013-7-31 Jack Wen
	{
		ActiveWindow(ChildIndex.ChangeNameController);
	}
	public void OnBindAccount()//2013-8-7 Jack Wen
	{
		ActiveWindow(ChildIndex.BindAccountController);
	}
	
	public void OnBindSuccess()//2013-8-7 Jack Wen
	{
		ActiveWindow(ChildIndex.BindSuccessController);
	}
	public void OnRegisterController(){//2013-8-19 Jack Wen
		ActiveWindow(ChildIndex.CYRegisterController);
	}
	public void OnHeroInfoWindow()
	{
		ActiveWindow(ChildIndex.HeroInfoController);
	}
	public void OnHeroInfoDetailWindow()
	{
		ActiveWindow(ChildIndex.HeroInfoDetailController);
	}
	public void OnPVPWindow()
	{
		ActiveWindow(ChildIndex.PvPController);
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn40).ToString());
		//BoxManager.showMessage("功能暂未开放");   
		//BoxManager.showMessageByID((int)MessageIdEnum.Msg18);
	}
	public void OnPvPTotalWindow()
	{
		ActiveWindow(ChildIndex.PvPTotalController);
	}
	public void OnQxzbPvPWindow()
	{
		ActiveWindow(ChildIndex.QxzbPvPController);
	}
	
	public LotteryController.LOTTERY_TYPE lotteryType =LotteryController.LOTTERY_TYPE.NONE;
	public void OnFriendLotteryWindow()
	{
		lotteryType = LotteryController.LOTTERY_TYPE.FRIEND;
		ActiveWindow(ChildIndex.LotteryController);
		
	}
	public void OnDiamondLotteryWindow()
	{
		lotteryType = LotteryController.LOTTERY_TYPE.DIAMOND;
		ActiveWindow(ChildIndex.LotteryController);
		
	}
	public void OnLotteryAnimationWindow()
	{
		ActiveWindow(ChildIndex.LotteryAnimationController);
	}

    public void OnMonthCardWindow()
    {
        ActiveWindow(ChildIndex.MonthCardController);
    }
	
	public void OnPataWindow(){
		ActiveWindow(ChildIndex.PataController);
	}
	
	public void OnShowBGZWindow()
	{
		ActiveWindow(ChildIndex.BaZhenTuController);
	}
	
	public void RefreshRecordList()	{		if(controllers.ContainsKey(ChildIndex.PurchaseRecordController))		{			PurchaseRecordController prc = controllers[ChildIndex.PurchaseRecordController].GetComponent<PurchaseRecordController>();			if(prc != null)			{				prc.FreshUI();			}		}		if(controllers.ContainsKey(ChildIndex.PurchaseController))		{			PurchaseController pc = controllers[ChildIndex.PurchaseController].GetComponent<PurchaseController>();			if(pc != null)			{				pc.UpdateFirst();				pc.RefreshUI(false);			}		}	}
    public void OnSkillWindow()
    {
        ActiveWindow(ChildIndex.SkillController);
    }

    public void OnSelectSkillHeroWindow()
    {
        ActiveWindow(ChildIndex.SelectSkillHeroController);
    }

    public void OnSkillLearnWindow()
    { 
        ActiveWindow(ChildIndex.SkillLearnController);
    }

    public void OnSkillUpdateWindow()
    {
        ActiveWindow(ChildIndex.SkillUpdateController);
    }

    public void OnWorldBossWindow()
    {
        ActiveWindow(ChildIndex.WorldBossController);
    }
	
	public void OnPVPTeamWindow()
	{
		ActiveWindow(ChildIndex.PVPTeamController);
	}

    public void OnWorldBossRankWindow()
    {
        ActiveWindow(ChildIndex.WorldBossRankController);
    }

    public void OnWorldBossAwardWindow()
    {
        ActiveWindow(ChildIndex.WorldBossAwardController);
    }
	
    private void ActiveWindow(ChildIndex index)
    {
// 		if(index == ChildIndex.GGLController)
// 		{
// 			MainController mc = mainController.GetComponent<MainController>();
// 			mc.ShowGGL();
// 		}
// 		else
// 		{
// 			MainController mc = mainController.GetComponent<MainController>();
// 			mc.ShowtopInfo();
// 		}
		
		foreach (ChildIndex key in controllers.Keys)
	    {
	        if (index != key)
	        {
                //controllers[key].SendMessage("CloseWindow", SendMessageOptions.DontRequireReceiver);
                controllers[key].SetActive(false);
	        }
	    }
		
		
		if(!controllers.ContainsKey(index))
		{
            GameObject window = ResourceManager.Instance.LoadWindow(index.ToString());
            controllers.Add(index, window);
            window.transform.parent = windowPanel.transform;
            window.transform.localScale = Vector3.one;
            window.transform.localPosition = Vector3.zero;
		}
//		else
//		{
//			controllers[index].SetActive(false);
//		}
	    
		
		foreach (ChildIndex key in controllers.Keys)
	    {
	        if (index == key)
	        {
				lastPreviousWindowIndex = currentWindowIndex;
				currentWindowIndex = index;
				controllers[key].transform.localPosition = Vector3.zero;
				controllers[key].SetActive(true);
				break;
	        }
	    }
//        for (int i = 0; i < childControllers.Length; i++)
//        {
//            if (i == index)
//            {
//				//ËùÓÐµÄ¶ŒÒÔ0,0,0µãÎª»ù×Œ£¬²»ÓÃÃ¿žö¶ŒœšÊý×é
////                childControllers[i].transform.localPosition = Vector3.zero;//arrayChildInpos[i];
//				childControllers[i].transform.position = Vector3.zero;
//				childControllers[i].SetActive(true);
//				lastPreviousWindowIndex = currentWindowIndex;
//				currentWindowIndex = i;
//            }
//            else
//            {
////				Debug.Log("set active false:"+i);
//                childControllers[i].SetActive(false);
//            }
//        }
    }
	public void backToPreviousWindow()
	{
		if(lastPreviousWindowIndex!=ChildIndex.NONE)
		{
			ActiveWindow(lastPreviousWindowIndex);
		}
	}
	
	public GameObject getController(ChildIndex index)
	{
		if(controllers.ContainsKey(index))
		{
			return controllers[index];
		}else
		{
			return null;
		}
	}
	public ChildIndex getCurrentWindowIndex()
	{
		return this.currentWindowIndex;
	}


    //ÉèÖÃÖ÷œçÃæµ×²¿°ŽÅ¥ÏÔÊŸ×ŽÌ¬
    public void SetMainUIBottomBarActive(bool bActive)
    {
        if (bActive)
        {
            mainController.SendMessage("showBottomBar");
        } 
        else
        {
            mainController.SendMessage("hideBottomBar");
        }
    }
	public void SetNoticeActive(bool bActive)
    {
        if (bActive)
        {
            mainController.SendMessage("showNoticeBar");
        } 
        else
        {
            mainController.SendMessage("hideNoticeBar");
        }
    }
	private void UnActiveWindow(ChildIndex index){
		if(!controllers.ContainsKey(index))
		{
			return;
		}
		foreach (ChildIndex key in controllers.Keys)
	    {
	        if (index == key)
	        {
				controllers[key].SetActive(false);
	        }
	    }
	}
	
	static public ChildIndex GetLastWindow()
	{
		Debug.Log(lastPreviousWindowIndex);
		return lastPreviousWindowIndex;
	}

    public void CheckPublicNoticeShow() 
    {
        if (!Obj_MyselfPlayer.ShowedPublicNotice && !GameManager.Instance.isGuideMode())
        {
            Obj_MyselfPlayer.ShowedPublicNotice = true;
			Debug.Log("Enter define of unity_iphone");
#if UNITY_ANDROID
			StartCoroutine("showNotice");
#else
			KDTLWebView.ShowPreWebView();
//			WebMediator.Show();
//            KDTLWebView.ShowWebView(DeviceHelper.GetNoticeUrl(), KDTLWebView.webViewMode.NOTICE);
#endif
        }
    } 

	void OnDestroy()
	{
		//AccountManager.Instance.DestroyAllUI();
//		ResourceManager.Instance.Clean();
//		AtlasManager.Instance.Clean();
	}
#if UNITY_ANDROID
	IEnumerator showNotice()
	{
		yield return new WaitForSeconds(1f);
		KDTLWebView.ShowWebView(DeviceHelper.GetNoticeUrl(), KDTLWebView.webViewMode.NOTICE);
	}
#endif
	
	public void OnPVPBattleBeforeController()
	{
		ActiveWindow(ChildIndex.PVPBattleBeforeController);
	}
	
	public void OnQxzbInstructionController()
	{
		ActiveWindow(ChildIndex.QxzbInstructionController);
	}
	
	public void OnQxzbRewardInstructionController()
	{
		ActiveWindow(ChildIndex.QxzbRewardInstructionController);
	}
	
	public void OnChangeCardContoller()
	{
		ActiveWindow(ChildIndex.ChangeCardController);
	}
}
