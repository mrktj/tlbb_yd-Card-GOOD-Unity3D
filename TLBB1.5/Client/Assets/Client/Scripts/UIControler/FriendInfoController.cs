using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;
public class FriendInfoController : MonoBehaviour {
	
	public UILabel labelFriendName;
	public UILabel labelFriendID;
	//public UILabel labelReceivePowerData;
	//public UILabel labelSendPowerData;
	

	//card
	public UITexture cardBigIcon;
	public UISprite cardFrame;
	public UISprite cardOutside;
	
	public GameObject mainLogic;
	
	public GameObject sendButton;
	public GameObject acceptButton;
	public GameObject sendMail;
	
	public UISprite category;
		
	private string friendID;
	UserFriend uf;
	// Use this for initialization
	void Start () {
		mainLogic = GameObject.Find("MainUILogic");
	}
	
	void OnEnable()
	{
		if(Obj_MyselfPlayer.GetMe().currentFriend==null)
		{
			return;
		}
		uf = Obj_MyselfPlayer.GetMe().currentFriend;
		labelFriendName.text = uf.name;
		labelFriendID.text = "ID："+uf.guid.ToString();
		/*
		 * 2013-7-27 Jack Wen 
		 * 策划删除接收体力与发送体力时间提示
		labelReceivePowerData.text = "好友在"+uf.acceptPowerDay.ToString()+"天前接受了你的体力";//uf.acceptPowerDay.ToString();
		*/
		//-------------------------------------------------------------------------------
		//2013-5-27
		if(uf.canGivePower == true){
			sendButton.transform.GetComponent<UIButton>().isEnabled = true;	
			sendButton.transform.FindChild("Label").GetComponent<UILabel>().text = "赠送体力";
		}else{
			sendButton.transform.GetComponent<UIButton>().isEnabled = false;
			sendButton.transform.FindChild("Label").GetComponent<UILabel>().text = "今日已赠送";
		}
		//-------------------------------------------------------------------------------
//		Debug.Log("uf.cardTempletID="+uf.cardTempletID);
//		Debug.Log("Appearance="+TableManager.GetCardByID(uf.cardTempletID).Appearance);
//		Debug.Log("spriteName"+TableManager.GetAppearanceByID(TableManager.GetCardByID(uf.cardTempletID).Appearance).BodyIcon);
		if(uf.cardTempletID > 0)
		{
			AtlasManager.Instance.setBodyByTempletID(cardBigIcon,uf.cardTempletID);///.SetBodyName(cardBigIcon,TableManager.GetAppearanceByID(TableManager.GetCardByID(uf.cardTempletID).Appearance).BodyIcon);;//cardBigIcon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(uf.cardTempletID).Appearance).BodyIcon;//
			
			UILabel friendLevelLabel = transform.FindChild("Labels/Label-Player-Level-Value").GetComponent<UILabel>();
			friendLevelLabel.text = uf.level.ToString();
			UILabel cardLevelLabel = transform.FindChild("Labels/Label-Card-Level-Value").GetComponent<UILabel>();
			cardLevelLabel.text = uf.cardLevel.ToString();

            if (uf.cardTempletID > 0)
            {
                cardFrame.spriteName = UserCardItem.cardFrameName[TableManager.GetCardByID(uf.cardTempletID).Star];
                category.spriteName = UserCardItem.elementTypeName[TableManager.GetCardByID(uf.cardTempletID).Element];
            	cardOutside.spriteName = UserCardItem.largeCardBorderName[TableManager.GetCardByID(uf.cardTempletID).Star];
			}
			category.MakePixelPerfect();
		}
		
		if(uf.canGetPower == true)
		{
			/*
			 * 2013-7-27 Jack Wen
			 * 策划删除接收体力与发送体力时间提示
			labelSendPowerData.text = "好友在"+uf.givePowerDay.ToString()+"天前赠送了你体力";//uf.givePowerDay.ToString();		
			*/	
			acceptButton.transform.FindChild("Label").GetComponent<UILabel>().text = "接受体力";
			acceptButton.transform.GetComponent<UIButton>().isEnabled = true;
		}else
		{
			//labelSendPowerData.text = "";		2013-7-27 Jack Wen
			//acceptButton.SetActive(false);
			//acceptButton.transform.FindChild("Label").GetComponent<UILabel>().text = "今日已接受";
			acceptButton.transform.GetComponent<UIButton>().isEnabled = false;
		}
	}
	
