using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

public class SelectAssistController : MonoBehaviour {
	
	private string SelectAssistItem="SelectAssistItem";
	public UIScrollBar scrollBar;
	public GameObject listParent;
	private GameObject logicTarget;
	private List<AssistFriend> assistanceList;
	//Transform 的缓存
    private Transform cachedTransform;
	private List<GameObject> items = new List<GameObject>();
	private int friendGuid=0;
	public UIDraggablePanel dragPanel;
	
	void Awake()
	{
//		cardList = Obj_MyselfPlayer.GetMe().cardBagList;
//		materialList = new List<UserCardItem>();
//		materialNum = 0;
		//register to get guid back
//		Messenger.AddListener<long>("SelectedAssistFriend",SelectAssistHero);
	}
	// Use this for initialization
	void OnEnable () {
		//Obj_MyselfPlayer.GetMe().bInPvP = false;
		Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.PVE;
		logicTarget = GameObject.Find("MainUILogic");
		int count = listParent.transform.childCount;
		for(int j = count-1; j >= 0; j--)
		{
			Destroy(listParent.transform.GetChild(j).gameObject);
		}
		listParent.GetComponent<UIGrid>().repositionNow = true;
		assistanceList = Obj_MyselfPlayer.GetMe().AssistanceList;
		cachedTransform = transform;
		friendGuid=0;
		//设置 items的获得函数
        BagItemsPage bagItempage = cachedTransform.GetComponent<BagItemsPage>();
        if (bagItempage != null)
        {
			bagItempage.onCalculateShowAssistFriendItemsFunction=null;
            bagItempage.onDragonFinishedClearItemsFun=null;
            bagItempage.onShowAssistFriendItemFunction=null;
            bagItempage.onCalculateShowAssistFriendItemsFunction += CalculateShowItems;
            bagItempage.onDragonFinishedClearItemsFun += OnDragonFinishedClearItemsFun;
            bagItempage.onShowAssistFriendItemFunction += ShowCardItem;
        }
		 //开始分页显示功能
       if (cachedTransform != null)
       {
           cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages();
       }
//		int i = 0;
//		foreach(AssistFriend af in assistanceList)
//		{
//			//如果是0的数据有错空过去
//			if(af.cardTempleId==0||af.name=="")
//			{
//				continue;
//			}
//			GameObject newItem =  ResourceManager.Instance.loadWidget(SelectAssistItem);//(GameObject)Instantiate(SelectAssistItem);
//			newItem.transform.parent = listParent.transform;
//			newItem.transform.localPosition = new Vector3(0, 0, -1);
//			newItem.transform.localScale = new Vector3(1, 1, 1);
//			//update item content
//			UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
//			nameLabel.text = af.name;
//			UILabel isFriendLabel = newItem.transform.FindChild("Labels/Label-Is-Friend").GetComponent<UILabel>();
//			isFriendLabel.text = af.isMyFriend?"八拜之交":"助战侠士";
//			UILabel skillLabel = newItem.transform.FindChild("Labels/Label-Friend-Skill").GetComponent<UILabel>();
//			Tab_Card tab_card = TableManager.GetCardByID(af.cardTempleId);
//			Tab_Leaderskill leaderSkill = TableManager.GetLeaderskillByID(tab_card.SkillLeader);
//			if(leaderSkill!=null)
//			{
//				skillLabel.text = LanguageManger.GetWords(leaderSkill.Name);
//			}
//			UILabel friendShipValueLabel = newItem.transform.FindChild("Labels/Label-FriendShip-Value").GetComponent<UILabel>();
//			friendShipValueLabel.text = af.friendShipNum.ToString();
//			UILabel friendLevelLabel = newItem.transform.FindChild("Labels/Label-Friend-Level-Value").GetComponent<UILabel>();
//			friendLevelLabel.text = af.level.ToString();
//			UILabel cardLevelLabel = newItem.transform.FindChild("Labels/Label-Card-Level-Value").GetComponent<UILabel>();
//			cardLevelLabel.text = af.cardLevel.ToString();
//			UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
//			string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(af.cardTempleId).Appearance).HeadIcon;
//			AtlasManager.Instance.setHeadName(icon,  atlasname);
//			icon.transform.localScale=new Vector3(75,75,1);
//			GameObject starManage=newItem.transform.FindChild("StarManager").gameObject;
//			Transform transformStars = starManage.transform;
//            for (int j = 1; j <= 7; j++)
//           	 {
//                if (j <=TableManager.GetCardByID(af.cardTempleId).Star)
//                {
//                    GameObject starIcon = transformStars.FindChild("Star_" + j).gameObject;
//                   	 starIcon.SetActive(true);
//                }
//                else
//                {
//                    GameObject starIcon = transformStars.FindChild("Star_" + j).gameObject;
//                    starIcon.SetActive(false);
//				}
//			}
////			UISprite bg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG").GetComponent<UISprite>();
////			bg.spriteName = UserCardItem.iconFrameName[TableManager.GetCardByID(af.cardTempleId).Star];
////			bg.transform.localScale=new Vector3(95,90,1);
//			
//			newItem.name = af.guid.ToString();
//			UIEventListener.Get(newItem).onClick += SelectAssistHero;
//			GameObject cardInfo = newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject;
//			cardInfo.name=af.guid.ToString();
//			UIEventListener.Get(cardInfo).onClick+=SelectCardInfo;
//			if(i==0)
//			{
//				guideItem = newItem;
//				i=-1;
//			}
//		}
		listParent.GetComponent<UIGrid>().repositionNow = true;
		
		switch(GuideManager.Instance.currentStep){
		case GuideManager.GUIDE_STEP.COPY1_1:
			GuideCopy1_1.Instance.NextStep();//新手战斗引导 LABEL_5
			break;
		case GuideManager.GUIDE_STEP.COPY1_2:
			GuideCopy1_2.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY1_3:
			GuideCopy1_3.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY1_4:
			GuideCopy1_4.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY1_5:
			GuideCopy1_5.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY2_1:
			GuideCopy2_1.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		default:
			break;
		}

	}
	
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
	
