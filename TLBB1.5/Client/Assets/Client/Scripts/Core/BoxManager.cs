using UnityEngine;
using System;
using System.Collections;
using xjgame.message;
using Games.LogicObject;
using GCGame.Table;
using Games.CharacterLogic;
using card.net;
using card;

//using PBMessage;
/*
 *  弹出框管理类
 * 
 * 
 *
 *
 *
 */

public class BoxManager {
	
//	public static BoxManager Instance
//	{
//		get
//		{
//			if(!_instance)
//			{
//				_instance = GameObject.FindObjectOfType(typeof(BoxManager)) as BoxManager;
//				if(!_instance)
//				{
//					GameObject gm = new GameObject("BoxManager");
//					_instance = gm.AddComponent(typeof(BoxManager)) as BoxManager;
//				}
//			}
//			return _instance;			
//		}
//
//	}
	//type of box's prefab
//	public static GameObject waitingBox;
//	public static GameObject messageBox;
//	public static GameObject confirmBox;
//	public static GameObject processBox;
//	public static GameObject inputBox;
	public static MainUILogic mainLogic;
	
	public static GameObject topFrame;
	
	public enum MessageType{
		NONE,
		WaitingBox,
		MessageBox,
		ConfirmBox,
		ProcessBox,
		InputBox,
		CardInfoBox,
		ReviveBox,//战斗卡牌复活--
		BagFullBox, //背包已满指引
		IfGuideBox, //是否新手指引
		ChangeCardBox, //换卡用
	};
	
	public static GameObject buttonShop;
	public static GameObject buttonUpdate;
	public static GameObject buttonSell;
	
	public static GameObject buttonYes;
	public static GameObject buttonNo;
	public static string inputText;
	public static ErrorType currentErrorType;
	const int box_z = -600;
	public static void showWaitingBox(string label)
	{
//		if(waitingBox==null)
//		{
//			waitingBox = Resources.Load("WaitingBox") as GameObject;
//		}
		removeMessage();
		MessageType type = MessageType.WaitingBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(waitingBox);
		topFrame.transform.parent =  Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		//-200,waiting box应该是在最前层
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(type,label);
		buttonYes = mbc.buttonYes;
		buttonNo = mbc.buttonNo;
	}
	
	//根据type显示不同Message
	public static void show (int type, string content, string title)
	{
//		title += "WML Test";
		if (content.Contains("<n>"))
			content = content.Replace("<n>","\n");
		
		switch (type)
		{
		case 0:
			BoxManager.showMessage(content,title);
			break;
		case 1:
			BoxManager.showConfirmMessage(content,title);
			break;
		case 2:
			BoxManager.showBagFullBox(content,title);
			break;
		case 3:
			BoxManager.showIfGuideBox(content,title);
			break;
		case 4:
			BoxManager.showProcessMessage(content);
			break;
		case 5:
			BoxManager.showWaitingBox(content);
			break;
		case 9:
			BoxManager.showIfGuideBox(content,title);
			break;
		case 6:
		case 7:
		case 8:
		case 10:
		case 11:
		case 13: 
			BoxManager.showCustomConfirmBox(content,title,type);
			break;
		case 12:
			BoxManager.showChangeCardBox(content,title);
			break;
		default:
			BoxManager.showMessage(content,title);
			break;
		}	
	}
	
	public static void showMessageByID(int id,string arg)
	{
		Tab_Popup msg = TableManager.GetPopupByID(id);
		if (msg == null)
		{
			Debug.LogError("Msg is Null ID: " + id);
			return;
		}
		string content = string.Format(msg.Content,arg);
		show(int.Parse(msg.Type),content,msg.Title);
	}
	
	public static void showMessageByID(int id, string arg1, string arg2)
	{
		Tab_Popup msg = TableManager.GetPopupByID(id);
		if (msg == null)
		{
			Debug.LogError("Msg is Null ID: " + id);
			return;
		}
		string content = string.Format(msg.Content,arg1,arg2);
		show(int.Parse(msg.Type),content,msg.Title);
	}
	
