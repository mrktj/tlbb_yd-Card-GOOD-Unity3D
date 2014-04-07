using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using Module.Log;

public class SelectHeroController : MonoBehaviour {
	
	private string updateHeroItem = "CardUpdateItem";
	private string materialHeroItem = "MaterialHeroItem";
	private string evolutionHeroItem = "CardEvolutionItem";
	private string strengthenHeroItem = "CardStrengthenItem";
	public GameObject gridUpdate;
	public GameObject gridMaterial;
	public GameObject gridEvolution;
	public GameObject gridStrengthen;
	
	public  GameObject AutoFillBtn;
	public  GameObject backButton;
    private GameObject logicTarget;
	public  UIScrollBar scrollBar;
	//private GameObject updateController;
	//private GameObject evolutionController;
	//private GameObject strengthenController;
    private UserCardItem materialCard; //-----选择材料时为了提示，临时保存的
    private GameObject chooseItemObj; //-----选择材料时为了提示，临时保存的
	
	public GameObject noMaterialItem;
	public GameObject bottomBar;
	public UILabel costMoney;
	public UILabel totalExp;
	public GameObject gbTitle;
	public UIDraggablePanel dragPanel;
    private List<GameObject> items = new List<GameObject>();
	private string strCurListItemName;
	enum GRID_TYPE
	{
		UPDATE=0,
		MATERIAL,
		EVOLUTION,
		STRENGTHEN,
	}
	
	private List<UserCardItem> cardList;
	private const int maxHeroNum = 6;
	
	private UserCardItem updateHero;
	private List<UserCardItem> materialList = null;
	private UserCardItem evolutionHero;
	private UserCardItem strengthenHero;
	
	private GameObject curGrid;

    //Transform 的缓存
    private Transform cachedTransform;

    int iGuide = 0;

    //记录是否选择精进卡牌
    private bool bChooseCard = false;
	
	void Awake()
	{
        cachedTransform = transform;
	}
	
	// Use this for initialization
	void Start () {
		logicTarget = GameObject.Find("MainUILogic");
		//cant find
		//updateController = GameObject.Find("CardUpdateController");
		//evolutionController = GameObject.Find("CardEvolutionController");
		//strengthenController = GameObject.Find("CardStrengthenController");
		
		cardList = Obj_MyselfPlayer.GetMe().cardBagList;
		materialList = new List<UserCardItem>();
		
        items = new List<GameObject>();

		OnEnable();
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

    void OnDisable()
    {
        this.resetItems();
    }


/////////////////////////////////////////////////////////////////////////
       /***************************升级***************************/

    //计算升级要显示的Items
    public List<UserCardItem> CalculateUpdateShowItems()
    {
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
		List<UserCardItem> updateList = new List<UserCardItem>();
		
		iGuide = 0;
		foreach(var card in cardList)
		{
			//判断是否已经属于材料卡牌
	        bool bContainMeter = false;
	        foreach (UserCardItem meterCard in Obj_MyselfPlayer.GetMe().updateMaterialItems)
	        {
	            if (meterCard != null && meterCard.cardID == card.cardID)
	            {
	                bContainMeter = true;
	                break;
	            }
	        }
	
	        if (bContainMeter)
	        {
	            continue;
	        }
			
			updateList.Add(card);
		}
		
		
		
        //先把上阵和不上阵的分开来，再排序
        List<UserCardItem> listInFightArray = new List<UserCardItem>();
        //List<UserCardItem> listNoInFightArray = new List<UserCardItem>();
		List<UserCardItem> ListNoUpdateAndEvolution = new List<UserCardItem>();
		List<UserCardItem> listUpdateMaterials = new List<UserCardItem>();
		List<UserCardItem> listEvolutionMaterials = new List<UserCardItem>();

        //先找队长和队员
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            foreach (var card in updateList)
            {
                if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == card.cardID)
                {
                    listInFightArray.Add(card);
                    break;
                }
            }
        }
		
		
        //再找其他卡牌
        foreach (var card in updateList)
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
        //listNoInFightArray.Sort(UpdateCompareTo);
		ListNoUpdateAndEvolution.Sort(UpdateCompareTo);
		listUpdateMaterials.Sort(UpdateCompareTo);
		listEvolutionMaterials.Sort(UpdateCompareTo);
		
        //合并分别排过序的卡牌
        listInFightArray.AddRange(ListNoUpdateAndEvolution);
		listInFightArray.AddRange(listUpdateMaterials);
		listInFightArray.AddRange(listEvolutionMaterials);
        updateList = listInFightArray;

        return updateList;
    }


    //显示升级界面的Item
    public void ShowUpdateCardItem(UserCardItem card)
    {
        //GameObject newItem = ResourceManager.Instance.loadWidget(updateHeroItem);
		GameObject newItem = CardListItemPool.Instance.GetListItem(updateHeroItem);
        newItem.transform.parent = gridUpdate.transform;
		
		strCurListItemName = updateHeroItem;

        items.Add(newItem);
		
		bool success = newItem.GetComponent<UIUpdateItemView>().InitWithUserCardItem(card);
		if(success){
			UIEventListener.Get(newItem).onClick += OnSelectUpdateItem;

			GameObject CardIconBtn = newItem.GetComponent<UIUpdateItemView>().GetCardIconBtn();
			UIEventListener.Get(CardIconBtn).onClick += ShowCardInfo;
		}

        if (card == updateHero)
        {
            newItem.GetComponent<UIUpdateItemView>().ShowSelected();
        }
        if (iGuide == 0/*card.templateID == GuideManager.Instance.selectedTempletID*/)
        {
			iGuide = -1;
            this.guideCardItem = newItem;
			/*
			if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.CRAFT){
				GuideUpdate.Instance.NextStep();
			}*/
        }
    }

    //升级排序算法
    static public int UpdateCompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        //排序顺序按照 先星级 再等级，最后ID的方式进行
        if (cardB.quality != cardA.quality)
        {
            return cardB.quality.CompareTo(cardA.quality);
        }
		else if(cardB.templateID != cardA.templateID)
        {
            return (-1 * cardB.templateID.CompareTo(cardA.templateID));
        }
        else
        {
            return cardB.level.CompareTo(cardA.level);
        }
       

    }

        /************************升级************************/
/////////////////////////////////////////////////////////////////////////







