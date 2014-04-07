using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using card;
using Games.LogicObject;
using GCGame.Table;
////using Module.Log;

public class HeroesController : MonoBehaviour {
	public GameObject listParent;

    public UILabel cardNumLabel;
    private string heroes_Key = "Heroes_Key";
//     //----拖拽界面
//     public GameObject goDragPanel;  
// 
//     //物品的最大显示数量
//     public int nItemsMaxNum = 20;
//     //当前页面
//     private int nCurPage = 1;
//     //最大页
//     private int nMaxPage = 0;
// 
//     //记录是不是第一次进入
//     private bool bFirst = true;

    
    private string listItem = "HeroTableItem";
	//private string listItem = "tttt";
	private GameObject logicTarget;
	
	private List<UserCardItem> cardList;
	private List<GameObject> items = new List<GameObject>();

    //Transform 的缓存
    private Transform cachedTransform;
	public UIScrollBar scrollBar;
	public UIDraggablePanel dragPanel;
    private int currentPageCount = 0;
	public static GameObject curChooseItem = null;
	
	
    void Awake()
    {
        logicTarget = GameObject.Find("/MainUILogic");

      	cachedTransform = transform;

        //设置 items的获得函数
        BagItemsPage bagItempage = cachedTransform.GetComponent<BagItemsPage>();
        if (bagItempage != null)
        {
            bagItempage.onCalculateShowItemsFunction += CalculateShowItems;
            bagItempage.onDragonFinishedClearItemsFun += OnDragonFinishedClearItemsFun;
            bagItempage.onShowCardItemFunction += ShowCardItem;
			
			bagItempage.onCalculateShowFriendsItemsFunction = null;
        }
		

        if (logicTarget == null)
        {
            Debug.Log("can not find");
        }
    }

	void Start()
	{
//         if (goDragPanel != null)
//         {
//             goDragPanel.GetComponent<UIDraggablePanel>().onDragFinished += OnDragonFinished;
//         }
       
	}
	void OnDisable()
	{
		BagItemsPage bagItemPg = transform.GetComponent<BagItemsPage>();
		if(bagItemPg != null)
		{
			int currentPage = bagItemPg.GetCurrentPage();
       		ScrollData scData = new ScrollData(scrollBar.scrollValue, currentPage);
       	 	Obj_MyselfPlayer.GetMe().SetScrollValue(heroes_Key, scData);
		}
       
		resetItems();
	}
	
	void OnEnable()
	{
        if (logicTarget != null)
        {
            logicTarget.GetComponent<MainUILogic>().SetMainUIBottomBarActive(true);
        }

		//this.ShowBagInfo();
		
        //开始分页显示功能
       if (cachedTransform != null)
       {
           currentPageCount = 0;
           FreshBar();
       }
		
	}
	
	// Update is called once per frame
	void Update () {
		//FreashScroll();
	}
	
	
	private void FreashScroll(){	
		if(scrollBar.scrollValue == 0 || scrollBar.scrollValue == 1){
			dragPanel.scale.y = 0.1f;
		}
		else{
			dragPanel.scale.y = 1;
		}
	}

    public void FreshBar()
    {
        if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(heroes_Key))
        {
            List<UserCardItem> showList = CalculateShowItems();
            if (showList != null)
            {
                if ((showList.Count / 20) + 1 >= Obj_MyselfPlayer.GetMe().scrollRecord[heroes_Key].page)
                {
                    cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages(Obj_MyselfPlayer.GetMe().scrollRecord[heroes_Key].page);
                }
                else
                {
                    cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages((showList.Count / 20) + 1);
                }
                if (currentPageCount > 4)
                {
                    scrollBar.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[heroes_Key].scrollValue;
                }
                else
                {
                    scrollBar.scrollValue = 0;
                }
            }
        }
        else
        {
            cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages(1);
            scrollBar.scrollValue = 0;
        }
    }
	
