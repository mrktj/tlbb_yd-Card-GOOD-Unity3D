using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class BattleProcedureCardMeet : BattleProcedureBase
    {

        private BattleProcedureManager m_Manager;
        private float m_DeltaTime = 0f;
        private float m_TotalTime = 1f;

        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_CARD_MEET;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;
            return true;
        }
        public override void OnEnter()
        {
			m_DeltaTime = 0f;
            BattleCardManager.Instance.CreateCard(m_Manager.GetBattlePlayer().GetBattleRoundData().troopData.otherMembers);
			BattleType battleType = Games.CharacterLogic.Obj_MyselfPlayer.GetMe().battleType;
            if (battleType == BattleType.PVP || battleType == BattleType.WORLD_BOSS || battleType == BattleType.QxzbPvP ||
				(battleType == BattleType.PVE && m_Manager.GetBattlePlayer().RoundCounter == 3) ||
				(battleType == BattleType.CHONG_LOU && m_Manager.GetBattlePlayer().GetBattleRoundData().ChonglouLast ))
            {
                //BOSS出场
                AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_boss.ToString(), false, 0, Obj_MyselfPlayer.GetMe().acceleration);
            }
			if(Obj_MyselfPlayer.GetMe().battleType == BattleType.WORLD_BOSS)
			{
				BattleSlot[] battleSlots = BattleCardManager.Instance.BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_OTHER];
				foreach(BattleSlot bs in battleSlots)
				{
					if(bs.Battle_Card != null)
					{
						bs.SetCurHp(Obj_MyselfPlayer.GetMe().worldBossHpBeginBattle,false);
					}
				}
			}
        }
        public override void OnLeave()
        {

        }
        public override void Update()
        {
            m_DeltaTime += Time.deltaTime;
            if (m_DeltaTime >= m_TotalTime)
            {
                m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TIP);
            }
        }
    }
}
