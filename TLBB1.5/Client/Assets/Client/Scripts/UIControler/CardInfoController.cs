using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

public class CardInfoController : MonoBehaviour
{
    private MainUILogic mainLogic;
    //public UITexture iconBig;
    //public UISprite iconCategory;
    public UILabel cardName;
    public UILabel cardLevel;
    public UILabel cardLeaderShip;
    public UILabel cardHP;
    public UILabel cardHPAdd;
    public UILabel cardAttack;
    public UILabel cardAttackAdd;
    public UISprite cardCanEvl;
    public UILabel otherInfo;
    public GameObject starManage;

    public UILabel skillNormal;
    public UILabel skillActive;
    public UILabel skillLeader;
    public UILabel skillMix;
    public UILabel sdudySkillDes;

    public UILabel skillNormalName;
    public UILabel skillActiveName;
    public UILabel skillLeaderName;
    public UILabel skillMixName;
    public UILabel studySkillName;
    //public UISprite cardFrame;
    public GameObject[] roundImg;

    public GameObject largeCardObj;

    public UISprite isProtected;
    private Dictionary<long, string> protectedCardIDs;
    private MainController mainController;
    static public GameObject heroControl;
    static private bool bInshow = false;
#if UNITY_ANDROID
    //public GameObject mainPanel;
#endif

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
#if UNITY_ANDROID
		// 修改android版本字体灰色的问题 //
       // mainPanel = GameObject.FindWithTag("MainPanel");
       // if(mainPanel != null)
        //{
            //mainPanel.SetActiveRecursively(false);
        //    mainPanel.active = false;
        //}
#endif
    }
#if UNITY_ANDROID
	void OnDestroy()
	{
		//mainPanel.SetActiveRecursively(true);
		//mainPanel.active = true;
	}