	public static void showMessageByID(int id)
	{
		Tab_Popup msg = TableManager.GetPopupByID(id);
		if (msg == null)
		{
			Debug.LogError("Msg is Null ID: " + id);
			return;
		}
		string content = msg.Content;
		show(int.Parse(msg.Type),content,msg.Title);
	}
	
	public static void showChangeCardBox(string label, string titleStr)
	{
		removeMessage();
		MessageType type = MessageType.ChangeCardBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(confirmBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(type,label,titleStr);
		buttonShop = mbc.buttonShop;
		buttonYes = mbc.buttonYes;
		buttonNo = mbc.buttonNo;
		UIEventListener.Get(buttonShop).onClick += GoProtectCard;
	}
	
	public static void showCustomConfirmBox(string label, string titleStr, int type)
	{
		removeMessage();
		MessageType boxType = MessageType.ConfirmBox;
		topFrame = ResourceManager.Instance.LoadPopUp(boxType.ToString());//(GameObject)GameObject.Instantiate(confirmBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(boxType,label,titleStr);
		buttonYes = mbc.buttonYes;
		buttonNo = mbc.buttonNo;
		UISprite buttonSprite = buttonYes.transform.FindChild("Sprite").GetComponent<UISprite>();
		switch (type)
		{
		case 6:
			buttonSprite.spriteName = "chongzhi";
			UIEventListener.Get(buttonYes).onClick += GoRecharge;
			break;
		case 7:
			buttonSprite.spriteName = "shangpu_anniu_shenqing_1";
			UIEventListener.Get (buttonYes).onClick += GoShop;
			break;
		case 8:
			buttonSprite.spriteName = "chuangdangwulin";
			UIEventListener.Get (buttonYes).onClick += GoPVEWindow;
			break;
		case 10:
			buttonSprite.spriteName = "cancelprotect_1";
			UIEventListener.Get (buttonYes).onClick += GoHeroInfoController;
			break;
		case 11:
			buttonSprite.spriteName = "withdraw_1";
			UIEventListener.Get (buttonYes).onClick += GoSelectHeroController;
			break;
		case 13: 
			buttonSprite.spriteName = "exchange_1";
			break;
		default :
			break;
		}
	}
	
	public static void GoHeroInfoController(GameObject button)
	{
//		mainLogic.gameObject.SendMessage("LoadCardInfoUI");
	}
	
	public static void GoSelectHeroController(GameObject button)
	{
		if (Obj_MyselfPlayer.GetMe().materialChangeCard != null)
		{
			if (Obj_MyselfPlayer.GetMe().materialChangeCard.IsInFightArray())
			{
				if (Obj_MyselfPlayer.GetMe().materialChangeCard.cardID == Obj_MyselfPlayer.GetMe().battleArray[0])
					mainLogic.LoadSelectHeaderWindow();
				else
					mainLogic.LoadSelectMemberWindow();
			}
				
		}
	}
	
	public static void showMessage(string label, string titleStr)
	{
		
//		if(messageBox==null)
//		{
//			messageBox = Resources.Load("MessageBox") as GameObject;
//		}
		removeMessage();
//		Debug.LogError("showMessage:"+label);
		MessageType type = MessageType.MessageBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(messageBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(type,label,titleStr);
		buttonYes = mbc.buttonYes;
		buttonNo = mbc.buttonNo;
	}
	
	//显示 选择是否新手指引 的弹窗
	public static void showIfGuideBox (string label, string titleStr)
	{
		removeMessage();
		MessageType type = MessageType.IfGuideBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(confirmBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(type,label,titleStr);
		buttonYes = mbc.buttonYes;
		buttonNo = mbc.buttonNo;
	}
	
	//王明磊 - 背包已满 弹窗指引
	public static void showBagFullBox (string label, string titleStr)
	{
		removeMessage();
		MessageType type = MessageType.BagFullBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(confirmBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(type,label,titleStr);
		buttonShop = mbc.buttonShop;
		buttonUpdate = mbc.buttonUpdate;
		buttonSell = mbc.buttonSell;
		buttonNo = mbc.buttonNo;
	}
	
	public static void showConfirmMessage(string label, string titleStr)
	{
//		if(confirmBox==null)
//		{
//			confirmBox = Resources.Load("ConfirmBox") as GameObject;
//		}
		removeMessage();
		MessageType type = MessageType.ConfirmBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(confirmBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(type,label,titleStr);
		buttonYes = mbc.buttonYes;
		buttonNo = mbc.buttonNo;
	}
	public static void showInputBoxMessage(string label)
	{
//		if(confirmBox==null)
//		{
//			confirmBox = Resources.Load("InputBox") as GameObject;
//		}
		removeMessage();
		MessageType type = MessageType.InputBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(confirmBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);//.transform;//Translate(Vector3.back*box_z,Space.Self);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(type,label);
		buttonYes = mbc.buttonYes;
		buttonNo = mbc.buttonNo;
	}
	
	public static void showProcessMessage(string label)
	{
		removeMessage();
		MessageType type = MessageType.ProcessBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(processBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.init(type,label);
		buttonYes = mbc.buttonYes;
		buttonNo = mbc.buttonNo;
	}
	public static void showCardInfoMessage(int card_guid,int card_templateid)
	{
		removeMessage();
		MessageType type = MessageType.CardInfoBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(processBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.initCardInfo(type,card_guid,card_templateid);
//		buttonYes = mbc.buttonYes;
//		buttonNo = mbc.buttonNo;
	}
	public static void showCardInfoMessage(UserCardItem cardInfo)
	{
		removeMessage();
		MessageType type = MessageType.CardInfoBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(processBox);
        topFrame.transform.parent = Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.initCardInfo(type,cardInfo);
//		buttonYes = mbc.buttonYes;
//		buttonNo = mbc.buttonNo;
	}


	//-----------------------------------------------------------------------------
	//2013-7-29 Jack Wen
	public static void showCardInfoMessage(UserFriend uf)
	{
		removeMessage();
		MessageType type = MessageType.CardInfoBox;
		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(processBox);
		topFrame.transform.parent =  Camera.main.transform;
		topFrame.transform.localScale = Vector3.one;
		topFrame.transform.localPosition = new Vector3(0,0,box_z);
		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
		mbc.initCardInfo(type,uf);
//		buttonYes = mbc.buttonYes;
//		buttonNo = mbc.buttonNo;
	}
	

	//-----------------------------------------------------------------------------
	public static void showErrorMessage(int/*PBMessage.SCErrorMsg.ErrorType*/ type)
	{
		
		switch((ErrorType)type)
		{
		case ErrorType.FRIEND_ADD://0
//			BoxManager.showMessage("您已向该玩家提出好友申请");   
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg1);
			BoxManager.showMessageByID((int)MessageIdEnum.Msg147);
			break;
		case ErrorType.ACCEPT_POWER://3
//			BoxManager.showMessage("您的体力已满");    
			BoxManager.showMessageByID((int)MessageIdEnum.Msg15);
			break;
		case ErrorType.SEARCH_FRIEND_NOT_FOUND://5
//			BoxManager.showMessage("未找到玩家");    
			BoxManager.showMessageByID((int)MessageIdEnum.Msg3);
			break;
		case ErrorType.SEARCH_FRIEND_ALREADY_MY_FRIEND://6
//			BoxManager.showMessage("该玩家已经是我的好友");    
			BoxManager.showMessageByID((int)MessageIdEnum.Msg4);
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg147);
			break;
		case ErrorType.LOGIN_VERSION_WRONG://7
//			BoxManager.showMessage("客户端版本需要更新");   
			BoxManager.showMessageByID((int)MessageIdEnum.Msg5);
            UIEventListener.Get(buttonYes).onClick += GotoVersionUpdate;
			break;
		case ErrorType.LOGIN_NAME_SAME://8
//			BoxManager.showMessage("已有同名玩家"); 
				Debug.LogWarning("LOGIN_NAME_SAME");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg6);
			break;
        case ErrorType.TASK_LIST://9
//            BoxManager.showMessage("获取任务列表错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg75);
            break;
        case ErrorType.TASK_FINISH://10
//            BoxManager.showMessage("获取任务奖励错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg76);
            break;
        case ErrorType.CARD_COMBINE://11
//            BoxManager.showMessage("卡牌合成错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg77);
			UIEventListener.Get(buttonYes).onClick += ContinueShow;
            break;
        case ErrorType.CARD_EVOLVE://12
//            BoxManager.showMessage("卡牌进化错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg78);
			//UIEventListener.Get(buttonYes).onClick += ContinueShow;
            break;
        case ErrorType.CARD_STRENGTHEN://13
//            BoxManager.showMessage("卡牌强化错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg79);
			//UIEventListener.Get(buttonYes).onClick += ContinueShow;
            break;
		case ErrorType.FRIEND_MAIL_FULL://14
//            BoxManager.showMessage("邮件不存在"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg80);
			break;
        case ErrorType.MAIL_NOT_EXIST://15
//          BoxManager.showMessage("对方收件箱已满，邮件发送失败"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg7);
            break;
        case ErrorType.MAIL_FRIEND_NOT_EXIST://16
//            BoxManager.showMessage("要发送的好友不存在"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg81);
            break;
        case ErrorType.MAIL_ITEM_DONE://17
//            BoxManager.showMessage("物品已经领取完毕");  
			BoxManager.showMessageByID((int)MessageIdEnum.Msg82);
            break;
        case ErrorType.COMBINE_SWALLOWED_CARDLIST://18
//            BoxManager.showMessage("吞噬卡牌组错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg83);
            break;
        case ErrorType.COMBINE_SWALLOWED_NOTEXIST://19
//            BoxManager.showMessage("吞噬卡牌不存在");  
			BoxManager.showMessageByID((int)MessageIdEnum.Msg84);
            break;
		case ErrorType.COMBINE_NO_MONEY://20
//			BoxManager.showMessage("合成费用不足");
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg8);
			NetworkSender.Instance().buyGold(BuyGoldFinish,1);
			break;
        case ErrorType.EVOLVE_MATERIALS_CARDLIST://21
//            BoxManager.showMessage("材料卡牌组错误");  
			BoxManager.showMessageByID((int)MessageIdEnum.Msg85);
            break;
        case ErrorType.EVOLVE_MATERIALS_NOTEXIST://22
//            BoxManager.showMessage("材料卡牌不存在"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg86);
            break;
        case ErrorType.EVOLVE_NOT_CONDITION://23
//            BoxManager.showMessage("不满足进化条件"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg87);
            break;
        case ErrorType.STRENGTHEN_NULL_CARD://24
//            BoxManager.showMessage("强化卡牌不存在"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg88);
            break;
        case ErrorType.STRENGTHEN_NOT_ITEM://25
//            BoxManager.showMessage("强化道具不足"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg89);
            break;
		case ErrorType.STRENGTHEN_NO_MONEY://26
//			BoxManager.showMessage("强化费用不足");
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg9);
			NetworkSender.Instance().buyGold(BuyGoldFinish,1);
			break;
        case ErrorType.GAMBLE_NO_FRIEND_POINT://27
//          BoxManager.showMessage("友情点数不足");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg51);
            break;
        case ErrorType.GAMBLE_NO_DOLLAR://28
//          BoxManager.showMessage("元宝不足");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg73);
            break;
        case ErrorType.GAMBLE_NO_LUCKY://29
//            BoxManager.showMessage("幸运值不足够"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg90);
            break;
        case ErrorType.GAMBLE_ERR://30
//            BoxManager.showMessage("抽奖错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg91);
            break;
        case ErrorType.SHOP_POWER://31
//            BoxManager.showMessage("购买体力错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg92);
            break;
        case ErrorType.SHOP_BAG://32
//            BoxManager.showMessage("购买背包错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg93);
            break;
        case ErrorType.SHOP_FRIEND://33
//            BoxManager.showMessage("购买好友上限错误");  
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg94);
            break;
        case ErrorType.SHOP_ERR://35
//            BoxManager.showMessage("商城错误，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg94);
            break;
		case ErrorType.BAG_FULL://37
//			BoxManager.showBagFullBox("您携带的侠士已经达到上限可以将侠士吸收、出售或者扩充您的背包.");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg74);
			break;
        case ErrorType.FRIEND_FULL://38
//          BoxManager.showMessage("您的好友数量已达上限");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg12);
            break;
        case ErrorType.BATTLE_ERR://40
//            BoxManager.showMessage("战斗异常，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg95);
            break;
        case ErrorType.BATTLE_ERR_COPY_NULL://41
//          //使用副本未开启
			BoxManager.showMessageByID((int)MessageIdEnum.Msg97);
            break;
        case ErrorType.BATTLE_ERR_COPY_LEVEL://42
//            BoxManager.showMessage("对不起，您的等级不够进入此副本"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg96);
            break;
        case ErrorType.BATTLE_ERR_COPY_NOTOPEN://43
//            BoxManager.showMessage("副本未开启"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg97);
            break;
        case ErrorType.BATTLE_ERR_COPY_TIMES://44
//            BoxManager.showMessage("副本次数已到上限"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg98);
            break;
        case ErrorType.BATTLE_ERR_CARD_ERR://45
//            BoxManager.showMessage("战斗卡牌异常，请稍后重试"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg99);
            break;
        case ErrorType.BATTLE_ERR_PLAYER_POWER://46
//          BoxManager.showMessage("您的体力不足");
			NetworkSender.Instance().buyPower(BuyPowerDone);
            break;
		case ErrorType.SHOP_POWER_TIMES://47
//			BoxManager.showMessage("今日购买体力次数已达限制");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg10);
			break;
		case ErrorType.SHOP_DOLLAR://48
//			BoxManager.showMessage("当前元宝不足");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg11);
			break;
		case ErrorType.ADD_FRIEND_ALREADY_FRIEND://49
//			BoxManager.showMessage("对方已经是您的好友");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg4);
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg147);
			break;
		case ErrorType.ADD_FRIEND_MY_FULL://50
//			BoxManager.showMessage("您的好友数量已达上限");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg12);
			break;
        case ErrorType.ADD_FRIEND_HE_FULL://51
//            BoxManager.showMessage("对方好友数量已达上限"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg13);
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg147);
            break;
        case ErrorType.ADD_FRIEND_NOTFIND://56
//            BoxManager.showMessage("玩家未找到"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg3);
            break;
		case ErrorType.EVOLVE_NO_MONEY://57
//			BoxManager.showMessage("进化费用不足"); 
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg14);
			NetworkSender.Instance().buyGold(BuyGoldFinish,1);
			break;
        case ErrorType.MAIL_TOPLAYER_MAILFULL://58
//            BoxManager.showMessage("对方收件箱已满"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg7);
            break;
        case ErrorType.BIND_ERR://59
//            BoxManager.showMessage("绑定账号异常，请稍后重试");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg24);
            break;
        case ErrorType.BIND_ALREADY://60
//            BoxManager.showMessage("此账号已经被绑定");
			Debug.LogWarning("BIND_ALREADY");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg243);
            break;
        case ErrorType.BILLING_NET_ERR://61