	void OnDisable()
	{
		Obj_MyselfPlayer.GetMe().currentFriend = null;
	}
	
	public void SendPower(){
		NetworkSender.Instance().giveFriendPower(SendPowerDone,uf.guid);
	}
	public void SendPowerDone(bool isSuccess){
		//sendButton.SetActive(false);
		
		BoxManager.showMessageByID((int)MessageIdEnum.Msg190, uf.name);
		sendButton.transform.GetComponent<UIButton>().isEnabled = false;
		sendButton.transform.FindChild("Label").GetComponent<UILabel>().text = "今日已赠送";
	}
	public void ReceivePower(){
		NetworkSender.Instance().getFriendPower(ReceivePowerDone,uf.guid);
	}
	public void ReceivePowerDone(bool isSuccess){
		//acceptButton.SetActive(false);
		//acceptButton.transform.FindChild("Label").GetComponent<UILabel>().text = "今日已接受";
        //您已获得{0}赠送的2点体力<n>今天还可接受{1}名好友的赠送哦
        string num = (10 - Obj_MyselfPlayer.GetMe().receive_power_time).ToString();
        BoxManager.showMessageByID((int)MessageIdEnum.Msg191, uf.name, num);
		acceptButton.transform.GetComponent<UIButton>().isEnabled = false;
	}
	/*
	public void SendChatContent(){
		BoxManager.showInputBoxMessage("请输入内容"); 
//		Messenger.AddListener<bool,string>("BOX",OnChatContentReady);
		UIEventListener.Get(BoxManager.getYesButton()).onClick += OnChatContentReady;
	}
	void OnChatContentReady(GameObject button)
	{
//		Messenger.RemoveListener<bool,string>("BOX",OnChatContentReady);
//		UIEventListener.Get(BoxManager.getYesButton()).onClick -= OnChatContentReady;
		OnChat(BoxManager.getInputText(),uf.guid);
		
	}
	void OnChat(string content,long friend_guid)
	{
		NetworkSender.Instance().sendChatContent(OnChatDone,content,friend_guid);
	}
	public void OnChatDone(bool isSuccess){
		//mainLogic.SendMessage("backToPreviousWindow");
	}
	*/
	public void DeleteFriend(){
		BoxManager.showMessageByID((int)MessageIdEnum.Msg70);
		UIEventListener.Get(BoxManager.buttonYes).onClick += DeleteFriendConfirm;
		//NetworkSender.Instance().deleteFriend(DeleteFriendDone,uf.guid);
	}
	public void DeleteFriendDone(bool isSuccess){
		mainLogic.SendMessage("OnFriendWindow");
	}
	public void OnSendMailWindow()
	{
		mainLogic.GetComponent<MainUILogic>().sendMailState = 1;
		mainLogic.GetComponent<MainUILogic>().friendInfo=uf;
		mainLogic.GetComponent<MainUILogic>().OnSendMailWindow();//mainuilogic里面没有这个方法
	}
	public void backToPreviousWindow()
	{
		//2013-7-30 Jack Wen
		//mainLogic.SendMessage("backToPreviousWindow");
		mainLogic.SendMessage("OnFriendWindow");
	}
	//-----------------------------------------------------------------
	//2013-7-26 Jack Wen
	public void DeleteFriendConfirm(GameObject button){
		NetworkSender.Instance().deleteFriend(DeleteFriendDone,uf.guid);
	}

	//-----------------------------------------------------------------
}