	public void ShowCardItem(AssistFriend af)
    {
        if (af == null)
        {
            return;
        }
		//如果是0的数据有错空过去
			if(af.cardTempleId==0||af.name=="")
			{
				return;
			}
			GameObject newItem =  ResourceManager.Instance.loadWidget(SelectAssistItem);//(GameObject)Instantiate(SelectAssistItem);
			newItem.transform.parent = listParent.transform;
			newItem.transform.localPosition = new Vector3(0, 0, -1);
			newItem.transform.localScale = new Vector3(1, 1, 1);
			//update item content
			UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
			nameLabel.text = af.name;
			//------------------------卡牌背景及外框--------------------------------
			//2013-10-12 Jack Wen
			UISprite icon_bg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG").GetComponent<UISprite>();
			UISprite icon_border = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite").GetComponent<UISprite>();
			int icon_star = TableManager.GetCardByID(af.cardTempleId).Star;
			icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
			icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
			//--------------------------------------------------------------------
			UILabel isFriendLabel = newItem.transform.FindChild("Labels/Label-Is-Friend").GetComponent<UILabel>();
			isFriendLabel.text = af.isMyFriend?"八拜之交":"江湖侠士";
			UILabel skillLabel = newItem.transform.FindChild("Labels/Label-Friend-Skill").GetComponent<UILabel>();
			Tab_Card tab_card = TableManager.GetCardByID(af.cardTempleId);
			Tab_Leaderskill leaderSkill = TableManager.GetLeaderskillByID(tab_card.SkillLeader);
			if(leaderSkill!=null)
			{
				skillLabel.text = LanguageManger.GetWords(leaderSkill.Name);
			}
		else
		{
			skillLabel.text = "";
		}
			UILabel friendShipValueLabel = newItem.transform.FindChild("Labels/Label-FriendShip-Value").GetComponent<UILabel>();
			friendShipValueLabel.text = af.friendShipNum.ToString();
			UILabel friendLevelLabel = newItem.transform.FindChild("Labels/Label-Friend-Level-Value").GetComponent<UILabel>();
			friendLevelLabel.text = af.level.ToString();
			UILabel cardLevelLabel = newItem.transform.FindChild("Labels/Panel-Lv/Label-lv").GetComponent<UILabel>();
			cardLevelLabel.text = af.cardLevel.ToString();
			UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
			string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(af.cardTempleId).Appearance).HeadIcon;
			AtlasManager.Instance.setHeadName(icon,  atlasname);
			icon.transform.localScale=new Vector3(82,82,1);
			GameObject starManage=newItem.transform.FindChild("StarManager").gameObject;
			Transform transformStars = starManage.transform;
            for (int j = 1; j <= 7; j++)
           	 {
                if (j <=TableManager.GetCardByID(af.cardTempleId).Star)
                {
                    GameObject starIcon = transformStars.FindChild("Star_" + j).gameObject;
                   	 starIcon.SetActive(true);
                }
                else
                {
                    GameObject starIcon = transformStars.FindChild("Star_" + j).gameObject;
                    starIcon.SetActive(false);
				}
			}
			UISprite property = newItem.transform.FindChild("Sprite/Sprite-Property").GetComponent<UISprite>();
			property.spriteName = UserCardItem.elementTypeName[ TableManager.GetCardByID(af.cardTempleId).Element ];
//			UISprite bg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG").GetComponent<UISprite>();
//			bg.spriteName = UserCardItem.iconFrameName[TableManager.GetCardByID(af.cardTempleId).Star];
//			bg.transform.localScale=new Vector3(95,90,1);
			
