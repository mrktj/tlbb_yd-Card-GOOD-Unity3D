using UnityEngine;
using System.Collections;
using GCGame.Table;
using Games.CharacterLogic;
using Games.LogicObject;
using System.Collections.Generic;
using card.net;
#if UNITY_ANDROID
using card;
#endif
public class MessageBoxController : MonoBehaviour
{
    public GameObject myAnimation;
    public GameObject label;
    public GameObject boxPanel;
    //王明磊 -  背包已满 弹窗指引
    public GameObject title;
    public GameObject buttonShop = null;
    public GameObject buttonUpdate = null;
    public GameObject buttonSell = null;


    public GameObject buttonNo = null;
    public GameObject buttonYes = null;
    public GameObject inputLabel = null;
    BoxManager.MessageType type = BoxManager.MessageType.NONE;

    //for card info
    //public UITexture cardIcon;
    //public UISprite cardCategory;
    public UILabel attack;
    public UILabel attack_add;
    public UISprite cardCanEvl;
    public UILabel H_name;
    public UILabel H_des;
    public UILabel L_name;
    public UILabel L_des;
    public UILabel Z_name;
    public UILabel Z_des;
    public UILabel N_name;
    public UILabel N_des;
    public UILabel HP;
    public UILabel HP_add;
    public UILabel leadership;
    public UILabel level;
    public UILabel level_src;
    public GameObject[] startManager;
    public UILabel cardName;
    public UILabel cardLevelMax;
    public UILabel studySkillName;
    public UILabel sdudySkillDes;
    public GameObject largeCardObj;

    //public UISprite cardFrame;
    private double startBoxTime = -1;
    const int timeoutSec = 15;
    public GameObject[] roundImg;
    // Update is called once per frame
    private float waitTime = 0.0f;
    private bool waitHide = true;
    public float waiMaxTime = 0.8f;

    private double templateTime = 0;
    void Awake()
    {
        //mainLogic = GameObject.Find("MainUILogic");
    }
    void Start()
    {
        //		templateTime = GameManager.Instance.globalTimeCount;
        switch (type)
        {
            case BoxManager.MessageType.WaitingBox:
            case BoxManager.MessageType.ProcessBox:
                waitTime = 0;
                waitHide = true;
                boxPanel.SetActive(false);
                break;
            default:
                break;
        }
    }
    void OnDestroy()
    {
        //		Debug.LogError("box alive : " + (GameManager.Instance.globalTimeCount - templateTime));
    }
    void Update()
    {
        waitTime += Time.deltaTime;
        switch (type)
        {
            case BoxManager.MessageType.WaitingBox:
                if (waitHide && waitTime >= waiMaxTime)
                {
                    waitHide = false;
                    boxPanel.SetActive(true);
                    return;
                }
                if (myAnimation != null)
                {
                    myAnimation.transform.Rotate(-1 * Vector3.forward * Time.deltaTime * 500);
                }
                if (GameManager.Instance.globalTimeCount - this.startBoxTime > timeoutSec)
                {
                    //首次登陆账号连接超时：
                    if (GameManager.Instance.sceneName.Equals(Utils.UI_NAME_Login) &&
                        AccountManager.Instance.IsAccountLogin())
                    {
                        Destroy(gameObject);
                        //BoxManager.showMessage("连接超时，请重试");   
                        BoxManager.showMessageByID((int)MessageIdEnum.Msg19);
                        NetworkSender.Instance().sendFinish(false);
                        //服务器轮询
                        GameObject sc = GameObject.FindWithTag("MainCamera").transform.FindChild("Anchor/Panel/SplashController(Clone)").gameObject;
                        SplashController scScript = sc.GetComponent<SplashController>();
                        scScript.SelectAnotherServer();
                        break;
                    }
                    //普通连接超时
                    else
                    {
                        Destroy(gameObject);
                        //BoxManager.showMessage("连接超时，请重试");   
                        BoxManager.showMessageByID((int)MessageIdEnum.Msg19);
                        NetworkSender.Instance().sendFinish(false);
                        if (Application.loadedLevelName == Utils.UI_NAME_Battle ||
                            !GuideManager.Instance.isEnd())
                        {
                            UIEventListener.Get(BoxManager.getYesButton()).onClick += NetManager.ReLink;
                        }
                    }
                }
                break;
            case BoxManager.MessageType.ProcessBox:
                if (waitHide && waitTime > waiMaxTime)
                {
                    waitHide = false;
                    boxPanel.SetActive(true);
                    return;
                }
                if (myAnimation != null)
                {
                    myAnimation.transform.Rotate(-1 * Vector3.forward * Time.deltaTime * 500);
                }
                break;
            //		case BoxManager.MessageType.PROCESS:
            //FIXME: process can not work right
            //			Debug.Log("progress:"+GameManager.Instance.async.progress);
            //			break;
#if UNITY_ANDROID
		case BoxManager.MessageType.MessageBox:
			if(myAnimation!=null)
			{
				myAnimation.transform.Rotate(-1*Vector3.forward*Time.deltaTime*500);
			}
			break;
#endif
        }
    }
    public void init(BoxManager.MessageType type, string label)
    {
        this.setType(type);
        this.setLabel(label);
    }

