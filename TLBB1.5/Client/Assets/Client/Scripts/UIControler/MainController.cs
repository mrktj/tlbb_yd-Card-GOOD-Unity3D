using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using card.net;
using GCGame.Table;
using System;
using System.Collections.Generic;
public class MainController : MonoBehaviour
{
    public GameObject userName;
    public GameObject userLevel;
    public GameObject userEXP;
    public GameObject userPower;
    public GameObject userMoney;
    public GameObject userDollar;
    public GameObject userPowerSlider;
    public GameObject timeInfo;
    //public GameObject bg;
    public GameObject topInfo;
    public UILabel labelTime;
    public UILabel labelTimeTitle;
    public UILabel lableTimeInfo;
    public UILabel lableExpInfo;
    // Use this for initialization
    public GameObject bottomBar;
    public GameObject topBar;
    public GameObject bottomInfo;

    //活动按钮
    public GameObject bg_Activity;
    public GameObject ActivityTopBar;
    public GameObject BaZhenTuBtn;
    public GameObject GGLBtn;
    public GameObject MonthCardBtn;
    public GameObject WorldBossBtn;
	public GameObject PataBtn;
	public GameObject ChangeCard;
    public GameObject[] BtnList;
    public GameObject worldBossActivePromopt;
	public GameObject feedBack;

    private int time = 0;
    private TimeSpan misTiming = new TimeSpan();
    private int addPower = 0;

    private MainUILogic mainLogic;
    void Start()
    {
        //更新用户数据
        updateUserInfo();
        //		cloneGuideItem();
        if (needFlashWulin)
        {
            startFlashWuLin();
        }
        if (needFlashLunJian)
        {
            startFlashLunJian();
        }
        mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
        //---------------------------新手引导中断参考变量----------------------------
        PlayerPrefs.SetInt("GUIDE_INT_TEMP", 0);
        Debug.Log("*** Set PlayerPrefs ***    ***GUIDE_INT_TEMP = 0***");
        //----------------------------------END-----------------------------------	
        //Obj_MyselfPlayer.GetMe().TestClearPlayerSet();
    }
    // Update is called once per frame
    void Update()
    {
        misTiming = DateTime.Now - GameManager.Instance.lastPowerTime;
        addPower = (int)(misTiming.TotalSeconds / 300);
        if (addPower > 0)
        {
            if (Obj_MyselfPlayer.GetMe().power + addPower <= TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue)
            {
                Obj_MyselfPlayer.GetMe().power += addPower;
            }
            else
            {
                Obj_MyselfPlayer.GetMe().power = TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue;
            }
            updateUserInfo();
            GameManager.Instance.lastPowerTime = GameManager.Instance.lastPowerTime.AddSeconds(addPower * 300);
        }
        time = 300 - (int)(misTiming.TotalSeconds % 300);
		if(labelTime != null)
		{
			labelTime.text = (time / 60).ToString("00") + ":" + (time % 60).ToString("00");
		}
		
		if(lableTimeInfo != null)
		{
			 lableTimeInfo.text = (DateTime.Now).ToString("HH:mm:ss");
		}
       
    }
    void OnEnable()
    {
        //---------------------------新手引导中断参考变量----------------------------
        PlayerPrefs.SetInt("GUIDE_INT_TEMP", 0);
        Debug.Log("*** Set PlayerPrefs ***    ***GUIDE_INT_TEMP = 0***");
        //----------------------------------END-----------------------------------	
		if(Obj_MyselfPlayer.GetMe().giftison == 1){
			feedBack.SetActive(true);
		}
    }

