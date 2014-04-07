using UnityEngine;
using System.Collections;
using Games.LogicObject;
using System.Collections.Generic;
using Games.CharacterLogic;
using GCGame.Table;

public class SelectSkillHeroController : MonoBehaviour
{
    public GameObject grid;
    public GameObject bottomInfo;
    public UILabel costMoney;
    public UILabel exp;
    private GameObject mainLogic;
    private MainController mainController;
    private SkillHeroType skillHeroType;
    private Transform cachedTransform;
    private List<UserCardItem> cardList;
    private List<GameObject> items = new List<GameObject>();
    private string skillHeroItem = "SkillHeroItem";
    private List<UserCardItem> cardListSort = new List<UserCardItem>();
    private long[] teamMemberArray;
    private List<long> selectIdList = new List<long>();

    // Use this for initialization
    void Start()
    {
    }

    void OnEnable()
    {
        if (mainController == null)
        {
            mainLogic = GameObject.Find("MainUILogic");
            mainController = mainLogic.GetComponent<MainUILogic>().mainController.GetComponent<MainController>();
        }
        skillHeroType = Obj_MyselfPlayer.GetMe().skillHeroType;
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        cardListSort.Clear();
        teamMemberArray = Obj_MyselfPlayer.GetMe().teamMemberArray;
        if (skillHeroType == SkillHeroType.UPDATE_CHILD)
        {
            //初始化已选择被吞噬升级卡牌id信息
            selectIdList.Clear();
            if (Obj_MyselfPlayer.GetMe().updateChildHeroItems.Count > 0)
            {
                foreach (UserCardItem card in Obj_MyselfPlayer.GetMe().updateChildHeroItems)
                {
                    selectIdList.Add(card.cardID);
                }
            }
            FreshCostAndExpInfo();
        }
        FreshUI();
        if (cardList == null)
        {
            return;
        }
        cachedTransform = transform;
        //设置 items的获得函数
        BagItemsPage bagItempage = cachedTransform.GetComponent<BagItemsPage>();
        if (bagItempage != null)
        {
            bagItempage.onCalculateShowItemsFunction = null;
            bagItempage.onDragonFinishedClearItemsFun = null;
            bagItempage.onShowCardItemFunction = null;
            bagItempage.onCalculateShowItemsFunction += CalculateShowItems;
            bagItempage.onDragonFinishedClearItemsFun += OnDragonFinishedClearItemsFun;
            bagItempage.onShowCardItemFunction += ShowCardItem;
        }
        //开始分页显示功能
        if (cachedTransform != null)
        {
            cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages();
        }
        grid.GetComponent<UIGrid>().repositionNow = true;
    }

    void OnDisable()
    {
        resetItems();
    }

    /// <summary>
    /// 控制界面ui显示
    /// </summary>
    private void FreshUI()
    {
        if (skillHeroType == SkillHeroType.UPDATE_CHILD)
        {
            bottomInfo.SetActive(true);
            mainController.hideBottomBar();
        }
        else
        {
            bottomInfo.SetActive(false);
        }
    }

    /// <summary>
    /// 刷新消耗金币和获得经验信息
    /// </summary>
    private void FreshCostAndExpInfo()
    {
        int updateCostMoney = 0;
        int updateExp = 0;
        foreach (UserCardItem card in cardList)
        {
            for (int i = 0; i < selectIdList.Count; i++)
            {
                if (card.cardID == selectIdList[i])
                {
                    Tab_Studyskill tabStydyskill = TableManager.GetStudyskillByID(card.skillStudyId);
                    if (tabStydyskill != null)
                    {
                        updateExp += tabStydyskill.SkillExperience;
                    }
                    break;
                }
            }
        }
        Tab_Studyskill mainCardSkill = TableManager.GetStudyskillByID(Obj_MyselfPlayer.GetMe().updateMainHeroItem.skillStudyId);
        if (mainCardSkill != null)
        {
            updateCostMoney = mainCardSkill.CostMoney * selectIdList.Count;
        }
        costMoney.text = updateCostMoney.ToString();
        exp.text = updateExp.ToString();
    }

