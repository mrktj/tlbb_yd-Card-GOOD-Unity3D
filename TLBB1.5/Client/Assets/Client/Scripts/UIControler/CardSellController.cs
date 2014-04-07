using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

using card.net;

public class CardSellController : MonoBehaviour {
	public GameObject mainLogic;
//	public UISprite iconSmall;
//	public UILabel cardName;
//	public UILabel cardLevel;
//	public UILabel cardHP;
//	public UILabel cardAttackValue;
//	public UISprite cardStar;
	
//	public GameObject cardPrefab;
	public GameObject listParent;
	
	public UILabel selectedNum;
	public UILabel selectNumMoney;

    public GameObject ComfireBtn;


    public List<UserCardItem> selectedCard;  //选择要售卖的卡牌
	public List<UserCardItem> myCards;
	private List<GameObject> items = new List<GameObject>();
	public UIScrollBar scrollBar;
	public UIDraggablePanel dragPanel;
	
    //Transform 的缓存
    private Transform cachedTransform;

//	card guid for key
	public Dictionary<long,UserCardItem> sellCards = new Dictionary<long,UserCardItem>();
	
	// Use this for initialization
	void Awake()
	{
		mainLogic = GameObject.Find("MainUILogic");

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
	}
	
	void Start () {
		
	}
	void OnDisable()
	{
		sellCards.Clear();
		resetItems();

        selectedNum.text = "0";
        selectNumMoney.text = "0";
        selectedCard.Clear();
	}
	
	void OnEnable()
	{
        if (selectedCard == null)
        {
            selectedCard = new List<UserCardItem>();
        }

        if (mainLogic != null)
        {
            mainLogic.GetComponent<MainUILogic>().SetMainUIBottomBarActive(false);
        }

        //开始分页显示功能
        if (cachedTransform != null)
        {
            BagItemsPage bagItemPg  = cachedTransform.GetComponent<BagItemsPage>();
			if(bagItemPg != null)
			{
				bagItemPg.StartBagItemPages();
			}
        }

        this.FreshButton();
		
		selectedNum.text = "0";
 		selectNumMoney.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		//FreashScroll();
	}
	
	
	private void FreashScroll(){
		
		if(scrollBar == null || dragPanel == null)
		{
			return;
		}
		
		if(scrollBar.scrollValue == 0 || scrollBar.scrollValue == 1){
			dragPanel.scale.y = 0.1f;
		}
		else{
			dragPanel.scale.y = 1;
		}
	}


    //卡牌出售比较函数
    static public int SellCompareTo(UserCardItem cardA, UserCardItem cardB)
    {
		if(cardA == null || cardB == null)
		{
			return 0;
		}
		
        if (cardB.quality != cardA.quality)
        {
            return (-1 * cardB.quality.CompareTo(cardA.quality));
        }
        else if (cardB.templateID != cardA.templateID)
        {
            return (-1 * cardB.templateID.CompareTo(cardA.templateID));
        }
        else if (cardB.level != cardA.level)
        {
            return (-1 * cardB.level.CompareTo(cardA.level));
        }
		else
		{
			return (-1 * cardB.cardID.CompareTo(cardA.cardID));
		}

    }


     //计算要显示
    public List<UserCardItem> CalculateShowItems()
    {

        List<UserCardItem> cardList = Obj_MyselfPlayer.GetMe().cardBagList;

		
		List<UserCardItem> sellList = new List<UserCardItem>();	
		foreach(UserCardItem card in cardList)
		{
			if (card.IsInFightArray() || card.isProtected)
	        {
	            continue;
	        }
			
			sellList.Add(card);
		}
		
		
		List<UserCardItem> expCards = new List<UserCardItem>();
		List<UserCardItem> evolutionCards = new List<UserCardItem>();	
		List<UserCardItem> NormalCards = new List<UserCardItem>();	
		foreach(var card in sellList)
		{
			if(card.cardType == UserCardItem.CardType.EXP_CARD)
			{
				expCards.Add(card);
			}
			else if(card.cardType == UserCardItem.CardType.EVOLUTION_CARD)
			{
				evolutionCards.Add(card);
			}
			else
			{
				NormalCards.Add(card);
			}
		}
		
        //按星级，等级 id排序
        NormalCards.Sort(SellCompareTo);
		expCards.Sort(SellCompareTo);
		evolutionCards.Sort(SellCompareTo);
		
		NormalCards.AddRange(expCards);
		NormalCards.AddRange(evolutionCards);
		
		
        return NormalCards;
    }