    public void init(BoxManager.MessageType type, string label, string titleStr)
    {
        this.setType(type);
        this.setLabel(label);
        this.setTitle(titleStr);
    }
    //private GameObject mainLogic;
    void OnEnable()
    {

    }

    public void ShowBottomNotice()
    {
        //if(type == BoxManager.MessageType.CardInfoBox);
        //mainLogic.transform.GetComponent<MainUILogic>().mainController.GetComponent<MainController>().showBottomBar();
    }
    //zbz修改（以后显示的计算都放在这里） 如果Card != null 则用card, 否用templateid
    private void CalculateCardInfoAndShow(UserCardItem card, int nTemplateid)
    {
        int templateid = 0;
        if (card != null)
        {
            templateid = card.templateID;
        }
        else if (nTemplateid > 0)
        {
            templateid = nTemplateid;
        }
        else
        {
            return;
        }
        Tab_Card tabCard = TableManager.GetCardByID(templateid);
        if (tabCard != null)
        {
            Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
            if (tabAppearance != null)
            {
                cardName.text = LanguageManger.GetWords(tabAppearance.Name);
                if (tabAppearance.DropDescripe == -1)
                {
                    level_src.text = "获得途径:无";
                }
                else
                {
                    level_src.text = LanguageManger.GetWords(tabAppearance.DropDescripe);
                }
            }
            if (largeCardObj != null)
            {
                largeCardObj.GetComponent<CardLarge>().SetCardTemplateID(templateid);
            }
            if (card != null)
            {
                if (Obj_MyselfPlayer.GetMe().IsCardInBagByID(card.cardID))
                {
                    HP.text = (card.GetHpBase() + card.GetFengShuiHp() + card.GetStudySkillHp() ).ToString();
                    attack.text = (card.GetAttackBase() + card.GetFengShuiAttc()).ToString();
                }
                else
                {
                    HP.text = card.GetHpBase().ToString();
                    attack.text = card.GetAttackBase().ToString();
                }

                if (card.GetHpAdd() > 0) //有加成
                {
                    HP_add.text = "+" + card.GetHpAdd().ToString();
                }
                else
                {
                    HP_add.text = "";
                }
                if (card.GetAttackAdd() > 0)
                {
                    attack_add.text = "+" + card.GetAttackAdd().ToString();
                }
                else
                {
                    attack_add.text = "";
                }
                level.text = card.level + "/" + tabCard.MaxLevel.ToString();
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
                    sdudySkillDes.text = ConvertAttType(studySkill.SkillDes, templateid);
                }
                else
                {
                    studySkillName.text = "[a65a04]无";
                    sdudySkillDes.text = "";
                }
                leadership.text = card.GetLeaderShip().ToString();
            }
            else
            {
                int hp = tabCard.HpBase;
                int nLevBase = tabCard.LevelBase;
                hp += tabCard.HpGrow * (tabCard.GambleLevel - nLevBase);
                HP.text = hp.ToString();
                int att = tabCard.AttackBase;
                att += tabCard.AttackGrow * (tabCard.GambleLevel - nLevBase);
                attack.text = att.ToString();
                attack_add.text = "";
                HP_add.text = "";
                level.text = tabCard.GambleLevel.ToString() + "/" + tabCard.MaxLevel.ToString();
                //学习技能
                Tab_Studyskill studySkill = TableManager.GetStudyskillByID(tabCard.SkillStudy);
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
                    studySkillName.text += "[9d2f07] " + tabCard.SkillStudylv + "/" + studySkill.SkillHighLevel;
                    sdudySkillDes.text = ConvertAttType(studySkill.SkillDes, templateid);
                    //领导力
                    leadership.text = (tabCard.LeaderBase + studySkill.LeaderNum).ToString();
                }
                else
                {
                    studySkillName.text = "[a65a04]无";
                    sdudySkillDes.text = "";
                    //领导力
                    leadership.text = tabCard.LeaderBase.ToString();
                }
            }
            Tab_Skill skill = null;
            Tab_SkillDisplay skillDisplay = null;
            skill = TableManager.GetSkillByID(tabCard.SkillComm);
            if (skill != null)
            {
                skillDisplay = TableManager.GetSkillDisplayByID(skill.Effect);
                N_name.text = LanguageManger.GetWords(skillDisplay.Name);
                N_des.text = ConvertAttType(LanguageManger.GetWords(skillDisplay.Describe), templateid);
            }
            skill = TableManager.GetSkillByID(tabCard.SkillVol);
            cardLevelMax.text = "";
            if (skill != null)
            {

                skillDisplay = TableManager.GetSkillDisplayByID(skill.Effect);
                Z_name.text = LanguageManger.GetWords(skillDisplay.Name);
                Z_des.text = ConvertAttType(LanguageManger.GetWords(skillDisplay.Describe), templateid);
                //当实现的卡牌是存在的卡牌时(就是除了进化后预览效果这样的卡牌)
                if (card != null)
                {
                    Z_name.text += "[9d2f07]" + card.skillLevel + "/" + skill.SkillMaxlevel;
                    Debug.Log("card skill level : " + card.skillLevel);
                }
                else
                {
                    Z_name.text += "[9d2f07]" + 1 + "/" + skill.SkillMaxlevel;

                    Debug.Log("card skill level : not find card");
                }
                //2013-8-9 Jack Wen
                cardLevelMax.gameObject.SetActive(false);
                ShowRoundNum(skill.FirstRelease);
            }
            else
            {
                ShowRoundNum(2);
            }

