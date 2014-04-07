using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
//using PBMessage;
using card;
using card.net;
using Module.Log;

public class BattleBeforeController : MonoBehaviour
{
    private CardAtlas cardAtlas;
    public GameObject[] cardIcon;
    private string[] element;
    private string[] element2;
    private GameObject logicTarget;
    bool optSuccess = false;//操作是否成功

    public UILabel leaderSkill;
    public UILabel leaderSkillName;
    public UILabel friendSkill;
    public UILabel friendSkillName;

    //战斗阵型(大于等于零的时候对应的是战斗队列里的0-4,和5是助战好友
    private int[] fightArray;
    //private int nSetState = -1;//---记录此战斗队列的情况

    void Awake()
    {
        fightArray = new int[6];
        for (int i = 0; i < 6; i++)
        {
            fightArray[i] = -1;
        }
        element = new string[5];
        element[0] = "card_nature_jin";
        element[1] = "card_nature_mu";
        element[2] = "card_nature_shui";
        element[3] = "card_nature_huo";
        element[4] = "card_nature_tu";

        element2 = new string[5];
        element2[0] = "";
    }
    // Use this for initialization
    void Start()
    {
        //		cardAtlas = GameObject.Find("CardAtlas").GetComponent<CardAtlas>();
        //		if(cardAtlas == null)
        //		{
        //			Debug.LogError("BattleBeforeController, can not find CardAtlas !");
        //		}
        //		logicTarget = GameObject.Find("MainUILogic");
        //		FreshUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        //		if(cardAtlas == null)
        //		{
        //			return;
        //		}
        //for guide
        /*
        if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.COPY){
            GuideBattleArray();
        }*/

        logicTarget = GameObject.Find("MainUILogic");
        FreshUI();

        switch (GuideManager.Instance.currentStep)
        {
            case GuideManager.GUIDE_STEP.COPY1_1:
                GuideCopy1_1.Instance.NextStep();//新手战斗引导 LABEL_6
                break;
            case GuideManager.GUIDE_STEP.COPY1_2:
                GuideCopy1_2.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY1_3:
                GuideCopy1_3.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY1_4:
                GuideCopy1_4.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY1_5:
                GuideCopy1_5.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY2_1:
                GuideCopy2_1.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            default:
                break;
        }

        //this.LoadFightArray();
    }

    //     private void LoadFightArray()
    //     {
    //         Obj_MyselfPlayer.GetMe().nSetState = PlayerPrefs.GetInt("SetState");
    // 
    //         for (int i = 0; i < 6; i++)
    //         {
    //             fightArray[i] = i;
    //         }
    // 
    //         //设置状态大于零的情况，说明之前有保存过
    //         if (this.CheckSetState())
    //         {
    //             for (int i = 0; i < 6; i++)
    //             {
    //                 fightArray[i] = PlayerPrefs.GetInt(i.ToString());
    //             }
    //         }
    //     }



    void GuideBattleArray()
    {
        if (Obj_MyselfPlayer.GetMe().battleArray[1] == -1)
        {
            Debug.Log("GuideBattleArray   CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCc");
            Obj_MyselfPlayer.GetMe().battleArray[1] = Obj_MyselfPlayer.GetMe().battleArray[4];
            Obj_MyselfPlayer.GetMe().battleArray[4] = -1;
            Debug.Log("wo yun 4:");
            Obj_MyselfPlayer.GetMe().SavebattleArray();
        }
    }

    void OnDisable()
    {
        if (!optSuccess)
        {
            if (Obj_MyselfPlayer.GetMe().battleType == Games.Battle.BattleType.PVE)
            {
                //没有点击开始战斗的退出把助战好友删掉
                for (int i = 0; i < Obj_MyselfPlayer.GetMe().battleArray.Length; i++)
                {
                    if (Obj_MyselfPlayer.GetMe().battleArray[i] == Obj_MyselfPlayer.GetMe().currentAssistFriend.cardGuiId)
                    {
                        Obj_MyselfPlayer.GetMe().battleArray[i] = -1;
                        break;
                    }
                }
            }

        }
        else
        {
            optSuccess = false;
        }
    }