/////////////////////////////////////////////////////////////////////////
       /************************升级材料*********************/
    
    //计算升级材料要显示的Items
    public List<UserCardItem> CalculateUpdateMaterialShowItems()
    {
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
		
		List<UserCardItem> materialCardTotal = null;
		
        //把话费的钱和升级的经验 置为0
        costMoney.text = "" + 0;
        totalExp.text = "" + 0;

        iGuide = 0;
        //按星级 ，等级 ，ID 排序
        List<UserCardItem> expCard = new List<UserCardItem>(); //----专门升级经验的卡牌，分出来
		List<UserCardItem> evolutionCard = new List<UserCardItem>(); //----专门升级经验的卡牌，分出来
        List<UserCardItem> NormalCards = new List<UserCardItem>();
		
        foreach (UserCardItem card in cardList)
        {
			if((card.cardType == UserCardItem.CardType.EXP_CARD)
				  && CheckCardIfCanBeMaterial(card))
			{
				expCard.Add(card);
			}
			else if((card.cardType == UserCardItem.CardType.EVOLUTION_CARD)
				     && CheckCardIfCanBeMaterial(card))
			{
				evolutionCard.Add(card);
			}
			else if(CheckCardIfCanBeMaterial(card))
			{
				NormalCards.Add(card);
			}
			
			
			/*
           //检查是否可以成为材料卡牌
			if(!CheckCardIfCanBeMaterial(card))
			{
				continue;
			}


            if (card.IsKindOfExpCard())
            {
                expCard.Add(card);
            }
            else
            {
                cards.Add(card);
            }
            */
        }

        //排序
        expCard.Sort(MaterialExpCardCompareTo);
		NormalCards.Sort(MaterialCompareTo);
        evolutionCard.Sort(MaterialCompareTo);
		
        expCard.AddRange(NormalCards);
		expCard.AddRange(evolutionCard);
		
		
        materialCardTotal = expCard;
        return materialCardTotal;
    }
	
	
	//判断卡牌是否能为材料卡牌
	private bool CheckCardIfCanBeMaterial(UserCardItem card)
	{
			//检查不要是升级的英雄
            if (card.cardID == updateHero.cardID)
            {
                return false;
            }
            //检查是不是team member
            bool is_team = false;
            for (int i = 0; i < 5; i++)
            {
                if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == card.cardID)
                {
                    is_team = true;
                    break;
                }
            }
            if (is_team)
            {
               return false;
            }
            //检查是不是被保护
            if (card.isProtected)
            {
               return false;
            }
		
		return true;
	}
	
	//检查卡牌是否能被自动填充
	private bool CheckCardIfCanBeAutoFilled(UserCardItem card)
	{
		if(!CheckCardIfCanBeMaterial(card))
		{
			return false;
		}
		
		//判断是否在群雄争霸中
		if(CheckIfChooseCardInQxzb())
		{
			return false;
		}
		
		//经验卡牌
		if(card.cardType == UserCardItem.CardType.EXP_CARD)
		{
			return true;
		}
		else
		{
			if(card.GetProvidedExp() >= 1000 
				  && card.quality <= 2
				     && card.GetMaxquality() <= 3)
			{
				return true;
			}
		}
		
		
		return false;
	}
	
	
	//自动填充材料卡牌
	private List<UserCardItem> GetAutoFilltheMaterial()
	{
		List<UserCardItem> cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        //按星级 ，等级 ，ID 排序
        List<UserCardItem> expCard = new List<UserCardItem>(); //----专门升级经验的卡牌，分出来
        List<UserCardItem> cards = new List<UserCardItem>();
		
        foreach(UserCardItem card in cardList)
        {
			if(!CheckCardIfCanBeAutoFilled(card) || materialList.Contains(card))
			{
				continue;
			}
		

            if (card.cardType == UserCardItem.CardType.EXP_CARD)
            {
                expCard.Add(card);
            }
            else
            {
				
                cards.Add(card);
            }
        }

        //排序
        expCard.Sort(SelectHeroController.MaterialExpCardCompareTo);
        cards.Sort(SelectHeroController.MaterialCompareTo);

        expCard.AddRange(cards);

        return expCard;
	}

    //显示升级界面的Item
    public void ShowUpdateMaterialCardItem(UserCardItem card)
    {
        //GameObject newItem = ResourceManager.Instance.loadWidget(materialHeroItem);
		GameObject newItem = CardListItemPool.Instance.GetListItem(materialHeroItem);
        newItem.transform.parent = gridMaterial.transform;
        newItem.transform.localPosition = new Vector3(0, 0, -20);
        newItem.transform.localScale = new Vector3(1, 1, 1);
        newItem.name = card.cardID.ToString();
		
		strCurListItemName = materialHeroItem;

        items.Add(newItem);

        UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
        icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).HeadIcon;
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		UISprite icon_bg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG").GetComponent<UISprite>();
		UISprite icon_border = newItem.transform.FindChild("CardIcon/CardIconBtn/Sp-outside").GetComponent<UISprite>();
		int icon_star = TableManager.GetCardByID(card.templateID).Star;
		icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
		icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
		//--------------------------------------------------------------------

        UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
        int nameLangId = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).Name;
        nameLabel.text = LanguageManger.GetWords(nameLangId);

        UILabel lifeLabel = newItem.transform.FindChild("Labels/Label-Hp-Value").GetComponent<UILabel>();
        lifeLabel.text = card.GetHp().ToString();

        UILabel attackLabel = newItem.transform.FindChild("Labels/Label-Attack-Value").GetComponent<UILabel>();
        attackLabel.text = card.GetAttack().ToString();

        UILabel levelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
        levelLabel.text = card.level.ToString();

        UISprite selectSprite = newItem.transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
        selectSprite.gameObject.SetActive(false);


        //设置listItem为显示状态
        this.SetListItemEnableState(newItem, true);

        //五行图标显示
        UISprite sttributeIcon = newItem.transform.FindChild("Sprite-Attribute").GetComponent<UISprite>();
        sttributeIcon.spriteName = card.GetAttributeIconName();


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

        UILabel ExpLabel = newItem.transform.FindChild("Labels/Label-Exp-Value").GetComponent<UILabel>();
        int cardExp = TableManager.GetCardByID(card.templateID).ExpBase * card.level;
        ExpLabel.text = cardExp.ToString();


        foreach (UserCardItem item in materialList)
        {
            if (item.cardID == card.cardID)
            {
                selectSprite.gameObject.SetActive(true);
                break;
            }
        }

        

        if (iGuide == 0)
        {
            this.guideMaterialItem = newItem;
            iGuide = -1;/*
			if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.CRAFT){
				GuideUpdate.Instance.NextStep();
			}*/
        }

        GameObject ChooseBtn = newItem;
        UIEventListener.Get(ChooseBtn).onClick += OnSelectMaterial;

        GameObject CardIconBtn = newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject;
        UIEventListener.Get(CardIconBtn).onClick += ShowCardInfo;
    }


    //选择材料算法
    static public int MaterialCompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        //排序顺序按照 先是星级 再ID，最后等级的方式进行
        if (cardB.quality != cardA.quality)
        {
            return -1 * cardB.quality.CompareTo(cardA.quality);
        }
        else if (cardB.templateID != cardA.templateID)
        {
            return (-1 * cardB.templateID.CompareTo(cardA.templateID));
        }
        else if (cardB.level != cardA.level)
        {
            return (-1*cardB.level.CompareTo(cardA.level));
        }
		else
		{
			return (-1*cardB.cardID.CompareTo(cardA.cardID));
		}
    }
	
	//选择材料，经验卡牌排序
	static public int MaterialExpCardCompareTo(UserCardItem cardA, UserCardItem cardB)
	{
		int nExpCardA = TableManager.GetCardByID(cardA.templateID).ExpBase * cardA.level;
        int nExpCardB = TableManager.GetCardByID(cardB.templateID).ExpBase * cardB.level;
	
		return nExpCardB.CompareTo(nExpCardA);
	}

    //显示升级材料完成后的调用
    public void OnShowUpdateMaterialCardItemComplete()
    {
		UserCardItem mhero = Obj_MyselfPlayer.GetMe().updateHeroItem;
        int money = mhero.GetUpDateCostMoney(materialList);             //----升级所花费的金币
		
		int exp = 0;
		foreach (UserCardItem item in materialList)
        {
            //计算当前选择的卡片 话费的金钱 和 所带的经验
            exp += TableManager.GetCardByID(item.templateID).ExpBase * item.level;
        }
		
		costMoney.text = money.ToString();
        totalExp.text = exp.ToString();
		
        //刷新item状态
        this.FreshAllMaterialItemsState();
    }
	
	//刷新自动填充按钮状态
	void  FreshAutoFillBtn()
	{
		bool bCanAutoFill = false;
		List<UserCardItem> materialCard = GetAutoFilltheMaterial();
		if(materialList.Count < 6 && materialCard.Count > 0)
		{
			bCanAutoFill = true;
		}
		
		AutoFillBtn.GetComponent<UIImageButton>().isEnabled = bCanAutoFill;
	}
	
	//自动填充按钮
	void AutoFillBtnMessage()
	{
		List<UserCardItem> cardLists = GetAutoFilltheMaterial();
		int nCanFillNum = 6-materialList.Count < cardLists.Count ?  6-materialList.Count : cardLists.Count; //需要填充的个数
		int  itemNum = items.Count;
		int nFilledNum = 0; //已经填充了的个数
		
			
			
			for(int j=0; j<nCanFillNum; j++)
			{
				if(nFilledNum == nCanFillNum)
				{
					break;
				}
	
				for(int i=0; i<itemNum; i++)	
				{
					string name = items[i].name;
					if(int.Parse(name) == cardLists[j].cardID)
					{
						
						materialList.Add(cardLists[j]);
						nFilledNum ++;
						UISprite selectSprite = items[i].transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
        				selectSprite.gameObject.SetActive(true);
					}
				}
			}
		
		this.FreshAllMaterialItemsState();
		FreshAutoFillBtn();
		SetUpdateMaterialAndReturnUpdateWindow(null);
	}

            /************************升级材料*********************/
