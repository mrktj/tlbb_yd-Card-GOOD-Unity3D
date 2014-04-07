using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using card.net;
using Games.CharacterLogic;
using GCGame.Table;

public class PvPShopControl : MonoBehaviour {
	
	private const string itemName = "PvPShopItem";
	public UILabel pvpScore;
	public GameObject listParent;
	private List<GameObject> itemList;
	private GameObject logicTarget;

	private int	HP_ITEM = 0;
	private int nCurBuyItemID = 0;
	
	
	void Awake()
	{
		itemList = new List<GameObject>();
		logicTarget = GameObject.Find("MainUILogic");
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable()
	{
		this.FreshScoreRet(true);
		//NetworkSender.Instance().FreshPvPShopInfo(FreshScoreRet);
	}
	
	void OnDisable()
	{
		this.ClearItem();
	}
	
	
	void ClearItem()
	{
		if(itemList != null)
		{
			foreach(GameObject item in itemList)
			{
				CardListItemPool.Instance.DestroyItemAndPushToPool(item, itemName);	
			}
			
			itemList.Clear();
		}	
	}
	
	public void FreshScore()
	{
		NetworkSender.Instance().FreshPvPShopInfo(FreshScoreRet);
	}
	
	//显示积分商店Item
	private void ShowPvPShopItem()
	{
		
		int nLen = TableManager.GetPvpShop().Count;
		for(int i=1; i<=nLen; i++)
		{
			GameObject item = CardListItemPool.Instance.GetListItem(itemName);
			item.transform.parent = listParent.transform;
			item.transform.localPosition = new Vector3(0,0,0);
			item.transform.localScale = new Vector3(1,1,1);
			item.name = i.ToString();
			
			UILabel socreLabel = item.transform.FindChild("Labels/Label-NeedScore-Value").GetComponent<UILabel>();
			int nScore = TableManager.GetPvpShopByID(i).Score;
			socreLabel.text = nScore.ToString();
			
			UILabel scoreLabelFront = item.transform.FindChild("Labels/Label-NeedScore").GetComponent<UILabel>();
			
			UILabel nameLabel = item.transform.FindChild("Labels/Label-Name").GetComponent<UILabel>();
			nameLabel.text = TableManager.GetPvpShopByID(i).Title;
			
			
			/*
			if(TableManager.GetPvpShopByID(i).Type == PvPItemEnum.HP_ITEM)
			{
				nameLabel.text = "强身丸";
			}
			else
			{
				nameLabel.text = "大力丸";
			}
			*/
			
			
			
			UILabel describLabel = item.transform.FindChild("Labels/Label-Describ").GetComponent<UILabel>();
			describLabel.text = TableManager.GetPvpShopByID(i).Description;
				

			
			UISprite icon = item.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
			
			if(TableManager.GetPvpShopByID(i).Type == HP_ITEM)
			{
				icon.spriteName = "qiangshendan";
			}
			else
			{
				icon.spriteName = "daliwan";
			}
			
			//nameLabel.text = TableManager.GetPvpShopByID(i).;
			
			GameObject Btn = item.transform.FindChild("Btn").gameObject;
			UIImageButton[] imgBtns = Btn.GetComponents<UIImageButton>();
			//UIImageButton imgBtn = Btn.GetComponent<UIImageButton>();
			
			if(nScore > Obj_MyselfPlayer.GetMe().nlTotalScore)
			{
				foreach(UIImageButton imgBtn in imgBtns)
				{
					imgBtn.isEnabled = false;
				}
				//imgBtn.isEnabled = false;
				socreLabel.color = Color.red;
				scoreLabelFront.color = Color.red;
				UIEventListener.Get(Btn).onClick = null;
			}
			else
			{
				foreach(UIImageButton imgBtn in imgBtns)
				{
					imgBtn.isEnabled = true;
				}
				socreLabel.color = Color.green;
				scoreLabelFront.color =Color.green;
				UIEventListener.Get(Btn).onClick = OnSelectPvPItem;
			}
			
			
			UIGrid grid = listParent.GetComponent<UIGrid>();
			grid.Reposition();
			grid.repositionNow = true;
				
			itemList.Add(item);
		}
		
	}
	
	//刷新积分数据 消息回调
	public void FreshScoreRet(bool bSucess)
	{
		pvpScore.text  = Obj_MyselfPlayer.GetMe().nlTotalScore.ToString();
		
		this.ClearItem();
		ShowPvPShopItem();
	}
	
	//选择PvPitem
	public void OnSelectPvPItem(GameObject go)
	{
		int nlPareID = int.Parse(go.transform.parent.name);
		nCurBuyItemID = nlPareID;
		NetworkSender.Instance().ChoosePvPItem(BuyPvPItemRet, nlPareID);
	}
	
	//购买
	public void BuyPvPItemRet(bool bSucess)
	{
		//刷新界面
		FreshScoreRet(true);
		
		switch(nCurBuyItemID)
		{
			case 1: BoxManager.showMessageByID((int)MessageIdEnum.Msg113); break;
			case 2: BoxManager.showMessageByID((int)MessageIdEnum.Msg114); break;
			case 3: BoxManager.showMessageByID((int)MessageIdEnum.Msg115); break;
			case 4: BoxManager.showMessageByID((int)MessageIdEnum.Msg116); break;
		}
	}
	
	//返回pvp界面
	public void ReturnToPvP()
	{
		if(logicTarget != null)
		{
			logicTarget.SendMessage("OnPVPWindow");
		}
	}
}
