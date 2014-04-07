//
//  UICardItemView.cs
//
//  Created by JackWen on 13-12-13.
//  Copyright (c) 2013年 JackWen. All rights reserved.
//
//	PVP List Item UI View
//
using UnityEngine;
using System.Collections;
using Games.LogicObject;
using GCGame.Table;
using Games.CharacterLogic;

public class UIPVPListItemView : MonoBehaviour {

	public GameObject cardIconBtn;
	public UISprite cardIconIcon;
	public UISprite cardIconBG;
	public UISprite cardIconBorder;
	
	public GameObject fightBtn;
	
	public UILabel labelFightValue;
	public UILabel labelLevelValue;
	public UILabel labelName;
	public UILabel labelRankValuel;
	public UILabel labelScoreValue;
	
	
	public bool InitWithPlayerInfo(PVPPlayerInfo playerInfo, UIEventListener.VoidDelegate ShowCardInfo, UIEventListener.VoidDelegate OnSelectPvPItem){
		
		transform.localPosition = new Vector3(0, 0, -1);
    	transform.localScale = new Vector3(1, 1, 1);
    	gameObject.name = playerInfo.nlGUID.ToString();
		
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		//templeID < 0 情况的容错处理,暂时显示默认头像，且错误头像点击不给予反映//
		Tab_Card tabPCard = TableManager.GetCardByID(playerInfo.nTempleID);
		if(tabPCard != null)
		{
			int icon_star = tabPCard.Star;
			cardIconBG.spriteName = UserCardItem.littleCardFrameName[icon_star];
			cardIconBorder.spriteName = UserCardItem.littleCardBorderName[icon_star];
			
			UIEventListener.Get(cardIconBtn).onClick = ShowCardInfo;
		}
		//--------------------------------------------------------------------
		
		labelRankValuel.text = playerInfo.nRank.ToString();
		labelScoreValue.text = GetOccupyScoreByRank(playerInfo.nRank).ToString();
		
		long leadCardID = 0;
		UserCardItem leaderCard = null;
		
		if(playerInfo.nlGUID == Obj_MyselfPlayer.GetMe().accountID)
		{
			leadCardID = Obj_MyselfPlayer.GetMe().GetTeamLeaderCardID();
			leaderCard = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(leadCardID);
		}
			
		int apperanceID = 0;
		Tab_Card tabCard;
		if(playerInfo.nlGUID == Obj_MyselfPlayer.GetMe().accountID){
			labelName.text = Obj_MyselfPlayer.GetMe().accountName;			
			labelFightValue.text = Obj_MyselfPlayer.GetMe().GetFightValue().ToString();
			labelLevelValue.text =  leaderCard.level.ToString();
			
			tabCard = TableManager.GetCardByID(leaderCard.templateID);
			if(tabCard != null){				
				apperanceID = tabCard.Appearance;
			}
		}
		else{
			labelName.text = playerInfo.strName;
			labelFightValue.text = playerInfo.nFight.ToString();
			labelLevelValue.text = playerInfo.nLev.ToString();
			
			tabCard = TableManager.GetCardByID(playerInfo.nTempleID);
			if(tabCard != null){
				apperanceID = tabCard.Appearance;
			}
		}
		
		Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(apperanceID);
		if(tabAppearance != null){
			cardIconIcon.spriteName = tabAppearance.HeadIcon;
		}
   		
		
		if (playerInfo.nRank >= Obj_MyselfPlayer.GetMe().nHeroRank){
			fightBtn.SetActive(false);
		}
		else if(Obj_MyselfPlayer.GetMe().nHeroRank >= 16 &&  playerInfo.nRank <=10) {//大于等于16名的情况是前10不能挑战
			fightBtn.SetActive(false);
		}
		else if(Obj_MyselfPlayer.GetMe().nHeroRank < 16 && playerInfo.nRank <  (Obj_MyselfPlayer.GetMe().nHeroRank - 5)) { //
			fightBtn.SetActive(false);
		}
		else{
			fightBtn.SetActive(true);
		}
		
    	UIEventListener.Get(fightBtn).onClick = OnSelectPvPItem;
		
		if(tabPCard == null ||
			tabCard == null ||
			tabAppearance == null)
		{
			return false;
		}
		return true;
	}
	
	//根据排名获得占领积分
	private int GetOccupyScoreByRank(int nRank)
	{
		int nOcupyScore = 0;
		int nLen = TableManager.GetPvpScore().Count;
		for(int i=1; i<= nLen; i++)
		{
			//这里的RankMin 是排名的数字，名次越小，数字越大
			if(TableManager.GetPvpScoreByID(i).RankMin >= nRank
				  && TableManager.GetPvpScoreByID(i).RankMax <= nRank)
			{
				nOcupyScore = TableManager.GetPvpScoreByID(i).Score;
				break;
			}
		}
		
		return nOcupyScore;
	}
}
