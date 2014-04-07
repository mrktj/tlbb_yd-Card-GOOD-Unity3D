using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using card.net;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using System;
using Games.Battle;

public class BattleEndController : MonoBehaviour
{

    private GameObject mainUILogic;
    public UILabel mainCopy;
    public UILabel subCopy;
    public UILabel moneyCount;
    public UILabel expCount;
    public UILabel levelValue;
    public UISprite trophy;
    public UISlider expProgress;
    public GameObject BattleTrophys;
    public GameObject[] TrophyItems;
    public GameObject backGround;
    public GameObject levUpData;
    public GameObject normalBattle;
    public GameObject worldBossBattle;
    public GameObject pataBattle;
    public UILabel pataMoney;
    public UILabel pataRuby;
    public UILabel pataFloor;
    public GameObject pataContinueBtn;
    public GameObject pataBackBtn;
    public UILabel bossDamage;
    public UILabel bossGetMoney;
    public UILabel worldBossName;

    private List<DropBag> dropBags;
    private int moneyMax = 0;
    private int moneyNow = 0;
    private int moneyCut = 0;
    private int expMax = 0;
    private int expNow = 0;
    private int expCut = 0;
    private int damageMax = 0;
    private int damageNow = 0;
    private int damageCut = 0;
    private int nexLevelExp = 0;
    private int goodIndex = 0;
    private int lastLevelExp = 0;
    private List<GameObject> goodsItems = new List<GameObject>();
    private bool AniStart = false;
    private int nowLev = 0;
    public GameObject Stars;
    public UILabel[] Fragments;