    public void ShowCardItem(UserCardItem card)
    {
        if (card == null)
        {
            return;
        }
		
        GameObject newItem = CardListItemPool.Instance.GetListItem("CardSellItem");//(GameObject)Instantiate(cardPrefab);
        if(newItem == null)
		{
			return;
		}
		
		newItem.transform.parent = listParent.transform;
        newItem.transform.localPosition = new Vector3(0, 0, -1);
        newItem.transform.localScale = new Vector3(1, 1, 1);
        newItem.name = card.cardID.ToString();
        items.Add(newItem);
        //			UIEventListener.Get(newItem).onClick +=
		
		Tab_Card tabCard =  TableManager.GetCardByID(card.templateID);
		
		Transform labNameTrans = newItem.transform.FindChild("Labels/Label-Name");
		if(labNameTrans != null
			   && tabCard != null)
		{
			Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
			if(tabAppearance != null)
			{
				UILabel nameLabel = labNameTrans.GetComponent<UILabel>();
				if(nameLabel != null)
				{
					int nameLangId = tabAppearance.Name;
	       			nameLabel.text = LanguageManger.GetWords(nameLangId);
				}
			}
			
		}
		
			
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		Transform tranSpBg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG");
		if(tranSpBg != null
			 && tabCard != null)
		{
			UISprite icon_bg = tranSpBg.GetComponent<UISprite>();
			if(icon_bg != null)
			{
				icon_bg.spriteName = UserCardItem.littleCardFrameName[tabCard.Star];
			}
		}
		
		Transform tranSp_Outside = newItem.transform.FindChild("CardIcon/CardIconBtn/sp-Outside");
		if(tranSp_Outside != null
			&& tabCard != null)
		{
			UISprite icon_border = tranSp_Outside.GetComponent<UISprite>();
			icon_border.spriteName = UserCardItem.littleCardBorderName[tabCard.Star];
		}
		//--------------------------------------------------------------------
        
		Transform transLabel_lev_value = newItem.transform.FindChild("Labels/Label-Level-Value");
		if(transLabel_lev_value != null)
		{
			UILabel cardLevelLabel = transLabel_lev_value.GetComponent<UILabel>();
			if(cardLevelLabel != null)
			{
				cardLevelLabel.text = card.level.ToString();
			}
		}
        //UILabel cardLevelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
        
		
		Transform tranLab_Hp_Val = newItem.transform.FindChild("Labels/Label-Hp-Value");
		if(tranLab_Hp_Val != null)
		{
			UILabel cardHPLabel = tranLab_Hp_Val.GetComponent<UILabel>();
			if(cardHPLabel != null)
			{
				cardHPLabel.text = card.GetHp().ToString();
			}
		}
		
		Transform tranLab_Att_Val = newItem.transform.FindChild("Labels/Label-Attack-Value");
		if(tranLab_Att_Val != null)
		{
			UILabel cardAttackValueLabel = tranLab_Att_Val.GetComponent<UILabel>();
			if(cardAttackValueLabel != null)
			{
				cardAttackValueLabel.text = card.GetAttack().ToString();
			}
		}
       
        Transform tranLab_Mony_Val = newItem.transform.FindChild("Labels/Label-Money-Value");
		if(tranLab_Mony_Val != null)
		{
			UILabel cardMoneyValueLabel = tranLab_Mony_Val.GetComponent<UILabel>();
			if(cardMoneyValueLabel != null)
			{
				cardMoneyValueLabel.text = card.GetMoneyValue().ToString();
			}
		}

        
        Transform tranSp_Icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon");
		if(tranSp_Icon != null)
		{
			 UISprite icon = tranSp_Icon.GetComponent<UISprite>();
			 Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
			 if( icon != null 
				  && tabAppearance != null)
			 {
				 icon.spriteName = tabAppearance.HeadIcon;
			 }
		}

		
        //星级显示
        Transform transformStars = newItem.transform.FindChild("Stars");
		if(transformStars != null)
		{
			for (int j = 1; j <= 7; j++)
	        {
	            if (j <= card.quality)
	            {
					Transform tranStar = transformStars.FindChild("star_" + j);
					if(tranStar != null)
					{
	                	tranStar.gameObject.SetActive(true);
					}
	               
	            }
	            else
	            {
					Transform tranStar = transformStars.FindChild("star_" + j);
					if(tranStar != null)
					{
	                	tranStar.gameObject.SetActive(false);
					}
	            }
	
	        }

		}
        
		
		Transform tranSp_Selected = newItem.transform.FindChild("Sprites/Sprite-Selected");
		if(tranSp_Selected != null)
		{
			 UISprite selectSprite = tranSp_Selected.GetComponent<UISprite>();
	        //设置显示状态
	        if (CheckIfChooseItem(card))
	        {
	            if (selectSprite != null)
	            {
	                selectSprite.gameObject.SetActive(true);
	            }
	        }
	        else
	        {
	            if (selectSprite != null)
	            {
	                selectSprite.gameObject.SetActive(false);
	            }
	        }
		}
       


        //五行图标显示
		Transform tranSpAttribut = newItem.transform.FindChild("Sprite-Attribute");
		if(tranSpAttribut != null)
		{
			UISprite sttributeIcon = tranSpAttribut.GetComponent<UISprite>();
			if(sttributeIcon != null)
			{
				sttributeIcon.spriteName = card.GetAttributeIconName();
			}
		}
       
        

//         UISprite selectSprite = newItem.transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
//         selectSprite.gameObject.SetActive(false);

        UIEventListener.Get(newItem).onClick += OnSelectItem;
		Transform tranCardIconBtn = newItem.transform.FindChild("CardIcon/CardIconBtn");
		if(tranCardIconBtn != null)
		{
			GameObject CardIconBtn = tranCardIconBtn.gameObject;
       	    UIEventListener.Get(CardIconBtn).onClick += ShowCardInfo;
		}
        
		

    }

