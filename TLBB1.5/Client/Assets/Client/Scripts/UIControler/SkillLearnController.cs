using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using Games.LogicObject;
using System.Collections.Generic;
using GCGame.Table;
using card.net;

public class SkillLearnController : MonoBehaviour
{
    public GameObject beforeLearn;
    public GameObject afterLearn;
    public GameObject mainCard;
    public GameObject childCard;
    public GameObject childCardBG;
    public GameObject tips;
    public GameObject addTip;
    public GameObject selectTip;
    //public GameObject learnAni;
    public UIImageButton confirmBtn;
    public UISprite beforeSkill;
    public UISprite afterSkill;
    public UILabel beforeSkillLev;
    public UILabel afterSkillLev;
    public UILabel beforeSkillDes;
    public UILabel afterSkillDes;
    public UILabel costMoney;
    private GameObject mainLogic;
    private List<UserCardItem> cardList;
    private UserCardItem learnMainHeroItem;
    private UserCardItem learnChildHeroItem;
    private int costMoneyValue = 100000;
    private bool finishLearn = false;
    private string effectName = "ji_neng_xue_xi";
    private GameObject effectAni;

    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic");
    }

    void OnEnable()
    {
        //learnAni.SetActive(false);
        FreshUI();
    }

    void OnDisable()
    {
        Destroy(effectAni);
    }

    /// <summary>
    /// 刷新界面
    /// </summary>
    private void FreshUI()
    {
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        learnMainHeroItem = Obj_MyselfPlayer.GetMe().learnMainHeroItem;
        learnChildHeroItem = Obj_MyselfPlayer.GetMe().learnChildHeroItem;
        costMoney.text = costMoneyValue.ToString();
        CardExist();
        if (learnMainHeroItem == null)
        {
            mainCard.SetActive(false);
            childCard.SetActive(false);
            childCardBG.GetComponent<UIButtonMessage>().enabled = false;
            beforeLearn.SetActive(false);
            afterLearn.SetActive(false);
            confirmBtn.isEnabled = false;
            tips.SetActive(false);
            addTip.SetActive(false);
            selectTip.SetActive(true);
        }
        else
        {
            mainCard.GetComponent<CardLarge>().SetCardTemplateID(learnMainHeroItem.templateID);
            mainCard.SetActive(true);
            childCardBG.GetComponent<UIButtonMessage>().enabled = true;
            beforeLearn.SetActive(true);
            selectTip.SetActive(false);
            Tab_Studyskill tabStydyskill = TableManager.GetStudyskillByID(learnMainHeroItem.skillStudyId);
            if (tabStydyskill != null)
            {
                if (finishLearn)
                {
                    beforeSkill.spriteName = tabStydyskill.SkillName;
                    finishLearn = false;
                }
                else
                {
                    beforeSkill.spriteName = tabStydyskill.SkillName + "_原技能";
                }
                beforeSkillDes.text = tabStydyskill.SkillDes;
            }
            else
            {
                beforeSkill.spriteName = "wu";
                beforeSkillDes.text = "";
            }
            beforeSkillLev.text = learnMainHeroItem.skillStudyLev.ToString();
            if (learnChildHeroItem == null)
            {
                childCard.SetActive(false);
                afterLearn.SetActive(false);
                confirmBtn.isEnabled = false;
                tips.SetActive(false);
                addTip.SetActive(true);
            }
            else
            {
                childCard.GetComponent<CardIcon>().SetCardTemplateID(learnChildHeroItem.templateID);
                childCard.SetActive(true);
                afterLearn.SetActive(true);
                confirmBtn.isEnabled = true;
                Tab_Studyskill tabStydyskillchild = TableManager.GetStudyskillByID(learnChildHeroItem.skillStudyId);
                if (tabStydyskillchild != null)
                {
                    afterSkill.spriteName = tabStydyskillchild.SkillName;
                    afterSkillDes.text = tabStydyskillchild.SkillDes;
                    afterSkillLev.text = learnChildHeroItem.skillStudyLev.ToString();
                }
                tips.SetActive(true);
                addTip.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 检测卡牌是否还存在
    /// </summary>
    private void CardExist()
    {
        //检查背包中是否有内存中的选中卡牌如果没有清空数据
        if (cardList == null)
        {
            learnMainHeroItem = null;
            learnChildHeroItem = null;
        }
        else
        {
            if (learnMainHeroItem != null)
            {
                bool isFindMainHero = false;
                foreach (UserCardItem card in cardList)
                {
                    if (card.cardID == learnMainHeroItem.cardID)
                    {
                        learnMainHeroItem = card;
                        isFindMainHero = true;
                        break;
                    }
                }
                if (!isFindMainHero)
                {
                    //找不到主英雄 全清掉
                    learnMainHeroItem = null;
                    learnChildHeroItem = null;
                    return;
                }
            }
            else
            {
                //主英雄空子英雄也清空
                learnChildHeroItem = null;
                return;
            }
            if (learnChildHeroItem != null)
            {
                //如果子英雄和主英雄一样，清掉子英雄
                if (learnMainHeroItem.cardID == learnChildHeroItem.cardID)
                {
                    learnChildHeroItem = null;
                    return;
                }
                bool isFindChildHero = false;
                foreach (UserCardItem card in cardList)
                {
                    if (card.cardID == learnChildHeroItem.cardID)
                    {
                        learnChildHeroItem = card;
                        isFindChildHero = true;
                        break;
                    }
                }
                if (!isFindChildHero)
                {
                    //找不到子英雄 只清子英雄
                    learnChildHeroItem = null;
                }
                //如果被保护了清除卡牌
                if (learnChildHeroItem.isProtected)
                {
                    learnChildHeroItem = null;
                }
            }
        }
    }

    /// <summary>
    /// 确认学习
    /// </summary>
    private void ConfirmFirst()
    {
        Tab_Studyskill tabStydyskill = TableManager.GetStudyskillByID(learnMainHeroItem.skillStudyId);
        Tab_Studyskill tabStydyskillchild = TableManager.GetStudyskillByID(learnChildHeroItem.skillStudyId);
        if (tabStydyskill != null)
        {
            if (tabStydyskillchild.LeaderNum > tabStydyskill.LeaderNum)
            {
                BoxManager.showMessageByID((int)MessageIdEnum.Msg244, (tabStydyskillchild.LeaderNum - tabStydyskill.LeaderNum).ToString());
                UIEventListener.Get(BoxManager.getYesButton()).onClick += ConfirmLearn;
            }
            else
            {
                ConfirmLearn(gameObject);
            }
        }
        else
        {
            if (tabStydyskillchild.LeaderNum > 0)
            {
                BoxManager.showMessageByID((int)MessageIdEnum.Msg244, tabStydyskillchild.LeaderNum.ToString());
                UIEventListener.Get(BoxManager.getYesButton()).onClick += ConfirmLearn;
            }
            else
            {
                ConfirmLearn(gameObject);
            }
        }
    }

    /// <summary>
    /// 确认学习
    /// </summary>
    private void ConfirmLearn(GameObject go)
    {
        Tab_Studyskill tabStydyskill = TableManager.GetStudyskillByID(learnMainHeroItem.skillStudyId);
        if (tabStydyskill != null)
        {
            BoxManager.showMessageByID((int)MessageIdEnum.Msg201);
            UIEventListener.Get(BoxManager.getYesButton()).onClick += Confirm;
        }
        else
        {
            if (costMoneyValue > Obj_MyselfPlayer.GetMe().money)
            {
                NetworkSender.Instance().buyGold(BuyGoldFinish, 1);
                return;
            }
            NetworkSender.Instance().RequestLearnSkill(LearnSkillDone);
        }
    }

    /// <summary>
    /// 确认覆盖技能
    /// </summary>
    /// <param name="btn"></param>
    private void Confirm(GameObject btn)
    {
        if (costMoneyValue > Obj_MyselfPlayer.GetMe().money)
        {
            NetworkSender.Instance().buyGold(BuyGoldFinish, 1);
            return;
        }
        NetworkSender.Instance().RequestLearnSkill(LearnSkillDone);
    }

    /// <summary>
    /// 买金币
    /// </summary>
    /// <param name="isSucess"></param>
    public void BuyGoldFinish(bool isSucess)
    {
        if (isSucess)
        {
            if (mainLogic == null)
                mainLogic = GameObject.Find("MainUILogic");
            mainLogic.GetComponent<MainUILogic>().SendMessage("refreshTopBar");
            BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
            Debug.Log("Buy Gold Finish");
        }
        else
            Debug.LogError("Buy Gold Error");
    }

    /// <summary>
    /// 完成技能学习处理数据
    /// </summary>
    /// <param name="isSuccess"></param>
    private void LearnSkillDone(bool isSuccess)
    {
        Destroy(effectAni);
        GameObject newEffect = ResourceManager.Instance.loadEffect(effectName);
        newEffect.transform.parent = transform;
        newEffect.transform.localScale = new Vector3(950f, 950f, 1f);
        newEffect.transform.localPosition = new Vector3(-120f, 130f, -500f);
        effectAni = newEffect;
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        if (cardList != null)
        {
            foreach (UserCardItem card in cardList)
            {
                if (card.cardID == learnMainHeroItem.cardID)
                {
                    Obj_MyselfPlayer.GetMe().learnMainHeroItem = card;
                    Obj_MyselfPlayer.GetMe().learnChildHeroItem = null;
                    finishLearn = true;
                    FreshUI();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 回技能界面
    /// </summary>
    private void ReturnToSkillWindow()
    {
        mainLogic.SendMessage("OnSkillWindow");
    }

    //选择主英雄
    private void SelectMainHero()
    {
        Obj_MyselfPlayer.GetMe().skillHeroType = SkillHeroType.LEARN_MAIN;
        mainLogic.SendMessage("OnSelectSkillHeroWindow");
    }

    //选择技能英雄
    private void SelectChildHero()
    {
        Obj_MyselfPlayer.GetMe().skillHeroType = SkillHeroType.LEARN_CHILD;
        mainLogic.SendMessage("OnSelectSkillHeroWindow");
    }
}