//            BoxManager.showMessage("网络不通");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg24);
            break;
		case ErrorType.POWER_FULL://62
//			BoxManager.showMessage("体力值已满"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg15);
			break;
		case ErrorType.FRIEND_IS_NOT_MINE://63
//			BoxManager.showMessage("对方已不是您的好友"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg16);
			GameObject.Find("MainUILogic").SendMessage("OnFriendWindow");
			break;
		case ErrorType.POWER_TIMES_FULL://64
//			BoxManager.showMessage("今日获取体力次数已达限制");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg110);
			break;
		case ErrorType.POWER_SEND_ALREADY://65
//			BoxManager.showMessage("您已经送过该好友体力值");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg17);
			break;
		case ErrorType.ADD_FRIEND_ALREADY_MAIL://66
//			BoxManager.showMessage("已经向对方发送过好友申请邮件"); 
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg1); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg147);
			break;
		case ErrorType.BAG_MAX://67
//			BoxManager.showMessage("购买背包已达到上限"); 
			BoxManager.showMessageByID((int)MessageIdEnum.Msg101);
			break;
		case ErrorType.SHOP_PVP_TIMES://69
//			BoxManager.showMessage("今日购买PVP已达上限","购买失败");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg141);
			break;
		case ErrorType.FRIEND_MAX://76