    // Use this for initialization
    void Start()
    {
        mainUILogic = GameObject.Find("MainUILogic");
        bool isOpened = false;
        foreach (KeyValuePair<int, int> item in FengShuiData.Instance().WuxingInfor)
        {
            if (item.Value > 0)
            {
                isOpened = true;
                break;
            }
        }
        if (!isOpened && FengShuiData.Instance().star >= 35)
        {
            if (!PlayerPrefs.HasKey(Obj_MyselfPlayer.GetMe().accountID.ToString() + "_FengShuiHadOpen"))
            {
                BoxManager.showMessageByID((int)MessageIdEnum.Msg184);
                PlayerPrefs.SetInt(Obj_MyselfPlayer.GetMe().accountID.ToString() + "_FengShuiHadOpen", 1);
                UIEventListener.Get(BoxManager.buttonYes).onClick += GoFengShuiWindow;
            }
        }
    }
    public void GoFengShuiWindow(GameObject button)
    {
        MainUILogic mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
        mainLogic.OnImproveWindow();
    }
    void OnEnable()
    {
        mainUILogic = GameObject.Find("MainUILogic");
        Debug.Log("xlym: OnEnable BattleEndController");
        if (Obj_MyselfPlayer.GetMe().battleData.isPlayed)
        {
            if (Obj_MyselfPlayer.GetMe().battleType == BattleType.WORLD_BOSS)
            {
                normalBattle.SetActive(false);
                worldBossBattle.SetActive(true);
                pataBattle.SetActive(false);
                if (Obj_MyselfPlayer.GetMe().battleData.isWin)
                {
                    worldBossName.text = Obj_MyselfPlayer.GetMe().lastKillInfo.lastBossInfo.name;
                }
                else
                {
                    worldBossName.text = Obj_MyselfPlayer.GetMe().activeBoss.name;
                }
                UIEventListener.Get(backGround).onClick += OnWorldBossController;
                moneyMax = Obj_MyselfPlayer.GetMe().worldBossRewardMoney;
                moneyCut = moneyMax / 50;
                if (moneyCut == 0)
                {
                    moneyCut = 1;
                }
                damageMax = (int)Obj_MyselfPlayer.GetMe().worldBossCurrentDamage;
                damageCut = damageMax / 50;
                if (damageCut == 0)
                {
                    damageCut = 1;
                }
            }
            else if (Obj_MyselfPlayer.GetMe().battleType == BattleType.CHONG_LOU)
            {
                normalBattle.SetActive(false);
                worldBossBattle.SetActive(false);
                pataBattle.SetActive(true);
                pataMoney.text = Obj_MyselfPlayer.GetMe().pataTotalRewardMoney.ToString();
                pataRuby.text = Obj_MyselfPlayer.GetMe().pataTotalRewardYuanbao.ToString();
                pataFloor.text = Obj_MyselfPlayer.GetMe().lastPataNum.ToString();


                UIEventListener.Get(pataBackBtn).onClick += OnPataBackBtn;
                if (Obj_MyselfPlayer.GetMe().pataTimes > 0)
                {
                    pataContinueBtn.collider.enabled = true;
                    pataContinueBtn.transform.FindChild("Sprite").GetComponent<UISprite>().spriteName = "jixutiaozhan_1";
                    pataContinueBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "anniu_1";
					UIEventListener.Get(pataContinueBtn).onClick += OnPataContinueBtn;
                }
                else
                {
					pataContinueBtn.collider.enabled = false;
                    pataContinueBtn.transform.FindChild("Sprite").GetComponent<UISprite>().spriteName = "jixutiaozhan_3";
                	pataContinueBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "anniu_3";
				}


            }
            else
            {
                worldBossBattle.SetActive(false);
                normalBattle.SetActive(true);
                pataBattle.SetActive(false);
                if (!Obj_MyselfPlayer.GetMe().battleData.isWin)
                {
                    Obj_MyselfPlayer.GetMe().battleData.addExp = 0;
                }
                if (Obj_MyselfPlayer.GetMe().exp - Obj_MyselfPlayer.GetMe().battleData.addExp < 0)
                {
                    nowLev = Obj_MyselfPlayer.GetMe().lastLevel;
                    levelValue.text = nowLev.ToString();
                    lastLevelExp = TableManager.GetIdexperienceByID((Obj_MyselfPlayer.GetMe().lastLevel)).IDExperience;
                    if (Obj_MyselfPlayer.GetMe().level == 16)
                    {
                        mainUILogic.SendMessage("flashLunJian");
                    }
                }
                else
                {
                    nowLev = Obj_MyselfPlayer.GetMe().level;
                    levelValue.text = nowLev.ToString();
                    lastLevelExp = TableManager.GetIdexperienceByID((Obj_MyselfPlayer.GetMe().level)).IDExperience;
                }
                expProgress.sliderValue = (float)Obj_MyselfPlayer.GetMe().lastExp / (float)lastLevelExp;
                nexLevelExp = TableManager.GetIdexperienceByID((Obj_MyselfPlayer.GetMe().level)).IDExperience;
                Debug.Log("nexLevelExp=" + nexLevelExp);
                mainCopy.text = LanguageManger.GetWords(TableManager.GetCopyByID(Obj_MyselfPlayer.GetMe().curMainCopy.copyId).Copyname);
                subCopy.text = LanguageManger.GetWords(TableManager.GetCopydetailByID(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID).Copyname);
                //expProgress.sliderValue = 0.8f;
                dropBags = Obj_MyselfPlayer.GetMe().battleData.winDropBags;
                BattleTrophys.gameObject.SetActive(false);
                if (Obj_MyselfPlayer.GetMe().battleData.isWin)
                {
                    AniStart = true;
                    moneyMax = 0;
                    foreach (DropBag bag in dropBags)
                    {
                        Debug.Log("bag.type=" + bag.type.ToString());
                        if (bag.type == DropType.MONEY && bag.val > 0)
                        {
                            moneyMax += bag.val;
                        }
                    }
                    Debug.Log("moneyMax=" + moneyMax);
                    moneyCut = moneyMax / 50;
                    if (moneyCut == 0)
                    {
                        moneyCut = 1;
                    }
                    //moneyCount.text = money.ToString();
                    expCount.text = Obj_MyselfPlayer.GetMe().battleData.addExp.ToString();
                    expMax = Obj_MyselfPlayer.GetMe().battleData.addExp;
                    expCut = expMax / 50;
                    if (expCut == 0)
                    {
                        expCut = 1;
                    }
                    //				BattleTrophys.gameObject.SetActive(true);
                    SetDropCard();
                }

                if (Obj_MyselfPlayer.GetMe().exp - Obj_MyselfPlayer.GetMe().battleData.addExp < 0)
                {
                    //升级
                    AudioManager.Instance.PlayEffectSound("upgrade");
                    levUpData.SetActive(true);
                    RefreshLevUpData();
                    trophy.spriteName = "shengji";
                    trophy.MakePixelPerfect();
                    UIEventListener.Get(backGround).onClick += OnMainWindow;
                }
                else
                {
                    AudioManager.Instance.PlayEffectSound("reward");
                    levUpData.SetActive(false);
                    if (Obj_MyselfPlayer.GetMe().newTemplateID.Count > 0)
                    {
                        //获得图鉴中没有的新卡牌进入卡牌展示界面
                        UIEventListener.Get(backGround).onClick += HeroInfoWindowClick;
                    }
                    else
                    {
                        UIEventListener.Get(backGround).onClick += OnConfirmButtonClick;
                    }
                    if (Obj_MyselfPlayer.GetMe().battleData.isWin)
                    {
                        trophy.spriteName = "zhandou_zhanlipin";
                        trophy.MakePixelPerfect();
                        BattleTrophys.gameObject.SetActive(true);
                    }
                }
                //星级
                int starLev = Obj_MyselfPlayer.GetMe().currentCopyStar;
                //显示星级
                for (int i = 1; i <= 3; i++)
                {
                    //判断星级
                    if (i <= starLev)
                    {
                        Stars.transform.FindChild("Sprite-Star" + i).gameObject.SetActive(true);
                        Stars.transform.FindChild("Star" + i + "Ani").gameObject.SetActive(true);
                    }
                    else
                    {
                        Stars.transform.FindChild("Sprite-Star" + i).gameObject.SetActive(false);
                        Stars.transform.FindChild("Star" + i + "Ani").gameObject.SetActive(false);
                    }
                }
                Stars.GetComponent<Animation>().Play();
                //碎片
                //clear
                for (int i = 0; i < Fragments.Length; i++)
                {
                    Fragments[i].text = "0";
                }
                List<FragmentInfo> fragementResult = new List<FragmentInfo>();
                if (Obj_MyselfPlayer.GetMe().currentFragmentList != null)
                {
                    foreach (var fragment in Obj_MyselfPlayer.GetMe().currentFragmentList)
                    {
                        bool isFind = false;
                        for (int i = 0; i < fragementResult.Count; i++)
                        {
                            if (fragementResult[i].id == fragment.id)
                            {
                                isFind = true;
                                fragementResult[i].num += fragment.num;
                                break;
                            }
                        }
                        if (!isFind)
                        {
                            FragmentInfo newone = new FragmentInfo();
                            newone.id = fragment.id;
                            newone.num = fragment.num;
                            fragementResult.Add(newone);
                        }
                    }
                }
                if (fragementResult != null)
                {
                    foreach (var fragment in fragementResult)
                    {
                        Fragments[fragment.id].text = fragment.num.ToString();
                    }
                }
            }
        }
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
    }