/////////////////////////////////////////////////////////////////////////////////








/////////////////////////////////////////////////////////////////////////////////
            /************************卡牌强化*********************/


     //计算强化卡牌要显示的Items
    public List<UserCardItem> CalculateStrengthCardShowItems()
    {
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;

        iGuide = 0;

        //先把上阵和不上阵的分开来，再排序
        List<UserCardItem> listInFightArray = new List<UserCardItem>();
        List<UserCardItem> listNoInFightArray = new List<UserCardItem>();

        //在不上阵的中再分强化未满，和强化已满
        List<UserCardItem> listStrenghNoMaterialNotMaxArray = new List<UserCardItem>();
        List<UserCardItem> listStrenghNoMaterialMaxArray = new List<UserCardItem>();
		List<UserCardItem> expArray = new List<UserCardItem>();
		List<UserCardItem> evolutionArray = new List<UserCardItem>();
		
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
                listNoInFightArray.Add(card);
            }
        }

        //分强化满级和不满级
        foreach (var card in listNoInFightArray)
        {
            if (card.IsStrengthMax() && !CheckIfExpCardOrEvolution(card))
            {
                listStrenghNoMaterialMaxArray.Add(card);
            }
            else if(!card.IsStrengthMax() && !CheckIfExpCardOrEvolution(card))
            {
                listStrenghNoMaterialNotMaxArray.Add(card);
            }
			else if(card.cardType == UserCardItem.CardType.EXP_CARD)
			{
				expArray.Add(card);
			}
			else if(card.cardType == UserCardItem.CardType.EVOLUTION_CARD)
			{
				evolutionArray.Add(card);
			}
        }
		
        //排序和合并
        listStrenghNoMaterialNotMaxArray.Sort(StrengthCompareTo);
        listStrenghNoMaterialMaxArray.Sort(StrengthCompareTo);
		expArray.Sort(StrengthCompareTo);
		evolutionArray.Sort(StrengthCompareTo);
		
        listInFightArray.AddRange(listStrenghNoMaterialNotMaxArray);
        listInFightArray.AddRange(listStrenghNoMaterialMaxArray);
		listInFightArray.AddRange(expArray);
		listInFightArray.AddRange(evolutionArray);
		
        //按星级，等级，ID 排序
        cardList = listInFightArray;

        return cardList;
    }
	
	bool CheckIfExpCardOrEvolution(UserCardItem card)
	{
		if(card.cardType == UserCardItem.CardType.EVOLUTION_CARD 
			  || card.cardType == UserCardItem.CardType.EXP_CARD)
		{
			return true;
		}
		
		return false;
	}


    //显示强化界面的Item
    public void ShowStrengthCardItem(UserCardItem card)
    {
        //GameObject newItem = ResourceManager.Instance.loadWidget(strengthenHeroItem);
		GameObject newItem = CardListItemPool.Instance.GetListItem(strengthenHeroItem);
        newItem.transform.parent = gridStrengthen.transform;
        newItem.transform.localPosition = new Vector3(0, 0, -1);
        newItem.transform.localScale = new Vector3(1, 1, 1);
        newItem.name = card.cardID.ToString();
		
		strCurListItemName = strengthenHeroItem;
		
        items.Add(newItem);

        UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
        icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).HeadIcon;
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		UISprite icon_bg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG").GetComponent<UISprite>();
		UISprite icon_border = newItem.transform.FindChild("CardIcon/CardIconBtn/SpOutsaide").GetComponent<UISprite>();
		int icon_star = TableManager.GetCardByID(card.templateID).Star;
		icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
		icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
		//--------------------------------------------------------------------

        UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
        int nameLangId = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).Name;
        nameLabel.text = LanguageManger.GetWords(nameLangId);

        UILabel lifeLabel = newItem.transform.FindChild("Labels/Label-Hp-Value").GetComponent<UILabel>();
        lifeLabel.text = "" + (card.GetHp() - card.GetHpAdd());

        UILabel attackLabel = newItem.transform.FindChild("Labels/Label-Attack-Value").GetComponent<UILabel>();
        attackLabel.text = "" + (card.GetAttack() - card.GetAttackAdd());

        UILabel levelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
        levelLabel.text = card.level.ToString();

        UILabel attSthLabel = newItem.transform.FindChild("Labels/Label-AttSth-Value").GetComponent<UILabel>();
        attSthLabel.text = "(" + card.addQualityAtt.ToString() + "/99)";

        UILabel hpSthLabel = newItem.transform.FindChild("Labels/Label-HpSth-Value").GetComponent<UILabel>();
        hpSthLabel.text = "(" + card.addQualityHp.ToString() + "/99)";

        UISprite selectSprite = newItem.transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
        selectSprite.gameObject.SetActive(false);


        //五行图标显示
        UISprite sttributeIcon = newItem.transform.FindChild("Sprite-Attribute").GetComponent<UISprite>();
        sttributeIcon.spriteName = card.GetAttributeIconName();


        //攻击和血量强化 显示
        UILabel strenghHpLabel = newItem.transform.FindChild("Labels/Label-HpStrengh-Value").GetComponent<UILabel>();
        strenghHpLabel.text = "+" + card.GetHpAdd().ToString();
        if (card.GetHpAdd() == 0)
        {
            strenghHpLabel.gameObject.SetActive(false);
        }
		else
		{
			strenghHpLabel.gameObject.SetActive(true);
		}

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

        UILabel strenghAttackLabel = newItem.transform.FindChild("Labels/Label-AttackStrengh-Value").GetComponent<UILabel>();
        strenghAttackLabel.text = "+" + card.GetAttackAdd().ToString();
        if (card.GetAttackAdd() == 0)
        {
            strenghAttackLabel.gameObject.SetActive(false);
        }
		else
		{
			strenghAttackLabel.gameObject.SetActive(true);
		}
		

        if (card == strengthenHero)
        {
            selectSprite.gameObject.SetActive(true);
        }
        if (iGuide == 0)
        {
            iGuide = -1;
            this.guideStrengthenItem = newItem;
        }
        UIEventListener.Get(newItem).onClick += OnSelectStrengthenItem;

        GameObject CardIconBtn = newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject;
        UIEventListener.Get(CardIconBtn).onClick += ShowCardInfo;
    }


    //选择强化侠士算法
    static public int StrengthCompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        //排序顺序按照 先星级 再等级，最后ID的方式进行
        if (cardB.quality != cardA.quality)
        {
            return cardB.quality.CompareTo(cardA.quality);
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

    /************************卡牌强化*********************/
/////////////////////////////////////////////////////////////////////////////////



    




 /////////////////////////////////////////////////////////////////////////////////
    /************************卡牌精进*********************/

     //计算强化卡牌要显示的Items
    public List<UserCardItem> CalculateEvolutionCardShowItems()
    {
        iGuide = 0;
        cardList = Obj_MyselfPlayer.GetMe().cardBagList;
        bChooseCard = false;

        //先把满足精进和不满足精进(完全不能精进的不显示)的分开来，再排序
        List<UserCardItem> listCanEvolutionArray = new List<UserCardItem>();
        List<UserCardItem> listCannotEvolutionArray = new List<UserCardItem>();

        foreach (var card in cardList)
        {
            if (card.IsFullLevel() && TableManager.GetCardByID(card.templateID).NextCard != -1 && CheckEvolutionMaterialEnough(card))
            {
                listCanEvolutionArray.Add(card);
            }
            else if (TableManager.GetCardByID(card.templateID).NextCard != -1)
            {
                listCannotEvolutionArray.Add(card);
            }
        }

        //再把上阵和不上阵的分开来，再排序(上阵和不上阵的是在 不能精进里面筛选的)
        List<UserCardItem> listInFightArray = new List<UserCardItem>();
        List<UserCardItem> listNoInFightArray = new List<UserCardItem>();

        //先找队长和队员
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
        {
            foreach (var card in listCannotEvolutionArray)
            {
                if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == card.cardID)
                {
                    listInFightArray.Add(card);
                    break;
                }
            }
        }

        //再找其他卡牌
        foreach (var card in listCannotEvolutionArray)
        {
            if (!card.IsInFightArray())
            {
                listNoInFightArray.Add(card);
            }
        }

        //排序并合并
        listNoInFightArray.Sort(EvolutionCompareTo);
        listInFightArray.AddRange(listNoInFightArray);
        listCanEvolutionArray.Sort(EvolutionCompareTo);
        listCanEvolutionArray.AddRange(listInFightArray);
        cardList = listCanEvolutionArray;

        return cardList;
    }


     //显示精进界面的Item
    public void ShowEvolutionCardItem(UserCardItem card)
    {
        //GameObject newItem = ResourceManager.Instance.loadWidget(evolutionHeroItem);
        GameObject newItem = CardListItemPool.Instance.GetListItem(evolutionHeroItem);
		newItem.transform.parent = gridEvolution.transform;
        newItem.transform.localPosition = new Vector3(0, 0, -1);
        newItem.transform.localScale = new Vector3(1, 1, 1);
        newItem.name = card.cardID.ToString();
		
		strCurListItemName = evolutionHeroItem;
		
        items.Add(newItem);

        UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
        icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).HeadIcon;
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		UISprite icon_bg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG").GetComponent<UISprite>();
		UISprite icon_border = newItem.transform.FindChild("CardIcon/CardIconBtn/SpOutside").GetComponent<UISprite>();
	
		int icon_star = TableManager.GetCardByID(card.templateID/* Obj_MyselfPlayer.GetMe(). friendSearchResult.cardTempletID*/).Star;
		icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
		icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
		//--------------------------------------------------------------------

        UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
        int nameLangId = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).Name;
        nameLabel.text = LanguageManger.GetWords(nameLangId);

        UILabel lifeLabel = newItem.transform.FindChild("Labels/Label-Hp-Value").GetComponent<UILabel>();
        lifeLabel.text = card.GetHp().ToString();

        UILabel attackLabel = newItem.transform.FindChild("Labels/Label-Attack-Value").GetComponent<UILabel>();
        attackLabel.text = card.GetAttack().ToString();

        UILabel levelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
        levelLabel.text = card.level.ToString();

        UILabel maxlevelLabel = newItem.transform.FindChild("Labels/Label-MaxLevel-Value").GetComponent<UILabel>();
        maxlevelLabel.text = card.level.ToString() + "/" + TableManager.GetCardByID(card.templateID).MaxLevel.ToString();

        UILabel evoLabel = newItem.transform.FindChild("Labels/Label-Evolution").GetComponent<UILabel>();
        if (card.IsFullLevel() && TableManager.GetCardByID(card.templateID).NextCard != -1 && CheckEvolutionMaterialEnough(card))
        {
            evoLabel.text = "可精进";
            evoLabel.color = Color.green;
            maxlevelLabel.color = Color.green;
        }
        else if (TableManager.GetCardByID(card.templateID).NextCard == -1) //--不可进化
        {
            evoLabel.text = "不可精进";
            evoLabel.color = Color.red;
            maxlevelLabel.color = Color.white;
        }
        else if (!card.IsFullLevel())
        {
            evoLabel.text = "等级不足";
            evoLabel.color = Color.red;
            maxlevelLabel.color = Color.red;
        }
        else
        {
            evoLabel.text = "材料不足";
            evoLabel.color = Color.red;
            maxlevelLabel.color = Color.red;
        }

        //五行图标显示
        UISprite sttributeIcon = newItem.transform.FindChild("Sprite-Attribute").GetComponent<UISprite>();
        sttributeIcon.spriteName = card.GetAttributeIconName();


        UISprite selectSprite = newItem.transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
        selectSprite.gameObject.SetActive(false);


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


        //当前有选择要进化的卡牌
        if (card == evolutionHero)
        {
            bChooseCard = true;
            selectSprite.gameObject.SetActive(true);
        }


        //只要不是 不可进化卡牌 即可进入进化界面
        if (TableManager.GetCardByID(card.templateID).NextCard != -1)
        {
            UIEventListener.Get(newItem).onClick += OnSelectEvolutionItem;
        }

        GameObject CardIconBtn = newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject;
        UIEventListener.Get(CardIconBtn).onClick += ShowCardInfo;

    }


    //选择精进侠士算法
    static public int EvolutionCompareTo(UserCardItem cardA, UserCardItem cardB)
    {
        //排序顺序按照 先星级 再等级，最后ID的方式进行
        if (cardB.quality != cardA.quality)
        {
            return cardB.quality.CompareTo(cardA.quality);
        }
        else if(cardB.templateID != cardA.templateID)
        {
            return (-1 * cardB.templateID.CompareTo(cardA.templateID));
        }
		 else
        {
            return cardB.level.CompareTo(cardA.level);
        }

    }


    //判断精进要使用的材料是否充足
    private bool CheckEvolutionMaterialEnough(UserCardItem heroCardItem)
    {
        //选择的英雄的类型ID
        int temp_id = heroCardItem.templateID;

        //获得英雄星级

        int star = TableManager.GetCardByID(temp_id).Star;

        int nEvolveID = 0;
        //小于6的情况跟五行无关
        if (star < 6)
        {
            nEvolveID = star;
        }
        else//星级为6的卡牌升到7星要根据五行来判断(星级+五行ID)
        {
            nEvolveID += (star + heroCardItem.GetAttributeID());
        }

        UserCardItem[] materialCardItems = new UserCardItem[5];
        int[] matIDArray = new int[5];
        for (int i = 0; i < 5; i++)
        {
            matIDArray[i] = -1;
            materialCardItems[i] = null;
        }

        for (int i = 0; i < 5; i++)
        {
            matIDArray[i] = TableManager.GetEvolveByID(nEvolveID).GetCardIDbyIndex(i);

            foreach (UserCardItem item in Obj_MyselfPlayer.GetMe().cardBagList)
            {
                //被保护的卡牌不能被吞
                if (!item.IsInFightArray() && !item.isProtected && item.templateID == matIDArray[i])
                {
                    bool isUsed = false;
                    for (int j = 0; j < 5; j++)
                    {
                        if (materialCardItems[j] != null &&
                            materialCardItems[j].cardID == item.cardID)
                        {
                            isUsed = true;
                        }
                    }
                    if (!isUsed)
                    {
                        materialCardItems[i] = item;
                        break;
                    }
                }
            }
        }

        //判断材料是否充足
        bool bEnough = true;
        for (int i = 0; i < 5; i++)
        {
            if (matIDArray[i] != -1 && materialCardItems[i] == null)
            {
                bEnough = false;
                break;
            }
        }

        return bEnough;
    }


    //显示精进卡牌结束
    public void OnShowEvolutionCardItemComplete()
    {
        //如果当前没有选择，则把 evolutionHeroItem 置为空
        if (!bChooseCard)
        {
            Obj_MyselfPlayer.GetMe().evolutionHeroItem = null;
        }
    }

    /************************卡牌精进*********************/