            Tab_Leaderskill leaderSkill = TableManager.GetLeaderskillByID(tabCard.SkillLeader);//.GetSkillByID(tabCard.SkillLeader);
            if (leaderSkill != null)
            {
                L_name.text = LanguageManger.GetWords(leaderSkill.Name);
                Debug.Log("L_name.text=" + L_name.text);
                L_des.text = ConvertAttType(LanguageManger.GetWords(leaderSkill.Note), templateid);
            }
            else
            {
                L_name.text = "无";
                L_des.text = "";
            }
            skill = TableManager.GetSkillByID(tabCard.SkillComb);
            if (skill != null)
            {
                skillDisplay = TableManager.GetSkillDisplayByID(skill.Effect);
                H_name.text = LanguageManger.GetWords(skillDisplay.Name);
                H_des.text = ConvertAttType(LanguageManger.GetWords(skillDisplay.Describe), templateid);
            }
            cardCanEvl.spriteName = tabCard.NextCard == -1 ? "cannotEvl" : "canEvl";
            int maxStar = tabCard.HighStarDisplay;
            //		level;
            for (int i = 0; i < startManager.Length; i++)
            {
                if (i < tabCard.Star)
                {
                    UISprite starIcon = startManager[i].GetComponent<UISprite>();
                    starIcon.spriteName = "xingxing";
                    starIcon.MakePixelPerfect();
                    starIcon.gameObject.SetActive(true);
                }
                else
                {
                    UISprite starIcon = startManager[i].GetComponent<UISprite>();
                    if (i < maxStar)
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

    public void initCardInfo(BoxManager.MessageType type, int guid, int templateid)
    {
        this.setType(type);

        List<UserCardItem> cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        UserCardItem card = null;
        if (guid != -1)
        {
            foreach (UserCardItem myCard in cardList)
            {
                if (myCard.cardID == guid)
                {
                    card = myCard;

                    templateid = myCard.templateID;
                }

            }
        }

        this.CalculateCardInfoAndShow(card, templateid);

    }

    public void initCardInfo(BoxManager.MessageType type, UserCardItem card)
    {
        this.setType(type);
        this.CalculateCardInfoAndShow(card, -1);
    }
    //-----------------------------------------------------------------------------
    //2013-7-29 Jack Wen
    public void initCardInfo(BoxManager.MessageType type, UserFriend uf)
    {
        this.setType(type);
        UserCardItem card = new UserCardItem();
        card.templateID = uf.cardTempletID;
        card.cardID = uf.guid;
        card.level = uf.cardLevel;
        card.addQualityAtt = uf.attAdd;
        card.addQualityHp = uf.hpAdd;
        card.skillLevel = uf.skillLevel;
        card.skillStudyId = uf.studySkillId;
        card.skillStudyLev = uf.studySkillLev;
        Tab_Card tabCard = TableManager.GetCardByID(uf.cardTempletID);
        card.quality = tabCard.Star;
        this.CalculateCardInfoAndShow(card, -1);
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
                    roundImg[0].SetActive(true);
                    roundImg[1].SetActive(false);
                    roundImg[2].SetActive(true);
                    roundImg[3].SetActive(false);
                    roundImg[4].SetActive(true);
                    roundImg[5].SetActive(false);
                }
                break;
        }
    }