    void OnDisable()
    {
        moneyCount.text = "0";
        expCount.text = "0";
        moneyNow = 0;
        moneyMax = 0;
        moneyCut = 0;
        expNow = 0;
        expMax = 0;
        expCut = 0;
        damageMax = 0;
        damageNow = 0;
        damageCut = 0;
        goodsItems.Clear();
        goodIndex = 0;
        AniStart = false;
        if (Obj_MyselfPlayer.GetMe().battleType != BattleType.PVE)
        {
            mainUILogic.GetComponent<MainUILogic>().mainController.GetComponent<MainController>().showBottomBar();
        }
    }

    public void FinishBattleDone(bool bSuccess)
    {
        if (Obj_MyselfPlayer.GetMe().exp - Obj_MyselfPlayer.GetMe().battleData.addExp < 0)
        {
            UIEventListener.Get(backGround).onClick += OnMainWindow;
        }
        else
        {
            if (Obj_MyselfPlayer.GetMe().newTemplateID.Count > 0)
            {
                //获得图鉴中没有的新卡牌进入卡牌展示界面
                UIEventListener.Get(backGround).onClick += HeroInfoWindowClick;
            }
            else
            {
                UIEventListener.Get(backGround).onClick += OnConfirmButtonClick;
            }
        }
    }