    private bool CheckIfChooseItem(UserCardItem userCard)
    {
        if (userCard == null)
        {
            return false;
        }

        foreach (UserCardItem card in selectedCard)
        {
            if (card.cardID == userCard.cardID)
            {
                return true;
            }
        }

        return false;
    }

    //拖拽完的调用函数
    public void OnDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode dragonMode)
    {
        //清理上一页的Items
        this.resetItems();
    }

	public void resetItems()
	{
		foreach(GameObject item in items)
		{
			if(item != null)
			{
				//item.transform.parent = null;
				//Destroy(item);	
				CardListItemPool.Instance.DestroyItemAndPushToPool(item, "CardSellItem");
			}

		}
		items.Clear();
// 		selectedNum.text = "0";
// 		selectNumMoney.text = "0";
		sellCards.Clear();
        //selectedCard.Clear();
	}

// 	public void showItems()
// 	{
// 		if(myCards == null)
// 		{
// 			myCards = new List<UserCardItem>();
// 		}
// 		myCards.Clear();
// 		//add item without battle and protected card
// 
//         List<UserCardItem> cardList = Obj_MyselfPlayer.GetMe().cardBagList;
// 
//         //按星级，等级 id排序
//         cardList.Sort(SellCompareTo);
// 
//         foreach (UserCardItem card in cardList)
// 		{
// 
//             if (card.IsInFightArray() || card.isProtected)
//             {
//                 continue;
//             }
// 
// 			GameObject newItem = ResourceManager.Instance.loadWidget("CardSellItem");//(GameObject)Instantiate(cardPrefab);
// 			newItem.transform.parent = listParent.transform;
// 			newItem.transform.localPosition = new Vector3(0, 0, -1);
// 			newItem.transform.localScale = new Vector3(1, 1, 1);
// 			newItem.name = card.cardID.ToString();
// 			
// 
// 
// 			items.Add(newItem);
// 
// 			
// 			UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
// 			int nameLangId = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).Name;
//             		nameLabel.text = LanguageManger.GetWords(nameLangId);
// 			
// 			UILabel cardLevelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
//           		cardLevelLabel.text = card.level.ToString();	
// 			
// 			UILabel cardHPLabel = newItem.transform.FindChild("Labels/Label-Hp-Value").GetComponent<UILabel>();
//             		cardHPLabel.text = card.GetHp().ToString();
// 			
// 			UILabel cardAttackValueLabel = newItem.transform.FindChild("Labels/Label-Attack-Value").GetComponent<UILabel>();
//            		cardAttackValueLabel.text = card.GetAttack().ToString();
// 
//             UILabel cardMoneyValueLabel = newItem.transform.FindChild("Labels/Label-Money-Value").GetComponent<UILabel>();
//             cardMoneyValueLabel.text = card.GetMoneyValue().ToString();
// 			
// 			UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
// 			icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).HeadIcon;
// 
// 
//             //星级显示
//             Transform transformStars = newItem.transform.FindChild("Stars");
//             for (int j = 1; j <= 7; j++)
//             {
//                 if (j <= card.quality)
//                 {
//                     GameObject starIcon = transformStars.FindChild("star_" + j).gameObject;
//                     starIcon.SetActive(true);
//                 }
//                 else
//                 {
//                     GameObject starIcon = transformStars.FindChild("star_" + j).gameObject;
//                     starIcon.SetActive(false);
//                 }
// 
//             }
// 
// 
//             //五行图标显示
//             UISprite sttributeIcon = newItem.transform.FindChild("Sprite-Attribute").GetComponent<UISprite>();
//             sttributeIcon.spriteName = card.GetAttributeIconName();
// 			
// 			UISprite selectSprite = newItem.transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
// 			selectSprite.gameObject.SetActive(false);
// 
//             UIEventListener.Get(newItem).onClick += OnSelectItem;
//             GameObject CardIconBtn = newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject;
//             UIEventListener.Get(CardIconBtn).onClick += ShowCardInfo;
// 			
// 
// 		}
// 
// 	}


    public void ShowCardInfo(GameObject selectedItem)
    {
		if(selectedItem == null)
		{
			return;
		}
		
        Obj_MyselfPlayer.GetMe().selectedCardID = long.Parse(selectedItem.transform.parent.parent.name);
        BoxManager.showCardInfoMessage((int)Obj_MyselfPlayer.GetMe().selectedCardID, -1);
        //mainLogic.SendMessage("LoadCardInfoUI");
    }
	
	public bool CheckSellCardArrayContainQxzbCard()
	{
		foreach(UserCardItem card in selectedCard)
		{
			if(card != null 
				  && card.IsInQxzbFightArray())
			{
				return true;
			}
		}
		
		return false;
	}


	//show box message
	public void showConfirmBox()
	{
        if (selectedCard.Count > 0)
		{
			bool bContaingHighStarCard = false;
			foreach(var card in selectedCard)
			{
				if(card.quality >= 6)
				{
					bContaingHighStarCard = true;
					break;
				}
			}
			MessageIdEnum msg = MessageIdEnum.Msg71;
//            string strDescri = "共出售 "+ selectedNum.text + " 个侠士，可获得 " + selectNumMoney.text + " 金币\n";
			
			if(CheckSellCardArrayContainQxzbCard())
			{
				msg = MessageIdEnum.Msg194;
			}
			else if(bContaingHighStarCard)
				msg = MessageIdEnum.Msg108;
			
//            BoxManager.showConfirmMessage(strDescri);
			BoxManager.showMessageByID((int)msg,selectedNum.text,selectNumMoney.text);
			UIEventListener.Get(BoxManager.getYesButton()).onClick += sellCard;
            UIEventListener.Get(BoxManager.getNoButton()).onClick += CancelSellCard;
			
//			Messenger.AddListener<bool>("BOX",sellCard);			
		}else
		{
//            BoxManager.showMessage("未选择任何卡牌"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg100);
		}

	}

    public void CancelSellCard(GameObject button)
    {
        this.FreshButton();
    }

	public void sellCard(GameObject button)
	{
//		UIEventListener.Get(BoxManager.getYesButton()).onClick -= sellCard;
		/*
		bool bContaingHighStarCard = false;
		foreach(var card in selectedCard)
		{
			if(card.quality >= 6)
			{
				bContaingHighStarCard = true;
				break;
			}
		}
		
		if(bContaingHighStarCard)
		{
			BoxManager.showConfirmMessage("选择出售的侠士中有6星以上侠士。\n是否继续？");
			UIEventListener.Get(BoxManager.getYesButton()).onClick += SendSellCardMessage;
            //UIEventListener.Get(BoxManager.getNoButton()).onClick += CancelUpdateCard;
			return;
		}
        */
		this.SendSellCardMessage(null);
	}
	
	private void SendSellCardMessage(GameObject btn)
	{
		NetworkSender.Instance().sellCard(sellCardDone, selectedCard);
	}

	//sell card done
	public void sellCardDone(bool isSuccess)
	{
        AudioManager.Instance.PlayEffectSound("card_sell");
		mainLogic.SendMessage("refreshTopBar");
        BagItemsPage bagItempage = cachedTransform.GetComponent<BagItemsPage>();

        if (bagItempage != null)
        {
            bagItempage.Reset();
        }
		
		Transform listTrans = listParent.transform.parent.transform;
		listTrans.localPosition = new Vector3(-5, listTrans.localPosition.y, listTrans.localPosition.z);
		//刷新文本信息
		selectedCard.Clear();
		this.updateSellNumAndMoney();
        //刷新列表
        this.FreshButton();
	}

    //刷新确定出售按钮
    public void FreshButton()
    {
        MonoBehaviour[] MonoBes;
        MonoBes = ComfireBtn.GetComponents<MonoBehaviour>();

        if (selectedCard.Count > 0)
        {
            ComfireBtn.GetComponent<UIImageButton>().isEnabled = true;
            ComfireBtn.transform.FindChild("Sprite").GetComponent<UISprite>().color = Color.white;
//             ComfireBtn.transform.FindChild("Background").GetComponent<UISprite>().color = Color.white;
        }
        else
        {
            ComfireBtn.GetComponent<UIImageButton>().isEnabled = false;
			ComfireBtn.transform.FindChild("Sprite").GetComponent<UISprite>().color = Color.grey;
//             ComfireBtn.transform.FindChild("Label").GetComponent<UILabel>().color = Color.grey;
//             ComfireBtn.transform.FindChild("Background").GetComponent<UISprite>().color = Color.grey;
        }
    }
	
	//点击选择的响应函数
     public void OnSelectItem(GameObject go)
	{
        UserCardItem item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(long.Parse(go.name));
        
		UISprite selectSprite = go.transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
        bool bCurChooseState = false;
        if (selectSprite != null)
        {
            bCurChooseState = !selectSprite.active;  //表示当前的选择状态
            selectSprite.gameObject.SetActive(bCurChooseState);
        }
        

        if (selectedCard != null)
        {
            //当前为取消 选择状态 
            if (selectedCard.Contains(item) && !bCurChooseState)
            {
                selectedCard.Remove(item);
            }//当前为  选择状态
            else if (!selectedCard.Contains(item) && bCurChooseState)
            {
                selectedCard.Add(item);
            }
        }

        //更新选择贩卖卡牌的数量 和 总价
        this.updateSellNumAndMoney();

        this.FreshButton();
	}

