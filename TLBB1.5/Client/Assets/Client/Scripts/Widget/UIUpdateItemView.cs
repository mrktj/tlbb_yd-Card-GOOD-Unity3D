//
//  UIUpdateItemView.cs
//
//  Created by JackWen on 13-11-30.
//  Copyright (c) 2013年 JackWen. All rights reserved.
//
//	Update UI View
//
using UnityEngine;
using System.Collections;
using Games.LogicObject;
using GCGame.Table;
using Games.CharacterLogic;

public class UIUpdateItemView : MonoBehaviour {

	public UISprite iconBG;
	public UISprite iconFrame;
	public UISprite iconCard;
	public UISprite attribute;
	public UISprite selected;
	
	public GameObject cardItemBtn;
	
	public UILabel labelMaxLevel;
	public UILabel labelName;
	public UILabel labelCardLevel;
	public UILabel labelHP;
	public UILabel labelAttack;
	
	public GameObject[] starArray;
	
	public bool InitWithUserCardItem(UserCardItem card)
	{	
		transform.localPosition = new Vector3(0, 0, -1);
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.name = card.cardID.ToString();
		
		if (card == null){
            return false;
        }
		
		//读表双重验证
		Tab_Card tabCard = TableManager.GetCardByID(card.templateID);
		if(tabCard == null)
			return false;
		Tab_Appearance tabApp = TableManager.GetAppearanceByID(tabCard.Appearance);
		if(tabApp == null)
			return false;
		
        iconCard.spriteName = tabApp.HeadIcon;
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		int icon_star = tabCard.Star;
		iconBG.spriteName = UserCardItem.littleCardFrameName[icon_star];
		iconFrame.spriteName = UserCardItem.littleCardBorderName[icon_star];
		//--------------------------------------------------------------------

        int nameLangId = tabApp.Name;
        labelName.text = LanguageManger.GetWords(nameLangId);
        labelHP.text = card.GetHp().ToString();
        labelAttack.text = card.GetAttack().ToString();
        labelCardLevel.text = card.level.ToString();
		HideSelected();

        //等级状态显示
        labelMaxLevel.text = card.level.ToString() + "/" + tabCard.MaxLevel;

        //五行图标显示
        attribute.spriteName = card.GetAttributeIconName();


        //星级显示
        for (int j = 1; j <= 7; j++){
            if (j <= card.quality){
                starArray[j-1].SetActive(true);
            }
            else{
                starArray[j-1].SetActive(false);
            }
        }
		
		return true;
	}
	
	public GameObject GetCardIconBtn(){
		return cardItemBtn;
	}
	
	public void ShowSelected(){
		selected.gameObject.SetActive(true);
	}
	public void HideSelected(){
		selected.gameObject.SetActive(false);
	}
	
}
