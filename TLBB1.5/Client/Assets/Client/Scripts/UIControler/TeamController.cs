using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

using card.net;

public class TeamController : MonoBehaviour
{

    public GameObject[] teamMember;
    public GameObject taskPrompt;
    public GameObject mailPrompt;
    public GameObject monthCardPrompt;
    public UILabel leaderShipShow;
    public UILabel strengthShow;
    private MainUILogic mainLogic;

    public GameObject giftButton;


    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
        //TweenAlpha.Begin(prompt.gameObject,1,0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnEnable()
    {
        //		if(Obj_MyselfPlayer.GetMe().giftison == 1)
        //		{
        //			giftButton.SetActive(true);
        //		}
        //		else
        //		{
        //			giftButton.SetActive(false);
        //		}
        List<UserCardItem> cardList;
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        int[] templeID = { -1, -1, -1, -1, -1 };
        //更新templateID数组
        Debug.Log("*******TeamMember********");
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(i + ":");
            long guID = Obj_MyselfPlayer.GetMe().teamMemberArray[i];
            if (guID != -1)
            {
                foreach (UserCardItem card in Obj_MyselfPlayer.GetMe().cardBagList)
                {
                    if (card.cardID == guID)
                    {
                        templeID[i] = card.templateID;
                        teamMember[i].transform.FindChild("CardIconBtn/Panel/Label-Lev").GetComponent<UILabel>().text = card.level.ToString();
                        break;
                    }
                }
            }
            else
            {
                templeID[i] = -1;
            }
            Debug.Log("guid:" + guID);
            Debug.Log("templeID:" + templeID[i]);
        }
        Debug.Log("*******TeamMember********");
        //更新头像
        for (int i = 0; i < 5; i++)
        {
            if (templeID[i] != -1)
            {
                teamMember[i].transform.FindChild("CardIconBtn/Panel").gameObject.SetActive(true);
                UITexture card = teamMember[i].transform.FindChild("CardIconBtn/Panel/PanelCard/card").GetComponent<UITexture>();
                AtlasManager.Instance.setBodyByTempletID(card, templeID[i]);
                UISprite frame = teamMember[i].transform.FindChild("CardIconBtn/Panel/Frame").GetComponent<UISprite>();
                frame.spriteName = UserCardItem.spriteFrameName[TableManager.GetCardByID(templeID[i]).Star];
                frame.MakePixelPerfect();
            }
            else
            {
                teamMember[i].transform.FindChild("CardIconBtn/Panel").gameObject.SetActive(false);
            }
        }
        int nowLeaderShip = Obj_MyselfPlayer.GetMe().GetLeaderShipValue();
        int nowstreng = Obj_MyselfPlayer.GetMe().GetFightValue();
        leaderShipShow.text = nowLeaderShip + "/" + Obj_MyselfPlayer.GetMe().leadership;
        strengthShow.text = nowstreng.ToString();

        UpdateGuide();

        //		if(!GuideManager.needGuide())
        //		{
        //			GuideManager.continueGuide();
        //
        //		}

