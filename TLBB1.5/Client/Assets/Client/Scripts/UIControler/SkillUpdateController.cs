using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using Games.LogicObject;
using System.Collections.Generic;
using GCGame.Table;
using card.net;

public class SkillUpdateController : MonoBehaviour
{
    public GameObject beforeUpdate;
    public GameObject afterUpdate;
    public GameObject mainCard;
    public GameObject[] childCards;
    public GameObject[] childCardsBG;
    public GameObject[] addTips;
    public GameObject selectTip;
    public UISlider expBar;
    public GameObject expBarForeground;
    public UISlider expBarAfter;
    public UISprite skillName;
    public UILabel beforeLev;
    public UILabel afterLev;
    public UILabel maxLev;
    public UILabel skillDes;
    public UILabel afterSkillDes;
    public UILabel expValue;
    public UIImageButton confirmBtn;
    public UILabel costMoney;
    public GameObject tips;
    private GameObject mainLogic;
    private List<UserCardItem> cardList;
    private UserCardItem updateMainHeroItem;
    private List<UserCardItem> updateChildHeroItems;
    private int costMoneyValue = 0;
    private string effectName = "ji_neng_sheng_ji";
    private GameObject effectAni;
    private int afterLeaderNum = 0;

    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic");
    }

    void OnEnable()
    {
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
        updateMainHeroItem = Obj_MyselfPlayer.GetMe().updateMainHeroItem;
        updateChildHeroItems = Obj_MyselfPlayer.GetMe().updateChildHeroItems;
        CardExist();
        if (updateMainHeroItem == null)
        {
            mainCard.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                childCards[i].SetActive(false);
                childCardsBG[i].GetComponent<UIButtonMessage>().enabled = false;
            }
            beforeUpdate.SetActive(false);
            afterUpdate.SetActive(false);
            confirmBtn.isEnabled = false;
            tips.SetActive(false);
            expBarAfter.gameObject.SetActive(false);
            expBarForeground.SetActive(true);
            for (int i = 0; i < addTips.Length; i++)
            {
                addTips[i].SetActive(false);
            }
            selectTip.SetActive(true);
        }
        else
        {
            beforeLev.text = "等级" + updateMainHeroItem.skillStudyLev;
            mainCard.GetComponent<CardLarge>().SetCardTemplateID(updateMainHeroItem.templateID);
            mainCard.SetActive(true);
            selectTip.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                childCardsBG[i].GetComponent<UIButtonMessage>().enabled = true;
            }
            beforeUpdate.SetActive(true);
            int index = 0;
            for (int i = 0; i < updateChildHeroItems.Count; i++)
            {
                childCards[i].GetComponent<CardIcon>().SetCardTemplateID(updateChildHeroItems[i].templateID);
                childCards[i].SetActive(true);
                afterUpdate.SetActive(true);
                index++;
            }
            for (int i = index; i < 5; i++)
            {
                childCards[i].SetActive(false);
            }
            Tab_Studyskill tabStydyskill = TableManager.GetStudyskillByID(updateMainHeroItem.skillStudyId);
            if (tabStydyskill != null)
            {
                maxLev.text = "(最高等级" + tabStydyskill.SkillHighLevel + ")";
                skillDes.text = tabStydyskill.SkillDes;
                if (tabStydyskill.SkillNextLevel != -1)
                {
                    expBar.sliderValue = (float)updateMainHeroItem.skillStudyExp / (float)tabStydyskill.Experience;
                    expValue.text = updateMainHeroItem.skillStudyExp + "/" + tabStydyskill.Experience;
                }
                else
                {
                    expBar.sliderValue = 1;
                    expValue.text = tabStydyskill.Experience + "/" + tabStydyskill.Experience;
                }
                skillName.spriteName = tabStydyskill.SkillName;
            }
            if (index > 0)
            {
                confirmBtn.isEnabled = true;
                tips.SetActive(true);
                CalUpdateData();
                expBarAfter.gameObject.SetActive(true);
                expBarForeground.SetActive(false);
            }
            else
            {
                tips.SetActive(false);
                confirmBtn.isEnabled = false;
                afterUpdate.SetActive(false);
                expBarAfter.gameObject.SetActive(false);
                expBarForeground.SetActive(true);
            }
            for (int i = 0; i < addTips.Length; i++)
            {
                if (i < index)
                {
                    addTips[i].SetActive(false);
                }
                else
                {
                    addTips[i].SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// 计算升级数据
    /// </summary>
    private void CalUpdateData()
    {
        int exp = 0;
        //总共经验金钱
        for (int i = 0; i < updateChildHeroItems.Count; i++)
        {
            Tab_Studyskill tabStydyskill = TableManager.GetStudyskillByID(updateChildHeroItems[i].skillStudyId);
            if (tabStydyskill != null)
            {
                exp += tabStydyskill.SkillExperience;
            }
        }
        int nowLev = updateMainHeroItem.skillStudyLev;
        Tab_Studyskill mainCardSkill = TableManager.GetStudyskillByID(updateMainHeroItem.skillStudyId);
        costMoneyValue = mainCardSkill.CostMoney * updateChildHeroItems.Count;
        //bool isUpdate = false;
        int nowExp = updateMainHeroItem.skillStudyExp;
        while (exp + nowExp >= mainCardSkill.Experience)
        {
            if (mainCardSkill.SkillNextLevel != -1)
            {
                //isUpdate = true;
                //升级
                nowLev++;
                exp = exp + nowExp - mainCardSkill.Experience;
                nowExp = 0;
                mainCardSkill = TableManager.GetStudyskillByID(mainCardSkill.SkillNextLevel);
            }
            else
            {
                //满级
                exp = mainCardSkill.Experience;
                break;
            }
        }
        if (mainCardSkill.SkillNextLevel != -1)
        {
            expValue.text = (exp + nowExp) + "/" + mainCardSkill.Experience;
            expBarAfter.sliderValue = (float)(exp + nowExp) / (float)mainCardSkill.Experience;
        }
        else
        {
            expValue.text = mainCardSkill.Experience + "/" + mainCardSkill.Experience;
            expBarAfter.sliderValue = 1;
        }
        afterLeaderNum = mainCardSkill.LeaderNum;
        //if (isUpdate)
        //{
        //    //升级
        //    expBarAfter.sliderValue = 1;
        //    //afterSkillDes.text = mainCardSkill.SkillDes;
        //    //afterSkillDes.gameObject.SetActive(true);
        //}
        //else
        //{
        //    //没升级
        //    expBarAfter.sliderValue = (float)(exp + updateMainHeroItem.skillStudyExp) / (float)mainCardSkill.Experience;
        //    //afterSkillDes.gameObject.SetActive(false);
        //}
        afterLev.text = "等级" + nowLev;
        costMoney.text = costMoneyValue.ToString();
    }

    /// <summary>
    /// 检测卡牌是否还存在
    /// </summary>
    private void CardExist()
    {
        //检查背包中是否有内存中的选中卡牌如果没有清空数据
        if (cardList == null)
        {
            updateMainHeroItem = null;
            updateChildHeroItems.Clear();
        }
        else
        {
            if (updateMainHeroItem != null)
            {
                bool isFindMainHero = false;
                foreach (UserCardItem card in cardList)
                {
                    if (card.cardID == updateMainHeroItem.cardID)
                    {
                        updateMainHeroItem = card;
                        isFindMainHero = true;
                        break;
                    }
                }
                if (!isFindMainHero)
                {
                    //找不到主英雄 全清掉
                    updateMainHeroItem = null;
                    updateChildHeroItems.Clear();
                    return;
                }
            }
            else
            {
                //主英雄空子英雄也清空
                updateChildHeroItems.Clear();
                return;
            }
            if (updateChildHeroItems.Count > 0)
            {
                //如果子英雄和主英雄一样，清掉子英雄
                for (int i = 0; i < updateChildHeroItems.Count; i++)
                {
                    if (updateMainHeroItem.cardID == updateChildHeroItems[i].cardID)
                    {
                        updateChildHeroItems.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < updateChildHeroItems.Count; i++)
                {
                    bool isFindChildHero = false;
                    foreach (UserCardItem card in cardList)
                    {
                        if (card.cardID == updateChildHeroItems[i].cardID)
                        {
                            updateChildHeroItems[i] = card;
                            isFindChildHero = true;
                            break;
                        }
                    }
                    if (!isFindChildHero)
                    {
                        //找不到子英雄 清掉对应英雄
                        updateChildHeroItems.RemoveAt(i);
                        i--;
                    }
                }
                //把子英雄中被保护的英雄去掉
                for (int i = 0; i < updateChildHeroItems.Count; i++)
                {
                    if (updateChildHeroItems[i].isProtected)
                    {
                        updateChildHeroItems.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 确认升级
    /// </summary>
    private void Confirm()
    {
        Tab_Studyskill mainCardSkill = TableManager.GetStudyskillByID(updateMainHeroItem.skillStudyId);
        if (mainCardSkill.SkillHighLevel == updateMainHeroItem.skillStudyLev)
        {
            BoxManager.showMessageByID((int)MessageIdEnum.Msg213);
            return;
        }
        if (afterLeaderNum > mainCardSkill.LeaderNum)
        {
            BoxManager.showMessageByID((int)MessageIdEnum.Msg245, (afterLeaderNum - mainCardSkill.LeaderNum).ToString());
            UIEventListener.Get(BoxManager.getYesButton()).onClick += ConfirmUpdate;
        }
        else
        {
            ConfirmUpdate(gameObject);
        }
    }

    /// <summary>
    /// 确认升级
    /// </summary>
    private void ConfirmUpdate(GameObject go)
    {
        if (costMoneyValue > Obj_MyselfPlayer.GetMe().money)
        {
            NetworkSender.Instance().buyGold(BuyGoldFinish, 1);
            return;
        }
        NetworkSender.Instance().RequestUpdateSkill(UpdateSkillDone);
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
    /// 完成技能升级处理数据 1000 50
    /// </summary>
    /// <param name="isSuccess"></param>
    private void UpdateSkillDone(bool isSuccess)
    {
        Destroy(effectAni);
        GameObject newEffect = ResourceManager.Instance.loadEffect(effectName);
        newEffect.transform.parent = transform;
        newEffect.transform.localPosition = new Vector3(0, 50, -350);
        newEffect.transform.localScale = new Vector3(1000f, 1000f, 1f);
        effectAni = newEffect;
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        if (cardList != null)
        {
            foreach (UserCardItem card in cardList)
            {
                if (card.cardID == updateMainHeroItem.cardID)
                {
                    Obj_MyselfPlayer.GetMe().updateMainHeroItem = card;
                    Obj_MyselfPlayer.GetMe().updateChildHeroItems.Clear();
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
        Obj_MyselfPlayer.GetMe().skillHeroType = SkillHeroType.UPDATE_MAIN;
        mainLogic.SendMessage("OnSelectSkillHeroWindow");
    }

    //选择技能英雄
    private void SelectChildHero()
    {
        Obj_MyselfPlayer.GetMe().skillHeroType = SkillHeroType.UPDATE_CHILD;
        mainLogic.SendMessage("OnSelectSkillHeroWindow");
    }
}