    public void updateUserInfo()
    {
        Obj_MyselfPlayer myPlayer = Obj_MyselfPlayer.GetMe();
        UILabel nameLabel = userName.GetComponent<UILabel>();
        nameLabel.text = myPlayer.accountName;

        UILabel levelLabel = userLevel.GetComponent<UILabel>();
        levelLabel.text = myPlayer.level.ToString();

        //根据等级得到经验表
        Tab_Idexperience tab_idexp = TableManager.GetIdexperienceByID(myPlayer.level);
		
		if(tab_idexp == null)
		{
			return;
		}
		
        UISlider expSlider = userEXP.GetComponent<UISlider>();
		if(expSlider != null 
			&& tab_idexp != null
			  && expSlider != null)
		{
			Debug.Log("myPlayer:");
			Debug.Log(myPlayer);
			Debug.Log("tab_idexp:");
			
			 expSlider.sliderValue = (float)myPlayer.exp / (float)tab_idexp.IDExperience;
		}
       
        //TODO:体力随等级提升,需要等策划配表
        UISlider powerSlider = userPowerSlider.GetComponent<UISlider>();
        powerSlider.sliderValue = (float)myPlayer.power / (float)tab_idexp.IDPhysicalValue;
        UILabel powerLabel = userPower.GetComponent<UILabel>();
        powerLabel.text = myPlayer.power.ToString() + "/" + tab_idexp.IDPhysicalValue;
        if (myPlayer.power >= tab_idexp.IDPhysicalValue)
        {
            labelTime.gameObject.SetActive(false);
            labelTimeTitle.gameObject.SetActive(false);
        }
        else
        {
            labelTime.gameObject.SetActive(true);
            labelTimeTitle.gameObject.SetActive(true);
        }

        UILabel moneyLabel = userMoney.GetComponent<UILabel>();
        moneyLabel.text = myPlayer.money.ToString();
        UILabel dollarLabel = userDollar.GetComponent<UILabel>();
        dollarLabel.text = myPlayer.dollar.ToString();


        Hashtable cardList = TableManager.GetCard();
        foreach (DictionaryEntry dic in cardList)
        {
            HeroInfo hero = new HeroInfo();
            hero.templateId = Convert.ToInt32(dic.Key);
            Obj_MyselfPlayer.GetMe().heroList.Add(hero);
        }
    }
    public void hideTopBar()
    {
        topBar.SetActive(false);
    }
    public void showTopBar()
    {
        topBar.SetActive(true);
    }
    public void hideBottomBar()
    {
        bottomBar.SetActive(false);
        bottomInfo.SetActive(false);
    }
    public void showBottomBar()
    {
        bottomBar.SetActive(true);
        bottomInfo.SetActive(true);
    }
    public void refreshTopBar()
    {
        updateUserInfo();
    }

    //检查顶上信息栏是否显示
    bool CheckIfTopinfoIsVisiable()
    {
        return topInfo.activeSelf;
    }

