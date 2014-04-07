using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
//using System.Linq;
using card.net;

public class SelectTeamHeaderController : MonoBehaviour
{

    private string teamHeaderItem = "TeamHeaderItem";
    public GameObject grid;
    private GameObject mainUILogic;
    public UIScrollBar scrollBar;
    private List<UserCardItem> cardListSort = new List<UserCardItem>();
    private List<GameObject> items = new List<GameObject>();
    //Transform 的缓存
    private Transform cachedTransform;
    private List<UserCardItem> cardList;
    private int nowLeaderShip = 0;
    private int headerLeaderShip = 0;
    private int maxLeaderShip = 0;
    private int iguide = 0;
    public UIDraggablePanel dragPanel;
    public UILabel heroCountLabel;
    //public UILabel leaderShipShow;

    // Use this for initialization
    void Start()
    {
        mainUILogic = GameObject.Find("MainUILogic");
    }

    // Update is called once per frame
    void Update()
    {
        //FreashScroll();
    }

    void OnEnable()
    {
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        cardListSort.Clear();
        heroCountLabel.text = cardList.Count + "/" + Obj_MyselfPlayer.GetMe().bagMax;
        int count = grid.transform.childCount;
        for (int i = count - 1; i >= 0; i--)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
        grid.GetComponent<UIGrid>().repositionNow = true;

        grid.SetActive(true);
        //领导力
        nowLeaderShip = Obj_MyselfPlayer.GetMe().GetLeaderShipValue();
        headerLeaderShip = 0;
        maxLeaderShip = Obj_MyselfPlayer.GetMe().leadership;
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
        //leaderShipShow.text=nowLeaderShip+"/"+maxLeaderShip;

    }

    void OnDisable()
    {
        resetItems();
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

    //清理Items
    public void resetItems()
    {
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                //                item.transform.parent = null;
                //                Destroy(item);	
                CardListItemPool.Instance.DestroyItemAndPushToPool(item, teamHeaderItem);
            }
        }
        items.Clear();
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

        //cardListSort = cardInFigthArray;
        //cardListSort.Add(NormalCard);
        //cardListSort.Add(expCard);
        //cardListSort.Add(evolutionCard);
        /*
		
        for(int i=0;i<Obj_MyselfPlayer.GetMe().teamMemberArray.Length;i++)
        {
            for(int j=0;j<cardList.Count;j++)
            {
                if(Obj_MyselfPlayer.GetMe().teamMemberArray[i]==cardList[j].cardID)
                {
                    cardListSort.Add(cardList[j]);
                    break;
                }
            }
        }
		
        for(int i=0;i<cardList.Count;i++)
        {
            bool isFind=false;
            for(int j=0; j<Obj_MyselfPlayer.GetMe().teamMemberArray.Length; j++)
            {
                if(cardList[i].cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[j])
                {
                    isFind=true;
                    break;
                }
            }
            if(!isFind)
            {
                cardListSort.Add(cardList[i]);
            }
        }
		
        */