    public void OnMainWindow(GameObject go)
    {
        Debug.Log("xlym: Click to LevelUp");
        AudioManager.Instance.PlayEffectSound("reward");
        UIEventListener.Get(backGround).onClick -= OnMainWindow;
        levUpData.SetActive(false);
        trophy.spriteName = "zhandou_zhanlipin";
        trophy.MakePixelPerfect();
        BattleTrophys.gameObject.SetActive(true);

        if (Obj_MyselfPlayer.GetMe().newTemplateID.Count > 0)
        {
            //获得图鉴中没有的新卡牌进入卡牌展示界面
            UIEventListener.Get(backGround).onClick += HeroInfoWindowClick;
        }
        else
        {
            UIEventListener.Get(backGround).onClick += OnConfirmButtonClick;
        }
    }

    private void SetDropCard()
    {
        List<DropBag> drop_bags = new List<DropBag>();
        foreach (DropBag bag in dropBags)
        {
            if (bag.type == DropType.CARD || bag.type == DropType.TIEM)
            {
                drop_bags.Add(bag);
            }
        }

        if (drop_bags.Count > 0)
        {
            for (int i = 0; i < TrophyItems.Length; i++)
            {
                TrophyItems[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < drop_bags.Count; i++)
            {
                if (i >= TrophyItems.Length)
                {
                    Debug.LogError("drop bag too much !" + drop_bags.Count);
                    break;
                }

                if (drop_bags[i].type == DropType.CARD)
                {
                    TrophyItems[i].gameObject.SetActive(true);
                    TrophyItems[i].transform.FindChild("Sprite-Icon").gameObject.SetActive(true);
                    TrophyItems[i].transform.FindChild("Sprite-Item").gameObject.SetActive(true);
                    TrophyItems[i].GetComponent<CardIcon>().SetCardTemplateID(drop_bags[i].val);
                    TrophyItems[i].GetComponent<CardIcon>().cardIcon.transform.localScale = new Vector3(82, 82, 1);
                    if (Obj_MyselfPlayer.GetMe().ChangeHeroInfoState(drop_bags[i].val, 2))
                    {
                        //得到了图鉴里没有的英雄
                        //Obj_MyselfPlayer.GetMe().newTemplateID.Add(dropBags[i].val);
                    }
                }
                else if (drop_bags[i].type == DropType.TIEM)
                {
                    TrophyItems[i].gameObject.SetActive(true);
                    TrophyItems[i].transform.FindChild("Sprite-Icon").gameObject.SetActive(false);
                    TrophyItems[i].transform.FindChild("Sprite-Item").gameObject.SetActive(true);
                    TrophyItems[i].transform.FindChild("Sprite-Item").GetComponent<UISprite>().spriteName = TableManager.GetItemByID(drop_bags[i].val).Icon;
                    TrophyItems[i].transform.FindChild("Sprite-Item").transform.localScale = new Vector3(82, 82, 1);
                }
                goodsItems.Add(TrophyItems[i]);
            }
        }
        else
        {
            BattleTrophys.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Obj_MyselfPlayer.GetMe().battleType == BattleType.PVE)
        {
            if (moneyNow + moneyCut < moneyMax)
            {
                moneyNow += moneyCut;
                moneyCount.text = (moneyNow).ToString();
            }
            else
            {
                moneyCount.text = moneyMax.ToString();
            }
            if (expNow + expCut < expMax)
            {
                expNow += expCut;
                expCount.text = (expNow).ToString();
            }
            else
            {
                expCount.text = expMax.ToString();
            }
            if (goodIndex < goodsItems.Count)
            {
                if (goodIndex == 0)
                {
                    goodsItems[goodIndex].transform.FindChild("Sprite-Frame").GetComponent<TweenAlpha>().enabled = true;
                    goodsItems[goodIndex].transform.FindChild("Sprite-Icon").GetComponent<TweenAlpha>().enabled = true;
                    goodsItems[goodIndex].transform.FindChild("Sprite-Item").GetComponent<TweenAlpha>().enabled = true;
                    //goodsItems[goodIndex].GetComponent<TweenAlpha>().enabled=true;
                    goodIndex++;
                }
                else if (goodsItems[goodIndex - 1].transform.FindChild("Sprite-Frame").GetComponent<TweenAlpha>().alpha >= 0.8)
                {
                    goodsItems[goodIndex].transform.FindChild("Sprite-Frame").GetComponent<TweenAlpha>().enabled = true;
                    goodsItems[goodIndex].transform.FindChild("Sprite-Icon").GetComponent<TweenAlpha>().enabled = true;
                    goodsItems[goodIndex].transform.FindChild("Sprite-Item").GetComponent<TweenAlpha>().enabled = true;
                    //goodsItems[goodIndex].GetComponent<TweenAlpha>().enabled=true;
                    goodIndex++;
                }
            }
            if (AniStart)
            {
                if (nowLev < Obj_MyselfPlayer.GetMe().level)
                {
                    if (expProgress.sliderValue >= 1)
                    {
                        nowLev++;
                        levelValue.text = nowLev.ToString();
                        expProgress.sliderValue = 0;
                    }
                    expProgress.sliderValue += Time.deltaTime * 0.5f;
                }
                else if (nowLev == Obj_MyselfPlayer.GetMe().level)
                {
                    if (expProgress.sliderValue + Time.deltaTime * 0.5f < (float)Obj_MyselfPlayer.GetMe().exp / (float)TableManager.GetIdexperienceByID((Obj_MyselfPlayer.GetMe().level)).IDExperience)
                    {
                        expProgress.sliderValue += Time.deltaTime * 0.5f;
                    }
                    else
                    {
                        expProgress.sliderValue = (float)Obj_MyselfPlayer.GetMe().exp / (float)TableManager.GetIdexperienceByID((Obj_MyselfPlayer.GetMe().level)).IDExperience;
                        AniStart = false;
                    }
                }
            }
        }
        else if (Obj_MyselfPlayer.GetMe().battleType == BattleType.WORLD_BOSS)
        {
            if (moneyNow + moneyCut < moneyMax)
            {
                moneyNow += moneyCut;
                bossGetMoney.text = (moneyNow).ToString();
            }
            else
            {
                bossGetMoney.text = moneyMax.ToString();
            }
            if (damageNow + damageCut < damageMax)
            {
                damageNow += damageCut;
                bossDamage.text = (damageNow).ToString();
            }
            else
            {
                bossDamage.text = damageMax.ToString();
            }
        }
    }

    void OnConfirmButtonClick(GameObject go)
    {
        Debug.Log("xlym: Click to OnFriendShipSettleWindow");
        UIEventListener.Get(backGround).onClick -= OnConfirmButtonClick;
        //		mainUILogic.GetComponent<MainUILogic>().mainController.SetActive(true);
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_1_END){
            GuideManager.Instance.checkGuideState();
		}else if(Obj_MyselfPlayer.GetMe().level >= 16
			&&!PlayerPrefs.HasKey(Obj_MyselfPlayer.GetMe().accountID.ToString()+"_huodong_pingfen")
			&& 5 == Obj_MyselfPlayer.GetMe().curMainCopy.copyId
			&& 46 == Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID
		){
			PlayerPrefs.SetInt(Obj_MyselfPlayer.GetMe().accountID.ToString()+"_huodong_pingfen",1);
			PlayerPrefs.Save();			
			Debug.Log("pop goto love");
			LoveBoxPopUp();
		}
		else{
			mainUILogic.SendMessage("OnFriendShipSettleWindow");//, gameObject, SendMessageOptions.DontRequireReceiver);
//		Destroy(gameObject);		
		}
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY2_1)//处理最后一个新手副本断线重连
        {
            if (NetManager.is_relink == true)
            {
                GuideManager.Instance.currentStep = GuideManager.GUIDE_STEP.END;
                NetManager.is_relink = false;
            }
        }
    }

    void OnWorldBossController(GameObject go)
    {
        mainUILogic.SendMessage("OnWorldBossWindow");
    }

    void HeroInfoWindowClick(GameObject go)
    {
        UIEventListener.Get(backGround).onClick -= HeroInfoWindowClick;
        Obj_MyselfPlayer.GetMe().heroWindowState = 1;
        mainUILogic.SendMessage("OnHeroInfoDetailWindow");
    }

    private void RefreshLevUpData()
    {
        UILabel levelBefore = levUpData.transform.FindChild("Label-Level-Before").GetComponent<UILabel>();
        levelBefore.text = (Obj_MyselfPlayer.GetMe().lastLevel).ToString();
        UILabel powerBefore = levUpData.transform.FindChild("Label-Power-Before").GetComponent<UILabel>();
        powerBefore.text = Obj_MyselfPlayer.GetMe().lastPower.ToString();
        UILabel heroBefore = levUpData.transform.FindChild("Label-Hero-Before").GetComponent<UILabel>();
        heroBefore.text = Obj_MyselfPlayer.GetMe().lastHero.ToString();
        UILabel leaderShipBefore = levUpData.transform.FindChild("Label-LeaderShip-Before").GetComponent<UILabel>();
        leaderShipBefore.text = Obj_MyselfPlayer.GetMe().lastLeaderShip.ToString();
        UILabel friendBefore = levUpData.transform.FindChild("Label-Friend-Before").GetComponent<UILabel>();
        friendBefore.text = Obj_MyselfPlayer.GetMe().lastFriend.ToString();

        UILabel levelNow = levUpData.transform.FindChild("Label-Level-Now").GetComponent<UILabel>();
        levelNow.text = "[44af1d]" + Obj_MyselfPlayer.GetMe().level.ToString();
        UILabel powerNow = levUpData.transform.FindChild("Label-Power-Now").GetComponent<UILabel>();
        UISprite powerArrow = levUpData.transform.FindChild("Sprite-Power-Arrow").GetComponent<UISprite>();
        if (TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue > Obj_MyselfPlayer.GetMe().lastPower)
        {
            powerNow.text = "[44af1d]" + TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue.ToString() + "[000000]";
            powerArrow.spriteName = "zhandou_jiantou_lv";
        }
        else
        {
            powerNow.text = "[f1eccf]" + TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue.ToString() + "[000000]";
            powerArrow.spriteName = "zhandou_jiantou_hei";
        }
        UILabel heroNow = levUpData.transform.FindChild("Label-Hero-Now").GetComponent<UILabel>();
        UISprite heroArrow = levUpData.transform.FindChild("Sprite-Hero-Arrow").GetComponent<UISprite>();
        if (Obj_MyselfPlayer.GetMe().bagMax > Obj_MyselfPlayer.GetMe().lastHero)
        {
            heroNow.text = "[44af1d]" + Obj_MyselfPlayer.GetMe().bagMax.ToString() + "[000000]";
            heroArrow.spriteName = "zhandou_jiantou_lv";
        }
        else
        {
            heroNow.text = "[f1eccf]" + Obj_MyselfPlayer.GetMe().bagMax.ToString() + "[000000]";
            heroArrow.spriteName = "zhandou_jiantou_hei";
        }
        UILabel leaderShipNow = levUpData.transform.FindChild("Label-LeaderShip-Now").GetComponent<UILabel>();
        UISprite leaderShipArrow = levUpData.transform.FindChild("Sprite-LeaderShip-Arrow").GetComponent<UISprite>();
        if (Obj_MyselfPlayer.GetMe().leadership > Obj_MyselfPlayer.GetMe().lastLeaderShip)
        {
            leaderShipNow.text = "[44af1d]" + Obj_MyselfPlayer.GetMe().leadership.ToString() + "[000000]";
            leaderShipArrow.spriteName = "zhandou_jiantou_lv";
        }
        else
        {
            leaderShipNow.text = "[f1eccf]" + Obj_MyselfPlayer.GetMe().leadership.ToString() + "[000000]";
            leaderShipArrow.spriteName = "zhandou_jiantou_hei";
        }
        UILabel friendNow = levUpData.transform.FindChild("Label-Friend-Now").GetComponent<UILabel>();
        UISprite friendArrow = levUpData.transform.FindChild("Sprite-Friend-Arrow").GetComponent<UISprite>();
        if (Obj_MyselfPlayer.GetMe().friendNumMax > Obj_MyselfPlayer.GetMe().lastFriend)
        {
            friendNow.text = "[44af1d]" + Obj_MyselfPlayer.GetMe().friendNumMax.ToString() + "[000000]";
            friendArrow.spriteName = "zhandou_jiantou_lv";
        }
        else
        {
            friendNow.text = "[f1eccf]" + Obj_MyselfPlayer.GetMe().friendNumMax.ToString() + "[000000]";
            friendArrow.spriteName = "zhandou_jiantou_hei";
        }
    }

    public void OnPataContinueBtn(GameObject go)
    {
        if (mainUILogic != null)
        {
            Obj_MyselfPlayer.GetMe().lastPataNum = 0;
            Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.CHONG_LOU;
            mainUILogic.GetComponent<MainUILogic>().OnBattleBefore();
        }
    }

    public void OnPataBackBtn(GameObject go)
    {
        if (mainUILogic == null)
        {
            mainUILogic = GameObject.Find("MainUILogic");
        }
        Obj_MyselfPlayer.GetMe().lastPataNum = 0;
        mainUILogic.SendMessage("OnPataWindow");
        mainUILogic.GetComponent<MainUILogic>().SetMainUIBottomBarActive(true);
    }
	
	//by zhouwei of haiwai
	public void OnGotoriendShipSettleWindow(GameObject go){
		mainUILogic.SendMessage("OnFriendShipSettleWindow");
	}	
	
	public void LoveBoxPopUp(){
#if UNITY_ANDROID
		OnGooglePlayButtonClick();
#elif UNITY_IPHONE
		OnAppstoreButtonClick();
#endif
	}
	