    void FreshIcon(GameObject icon, long cardID)
    {
        int templeID = -1;
        UserCardItem card_item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cardID);
        if (card_item != null)
        {
            templeID = card_item.templateID;
        }
        else if (Obj_MyselfPlayer.GetMe().currentAssistFriend != null && Obj_MyselfPlayer.GetMe().currentAssistFriend.cardGuiId == cardID)
        {
            templeID = Obj_MyselfPlayer.GetMe().currentAssistFriend.cardTempleId;
        }
        else
        {
            templeID = -1;
        }
        LogModule.DebugLog("BattleBefore, FreshIcon(), Card Guid: " + cardID + ", Card templeid: " + templeID);
        if (templeID != -1)
        {
            icon.SetActive(true);
            icon.GetComponent<CardLarge>().SetCardTemplateID(templeID);
            //icon.transform.FindChild("Frame").transform.localScale=new Vector3(160,200,1);
            //icon.transform.FindChild("Panel/Category").transform.localScale=new Vector3(32,28,1);
            UILabel lev = icon.transform.FindChild("Label-Level-Value").GetComponent<UILabel>();
            /*
            UILabel nameLabel = icon.transform.FindChild("Panel/Label").GetComponent<UILabel>();
            UISprite frame = icon.transform.FindChild("Frame").GetComponent<UISprite>();
            UISprite outside = icon.transform.FindChild("outside").GetComponent<UISprite>();
            UISprite nameBg = icon.transform.FindChild("Panel/nameBg").GetComponent<UISprite>();
            */
            //UISprite spCategory = icon.transform.FindChild("Panel/Category").GetComponent<UISprite>();

            Transform transformStars = icon.transform.FindChild("Stars");
            if (card_item != null)
            {
                /*
                Tab_Card cardTab = TableManager.GetCardByID(card_item.templateID);
                if(cardTab != null)
                {
                    Tab_Appearance  appearcanceTab = TableManager.GetAppearanceByID(cardTab.Appearance);
                    if(appearcanceTab != null)
                    {
						
                        nameLabel.text = LanguageManger.GetWords(appearcanceTab.Name);
                    }
					
                }
				
                frame.spriteName = UserCardItem.largeCardName[cardTab.Star];
                outside.spriteName = UserCardItem.largeCardBorderName[cardTab.Star];
                nameBg.spriteName = UserCardItem.largeCardNameBg[cardTab.Star];
                */
                //spCategory.spriteName = 
                lev.text = card_item.level.ToString();
                for (int j = 1; j <= 7; j++)
                {
                    if (j <= card_item.quality)
                    {
                        GameObject starIcon = transformStars.FindChild("star_" + j).gameObject;
                        starIcon.SetActive(true);
                    }
                    else
                    {
                        GameObject starIcon = transformStars.FindChild("star_" + j).gameObject;
                        starIcon.SetActive(false);
                    }
                }
            }
            else
            {
                if (Obj_MyselfPlayer.GetMe().currentAssistFriend != null)
                {
                    /*
                    Tab_Card cardTab = TableManager.GetCardByID(Obj_MyselfPlayer.GetMe().currentAssistFriend.cardTempleId);
                    if(cardTab != null)
                    {
                        int nStar = cardTab.Star;
                        Tab_Appearance  appearcanceTab = TableManager.GetAppearanceByID(cardTab.Appearance);
                        if(appearcanceTab != null)
                        {
                            nameLabel.text = LanguageManger.GetWords(appearcanceTab.Name);
                        }
						
                        frame.spriteName = UserCardItem.largeCardName[cardTab.Star];
                        outside.spriteName = UserCardItem.largeCardBorderName[cardTab.Star];
                        nameBg.spriteName = UserCardItem.largeCardNameBg[cardTab.Star];
						
                    }
                    */

                    lev.text = Obj_MyselfPlayer.GetMe().currentAssistFriend.cardLevel.ToString();
                    for (int j = 1; j <= 7; j++)
                    {
                        if (j <= TableManager.GetCardByID(Obj_MyselfPlayer.GetMe().currentAssistFriend.cardTempleId).Star)
                        {
                            GameObject starIcon = transformStars.FindChild("star_" + j).gameObject;
                            starIcon.SetActive(true);
                        }
                        else
                        {
                            GameObject starIcon = transformStars.FindChild("star_" + j).gameObject;
                            starIcon.SetActive(false);
                        }
                    }
                }

            }
        }
        else
        {
            icon.SetActive(false);
        }
    }




    public void FreshUI()
    {
        //this.LoadFightArray();

        Obj_MyselfPlayer.GetMe().RefreshBattleArray();

        for (int i = 0; i < 6; i++)
        {
            FreshIcon(cardIcon[i], Obj_MyselfPlayer.GetMe().battleArray[i]);
        }



        //refresh friedn skill
        Int64 leaderguid = Obj_MyselfPlayer.GetMe().teamMemberArray[0];
        if (leaderguid == -1)
        {
            Debug.LogError("Team leader guid is -1");
            return;
        }
        Int32 leaderid = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(leaderguid).templateID;
        Int32 skillid = TableManager.GetCardByID(leaderid).SkillLeader;
        if (skillid == -1)
        {
            leaderSkill.text = "无";
            Debug.Log("leader skill is -1");
        }
        else
        {
            string describe = LanguageManger.GetWords(TableManager.GetLeaderskillByID(skillid).Note);
            if (string.IsNullOrEmpty(describe))
            {
                Debug.Log("Leader skill describe is -1");
            }
            leaderSkill.text = describe;
        }

        if (Obj_MyselfPlayer.GetMe().currentAssistFriend != null && Obj_MyselfPlayer.GetMe().battleType == Games.Battle.BattleType.PVE)
        {
            friendSkillName.gameObject.SetActive(true);
            Int32 friendid = Obj_MyselfPlayer.GetMe().currentAssistFriend.cardTempleId;
            skillid = TableManager.GetCardByID(friendid).SkillLeader;
            if (skillid == -1)
            {
                friendSkill.text = "无";
                Debug.LogError("leader skill is -1");
            }
            else
            {
                string describe = LanguageManger.GetWords(TableManager.GetLeaderskillByID(skillid).Note);
                if (string.IsNullOrEmpty(describe))
                {
                    Debug.Log("Friend skill describe is -1");
                }
                friendSkill.text = describe;
            }
        }
        else
        {
            friendSkill.text = "";
            friendSkillName.gameObject.SetActive(false);
            //Debug.LogError("no friend");
        }
    }

    public void OnStartBattle()
    {
        
        //		if(Obj_MyselfPlayer.GetMe().bInPvP)
        //		{
        //			NetworkSender.Instance().AskPVPBattleData(OnAskBattleDataRet
        //				                                       , Obj_MyselfPlayer.GetMe().pvpChoosePlayer.nlGUID
        //				                                          , Obj_MyselfPlayer.GetMe().pvpChoosePlayer.strName);
        //		}
        //		else
        //		{
        //			NetworkSender.Instance().AskBattleData(OnAskBattleDataRet, 1,Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
        //		}
        switch (Obj_MyselfPlayer.GetMe().battleType)
        {
            case Games.Battle.BattleType.PVE:
                NetworkSender.Instance().AskBattleData(OnAskBattleDataRet, 1, Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
                break;
            case Games.Battle.BattleType.PVP:
                NetworkSender.Instance().AskPVPBattleData(OnAskBattleDataRet,
                    Obj_MyselfPlayer.GetMe().pvpChoosePlayer.nlGUID, Obj_MyselfPlayer.GetMe().pvpChoosePlayer.strName);
                break;
            case Games.Battle.BattleType.CHONG_LOU:
                NetworkSender.Instance().AskChonglouBattleData(OnAskBattleDataRet);
                break;
            case Games.Battle.BattleType.WORLD_BOSS:
				ErrorEventListener.SetHandler((int)xjgame.message.ErrorType.WB_BATTLE_BOSS_DEAD,WorldBossError);
				ErrorEventListener.SetHandler((int)xjgame.message.ErrorType.WB_BATTLE_BOSS_HIDE,WorldBossError);
                NetworkSender.Instance().AskWorldBossBata(OnAskBattleDataRet);
                break;
        }
    }
	
	private void WorldBossError()
	{
		NetworkSender.Instance().RequestWorldBossInfo(BackToWorldBoss);
	}
	
	private void BackToWorldBoss(bool result)
	{
		logicTarget.SendMessage("OnWorldBossWindow");
	}
	
    public void OnAskBattleDataRet(bool bSuccess)
    {
		//LoadingController.Instance.show_load();//战斗切换loading界面//
        switch (GuideManager.Instance.currentStep)
        {
            case GuideManager.GUIDE_STEP.COPY1_1:
                GuideCopy1_1.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY1_2:
                GuideCopy1_2.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY1_3:
                GuideCopy1_3.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY1_4:
                GuideCopy1_4.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY1_5:
                GuideCopy1_5.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            case GuideManager.GUIDE_STEP.COPY2_1:
                GuideCopy2_1.Instance.NextStep();//新手战斗引导 SELECT_6
                break;
            default:
                break;
        }
        optSuccess = true;
        GameObject obj1 = GameObject.FindWithTag("main_ui_logic");
        MainUILogic mainlogic = obj1.GetComponent<MainUILogic>();
        mainlogic.LoadBattleUIScene();
    }

    public GameObject getGuideItem()
    {
        return transform.FindChild("Buttons/BattleBtn").gameObject;
    }

    public GameObject getGuideCard()
    {
        return transform.FindChild("Battle/CardLarge1").gameObject;
    }
    public GameObject getGuideRowPosition()
    {
        return transform.FindChild("Battle/CardLarge4").gameObject;
    }
    public GameObject getTeamBG2Position()
    {
        return transform.FindChild("Sprite/bg2/Sprite_bg2").gameObject;
    }
    public GameObject getTeamBG5Position()
    {
        return transform.FindChild("Sprite/bg5/Sprite_bg5").gameObject;
    }

    public void backToPreviousWindow()
    {
        //根据是pvp就返回pvp界面，是pve就返回助战好友界面
        //		if(Obj_MyselfPlayer.GetMe().bInPvP)
        //		{
        //			logicTarget.SendMessage("OnPVPWindow");
        //		}
        //		else
        //		{
        //			logicTarget.SendMessage("OnSelectAssistWindow");
        //		}
        switch (Obj_MyselfPlayer.GetMe().battleType)
        {
            case Games.Battle.BattleType.PVE:
                logicTarget.SendMessage("OnSelectAssistWindow");
                break;
            case Games.Battle.BattleType.PVP:
                logicTarget.SendMessage("OnPVPWindow");
                break;
            case Games.Battle.BattleType.CHONG_LOU:
                logicTarget.SendMessage("OnPataWindow");
                break;
            case Games.Battle.BattleType.WORLD_BOSS:
                logicTarget.SendMessage("OnWorldBossWindow");
                break;
        }
    }
}
