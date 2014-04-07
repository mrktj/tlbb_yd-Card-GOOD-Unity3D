using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
//using System.Linq;
using card.net;

public class SelectTeamMemberController : MonoBehaviour
{

    private string teamMemberItem = "TeamMemberItem";
    public GameObject grid;
    private GameObject mainUILogic;
    public UIScrollBar scrollBar;
    public UILabel leaderShipShow;
    public UILabel heroCount;
    private MainController mainController;
    private long[] templeTeam;
    private int nowLeaderShip;
    private int maxLeaderShip;
    private List<UserCardItem> cardList = new List<UserCardItem>();
    //Transform 的缓存
    private Transform cachedTransform;
    private List<UserCardItem> cardListSort = new List<UserCardItem>();
    private List<GameObject> items = new List<GameObject>();
    private int memberNum = 0;
    public UIDraggablePanel dragPanel;
    public UILabel heroCountLabel;
    // Use this for initialization
    void Awake()
    {
        templeTeam = new long[5];
    }

    // Update is called once per frame
    void Update()
    {
        //FreashScroll();
    }

    private void FreashScroll()
    {
        if (scrollBar.scrollValue == 0 || scrollBar.scrollValue == 1)
        {
            dragPanel.scale.y = 0.1f;
        }
        else
        {
            dragPanel.scale.y = 1;
        }
    }

    void OnEnable()
    {
        if (mainController == null)
        {
            mainUILogic = GameObject.Find("MainUILogic");
            mainController = mainUILogic.GetComponent<MainUILogic>().mainController.GetComponent<MainController>();
        }
        mainController.hideBottomBar();
        //获取用户背包
        //		List<UserCardItem> cardList;
        //获取领导力
        nowLeaderShip = Obj_MyselfPlayer.GetMe().GetLeaderShipValue();
        maxLeaderShip = Obj_MyselfPlayer.GetMe().leadership;
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        heroCountLabel.text = cardList.Count + "/" + Obj_MyselfPlayer.GetMe().bagMax;
        cardListSort.Clear();
        //保存队伍
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            templeTeam[i] = Obj_MyselfPlayer.GetMe().teamMemberArray[i];
        }
        int count = grid.transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
        grid.GetComponent<UIGrid>().repositionNow = true;