//			BoxManager.showMessage("扩充好友已达上限","扩充好友失败");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg140);
			break;
		case ErrorType.LEAD_POINT_NOT_ENOUGH: //68
			BoxManager.showMessageByID((int)MessageIdEnum.Msg55);
			break;
		case ErrorType.BATTLE_PVP_TIMES: //70
			BoxManager.showMessageByID((int)MessageIdEnum.Msg142);
			break;
		case ErrorType.PVP_SHOP_SCORE: //71
			BoxManager.showMessageByID((int)MessageIdEnum.Msg143);
			break;
		case ErrorType.PVP_SHOP_ERR: //72
			BoxManager.showMessageByID((int)MessageIdEnum.Msg144);
			break;
		case ErrorType.BATTLE_PVP_ERR: //74
			BoxManager.showMessageByID((int)MessageIdEnum.Msg138);
			break;
		case ErrorType.BATTLE_PVP_ERR_CARD_ERR: //75
			BoxManager.showMessageByID((int)MessageIdEnum.Msg138);
			break;
		case ErrorType.BATTLE_PVP_RANK_ERR: //78
			BoxManager.showMessageByID((int)MessageIdEnum.Msg136);
			break;
		case ErrorType.BATTLE_PVP_LIST_ERR: //79
			BoxManager.showMessageByID((int)MessageIdEnum.Msg137);
			break;
        case ErrorType.SCODE_ERR:  //77
