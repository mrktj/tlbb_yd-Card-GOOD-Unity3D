using UnityEngine;
using System.Collections;
using Games.LogicObject;
using Games.CharacterLogic;
using GCGame.Table;

public class PVPTeamController : MonoBehaviour {
	
	private MainUILogic mainUILogic;
	public GameObject[] teamMember;
	public UILabel leaderShipShow;
    public UILabel strengthShow;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnEnable() {
		mainUILogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
//		Debug.LogError("Init Team");
		
		long leaderCardID = Obj_MyselfPlayer.GetMe().curPvPLearder;
		if (leaderCardID <= 0 || !Obj_MyselfPlayer.GetMe().IsCardInBagByID(leaderCardID)) //无队长或者队长卡不存在
		{
			this.transform.gameObject.SetActive(false);
			LoadSelectHeaderWindow();
			return;
		}
		
		initTeamCard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void initTeamCard()
	{
//		Debug.LogError("-------------------------");
//		Debug.LogError("Leader = " + Obj_MyselfPlayer.GetMe().curPvPLearder);
//		for (int i = 0; i < 6; i++)
//		{
//			Debug.LogError("PvP Array : [ " + i + " ] = " + Obj_MyselfPlayer.GetMe().PvPBattleArray[i]);
//		}
//		Debug.LogError("-------------------------");
		int nextEmptyIndex = 0; //下一个空白的队员位置
//		teamMember[0].transform.FindChild("CardIconBtn/Panel").gameObject.SetActive(false);
		for(; nextEmptyIndex < 5; nextEmptyIndex++)
			teamMember[nextEmptyIndex].transform.FindChild("CardIconBtn/Panel").gameObject.SetActive(false);
		nextEmptyIndex = 1;
		foreach(UserCardItem item in Obj_MyselfPlayer.GetMe().cardBagList) //为保证TeamController中显示的队员顺序不随调整阵型中位置的变化而变化,将foreach作为外层循环
		{
			if (item.qxzbFightIndex <= 0 || (item.qxzbFightIndex / 10) % 10 != Obj_MyselfPlayer.GetMe().curPvPStar)
				continue;
			for(int i = 0; i < 6; i++)
			{
				if (item.cardID == Obj_MyselfPlayer.GetMe().PvPBattleArray[i])
				{
					int pos = -1;
					if (item.cardID != Obj_MyselfPlayer.GetMe().curPvPLearder) //Not Leader
					{
						pos = nextEmptyIndex;
						nextEmptyIndex++;
					}
					else
						pos = 0;
					if (pos > 4)
						break;
					teamMember[pos].transform.FindChild("CardIconBtn/Panel").gameObject.SetActive(true);
	                teamMember[pos].transform.FindChild("CardIconBtn/Panel/Label-Lev").GetComponent<UILabel>().text = item.level.ToString();
	              	UITexture card = teamMember[pos].transform.FindChild("CardIconBtn/Panel/PanelCard/card").GetComponent<UITexture>();
	             	AtlasManager.Instance.setBodyByTempletID(card, item.templateID);
	            	UISprite frame = teamMember[pos].transform.FindChild("CardIconBtn/Panel/Frame").GetComponent<UISprite>();
	              	frame.spriteName = UserCardItem.spriteFrameName[TableManager.GetCardByID(item.templateID).Star];
	             	frame.MakePixelPerfect();
				}
			}
		}
		int nowLeaderShip = Obj_MyselfPlayer.GetMe().GetPvPLeaderShipValue();
        int nowstreng = Obj_MyselfPlayer.GetMe().GetPvPFightValue();
        leaderShipShow.text = nowLeaderShip + "/" + Obj_MyselfPlayer.GetMe().leadership;
        strengthShow.text = nowstreng.ToString();

	}
	
	void OnChooseFinish()
	{
		if (mainUILogic == null)
			mainUILogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		mainUILogic.OnPVPBattleBeforeController();
	}
	
	void LoadSelectHeaderWindow()
	{
		if (mainUILogic == null)
			mainUILogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
        mainUILogic.LoadPvPSelectHeaderWindow();
	}
	
	void LoadSelectMemberWindow()
	{
		if (mainUILogic == null)
			mainUILogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		mainUILogic.LoadPvPSelectMemberWindow();
	}
	
}





















