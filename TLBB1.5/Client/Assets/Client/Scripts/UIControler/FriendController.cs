using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

using card.net;
public class FriendController : MonoBehaviour
{

    public GameObject gridFriend;
    public UILabel labelFriendValue;
    public UILabel labelPage;
    //	public UILabel labelIDValue;
    private string friendListItem = "FriendListItem";
    public GameObject mainLogic;

    public UIScrollBar src;
    public UIDraggablePanel dp;

    public GameObject noFriend;

    private List<UserFriend> friendList;

    private int friendNum = 0;
    private int friendMax = 0;

    //private int allPage = 1;
    //private int curPage = 1;

    const int eachPageNum = 20;
    private string friend_Key = "Friend_Key";
    private int currentPageCount = 0;
	public static bool isNeedRestore = false;

    List<GameObject> items = new List<GameObject>();

    //Transform 的缓存
    private Transform cachedTransform;

    void Awake()
    {
        cachedTransform = transform;

        //设置 items的获得函数
        BagItemsPage bagItempage = cachedTransform.GetComponent<BagItemsPage>();
        if (bagItempage != null)
        {
            bagItempage.onCalculateShowFriendsItemsFunction += CalculateShowItems;
            bagItempage.onDragonFinishedClearItemsFun += OnDragonFinishedClearItemsFun;
            bagItempage.onShowFriendItemFunction += ShowCardItem;

            bagItempage.onCalculateShowItemsFunction = null;
        }

        Debug.Log("FriendController Awake");
        //		for(int i=0; i<friendNum; i++)
        //		{
        //			Obj_OtherPlayer friend = new Obj_OtherPlayer();
        //			friend.SetAccountName("MyFriend"+i);
        //			friendList.Add(friend);
        //		}
    }
    void OnEnable()
    {
        //		Debug.Log("FriendController OnEnable");

        //开始分页显示功能
        if (cachedTransform != null)
        {
            currentPageCount = 0;
            FreshBar();
            //cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages();
            friendList = Obj_MyselfPlayer.GetMe().friendsList;
            friendNum = friendList.Count;
            friendMax = Obj_MyselfPlayer.GetMe().friendNumMax;
            labelFriendValue.text = friendNum.ToString() + "/" + friendMax.ToString();
            if (friendNum <= 0)
                noFriend.SetActive(true);
            else
                noFriend.SetActive(false);
            //JackWen 玩家由好友信息转回则回到前页
        }
    }
    void OnDisable()
    {
        //		Debug.Log("FriendController OnDisable");
        int currentPage = transform.GetComponent<BagItemsPage>().GetCurrentPage();
        ScrollData scData = new ScrollData(src.scrollValue, currentPage);
        Obj_MyselfPlayer.GetMe().SetScrollValue(friend_Key, scData);
        foreach (GameObject obj in items)
        {
            Destroy(obj);
        }
        items.Clear();
    }
    // Use this for initialization
    void Start()
    {
        //		showWindows();
        //		Debug.Log("FriendController Start");
        mainLogic = GameObject.Find("MainUILogic");
    }
    void Update()
    {
        //FreashScroll();
    }