        //搜索队长卡牌
        grid.SetActive(true);
        memberNum = 0; //队员数量
        for (int i = 1; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] != -1)
                memberNum++;
        }
        //cardList.Sort(CompareTo);
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
        scrollBar.scrollValue = 0;
        leaderShipShow.text = nowLeaderShip + "/" + maxLeaderShip;
        heroCount.text = memberNum.ToString();

    }

    public void ShowCardItem(UserCardItem card)
    {
        if (card == null)
        {
            return;
        }
        if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[0])
        {
            GameObject newItem = CardListItemPool.Instance.GetListItem(teamMemberItem);
			newItem.transform.GetComponent<UIDragPanelContents>().draggablePanel = dragPanel;
            newItem.transform.parent = grid.transform;

            bool success = newItem.GetComponent<UICardItemView>().InitTeamMemberLeaderWithCard(card);
            GameObject cardIconBtn = newItem.GetComponent<UICardItemView>().GetCardIconBtn();
            UIEventListener.Get(cardIconBtn).onClick += cardInfo;

            items.Add(newItem);
        }
        else
        {
            GameObject newItem = CardListItemPool.Instance.GetListItem(teamMemberItem);
			newItem.transform.GetComponent<UIDragPanelContents>().draggablePanel = dragPanel;
            newItem.transform.parent = grid.transform;

            bool success = newItem.GetComponent<UICardItemView>().InitTeamMemberOtherWithCard(card, SelectMember, memberNum, nowLeaderShip, maxLeaderShip,false);
            GameObject cardIconBtn = newItem.GetComponent<UICardItemView>().GetCardIconBtn();
            UIEventListener.Get(cardIconBtn).onClick += cardInfo;

            items.Add(newItem);

            //判断是否是阮星竹
            if (card.templateID == 1190)
            {
                guideItem = newItem;
            }
            if (card.templateID == 1321 ||//包不同
                card.templateID == 1318 ||//风波恶
                card.templateID == 1052)//甘宝宝
            {
                guideItem2 = newItem;
            }
        }
    }

    public List<UserCardItem> CalculateShowItems()
    {
        //cardList.Sort(CompareTo);


        List<UserCardItem> cardInFigthArray = new List<UserCardItem>(); //----在上阵列表中的(也就是队员)
        List<UserCardItem> expCard = new List<UserCardItem>(); 			//---升级材料卡牌
        List<UserCardItem> evolutionCard = new List<UserCardItem>();	//---精进材料卡牌
        List<UserCardItem> NormalCard = new List<UserCardItem>();		//---非经验也非精进卡牌


        //先保证队长和队员顺序，加入上阵卡牌
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            foreach (var card in cardList)
            {
                if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[i])
                {
                    cardInFigthArray.Add(card);
                    break;
                }
            }
        }

        foreach (var card in cardList)
        {
            if (card.cardType == UserCardItem.CardType.EXP_CARD)
            {
                expCard.Add(card);
            }
            else if (card.cardType == UserCardItem.CardType.EVOLUTION_CARD)
            {
                evolutionCard.Add(card);
            }
            else if (!card.IsInFightArray())
            {
                NormalCard.Add(card);
            }
        }


        NormalCard.Sort(CompareTo);
        expCard.Sort(CompareTo);
        evolutionCard.Sort(CompareTo);


        cardListSort.Clear();
        cardListSort.AddRange(cardInFigthArray);
        cardListSort.AddRange(NormalCard);
        cardListSort.AddRange(expCard);
        cardListSort.AddRange(evolutionCard);


        return cardListSort;
    }

    //拖拽完的调用函数
    public void OnDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode dragonMode)
    {
        //清理上一页的Items
        this.resetItems();
    }

    //清理Items
    public void resetItems()
    {
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                //                item.transform.parent = null;
                //                Destroy(item);	
                CardListItemPool.Instance.DestroyItemAndPushToPool(item, teamMemberItem);
            }
        }
        items.Clear();
    }

    //排序算法
    static public int CompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        //排序顺序按照 先领导力 再ID，最后等级的方式进行
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

    void OnDisable()
    {
        this.resetItems();
        mainController.showBottomBar();
    }

    private int GetMaxStart(int cardID)
    {
        if (TableManager.GetCardByID(cardID).NextCard == -1)
        {
            return TableManager.GetCardByID(cardID).Star;
        }
        else
        {
            return GetMaxStart(TableManager.GetCardByID(cardID).NextCard);
        }
    }

    void DontSelect(long cardID)
    {
        //卡牌下阵
        for (int i = 1; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == cardID)
            {
                Obj_MyselfPlayer.GetMe().teamMemberArray[i] = -1;
            }
        }
        //重新排列
        List<long> newTeam = new List<long>();
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] != -1)
            {
                newTeam.Add(Obj_MyselfPlayer.GetMe().teamMemberArray[i]);
            }
        }
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            if (i < newTeam.Count)
            {
                Obj_MyselfPlayer.GetMe().teamMemberArray[i] = newTeam[i];
            }
            else
            {
                Obj_MyselfPlayer.GetMe().teamMemberArray[i] = -1;
            }
        }
        for (int i = 0; i < cardList.Count; i++)
        {
            if (cardList[i].cardID == cardID)
            {
                nowLeaderShip -= cardList[i].GetLeaderShip();
                break;
            }
        }
        //刷新列表
        FreashTeamUI();
    }
    void SelectMem(long cardID)
    {
        //卡牌上阵
        for (int i = 1; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == -1)
            {
                Obj_MyselfPlayer.GetMe().teamMemberArray[i] = cardID;
                break;
            }
        }
        for (int i = 0; i < cardList.Count; i++)
        {
            if (cardList[i].cardID == cardID)
            {
                nowLeaderShip += cardList[i].GetLeaderShip();
                break;
            }
        }
        //刷新列表
        FreashTeamUI();
    }
    void FreashTeamUI()
    {
        //刷新队员列表显示
        memberNum = 0;
        int count = grid.transform.childCount;
        for (int i = 1; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] != -1)
                memberNum++;
        }
        if (memberNum >= 4)
        {
            for (int i = 0; i < count; i++)
            {
                Transform item = grid.transform.GetChild(i);
                item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = false;
                //item.FindChild("BG").GetComponent<UIButton>().isEnabled = false;
                item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = false;
                item.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 0.5f;
                //item.FindChild("BackgroundEnable").gameObject.SetActive(false);
                //item.FindChild("BackgroundDisable").gameObject.SetActive(true);
                UIEventListener.Get(item.gameObject).onClick -= SelectMember;
                int leaderShipValue = 0;
                for (int j = 0; j < cardList.Count; j++)
                {
                    if (cardList[j].cardID == long.Parse(item.gameObject.name))
                    {
                        leaderShipValue = cardList[j].GetLeaderShip();
                        break;
                    }
                }
                //item.GetComponent<UIButtonMessage>().enabled = false;
                item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValue + "[000000]";
                //item.transform.FindChild("Labels/Label-CanUse").GetComponent<UILabel>().text="";
                for (int j = 1; j < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; j++)
                {
                    if (long.Parse(item.name) == Obj_MyselfPlayer.GetMe().teamMemberArray[j])
                    {
                        item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = true;
                        //item.FindChild("BG").GetComponent<UIButton>().isEnabled = true;
                        item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
                        item.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
                        //item.FindChild("BackgroundEnable").gameObject.SetActive(true);
                        //item.FindChild("BackgroundDisable").gameObject.SetActive(false);
                        UIEventListener.Get(item.gameObject).onClick += SelectMember;
                        //item.GetComponent<UIButtonMessage>().enabled = true;
                        item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValue + "[000000]";
                        //item.transform.FindChild("Labels/Label-CanUse").GetComponent<UILabel>().text="";
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                Transform item = grid.transform.GetChild(i);
                UIEventListener.Get(item.gameObject).onClick -= SelectMember;
                bool canUse = true;
                int leaderShipValue = 0;
                for (int k = 0; k < cardList.Count; k++)
                {
                    if (cardList[k].cardID == long.Parse(item.name))
                    {
                        leaderShipValue = cardList[k].GetLeaderShip();
                        if (nowLeaderShip + cardList[k].GetLeaderShip() > maxLeaderShip)
                        {
                            canUse = false;
                        }
                        break;
                    }
                }
                if (canUse)
                {
                    item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = false;
                    //item.FindChild("BG").GetComponent<UIButton>().isEnabled = true;
                    item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
                    item.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
                    //item.FindChild("BackgroundEnable").gameObject.SetActive(true);
                    //item.FindChild("BackgroundDisable").gameObject.SetActive(false);
                    UIEventListener.Get(item.gameObject).onClick += SelectMember;
                    //item.GetComponent<UIButtonMessage>().enabled = true;
                    item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValue + "[000000]";
                    //item.transform.FindChild("Labels/Label-CanUse").GetComponent<UILabel>().text="";
                }
                else
                {
                    item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = false;
                    //item.FindChild("BG").GetComponent<UIButton>().isEnabled = false;
                    item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = false;
                    item.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 0.5f;
                    //item.FindChild("BackgroundEnable").gameObject.SetActive(false);
                    //item.FindChild("BackgroundDisable").gameObject.SetActive(true);
                    //item.GetComponent<UIButtonMessage>().enabled = false;
                    item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[FF231A]" + leaderShipValue + "[000000]";
                    //item.transform.FindChild("Labels/Label-CanUse").GetComponent<UILabel>().text="领导力不足";
                }
                for (int j = 1; j < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; j++)
                {
                    if (long.Parse(item.name) == Obj_MyselfPlayer.GetMe().teamMemberArray[j])
                    {
                        item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = true;
                        //item.FindChild("BG").GetComponent<UIButton>().isEnabled = true;
                        item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
                        item.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
                        //item.FindChild("BackgroundEnable").gameObject.SetActive(true);
                        //item.FindChild("BackgroundDisable").gameObject.SetActive(false);
                        UIEventListener.Get(item.gameObject).onClick -= SelectMember;
                        UIEventListener.Get(item.gameObject).onClick += SelectMember;
                        //item.GetComponent<UIButtonMessage>().enabled = true;
                        item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValue + "[000000]";
                        //item.transform.FindChild("Labels/Label-CanUse").GetComponent<UILabel>().text="";
                        break;
                    }
                }
            }
        }
        //队长不可选
        Transform leader = grid.transform.GetChild(0);
        if (Obj_MyselfPlayer.GetMe().teamMemberArray.Length > 0)
        {
            if (Obj_MyselfPlayer.GetMe().teamMemberArray[0] == long.Parse(leader.gameObject.name))
            {
                leader.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = true;
                leader.FindChild("BG").GetComponent<UIImageButton>().isEnabled = false;
                leader.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 0.5f;
                UIEventListener.Get(leader.gameObject).onClick -= SelectMember;
                int leaderShipValueHeader = 0;
                for (int j = 0; j < cardList.Count; j++)
                {
                    if (cardList[j].cardID == long.Parse(leader.gameObject.name))
                    {
                        leaderShipValueHeader = cardList[j].GetLeaderShip();
                        break;
                    }
                }
                leader.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValueHeader + "[000000]";
            }
        }
        leaderShipShow.text = nowLeaderShip + "/" + maxLeaderShip;
        heroCount.text = memberNum.ToString();
    }

    public void SelectMember(GameObject go)
    {
        bool isChecked = go.transform.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked;
        if (isChecked)
        {
            DontSelect(long.Parse(go.name));
        }
        else
        {
            //			if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.TEAM_MEMBER)
            //			{
            //				GuideManager.Instance.getCurrentGuideWindow().GetComponent<GuideMemberController>().OnItemClick(null);
            //			}
            SelectMem(long.Parse(go.name));
        }

        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.TEAM_MEMBER)
            GuideMember.Instance.NextStep();//选择队员指引阶段 SELECT_2
        else if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.TEAM_MEMBER2)
            GuideMember2.Instance.NextStep();//选择队员指引阶段 SELECT_2

    }

    public void cardInfo(GameObject go)
    {
        if (!GuideManager.Instance.isEnd())
        {
            return;
        }
        int cardID = int.Parse(go.name);
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().cardBagList.Count; i++)
        {
            if (cardID == Obj_MyselfPlayer.GetMe().cardBagList[i].cardID)
            {
                BoxManager.showCardInfoMessage(cardID, Obj_MyselfPlayer.GetMe().cardBagList[i].templateID);
                break;
            }
        }
    }

    public void ResetTeam()
    {
        //还原队伍
        for (int i = 0; i < 5; i++)
        {
            Obj_MyselfPlayer.GetMe().teamMemberArray[i] = templeTeam[i];
        }
        mainUILogic.SendMessage("ReturnToMainUI", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    public void ChangeMember()
    {
        //王明磊 : 统计模块代码 -> Statistics
        //如果是Guide阶段,需要统计此按钮的点击信息
        if (GameManager.Instance.isGuideMode())
            PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn14).ToString());
        Obj_MyselfPlayer.GetMe().RefreshBattleArray();
        NetworkSender.Instance().sendChangeMember(ChangeMemberDone, Obj_MyselfPlayer.GetMe().teamMemberArray);
    }

    public void ChangeMemberDone(bool bSuccess)
    {
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.TEAM_MEMBER2)
        {
            GuideMember2.Instance.NextStep();//选择队员指引阶段 SELECT_3
            recFinishStep(true);
        }
        else
        {
            mainUILogic.SendMessage("ReturnToMainUI", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
    public void recFinishStep(bool bSuccess)
    {
        mainUILogic.SendMessage("ReturnToMainUI", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    //for new guide
    private GameObject guideItem;
    public GameObject getGuideItem()
    {
        return guideItem;
    }
    private GameObject guideItem2;
    public GameObject getGuideItem2()
    {
        return guideItem2;
    }
    public GameObject getGuideConfirmButton()
    {
        return transform.FindChild("BottomInfo/TopRightBtn").gameObject;
    }
    public void SetPanelListEnable(bool bEnable)
    {
        dragPanel.enabled = bEnable;
    }
}