/////////////////////////////////////////////////////////////////////////////////
    



    //拖拽完的调用函数
    public void OnDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode dragonMode)
    {
        //清理上一页的Items
        this.resetItems();
    }


    //清除Items
    private void resetItems()
    {
        foreach (GameObject item in items)
		{
			if(item != null)
			{
				//item.transform.parent = null;
				//Destroy(item);
				CardListItemPool.Instance.DestroyItemAndPushToPool(item, strCurListItemName);
			}
		}
		items.Clear();
    }

	
	

	//显示选择界面
	private void ShowSelectWindowBottom(bool bShow)
	{
		//transform.FindChild("Sprites").gameObject.SetActive(bShow);
		costMoney.gameObject.SetActive(bShow);
        totalExp.gameObject.SetActive(bShow);
	}
	


	private void UpdateList(int type)
	{
		GameObject parent = null;

        //设置 items的获得函数
        BagItemsPage bagItempage = cachedTransform.GetComponent<BagItemsPage>();
		if (type == (int)GRID_TYPE.UPDATE)
		{
			parent = gridUpdate;
            if (bagItempage != null)
            {
                bagItempage.onCalculateShowItemsFunction = CalculateUpdateShowItems;
                bagItempage.onDragonFinishedClearItemsFun = OnDragonFinishedClearItemsFun;
                bagItempage.onShowCardItemFunction = ShowUpdateCardItem;
                bagItempage.onShowCardItemCompleteFunction = null;
				bagItempage.StartBagItemPages();
            }
			
			if(noMaterialItem != null)
			{
				noMaterialItem.SetActive(false);
			}
			//UpdateHeroList(parent);
		}
        else if (type == (int)GRID_TYPE.MATERIAL)
        {
            parent = gridMaterial;
			int nItemNum = 0;
            if (bagItempage != null)
            {
                bagItempage.onCalculateShowItemsFunction = CalculateUpdateMaterialShowItems;
                bagItempage.onDragonFinishedClearItemsFun = OnDragonFinishedClearItemsFun;
                bagItempage.onShowCardItemFunction = ShowUpdateMaterialCardItem;
                bagItempage.onShowCardItemCompleteFunction = OnShowUpdateMaterialCardItemComplete;
				bagItempage.StartBagItemPages();
				nItemNum = bagItempage.currentItemsNum;
            }
			
			if(nItemNum > 0 && noMaterialItem != null)
			{
				bottomBar.SetActive(true);
				noMaterialItem.SetActive(false);
			}
			else if(noMaterialItem != null)
			{
				bottomBar.SetActive(false);
				noMaterialItem.SetActive(true);
			}
			
			//UpdateMaterialList(parent);
        }
        else if (type == (int)GRID_TYPE.EVOLUTION)
        {
            parent = gridEvolution;
            if (bagItempage != null)
            {
                bagItempage.onCalculateShowItemsFunction = CalculateEvolutionCardShowItems;
                bagItempage.onDragonFinishedClearItemsFun = OnDragonFinishedClearItemsFun;
                bagItempage.onShowCardItemFunction = ShowEvolutionCardItem;
                bagItempage.onShowCardItemCompleteFunction = OnShowEvolutionCardItemComplete;
				bagItempage.StartBagItemPages();
            }
			
			if(noMaterialItem != null)
			{
				noMaterialItem.SetActive(false);
			}
			
			
			//UpdateEvolutionList(parent);
        }
		else if(type == (int)GRID_TYPE.STRENGTHEN)
		{
			parent = gridStrengthen;
            if (bagItempage != null)
            {
                bagItempage.onCalculateShowItemsFunction = CalculateStrengthCardShowItems;
                bagItempage.onDragonFinishedClearItemsFun = OnDragonFinishedClearItemsFun;
                bagItempage.onShowCardItemFunction = ShowStrengthCardItem;
                bagItempage.onShowCardItemCompleteFunction = null;
				bagItempage.StartBagItemPages();
            }
			
			if(noMaterialItem != null)
			{
				noMaterialItem.SetActive(false);
			}
			//UpdateStrengthenList(parent);
		}
		
		
	}
	
	private void EnalbleGrid(int grid)
	{
		scrollBar.scrollValue = 0;
		switch(grid)
		{
		case (int)GRID_TYPE.UPDATE:
			gridMaterial.SetActive(false);
			gridEvolution.SetActive(false);
			gridStrengthen.SetActive(false);
			gridUpdate.SetActive(true);
			ShowSelectWindowBottom(false);
			bottomBar.SetActive(false);
			UpdateList((int)GRID_TYPE.UPDATE);
			break;
			
		case (int)GRID_TYPE.MATERIAL:
			gridUpdate.SetActive(false);
			gridEvolution.SetActive(false);
			gridStrengthen.SetActive(false);
			gridMaterial.SetActive(true);
			ShowSelectWindowBottom(true);
			//bottomBar.SetActive(true);
			UpdateList((int)GRID_TYPE.MATERIAL);
			break;
			
		case (int)GRID_TYPE.EVOLUTION:
			gridUpdate.SetActive(false);
			gridMaterial.SetActive(false);
			gridStrengthen.SetActive(false);
			gridEvolution.SetActive(true);
			ShowSelectWindowBottom(false);
			bottomBar.SetActive(false);
			UpdateList((int)GRID_TYPE.EVOLUTION);
			break;
		case (int)GRID_TYPE.STRENGTHEN:
			gridUpdate.SetActive(false);
			gridEvolution.SetActive(false);
			gridMaterial.SetActive(false);
			gridStrengthen.SetActive(true);
			ShowSelectWindowBottom(false);
			bottomBar.SetActive(false);
			UpdateList((int)GRID_TYPE.STRENGTHEN);
			break;
		}
	}
	
	void OnEnable()
	{
		if(logicTarget == null)
		{
			return;
		}
		
		//在这里更新数据
		updateHero = Obj_MyselfPlayer.GetMe().updateHeroItem;
		materialList.Clear();
		for(int i = 0; i < Obj_MyselfPlayer.GetMe().updateMaterialItems.Length; i++)
		{
			if(Obj_MyselfPlayer.GetMe().updateMaterialItems[i] != null)
			{
				materialList.Add(Obj_MyselfPlayer.GetMe().updateMaterialItems[i]);
			}
		}
		evolutionHero = Obj_MyselfPlayer.GetMe().evolutionHeroItem;
		strengthenHero = Obj_MyselfPlayer.GetMe().strengthenHeroItem;
		
		switch(Obj_MyselfPlayer.GetMe().curCultivateType)
		{
		case CultivateType.UPDATE:
			if(Obj_MyselfPlayer.GetMe().isSelectHero)
			{
				OnBtnUpdate();
			}
			else
			{
				OnBtnMaterial();
			}
			break;
		case CultivateType.EVOLUTION:
			OnBtnEvolution();
			break;
		case CultivateType.STRENGTHEN:
			OnBtnStrengthen();
			break;
		default:
			break;
		}
	}

   

	public void OnBtnUpdate()
	{
		
		materialList.Clear();
		EnalbleGrid((int)GRID_TYPE.UPDATE);
		AutoFillBtn.SetActive(false);
		gbTitle.transform.GetComponent<UISprite>().spriteName = "xuanzeshengjixiashi";
		//transform.FindChild("Label-Title").GetComponent<UILabel>().text = "选择侠士";
		
	}
	
	public void OnBtnMaterial()
	{
		EnalbleGrid((int)GRID_TYPE.MATERIAL);
		AutoFillBtn.SetActive(true);
		FreshAutoFillBtn();
		gbTitle.transform.GetComponent<UISprite>().spriteName = "xuanzexishouxiashi";
		//transform.FindChild("Label-Title").GetComponent<UILabel>().text = "选择合成卡牌";
		
	}
	
	public void OnBtnEvolution()
	{
		EnalbleGrid((int)GRID_TYPE.EVOLUTION);
		AutoFillBtn.SetActive(false);
		gbTitle.transform.GetComponent<UISprite>().spriteName = "xuanzejingjinxiashi";
        //transform.FindChild("Label-Title").GetComponent<UILabel>().text = "选择侠士";
	}
	public void OnBtnStrengthen()
	{
		EnalbleGrid((int)GRID_TYPE.STRENGTHEN);
		AutoFillBtn.SetActive(false);
		gbTitle.transform.GetComponent<UISprite>().spriteName = "xuanzexiulianxiashi";
        //transform.FindChild("Label-Title").GetComponent<UILabel>().text = "选择侠士";
	}
	
	public void OnTopLeftBtn()
	{
		if(Obj_MyselfPlayer.GetMe().curCultivateType == CultivateType.UPDATE)
		{
			GameObject.FindWithTag("main_ui_logic").SendMessage("onUpdateWindow");
		}
		else if(Obj_MyselfPlayer.GetMe().curCultivateType == CultivateType.EVOLUTION)
		{
			GameObject.FindWithTag("main_ui_logic").SendMessage("onEvolutionWindow");
		}
		else if(Obj_MyselfPlayer.GetMe().curCultivateType == CultivateType.STRENGTHEN)
		{
			GameObject.FindWithTag("main_ui_logic").SendMessage("OnCardStrengthenWindow");
		}
		
	}
	
	
	//判断选择的材料是否在群雄争霸中
	private bool CheckIfChooseCardInQxzb()
	{
		foreach(UserCardItem cardItem in materialList)
		{
			if(cardItem != null
				 && cardItem.IsInQxzbFightArray())
			{
				return true;
			}
		}
		
		return false;
	}
	
	public void OnBottomComfirmBtn()
	{
		if(Obj_MyselfPlayer.GetMe().curCultivateType == CultivateType.UPDATE)
		{
			//判断是否含有高星卡牌
			bool bContainHighStarCard = false;
			//cb:把6改为materialList.Count，这里有数组越界！
			for(int i=0; i<materialList.Count; i++)
			{
				//6星以上的卡牌提示是否要吞噬
				if(materialList[i].quality >= 6)
				{
					bContainHighStarCard = true;
					break;
				}
			}
			
			if(CheckIfChooseCardInQxzb())
			{
				BoxManager.showMessageByID((int)MessageIdEnum.Msg196);
				UIEventListener.Get(BoxManager.getYesButton()).onClick += SetUpdateMaterialAndReturnUpdateWindow;
				return;
			}
			
			
			if(bContainHighStarCard)
			{
//				BoxManager.showConfirmMessage("材料侠士中有6星以上侠士。\n是否继续？");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg107);
				UIEventListener.Get(BoxManager.getYesButton()).onClick += SetUpdateMaterialAndReturnUpdateWindow;
            	//UIEventListener.Get(BoxManager.getNoButton()).onClick += CancelUpdateCard;
				return;
			}
			
			
			
			SetUpdateMaterialAndReturnUpdateWindow(null);
		}
	}
	
	//设置升级卡牌材料，并且返回升级界面
	private void SetUpdateMaterialAndReturnUpdateWindow(GameObject btn)
	{
		for(int i = 0; i < 6; i++)
			{
				if(i < materialList.Count)
				{
					Obj_MyselfPlayer.GetMe().updateMaterialItems[i] = materialList[i];
				}
				else
				{
					Obj_MyselfPlayer.GetMe().updateMaterialItems[i] = null;
				}
				
			}
		logicTarget.SendMessage("onUpdateWindow");
	}
	
	public void OnSelectUpdateItem(GameObject go)
	{
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE &&
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.NONE_2)
			GuideUpdate.Instance.NextStep();//卡牌升级指引阶段 SELECT_3
		UserCardItem item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(long.Parse(go.name));
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE)
			GuideManager.Instance.SetTempletID(item.templateID);
		if(item != null)
		{
			Obj_MyselfPlayer.GetMe().updateHeroItem = item;
		}
		logicTarget.SendMessage("onUpdateWindow");
	}


    //刷新材料卡牌
    private void FreshMaterialItem(UserCardItem material_item, GameObject go)
    {
        //检查当前是否已经选满6个
        if (materialList.Count >= 6 && !materialList.Contains(material_item))
        {
            return;
        }

        //检查是否已选
        if (materialList.Contains(material_item))
        {
            materialList.Remove(material_item);
            UISprite selectSprite = go.transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
            selectSprite.gameObject.SetActive(false);
        }
        else
        {
            materialList.Add(material_item);
            UISprite selectSprite = go.transform.FindChild("Sprites/Sprite-Selected").GetComponent<UISprite>();
            selectSprite.gameObject.SetActive(true);
        }


        this.FreshAllMaterialItemsState();
		FreshAutoFillBtn();
        int money = 0;
        int exp = 0;
        UserCardItem mhero = Obj_MyselfPlayer.GetMe().updateHeroItem;
        foreach (UserCardItem item in materialList)
        {
            exp += TableManager.GetCardByID(item.templateID).ExpBase * item.level;
			/*
            if ((item.addQualityHp + item.addQualityAtt) > 0)
            {
                money += mhero.level * 100 + (item.addQualityAtt + item.addQualityHp) * 10000 + (mhero.addQualityAtt + mhero.addQualityHp) * 10000;
            }
            else
            {
                money += mhero.level * 100;
            }
            */

        }
		
		money = mhero.GetUpDateCostMoney(materialList);

        costMoney.text = money.ToString();
        totalExp.text = exp.ToString();
    }


	public void OnSelectMaterial(GameObject go)
	{
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE &&
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.SELECT_6)
			GuideUpdate.Instance.NextStep();//卡牌升级指引阶段 SELECT_5
		UserCardItem material_item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(long.Parse(go.name));
		if(material_item == null)
		{
			Debug.LogError("OnSelectMaterial(), select item is null !");
			return;
		}
		/*
        if (material_item.quality >= 6 && !materialList.Contains(material_item))
        {
            //string strDescri = "是否确定要吞噬此 " + material_item.quality + " 星侠士"; 
            materialCard = material_item;
            chooseItemObj = go;
            //BoxManager.showConfirmMessage(strDescri); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg102,material_item.quality.ToString());
            UIEventListener.Get(BoxManager.getYesButton()).onClick += OkMaterial;
            UIEventListener.Get(BoxManager.getNoButton()).onClick += CancelMaterial;
            return;
        }
        */

        this.FreshMaterialItem(material_item, go);
       
	}

    //确定卡牌为材料
    public void OkMaterial(GameObject button)
    {
        if (materialCard == null || chooseItemObj == null)
        {
            return;
        }

        this.FreshMaterialItem(materialCard, chooseItemObj);
    }

    //刷新所有的item显示状态
    public void FreshAllMaterialItemsState()
    {

        //如果超过6个,不能选了
        GameObject curGrid = gridMaterial;
        if (materialList.Count >= 6)
        {
            for (int i = 0; i < curGrid.transform.childCount; i++)
            {
                GameObject item_obj = curGrid.transform.GetChild(i).gameObject;
                UserCardItem card_item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(long.Parse(item_obj.transform.name));
                if (card_item == null)
                {
                    Debug.LogError("OnSelectMaterial(), selected material is null");
                    continue;
                }

                if (materialList.Contains(card_item))
                {
                    item_obj.transform.Find("BG").GetComponent<UIImageButton>().isEnabled = true;
                    //this.SetListItemEnableState(item_obj, true);
                }
                else
                {
                    item_obj.transform.Find("BG").GetComponent<UIImageButton>().isEnabled = false;
                    //this.SetListItemEnableState(item_obj, false);
                }
            }
        }
        else
        {
            for (int i = 0; i < curGrid.transform.childCount; i++)
            {
                GameObject item_obj = curGrid.transform.GetChild(i).gameObject;
                UserCardItem card_item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(long.Parse(item_obj.transform.name));
                if (card_item == null)
                {
                    Debug.LogError("OnSelectMaterial(), selected material is null");
                    continue;
                }
                item_obj.transform.Find("BG").GetComponent<UIImageButton>().isEnabled = true;
                //this.SetListItemEnableState(item_obj, true);
            }
        }
    }
	
	/*
	//是否是已被选中的材料卡牌
	private bool IsChoosedMaterialCard(UserCardItem checkCard)
	{
		foreach(var materialCard in materialList)
		{
			if(materialCard.cardID == checked.)
			{
				
			}
		}
	}
	*/

    //取消卡牌为材料
    public void CancelMaterial(GameObject button)
    {

    }
	
	
	//设置列表item的Enable状态
	private void SetListItemEnableState(GameObject gObj, bool bEnable)
	{
		//gObj.GetComponent<UIImageButton>().isEnabled = bEnable;
		gObj.GetComponent<BoxCollider>().enabled = bEnable;
		//gObj.transform.FindChild("zhezhao").gameObject.SetActive(!bEnable);
	}

    public void ShowCardInfo(GameObject selectedItem)
    {
		if(!GuideManager.Instance.isEnd()){
			return;
		}
        int cardID = (int)long.Parse(selectedItem.transform.parent.parent.name);
        for (int i = 0; i < Obj_MyselfPlayer.GetMe().cardBagList.Count; i++)
        {
            if (cardID == Obj_MyselfPlayer.GetMe().cardBagList[i].cardID)
            {
                BoxManager.showCardInfoMessage(cardID, Obj_MyselfPlayer.GetMe().cardBagList[i].templateID);
                break;
            }
        }
    }
	
	public void OnSelectEvolutionItem(GameObject go)
	{
		UserCardItem item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(long.Parse(go.name));
		if(item != null)
		{
			Obj_MyselfPlayer.GetMe().evolutionHeroItem = item;
		}
		logicTarget.SendMessage("onEvolutionWindow");
	}
	
	public void OnSelectStrengthenItem(GameObject go)
	{
		UserCardItem item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(long.Parse(go.name));
		if(item != null)
		{
			Obj_MyselfPlayer.GetMe().strengthenHeroItem = item;
		}
		logicTarget.SendMessage("OnCardStrengthenWindow");
	}
	
	//for guide
	private GameObject guideCardItem;
	public GameObject getGuideCardItem()
	{
		return guideCardItem;
	}
	private GameObject guideMaterialItem;
	public GameObject getGuideMaterialItem()
	{
		return guideMaterialItem;
	}
	public GameObject getGuideConfirmButton()
	{
		return this.transform.FindChild("ConfirmChooseSP/Button").gameObject;
	}
	private GameObject guideStrengthenItem;
	public GameObject getGuideStrengthenItem()
	{
		return guideStrengthenItem;
	}
	public void SetPanelListEnable(bool bEnable){
		dragPanel.enabled = bEnable;
	}
}