			newItem.name = af.guid.ToString();
			UIEventListener.Get(newItem).onClick += SelectAssistHero;
			GameObject cardInfo = newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject;
			cardInfo.name=af.guid.ToString();
			UIEventListener.Get(cardInfo).onClick+=SelectCardInfo;
			if(friendGuid==0)
			{
				guideItem = newItem;
				friendGuid=-1;
			}
            Transform QxzbTopStarTrans = newItem.transform.FindChild("QxzbTopStar");
            if (QxzbTopStarTrans != null)
            {
                //沒有群雄爭霸第一名
                if (af.nQxzbTopStar >= 3
                     && af.nQxzbTopStar <= 7)
                {
                    QxzbTopStarTrans.gameObject.SetActive(true);

                    Transform starBgSpTrans = QxzbTopStarTrans.FindChild("StarBgObj/starBgSp");
                    if (starBgSpTrans != null)
                    {
                        UISprite spStarBg = starBgSpTrans.GetComponent<UISprite>();
                        if (spStarBg != null)
                        {
                            spStarBg.spriteName = UserFriend.friendStarTopBg[af.nQxzbTopStar];
                        }
                    }

                    Transform topStarTrans = QxzbTopStarTrans.FindChild("StarRank");
                    if (topStarTrans != null)
                    {
                        UISprite spTopStar = topStarTrans.GetComponent<UISprite>();
                        if (spTopStar != null)
                        {
                            spTopStar.spriteName = UserFriend.friendStarTop[af.nQxzbTopStar];
                        }
                    }
                }
                else
                {
                    QxzbTopStarTrans.gameObject.SetActive(false);
                }

            }
		items.Add(newItem);
	}
	
	public List<AssistFriend>  CalculateShowItems()
    {
		assistanceList.Sort(CompareTo);
		return assistanceList;
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
		foreach(GameObject item in items)
		{
            if (item != null)
            {
                item.transform.parent = null;
                Destroy(item);	
            }
		}
		items.Clear();
	}
	
	void OnDisable()
	{
		resetItems();
		if(assistanceList!=null)
		{
			assistanceList.Clear();
		}
		friendGuid=0;
		Debug.Log("xlym:selectAssist Complete!!!");
	}
	
	//排序算法
    static public int CompareTo(AssistFriend cardA, AssistFriend cardB)
    {
		//排序顺序按照 先侠义点数 再品质的方式进行
		if(cardB.friendShipNum!=cardA.friendShipNum)
		{
			return cardB.friendShipNum.CompareTo(cardA.friendShipNum);
		}
		else
		{
			return TableManager.GetCardByID(cardB.cardTempleId).Star.CompareTo(TableManager.GetCardByID(cardA.cardTempleId).Star);
		}
    }
	
	public void updateBattleArray(AssistFriend af)
	{
		Debug.Log("xlym:update select Guid:"+af.guid);
		//TODO:update 	Obj_MyselfPlayer.GetMe().battleArray
        Obj_MyselfPlayer.GetMe().LoadbattleArray();

		long[] array = Obj_MyselfPlayer.GetMe().battleArray;
        Debug.Log("Obj_MyselfPlayer.GetMe().nfightFriendPos : " + Obj_MyselfPlayer.GetMe().nfightFriendPos);
		Obj_MyselfPlayer.GetMe().DebugBattleArray();
        if (!Obj_MyselfPlayer.GetMe().CheckSetState())
        {
          	Obj_MyselfPlayer.GetMe().nfightFriendPos = Obj_MyselfPlayer.GetMe().GetTeamFightNum();
        }
		else if(Obj_MyselfPlayer.GetMe().CheckSetState() && Obj_MyselfPlayer.GetMe().nfightFriendPos < 0)
		{
			 for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == -1)
                {
                    //af.cardGuiId = 100000000;//服务器发来的好友卡牌GUID有问题--
                    array[i] = af.cardGuiId;
					Debug.Log("xlym:update select cardGuiId:"+af.cardGuiId);
                    //之前没有设置过，助战好友位置
                    Obj_MyselfPlayer.GetMe().nfightFriendPos = i;
                    break;
                }

            }
		}
        else
        {
            array[Obj_MyselfPlayer.GetMe().nfightFriendPos] = af.cardGuiId;
			Debug.Log("xlym:update select cardGuiId:"+af.cardGuiId);
        }
		Debug.Log("wo yun 5:");
        Obj_MyselfPlayer.GetMe().SavebattleArray();
		Obj_MyselfPlayer.GetMe().DebugBattleArray();
	}
	
	public void SelectCardInfo(GameObject item)
	{
		if(!GuideManager.Instance.isEnd()){
			return;
		}
		foreach(AssistFriend af in assistanceList)
		{
			if(af.guid == long.Parse(item.name))
			{
				UserCardItem card=new UserCardItem();
				card.addQualityAtt=af.attackTimes;
				card.addQualityHp=af.hpTimes;
				card.cardID=af.cardGuiId;
				card.level=af.cardLevel;
				card.quality=TableManager.GetCardByID(af.cardTempleId).Star;
				card.skillLevel=af.skillLevel;
				card.templateID=af.cardTempleId;
                card.skillStudyId = af.studySkillId;
                card.skillStudyLev = af.studySkillLev;
				BoxManager.showCardInfoMessage(card);
				break;
			}
		}
	}
	
	public void SelectAssistHero(GameObject item)
	{
		switch(GuideManager.Instance.currentStep){
		case GuideManager.GUIDE_STEP.COPY1_1:
			GuideCopy1_1.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY1_2:
			GuideCopy1_2.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY1_3:
			GuideCopy1_3.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY1_4:
			GuideCopy1_4.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY1_5:
			GuideCopy1_5.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		case GuideManager.GUIDE_STEP.COPY2_1:
			GuideCopy2_1.Instance.NextStep();//新手战斗引导 SELECT_4
			break;
		default:
			break;
		}
		foreach(AssistFriend af in assistanceList)
		{
			if(af.guid == long.Parse(item.name))
			{
				Obj_MyselfPlayer.GetMe().currentAssistFriend = af;
				updateBattleArray(af);
				break;
			}
		}
		logicTarget.SendMessage("OnBattleBefore");
//			newItem.name = card.cardID.ToString();
//			
//			UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
//			icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).GetIconbyIndex( (int)CardIconType.ICON_HEAD );
//			
//			UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
//			int nameLangId = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).Name;
//            nameLabel.text = LanguageManger.GetWords(nameLangId);
//			
//			UILabel lifeLabel = newItem.transform.FindChild("Labels/Label-Hp-Value").GetComponent<UILabel>();
//         	lifeLabel.text = (TableManager.GetCardByID(card.templateID).HpBase + TableManager.GetCardByID(card.templateID).HpGrow * card.level).ToString();			
//
//			UILabel attackLabel = newItem.transform.FindChild("Labels/Label-Attack-Value").GetComponent<UILabel>();
//           	attackLabel.text = (TableManager.GetCardByID(card.templateID).AttackBase + TableManager.GetCardByID(card.templateID).AttackGrow * card.level).ToString();
//			
//			UILabel levelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
//       		levelLabel.text = card.level.ToString(
	}
	public void backToPreviousWindow()
	{
		logicTarget.SendMessage("LoadPveBossList");
	}