    /// <summary>
    /// 筛选卡牌
    /// </summary>
    /// <returns></returns>
    private List<UserCardItem> CalculateShowItems()
    {
        cardListSort.Clear();
        switch (skillHeroType)
        {
            case SkillHeroType.LEARN_MAIN:
                List<UserCardItem> cardInFigthArray = new List<UserCardItem>();
                foreach (UserCardItem card in cardList)
                {
                    if (Obj_MyselfPlayer.GetMe().learnMainHeroItem != null)
                    {
                        //跳过已选择主英雄
                        if (Obj_MyselfPlayer.GetMe().learnMainHeroItem.cardID == card.cardID)
                        {
                            continue;
                        }
                        else
                        {
                            bool isFind = false;
                            for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
                            {
                                if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[i])
                                {
                                    cardInFigthArray.Add(card);
                                    isFind = true;
                                    break;
                                }
                            }
                            if (!isFind)
                            {
                                cardListSort.Add(card);
                            }
                        }
                    }
                    else
                    {
                        bool isFind = false;
                        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
                        {
                            if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[i])
                            {
                                cardInFigthArray.Add(card);
                                isFind = true;
                                break;
                            }
                        }
                        if (!isFind)
                        {
                            cardListSort.Add(card);
                        }
                    }
                }
                cardListSort.Sort(CompareTo);
                if (cardListSort.Count > 0)
                {
                    cardListSort.InsertRange(0, cardInFigthArray);
                }
                else
                {
                    cardListSort.AddRange(cardInFigthArray);
                }
                //把选择的主英雄放在前面
                if (Obj_MyselfPlayer.GetMe().learnMainHeroItem != null)
                {
                    PutFirst(Obj_MyselfPlayer.GetMe().learnMainHeroItem.cardID);
                }
                break;
            case SkillHeroType.LEARN_CHILD:
                foreach (UserCardItem card in cardList)
                {
                    //跳过主英雄
                    if (Obj_MyselfPlayer.GetMe().learnMainHeroItem.cardID == card.cardID)
                    {
                        continue;
                    }
                    //跳过在出阵侠士中的英雄
                    bool inTeam = IsInTeam(card);
                    if (inTeam)
                    {
                        continue;
                    }
                    //跳过没有技能的英雄
                    Tab_Studyskill studySkill = TableManager.GetStudyskillByID(card.skillStudyId);
                    if (studySkill == null)
                    {
                        continue;
                    }
                    //跳过被保护英雄
                    if (card.isProtected)
                    {
                        continue;
                    }
                    if (Obj_MyselfPlayer.GetMe().learnChildHeroItem != null)
                    {
                        //跳过已选择子英雄
                        if (Obj_MyselfPlayer.GetMe().learnChildHeroItem.cardID == card.cardID)
                        {
                            continue;
                        }
                        else
                        {
                            cardListSort.Add(card);
                        }
                    }
                    else
                    {
                        cardListSort.Add(card);
                    }
                }
                cardListSort.Sort(SkillLevCompareTo);
                //把选择的子英雄放在前面
                if (Obj_MyselfPlayer.GetMe().learnChildHeroItem != null)
                {
                    PutFirst(Obj_MyselfPlayer.GetMe().learnChildHeroItem.cardID);
                }
                break;
            case SkillHeroType.UPDATE_MAIN:
                List<UserCardItem> cardInFigthArrayUpdate = new List<UserCardItem>();
                foreach (UserCardItem card in cardList)
                {
                    //跳过没有技能的英雄
                    Tab_Studyskill studySkill = TableManager.GetStudyskillByID(card.skillStudyId);
                    if (studySkill == null)
                    {
                        continue;
                    }
                    if (Obj_MyselfPlayer.GetMe().updateMainHeroItem != null)
                    {
                        //跳过已选择主英雄
                        if (Obj_MyselfPlayer.GetMe().updateMainHeroItem.cardID == card.cardID)
                        {
                            continue;
                        }
                        else
                        {
                            bool isFind = false;
                            for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
                            {
                                if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[i])
                                {
                                    cardInFigthArrayUpdate.Add(card);
                                    isFind = true;
                                    break;
                                }
                            }
                            if (!isFind)
                            {
                                cardListSort.Add(card);
                            }
                        }
                    }
                    else
                    {
                        bool isFind = false;
                        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
                        {
                            if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[i])
                            {
                                cardInFigthArrayUpdate.Add(card);
                                isFind = true;
                                break;
                            }
                        }
                        if (!isFind)
                        {
                            cardListSort.Add(card);
                        }
                    }
                }
                cardListSort.Sort(SkillLevCompareTo);
                if (cardListSort.Count > 0)
                {
                    cardListSort.InsertRange(0, cardInFigthArrayUpdate);
                }
                else
                {
                    cardListSort.AddRange(cardInFigthArrayUpdate);
                }
                //把选择的主英雄放在前面
                if (Obj_MyselfPlayer.GetMe().updateMainHeroItem != null)
                {
                    PutFirst(Obj_MyselfPlayer.GetMe().updateMainHeroItem.cardID);
                }
                break;
            case SkillHeroType.UPDATE_CHILD:
                foreach (UserCardItem card in cardList)
                {
                    //跳过主英雄
                    if (Obj_MyselfPlayer.GetMe().updateMainHeroItem.cardID == card.cardID)
                    {
                        continue;
                    }
                    //跳过在出阵侠士中的英雄
                    bool inTeam = IsInTeam(card);
                    if (inTeam)
                    {
                        continue;
                    }
                    //跳过没有技能的英雄和不是同技能组的英雄
                    Tab_Studyskill studySkill = TableManager.GetStudyskillByID(card.skillStudyId);
                    if (studySkill == null || studySkill.Skillgroup != TableManager.GetStudyskillByID(Obj_MyselfPlayer.GetMe().updateMainHeroItem.skillStudyId).Skillgroup)
                    {
                        continue;
                    }
                    //跳过被保护英雄
                    if (card.isProtected)
                    {
                        continue;
                    }
                    //跳过已选择吞噬英雄
                    if (Obj_MyselfPlayer.GetMe().updateChildHeroItems.Count > 0)
                    {
                        bool isFind = false;
                        foreach (UserCardItem childCard in Obj_MyselfPlayer.GetMe().updateChildHeroItems)
                        {
                            if (childCard.cardID == card.cardID)
                            {
                                isFind = true;
                                break;
                            }
                        }
                        if (isFind)
                        {
                            continue;
                        }
                    }
                    cardListSort.Add(card);
                }
                cardListSort.Sort(SkillExpCompareTo);
                //把已选择吞噬卡牌放在前面
                List<UserCardItem> cardInSelectArray = new List<UserCardItem>();
                for (int i = 0; i < Obj_MyselfPlayer.GetMe().updateChildHeroItems.Count; i++)
                {
                    foreach (var card in cardList)
                    {
                        if (card.cardID == Obj_MyselfPlayer.GetMe().updateChildHeroItems[i].cardID)
                        {
                            cardInSelectArray.Add(card);
                            break;
                        }
                    }
                }
                if (cardInSelectArray.Count > 0)
                {
                    if (cardListSort.Count > 0)
                    {
                        cardListSort.InsertRange(0, cardInSelectArray);
                    }
                    else
                    {
                        cardListSort.AddRange(cardInSelectArray);
                    }
                }
                break;
            default:
                break;
        }
        return cardListSort;
    }

    /// <summary>
    /// 按技能等级品质排序
    /// </summary>
    /// <param name="cardA"></param>
    /// <param name="cardB"></param>
    /// <returns></returns>
    static public int SkillLevCompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        if (TableManager.GetStudyskillByID(cardA.skillStudyId).SkillQuality != TableManager.GetStudyskillByID(cardB.skillStudyId).SkillQuality)
        {
            return TableManager.GetStudyskillByID(cardB.skillStudyId).SkillQuality.CompareTo(TableManager.GetStudyskillByID(cardA.skillStudyId).SkillQuality);
        }
        else if (TableManager.GetStudyskillByID(cardA.skillStudyId).SkillName != TableManager.GetStudyskillByID(cardB.skillStudyId).SkillName)
        {
            return (-1 * (TableManager.GetStudyskillByID(cardA.skillStudyId).SkillName.CompareTo(TableManager.GetStudyskillByID(cardB.skillStudyId).SkillName)));
        }
        else if (cardB.skillStudyLev != cardA.skillStudyLev)
        {
            return cardB.skillStudyLev.CompareTo(cardA.skillStudyLev);
        }
        else
        {
            return (-1 * cardB.templateID.CompareTo(cardA.templateID));
        }
    }

    /// <summary>
    /// 技能经验排序
    /// </summary>
    /// <param name="cardA"></param>
    /// <param name="cardB"></param>
    /// <returns></returns>
    static public int SkillExpCompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        if (TableManager.GetStudyskillByID(cardB.skillStudyId).SkillExperience != TableManager.GetStudyskillByID(cardA.skillStudyId).SkillExperience)
        {
            return TableManager.GetStudyskillByID(cardB.skillStudyId).SkillExperience.CompareTo(TableManager.GetStudyskillByID(cardA.skillStudyId).SkillExperience);
        }
        else
        {
            return (-1 * TableManager.GetStudyskillByID(cardB.skillStudyId).SkillQuality.CompareTo(TableManager.GetStudyskillByID(cardA.skillStudyId).SkillQuality));
        }
    }

    /// <summary>
    /// 普通排序
    /// </summary>
    /// <param name="cardA"></param>
    /// <param name="cardB"></param>
    /// <returns></returns>
    static public int CompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        if (cardB.GetLeaderShip() != cardA.GetLeaderShip())
        {
            return cardB.GetLeaderShip().CompareTo(cardA.GetLeaderShip());
        }
        else if (cardB.templateID != cardA.templateID)
        {
            return (-1 * cardB.templateID.CompareTo(cardA.templateID));
        }
        else
        {
            return cardB.level.CompareTo(cardA.level);
        }
    }

    /// <summary>
    /// 把已选择英雄放到前面
    /// </summary>
    /// <param name="cardID"></param>
    private void PutFirst(long cardID)
    {
        foreach (UserCardItem card in cardList)
        {
            if (cardID == card.cardID)
            {
                if (cardListSort.Count > 0)
                {
                    cardListSort.Insert(0, card);
                }
                else
                {
                    cardListSort.Add(card);
                }
                break;
            }
        }
    }

    /// <summary>
    /// 判断卡牌是否在出阵队伍中
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    private bool IsInTeam(UserCardItem card)
    {
        bool isIn = false;
        for (int i = 0; i < teamMemberArray.Length; i++)
        {
            if (teamMemberArray[i] == card.cardID)
            {
                isIn = true;
                break;
            }
        }
        return isIn;
    }

    /// <summary>
    /// 拖拽完的调用函数
    /// </summary>
    /// <param name="dragonMode"></param>
    private void OnDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode dragonMode)
    {
        resetItems();
    }

    /// <summary>
    /// 清理items
    /// </summary>
    private void resetItems()
    {
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                CardListItemPool.Instance.DestroyItemAndPushToPool(item, skillHeroItem);
            }
        }
        items.Clear();
    }

    /// <summary>
    /// Item赋值
    /// </summary>
    /// <param name="card"></param>
    private void ShowCardItem(UserCardItem card)
    {
        if (card == null)
        {
            return;
        }
        GameObject newItem = CardListItemPool.Instance.GetListItem(skillHeroItem);
        newItem.transform.parent = grid.transform;
        if (skillHeroType == SkillHeroType.UPDATE_CHILD)
        {
            newItem.GetComponent<UISkillHeroItemView>().InitItem(card, true);
        }
        else
        {
            newItem.GetComponent<UISkillHeroItemView>().InitItem(card, false);
        }
        GameObject cardIconBtn = newItem.GetComponent<UISkillHeroItemView>().GetCardIconBtn();
        cardIconBtn.name = card.cardID.ToString();
        UIEventListener.Get(cardIconBtn).onClick += cardInfo;
        switch (skillHeroType)
        {
            case SkillHeroType.LEARN_MAIN:
                if (Obj_MyselfPlayer.GetMe().learnMainHeroItem != null)
                {
                    if (Obj_MyselfPlayer.GetMe().learnMainHeroItem.cardID == card.cardID)
                    {
                        newItem.GetComponent<UISkillHeroItemView>().IsChecked = true;
                        newItem.GetComponent<UISkillHeroItemView>().backGround.alpha = 0.5f;
                    }
                    else
                    {
                        UIEventListener.Get(newItem).onClick += SelectCard;
                    }
                }
                else
                {
                    UIEventListener.Get(newItem).onClick += SelectCard;
                }
                break;
            case SkillHeroType.LEARN_CHILD:
                if (Obj_MyselfPlayer.GetMe().learnChildHeroItem != null)
                {
                    if (Obj_MyselfPlayer.GetMe().learnChildHeroItem.cardID == card.cardID)
                    {
                        newItem.GetComponent<UISkillHeroItemView>().IsChecked = true;
                        newItem.GetComponent<UISkillHeroItemView>().backGround.alpha = 0.5f;
                    }
                    else
                    {
                        UIEventListener.Get(newItem).onClick += SelectCard;
                    }
                }
                else
                {
                    UIEventListener.Get(newItem).onClick += SelectCard;
                }
                break;
            case SkillHeroType.UPDATE_MAIN:
                if (Obj_MyselfPlayer.GetMe().updateMainHeroItem != null)
                {
                    if (Obj_MyselfPlayer.GetMe().updateMainHeroItem.cardID == card.cardID)
                    {
                        newItem.GetComponent<UISkillHeroItemView>().IsChecked = true;
                        newItem.GetComponent<UISkillHeroItemView>().backGround.alpha = 0.5f;
                    }
                    else
                    {
                        UIEventListener.Get(newItem).onClick += SelectCard;
                    }
                }
                else
                {
                    UIEventListener.Get(newItem).onClick += SelectCard;
                }
                break;
            case SkillHeroType.UPDATE_CHILD:
                if (Obj_MyselfPlayer.GetMe().updateChildHeroItems.Count > 0)
                {
                    foreach (UserCardItem childCard in Obj_MyselfPlayer.GetMe().updateChildHeroItems)
                    {
                        if (childCard.cardID == card.cardID)
                        {
                            newItem.GetComponent<UISkillHeroItemView>().IsChecked = true;
                        }
                    }
                }
                UIEventListener.Get(newItem).onClick += SelectCard;
                break;
            default:
                break;
        }
        items.Add(newItem);
    }

    /// <summary>
    /// 选择卡牌
    /// </summary>
    private void SelectCard(GameObject go)
    {
        long cardID = long.Parse(go.name);
        if (skillHeroType != SkillHeroType.UPDATE_CHILD)
        {
            foreach (UserCardItem card in cardList)
            {
                if (card.cardID == cardID)
                {
                    switch (skillHeroType)
                    {
                        case SkillHeroType.LEARN_MAIN:
                            Obj_MyselfPlayer.GetMe().learnMainHeroItem = card;
                            break;
                        case SkillHeroType.LEARN_CHILD:
                            Obj_MyselfPlayer.GetMe().learnChildHeroItem = card;
                            break;
                        case SkillHeroType.UPDATE_MAIN:
                            Obj_MyselfPlayer.GetMe().updateMainHeroItem = card;
                            Obj_MyselfPlayer.GetMe().updateChildHeroItems.Clear();
                            break;
                        default:
                            break;
                    }
                    ReturnToPrevious();
                    break;
                }
            }
        }
        else
        {
            bool isCheck = go.GetComponent<UISkillHeroItemView>().IsChecked;
            if (isCheck)
            {
                //非选中
                ItemDontSelect(cardID);
            }
            else
            {
                //选中
                ItemSelect(cardID);
            }
        }
    }

    /// <summary>
    /// 选中
    /// </summary>
    private void ItemSelect(long cardID)
    {
        if (selectIdList.Count == 5)
        {
            return;
        }
        selectIdList.Add(cardID);
        FreshItems();
    }

    /// <summary>
    /// 非选中
    /// </summary>
    private void ItemDontSelect(long cardID)
    {
        if (selectIdList.Count == 0)
        {
            return;
        }
        selectIdList.Remove(cardID);
        FreshItems();
    }

    /// <summary>
    /// 刷新Items状态
    /// </summary>
    private void FreshItems()
    {
        int count = grid.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject item = grid.transform.GetChild(i).gameObject;
            UISkillHeroItemView itemComponent = item.GetComponent<UISkillHeroItemView>();
            itemComponent.UIClear();
            if (selectIdList.Count == 5)
            {
                itemComponent.backGround.alpha = 0.5f;
            }
            long cardID = long.Parse(item.name);
            foreach (long selectCardID in selectIdList)
            {
                if (cardID == selectCardID)
                {
                    itemComponent.IsChecked = true;
                    itemComponent.backGround.alpha = 1;
                    break;
                }
            }
        }
        FreshCostAndExpInfo();
    }

    /// <summary>
    /// 点击小头像显示信息
    /// </summary>
    /// <param name="go"></param>
    private void cardInfo(GameObject go)
    {
        int cardID = int.Parse(go.name);
        for (int i = 0; i < cardList.Count; i++)
        {
            if (cardID == cardList[i].cardID)
            {
                BoxManager.showCardInfoMessage(cardID, cardList[i].templateID);
                break;
            }
        }
    }

    /// <summary>
    /// 确认选择被吞噬卡牌
    /// </summary>
    private void SelectConfirm()
    {
        Obj_MyselfPlayer.GetMe().updateChildHeroItems.Clear();
        for (int i = 0; i < selectIdList.Count; i++)
        {
            foreach (UserCardItem card in cardList)
            {
                if (card.cardID == selectIdList[i])
                {
                    Obj_MyselfPlayer.GetMe().updateChildHeroItems.Add(card);
                    break;
                }
            }
        }
        ReturnToPrevious();
    }

    /// <summary>
    /// 返回前一窗口
    /// </summary>
    private void ReturnToPrevious()
    {
        switch (skillHeroType)
        {
            case SkillHeroType.LEARN_MAIN:
                mainLogic.SendMessage("OnSkillLearnWindow");
                break;
            case SkillHeroType.LEARN_CHILD:
                mainLogic.SendMessage("OnSkillLearnWindow");
                break;
            case SkillHeroType.UPDATE_MAIN:
                mainLogic.SendMessage("OnSkillUpdateWindow");
                break;
            case SkillHeroType.UPDATE_CHILD:
                mainController.showBottomBar();
                mainLogic.SendMessage("OnSkillUpdateWindow");
                break;
            default:
                break;
        }
    }
}
