using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.LogicObject;
using Games.CharacterLogic;
using GCGame.Table;

public class PVPSelectTeamMemberController : MonoBehaviour {
	
	public static bool isSelectLeader = false; //静态变量由调用处赋值
	private string teamMemberItem = "TeamMemberItem";
	private string teamHeaderItem = "TeamHeaderItem";
    public GameObject grid;
    private MainUILogic mainUILogic;
    public UIScrollBar scrollBar;
    public UILabel leaderShipShow;
    public UILabel heroCount;
    private MainController mainController;
    public UIDraggablePanel dragPanel;
    public UILabel heroCountLabel;
	public UISprite title;
	public GameObject bottomBox;
	private Transform cachedTransform;
	private List<UserCardItem> cardList = new List<UserCardItem>();
	private List<GameObject> items = new List<GameObject>();
	private int memberNum;
	private int nowLeaderShip = 0;
    private int headerLeaderShip = 0;
    private int maxLeaderShip = 0;
	private long[] TempBattleArray ={ -1, -1,-1, -1, -1, -1};	       //--缓存新PVP阵形,
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable() {
		
		for (int i = 0; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
		{
			TempBattleArray[i] = Obj_MyselfPlayer.GetMe().PvPBattleArray[i];
		}
		
//		Debug.LogError(isSelectLeader == true ? "Select Team Leader" : "Select Team Member");
		mainUILogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		if (mainController == null)
            mainController = mainUILogic.mainController.GetComponent<MainController>();
		if (isSelectLeader && bottomBox != null)
		{
			bottomBox.SetActive(false);
			title.spriteName = "zhujiemian_xuanzeduizhang";
			mainController.showBottomBar();
		}
		else
		{
			bottomBox.SetActive(true);
			title.spriteName = "zhujiemian_xuanzeduiyuan";
			mainController.hideBottomBar();
		}
		initCardList();
		
	}
	
	void initCardList()
	{
		cardList.Clear();
		foreach (UserCardItem item in Obj_MyselfPlayer.GetMe().cardBagList)
		{
			if (item.quality != Obj_MyselfPlayer.GetMe().curPvPStar || item.cardType != UserCardItem.CardType.NORMAL_CARD)
				continue;
			cardList.Add(item);
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
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().PvPBattleArray[i] != -1)
                memberNum++;
        }
		memberNum--;
	    //领导力
        nowLeaderShip = Obj_MyselfPlayer.GetMe().GetPvPLeaderShipValue();
        headerLeaderShip = 0;
        maxLeaderShip = Obj_MyselfPlayer.GetMe().leadership; //PVP也用外面统一的领导力
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
			if (isSelectLeader)
				bagItempage.onShowCardItemFunction += SelectLeaderShowCardItem;
			else
				bagItempage.onShowCardItemFunction += SelectMemberShowCardItem;
        }
        //开始分页显示功能
        if (cachedTransform != null)
        {
            cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages();
        }
		if (!isSelectLeader)
		{
			leaderShipShow.text = nowLeaderShip + "/" + maxLeaderShip;
			heroCount.text = memberNum.ToString();
		}
			