//	public void OnBtnUpdate()
//	{
//		materialList.Clear();
//		materialNum = 0;
//		EnalbleGrid((int)GRID_TYPE.UPDATE);
//		backButton.transform.GetComponent<UIButtonMessage>().functionName = "ReturnToMainUI";
//		transform.FindChild("Label-Title").GetComponent<UILabel>().text = "选择英雄";
//	}
//	
//	public void OnBtnMaterial()
//	{
//		EnalbleGrid((int)GRID_TYPE.MATERIAL);
//		backButton.transform.GetComponent<UIButtonMessage>().functionName = "onUpdateWindow";
//		transform.FindChild("Label-Title").GetComponent<UILabel>().text = "选择合成卡牌";
//	}
//	
//	public void OnBtnEvolution()
//	{
//		EnalbleGrid((int)GRID_TYPE.EVOLUTION);
//		backButton.transform.GetComponent<UIButtonMessage>().functionName = "ReturnToMainUI";
//		transform.FindChild("Label-Title").GetComponent<UILabel>().text = "选择英雄";
//	}
	
		//for guide
	private GameObject guideItem = null;
	public GameObject getGuideItem()
	{
		return guideItem;	
	}
	public void SetPanelListEnable(bool bEnable){
		dragPanel.enabled = bEnable;
	}
}