#endif
    void OnEnable()
    {
        if (mainController == null)
        {
            mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
            mainController = mainLogic.mainController.GetComponent<MainController>();
        }
        if (Obj_MyselfPlayer.GetMe().selectedCardID != -1)
        {
            FreshCardInfo(Obj_MyselfPlayer.GetMe().selectedCardID);
        }
    }
    void OnDisable()
    {
        //mainController.showTopBar();
        //mainController.showBottomBar();
    }

    public static void ShowCardInfo()
    {
        if (bInshow)
        {
            return;
        }

        bInshow = true;
        GameObject cardInfoWnd = ResourceManager.Instance.LoadWindow("CardInfoController");
        cardInfoWnd.transform.parent = Camera.main.transform;
        cardInfoWnd.transform.localPosition = new Vector3(0, 0, -200);
        cardInfoWnd.transform.localScale = Vector3.one;
    }

    public void protectCard()
    {
        //clear
        if (protectedCardIDs == null)
        {
            protectedCardIDs = new Dictionary<long, string>();
        }
        else
        {
            protectedCardIDs.Clear();
        }
        //change protect state
        List<UserCardItem> cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        foreach (UserCardItem card in cardList)
        {
            if (card.cardID == Obj_MyselfPlayer.GetMe().selectedCardID)
            {
                card.isProtected = !card.isProtected;
                isProtected.spriteName = card.isProtected ? "renwuxinxi_quxiaobaohu" : "renwuxinxi_baohu";
                isProtected.MakePixelPerfect();
            }
            if (card.isProtected)
            {
                protectedCardIDs.Add(card.cardID, card.cardID.ToString());
            }
        }

        if (protectedCardIDs.Count > 0)
        {
            //generate array and save
            string[] IDs = new string[protectedCardIDs.Count];
            int idx = 0;
            foreach (long key in protectedCardIDs.Keys)
            {
                IDs[idx++] = protectedCardIDs[key];
            }
            //			int idx = 0;
            //			IEnumerator enu = protectedCardIDs.Values.GetEnumerator();
            //			while(enu.MoveNext())
            //			{
            //				IDs[idx] = enu.Current.ToString();
            //				idx++;			
            //			}
            GameManager.saveProtectedCardsID(IDs);
        }
        else
        {
            string[] str = new string[1];
            str[0] = "0";
            GameManager.saveProtectedCardsID(str);
        }
    }
    public void FreshCardInfo(long cardID)
    {
        Obj_MyselfPlayer.GetMe().bShowCardInfo = true;

        List<UserCardItem> cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        foreach (UserCardItem card in cardList)
        {
            if (card.cardID == cardID)
            {

                if (largeCardObj != null)
                {
                    largeCardObj.GetComponent<CardLarge>().SetCardTemplateID(card.templateID);
                }
                Tab_Card tab_card = TableManager.GetCardByID(card.templateID);
                if (tab_card != null)
                {
                    Tab_Appearance tab_Appearance = TableManager.GetAppearanceByID(tab_card.Appearance);
                    if (tab_Appearance != null)
                    {
                        cardName.text = LanguageManger.GetWords(tab_Appearance.Name);
                        otherInfo.text = LanguageManger.GetWords(tab_Appearance.DropDescripe);
                    }
                    cardLevel.text = card.level + "/" + tab_card.MaxLevel.ToString();
                    if (card.GetHpAdd() > 0) //有加成
                    {
                        cardHPAdd.text = "+" + card.GetHpAdd().ToString();
                    }
                    else
                    {
                        cardHPAdd.text = "";
                    }
                    if (card.GetAttackAdd() > 0)
                    {
                        cardAttackAdd.text = "+" + card.GetAttackAdd().ToString();
                    }
                    else
                    {
                        cardAttackAdd.text = "";
                    }

                    cardHP.text = (card.GetHpBase() + card.GetFengShuiHp() + card.GetStudySkillHp()).ToString();
                    cardAttack.text = (card.GetAttackBase() + card.GetFengShuiAttc()).ToString();

                    cardLeaderShip.text = card.GetLeaderShip().ToString();
                    Transform transformStars = starManage.transform;
                    int maxStar = tab_card.HighStarDisplay;
                    for (int j = 1; j <= 7; j++)
                    {
                        if (j <= card.quality)
                        {
                            UISprite starIcon = transformStars.FindChild("Star_" + j).GetComponent<UISprite>();
                            starIcon.spriteName = "xingxing";
                            starIcon.MakePixelPerfect();
                            starIcon.gameObject.SetActive(true);
                        }
                        else
                        {
                            UISprite starIcon = transformStars.FindChild("Star_" + j).GetComponent<UISprite>();
                            if (j <= maxStar)
                            {
                                starIcon.spriteName = "haoyou_xingxing_beijing";
                                starIcon.MakePixelPerfect();
                                starIcon.gameObject.SetActive(true);
                            }
                            else
                            {
                                starIcon.gameObject.SetActive(false);
                            }
                        }
                    }
                    Tab_Skill skill = null;
                    Tab_SkillDisplay skillDisplay = null;
                    //普通技能
                    skill = TableManager.GetSkillByID(tab_card.SkillComm);
                    if (skill != null)
                    {
                        skillDisplay = TableManager.GetSkillDisplayByID(skill.Effect);
                        skillNormalName.text = LanguageManger.GetWords(skillDisplay.Name);
                        skillNormal.text = ConvertAttType(LanguageManger.GetWords(skillDisplay.Describe), card.templateID);
                    }
                    //主动技能
                    skill = TableManager.GetSkillByID(tab_card.SkillVol);
                    if (skill != null)
                    {
                        skillDisplay = TableManager.GetSkillDisplayByID(skill.Effect);
                        skillActiveName.text = LanguageManger.GetWords(skillDisplay.Name);
                        skillActive.text = ConvertAttType(LanguageManger.GetWords(skillDisplay.Describe), card.templateID);
                        skillActiveName.text += "[9d2f07]" + card.skillLevel + "/" + skill.SkillMaxlevel;
                        //显示进恩释放回合数
                        ShowRoundNum(skill.FirstRelease);
                    }
                    else
                    {
                        //没有绝技，不显示
                        ShowRoundNum(2);
                    }
                    //队长技能
                    Tab_Leaderskill leaderSkill = TableManager.GetLeaderskillByID(tab_card.SkillLeader);
                    if (leaderSkill != null)
                    {
                        skillLeaderName.text = LanguageManger.GetWords(leaderSkill.Name);
                        skillLeader.text = ConvertAttType(LanguageManger.GetWords(leaderSkill.Note), card.templateID);
                    }
                    else
                    {
                        skillLeaderName.text = "无";
                        skillLeader.text = "";
                    }
                    skill = TableManager.GetSkillByID(tab_card.SkillComb);
                    if (skill != null)
                    {
                        skillDisplay = TableManager.GetSkillDisplayByID(skill.Effect);
                        skillMixName.text = LanguageManger.GetWords(skillDisplay.Name);
                        skillMix.text = ConvertAttType(LanguageManger.GetWords(skillDisplay.Describe), card.templateID);
                    }
                    else
                    {
                        skillMixName.text = "无";
                        skillMix.text = "";
                    }
                    cardCanEvl.spriteName = tab_card.NextCard == -1 ? "cannotEvl" : "canEvl";
                    isProtected.spriteName = card.isProtected ? "renwuxinxi_quxiaobaohu" : "renwuxinxi_baohu";
                    isProtected.MakePixelPerfect();
                    //学习技能
                    Tab_Studyskill studySkill = TableManager.GetStudyskillByID(card.skillStudyId);
                    if (studySkill != null)
                    {
                        string color = "";
                        switch (studySkill.SkillQuality)
                        {
                            case 0:
                                color = "[2d8560]";
                                break;
                            case 1:
                                color = "[2368ad]";
                                break;
                            case 2:
                                color = "[852bed]";
                                break;
                            default:
                                break;
                        }
                        studySkillName.text = color + studySkill.SkillName;
                        studySkillName.text += "[9d2f07] " + card.skillStudyLev + "/" + studySkill.SkillHighLevel;
                        sdudySkillDes.text = ConvertAttType(studySkill.SkillDes, card.templateID);
                    }
                    else
                    {
                        studySkillName.text = "[a65a04]无";
                        sdudySkillDes.text = "";
                    }
                }
                break;
            }
        }
    }

    //根据人物替换描述信息
    public string ConvertAttType(string content, int templateId)
    {
        string result = "";
        if (TableManager.GetCardByID(templateId).AttackType == 0)
        {
            result = "内功";
        }
        else
        {
            result = "外功";
        }
        return content.Replace("$AttackType$", result);
    }

    //显示回合数
    public void ShowRoundNum(int nRound)
    {
        switch (nRound)
        {
            case 0:
                {
                    roundImg[0].SetActive(false);
                    roundImg[1].SetActive(true);
                    roundImg[2].SetActive(true);
                    roundImg[3].SetActive(false);
                    roundImg[4].SetActive(true);
                    roundImg[5].SetActive(false);
                }
                break;

            case 1:
                {
                    roundImg[0].SetActive(true);
                    roundImg[1].SetActive(false);
                    roundImg[2].SetActive(false);
                    roundImg[3].SetActive(true);
                    roundImg[4].SetActive(true);
                    roundImg[5].SetActive(false);
                }
                break;

            default:
                {
                    roundImg[0].SetActive(false);
                    roundImg[1].SetActive(false);
                    roundImg[2].SetActive(false);
                    roundImg[3].SetActive(false);
                    roundImg[4].SetActive(false);
                    roundImg[5].SetActive(false);
                }
                break;
        }
    }

    public void backToPreviousWindow()
    {
        if (heroControl != null)
        {
            heroControl.SendMessage("ChangeProtectedState");
        }
        CardInfoController.bInshow = false;
        Obj_MyselfPlayer.GetMe().bShowCardInfo = false;
        GameObject.Destroy(gameObject);
    }


}
