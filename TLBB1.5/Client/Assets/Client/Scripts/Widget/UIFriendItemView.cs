//
//  UIFriendItemView.cs
//
//  Created by JackWen on 13-11-30.
//  Copyright (c) 2013年 JackWen. All rights reserved.
//
//	Friend UI View
//
using UnityEngine;
using System.Collections;
using Games.LogicObject;
using GCGame.Table;
using Games.CharacterLogic;

public class UIFriendItemView : MonoBehaviour {
	
	public UISprite iconBG;
	public UISprite iconFrame;
	public UISprite iconCard;
	public UISprite attribute;
	
	public GameObject giftLabel;
	public GameObject cardItemBtn;
	
	public UILabel labelPlayerLevel;
	public UILabel labelName;
	public UILabel labelState;
	public UILabel labelCardLevel;
	
	public GameObject[] starArray;
	
	public bool InitWithUserFriend(UserFriend card)
	{	
		transform.localPosition = new Vector3(0, 0, -1);
		transform.localScale = new Vector3(1, 1, 1);			
		gameObject.name = card.guid.ToString();
		
		if (card == null){
            return false;
        }
		
       	labelName.text = card.name;//friendList[i].GetAccountName();
		
		//读表双重验证
		Tab_Card tabCard = TableManager.GetCardByID(card.cardTempletID);
		if(tabCard == null)
			return false;
		Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
		if(tabAppearance == null)
			return false;
		
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		int icon_star = tabCard.Star;
		iconBG.spriteName = UserCardItem.littleCardFrameName[icon_star];
		iconFrame.spriteName = UserCardItem.littleCardBorderName[icon_star];
		//--------------------------------------------------------------------
        //五行显示
		int nAttributeID = 0;//默认为金
        	nAttributeID = tabCard.Element;
        string strIconName = "";
        switch(nAttributeID){
            case 0: strIconName = "jin"; break;
            case 1: strIconName = "mu"; break;
            case 2: strIconName = "shui"; break;
            case 3: strIconName = "huo"; break;
            case 4: strIconName = "tu"; break;
        }
        attribute.spriteName = strIconName;
		//---------------------------------------------------------------------
		//2013-7-26 Jack Wen
		labelPlayerLevel.text = card.level.ToString();
		int lastOnlineTime = card.lastOnlineHours;
		int lastLogoutTime = card.lastLogoutHours;
		
		if(lastOnlineTime != -1){
			UIImageButton button = gameObject.GetComponent<UIImageButton>();
			button.normalSprite = "liebiao_beijing_1";
			button.hoverSprite = "liebiao_beijing_1";
			button.pressedSprite = "liebiao_beijing_2";
			UISprite back = gameObject.transform.FindChild("Sprite/Background").GetComponent<UISprite>();
			back.spriteName = "liebiao_beijing_1";
			back = gameObject.transform.FindChild("Sprite/haoyoubeijing").GetComponent<UISprite>();
			back.alpha = 1.0f;
			if( lastOnlineTime / 60 >= 24)
				labelState.text = "[a5e295]登录："+lastOnlineTime/60/24 +"天";
			else if( lastOnlineTime / 60 < 24 && lastOnlineTime / 60 > 0)
				labelState.text = "[a5e295]登录："+lastOnlineTime / 60 + "小时";
			else
				labelState.text = "[a5e295]登录："+System.Math.Abs(lastOnlineTime%60)+"分钟";
			//labelState.text = "[a5e295]登录："+lastOnlineTime/60+"小时前";
		} else {
			UIImageButton button = gameObject.GetComponent<UIImageButton>();
			button.normalSprite = "liebiao_beijing_3";
			button.hoverSprite = "liebiao_beijing_3";
			button.pressedSprite = "liebiao_beijing_3";
			UISprite back = gameObject.transform.FindChild("Sprite/Background").GetComponent<UISprite>();
			back.spriteName = "liebiao_beijing_3";
			back = gameObject.transform.FindChild("Sprite/haoyoubeijing").GetComponent<UISprite>();
			back.alpha = 0.5f;
			if( lastLogoutTime / 60 >= 24)
				labelState.text = "[222222]离线："+lastLogoutTime/60/24 +"天";
			else if( lastLogoutTime / 60 < 24 && lastLogoutTime / 60 > 0)
				labelState.text = "[222222]离线："+lastLogoutTime / 60 + "小时";
			else
				labelState.text = "[222222]离线："+System.Math.Abs(lastLogoutTime%60)+"分钟";
		}
		
		iconCard.spriteName = tabAppearance.HeadIcon;
		//2013-7-29 Jack Wen
        if (card.canGetPower){
            giftLabel.SetActive(true);
            if (Obj_MyselfPlayer.GetMe().receive_power_time < 10){
                giftLabel.GetComponent<TweenAlpha>().style = UITweener.Style.PingPong;
            }
            else{
                giftLabel.GetComponent<TweenAlpha>().style = UITweener.Style.Once;
            }
        }
        else
            giftLabel.SetActive(false);
		//2013-8-2 Jack Wen
		//加入好友主卡牌星级显示
		for (int start_i = 1; start_i < 8; start_i++){
			if (start_i <= tabCard.Star){
			    starArray[start_i - 1].SetActive(true);
			}
			else{
			    starArray[start_i - 1].SetActive(false);
			}
		}
		//2013-8-6 Jack Wen
		//显示卡牌等级
		labelCardLevel.text = card.cardLevel.ToString();
		//---------------------------------------------------------------------
		
		return true;
	}
	
	public GameObject GetCardIconBtn(){
		return cardItemBtn;
	}
	
}