// 	public void checkboxActive(GameObject item)
// 	{
// 		long guid= long.Parse(item.transform.parent.name);
// 		foreach(UserCardItem card in Obj_MyselfPlayer.GetMe().cardBagList)
// 		{
// 			if(card.cardID == guid)
// 			{
// 				sellCards.Add(guid,card);
// 				break;;
// 			}
// 		}
// 		updateSellNumAndMoney();
// 	}
// 	public void checkboxInactive(GameObject item)
// 	{
// 		long guid= long.Parse(item.transform.parent.name);
// 		foreach(UserCardItem card in Obj_MyselfPlayer.GetMe().cardBagList)
// 		{
// 			if(card.cardID == guid)
// 			{
// 				sellCards.Remove(guid);
// 				break;
// 			}
// 			
// 		}
// 		updateSellNumAndMoney();
// 	}
	
	
	//更新选择贩卖卡牌的数量 和 总价
	public void updateSellNumAndMoney()
	{
        int nTotalMoney = 0;
        foreach (UserCardItem card in selectedCard)
		{
            nTotalMoney += card.GetMoneyValue();
		}

        selectedNum.text = selectedCard.Count.ToString();
        selectNumMoney.text = nTotalMoney.ToString();
	}
	
	public void backToPreviousWindow()
	{
        GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().SetMainUIBottomBarActive(true);
        mainLogic.SendMessage("onHeroWindow");
	}
	
}
