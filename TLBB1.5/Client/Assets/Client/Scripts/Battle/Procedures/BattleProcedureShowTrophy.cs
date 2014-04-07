using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class BattleProcedureShowTrophy : BattleProcedureBase
    {
        private BattleProcedureManager m_Manager;

        private float m_DeltaTime = 0f;
        private float m_TotalTime = 2f;
		private BattleType battleType;
		
        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TROPHY;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;

            return true;
        }
        public override void OnEnter()
        {
			m_DeltaTime = 0f;
			battleType = Obj_MyselfPlayer.GetMe().battleType;
			if (m_Manager.GetBattlePlayer().GetBattleRoundData().isWin && battleType == BattleType.CHONG_LOU)
			{
				//显示本楼层奖励页面
				//免费复活死亡卡牌
				int money = Obj_MyselfPlayer.GetMe().pataRoundRewardMoney;
				int yuanbao = Obj_MyselfPlayer.GetMe().pataRoundRewardYuanbao;
				BattleUI.Instacne.ChonglouReword.GetComponent<ChonglouReword>().Display(money,yuanbao);
				m_TotalTime = 3.0f;
			}
        }
        public override void OnLeave()
        {
            BattleCardManager.Instance.CheckAllCardState();
            if (m_Manager.GetBattlePlayer().GetBattleRoundData().isWin)
            {
                BattleCardManager.Instance.DestroyCard(BattleCardType.E_BATTLE_CARD_TYPE_OTHER);
				
            }
        }
        public override void Update()
        {
            m_DeltaTime += Time.deltaTime;
            if (m_DeltaTime >= m_TotalTime)
            {
                if (m_Manager.GetBattlePlayer().GetBattleRoundData().isWin == false || (m_Manager.GetBattlePlayer().RoundCounter >= 3 
					&& battleType != BattleType.CHONG_LOU))
                {
					Obj_MyselfPlayer.GetMe().pataClearFlag = 1;
                    m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_RESULT);
                }
				else if(battleType == BattleType.CHONG_LOU && Obj_MyselfPlayer.GetMe().pataNum == 100)
				{
					Obj_MyselfPlayer.GetMe().pataClearFlag = 1;
					m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_RESULT);
				}
                else
                {
                    m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_REVIVE_FREE);
                }
            }
        }
    }
}