    //-----------------------------------------------------------------------------
    //	public string getElementSpriteName(int element)
    //	{
    //		switch(element)
    //		{
    //		case 0://金
    //			return "card_nature_jin";
    //		case 1://木
    //			return "card_nature_mu";
    //		case 2://水
    //			return "card_nature_shui";
    //		case 3://火
    //			return "card_nature_huo";
    //		case 4://土
    //			return "card_nature_tu";
    //		}
    //		return null;
    //	}
    public void setType(BoxManager.MessageType type)
    {
        this.type = type;
        if (buttonNo != null)
        {
            UIEventListener.Get(buttonNo).onClick += OnButtonClick;//(OnButtonClick);
        }
        if (buttonYes != null)
        {
            UIEventListener.Get(buttonYes).onClick += OnButtonClick;//(OnButtonClick);
        }
        if (type == BoxManager.MessageType.WaitingBox)
        {
            this.startBoxTime = GameManager.Instance.globalTimeCount;
        }
#if UNITY_ANDROID
		if(type == BoxManager.MessageType.ProcessBox)
		{
			this.myAnimation = this.transform.FindChild("Panel/Ani").gameObject;
		}
#endif

    }
    public BoxManager.MessageType getMessageType()
    {
        return type;
    }
    public void setLabel(string message)
    {
        UILabel messageLabel = label.GetComponent<UILabel>();
        messageLabel.text = message;
    }

    public void setTitle(string titleStr)
    {
        UILabel titleLabel = title.GetComponent<UILabel>();
        titleLabel.text = titleStr;
    }

    public void OnButtonClick(GameObject button)
    {
        if (buttonNo != null)
        {
            UIEventListener.Get(buttonNo).onClick -= OnButtonClick;//(OnButtonClick);
            buttonNo = null;
        }
        if (buttonYes != null)
        {
            UIEventListener.Get(buttonYes).onClick -= OnButtonClick;//(OnButtonClick);
            buttonYes = null;
        }
        if (inputLabel != null)
        {
            BoxManager.setInputText(inputLabel.GetComponent<UILabel>().text);
        }

        BoxManager.removeMessage();
    }

}