        grid.GetComponent<UIGrid>().repositionNow = true;
        scrollBar.scrollValue = 0;
        
	}
	
	public List<UserCardItem> CalculateShowItems()
    {
        List<UserCardItem> cardInFigthArray = new List<UserCardItem>(); //----在上阵列表中的(也就是队员)
        List<UserCardItem> NormalCard = new List<UserCardItem>();		//---非经验也非精进卡牌

        //先保证队长和队员顺序，加入上阵卡牌
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
        {
			foreach(UserCardItem card in cardList)
			{
				if (card.cardID == Obj_MyselfPlayer.GetMe().PvPBattleArray[i])
				{
					if (card.cardID != Obj_MyselfPlayer.GetMe().curPvPLearder)
                    	cardInFigthArray.Add(card);
					else
						cardInFigthArray.Insert(0,card);
                    break;
				}
			}
        }

        foreach (var card in cardList)
        {
            if (!cardInFigthArray.Contains(card))
            {
                NormalCard.Add(card);
            }
        }

        NormalCard.Sort(CompareTo);

        cardList.Clear();
        cardList.AddRange(cardInFigthArray);
        cardList.AddRange(NormalCard);
//		Debug.LogError("~~~~~~~~~~~~~~~~~~~~~~");
//		foreach(UserCardItem item in cardList)
//			Debug.LogError("Card : " + item.cardID);
//		Debug.LogError("~~~~~~~~~~~~~~~~~~~~~~");
        return cardList;
        
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
	
	//拖拽完的调用函数
    public void OnDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode dragonMode)
    {
        //清理上一页的Items
        this.resetItems();
    }
	
	void OnDisable()
    {
       	resetItems();
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
                CardListItemPool.Instance.DestroyItemAndPushToPool(item, isSelectLeader ? teamHeaderItem : teamMemberItem);
            }
        }
        items.Clear();
    }
	
	public void SelectLeaderShowCardItem(UserCardItem card)
    {
        if (card == null)
        {
            return;
        }
//		Debug.LogError("Card ID = " + card.cardID);
        if (card.cardID == Obj_MyselfPlayer.GetMe().curPvPLearder)
        {
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
            
			UIEventListener.Get(newItem).onClick += SelectHeader;
            SetHeaderItem(newItem, card, true);
            //newItem.transform.FindChild("BG").GetComponent<UIButton>().isEnabled = true;
            newItem.transform.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
            newItem.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
        }
    }
	
	public void SelectMemberShowCardItem(UserCardItem card)
    {
        if (card == null)
        {
            return;
        }
        if (card.cardID == Obj_MyselfPlayer.GetMe().curPvPLearder) //队长
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
            bool success = newItem.GetComponent<UICardItemView>().InitTeamMemberOtherWithCard(card, SelectMember, memberNum, nowLeaderShip, maxLeaderShip,true);
            GameObject cardIconBtn = newItem.GetComponent<UICardItemView>().GetCardIconBtn();
            UIEventListener.Get(cardIconBtn).onClick += cardInfo;
            items.Add(newItem);
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

	
	public void cardInfo(GameObject go)
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
	
	public void SelectHeader(GameObject go)
    {
		
        long cardID = long.Parse(go.name);
		//更新PvP数据
		bool inTeam = false;
		int oldLeaderIndex = -99;
		int firstEmptyIndex = -99;
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
		{
			//在阵型中把旧的队长删掉
			if (Obj_MyselfPlayer.GetMe().curPvPLearder > 0 && Obj_MyselfPlayer.GetMe().PvPBattleArray[i] == Obj_MyselfPlayer.GetMe().curPvPLearder)
			{
				oldLeaderIndex = i;
				Obj_MyselfPlayer.GetMe().PvPBattleArray[i] = -1;
			}
			if (Obj_MyselfPlayer.GetMe().PvPBattleArray[i] == cardID)
				inTeam = true;
			if (firstEmptyIndex == -99 && Obj_MyselfPlayer.GetMe().PvPBattleArray[i] == -1)
				firstEmptyIndex = i;
		}
		if (oldLeaderIndex == -99) //无队长. 设置新队长为第一个空位
		{
			oldLeaderIndex = firstEmptyIndex;
		}
		foreach(UserCardItem item in Obj_MyselfPlayer.GetMe().cardBagList)
		{
			if (item.cardID == Obj_MyselfPlayer.GetMe().curPvPLearder)
				item.qxzbFightIndex = -1;
			if (item.cardID == cardID && inTeam) //如果之前在阵容中不修改其所在位置
			{
				item.qxzbFightIndex = item.qxzbFightIndex%100;
			}
			else if (item.cardID == cardID && !inTeam)
			{
				item.qxzbFightIndex = 0 + oldLeaderIndex + item.quality*10;
				Obj_MyselfPlayer.GetMe().PvPBattleArray[oldLeaderIndex] = cardID; //之前不在阵容中,修改为之前的Leader的位置
			}
		}
			
		Obj_MyselfPlayer.GetMe().curPvPLearder = cardID;
		mainUILogic.OnPVPTeamWindow();
    }
	
	public void SelectMember(GameObject go)
    {
        bool isChecked = go.transform.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked;
        if (isChecked)
            DontSelect(long.Parse(go.name));
        else
            SelectMem(long.Parse(go.name));
    }

	void DontSelect(long cardID)
    {
        //卡牌下阵
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().PvPBattleArray[i] == cardID)
            {
                Obj_MyselfPlayer.GetMe().PvPBattleArray[i] = -1;
				UserCardItem item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cardID);
				if (item != null)
					item.qxzbFightIndex = -1;
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
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().PvPBattleArray[i] == -1)
            {
                Obj_MyselfPlayer.GetMe().PvPBattleArray[i] = cardID;
				UserCardItem item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cardID);
				if (item != null)
					item.qxzbFightIndex = item.quality * 10 + i + 100;
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
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().PvPBattleArray[i] != -1)
                memberNum++;
        }
		memberNum--;
        if (memberNum >= 4)
        {
            for (int i = 0; i < count; i++)
            {
                Transform item = grid.transform.GetChild(i);
                item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = false;
                item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = false;
                item.transform.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 0.5f;
                UIEventListener.Get(item.gameObject).onClick -= SelectMember;
				//获取现在列表里的当前项领导力
                int leaderShipValue = 0;
                for (int j = 0; j < cardList.Count; j++)
                {
                    if (cardList[j].cardID == long.Parse(item.gameObject.name))
                    {
                        leaderShipValue = cardList[j].GetLeaderShip();
                        break;
                    }
                }
                item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValue + "[000000]";
				for (int j = 0; j < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; j++)
	            {
					if (long.Parse(item.name) == Obj_MyselfPlayer.GetMe().PvPBattleArray[j])
	                {
	                    item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = true;
	                    item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
	                    item.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
	                    UIEventListener.Get(item.gameObject).onClick += SelectMember;
	                    item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValue + "[000000]";
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
				
				//计算领导力 判断能否上阵
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
                    item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
                    item.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
                    UIEventListener.Get(item.gameObject).onClick += SelectMember;
                    item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValue + "[000000]";
                }
                else
                {
                    item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = false;
                    item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = false;
                    item.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 0.5f;
                    item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[FF231A]" + leaderShipValue + "[000000]";
                }
                for (int j = 0; j < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; j++)
                {
                    if (long.Parse(item.name) == Obj_MyselfPlayer.GetMe().PvPBattleArray[j])
                    {
                        item.FindChild("Checkbox").GetComponent<UICheckbox>().isChecked = true;
                        item.FindChild("BG").GetComponent<UIImageButton>().isEnabled = true;
                        item.FindChild("Sprites/Sprite-name-bg").GetComponent<UISprite>().alpha = 1;
                        UIEventListener.Get(item.gameObject).onClick -= SelectMember;
                        UIEventListener.Get(item.gameObject).onClick += SelectMember;
                        item.transform.FindChild("Labels/Label-Leadership-Value").GetComponent<UILabel>().text = "[F1ECCF]" + leaderShipValue + "[000000]";
                        break;
                    }
                }
            }
        }
        //队长不可选
        Transform leader = grid.transform.GetChild(0);
        if (Obj_MyselfPlayer.GetMe().PvPBattleArray.Length > 0)
        {
            if (Obj_MyselfPlayer.GetMe().curPvPLearder == long.Parse(leader.gameObject.name))
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
//        leaderShipShow.text = nowLeaderShip + "/" + maxLeaderShip;
		if (!isSelectLeader)
		{
			leaderShipShow.text = nowLeaderShip + "/" + maxLeaderShip;
			heroCount.text = memberNum.ToString();
		}
    }

	void BackBtnPress()
	{
		long leaderCardID = Obj_MyselfPlayer.GetMe().curPvPLearder;
		if (isSelectLeader && (leaderCardID <= 0 || !Obj_MyselfPlayer.GetMe().IsCardInBagByID(leaderCardID))) //无队长或者队长卡不存在
		{
			mainUILogic.OnQxzbPvPWindow();
			return;
		}
		//对选择队员/队长信息的更新操作
		
		if (!isSelectLeader) //选择队员时,点返回按钮要恢复之前的阵容,不保存修改
		{
			//还原队伍
			int i = 0;
	        for (; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
	        {
				if(Obj_MyselfPlayer.GetMe().PvPBattleArray[i] != Obj_MyselfPlayer.GetMe().curPvPLearder)
				{
					DontSelect(Obj_MyselfPlayer.GetMe().PvPBattleArray[i]);
				}
	        }
			i = 0;
	        for (; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
	        {
				if(Obj_MyselfPlayer.GetMe().PvPBattleArray[i] != Obj_MyselfPlayer.GetMe().curPvPLearder)
				{
					SelectMem(TempBattleArray[i]);
				}
	        }
			i = 0;
			for (; i < Obj_MyselfPlayer.GetMe().PvPBattleArray.Length; i++)
			{
				Obj_MyselfPlayer.GetMe().PvPBattleArray[i] = TempBattleArray[i];
				if (TempBattleArray[i] > 0)
				{
					UserCardItem item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(TempBattleArray[i]);
					if (item != null)
						item.qxzbFightIndex = item.qxzbFightIndex / 10 * 10 + i;
				}
			}
			
		}
		mainController.showBottomBar();
		mainUILogic.OnPVPTeamWindow();
	}
	
	//选择队员中,点击确定按钮
	void ConfirmBtnPress()
	{
		long leaderCardID = Obj_MyselfPlayer.GetMe().curPvPLearder;
		if (isSelectLeader && (leaderCardID <= 0 || !Obj_MyselfPlayer.GetMe().IsCardInBagByID(leaderCardID))) //无队长或者队长卡不存在
		{
			mainUILogic.OnPVPBattleBeforeController();
			return;
		}
		//对选择队员/队长信息的更新操作
		mainController.showBottomBar();
		mainUILogic.OnPVPTeamWindow();
	}
	
}