        //有无任务或邮件 月卡活动
        CheckState();
    }

    public void CheckState()
    {
        if (Obj_MyselfPlayer.GetMe().taskListCount > 0)
        {
            taskPrompt.transform.FindChild("Label").GetComponent<UILabel>().text = Obj_MyselfPlayer.GetMe().taskListCount.ToString();
            taskPrompt.SetActive(true);
        }
        else
        {
            taskPrompt.transform.FindChild("Label").GetComponent<UILabel>().text = Obj_MyselfPlayer.GetMe().taskListCount.ToString();
            taskPrompt.SetActive(false);
        }
        if (Obj_MyselfPlayer.GetMe().mailListCount > 0)
        {
            mailPrompt.transform.FindChild("Label").GetComponent<UILabel>().text = Obj_MyselfPlayer.GetMe().mailListCount.ToString();
            mailPrompt.SetActive(true);
        }
        else
        {
            mailPrompt.transform.FindChild("Label").GetComponent<UILabel>().text = Obj_MyselfPlayer.GetMe().mailListCount.ToString();
            mailPrompt.SetActive(false);
        }
        if (Obj_MyselfPlayer.GetMe().monthCardFlag != 0)
        {
            monthCardPrompt.SetActive(true);
        }
        else
        {
            monthCardPrompt.SetActive(false);
        }
    }

    public void UpdateMailListDone(bool bSuccess)
    {

    }
	
    void OnImproveWindow()
    {
		/*
        bool isOpened = false;
        foreach (KeyValuePair<int, int> item in FengShuiData.Instance().WuxingInfor)
        {
            if (item.Value > 0)
            {
                isOpened = true;
                break;
            }
        }
        if (!isOpened && FengShuiData.Instance().star < 35 && !PlayerPrefs.HasKey(Obj_MyselfPlayer.GetMe().accountID.ToString() + "_FengShuiHadOpen"))
        {
            BoxManager.showMessageByID((int)MessageIdEnum.Msg187);
            UIEventListener.Get(BoxManager.buttonYes).onClick += GoPVEWindow;
        }
        else
			*/
            mainLogic.OnImproveWindow();
    }

    public void GoPVEWindow(GameObject button)
    {
        mainLogic.LoadMainToPveSceneList();
    }

    void OnMailWindow()
    {
        //王明磊 : 统计模块代码 -> Statistics
        PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn49).ToString());
        mainLogic.OnMailWindow();
    }
    public void onEvolutionWindow()
    {
        PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn46).ToString());
        mainLogic.onEvolutionWindow();
    }
    void onHeroWindow()
    {
        PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn44).ToString());
        mainLogic.onHeroWindow();
    }
    void OnTaskInfoWindow()
    {

        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.GIFT)
            GuideGift.Instance.NextStep();//奖赏引导 SELECT_1

        PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn47).ToString());
        mainLogic.OnTaskInfoWindow();
    }
    void OnCardStrengthenWindow()
    {
        //王明磊 : 统计模块代码 -> Statistics
        //如果不是Guide阶段,需要统计此按钮的点击信息
        if (!GameManager.Instance.isGuideMode())
            PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn48).ToString());
        mainLogic.OnCardStrengthenWindow();
        /*
        if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.STRENGTHEN)
            GuideStrengthen.Instance.NextStep();//修炼指引 SELECT_1
        */
    }
    void onUpdateWindow()
    {
        if (!GameManager.Instance.isGuideMode())
            PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn45).ToString());
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE &&
            GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.NONE_1)
            GuideUpdate.Instance.NextStep();//卡牌升级指引阶段 SELECT_1
        mainLogic.onUpdateWindow();
    }
    void LoadSelectHeaderWindow()
    {
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.LEADER)
            GuideLeader.Instance.NextStep();//选择队长指引阶段 SELECT_1

        if (GameManager.Instance.isGuideMode())
            PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn11).ToString());
        else
            PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn50).ToString());
        mainLogic.LoadSelectHeaderWindow();
    }
    void LoadSelectMemberWindow()
    {

        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.TEAM_MEMBER)
            GuideMember.Instance.NextStep();//选择队员指引阶段 SELECT_1
        else if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.TEAM_MEMBER2)
            GuideMember2.Instance.NextStep();//选择队员指引阶段 SELECT_1

        if (GameManager.Instance.isGuideMode())
            PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn13).ToString());
        else
            PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn51).ToString());
        mainLogic.LoadSelectMemberWindow();
    }

    //For Guide
    public Transform getGuideHeadButton()
    {
        return gameObject.transform.FindChild("MyTeam/TeamHeader/CardIconBtn");

    }
    public Transform getGuideMemberButton()
    {
        return gameObject.transform.FindChild("MyTeam/TeamMember0/CardIconBtn");
    }
    public Transform getGuideMember1Button()
    {
        return gameObject.transform.FindChild("MyTeam/TeamMember1/CardIconBtn");
    }
    public GameObject getGuideUpdateButton()
    {
        return gameObject.transform.FindChild("Buttons/Bottom/UpdateBtn").gameObject;
    }
    public GameObject getGuideStrengthenButton()
    {
        return gameObject.transform.FindChild("Buttons/Bottom/StrengthenBtn").gameObject;
    }
    public GameObject getGuideMissionButton()
    {
        return gameObject.transform.FindChild("Buttons/Top/Reward").gameObject;
    }

    public void UpdateGuide()
    {
        //guide非正常处理代码
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_1_END ||
            GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_2_END ||
            GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY1_3_END)
        {
            GuideManager.Instance.FinishedStep(GuideManager.Instance.currentStep);
        }
        //end
        if (!GuideManager.Instance.isInSubStep)
        {
            GuideManager.Instance.checkGuideState();
        }
        //		daixiugai
        //		else if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY_END){
        //			GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.COPY_END);
        //			GuideManager.Instance.checkGuideState();
        //		}
        else
        {
            if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.LEADER)
            {
                Debug.LogWarning("*** SelectHeader->next step *** UpdateGuide");
                GuideLeader.Instance.NextStep();//选择队长指引阶段 SELECT_2
            }
            else if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.TEAM_MEMBER)
            {
                Debug.LogWarning("*** SelectMember->next step *** UpdateGuide");
                GuideMember.Instance.NextStep();//选择队员指引阶段 SELECT_3
            }
            else if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.TEAM_MEMBER2)
            {
                Debug.LogWarning("*** SelectMember2->next step *** UpdateGuide");
                GuideMember2.Instance.NextStep();//选择队员指引阶段 SELECT_3
            }
        }
    }

    void OnGetGift()
    {
        GiftWindow.OpenWindow(this.gameObject);
    }

    void OnShopClick()
    {
        mainLogic.OnPurchaseWindow();
    }

    void OnGGLAndBaZhentuClick()
    {
		NetworkSender.Instance().AskActivity(AskActivityDone);
//        mainLogic.OnGGLWindow();
    }
	
	void AskActivityDone(bool isSuccess)
	{
		if (isSuccess)
			mainLogic.OnGGLWindow();
	}
	
    void OnSkillWindow()
    {
        mainLogic.OnSkillWindow();
    }
	
	void OpenNewPVP()
	{
		mainLogic.OnPVPBattleBeforeController();
	}
}

