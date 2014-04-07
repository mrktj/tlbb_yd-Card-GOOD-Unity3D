using UnityEngine;
using System.Collections;

using Games.CharacterLogic;
using Games.LogicObject;
using Games.Battle;

public class BattleTopUI : MonoBehaviour {
	
	public GameObject PVEWidgets;
	public GameObject PVPWidgets;
	public GameObject ChonglouWidgets;
	
	public UILabel pataRubyNum;
	public UILabel pataMoneyNum;
	public UILabel pataLevelWord;
	
	public UILabel cardLabel;
	public UILabel itemLabel;
	public UILabel moneyLabel;
	public UILabel curRoundValue;
	public UILabel curRound;
	public UILabel curStepValue;
	public UILabel curStep;
	public UISprite roundBg;
	
	public UILabel enemyName;

	private int cardCount = 0;
	private int itemCount = 0;
	private int moneyCount = 0;
	
	private BattlePlayer mBattlePlayer;
	private BattleProcedureType battleProcedureType;
	
	private BattleType battleType = BattleType.PVE;
	
	
	// Use this for initialization
	void Start () {		
		moneyLabel.text = "" + moneyCount;
		cardLabel.text = "" + cardCount;
		itemLabel.text = "" + itemCount;
		battleType = Obj_MyselfPlayer.GetMe().battleType;
		BattleLogic bl = GameObject.Find("BattleLogic").GetComponent<BattleLogic>();
		mBattlePlayer = bl.GetBattleCore().GetBattlePlayer();
		battleProcedureType = mBattlePlayer.GetBattleStateType();
		Reset();
	}
	
	private void Reset()
	{
		if(Obj_MyselfPlayer.GetMe().isGuideBattle)
		{
			ChonglouWidgets.SetActive(false);
			PVPWidgets.SetActive(false);
			curStep.gameObject.SetActive(false);
			curStepValue.gameObject.SetActive(false);
			curRound.gameObject.SetActive(false);
			curRoundValue.gameObject.SetActive(false);
			curStepValue.gameObject.SetActive(false);
			roundBg.gameObject.SetActive(false);
		}
		else
		{
			switch(battleType)
			{
			case BattleType.PVE:
				PVPWidgets.SetActive(false);
				ChonglouWidgets.SetActive(false);
				break;
			case BattleType.PVP:
			case BattleType.QxzbPvP:
				PVEWidgets.SetActive(false);
				PVPWidgets.SetActive(true);
				ChonglouWidgets.SetActive(false);
				enemyName.gameObject.SetActive(true);
				enemyName.text = Obj_MyselfPlayer.GetMe().accountName + " VS " + Obj_MyselfPlayer.GetMe().enemyName;
				break;
			case BattleType.WORLD_BOSS:
				PVPWidgets.SetActive(false);
				PVEWidgets.SetActive(false);
				ChonglouWidgets.SetActive(false);
				break;
			case BattleType.CHONG_LOU:
				PVPWidgets.SetActive(false);
				PVEWidgets.SetActive(false);
				ChonglouWidgets.SetActive(true);
				SetChonglouUI(Obj_MyselfPlayer.GetMe().battleData.missionID,
					Obj_MyselfPlayer.GetMe().pataRoundRewardYuanbao,
					Obj_MyselfPlayer.GetMe().pataRoundRewardMoney);
				break;
			}
			switch(battleProcedureType)
			{
			case BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TIP:
				curStep.gameObject.SetActive(true);
				curStepValue.gameObject.SetActive(true);
				curRound.gameObject.SetActive(false);
				curRoundValue.gameObject.SetActive(false);
				curStepValue.text = "0" + "/20";
                roundBg.gameObject.SetActive(true);
				break;
			case BattleProcedureType.E_BATTLE_PROCEDURE_BATTLING:
				curStep.gameObject.SetActive(true);
				curStepValue.gameObject.SetActive(true);
				curRound.gameObject.SetActive(false);
				curRoundValue.gameObject.SetActive(false);
				curStepValue.text = (mBattlePlayer.GetTurnCounter() + 1).ToString() + "/20";
                roundBg.gameObject.SetActive(true);
				break;
			case BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TROPHY:
				curStep.gameObject.SetActive(true);
				curStepValue.gameObject.SetActive(true);
				curRound.gameObject.SetActive(false);
				curRoundValue.gameObject.SetActive(false);
				curStepValue.text = mBattlePlayer.GetTurnCounter().ToString() + "/20";
                roundBg.gameObject.SetActive(true);
				break;
			case BattleProcedureType.E_BATTLE_PROCEDURE_WAITING:
				if(battleType == BattleType.PVE)
				{
					curStep.gameObject.SetActive(false);
					curStepValue.gameObject.SetActive(false);
					curRound.gameObject.SetActive(true);
					curRoundValue.gameObject.SetActive(true);
					curRoundValue.text = mBattlePlayer.RoundCounter + "/3";
	                roundBg.gameObject.SetActive(true);
				}
				else
				{
					curStep.gameObject.SetActive(false);
					curStepValue.gameObject.SetActive(false);
					curRound.gameObject.SetActive(false);
					curRoundValue.gameObject.SetActive(false);
	                roundBg.gameObject.SetActive(false);
				}
				break;
            case BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_START:			
			case BattleProcedureType.E_BATTLE_PROCEDURE_CARD_PREPARE:
			case BattleProcedureType.E_BATTLE_PROCEDURE_CARD_FORWARD:
			case BattleProcedureType.E_BATTLE_PROCEDURE_CARD_MEET:
			case BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_RESULT:
			case BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_END:
				curStep.gameObject.SetActive(false);
				curStepValue.gameObject.SetActive(false);
				curRound.gameObject.SetActive(false);
				curRoundValue.gameObject.SetActive(false);
                roundBg.gameObject.SetActive(false);
				break;
			default:
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

        if(battleProcedureType != mBattlePlayer.GetBattleStateType())
		{
			battleProcedureType = mBattlePlayer.GetBattleStateType();
			Reset();
		}
        UpdateStepValue();
	}
	
	void UpdateStepValue()
	{
		switch(battleProcedureType)
		{
		case BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TIP:
			curStepValue.text = "0" + "/20";
			break;
		case BattleProcedureType.E_BATTLE_PROCEDURE_BATTLING:
			curStepValue.text = (mBattlePlayer.GetTurnCounter() + 1).ToString() + "/20";
			break;
		case BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TROPHY:
			curStepValue.text = mBattlePlayer.GetTurnCounter().ToString() + "/20";
			break;
		}
	}
	
	public void AddMoney(int count)
	{
		moneyCount += count;
		moneyLabel.text = "" + moneyCount;
	}
	
	public void AddCard(int count)
	{
		cardCount += count;
		cardLabel.text = "" + cardCount;
		Debug.Log("cardCount = " + cardCount);
	}
	
	public void AddItem(int count)
	{
		itemCount += count;
		itemLabel.text = "" + itemCount;
	}
	
	public void SetChonglouUI(int level,int yuanbao,int coin)
	{
		//string word= "" + level + "层 " + yuanbao + "元宝 " + coin +"金币";
		//ChonglouWidgets.GetComponentInChildren<UILabel>().text = word;
		pataLevelWord.text = "" + level + "层可获得";
		pataRubyNum.text = yuanbao.ToString();
		pataMoneyNum.text = coin.ToString();
	}
	
}