// 	private void ShowBagInfo()
// 	{
// 		cardNumLabel.text = Obj_MyselfPlayer.GetMe().cardBagList.Count + "/" + Obj_MyselfPlayer.GetMe().bagMax;
// 	}
	

    //清理Items
	public void resetItems()
	{
		foreach(GameObject item in items)
		{
            if (item != null)
            {
                //item.transform.parent = null;
                //Destroy(item);
				CardListItemPool.Instance.DestroyItemAndPushToPool(item, listItem);
            }
		}
		items.Clear();
	}


    //计算要显示
    public List<UserCardItem>  CalculateShowItems()
    {
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;	
		List<UserCardItem> ShowList = new List<UserCardItem>();
		
		
        //先把上阵和不上阵的分开来，再排序
        List<UserCardItem> listInFightArray = new List<UserCardItem>();
		List<UserCardItem> ListNoUpdateAndEvolution = new List<UserCardItem>();
		List<UserCardItem> listUpdateMaterials = new List<UserCardItem>();
		List<UserCardItem> listEvolutionMaterials = new List<UserCardItem>();

        //先找队长和队员
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            foreach (var card in cardList)
            {
                if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == card.cardID)
                {
                    listInFightArray.Add(card);
                    break;
                }
            }
        }
		
		
        //再找其他卡牌
        foreach (var card in cardList)
        {
            if (!card.IsInFightArray())
            {
				if(card.cardType == UserCardItem.CardType.EXP_CARD)
				{
					listUpdateMaterials.Add(card);
				}
				else if(card.cardType == UserCardItem.CardType.EVOLUTION_CARD)
				{
					listEvolutionMaterials.Add(card);
				}
				else
				{
					ListNoUpdateAndEvolution.Add(card);
				}
            }
        }


        //分别按照排序规则排序：先星级，再等级，再ID
		ListNoUpdateAndEvolution.Sort(CompareTo);
		listUpdateMaterials.Sort(CompareTo);
		listEvolutionMaterials.Sort(CompareTo);
		
        //合并分别排过序的卡牌
        listInFightArray.AddRange(ListNoUpdateAndEvolution);
		listInFightArray.AddRange(listUpdateMaterials);
		listInFightArray.AddRange(listEvolutionMaterials);
        ShowList = listInFightArray;
		
		
		/*
        //先把上阵和不上阵的分开来，再排序
        List<UserCardItem> listInFightArray = new List<UserCardItem>();
        List<UserCardItem> listNoInFightArray = new List<UserCardItem>();

        //先找队长和队员
        for(int i=0;i<Obj_MyselfPlayer.GetMe().teamMemberArray.Length;i++)
		{
            foreach(var card in cardList)
            {
                if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == card.cardID)
			    {
                    listInFightArray.Add(card);
                    break;
			    }
            }
		}



        //再找其他卡牌
        foreach (var card in cardList)
        {
            if (!card.IsInFightArray())
            {
                listNoInFightArray.Add(card);
            }
        }

        //分别按照排序规则排序：先星级，再等级，再ID
        listNoInFightArray.Sort(CompareTo);
        //合并分别排过序的卡牌
        listInFightArray.AddRange(listNoInFightArray);
		
		 */
        return ShowList;
    }

    //排序算法
    static public int CompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        //排序顺序按照 先星级 再等级，最后ID的方式进行
        if (cardB.quality != cardA.quality)
        {
            return cardB.quality.CompareTo(cardA.quality);
        }
        else if (cardB.level != cardA.level)
        {
            return cardB.level.CompareTo(cardA.level);
        }
        else
        {
            return (-1 * cardB.templateID.CompareTo(cardA.templateID));
        }

    }


    public void ShowCardItem(UserCardItem card)
    {
        if (card == null)
        {
            return;
        }

        //GameObject newItem = ResourceManager.Instance.loadWidget(listItem);//(GameObject)Instantiate(listItem);
		GameObject newItem = CardListItemPool.Instance.GetListItem(listItem);
        newItem.transform.parent = listParent.transform;
		newItem.transform.localPosition = new Vector3(0, 0, -2);
        newItem.transform.localScale = new Vector3(1, 1, 1);
        newItem.name = card.cardID.ToString();
        items.Add(newItem);
        currentPageCount++;
        UIEventListener.Get(newItem).onClick += ShowCardInfo;
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		UISprite icon_bg = newItem.transform.FindChild("CardIcon/Button-CardIcon/SpriteBG").GetComponent<UISprite>();
		UISprite icon_border = newItem.transform.FindChild("CardIcon/Button-CardIcon/Sprite").GetComponent<UISprite>();
		int icon_star = TableManager.GetCardByID(card.templateID).Star;
		icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
		icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
		//--------------------------------------------------------------------
        UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
        int nameLangId = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).Name;
        nameLabel.text = LanguageManger.GetWords(nameLangId);

        UILabel lifeLabel = newItem.transform.FindChild("Labels/Label-Hp-Value").GetComponent<UILabel>();
        lifeLabel.text = (card.GetHp()).ToString();

        UILabel attackLabel = newItem.transform.FindChild("Labels/Label-Attack-Value").GetComponent<UILabel>();
        attackLabel.text = (card.GetAttack()).ToString();

        UILabel levelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
        levelLabel.text = card.level.ToString();
		
		//UIPanel panel = newItem.GetComponent<UIPanel>();

         //让图标无响应
        //newItem.transform.FindChild("CardIcon/Button-CardIcon").GetComponent<BoxCollider>().enabled = false;
       // GameObject CardIconBtn = newItem.transform.FindChild("CardIcon/Button-CardIcon").gameObject;
        UIEventListener.Get(newItem).onClick += ShowCardInfo;

        //星级显示
        Transform transformStars = newItem.transform.FindChild("Stars");
        for (int j = 1; j <= 7; j++)
        {
            if (j <= card.quality)
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

        //五行图标显示
        UISprite sttributeIcon = newItem.transform.FindChild("Sprite-Attribute").GetComponent<UISprite>();
        sttributeIcon.spriteName = card.GetAttributeIconName();

        UISprite icon = newItem.transform.FindChild("CardIcon/Button-CardIcon/Sprite-Icon").GetComponent<UISprite>();
        icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).HeadIcon;

        UILabel protectedLabel = newItem.transform.FindChild("Labels/Label-Is-Protected").GetComponent<UILabel>();
        bool isMember = false;

        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == card.cardID)
            {
                isMember = true;
            }
        }

        //判断卡牌是否被保护
        if (card.isProtected)
        {
            newItem.transform.FindChild("Sprites/sp-suo").gameObject.SetActive(true);
        }
        else
        {
            newItem.transform.FindChild("Sprites/sp-suo").gameObject.SetActive(false);
        }

        //显示是否上场
        if (isMember)
        {
            protectedLabel.text = "上场";
        }
        else
        {
            protectedLabel.text = "";
        }

    }

    public void ShowCardInfo(GameObject selectedItem)
    {
       Obj_MyselfPlayer.GetMe().selectedCardID = long.Parse(selectedItem.transform.name);
	   curChooseItem = selectedItem;
	   CardInfoController.heroControl = gameObject;
       logicTarget.SendMessage("LoadCardInfoUI");
    }

    //拖拽完的调用函数
    public void OnDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode dragonMode)
    {
        //清理上一页的Items
        this.resetItems();
    }
	
	//改变保护状态显示
	public void ChangeProtectedState()
	{
		UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(Obj_MyselfPlayer.GetMe().selectedCardID);	
		if (card.isProtected)
        {
            curChooseItem.transform.FindChild("Sprites/sp-suo").gameObject.SetActive(true);
        }
        else
        {
            curChooseItem.transform.FindChild("Sprites/sp-suo").gameObject.SetActive(false);
        }
		
	}

// 	public void ShowCardInfo(GameObject selectedItem)
// 	{
// 		Obj_MyselfPlayer.GetMe().selectedCardID = long.Parse(selectedItem.name);
// 		logicTarget.SendMessage("LoadCardInfoUI");
// 	}
// 	
	
	public void ReturnToMainUI()
	{
		logicTarget.SendMessage("ReturnToMainUI");
	}
	public void OnCardSellWindow()
	{
		logicTarget.SendMessage("OnCardSellWindow");
	}
}