        return cardListSort;
    }

    //拖拽完的调用函数
    public void OnDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode dragonMode)
    {
        //清理上一页的Items
        this.resetItems();
    }

    public void ShowCardItem(UserCardItem card)
    {
        if (card == null)
        {
            return;
        }
        if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[0])
        {
            //GameObject newItem = ResourceManager.Instance.loadWidget(teamHeaderItem);
            GameObject newItem = CardListItemPool.Instance.GetListItem(teamHeaderItem);
			newItem.transform.GetComponent<UIDragPanelContents>().draggablePanel = dragPanel;
            newItem.transform.parent = grid.transform;
            newItem.transform.localPosition = new Vector3(0, 0, -1);
            newItem.transform.localScale = new Vector3(1, 1, 1);
            SetHeaderItem(newItem, card, true);
            //------------------------卡牌背景及外框--------------------------------
            //2013-10-12 Jack Wen
            UISprite icon_bg = newItem.transform.FindChild("CardIcon").GetChild(0).FindChild("Sprite-Frame").GetComponent<UISprite>();
            UISprite icon_border = newItem.transform.FindChild("CardIcon").GetChild(0).FindChild("Sprite-BG").GetComponent<UISprite>();
            int icon_star = TableManager.GetCardByID(card.templateID).Star;
            icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
            icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
            //--------------------------------------------------------------------
            newItem.transform.FindChild("Sprites/Sprite-Header").gameObject.SetActive(true);
            newItem.transform.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = true;
            newItem.transform.FindChild("Checkbox").GetComponent<UICheckbox>().enabled = false;
            newItem.transform.FindChild("BG").GetComponent<UIImageButton>().isEnabled = false;
            newItem.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 0.5f;
            //newItem.transform.FindChild("BG").GetComponent<UIButton>().isEnabled = false;
            headerLeaderShip = card.GetLeaderShip();
            items.Add(newItem);
        }
        else
        {
            //GameObject newItem = ResourceManager.Instance.loadWidget(teamHeaderItem);
            GameObject newItem = CardListItemPool.Instance.GetListItem(teamHeaderItem);
			newItem.transform.GetComponent<UIDragPanelContents>().draggablePanel = dragPanel;
            newItem.transform.parent = grid.transform;
            newItem.transform.localPosition = new Vector3(0, 0, -1);
            newItem.transform.localScale = new Vector3(1, 1, 1);
            items.Add(newItem);
            //------------------------卡牌背景及外框--------------------------------
            //2013-10-12 Jack Wen
            UISprite icon_bg = newItem.transform.FindChild("CardIcon").GetChild(0).FindChild("Sprite-Frame").GetComponent<UISprite>();
            UISprite icon_border = newItem.transform.FindChild("CardIcon").GetChild(0).FindChild("Sprite-BG").GetComponent<UISprite>();
            int icon_star = TableManager.GetCardByID(card.templateID).Star;
            icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
            icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
            //--------------------------------------------------------------------
            newItem.transform.FindChild("Sprites/Sprite-Header").gameObject.SetActive(false);
            newItem.transform.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = false;
            newItem.transform.FindChild("Checkbox").GetComponent<UICheckbox>().enabled = true;
            newItem.transform.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
            newItem.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1f;
            //测试 领导力判断
            bool isFind = false;
            for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
            {
                if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[i])
                {
                    isFind = true;
                    break;
                }
            }
            if (isFind)
            {
                if (nowLeaderShip - headerLeaderShip > maxLeaderShip)
                {
                    SetHeaderItem(newItem, card, false);
                    //newItem.transform.FindChild("BG").GetComponent<UIButton>().isEnabled = false;
                    newItem.transform.FindChild("BG").GetComponent<UIImageButton>().isEnabled = false;
                    newItem.transform.FindChild("Checkbox").GetComponent<UICheckbox>().enabled = false;
                    newItem.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 0.5f;
                }
                else
                {
                    UIEventListener.Get(newItem).onClick += SelectHeader;
                    SetHeaderItem(newItem, card, true);
                    //newItem.transform.FindChild("BG").GetComponent<UIButton>().isEnabled = true;
                    newItem.transform.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
                    newItem.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
                }
            }
            else
            {
                if (nowLeaderShip - headerLeaderShip + card.GetLeaderShip() > maxLeaderShip)
                {
                    SetHeaderItem(newItem, card, false);
                    //newItem.transform.FindChild("BG").GetComponent<UIButton>().isEnabled = false;
                    newItem.transform.FindChild("BG").GetComponent<UIImageButton>().isEnabled = false;
                    newItem.transform.FindChild("Checkbox").GetComponent<UICheckbox>().enabled = false;
                    newItem.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 0.5f;
                }
                else
                {
                    UIEventListener.Get(newItem).onClick += SelectHeader;
                    SetHeaderItem(newItem, card, true);
                    //newItem.transform.FindChild("BG").GetComponent<UIButton>().isEnabled = true;
                    newItem.transform.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
                    newItem.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
                }
            }
            if (iguide == 0)
            {
                guideItem = newItem;
                iguide = -1;
            }
        }
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

    void SetHeaderItem(GameObject newItem, UserCardItem card, bool canUse)
    {
        newItem.name = card.cardID.ToString();
        GameObject cardIcon = newItem.transform.FindChild("CardIcon").GetChild(0).gameObject;
        UISprite icon = cardIcon.transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
        string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).HeadIcon;
        AtlasManager.Instance.setHeadName(icon, atlasname);
        icon.transform.localScale = new Vector3(82, 82, 1);

        UILabel name = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
        name.text = LanguageManger.GetWords(TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).Name);

        UILabel hp = newItem.transform.FindChild("Labels/Label-Hp-Value").GetComponent<UILabel>();
        hp.text = card.GetHp().ToString();

        UILabel level = newItem.transform.FindChild("Labels/Panel-Lv/Label-lv").GetComponent<UILabel>();
        level.text = card.level.ToString();

        UILabel attack = newItem.transform.FindChild("Labels/Label-Attack-Value").GetComponent<UILabel>();
        attack.text = card.GetAttack().ToString();

        UILabel leadership = newItem.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>();
        if (!canUse)
        {
            leadership.text = "[FF231A]" + card.GetLeaderShip().ToString() + "[000000]";
        }
        else
        {
            leadership.text = "[F1ECCF]" + card.GetLeaderShip().ToString() + "[000000]";
        }

        UISprite property = newItem.transform.FindChild("Sprites/Sprite-Property").GetComponent<UISprite>();
        property.spriteName = UserCardItem.elementTypeName[TableManager.GetCardByID(card.templateID).Element];

        Tab_Card tab_card = TableManager.GetCardByID(card.templateID);
        Tab_Leaderskill leaderSkill = TableManager.GetLeaderskillByID(tab_card.SkillLeader);
        UILabel leaderskill = newItem.transform.FindChild("Labels/Label-HeaderSkill").GetComponent<UILabel>();
        if (leaderSkill != null)
        {
            leaderskill.text = LanguageManger.GetWords(leaderSkill.Name);
        }
        else
        {
            leaderskill.text = "";
        }
        cardIcon.name = card.cardID.ToString();
        UIEventListener.Get(cardIcon).onClick += cardInfo;
    }

    public void SelectHeader(GameObject go)
    {
        long cardID = long.Parse(go.name);
        if (cardID <= 0)
        {
            NetworkSender.Instance().GetUserInfo(SelectHeaderDone);
            //BoxManager.showMessage("卡片信息有误!");   
        }
        else
        {
            Obj_MyselfPlayer.GetMe().teamMemberArray[0] = cardID;
            //计算teamMemberArray是否有重复
            for (int i = 1; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
            {
                if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == cardID)
                {
                    Obj_MyselfPlayer.GetMe().teamMemberArray[i] = -1;
                    break;
                }
            }
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
            Obj_MyselfPlayer.GetMe().RefreshBattleArray();
            NetworkSender.Instance().sendChangeMember(SelectHeaderDone, Obj_MyselfPlayer.GetMe().teamMemberArray);
        }
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

    public void SelectHeaderDone(bool bSuccess)
    {
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.LEADER)
        {
            recFinishStep(true);
        }
        else
        {
            mainUILogic.SendMessage("ReturnToMainUI", gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
    public void recFinishStep(bool isSuccess)
    {
        mainUILogic.SendMessage("ReturnToMainUI", gameObject, SendMessageOptions.DontRequireReceiver);
    }
    public void ReturnToMainUI()
    {
        mainUILogic.SendMessage("ReturnToMainUI");
    }


    //for new guide
    private GameObject guideItem;
    public GameObject getGuideItem()
    {
        return guideItem;
    }
    public void SetPanelListEnable(bool bEnable)
    {
        dragPanel.enabled = bEnable;
    }
}