//            BoxManager.showMessageByID((int)MessageIdEnum.Msg136);
			BoxManager.showMessageByID((int)MessageIdEnum.Msg151);
            break;
		case ErrorType.SCODE_ERR_USED:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg163);
			break;
		case ErrorType.SCODE_ERR_NOT_PASS:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg164);
			break;
		case ErrorType.SCODE_ERR_PARM_ERR:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg165);
			break;
		case ErrorType.SCODE_ERR_SAME_TYPE:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg166);
			break;
		case ErrorType.SCODE_ERR_NOT_EFFECT:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg167);
			break;
		case ErrorType.SCODE_ERR_NOT_THIS_AREA:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg168);
			break;
		case ErrorType.SCODE_ERR_NOT_THIS_GAME:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg169);
			break;
		case ErrorType.SCODE_ERR_DATA_ERR:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg170);
			break;
		case ErrorType.FS_ACTIVE_ERR:
			BoxManager.showMessage("风水激活失败",ClientConfigure.title); //WML MARK
			break;
		case ErrorType.FS_LEVUP_ERR:
			BoxManager.showMessage("风水升级失败",ClientConfigure.title); //WML MARK
			break;
		case ErrorType.FS_RESET_ERR:
			BoxManager.showMessage("风水重置失败",ClientConfigure.title); //WML MARK
			break;
		case ErrorType.FS_STAR_LESS:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg187);
			UIEventListener.Get(BoxManager.buttonYes).onClick += GoPVEWindow;
			break;
		case ErrorType.FS_SUIPIAN_LESS:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg183);
			UIEventListener.Get(BoxManager.buttonYes).onClick += GoPVEWindow;
			break;
			
		case ErrorType.BGZ_NO_TIMES:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg188);
			break;
        case ErrorType.STUDYSKILL_ERR:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg214);
            break;
        case ErrorType.STUDYSKILL_NO_MONEY:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg215);
            break;
        case ErrorType.STUDYSKILL_NO_SKILL:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg216);
            break;
        case ErrorType.STUDYSKILL_CARDLIST:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg217);
            break;
        case ErrorType.STUDYSKILL_NOT_EXIST:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg218);
            break;
        case ErrorType.STUDYSKILLUPDATE_ERR:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg219);
            break;
        case ErrorType.STUDYSKILLUPDATE_MAX_LEV:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg220);
            break;
        case ErrorType.STUDYSKILLUPDATE_NO_MONEY:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg221);
            break;
        case ErrorType.STUDYSKILLUPDATE_CARDLIST:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg222);
            break;
        case ErrorType.STUDYSKILLUPDATE_CARD_NUM:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg223);
            break;
        case ErrorType.STUDYSKILLUPDATE_NOT_EXIST:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg224);
            break;
        case ErrorType.STUDYSKILLUPDATE_GROUP:
            BoxManager.showMessageByID((int)MessageIdEnum.Msg225);
            break;
		case ErrorType.BATTLE_PATA_ERR:
			BoxManager.showMessage("爬塔战斗次数达到上限",ClientConfigure.title);
			break;
		case ErrorType.BATTLE_PATA_ERR_CARD_ERR:
			BoxManager.showMessage("爬塔战斗异常",ClientConfigure.title);
			break;
		case ErrorType.BATTLE_PATA_TIMES:
			BoxManager.showMessage("爬塔战斗卡牌异常",ClientConfigure.title);
			break;
		case ErrorType.WB_BATTLE_BOSS_DEAD:
			currentErrorType = (ErrorType)type;
			BoxManager.showMessageByID((int)MessageIdEnum.Msg236);
			UIEventListener.Get(buttonYes).onClick += HanderErrorType;
			break;
		case ErrorType.WB_BATTLE_BOSS_HIDE:
			currentErrorType = (ErrorType)type;
			BoxManager.showMessageByID((int)MessageIdEnum.Msg237);
			UIEventListener.Get(buttonYes).onClick += HanderErrorType;
			break;
		case ErrorType.QXZB_NOT_START:
			currentErrorType = (ErrorType)type;
			BoxManager.showMessageByID((int)MessageIdEnum.Msg234);
			UIEventListener.Get(buttonYes).onClick += BackToQxzbPvPController;
			break;
		default:
			//BoxManager.showMessage("服务器返回错误信息:"+(ErrorType)type); 
			Debug.LogError("服务器返回错误信息:"+(ErrorType)type);
			break;
		}
		
	}
	
	private static void BackToQxzbPvPController(GameObject go)
	{
		if (mainLogic == null)
		{
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		}
		mainLogic.OnQxzbPvPWindow();
	}
	
	private static void HanderErrorType(GameObject go)
	{
		ErrorEventListener.OnProduceErrorEvent((int)currentErrorType);
	}
	
	public static void BuyGoldFinish(bool isSucess)
	{
		if (isSucess)
		{
			if (mainLogic == null)
			{
				mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
			}
			mainLogic.SendMessage("refreshTopBar");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
			Debug.Log("Buy Gold Finish");
		}
		else
			Debug.LogError("Buy Gold Error");
	}
	
	public static void GoPVEWindow (GameObject button)
	{
		if (mainLogic == null)
		{
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		}
		mainLogic.LoadMainToPveSceneList();
	}
	
	public static void GoShop (GameObject button)
	{
		if (mainLogic == null)
		{
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		}
		mainLogic.OnShopWindow();
	}
	
	public static void GoProtectCard (GameObject button)
	{
		if (mainLogic == null)
		{
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		}
		mainLogic.onHeroWindow();
	}
	
	public static void GoRecharge(GameObject button)
	{
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn58).ToString());
//		BoxManager.showMessage("功能暂未开放");  
//		BoxManager.showMessageByID((int)MessageIdEnum.Msg18);
		if (mainLogic == null)
		{
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		}
		mainLogic.OnPurchaseWindow();
	}

	public static void BuyPowerDone (bool isSuccess)
	{
		if (isSuccess)
		{
			if (mainLogic == null)
				mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
			mainLogic.SendMessage("refreshTopBar");
//			BoxManager.showMessage("购买成功!");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
		}
		else
		{
			Debug.LogError("Shop Buy Error !");
		}
	}
	
	public static void showReviveMessage()
	{
		
	}