#if UNITY_ANDROID	
	//googleplay
	public void OnGooglePlayButtonClick(){	
		
		BoxManager.showMessageByID((int)MessageIdEnum.Msg802);
		UIEventListener.Get(BoxManager.buttonYes).onClick += RequestGooglePlayUrl;
		UIEventListener.Get(BoxManager.buttonNo).onClick += OnGotoriendShipSettleWindow;
	}
	
	public void RequestGooglePlayUrl(GameObject obj){
		
		NetworkSender.Instance().RequestTaskOver(RequestGooglePlayUrlRet,2);
		UIEventListener.Get(backGround).onClick += OnConfirmButtonClick;
		//test
		//GotoGooglePlayUrl(true);
	}
	
	public void RequestGooglePlayUrlRet(bool success){
		if(success){
			Debug.Log("GotoGooglePlayUrl success");
		}else{
			Debug.Log("GotoGooglePlayUrl error");
		}
		
		Application.OpenURL(ClientConfigure.getGooglePlayUrl());
		
		mainUILogic.SendMessage("OnFriendShipSettleWindow");
		
	}
	//end googleplay	
	
#elif UNITY_IPHONE
	//appstore
	public void OnAppstoreButtonClick(){	
		
		BoxManager.showMessageByID((int)MessageIdEnum.Msg803);
		UIEventListener.Get(BoxManager.buttonYes).onClick += RequestAppstoreUrl;
		UIEventListener.Get(BoxManager.buttonNo).onClick += OnGotoriendShipSettleWindow;
	}
	
	public void RequestAppstoreUrl(GameObject obj){
		

		//test
		//GotoGooglePlayUrl(true);
		
		Application.OpenURL(ClientConfigure.getAppstoreUrl());
		
		mainUILogic.SendMessage("OnFriendShipSettleWindow");				
	}
	
#endif	
}
