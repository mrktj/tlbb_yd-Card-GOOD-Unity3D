using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
namespace Games.Battle
{
	public class BattleProcedureReviveFree : BattleProcedureBase
	{
		private BattleProcedureManager m_Manager;
		private BattleType battleType;
		
		private bool changed = false;
		
		private float timer = 0f;
		private float time = 2f;
	    public BattleProcedureReviveFree()
	    {
	    }
	
	    public override BattleProcedureType GetProcedureType()
	    {
	        return BattleProcedureType.E_BATTLE_PROCEDURE_REVIVE_FREE;
	    }
	
	    public override bool Init(BattleProcedureManager manager)
	    {
	        m_Manager = manager;
	        return true;
	    }
	    public override void OnEnter()
	    {
			battleType = Obj_MyselfPlayer.GetMe().battleType;
			changed = false;
			if(battleType != BattleType.CHONG_LOU)
			{
				m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_WAITING);
				return;
			}
			BattleCardManager.Instance.MakeCardStatePrepareRevive();
			BattleCardManager.Instance.ReviveAllCardFree();
	    }
	
	    public override void OnLeave()
	    {
	
	    }
	
	    public override void Update()
	    {
			if(BattleCardManager.Instance.IsAllSelfCardAlive())
			{
				m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_WAITING);
			}
	    }
	}
}	