//	public static void showGuideMessage(string[] label,GuideManager.GUIDE_STEP step)
//	{
//		removeMessage();
//		MessageType type = MessageType.GuideBox;
//		topFrame = ResourceManager.Instance.LoadPopUp(type.ToString());//(GameObject)GameObject.Instantiate(messageBox);
//		topFrame.transform.parent =  Camera.mainCamera.transform;
//		topFrame.transform.localScale = Vector3.one;
//		topFrame.transform.localPosition = new Vector3(0,0,box_z);
//		MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
//		mbc.setLabels(label);
//		mbc.setType(type);
//		mbc.setCurrentSetp(step);
////		buttonYes = mbc.buttonYes;
//
//	}
	
	public static  void removeMessage()
	{
		if(topFrame!=null)
		{
//			Debug.LogError("removeMessage:"+topFrame.GetComponent<MessageBoxController>().label.GetComponent<UILabel>().text);
			topFrame.GetComponent<MessageBoxController>().ShowBottomNotice();
			GameObject.Destroy(topFrame);
			topFrame = null;
		}
		buttonYes = null;
		buttonNo = null;
	}	
	
	public static void removeWaitingMessage()
	{
		if(topFrame!=null)
		{
//			Debug.LogError("removeMessage:"+topFrame.GetComponent<MessageBoxController>().label.GetComponent<UILabel>().text);
			MessageBoxController mbc = topFrame.GetComponent<MessageBoxController>();
			if(mbc.getMessageType() == BoxManager.MessageType.WaitingBox)
			{
				topFrame.GetComponent<MessageBoxController>().ShowBottomNotice();
				GameObject.Destroy(topFrame);
				topFrame = null;
			}
		}	
	}
	
	public static GameObject getbuttonShop()
	{
		return buttonShop;
	}
	
	public static GameObject getbuttonUpdate()
	{
		return buttonUpdate;
	}
	
	public static GameObject getbuttonSell()
	{
		return buttonSell;
	}
	
	public static GameObject getYesButton()
	{
		return buttonYes;
	}
	
	public static GameObject getNoButton()
	{
		return buttonNo;
	}
	public static void setInputText(string text)
	{
		inputText = text;
	}
	public static string getInputText()
	{
		return inputText;
	}
	
	public static void ReturnLoginScene(GameObject go){
		if(GameManager.Instance.sceneName != Utils.UI_NAME_Login)
			GameManager.Instance.LoadLevel(Utils.UI_NAME_Login);
		Debug.Log("Test for messageBox : return to login UI");
	}

    public static void GotoVersionUpdate(GameObject go)
    {
        if (GameManager.Instance.sceneName != Utils.UI_NAME_Login)
            GameManager.Instance.LoadLevel(Utils.UI_NAME_Login);
        Debug.LogError("Need version update, GotoVersionUpdate");

        //打开
        DeviceHelper.OpenVersionUpdateUrl();
    }

	//处理客户端丢失服务器传回的升级，精进和强化的包,客户端进行显示效果，并发送重新数据请求的包//
	public static void ContinueShow(GameObject go)
	{		
		UIListener.Instance().OnReceiveMsg(true);
		NetworkSender.Instance().sendFinish(true);
		NetworkSender.Instance().GetUserInfo(null);
	}
}