    public void showTime()
    {

        if (!CheckIfTopinfoIsVisiable())
        {
            return;
        }


        timeInfo.SetActive(true);
        Tab_Idexperience tab_idexp = TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level);
        lableExpInfo.text = "当前经验: " + Obj_MyselfPlayer.GetMe().exp + "/" + tab_idexp.IDExperience;
    }

    public void hideTime()
    {
        //王明磊 : 统计模块代码 -> Statistics
        PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn52).ToString());
        timeInfo.SetActive(false);
    }
    public static bool needFlashWulin = false;
    public void startFlashWuLin()
    {
        Debug.Log("startFlashWuLin");
        TweenScale scale = transform.FindChild("BottomBar/PVEBtn").gameObject.AddComponent<TweenScale>();//ayer9.AddComponent<TweenScale>();
        scale.style = UITweener.Style.PingPong;
        scale.duration = 0.5f;
        scale.from = new Vector3(1, 1, 1);
        scale.to = new Vector3(1.1f, 1.1f, 1.1f);
        scale.Play(true);
    }
    public static bool needFlashLunJian = false;
    public void startFlashLunJian()
    {
        Debug.Log("startFlashWuLin");
        TweenScale scale = transform.FindChild("BottomBar/PVPBtn").gameObject.AddComponent<TweenScale>();//ayer9.AddComponent<TweenScale>();
        scale.style = UITweener.Style.PingPong;
        scale.duration = 0.5f;
        scale.from = new Vector3(1, 1, 1);
        scale.to = new Vector3(1.1f, 1.1f, 1.1f);
        scale.Play(true);

        /*
        //for guide
        if(PlayerPrefs.HasKey("FlashPVEBtn")){
            PlayerPrefs.DeleteKey("FlashPVEBtn");
            if(PlayerPrefs.GetInt("FlashPVEBtn",-1) == 1){
                GuideManager.Instance.CheckStopFlashBtn(this.getPVEButton().gameObject);
            }
        }*/
    }

    //操作都放进自身方法里
    public void OnFriendWindow()
    {
        Debug.Log("OnFriendWindow");
        mainLogic.OnFriendWindow();
        Debug.Log("OnFriendWindow Done");
    }
    public void OnHelpWindow()
    {
        mainLogic.OnHelpWindow();
    }

    //显示顶上玩家信息
    public void ShowtopInfo()
    {
        //bg.SetActive(true);
        bg_Activity.gameObject.SetActive(false);
        topInfo.SetActive(true);
        ActivityTopBar.SetActive(false);
        this.updateUserInfo();
    }

    void ShowBaZhenTu()
    {
        mainLogic.OnShowBGZWindow();
    }

    void ShowGGL()
    {
        mainLogic.OnGGLWindow();
    }

    //显示活动界面顶部按钮
    public void ShowActivityTopUI(ActivityType type)
    {
        topInfo.SetActive(false);
        ActivityTopBar.SetActive(true);
		/*
		if (Obj_MyselfPlayer.GetMe().isChangeCardOpen)
			ChangeCard.SetActive(true);
		else
			ChangeCard.SetActive(false);
		*/
		//如果八阵图领取结束
        if (Obj_MyselfPlayer.GetMe().Flags == 255)
        {
//            ActivityTopBar.transform.FindChild("Grid/BaZhenTuBtn").gameObject.SetActive(false); //ChangeCard 滑动功能使用
			ActivityTopBar.transform.FindChild("BaZhenTuBtn").gameObject.SetActive(false);
        }
        else
        {
//            ActivityTopBar.transform.FindChild("Grid/BaZhenTuBtn").gameObject.SetActive(true); //ChangeCard 滑动功能使用
			ActivityTopBar.transform.FindChild("BaZhenTuBtn").gameObject.SetActive(true);
        }

        switch (type)
        {
            case ActivityType.E_ACTIVITY_TYPE_GGL:
                BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "bazhentu-icon";
                BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "GGLBtnChoose";
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
  				MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "huodonganniu_1";
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "qianchonglou_Icon02";
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "chumoweidao-icon";
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
//				ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "dianshichengjin";
//                ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				bg_Activity.gameObject.SetActive(true);
                break;
            case ActivityType.E_ACTIVITY_TYPE_BAZHENTU:
                bg_Activity.gameObject.SetActive(true);
                BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "BZTBtnChoose";
                BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "GGL-icon";
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "huodonganniu_1";
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "qianchonglou_Icon02";
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();   
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "chumoweidao-icon";
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
//				ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "dianshichengjin";
//				ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				break;
            case ActivityType.E_ACTIVITY_TYPE_MONTH_CARD:
                BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "bazhentu-icon";
                BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "GGL-icon";
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "huodonganniu_2";
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "qianchonglou_Icon02";
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();		
	            WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "chumoweidao-icon";
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
//				ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "dianshichengjin";
//                ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				bg_Activity.gameObject.SetActive(false);
                break;
            case ActivityType.E_ACTIVITY_TYPE_WORLD_BOSS:
                BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "bazhentu-icon";
                BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "GGL-icon";
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "huodonganniu_1";
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "qianchonglou_Icon02";
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();		
	            WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "chumoweidao_1";
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
//				ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "dianshichengjin";
//                ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				bg_Activity.gameObject.SetActive(false);
                break;
			case ActivityType.E_ACTIVITY_TYPE_PATA:
				BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "bazhentu-icon";
				BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "GGL-icon";
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "huodonganniu_1";
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "qianchonglou_Icon01";
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();		
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "chumoweidao-icon";
//				ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "dianshichengjin";
//                ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				bg_Activity.gameObject.SetActive(false);
				break;
			case ActivityType.E_ACTIVITY_TYPE_CHANGE_CARD:
				BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "bazhentu-icon";
				BaZhenTuBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "GGL-icon";
                GGLBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "huodonganniu_1";
                MonthCardBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "qianchonglou_Icon02";
				PataBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "chumoweidao-icon";
                WorldBossBtn.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
//				ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "dianshichengjin_chosen";
//				ChangeCard.transform.FindChild("Background").GetComponent<UISprite>().MakePixelPerfect();
				bg_Activity.gameObject.SetActive(false);
				break;
            default:
                break;
        }
        if (Obj_MyselfPlayer.GetMe().monthCardFlag != 0)
        {
            MonthCardBtn.gameObject.SetActive(true);
        }
        else
        {
            MonthCardBtn.gameObject.SetActive(false); 
        }
        if (Obj_MyselfPlayer.GetMe().level >= 20)
        {
            WorldBossBtn.gameObject.SetActive(true);
			if (Obj_MyselfPlayer.GetMe().worldBossActiveFlag == 0)
        	{
            	worldBossActivePromopt.SetActive(true);
        	}
        	else
        	{
           	 	worldBossActivePromopt.SetActive(false);
        	}
        }
        else
        {
            WorldBossBtn.gameObject.SetActive(false);
        }
        
        ChangeBtnUI();
    }

    public void ChangeBtnUI()
    {
        //根据状态变坐标
        int posX = -320;
        for (int i = 0; i < BtnList.Length; i++)
        {
            if (BtnList[i].activeSelf)
            {
                BtnList[i].transform.localPosition = new Vector3(posX, -70, 0);
                posX += 125;
            }
        }
    }

    public void ReturnToMainUI()
    {

        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE &&
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.END)
            GuideUpdate.Instance.NextStep();//卡牌升级指引阶段 SELECT_8
        /*
        else if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.STRENGTHEN)
            GuideStrengthen.Instance.NextStep();//修炼指引 SELECT_6
        */

        Debug.Log("ReturnToMainUI");
        mainLogic.ReturnToMainUI();
        Debug.Log("ReturnToMainUI Done");
    }
    public void LoadMainToPveSceneList()
    {
        Debug.Log("LoadMainToPveSceneList");

        switch (GuideManager.Instance.currentStep)
        {
            case GuideManager.GUIDE_STEP.COPY1_1:
                GuideCopy1_1.Instance.NextStep();//新手战斗引导 SELECT_1
                break;
            case GuideManager.GUIDE_STEP.COPY1_2:
                GuideCopy1_2.Instance.NextStep();//新手战斗引导 SELECT_1
                break;
            case GuideManager.GUIDE_STEP.COPY1_3:
                GuideCopy1_3.Instance.NextStep();//新手战斗引导 SELECT_1
                break;
            case GuideManager.GUIDE_STEP.COPY1_4:
                GuideCopy1_4.Instance.NextStep();//新手战斗引导 SELECT_1
                break;
            case GuideManager.GUIDE_STEP.COPY1_5:
                GuideCopy1_5.Instance.NextStep();//新手战斗引导 SELECT_1
                break;
            case GuideManager.GUIDE_STEP.COPY2_1:
                GuideCopy2_1.Instance.NextStep();//新手战斗引导 SELECT_1
                break;
            default:
                break;
        }

        mainLogic.LoadMainToPveSceneList();
        Debug.Log("LoadMainToPveSceneList Done");
    }
    public void OnPVPTotalWindow()
    {
        //16级开放pvp功能
        int openLev = 16;
        if (Obj_MyselfPlayer.GetMe().level < openLev)
        {
            BoxManager.showMessageByID((int)MessageIdEnum.Msg111, openLev.ToString());
            return;
        }
        Debug.Log("OnPVPWindow");
        mainLogic.OnPvPTotalWindow();
        Debug.Log("OnPVPWindow Done");
    }
    public void OnShopWindow()
    {
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.LOTTERY)
            GuideLottery.Instance.NextStep();//抽奖指引 SELECT_1
        Debug.Log("OnShopWindow");
        mainLogic.OnShopWindow();
        Debug.Log("OnShopWindow Done");
    }


    //打开八阵图
    public void OpenBaZhenTuBtnWindow()
    {
        mainLogic.OnShowBGZWindow();
    }

    //打开刮刮乐
    public void OpenGGLWindow()
    {
		ActivityTopBar.transform.FindChild("Grid").GetComponent<UIGrid>().repositionNow = true;
        mainLogic.OnGGLWindow();
    }

    //打开月卡
    public void OpenMonthCard()
    {
        mainLogic.OnMonthCardWindow();
    }
	
	public void OpenPata(){
		mainLogic.OnPataWindow();
	}

    //打开世界boss
    public void OpenWorldBoss()
    {
        NetworkSender.Instance().RequestWorldBossInfo(OpenWorldBossDone);
    }

    //打开世界boss
    public void OpenWorldBossDone(bool isSuccess)
    {
        mainLogic.OnWorldBossWindow();
    }
	
	public void OpenQxzbPvPBattleBefore()
	{
		mainLogic.OnPVPBattleBeforeController();
	}
	
	public void OpenQxzbInstruciton()
	{
		mainLogic.OnQxzbInstructionController();
	}
	
	public void OpenQxzbRewardInstruciton()
	{
		mainLogic.OnQxzbRewardInstructionController();
	}
	
	public void onPatafuction()
	{
		if(mainLogic != null)
		{
			Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.CHONG_LOU;
			mainLogic.OnBattleBefore();
		}
	}
	
	public void onChangeCard()
	{
		NetworkSender.Instance().AskChangeCardList(AskChangeCardListDone);
	}
	public void AskChangeCardListDone(bool isSuccess)
	{
		if (isSuccess && Obj_MyselfPlayer.GetMe().changeCardTimer > 0)
			mainLogic.OnChangeCardContoller();
		else if (Obj_MyselfPlayer.GetMe().changeCardTimer < 0)
			BoxManager.showMessageByID((int)MessageIdEnum.Msg208);
	}
	
    //for guide
    public Transform getPVEButton()
    {
        return transform.FindChild("BottomBar/PVEBtn");
    }
    public GameObject getShopButton()
    {
        return transform.FindChild("BottomBar/ShopBtn").gameObject;
    }
    public GameObject getHomeButton()
    {
        return transform.FindChild("BottomBar/HomeBtn").gameObject;
    }
    public GameObject getPVPButton()
    {
        return transform.FindChild("BottomBar/PVPBtn").gameObject;
    }
	
	public void OnFeedBackClick(){
		
		KDTLWebView.ShowWebView(ClientConfigure.getFacebookUrl());
	}
}
