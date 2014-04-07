using UnityEngine;
using System.Collections;
using Games.LogicObject;
using Games.CharacterLogic;
using Module.Log;
using GCGame.Table;
using System;
using card.net;

public class PVPBattleBeforeController : MonoBehaviour {
	
	public MainUILogic mainUILogic;
	public GameObject[] cardIcon;
	public UILabel leaderSkill;
	public int curStar;
//	public long[] pvpIndex = {-1,-1,-1,-1,-1,-1}; //
	
	/*
	 *    Index :		0	1	2 
	 * 					3	4	5
	 */
	
	// Use this for initialization
	void Start () {
	}
	
	void OnEnable()
	{
		mainUILogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		curStar = Obj_MyselfPlayer.GetMe().curPvPStar;
		
		initPvPData();
		
		long leaderCardID = Obj_MyselfPlayer.GetMe().curPvPLearder;
		if (leaderCardID <= 0 || !Obj_MyselfPlayer.GetMe().IsCardInBagByID(leaderCardID)) //无队长或者队长卡不存在
		{
			this.transform.gameObject.SetActive(false);
			OnChooseCard();
			return;
		}
		
		FreshUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void backToPreviousWindow(GameObject btn)
	{
		if(mainUILogic != null)
		{
			mainUILogic.OnQxzbPvPWindow();
		}
	}
	
//	筛选PVP阵型信息
	void initPvPData ()
	{
		/*
		bool isNeedInit = true;
		if (Obj_MyselfPlayer.GetMe().curPvPLearder > 0)
				isNeedInit = false;
		*/
		int i = 0;
		for(; i < 6; i++)
		{
			Obj_MyselfPlayer.GetMe().PvPBattleArray[i] = -1;
		}
		Obj_MyselfPlayer.GetMe().curPvPLearder = -1;
		/*
		///*   临时代码 假数据	 Start ***************************
		i = 0;
		if (isNeedInit)
		{
			foreach (UserCardItem item in Obj_MyselfPlayer.GetMe().cardBagList)
			{
				if (item.quality == curStar && item.qxzbFightIndex < 0)
				{
					item.qxzbFightIndex = curStar * 10 + i;
					if (i == 0)
						item.qxzbFightIndex += 100;
					i++;
					if (i >= 5)
						break;
				}
			}
		}
		
		///*   临时代码 假数据	 Over	***************************
		*/
		UserCardItem firstMember = null;
		foreach (UserCardItem item in Obj_MyselfPlayer.GetMe().cardBagList)
		{
			Debug.Log("item.qxzbFightIndex: " + item.qxzbFightIndex);
			if (item.quality != curStar || item.qxzbFightIndex < 0 || curStar != (item.qxzbFightIndex%100)/10)
				continue;
			int index = item.qxzbFightIndex % 10;
			if (item.qxzbFightIndex / 100 == 0) //Team Leader
				Obj_MyselfPlayer.GetMe().curPvPLearder = item.cardID;
			else //Team Memeber
				firstMember = item;
			if (Obj_MyselfPlayer.GetMe().PvPBattleArray[index] == -1)
				Obj_MyselfPlayer.GetMe().PvPBattleArray[index] = item.cardID;
//			else
//				Debug.LogError("Error: some cards with same index in pvpIndex : " + index + " ->  [" + Obj_MyselfPlayer.GetMe().PvPBattleArray[index] + " , " + item.cardID + " ]");
		}
		
	}
	
	void FreshIcon(GameObject icon, long cardID)
	{
		int templeID = -1;
		if (cardID <= 0)
		{
			icon.SetActive(false);
			return;
		}
		UserCardItem card_item = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(cardID);
		if(card_item != null)
		{
			templeID = card_item.templateID;
		}
		LogModule.DebugLog("BattleBefore, FreshIcon(), Card Guid: " + cardID + ", Card templeid: " + templeID);
		if(templeID != -1)
		{
			icon.SetActive(true);
			icon.GetComponent<CardLarge>().SetCardTemplateID(templeID);
			UILabel lev=icon.transform.FindChild("Label-Level-Value").GetComponent<UILabel>();
			
			Transform transformStars = icon.transform.FindChild("Stars");
			if(card_item!=null)
			{
				lev.text=card_item.level.ToString();
				for (int j = 1; j <= 7; j++)
           	 	{
                	if (j <= card_item.quality)
                	{
                    	GameObject starIcon = transformStars.FindChild("star_" + j).gameObject;
                   	 	starIcon.SetActive(true);
                	}
                	else
                	{
                    	GameObject starIcon = transformStars.FindChild("star_" + j).gameObject;
                    	starIcon.SetActive(false);
                	}
            	}
			}
		}
		else
		{
			icon.SetActive(false);
		}
	}
	
	public void FreshUI()
	{
		Int32 leaderid;
		Int32 skillid = -1;
		if (Obj_MyselfPlayer.GetMe().curPvPLearder > 0)
		{
			leaderid = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(Obj_MyselfPlayer.GetMe().curPvPLearder).templateID;
			skillid = TableManager.GetCardByID(leaderid).SkillLeader;
		}
		if(skillid == -1)
           	leaderSkill.text = "无";
		else {
			string describe = LanguageManger.GetWords(TableManager.GetLeaderskillByID(skillid).Note);
			if(string.IsNullOrEmpty(describe))
			{
				Debug.Log("Leader skill describe is -1");
			}
			leaderSkill.text = describe;
		}
//		Debug.LogError("------------------");
		for(int i=0; i<6; i++)
		{
//			Debug.LogError("PvPBattleArray : [ " + i + " ] = " + Obj_MyselfPlayer.GetMe().PvPBattleArray[i]);
			Obj_MyselfPlayer.GetMe().refrashPvPBattleArray(i);
	       	FreshIcon(cardIcon[i], Obj_MyselfPlayer.GetMe().PvPBattleArray[i]);
		}
//		Debug.LogError("------------------");
		
	}
	
	public void OnChooseCard()
	{
		if (mainUILogic == null)
			mainUILogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		mainUILogic.OnPVPTeamWindow();
	}
	
	public void OnStartBattle()
	{
		long leaderCardID = Obj_MyselfPlayer.GetMe().curPvPLearder;
		if (leaderCardID <= 0 || !Obj_MyselfPlayer.GetMe().IsCardInBagByID(leaderCardID)) //无队长或者队长卡不存在
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg238);
			return;
		}
		NetworkSender.Instance().QxzbBattle(OnStartBattleDone);	
	}
	
	public void OnStartBattleDone(bool isSuccess)
	{
		if (isSuccess)
			mainUILogic.LoadBattleUIScene();
	}
	
}
