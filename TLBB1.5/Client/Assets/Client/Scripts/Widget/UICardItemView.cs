//
//  UICardItemView.cs
//
//  Created by JackWen on 13-11-30.
//  Copyright (c) 2013年 JackWen. All rights reserved.
//
//	Hero Item UI View
//
using UnityEngine;
using System.Collections;
using Games.LogicObject;
using GCGame.Table;
using Games.CharacterLogic;

public class UICardItemView : MonoBehaviour {
	
	public GameObject spriteSpriteHeader;//队长标记
	
	public UIImageButton bG;//背景按钮
	
	public UISprite spriteBG;//背景图片
	public UISprite cardIconSpriteFrame;//小卡牌背景
	public UISprite cardIconSpriteBG;//小卡牌边框
	public UISprite checkboxCheckmark;//选择标识图片
	public UISprite spriteSpriteNameBG;//卡牌名字背景图片
	
	public UICheckbox checkbox;//选择标识
	
	public bool InitTeamMemberLeaderWithCard(UserCardItem card){
		
		transform.localPosition = new Vector3(0,0,-1);
		transform.localScale = new Vector3(1,1,1);
		SetMemberItem(card, true);
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		int icon_star = TableManager.GetCardByID(card.templateID).Star;
		cardIconSpriteFrame.spriteName = UserCardItem.littleCardFrameName[icon_star];
		cardIconSpriteBG.spriteName = UserCardItem.littleCardBorderName[icon_star];
		//--------------------------------------------------------------------
		spriteSpriteHeader.SetActive(true);
		checkboxCheckmark.spriteName="xiashi_suo";
		
		checkboxCheckmark.transform.localPosition=new Vector3(-2,0,-5);
		checkboxCheckmark.MakePixelPerfect();
		checkbox.isChecked = true;
		bG.isEnabled = false;
		spriteSpriteNameBG.alpha=0.5f;
		
		return true;
	}
	
	public bool InitTeamMemberOtherWithCard(UserCardItem card, UIEventListener.VoidDelegate selectMember, int memberNum, int nowLeaderShip, int maxLeaderShip, bool isPVP){
		
		transform.localPosition = new Vector3(0,0,-1);
		transform.localScale = new Vector3(1,1,1);
		
		spriteSpriteHeader.SetActive(false);
		checkbox.isChecked = false;
		checkboxCheckmark.spriteName="xuanze";
		checkboxCheckmark.transform.localPosition=new Vector3(0,0,-5);
		
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		int icon_star = TableManager.GetCardByID(card.templateID).Star;
		cardIconSpriteFrame.spriteName = UserCardItem.littleCardFrameName[icon_star];
		cardIconSpriteBG.spriteName = UserCardItem.littleCardBorderName[icon_star];
		//--------------------------------------------------------------------
		
		//newItem.transform.FindChild("BG").GetComponent<UIButton>().isEnabled = false;
		bG.isEnabled = true;
		spriteSpriteNameBG.alpha=1f;
		
		UIEventListener.Get(gameObject).onClick += selectMember;
		bool canUse=true;
		if(memberNum == 4){
			bG.isEnabled = false;
			spriteSpriteNameBG.alpha=0.5f;
			UIEventListener.Get(gameObject).onClick-=selectMember;
		}
		else if(nowLeaderShip + card.GetLeaderShip() > maxLeaderShip){
			bG.isEnabled = false;
			spriteSpriteNameBG.alpha=0.5f;
			UIEventListener.Get(gameObject).onClick-=selectMember;
			canUse=false;
		}
		
		//搜索队员卡牌 分两种情况 PVP选队员 和 普通的选队员
		int length = isPVP ? Obj_MyselfPlayer.GetMe().PvPBattleArray.Length : Obj_MyselfPlayer.GetMe().teamMemberArray.Length;
		for(int i= isPVP ? 0 : 1 ; i < length; i++){
			if(card.cardID == (isPVP ? Obj_MyselfPlayer.GetMe().PvPBattleArray[i] : Obj_MyselfPlayer.GetMe().teamMemberArray[i])){
				checkbox.isChecked = true;
				bG.isEnabled = true;
				spriteSpriteNameBG.alpha=1;
				UIEventListener.Get(gameObject).onClick -= selectMember;
				UIEventListener.Get(gameObject).onClick += selectMember;
				canUse=true;
				break;
			}
		}
		
		SetMemberItem(card, canUse);
		checkboxCheckmark.MakePixelPerfect();
		
		return true;
	}
	
	
	
	
	
	public GameObject cardIconBtn;//小卡牌按钮
	
	public UISprite cardIconSpriteIcon;//小卡牌头像
	
	public UILabel labelsLabelName;//卡牌名称
	public UILabel labelsLabelHpValue;//血量值
	public UILabel labelsLabelLv;//等级
	public UILabel labelsLabelAttackValue;//攻击力
	public UILabel labelsLabelLeadershipValue;//领导力
	
	public UISprite spritesSpriteProperty;//卡牌属性图片
	
	public GameObject[] stars;
	
	public bool SetMemberItem(UserCardItem card, bool canUse)
	{
		transform.name = card.cardID.ToString();
		string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).HeadIcon;
		AtlasManager.Instance.setHeadName(cardIconSpriteIcon,  atlasname);
		cardIconSpriteIcon.transform.localScale=new Vector3(82,82,1);
			
		labelsLabelName.text = LanguageManger.GetWords(TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).Name);

		labelsLabelHpValue.text = card.GetHp().ToString();

		labelsLabelLv.text = card.level.ToString();
				
		labelsLabelAttackValue.text = card.GetAttack().ToString();
		
		if(!canUse){
			labelsLabelLeadershipValue.text = "[FF231A]"+card.GetLeaderShip().ToString()+"[000000]";
		}
		else{
			labelsLabelLeadershipValue.text = "[F1ECCF]"+card.GetLeaderShip().ToString()+"[000000]";
		}
		
		spritesSpriteProperty.spriteName = UserCardItem.elementTypeName[ TableManager.GetCardByID(card.templateID).Element ];
		
        for (int j = 1; j <= 7; j++){
           if (j <=card.quality){
				stars[j-1].SetActive(true);
           }
           else{
               stars[j-1].SetActive(false);
		   }
		}
		cardIconBtn.name=card.cardID.ToString();
		return true;
	}
	
	public GameObject GetCardIconBtn(){
		return cardIconBtn;
	}
	
}