    public void FreshBar()
    {
        if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(friend_Key) && isNeedRestore)
        {
            cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages(Obj_MyselfPlayer.GetMe().scrollRecord[friend_Key].page);
            if (currentPageCount > 4)
            {
                src.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[friend_Key].scrollValue;
            }
            else
            {
                src.scrollValue = 0;
            }
			isNeedRestore = false;
        }
        else
        {
            cachedTransform.GetComponent<BagItemsPage>().StartBagItemPages(1);
            src.scrollValue = 0;
        }
    }

    //	public void SearchFriend()
    //	{
    //		UIInput fID = transform.FindChild("Input-ID").GetComponent<UIInput>();
    //		if(fID.text == "")
    //		{
    //			//输入账号空
    //			return;
    //		}
    //		NetworkSender.Instance().SearchFriendsList(SearchFriendDone,long.Parse(fID.text));
    //	}
    //	public void SearchFriendDone(bool isSuccess)
    //	{
    //		if(isSuccess)
    //		{
    //			Obj_MyselfPlayer.GetMe().friendSearchResult
    //		}
    //	}
    //	
    public void showFriendDetail(GameObject item)
    {
        foreach (UserFriend uf in friendList)
        {
            if (uf.guid == long.Parse(item.name))
            {
                Obj_MyselfPlayer.GetMe().currentFriend = uf;
                break;
            }
        }
        mainLogic.SendMessage("LoadFriendInfoWindow");
    }

    public void ShowCardInfo(GameObject item)
    {
        //		GameObject prefab = logicTarget.GetComponent<MainUILogic>().childControllers[0];
        //		GameObject newItem = (GameObject)Instantiate(prefab);
        //		newItem.transform.parent = transform.parent;
        //		newItem.transform.localPosition = new Vector3(0, 0, -21);
        //		newItem.transform.localScale = new Vector3(1, 1, 1);
        //		newItem.SetActive(true);
        //		newItem.GetComponent<CardInfoController>().FreashCardInfo(long.Parse(go.transform.parent.parent.name));
        //Obj_MyselfPlayer.GetMe().selectedCardID = long.Parse(selectedItem.transform.parent.parent.name);
        //mainLogic.SendMessage("LoadCardInfoUI");
        foreach (UserFriend uf in friendList)
        {
            if (uf.guid == long.Parse(item.transform.parent.parent.name))
            {
                Obj_MyselfPlayer.GetMe().currentFriend = uf;
                break;
            }
        }
        BoxManager.showCardInfoMessage(Obj_MyselfPlayer.GetMe().currentFriend);
    }

    public void OnFriendAddWindow()
    {
        mainLogic.SendMessage("OnFriendAddWindow");
    }

    public void ReturnToMainUI()
    {
        mainLogic.SendMessage("ReturnToMainUI");
    }

    //---------------------------实现左右翻页部分----------------------
    //计算要显示
    public List<UserFriend> CalculateShowItems()
    {
        friendList = Obj_MyselfPlayer.GetMe().friendsList;
        //先把上阵和不上阵的分开来，再排序
        List<UserFriend> listOnlineArray = new List<UserFriend>();
        List<UserFriend> listNoOnlineArray = new List<UserFriend>();

        foreach (UserFriend friend in friendList)
        {
            if (friend.lastOnlineHours != -1)
            {
                listOnlineArray.Add(friend);
            }
            else
            {
                listNoOnlineArray.Add(friend);
            }
        }
        /* 1.按照在线分
         * 2.按照可领取体力分
         * 3.按照登陆时间分
         * 4.按照卡牌星级分*/
        listOnlineArray.Sort(CompareToOnline);
        listNoOnlineArray.Sort(CompareToOffLine);

        listOnlineArray.AddRange(listNoOnlineArray);

        return listOnlineArray;
    }
	//在线
    public int CompareToOnline(UserFriend a1, UserFriend a2)
    {
        /* 2.按照可领取体力分
         * 3.按照登陆时间分
         * 4.按照卡牌星级分*/
        if (a1.canGetPower != a2.canGetPower)
        {
            return a2.canGetPower.CompareTo(a1.canGetPower);
        }
        else if (a1.lastOnlineHours != a2.lastOnlineHours)
        {
            return (-1 * a2.lastOnlineHours.CompareTo(a1.lastOnlineHours));
        }
        else
        {
            if (a2.cardTempletID > 0 && a1.cardTempletID > 0)
            {
                return TableManager.GetCardByID(a2.cardTempletID).Star.CompareTo(TableManager.GetCardByID(a1.cardTempletID).Star);
            }
            else
            {
                return 0;
            }
            //return  .CompareTo(cardA.templateID);
        }
    }
	//离线
	 public int CompareToOffLine(UserFriend a1, UserFriend a2)
    {
        /* 2.按照可领取体力分
         * 3.按照登陆时间分
         * 4.按照卡牌星级分*/
        if (a1.canGetPower != a2.canGetPower)
        {
            return a2.canGetPower.CompareTo(a1.canGetPower);
        }
        else if (a1.lastLogoutHours != a2.lastLogoutHours)
        {
            return (-1 * a2.lastLogoutHours.CompareTo(a1.lastLogoutHours));
        }
        else
        {
            if (a2.cardTempletID > 0 && a1.cardTempletID > 0)
            {
                return TableManager.GetCardByID(a2.cardTempletID).Star.CompareTo(TableManager.GetCardByID(a1.cardTempletID).Star);
            }
            else
            {
                return 0;
            }
            //return  .CompareTo(cardA.templateID);
        }
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
                //item.transform.parent = null;
                //Destroy(item);	
                CardListItemPool.Instance.DestroyItemAndPushToPool(item, friendListItem);
            }
        }
        items.Clear();
    }

    public void ShowCardItem(UserFriend card)
    {
        if (card == null)
        {
            return;
        }

        //GameObject newItem =  ResourceManager.Instance.loadWidget(friendListItem);//(GameObject)Instantiate(friendListItem);
        GameObject newItem = CardListItemPool.Instance.GetListItem(friendListItem);
        newItem.transform.parent = gridFriend.transform;
        newItem.transform.localPosition = new Vector3(0, 0, -1);
        newItem.transform.localScale = new Vector3(1, 1, 1);
        newItem.name = card.guid.ToString();//friendList[i].GetAccountName();
		
		Transform QxzbTopStarTrans = newItem.transform.FindChild("QxzbTopStar");
		if(QxzbTopStarTrans != null)
		{
			//沒有群雄爭霸第一名
			if(card.nQxzbTopStar >= 3
				 && card.nQxzbTopStar <= 7)
			{
				QxzbTopStarTrans.gameObject.SetActive(true);
				
				Transform starBgSpTrans = QxzbTopStarTrans.FindChild("StarBgObj/starBgSp");
				if(starBgSpTrans != null)
				{
					UISprite spStarBg = starBgSpTrans.GetComponent<UISprite>();
					if(spStarBg != null)
					{
						spStarBg.spriteName = UserFriend.friendStarTopBg[card.nQxzbTopStar];
					}
				}
				
				Transform topStarTrans = QxzbTopStarTrans.FindChild("StarRank");
				if(topStarTrans != null)
				{
					UISprite spTopStar = topStarTrans.GetComponent<UISprite>();
					if(spTopStar != null)
					{
						spTopStar.spriteName = UserFriend.friendStarTop[card.nQxzbTopStar];
					}
				}
			}
			else
			{
				QxzbTopStarTrans.gameObject.SetActive(false);
			}
			
		}
		
        items.Add(newItem);
        currentPageCount++;
        bool success = newItem.GetComponent<UIFriendItemView>().InitWithUserFriend(card);

        if (success)
        {
            GameObject cib = newItem.GetComponent<UIFriendItemView>().GetCardIconBtn();
            UIEventListener.Get(cib).onClick += ShowCardInfo;
            UIEventListener.Get(newItem).onClick += showFriendDetail;
        }
        /*		
                UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
                nameLabel.text = card.name;//friendList[i].GetAccountName();
		
                if(card.cardTempletID > 0)
                {
                    //头像按钮显示卡牌信息
                    UIEventListener.Get(newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject).onClick += ShowCardInfo;
                    //------------------------卡牌背景及外框--------------------------------
                    //2013-10-12 Jack Wen
                    UISprite icon_bg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG").GetComponent<UISprite>();
                    UISprite icon_border = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite").GetComponent<UISprite>();
                    int icon_star = TableManager.GetCardByID(card.cardTempletID).Star;
                    icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
                    icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
                }
                //--------------------------------------------------------------------
                //五行显示
                UISprite sttributeIcon = newItem.transform.FindChild("Sprite-Attribut").GetComponent<UISprite>();
                int nAttributeID;
                if(card.cardTempletID > 0)
                {	
                    nAttributeID = TableManager.GetCardByID(card.cardTempletID).Element;
                }
                else
                {
                    nAttributeID = 0;   //默认显示金//
                }
                string strIconName = "";
                switch(nAttributeID)
                {
                    case 0: strIconName = "jin"; break;
                    case 1: strIconName = "mu"; break;
                    case 2: strIconName = "shui"; break;
                    case 3: strIconName = "huo"; break;
                    case 4: strIconName = "tu"; break;
                }
                sttributeIcon.spriteName = strIconName;
                //---------------------------------------------------------------------
                //2013-7-26 Jack Wen
                UILabel levelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
                   levelLabel.text = card.level.ToString();
                UILabel stateLabel = newItem.transform.FindChild("Labels/Label-State").GetComponent<UILabel>();
                int lastOnlineTime = card.lastOnlineHours;
                if(lastOnlineTime >= 0){
                    UIImageButton button = newItem.GetComponent<UIImageButton>();
                    button.normalSprite = "liebiao_beijing_1";
                    button.hoverSprite = "liebiao_beijing_2";
                    button.pressedSprite = "liebiao_beijing_3";
                    UISprite back = newItem.transform.FindChild("Sprite/Background").GetComponent<UISprite>();
                    back.spriteName = "liebiao_beijing_1";
                    back = newItem.transform.FindChild("Sprite/haoyoubeijing").GetComponent<UISprite>();
                    back.alpha = 1.0f;
                    stateLabel.text = "[a5e295]登录："+lastOnlineTime+"小时前";
                }
                else{
                    UIImageButton button = newItem.GetComponent<UIImageButton>();
                    button.normalSprite = "liebiao_beijing_3";
                    button.hoverSprite = "liebiao_beijing_3";
                    button.pressedSprite = "liebiao_beijing_3";
                    UISprite back = newItem.transform.FindChild("Sprite/Background").GetComponent<UISprite>();
                    back.spriteName = "liebiao_beijing_3";
                    back = newItem.transform.FindChild("Sprite/haoyoubeijing").GetComponent<UISprite>();
                    back.alpha = 0.5f;
                    stateLabel.text = "[222222]离线";
                }
                UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
                if(card.cardTempletID > 0)
                {
                    icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.cardTempletID).Appearance).HeadIcon;
                }
                //2013-7-29 Jack Wen
                GameObject gift = newItem.transform.FindChild("Gift").gameObject;
                if (card.canGetPower)
                {
                    gift.SetActive(true);
                    if (Obj_MyselfPlayer.GetMe().receive_power_time < 10)
                    {
                        gift.GetComponent<TweenAlpha>().style = UITweener.Style.PingPong;
                    }
                    else
                    {
                        gift.GetComponent<TweenAlpha>().style = UITweener.Style.Once;
                    }
                }
                else
                    gift.SetActive(false);
                //2013-8-2 Jack Wen
                //加入好友主卡牌星级显示
                if(card.cardTempletID > 0)
                {
                    Tab_Card tabCard = TableManager.GetCardByID(card.cardTempletID);
                    for (int start_i = 1; start_i < 8; start_i++)
                    {
                        if (start_i <= tabCard.Star)
                        {
                            newItem.transform.FindChild("StartManager/start_"+start_i).gameObject.SetActive(true);
                        }
                        else
                        {
                            newItem.transform.FindChild("StartManager/start_"+start_i).gameObject.SetActive(false);
                        }
                    }
                    UIEventListener.Get(newItem).onClick+=showFriendDetail;
                }
                //2013-8-6 Jack Wen
                //显示卡牌等级
                UILabel cardLevel = newItem.transform.FindChild("Sprite/Panel-Lv/Label-lv").GetComponent<UILabel>();
                cardLevel.text = card.cardLevel.ToString();
                //---------------------------------------------------------------------
        */
    }

    private void FreashScroll()
    {
        if (src.alpha == 0)
        {
            dp.scale.y = 0;
        }
        else
        {
            if (src.scrollValue == 0 || src.scrollValue == 1)
            {
                dp.scale.y = 0.1f;
            }
            else
            {
                dp.scale.y = 1;
            }
        }
    }
}
