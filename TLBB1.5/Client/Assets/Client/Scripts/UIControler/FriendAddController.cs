using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

using card.net;
public class FriendAddController : MonoBehaviour {

	public GameObject gridFriend;
	public UILabel labelFriendValue;
	public UILabel labelPage;
	public UILabel labelIDValue;
	public GameObject mainLogic;
	
	private List<UserFriend> friendRequsets;
	
	private int requestNum = 0;
	private int requestMax = 0;
	
	private int allPage = 1;
	private int curPage = 1;
	
	const int eachPageNum = 20;
	
	List<GameObject> items = new List<GameObject>();
	
	
	void Awake()
	{

//		for(int i=0; i<friendNum; i++)
//		{
//			Obj_OtherPlayer friend = new Obj_OtherPlayer();
//			friend.SetAccountName("MyFriend"+i);
//			friendList.Add(friend);
//		}
	}
	void OnEnable ()
	{
		showWindows();
	}
	void OnDisable()
	{
		clearItems();

	}
	// Use this for initialization
	void Start () {
//		showWindows();
		mainLogic = GameObject.Find("MainUILogic");
	}
	public void clearItems()
	{
		foreach(GameObject obj in items)
		{
			Destroy(obj);
		}
		items.Clear();
	}
	public void showWindows()
	{
//		Debug.Log("showWindow");
		labelIDValue.text = Obj_MyselfPlayer.GetMe().accountID.ToString();
		
		friendRequsets = new List<UserFriend>();//Obj_MyselfPlayer.GetMe().friendsList;//
		requestNum = friendRequsets.Count;
		requestMax = Obj_MyselfPlayer.GetMe().friendNumMax;
		labelFriendValue.text = requestNum.ToString()+"/"+requestMax.ToString();
//		Debug.Log("friendRequsets.Count:"+friendRequsets.Count);
//		Debug.Log("requestNum:"+requestNum);
		
		curPage = 1;
		if(requestNum<eachPageNum)
		{
			allPage = 1;
		}else
		{
			allPage = (requestNum%eachPageNum == 0) ? requestNum/eachPageNum : requestNum/eachPageNum+1;
		}
				
		labelPage.text = curPage + "/" + allPage;
		
		if(requestNum>0)
		{
			int startIdx = 0;
			int endIdx = 0;
			if(curPage == allPage)
			{
				startIdx = (curPage-1)*eachPageNum;
				endIdx = startIdx + (requestNum - (curPage-1)*eachPageNum);
			}else
			{
				startIdx = (curPage-1)*eachPageNum;
				endIdx = startIdx + eachPageNum;
			}
//			Debug.Log("startIdx:"+startIdx);
//			Debug.Log("endIdx:"+endIdx);
			
			for(int i=startIdx; i<endIdx; i++)
			{
				GameObject newItem =  ResourceManager.Instance.loadWidget("FriendAddItem");//(GameObject)Instantiate(friendListItem);
				newItem.transform.parent = gridFriend.transform;
				newItem.transform.localPosition = new Vector3(0, 0, -1);
				newItem.transform.localScale = new Vector3(1, 1, 1);			
				newItem.name = ((UserFriend)friendRequsets[i]).guid.ToString();//friendList[i].GetAccountName();
				
				//头像按钮显示卡牌信息
				newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject.name = ((UserFriend)friendRequsets[i]).guid.ToString();
				UIEventListener.Get(newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject).onClick += ShowCardInfo;
				
				UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
	           	nameLabel.text = ((UserFriend)friendRequsets[i]).name;//friendList[i].GetAccountName();
				
				Transform QxzbTopStarTrans = newItem.transform.FindChild("QxzbTopStar");
				if(QxzbTopStarTrans != null)
				{
					//沒有群雄爭霸第一名
					if(friendRequsets[i].nQxzbTopStar >= 3
						 && friendRequsets[i].nQxzbTopStar <= 7)
					{
						QxzbTopStarTrans.gameObject.SetActive(true);
						
						Transform starBgSpTrans = QxzbTopStarTrans.FindChild("StarBgObj/starBgSp");
						if(starBgSpTrans != null)
						{
							UISprite spStarBg = starBgSpTrans.GetComponent<UISprite>();
							if(spStarBg != null)
							{
								spStarBg.spriteName = UserFriend.friendStarTopBg[friendRequsets[i].nQxzbTopStar];
							}
						}
						
						Transform topStarTrans = QxzbTopStarTrans.FindChild("StarRank");
						if(topStarTrans != null)
						{
							UISprite spTopStar = topStarTrans.GetComponent<UISprite>();
							if(spTopStar != null)
							{
								spTopStar.spriteName = UserFriend.friendStarTop[friendRequsets[i].nQxzbTopStar];
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
		}
		gridFriend.GetComponent<UIGrid>().repositionNow = true;
	}
	
	public void SearchFriend()
	{
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn54).ToString());
		UIInput fID = transform.FindChild("Labels/Input-ID").GetComponent<UIInput>();
		if(fID.text == "")
		{
			//输入账号空
//			BoxManager.showMessage("请输入要查找玩家的ID");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg48);
			return;
		}
		if(fID.text.Contains(":"))
		{
			NetworkSender.Instance().GMCommand(GMCommandDone, fID.text);
		}
		else if(!CompID(fID.text))
		{
//			BoxManager.showMessage("请输入正确的玩家ID"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg50);
		}
		else 
		{
            string ID = (int.Parse(fID.text)).ToString();   //输入的ID前面的去0处理
            if (ID == Obj_MyselfPlayer.GetMe().accountID.ToString())
            {
                //输入账号为玩家ID
                //BoxManager.showMessage("无法添加自己为好友"); 
                BoxManager.showMessageByID((int)MessageIdEnum.Msg49);
                return;
            }
            else
            {
                NetworkSender.Instance().SearchFriendsList(SearchFriendDone, long.Parse(ID));
            }
		}

	}
	private bool CompID(string playerID){
		int n = playerID.Length;
		for(int i=0; i<n; i++)
		{
			if(playerID[i] > '9' || playerID[i] < '0')
				return false;
		}
		return true;
	}
	public void SearchFriendDone(bool isSuccess)
	{
		clearItems();
			
		GameObject newItem =  ResourceManager.Instance.loadWidget("FriendAddItem");//(GameObject)Instantiate(friendListItem);
		newItem.transform.parent = gridFriend.transform;
		newItem.transform.localPosition = new Vector3(0, 0, -1);
		newItem.transform.localScale = new Vector3(1, 1, 1);			
		newItem.name = Obj_MyselfPlayer.GetMe().friendSearchResult.guid.ToString();//friendList[i].GetAccountName();
			
		UIEventListener.Get(newItem.transform.FindChild("ApplyBtn").gameObject).onClick += friendAdd;
		
		Transform QxzbTopStarTrans = newItem.transform.FindChild("QxzbTopStar");
		if(QxzbTopStarTrans != null)
		{
			//沒有群雄爭霸第一名
			if(Obj_MyselfPlayer.GetMe().friendSearchResult.nQxzbTopStar >= 3
				 && Obj_MyselfPlayer.GetMe().friendSearchResult.nQxzbTopStar <= 7)
			{
				QxzbTopStarTrans.gameObject.SetActive(true);
				
				Transform starBgSpTrans = QxzbTopStarTrans.FindChild("StarBgObj/starBgSp");
				if(starBgSpTrans != null)
				{
					UISprite spStarBg = starBgSpTrans.GetComponent<UISprite>();
					if(spStarBg != null)
					{
						spStarBg.spriteName = UserFriend.friendStarTopBg[Obj_MyselfPlayer.GetMe().friendSearchResult.nQxzbTopStar];
					}
				}
				
				Transform topStarTrans = QxzbTopStarTrans.FindChild("StarRank");
				if(topStarTrans != null)
				{
					UISprite spTopStar = topStarTrans.GetComponent<UISprite>();
					if(spTopStar != null)
					{
						spTopStar.spriteName = UserFriend.friendStarTop[Obj_MyselfPlayer.GetMe().friendSearchResult.nQxzbTopStar];
					}
				}
			}
			else
			{
				QxzbTopStarTrans.gameObject.SetActive(false);
			}
			
		}
		
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		////templeID < 0 情况的容错处理,暂时显示默认头像，且错误头像点击不给予反映//
		if(Obj_MyselfPlayer.GetMe().friendSearchResult.cardTempletID > 0)
		{
			UISprite icon_bg = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-BG").GetComponent<UISprite>();
			UISprite icon_border = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite").GetComponent<UISprite>();
			int icon_star = TableManager.GetCardByID(Obj_MyselfPlayer.GetMe().friendSearchResult.cardTempletID).Star;
			icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
			icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
			UIEventListener.Get(newItem.transform.FindChild("CardIcon/CardIconBtn").gameObject).onClick += ShowCardInfo;
		}
		//--------------------------------------------------------------------
		UILabel nameLabel = newItem.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
	       nameLabel.text = Obj_MyselfPlayer.GetMe().friendSearchResult.name;//friendList[i].GetAccountName();
		//---------------------------------------------------------------------
		//2013-7-26 Jack Wen
		UILabel levelLabel = newItem.transform.FindChild("Labels/Label-Level-Value").GetComponent<UILabel>();
	       levelLabel.text = Obj_MyselfPlayer.GetMe().friendSearchResult.level.ToString();
		UILabel stateLabel = newItem.transform.FindChild("Labels/Label-State").GetComponent<UILabel>();
	    int lastOnlineTime = Obj_MyselfPlayer.GetMe().friendSearchResult.lastOnlineHours;
		int lastLogoutTime = Obj_MyselfPlayer.GetMe().friendSearchResult.lastLogoutHours;
		if(lastOnlineTime == -1 && lastLogoutTime >= 0){
			UIImageButton button = newItem.GetComponent<UIImageButton>();
			button.normalSprite = "liebiao_beijing_3";
			button.hoverSprite = "liebiao_beijing_3";
			button.pressedSprite = "liebiao_beijing_3";
			UISprite back = newItem.transform.FindChild("Sprite/Background").GetComponent<UISprite>();
			back.spriteName = "liebiao_beijing_3";
			back = newItem.transform.FindChild("Sprite/haoyoubeijing").GetComponent<UISprite>();
			back.alpha = 0.5f;
			if( lastLogoutTime / 60 >= 24)
				stateLabel.text = "[222222]离线："+lastLogoutTime/60/24+"天";
			else if( lastLogoutTime / 60 < 24 && lastLogoutTime / 60 > 0)
				stateLabel.text = "[222222]离线："+lastLogoutTime/60 + "小时";
			else
				stateLabel.text = "[222222]离线："+lastLogoutTime%60+"分钟";
		}
		else if(lastOnlineTime >= 0 && lastLogoutTime == -1){
			stateLabel.text = "[a5e295]登陆："+lastOnlineTime/60+"小时前";
		}
		
		//头像按钮显示卡牌信息
		//2013-7-30 Jack Wen
		
		//五行显示
        UISprite sttributeIcon = newItem.transform.FindChild("Sprite-Attribut").GetComponent<UISprite>();
		string strIconName = "";
		if(Obj_MyselfPlayer.GetMe().friendSearchResult.cardTempletID > 0)
		{
	        int nAttributeID = TableManager.GetCardByID(Obj_MyselfPlayer.GetMe().friendSearchResult.cardTempletID).Element;
	        switch(nAttributeID)
	        {
	            case 0: strIconName = "jin"; break;
	            case 1: strIconName = "mu"; break;
	            case 2: strIconName = "shui"; break;
	            case 3: strIconName = "huo"; break;
	            case 4: strIconName = "tu"; break;
	        }
		}
		else
		{
			strIconName = "jin";
		}
        sttributeIcon.spriteName = strIconName;
		UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
		icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(Obj_MyselfPlayer.GetMe().friendSearchResult.cardTempletID).Appearance).HeadIcon;
		//2013-8-2 Jack Wen
		//加入好友主卡牌星级显示
		if(Obj_MyselfPlayer.GetMe().friendSearchResult.cardTempletID > 0)
		{
			Tab_Card tabCard = TableManager.GetCardByID(Obj_MyselfPlayer.GetMe().friendSearchResult.cardTempletID);
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
		}
		//2013-8-6 Jack Wen
		//显示卡牌等级
		UILabel cardLevel = newItem.transform.FindChild("Sprite/Panel-Lv/Label-lv").GetComponent<UILabel>();
		cardLevel.text = Obj_MyselfPlayer.GetMe().friendSearchResult.cardLevel.ToString();
		//---------------------------------------------------------------------
		items.Add(newItem);

	}
	
	public void ShowCardInfo(GameObject item)
	{
		BoxManager.showCardInfoMessage(Obj_MyselfPlayer.GetMe().friendSearchResult);
	}
	
	//gm命令 call back
	public void GMCommandDone(bool isSuccess)
	{
		
	}
	//Call Back
	public void friendAdd(GameObject item)
	{
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn53).ToString());
		NetworkSender.Instance().ADDFriend(friendAddDone,Obj_MyselfPlayer.GetMe().friendSearchResult.guid);
	}
	public void friendAddDone(bool isSuccess)
	{
		mainLogic.SendMessage("backToPreviousWindow");
	}
	public void backToPreviousWindow()
	{
		mainLogic.SendMessage("OnFriendWindow");
		FriendController.isNeedRestore = true;
	}
//	
//	public void showFriendDetail(GameObject item)
//	{
//		foreach(UserFriend uf in friendList)
//		{
//			if(uf.guid == long.Parse(item.name))
//			{
//				Obj_MyselfPlayer.GetMe().currentFriend =  uf;
//				break;
//			}
//		}
//		mainLogic.SendMessage("LoadFriendInfoWindow");
//	}
}
